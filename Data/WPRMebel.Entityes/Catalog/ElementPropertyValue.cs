using System.ComponentModel.DataAnnotations;
using WPRMebel.Entityes.Base;

namespace WPRMebel.Entityes.Catalog
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