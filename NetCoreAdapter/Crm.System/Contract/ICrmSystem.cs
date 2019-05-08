namespace Crm.System.Contract
{
    public interface ICrmSystem
    {
        ILeadFunctionality LeadFunctionality { get; }
        IContactFunctionality ContactFunctionality { get; }
    }
}
