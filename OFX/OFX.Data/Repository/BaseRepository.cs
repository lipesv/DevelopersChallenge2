using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OFX.Data.Context;
using OFX.Data.Repository.Interfaces;
using OFX.Domain.Entities;

namespace OFX.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyContext _context;
        protected readonly DbSet<T> _dataSet;

        public BaseRepository(MyContext context)
        {
            _context = context;
            _dataSet = _context.Set<T>();
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                    item.Id = Guid.NewGuid();

                item.CreateAt = DateTime.Now;

                _dataSet.Add(item);
                await _context.SaveChangesAsync();

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var currentEntity = await _dataSet.SingleOrDefaultAsync(e => e.Id.Equals(item.Id));

                if (currentEntity == null)
                    return null;

                item.UpdateAt = DateTime.Now;
                item.CreateAt = currentEntity.CreateAt;

                _context.Entry(currentEntity).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var currentEntity = await _dataSet.SingleOrDefaultAsync(e => e.Id.Equals(id));

                if (currentEntity == null)
                    return false;

                _dataSet.Remove(currentEntity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _dataSet.SingleOrDefaultAsync(e => e.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            try
            {
                IQueryable<T> query = _dataSet;

                if (disableTracking) query = query.AsNoTracking();

                if (include != null) query = include(query);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await _dataSet.AnyAsync(e => e.Id.Equals(id));
        }

    }
}
