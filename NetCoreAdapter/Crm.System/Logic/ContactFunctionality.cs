using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crm.System.Contract;
using Crm.System.Contract.Model;

namespace Crm.System.Logic
{
    public class ContactFunctionality : IContactFunctionality
    {
        private static readonly List<Contact> Items = new List<Contact>();
        private int _memberCount;

        /// <inheritdoc />
        public Task<IEnumerable<Contact>> ReadAllAsync()
        {
            return Task.FromResult((IEnumerable<Contact>) Items);
        }

        internal Task<Guid> CreateAsync(Contact item)
        {
            item.Id = Guid.NewGuid();
            _memberCount++;
            item.CustomerNumber = $"Member {_memberCount}";
            Items.Add(item);
            return Task.FromResult(item.Id);
        }

        /// <inheritdoc />
        public Task Delete(Guid id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item != null) Items.Remove(item);
            return Task.CompletedTask;
        }
    }
}