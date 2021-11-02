using WPRMebel.Domain.Base.Catalog.Abstract;

namespace WPRMebel.Domain.Base.Catalog
{
    /// <summary>
    /// Погонный материал
    /// </summary>
    /// <remarks>Погонный материал</remarks>
    public class RunningMaterial : CatalogElement
    {
        /// <summary>Длина хлыста, бухты</summary>
        public int? Width { get; set; }
    }
}