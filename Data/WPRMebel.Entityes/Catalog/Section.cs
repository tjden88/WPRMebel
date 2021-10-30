using System.ComponentModel;
using WPRMebel.Entityes.Base;

namespace WPRMebel.Entityes.Catalog
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
}
