using WPRMebel.Domain.Base.Catalog.Abstract;

namespace WPRMebel.Domain.Base.Catalog
{
    /// <summary>
    /// Листовой материал
    /// </summary>
    /// <remarks>Листовой материал</remarks>
    public class SheetMaterial : CatalogElement
    {
        /// <summary>Максимальная длина детали</summary>
        public int? DetaliMaxWidth { get; set; } 

        /// <summary>Максимальная ширина детали</summary>
        public int? DetaliMaxHeight { get; set; } 
    }
}