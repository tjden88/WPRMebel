using System.Collections.ObjectModel;
using WPRMebel.Domain.Base.Catalog;

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
                Description = "Основные листовые материалы для производства каркасов, фасадов и т.д."
            },
            new()
            {
                Name = "Погонный материал",
                Description = "Погонные материалы и фурнитура (профили, трубы, столешницы и т.д.)"
            },
            new()
            {
                Name = "Кромкооблицовочный материал",
                Description = "Кромка и торцевые облицовочные материалы"
            },
            new()
            {
                Name = "Фурнитура",
                Description = "Различные элементы фурнитуры для производства мебели"
            },
            new()
            {
                Name = "Стяжки",
                Description = "Стяжная фурнитура"
            },
            new()
            {
                Name = "Услуги, операции",
                Description = "Работы мебельного цеха, услуги по доставке, сборке"
            },
            new()
            {
                Name = "Расходные материалы",
                Description = "Картон, стрейч-плёнка и т.д."
            }
        };
    }
}
