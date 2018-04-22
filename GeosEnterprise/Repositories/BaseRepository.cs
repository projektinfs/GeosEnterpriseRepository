using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        protected static void Delete(TEntity entity)
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

        protected static TEntity Insert(TEntity entity)
        {
            entity.CreatedBy = Session.Username;
            entity.CreatedDate = DateTime.Now;
            return _dbContext.Set<TEntity>().Add(entity);
        }

        protected static void Update(TEntity entity)
        {
            entity.ModifiedBy = Session.Username;
            entity.ModifiedDate = DateTime.Now;
            var editObject = GetById(entity.ID);
            _dbContext.Entry(editObject).CurrentValues.SetValues(entity);
        }

        protected static IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(p => p.DeletedDate == null).Where(predicate);
        }

        protected static TEntity ExecuteQuery(Func<TEntity> function, System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.RepeatableRead)
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

        protected static IList<TEntity> ExecuteQuery(Func<IList<TEntity>> function, System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.RepeatableRead)
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

        protected static void ExecuteQuery(Action action, System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.RepeatableRead)
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

    public abstract class BaseRepository<TEntity, DTOEntity> : BaseRepository<TEntity> 
        where TEntity : DBO.DBObject<int> 
        where DTOEntity : DTO.DTOObject<int>
    {
        protected static void Delete(DTOEntity entity)
        {
            var deleteEntity = GetById(entity.ID);
            if (Config.DoNotDeletePermanently)
            {
                deleteEntity.DeletedBy = Session.Username;
                deleteEntity.DeletedDate = DateTime.Now;
            }
            else
            {
                _dbContext.Set<TEntity>().Remove(deleteEntity);
            }
        }

        protected static void Update(DTOEntity entity)
        {
            var editObject = GetById(entity.ID);
            editObject.ModifiedBy = Session.Username;
            editObject.ModifiedDate = DateTime.Now;
            _dbContext.Entry(editObject).CurrentValues.SetValues(entity);
        }
    }
}
