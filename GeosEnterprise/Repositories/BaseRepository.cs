using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GeosEnterprise.Repositories
{
    public class BaseRepository<TEntity> where TEntity : DBO.DBObject<int>
    {
        private static readonly DbContext _dbContext = App.DB;

        public static void Delete(TEntity entity)
        {
            if (Config.DoNotDeletePermanently)
            {
                entity.DeletedBy = Session.Username;
                entity.DeletedDate = DateTime.Now;
            }
            else
            {
                _dbContext.Set<TEntity>().Remove(entity);
            }
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
            return _dbContext.Set<TEntity>().Add(entity);
        }

        public static void Update(TEntity entity)
        {
            var editObject = GetById(entity.ID);
            _dbContext.Entry(editObject).CurrentValues.SetValues(entity);
        }

        public static IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(p => p.DeletedDate == null).Where(predicate);
        }

        public static TEntity ExecuteQuery(Func<TEntity> function, System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.RepeatableRead)
        {
            TEntity result = null;
            using (DbContextTransaction transaction = _dbContext.Database.BeginTransaction(isolationLevel))
            {
                try
                {
                    result = function();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            } 
            _dbContext.SaveChanges();
            return result;
        }

        public static IList<TEntity> ExecuteQuery(Func<IList<TEntity>> function, System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.RepeatableRead)
        {
            IList<TEntity> result = null;
            using (DbContextTransaction transaction = _dbContext.Database.BeginTransaction(isolationLevel))
            {
                try
                {
                    result = function();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
            _dbContext.SaveChanges();
            return result;
        }

        public static void ExecuteQuery(Action action, System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.RepeatableRead)
        {
            using (DbContextTransaction transaction = _dbContext.Database.BeginTransaction(isolationLevel))
            {
                try
                {
                    action();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
