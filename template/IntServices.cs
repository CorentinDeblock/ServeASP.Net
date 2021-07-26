using Microsoft.EntityFrameworkCore;
using ServiceASP.Bases;
using ServiceASP.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceASP.template
{
    public interface IIntService<Entity, Model, Form> : IServices<Entity,Model, Form, int> { }
    public abstract class IntServices<DataContext, Entity, Model, Form> :
        BaseServices<DataContext, Entity, Model, Form, int>,
        IIntService<Entity,Model, Form>
        where Entity : class, IModel<int>
        where DataContext : DbContext
    {
        protected IntServices(DataContext dc, IMapper<Entity, Model, Form> mapper) : base(dc, mapper)
        {
        }
    }
}
