using CMSAutomationAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace WBC.Data.Repository
{
    public partial class Repository<T> : IRepository<T> where T : class
    {
        #region Fields

        private readonly DbContext _context;
        private DbSet<T> _entities;

        #endregion

        #region Ctor

        public Repository(DbContext context)
        {
            this._context = context;
        }

        #endregion

        #region Methods


        /// <summary>
        /// Entities
        /// </summary>
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }


        public virtual T GetById(object id)
        {
            return this.GetByIdAsync(id).Result;

        }

        public async Task<T> GetByIdAsync(object id)
        {

            return await this.Entities.FindAsync(id);
        }

        public virtual T Insert(T entity)
        {
            this.Entities.Add(entity);
            this._context.SaveChanges();

            return entity;
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                await this.Entities.AddAsync(entity);
                await this._context.SaveChangesAsync();

                return entity;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        public virtual IEnumerable<T> Insert(IEnumerable<T> entities)
        {
            return this.InsertAsync(entities).Result;
        }

        public virtual async Task<IEnumerable<T>> InsertAsync(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            try
            {

                var validationResults = new List<ValidationResult>();
                foreach (var entity in entities)
                {
                    if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
                    {
                        // throw new ValidationException() or do whatever you want
                    }
                }

                await this.Entities.AddRangeAsync(entities);
                await this._context.SaveChangesAsync();
                return entities;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

        }

        public virtual void Update(T entity)
        {
            try
            {
                this.Entities.Update(entity);
                this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public virtual bool Delete(T entity)
        {

            if (entity == null)
                throw new ArgumentNullException("entity");

            return this.DeleteAsync(entity).Result;

        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {

            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Remove(entity);
            await this._context.SaveChangesAsync();
            return true;
        }

        public virtual bool Delete(IEnumerable<T> entities)
        {

            return this.DeleteAsync(entities).Result;
        }

        public virtual async Task<bool> DeleteAsync(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            this.Entities.RemoveRange(entities);
            await this._context.SaveChangesAsync();
            return true;

        }


        public virtual void Save()
        {
            this._context.SaveChanges();
        }

        public virtual async Task<bool> SaveAsync()
        {
            try
            {
                await this._context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }




        #endregion



        public virtual Tuple<int, int> ExecuteCommand(string commandText)
        {
            var parameterReturn = new NpgsqlParameter
            {
                ParameterName = "jid",
                NpgsqlDbType = NpgsqlDbType.Integer,
                Direction = System.Data.ParameterDirection.Output,
            };
            int afftectRows = this._context.Database.ExecuteSqlRaw(commandText, parameterReturn);
            int returnId = 0;
            if (parameterReturn != null && parameterReturn.Value != null && !string.IsNullOrEmpty(parameterReturn.Value.ToString()) && !string.IsNullOrWhiteSpace(parameterReturn.Value.ToString()))
            {
                returnId = Convert.ToInt32(parameterReturn.Value);
            }
            return Tuple.Create(afftectRows, returnId);
        }


        public virtual int UpdateCommand(string commandText)
        {
            var parameterReturn = new NpgsqlParameter
            {
                ParameterName = "jid",
                NpgsqlDbType = NpgsqlDbType.Integer,
                Direction = System.Data.ParameterDirection.Output,
            };
            int afftectRows = this._context.Database.ExecuteSqlRaw(commandText, parameterReturn);
            return afftectRows;
        }


        public List<T> ExecuteQuery<T>(string query) where T : class, new()
        {
            using (var command = this._context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                this._context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    var lst = new List<T>();
                    var lstColumns = new T().GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToList();
                    while (reader.Read())
                    {
                        var newObject = new T();
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var name = reader.GetName(i);
                            PropertyInfo prop = lstColumns.FirstOrDefault(a => a.Name.ToLower().Equals(name.ToLower()));
                            if (prop == null)
                            {
                                continue;
                            }
                            var val = reader.IsDBNull(i) ? null : reader[i];
                            prop.SetValue(newObject, val, null);
                        }
                        lst.Add(newObject);
                    }
                    return lst;
                }
            }
        }

        public virtual IQueryable<T> SelectQuery(string query)
        {
            return this._context.Set<T>().FromSqlRaw(query).AsQueryable();
        }

     

        // ✅ Multiple Filter Implementation
        public async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            return await Entities.Where(filter).ToListAsync();
        }

        // ✅ Pagination Method Implementation
        public async Task<(IEnumerable<T> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var totalRecords = await Entities.CountAsync();
            var data = await Entities
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, totalRecords);
        }

        public Task<int> ExecuteSqlAsync(string sql, params object[] parameters)
        {
            return _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }
        public virtual int ExecuteRawQuerey(string commandText)
        {
            var afftectRows = this._context.Database.ExecuteSqlRaw(commandText);
            return afftectRows;
        }
        public virtual int ExecuteRawQuery(string commandText, params object[] parameters)
        {
            return _context.Database.ExecuteSqlRaw(commandText, parameters);
        }
    }
}
