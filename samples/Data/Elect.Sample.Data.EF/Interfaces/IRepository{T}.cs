namespace Elect.Sample.Data.EF.Interfaces
{
    public interface IRepository<T> : Elect.Data.EF.Interfaces.Repository.IBaseEntityRepository<T> where T : BaseEntity, new()
    {
    }
}
