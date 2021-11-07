using System;
using System.Collections.Generic;
using WPRMebel.Domain.Base.Catalog;
using WPRMebel.Domain.Base.Catalog.Abstract;

namespace WPRMebel.WpfAPI.Catalog
{
    /// <summary> Типы элементов каталога </summary>
    public enum CatalogElementTypes
    {
        Sheet,
        Running,
        Fitting,
        Service
    }

    /// <summary> Описание типов элементов каталога </summary>
    public class CatalogElementInfo
    {
        private readonly CatalogElementTypes _ElementType;

        public CatalogElementInfo(CatalogElementTypes ElementType) => _ElementType = ElementType;
        
        /// <summary>Имя типа элемента</summary>
        public string Name =>
            _ElementType switch
            {
                CatalogElementTypes.Sheet => "Площадной материал",
                CatalogElementTypes.Running => "Погонный материал",
                CatalogElementTypes.Fitting => "Фурнитура",
                CatalogElementTypes.Service => "Услуги",
                _ => throw new ArgumentOutOfRangeException(),
            };

        /// <summary>Описание</summary> TODO доделать описание
        public string Description =>
            _ElementType switch
            {
                CatalogElementTypes.Sheet => "Площадной материал",
                CatalogElementTypes.Running => "Погонный материал",
                CatalogElementTypes.Fitting => "Фурнитура",
                CatalogElementTypes.Service => "Услуги",
                _ => throw new ArgumentOutOfRangeException(),
            };

        // Factory
        public static CatalogElementInfo GetElementInfo(CatalogElement element)
        {
            if (element is SheetMaterial) return new CatalogElementInfo(CatalogElementTypes.Sheet);
            if (element is RunningMaterial) return new CatalogElementInfo(CatalogElementTypes.Running);
            if (element is Fitting) return new CatalogElementInfo(CatalogElementTypes.Fitting);
            if (element is Service) return new CatalogElementInfo(CatalogElementTypes.Service);

            throw new ArgumentException("Неопознанный тип элемента", nameof(element));
        }
    }

    /// <summary> Все возможные типы в каталоге </summary>
    public class CatalogElementsInfoLocator
    {
        public IEnumerable<CatalogElementInfo> CatalogElementInfos => new List<CatalogElementInfo>()
        {
            new(CatalogElementTypes.Sheet),
            new(CatalogElementTypes.Running),
            new(CatalogElementTypes.Fitting),
            new(CatalogElementTypes.Service)
        };
    }
}
