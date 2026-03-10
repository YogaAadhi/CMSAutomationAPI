using CMSAutomationAPI.Model;
using CMSAutomationAPI.Repository;

namespace CMSAutomationAPI.Services
{
    public class CptCodesMasterDeltaService
    {
        IRepository<CPT_CodesMasterDelta> _repository;

        private readonly RepositoryFactory<CPT_CodesMasterDelta> _factory;

        public CptCodesMasterDeltaService(RepositoryFactory<CPT_CodesMasterDelta> factory)
        {
            _factory = factory;
            _repository = _factory.CreateForWbc();
        }

        public IQueryable<CPT_CodesMasterDelta> GetAll(bool? isActive = null,
            string?[] hcpc_codes = null, string? status = null)
        {
            var data = _repository.Table.AsQueryable();

            if (hcpc_codes != null && hcpc_codes.Any())
            {
                data = data.Where(a => hcpc_codes.Contains(a.CptCode));
            }

            if (!string.IsNullOrEmpty(status))
            {
                data = data.Where(a => a.status == status);
            }

            return data;
        }

        public int ExecuteSqlAsync(string sql, params object[] parameters)
        {
            return _repository.ExecuteRawQuery(sql, parameters);
        }


    }
}
