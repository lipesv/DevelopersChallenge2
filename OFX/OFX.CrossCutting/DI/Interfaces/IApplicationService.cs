namespace OFX.CrossCutting.DI.Interfaces
{
    public interface IApplicationService
    {
        IApplicationService Register();
        IApplicationService CreateMapper();

        IContextService ConfigureContext();
        IContextService ApplyMigration();
    }
}
