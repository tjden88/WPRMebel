using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPR.MVVM.ViewModels;
using WPRMebel.Domain.Base.Catalog;
using WPRMebel.Domain.Base.Catalog.Abstract;
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
            OnPropertyChanged(nameof(Sections));
        }

        public IEnumerable<Section> Sections => _SectionRepository.Items.ToArray();

       // public IEnumerable<CatalogElement> CatalogElements =>;


    }
}
