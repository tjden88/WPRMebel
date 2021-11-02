using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.Domain.Base.Catalog;
using WPRMebel.Domain.Base.Catalog.Abstract;
using WPRMebel.Interfaces.Base.Repositories;
using WPRMebel.WpfAPI.Catalog.Interfaces;

namespace WPRMebel.WpfAPI.Catalog
{
    public class CatalogElementView : ICatalogElementView
    {
        private readonly INamedRepository<CatalogElement> _Repository;

        public CatalogElementView(INamedRepository<CatalogElement> Repository) => _Repository = Repository;

        public async Task<CatalogElement> GetById(int Id, CancellationToken Cancel = default) => await _Repository.GetByIdAsync(Id, Cancel);
        public async Task<CatalogElement> GetByName(string Name, CancellationToken Cancel = default) => await _Repository.GetByNameAsync(Name, Cancel);

        public async Task<IEnumerable<CatalogElement>> GetFromSection(Section Section, CancellationToken Cancel = default) =>
            await _Repository.Items.Where(element => element.Category.Section == Section).ToArrayAsync(Cancel);

        public async Task<IEnumerable<CatalogElement>> GetFromCategory(Category Category, CancellationToken Cancel = default) =>
            await _Repository.Items.Where(element => element.Category == Category).ToArrayAsync(Cancel);
    }

    public class CatalogElementView<T> : ICatalogElementView<T> where T : CatalogElement
    {
        public Task<T> GetById(int Id, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByName(string Name, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetFromSection(Section Section, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetFromCategory(Category Category, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
