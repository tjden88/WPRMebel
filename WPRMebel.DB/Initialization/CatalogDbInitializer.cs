using System.Collections.ObjectModel;
using WPRMebel.Entityes.Catalog;

namespace WPRMebel.DB.Initialization
{
    /// <summary>
    /// Инициализация начальных параметров БД каталога
    /// </summary>
    internal static class CatalogDbInitializer
    {
        public static Collection<Section> InitSections => new()
        {
            new()
            {
                Name = "Листовой материал",
                Description = "Основные листовые материалы для производства каркасов, фасадов и т.д.",
                SectionType = CatalogSectionTypes.SheetMaterial
            },
            new()
            {
                Name = "Погонный материал",
                Description = "Погонные материалы и фурнитура (профили, трубы, столешницы и т.д.)",
                SectionType = CatalogSectionTypes.RunningMaterial
            },
            new()
            {
                Name = "Кромкооблицовочный материал",
                Description = "Кромка и торцевые облицовочные материалы",
                SectionType = CatalogSectionTypes.RunningMaterial
            },
            new()
            {
                Name = "Фурнитура",
                Description = "Различные элементы фурнитуры для производства мебели",
                SectionType = CatalogSectionTypes.General
            },
            new()
            {
                Name = "Стяжки",
                Description = "Стяжная фурнитура",
                SectionType = CatalogSectionTypes.Consumable
            },
            new()
            {
                Name = "Услуги, операции",
                Description = "Работы мебельного цеха, услуги по доставке, сборке",
                SectionType = CatalogSectionTypes.Service
            },
            new()
            {
                Name = "Расходные материалы",
                Description = "Картон, стрейч-плёнка и т.д.",
                SectionType = CatalogSectionTypes.Consumable
            }
        };
    }
}
