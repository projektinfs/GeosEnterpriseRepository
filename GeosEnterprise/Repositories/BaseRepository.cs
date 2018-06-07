using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.Repositories
{
    public abstract class BaseRepository<TEntity>
        where TEntity : DBO.DBObject<int>
    {
        internal static readonly DbContext _dbContext = App.DB;

        public static TEntity Delete(TEntity entity)
        {
            return ExecuteQuery(() =>
            {
                if (Config.DoNotDeletePermanently)
                {
                    entity.DeletedBy = Session.Username;
                    entity.DeletedDate = DateTime.Now;
                    return entity;
                }
                else
                {
                    return _dbContext.Set<TEntity>().Remove(entity);
                }
            });
        }

        public static IList<TEntity> GetAllCurrent()
        {
            return _dbContext.Set<TEntity>().Where(p => p.DeletedDate == null).ToList();
        }

        public static TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public static TEntity Insert(TEntity entity)
        {
            return ExecuteQuery(() =>
            {
                entity.CreatedBy = Session.Username;
                entity.CreatedDate = DateTime.Now;
                return _dbContext.Set<TEntity>().Add(entity);
            });
        }

        public static TEntity Update(TEntity entity)
        {
            return ExecuteQuery(() =>
            {
                entity.ModifiedBy = Session.Username;
                entity.ModifiedDate = DateTime.Now;
                var editObject = GetById(entity.ID);
                _dbContext.Entry(editObject).CurrentValues.SetValues(entity);
                return editObject;
            });
        }

        protected static IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(p => p.DeletedDate == null).Where(predicate);
        }

        protected static TEntity ExecuteQuery(Func<TEntity> function, System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.RepeatableRead)
        {
            string exception = null;
            TEntity result = null;
            using (DbContextTransaction transaction = _dbContext.Database.BeginTransaction(isolationLevel))
            {
                try
                {
                    result = function();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    exception = ex.ToString();
                    transaction.Rollback();
                }
            }
            _dbContext.SaveChanges();
            UseLogger(result?.ID, func: function, error: exception);
            return result;
        }

        protected static IList<TEntity> ExecuteQuery(Func<IList<TEntity>> function, System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.RepeatableRead)
        {
            string exception = null;
            IList<TEntity> result = null;
            using (DbContextTransaction transaction = _dbContext.Database.BeginTransaction(isolationLevel))
            {
                try
                {
                    result = function();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    exception = ex.ToString();
                    transaction.Rollback();
                }
            }
            _dbContext.SaveChanges();
            UseLogger(result?.Count, function: function, error: exception);
            return result;
        }

        protected static void ExecuteQuery(Action action, System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.RepeatableRead)
        {
            string exception = null;
            using (DbContextTransaction transaction = _dbContext.Database.BeginTransaction(isolationLevel))
            {
                try
                {
                    action();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    exception = ex.ToString();
                    transaction.Rollback();
                }
            }
            UseLogger(action: action, error: exception);
            _dbContext.SaveChanges();
        }

        internal static void UseLogger(int? lastParam = 0, Action action = null, Func<IList<TEntity>> function = null, Func<TEntity> func = null, string error = null)
        {
            string methodName = null;
            object item = null;
            string type = null;
            string lastParameter = lastParam.ToString();

            if (action != null)
            {
                methodName = action.Method.Name;
                item = action.Target;
                type = action.Method.ReturnType.Name;
            }
            else if (function != null)
            {
                methodName = function.Method.Name;
                item = function.Target;
                type = function.Method.ReturnType.Name;
                lastParameter = null;
            }
            else if (func != null)
            {
                methodName = func.Method.Name;
                item = func.Target;
                type = func.Method.ReturnType.Name;
            }
            methodName = methodName.Substring(methodName.IndexOf('<') + 1, methodName.IndexOf('>') - 1);

            var t = item.GetType();
            var logText = $"Method: {methodName} | Type: {type}";

            if (!string.IsNullOrEmpty(lastParameter))
            {
                logText = $"{logText} | ID: {lastParameter}";
            }
            else
            {
                logText = $"{logText} | Count: {lastParameter}";
            }
            if (!string.IsNullOrEmpty(error))
            {
                logText = $"Bad request: {error} | {logText}";
            }
            DBO.Log log = new DBO.Log(logText);
            _dbContext.Set<DBO.Log>().Add(log);
        }


    }

    public abstract class BaseRepository<TEntity, DTOEntity> : BaseRepository<TEntity>
        where TEntity : DBO.DBObject<int>
        where DTOEntity : DTO.DTOObject<int>
    {
        public static DTOEntity Delete(DTOEntity entity)
        {
            return ExecuteQuery(() =>
            {
                var deleteEntity = GetById(entity.ID);
                if (Config.DoNotDeletePermanently)
                {
                    deleteEntity.DeletedBy = Session.Username;
                    deleteEntity.DeletedDate = DateTime.Now;
                    return entity;
                }
                else
                {
                    _dbContext.Set<TEntity>().Remove(deleteEntity);
                    return entity;
                }
            });
        }

        public static DTOEntity Update(DTOEntity entity)
        {
            return ExecuteQuery(() =>
            {
                var editObject = GetById(entity.ID);
                editObject.ModifiedBy = Session.Username;
                editObject.ModifiedDate = DateTime.Now;
                _dbContext.Entry(editObject).CurrentValues.SetValues(entity);
                return entity;
            });
        }

        protected static DTOEntity ExecuteQuery(Func<DTOEntity> function, System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.RepeatableRead)
        {
            string exception = null;
            DTOEntity result = null;
            using (DbContextTransaction transaction = _dbContext.Database.BeginTransaction(isolationLevel))
            {
                try
                {
                    result = function();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    exception = ex.ToString();
                    transaction.Rollback();
                }
            }
            _dbContext.SaveChanges();
            UseLogger(result.ID, func: function, error: exception);
            return result;
        }

        internal static void UseLogger(int lastParam = 0, Action action = null, Func<IList<DTOEntity>> function = null, Func<DTOEntity> func = null, string error = null)
        {
            string methodName = null;
            object item = null;
            string type = null;
            string lastParameter = lastParam.ToString();

            if (action != null)
            {
                methodName = action.Method.Name;
                item = action.Target;
                type = action.Method.ReturnType.Name;
            }
            else if (function != null)
            {
                methodName = function.Method.Name;
                item = function.Target;
                type = function.Method.ReturnType.Name;
                lastParameter = null;
            }
            else if (func != null)
            {
                methodName = func.Method.Name;
                item = func.Target;
                type = func.Method.ReturnType.Name;
            }
            methodName = methodName.Substring(methodName.IndexOf('<') + 1, methodName.IndexOf('>') - 1);

            var t = item.GetType();
            var logText = $"Method: {methodName} | Type: {type}";

            if (!string.IsNullOrEmpty(lastParameter))
            {
                logText = $"{logText} | ID: {lastParameter}";
            }
            else
            {
                logText = $"{logText} | Count: {lastParameter}";
            }
            if (!string.IsNullOrEmpty(error))
            {
                logText = $"Bad request: {error} | {logText}";
            }
            DBO.Log log = new DBO.Log(logText);
            _dbContext.Set<DBO.Log>().Add(log);
        }
    }
}
