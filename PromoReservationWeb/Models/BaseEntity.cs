using Microsoft.EntityFrameworkCore;
using PromoReservationWeb.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PromoReservationWeb.Models
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        bool IsDeleted { get; set; }
    }


    public interface IChangeTracker : IBaseEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime LastDateModified { get; set; }
        string CreateUser { get; set; }
        string LastModifiedUser { get; set; }
    }


    public interface IChangeAuditLogger<TBaseEntity> : IChangeTracker where TBaseEntity : IBaseEntity
    {
        ICollection<ChangeAuditLogger<TBaseEntity>> Logs { get; set; }
    }

    public class ChangeAuditLogger<TBaseTransaction> : IChangeTracker where TBaseTransaction : IBaseEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }


        public Guid TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public TBaseTransaction Transaction { get; set; }



        public DateTime CreatedDate { get; set; }
        public DateTime LastDateModified { get; set; }
        public string CreateUser { get; set; }
        public string LastModifiedUser { get; set; }

    }


    public interface IServiceCoreContract<TEntity> where TEntity : class, IBaseEntity
    {
        Task<TEntity> AddEntityAsync(TEntity entity);
        Task UpdateEntityAsync(TEntity entity, TEntity newEntity);
        Task UpdateEntityAsync(TEntity newEntity);
        Task<ICollection<TEntity>> GetListAsync(Func<TEntity, bool> filter);
        Task<TEntity> GetDetails(Guid Id);
        Task<TEntity> GetDetails(Guid Id, bool DetachItem);
        TEntity GetDetails(Func<TEntity, bool> filter);
        bool IsExist(Func<TEntity, bool> filter);
        Task<ICollection<TEntity>> GetAllAsync();
        Task<bool> DeleteEntity(Guid Id);
        Task SaveAsync();
    }


    public abstract class ServiceCoreContract<T> : IServiceCoreContract<T> where T : class, IBaseEntity,new()
    {


        private PromoOrderingContext _entities;


        public ServiceCoreContract(PromoOrderingContext context)
        {
            _entities = context;
        }

        public virtual async Task<T> AddEntityAsync(T entity)
        {

            var data = await _entities.Set<T>().AddAsync(entity);

            await SaveAsync();
            return data.Entity;
        }

        public async Task<bool> DeleteEntity(Guid Id)
        {
            var result = false;
            var item = await this.GetDetails(Id);
            if (item != null)
            {
                item.IsDeleted = true;
                result = true;
                await this.UpdateEntityAsync(item, item);
            }

            return result;
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            var data = await _entities.Set<T>().ToListAsync();
            return data;
        }

        public virtual async Task<T> GetDetails(Guid Id)
        {
            T item = await _entities.Set<T>().FindAsync(Id);
            //_entities.Entry(item).State = EntityState.Detached;
            return item;
        }

        public async Task<T> GetDetails(Guid Id, bool DetachItem)
        {
            if (!DetachItem)
            {
                return await GetDetails(Id);
            }
            else
            {
                var item = await GetDetails(Id);
                if (item != null)
                {
                    _entities.Entry(item).State = EntityState.Detached;
                }

                return item;
            }
        }

        public T GetDetails(Func<T, bool> filter)
        {
            var data = _entities.Set<T>().Where(filter).FirstOrDefault();
            return data;
        }

        public virtual async Task<ICollection<T>> GetListAsync(Func<T, bool> filter)
        {
            var data = _entities.Set<T>().Where(filter).ToList();
            return data;
        }

        public bool IsExist(Func<T, bool> filter)
        {
            var data = _entities.Set<T>().Any(filter);
            return data;
        }

        public virtual async Task SaveAsync()
        {
            await _entities.SaveChangesAsync();
            //  _entities.SaveChanges();
        }

        public virtual async Task UpdateEntityAsync(T entity, T newEntity)
        {
            //T ent;
            //ent = entity;
            _entities.Entry(entity).CurrentValues.SetValues(newEntity);
            var data = _entities.Entry(entity).State = EntityState.Modified;
            await SaveAsync();
        }

        public virtual async Task UpdateEntityAsync(T newEntity)
        {
       
            var oldEntity = await GetDetails(newEntity.Id);
           
          
            await UpdateEntityAsync(oldEntity, newEntity);
        }
    }




}
