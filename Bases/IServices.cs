using System.Collections.Generic;

namespace ServiceASP.Bases
{
    public interface IServices<Model,Form,IDType>
    {
        public IEnumerable<Model> Get();
        public Model Get(IDType id);
        public Form GetToForm(IDType id);

        public Form Update(IDType id,Form form);
        public Model Insert(Form form);
        public void Delete(IDType id);
    }
}
