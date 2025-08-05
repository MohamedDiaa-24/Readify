using Readify.DataAccess.Data;
using Readify.DataAccess.Repository.Interfaces;
using Readify.Models;

namespace Readify.DataAccess.Repository.Implementaion
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDBContext _dbContext;
        public ApplicationUserRepository(ApplicationDBContext dBContext) : base(dBContext)
        {

            _dbContext = dBContext;
        }

    }
}
