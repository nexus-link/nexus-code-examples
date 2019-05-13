using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.NexusAdapter.Contract;
using Crm.System.Contract;
using Crm.System.Contract.Exceptions;
using Crm.System.Contract.Model;

namespace Crm.System.Sdk
{
    public class LeadFunctionality : ILeadFunctionality
    {
        private readonly ContactFunctionality _contactFunctionality;
        private readonly IAdapterService _adapterService;
        private static readonly List<Lead> Items = new List<Lead>();


        public LeadFunctionality(ContactFunctionality contactFunctionality, IAdapterService adapterService)
        {
            _contactFunctionality = contactFunctionality;
            _adapterService = adapterService;
        }
        /// <inheritdoc />
        public Task<IEnumerable<Lead>> ReadAllAsync()
        {
            return Task.FromResult((IEnumerable<Lead>)Items);
        }

        internal Task<Lead> ReadAsync(Guid id)
        {
            return Task.FromResult(Items.FirstOrDefault(i => i.Id == id));
        }

        /// <inheritdoc />
        public Task<Guid> CreateAsync(Lead item)
        {
            item.Id = Guid.NewGuid();
            item.Status = Lead.StatusEnum.Active;
            item.UpdatedAt = DateTimeOffset.Now;
            Items.Add(item);
            return Task.FromResult(item.Id);
        }

        /// <exception cref="BusinessRuleException"></exception>
        /// <inheritdoc />
        public async Task<Guid> QualifyAsync(Guid id)
        {
            var lead = await ReadAsync(id);
            if (lead == null) throw new NotFoundException("Lead not found");
            switch (lead.Status)
            {
                case Lead.StatusEnum.Active:
                    lead.Status = Lead.StatusEnum.Qualified;
                    lead.Reason = "Qualified";
                    lead.UpdatedAt = DateTimeOffset.Now;
                    var contact = new Contact
                    {
                        Name = lead.Name,
                        OriginatingLeadId = lead.Id
                    };
                    var contactId = await _contactFunctionality.CreateAsync(contact);
                    await _adapterService.LeadWasQualified(lead.Id, contactId, lead.UpdatedAt);
                    return contactId;
                case Lead.StatusEnum.Qualified:
                    throw new BusinessRuleException(1, "The lead has already been qualified.");
                case Lead.StatusEnum.Rejected:
                    throw new BusinessRuleException(2, "The lead has already been rejected.");
                default:
                    throw new ProgrammersErrorException($"Unknown lead status: {lead.Status}");
            }
        }

        /// <inheritdoc />
        public async Task RejectAsync(Guid id, string reason)
        {
            var lead = await ReadAsync(id);
            if (lead == null) throw new NotFoundException("Lead not found");
            switch (lead.Status)
            {
                case Lead.StatusEnum.Active:
                    lead.Status = Lead.StatusEnum.Rejected;
                    lead.Reason = reason;
                    lead.UpdatedAt = DateTimeOffset.Now;
                    return;
                case Lead.StatusEnum.Qualified:
                    throw new BusinessRuleException(1, "The lead has already been qualified.");
                case Lead.StatusEnum.Rejected:
                    // Idempotent - already rejected
                    return;
                default:
                    throw new ProgrammersErrorException($"Unknown lead status: {lead.Status}");
            }
        }

        /// <inheritdoc />
        public Task DeleteAll()
        {
            Items.Clear();
            return Task.CompletedTask;
        }
    }
}