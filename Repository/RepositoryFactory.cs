using CMSAutomationAPI.Data;
using WBC.Data.Repository;

namespace CMSAutomationAPI.Repository
{
    public class RepositoryFactory<T> where T : class
    {
        private readonly AppDbContext _wbcContext;
      
        public RepositoryFactory(AppDbContext wbcContext)
        {
            _wbcContext = wbcContext;           
        }
        public IRepository<T> CreateForWbc() => new Repository<T>(_wbcContext);
       
    }
}
