namespace Readify.DataAccess.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        void Save();
    }
}
