using System.Globalization;
using WPR.MVVM.Validation;

namespace WPRMebel.WPF.Views.MainPages
{
    public class VaTest : ValidationBase<string>
    {
        public VaTest() => Message = "Введите более 3 символов";

        protected override bool Validated(string value, CultureInfo cultureInfo) => value.Length > 2;
    }

    public class ValidationRules
    {
        public ValidationBase SearchTextLengthValidationRule => new PredicateValidationRule<string>(s => s.Length > 2, "Введите более 3 символов");
    }
}
