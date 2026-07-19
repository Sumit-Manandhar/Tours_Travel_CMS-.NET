using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Travels.Utility.CustomAttrubute
{
    public class RequiredIfAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string _dependentProperty;
        private readonly object _targetValue;

        public RequiredIfAttribute(string dependentProperty, object targetValue)
        {
            _dependentProperty = dependentProperty;
            _targetValue = targetValue;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_dependentProperty);
            if (property == null)
            {
                return new ValidationResult($"Unknown property: {_dependentProperty}");
            }

            var dependentValue = property.GetValue(validationContext.ObjectInstance);
            if ((dependentValue == null && _targetValue == null) ||
                (dependentValue != null && dependentValue.Equals(_targetValue)))
            {
                if (value == null)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-requiredif", ErrorMessage);
            MergeAttribute(context.Attributes, "data-val-requiredif-dependentproperty", _dependentProperty);
            MergeAttribute(context.Attributes, "data-val-requiredif-targetvalue", _targetValue?.ToString());
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }
    }
}
