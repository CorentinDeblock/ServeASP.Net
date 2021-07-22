namespace ServiceASP.Bases
{
    public interface IMapper<Entity, Model, Form>
    {
        public Model MapEntityToModel(Entity entity);
        public Entity MapFormToEntity(Form form);
        public Form MapModelToForm(Model model);
    }
}
