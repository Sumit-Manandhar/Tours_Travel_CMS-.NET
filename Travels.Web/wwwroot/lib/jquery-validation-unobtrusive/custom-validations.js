$.validator.addMethod("requiredifnot", function (value, element, params) {
    var dependentPropertyId = params.dependentproperty.replace(/([:.])/g, '\\$1');
    var targetValue = params.targetvalue; // target value as string, e.g., "Package"

    // Get the selected value of the dependent dropdown
    var actualValue = $('#' + dependentPropertyId).val();

    // Check if actual value does NOT match target value (non-equality check)
    return actualValue !== targetValue ? $.trim(value).length > 0 : true;
});

$.validator.unobtrusive.adapters.add("requiredifnot", ["dependentproperty", "targetvalue"], function (options) {
    options.rules["requiredifnot"] = {
        dependentproperty: options.params.dependentproperty,
        targetvalue: options.params.targetvalue
    };
    options.messages["requiredifnot"] = options.message;
});