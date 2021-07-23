namespace ServiceASP.Bases
{
    public interface IMapper<Entity, Model, Form>
    {
        public Model MapEntityToModel(Entity entity);
        public Form MapModelToForm(Model model);
        public Entity MapFormToEntity(Form form);
        public Entity MapModelToEntity(Model model);
    }
}
