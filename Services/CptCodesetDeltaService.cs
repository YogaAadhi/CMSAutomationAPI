using CMSAutomationAPI.Model;
using CMSAutomationAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace CMSAutomationAPI.Services
{
    public class CptCodesetDeltaService
    {
        IRepository<CptCodesetDelta> _repository;

        private readonly RepositoryFactory<CptCodesetDelta> _factory;

        public CptCodesetDeltaService(RepositoryFactory<CptCodesetDelta> factory)
        {
            _factory = factory;
            _repository = _factory.CreateForWbc();






        }

        public IQueryable<CptCodesetDelta> GetAll(bool? isActive = null,
            string?[] hcpc_codes = null, string? status = null)
        {
            var data = _repository.Table.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                data = data.Where(a => a.Status == status);
            }

            if (hcpc_codes != null && hcpc_codes.Any())
            {
                data = data.Where(a => hcpc_codes.Contains(a.CptCode));
            }

            return data;
        }


        public int ExecuteSqlAsync(string sql, params object[] parameters)
        {
            return _repository.ExecuteRawQuery(sql, parameters);
        }
    }
}
