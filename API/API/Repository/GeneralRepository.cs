using API.Context;
using API.Controllers;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;
        private AccountsController accountController;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        public GeneralRepository(AccountsController accountController)
        {
            this.accountController = accountController;
        }

        public int Delete(Key key)
        {
            var find = entities.Find(key);
            if (find == null)
            {
                return 0;
            }
            entities.Remove(find);
            var result = myContext.SaveChanges();
            return result;
        }

        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
            
        }

        public Entity Get(Key key)
        {
            var result = entities.Find(key);
            return result;
        }

        public int Insert(Entity entity)
        {
            entities.Add(entity);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Update(Entity entity)
        {
            var find = entities.Find(entity);
            if (find == null)
            {
                return 0;
            }
            myContext.Entry(entity).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
            
        }
    }
}
