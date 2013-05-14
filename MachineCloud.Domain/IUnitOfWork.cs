namespace MachineCloud.Domain
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
