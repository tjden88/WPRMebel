using System.Collections.Generic;
using System.ComponentModel;
using WPRMebel.DB.Entities.Base;

namespace WPRMebel.DB.Entities.Catalog
{
    /// <summary> Список возможных типов секций </summary>
    public enum CatalogSectionTypes
    {
        [Description("Общий")]
        General,
        [Description("Листовой материал")]
        SheetMaterial,
        [Description("Погонный материал")]
        RunningMaterial,
        [Description("Услуги")]
        Service,
        [Description("Расходный материал")]
        Consumable
    }

    /// <summary>
    /// Секция каталога
    /// </summary>
    public class Section : NamedEntity
    {
        /// <summary>Описание секции</summary>
        public string Description { get; set; }

        /// <summary> Тип секции </summary>
        public CatalogSectionTypes SectionType { get; set; }
    }

    /// <summary>
    /// Поставщик
    /// </summary>
    public class Vendor : NamedEntity
    {
        /// <summary>Описание поставщика</summary>
        public string Description { get; set; }

        /// <summary> Категории элементов поставщика </summary>
        public IEnumerable<Category> Categories { get; set; }
    }

    public class Category : NamedEntity
    {
        /// <summary>Поставщик данной категории</summary>
        public Vendor Vendor { get; set; }
    }
}
