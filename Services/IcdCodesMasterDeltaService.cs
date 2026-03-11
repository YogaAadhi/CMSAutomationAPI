using CMSAutomationAPI.Model;
using CMSAutomationAPI.Repository;

namespace CMSAutomationAPI.Services
{
    public class IcdCodesMasterDeltaService
    {

        IRepository<ICD_CodesMasterDelta> _repository;

        private readonly RepositoryFactory<ICD_CodesMasterDelta> _factory;

        public IcdCodesMasterDeltaService(RepositoryFactory<ICD_CodesMasterDelta> factory)
        {
            _factory = factory;
            _repository = _factory.CreateForWbc();
        }

        public IQueryable<ICD_CodesMasterDelta> GetAll(bool? isActive = null,
         string?[] icd_codes = null, string? status = null)
        {
            var data = _repository.Table.AsQueryable();

            if (icd_codes != null && icd_codes.Any())
            {
                data = data.Where(a => icd_codes.Contains(a.IcdCode));
            }

            if (!string.IsNullOrEmpty(status))
            {
                data = data.Where(a => a.Status == status);
            }

            return data;
        }

        public int ExecuteSqlAsync(string sql, params object[] parameters)
        {
            return _repository.ExecuteRawQuery(sql, parameters);
        }


    }
}
