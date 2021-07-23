using System.Collections.Generic;

namespace ServiceASP.Bases
{
    public interface IServices<Model,Form,IDType>
    {
        public IEnumerable<Model> Get();
        public Model Get(IDType id);
        public Model Get(Form form);
        public Form GetToForm(IDType id);
        public void Save();

        public Form Update(IDType id,Form form);
        public Model Insert(Form form);
        public void Delete(IDType id);
        public bool Has(IDType id);
        public bool Has(Model Model);
        public bool Has(Form form);
    }
}
