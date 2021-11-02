using WPR.MVVM.ViewModels;
using WPRMebel.Domain.Base.Catalog;
using WPRMebel.Interfaces.Base.Repositories;
using WPRMebel.WpfAPI.Catalog.Interfaces;

namespace WPRMebel.WPF.ViewModels.MainPages
{
    internal class CatalogViewModel : ViewModel
    {
        private readonly IRepository<Section> _SectionRepository;
        private readonly ICatalogElementView _CatalogElementView;

        public CatalogViewModel(IRepository<Section> SectionRepository, ICatalogElementView CatalogElementView)
        {
            _SectionRepository = SectionRepository;
            _CatalogElementView = CatalogElementView;
        }
    }
}
