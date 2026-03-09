using CMSAutomationAPI.Model;
using CMSAutomationAPI.Repository;

namespace CMSAutomationAPI.Services
{
    public class IcdCodesetDeltaService
    {

        IRepository<ICD_CodeSetDelta> _repository;

        private readonly RepositoryFactory<ICD_CodeSetDelta> _factory;

        public IcdCodesetDeltaService(RepositoryFactory<ICD_CodeSetDelta> factory)
        {
            _factory = factory;
            _repository = _factory.CreateForWbc();
        }

        public IQueryable<ICD_CodeSetDelta> GetAll(bool? isActive = null,
            string?[] icd_codes = null, string? status = null)
        {
            var data = _repository.Table.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                data = data.Where(a => a.Status == status);
            }

            if (icd_codes != null && icd_codes.Any())
            {
                data = data.Where(a => icd_codes.Contains(a.IcdCode));
            }

            return data;
        }



        public int ExecuteSqlAsync(string sql, params object[] parameters)
        {
            return _repository.ExecuteRawQuery(sql, parameters);
        }


    }
}
