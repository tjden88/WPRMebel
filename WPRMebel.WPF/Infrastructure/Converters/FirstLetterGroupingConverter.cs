using System;
using System.Globalization;
using WPR.MVVM.Converters;

namespace WPRMebel.WPF.Infrastructure.Converters
{
    /// <summary> Фильтр строки, возвращает первый символ </summary>
    internal class FirstLetterGroupingConverter : ConverterBase
    {
        protected override object Convert(object v, Type t, object p, CultureInfo c) => v is string {Length: > 0} s ? s[..1] : string.Empty;
    }
}
