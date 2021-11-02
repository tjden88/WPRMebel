using System.ComponentModel.DataAnnotations;
using WPRMebel.Domain.Base.Catalog.Abstract;

namespace WPRMebel.Domain.Base.Catalog
{
    /// <summary>
    /// Значение свойства элемента 
    /// </summary>
    public class ElementPropertyValue : NamedEntity
    {
        /// <summary>Свойство, к которому относится значение</summary>
        [Required]
        public virtual ElementProperty ElementProperty { get; set; }
    }
}