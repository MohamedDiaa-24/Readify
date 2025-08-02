using Readify.DataAccess.Repository.Interfaces.IRepository;
using Readify.Models;

namespace Readify.DataAccess.Repository.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company company);
    }
}
