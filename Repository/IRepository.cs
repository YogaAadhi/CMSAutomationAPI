using System.Linq.Expressions;

namespace CMSAutomationAPI.Repository
{
    public partial interface IRepository<T> where T : class
    {

        T GetById(object id);

        Task<T> GetByIdAsync(object id);

        T Insert(T entity);

        Task<T> InsertAsync(T entity);

        IEnumerable<T> Insert(IEnumerable<T> entities);

        Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities);

        void Update(T entity);

        bool Delete(T entity);

        Task<bool> DeleteAsync(T entity);

        bool Delete(IEnumerable<T> entities);

        Task<bool> DeleteAsync(IEnumerable<T> entities);

        void Save();

        Task<bool> SaveAsync();

        IQueryable<T> Table { get; }

        IQueryable<T> TableNoTracking { get; }
        Tuple<int, int> ExecuteCommand(string commandText);
        int UpdateCommand(string commandText);
        IQueryable<T> SelectQuery(string query);
        List<T> ExecuteQuery<T>(string query) where T : class, new();
      
        // IQueryable<TModel> ExecuteRaw<TModel>(string script, params MysqlParameterModel[] sqlParameters) where TModel : class;
        //int ExecuteCommand(string commandText, params SqlParameter[] sqlParameters);
        

        // ✅ Pagination Method Added Below
        Task<(IEnumerable<T> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);

        // ✅ Multiple Field Filtering Support
        Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter);
        int ExecuteRawQuerey(string commandText);
        int ExecuteRawQuery(string commandText, params object[] parameters);



    }
}
