using Readify.DataAccess.Data;
using Readify.DataAccess.Repository.Interfaces;
using Readify.Models;

namespace Readify.DataAccess.Repository.Implementaion
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDBContext _dbContext;
        public CompanyRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }



        public void Update(Company company)
        {
            _dbContext.Set<Company>().Update(company);
        }
    }
}
