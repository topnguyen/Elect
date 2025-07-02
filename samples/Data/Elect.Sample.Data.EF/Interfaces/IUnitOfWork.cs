namespace Elect.Sample.Data.EF.Interfaces
{
    public interface IUnitOfWork : Elect.Data.EF.Interfaces.UnitOfWork.IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity, new();
    }
}
