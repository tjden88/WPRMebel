using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WPRMebel.Domain.Base.Catalog.Abstract;

namespace WPRMebel.Domain.Base.Catalog
{
    /// <summary>
    /// Дочерний элемент комплекта
    /// </summary>
    public class ChildCatalogElement : NamedEntity
    {
        /// <summary> Идентификатор элемента - владельца </summary>
        public int OwnerElementId { get; set; }

        /// <summary> Дочерний элемент владельца </summary>
        [Column]
        [Required]
        public virtual CatalogElement CatalogElement { get; set; }

        /// <summary> Количество элементов в комплекте </summary>
        public int Quantity { get; set; } = 1;
    }
}