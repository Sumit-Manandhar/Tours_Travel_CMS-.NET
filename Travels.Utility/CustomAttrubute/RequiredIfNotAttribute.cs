using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Travels.Utility.CustomAttrubute
{
    public class RequiredIfNotAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string _dependentProperty;
        private readonly object _targetValue;

        public RequiredIfNotAttribute(string dependentProperty, object targetValue)
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
            object targetValue; // Convert to property type
            if (property.PropertyType.IsEnum)
                // Convert the integer (or string) to the appropriate enum type
                targetValue = Enum.ToObject(property.PropertyType, _targetValue);
            else
                // For non-enum types, use Convert.ChangeType as usual
                targetValue = Convert.ChangeType(_targetValue, property.PropertyType);

            if ((dependentValue == null && targetValue != null) ||
                (dependentValue != null && !dependentValue.Equals(targetValue)))
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
            MergeAttribute(context.Attributes, "data-val-requiredifnot", ErrorMessage);
            MergeAttribute(context.Attributes, "data-val-requiredifnot-dependentproperty", _dependentProperty);
            MergeAttribute(context.Attributes, "data-val-requiredifnot-targetvalue", _targetValue.ToString());
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
