using System;
using System.Collections.Generic;

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
