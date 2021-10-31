using WPRMebel.Entityes.Base;

namespace WPRMebel.Entityes.Catalog
{
    /// <summary>
    /// Значение свойства элемента 
    /// </summary>
    public class ElementPropertyValue : NamedEntity
    {
        /// <summary>Свойство, к которому относится значение</summary>
        public ElementProperty ElementProperty { get; set; }
    }
}