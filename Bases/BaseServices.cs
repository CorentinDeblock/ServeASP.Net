using Microsoft.EntityFrameworkCore;
using ServiceASP.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceASP.Bases
{
    public enum CRUDAction
    {
        Create,
        Read,
        Update,
        Delete,
        Conversion
    }

    public abstract class BaseServices<DataContext, Entity, Model, Form, IDType> :
        IServices<Model, Form, IDType>
        where Entity : class, IModel<IDType>
        where IDType : IComparable<IDType>
        where DataContext : DbContext
    {
        protected DataContext Dc;
        protected DbSet<Entity> Set;
        protected IQueryable<Entity> Query;
        protected IMapper<Entity, Model, Form> Mapper;

        public BaseServices(DataContext dc,IMapper<Entity, Model, Form> mapper)
        {
            Dc = dc;
            Set = GetDbSet(dc);
            Query = PrepareQuery(Set);
            Mapper = mapper;
        }

        protected virtual Model MapEntityToModel(Entity entity,CRUDAction action = CRUDAction.Conversion)
        {
            return Mapper.MapEntityToModel(entity);
        }
        protected virtual Entity MapFormToEntity(Form form, CRUDAction action = CRUDAction.Conversion)
        {
            return Mapper.MapFormToEntity(form);
        }

        protected virtual Form MapEntityToForm(Entity entity, CRUDAction action = CRUDAction.Conversion)
        {
            return Mapper.MapModelToForm(Mapper.MapEntityToModel(entity));
        }

        protected abstract DbSet<Entity> GetDbSet(DataContext dc);
        
        protected virtual string InvalidModelError(Exception e) => $"{typeof(Entity).Name} is invalid";
        protected virtual IQueryable<Entity> PrepareQuery(DbSet<Entity> Entity)
        {
            return Set;
        }

        private Model OnSingleEntityModel(Entity entity)
        {
            return MapEntityToModel(entity, CRUDAction.Read);
        }

        private Form OnSingleEntityForm(Entity entity)
        {
            return MapEntityToForm(entity, CRUDAction.Read);
        }

        public virtual IEnumerable<Model> Get()
        {
            return Query.Select(OnSingleEntityModel);
        }

        public virtual Model Get(IDType id)
        {
            return Query
                .Where(a => a.Id.Equals(id))
                .Select(OnSingleEntityModel)
                .SingleOrDefault();
        }

        public virtual Form GetToForm(IDType id)
        {
            return Query
                .Where(a => a.Id.Equals(id))
                .Select(OnSingleEntityForm)
                .SingleOrDefault();
        }

        protected virtual Entity OnInsert(Form form)
        {
            Entity entity = MapFormToEntity(form, CRUDAction.Create);
            Set.Add(entity);
            return entity;
        }
        protected virtual Entity OnUpdate(IDType id,Form form)
        {
            Entity entity = MapFormToEntity(form, CRUDAction.Update);
            entity.Id = id;

            Set.Update(entity);
            return entity;

        }
        protected virtual Entity OnDelete(IDType id)
        {
            Entity entity = Set.FirstOrDefault(model => model.Id.Equals(id));
            Set.Remove(entity);
            return entity;
        }

        public Model Insert(Form form)
        {
            Entity Entity = OnInsert(form);

            Dc.SaveChanges();

            return MapEntityToModel(Entity,CRUDAction.Create); 
        }

        public Form Update(IDType id,Form form)
        {
            Entity Entity = OnUpdate(id,form);

            Dc.SaveChanges();

            return Mapper.MapModelToForm(Mapper.MapEntityToModel(Entity));
        }

        public void Delete(IDType id)
        {
            OnDelete(id);

            Dc.SaveChanges();
        }
    }
}
