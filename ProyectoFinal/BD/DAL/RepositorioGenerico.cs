﻿using ProyectoFinal.BD.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TiendaVideoclub.DAL
{
    public class RepositorioGenerico<TEntity> where TEntity : class
    {
        //Repositorio generico que permite el acceso generico a cualquier ta
        protected ClubCineContext context;
        DbSet<TEntity> dbSet;

        public RepositorioGenerico(ClubCineContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public void Actualizar(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Eliminar(TEntity entitytoDelete)
        {
            //if (context.Entry(entitytoDelete).State == EntityState.Detached)
            //{
            //    dbSet.Attach(entitytoDelete);
            //}
            dbSet.Remove(entitytoDelete);

            context.Entry(entitytoDelete).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void EliminarVarios(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = dbSet.Where(predicate).ToList();
            entities.ForEach(x => context.Entry(x).State = EntityState.Deleted);
            context.SaveChanges();
        }

        public void Crear(TEntity entitycrear)
        {
            dbSet.Add(entitycrear);
            context.SaveChanges();
        }

        public List<TEntity> ObtenerTodo()
        {
            return context.Set<TEntity>().ToList();
        }

        public TEntity ObtenerUno(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.FirstOrDefault(predicate);
        }

        public List<TEntity> ObtenerVarios(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        public List<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
    }
}
