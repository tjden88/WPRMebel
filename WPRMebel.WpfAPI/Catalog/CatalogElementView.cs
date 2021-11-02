using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPRMebel.Domain.Base.Catalog;
using WPRMebel.Domain.Base.Catalog.Abstract;
using WPRMebel.WpfAPI.Catalog.Interfaces;

namespace WPRMebel.WpfAPI.Catalog
{
    public class CatalogElementView : ICatalogElementView
    {
        public CatalogElement GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public CatalogElement GetByName(string Name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CatalogElement> GetFromSection(Section Section)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CatalogElement> GetFromCategory(Category Category)
        {
            throw new NotImplementedException();
        }
    }

    public class CatalogElementView<T> : ICatalogElementView<T> where T : CatalogElement
    {
        public T GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public T GetByName(string Name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetFromSection(Section Section)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetFromCategory(Category Category)
        {
            throw new NotImplementedException();
        }
    }
}
