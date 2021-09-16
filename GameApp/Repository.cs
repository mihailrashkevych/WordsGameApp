using GameApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp
{
    class Repository<TEntity> where TEntity : class
    {
        GameContext _context;
        DbSet<TEntity> _dbSet;

        public Repository()
        {
            _context = new GameContext();
            _dbSet = _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity> Create(TEntity item)
        {
            var entry = await _dbSet.AddAsync(item);
            _context.SaveChanges();
            return entry.Entity;
        }
        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
    }
}
