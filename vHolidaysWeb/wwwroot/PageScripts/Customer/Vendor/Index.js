var VendorIndex = function () {

    submitHandler = function () {
        $("#creatVendorButton").on("click", function (e) {
            e.preventDefault();
            $.validator.unobtrusive.parse($('#creatVendorForm'))
            var form = $("#creatVendorForm")[0];
            if ($(form).valid()) {
                $.ajax({
                    type: "POST",
                    url: "/Customer/Vendor/Create",
                    data: $(form).serialize(),
                    dataType: "json",
                    success: function (response) {
                        showSuccessMessage(response.message, function () {
                            window.location.href = "/";
                        })
                    },
                    error: function (xhr, status, error) {
                        showErrorMessage(error)
                    }
                });
            }
        });
    };

    countryChangeHandler = function (prefix) {
        $(`#${prefix}CountryId`).on('change', function () {
            var countryId = $(this).val();
            if (countryId) {
                handleCity(prefix);
                handleState(prefix);
            } else {
                $(`#${prefix}CityId`).empty().trigger('change');
                $(`#${prefix}StateId`).empty().trigger('change');
            }
        });
    }

    handleCity = function (prefix, removeDefault) {
        var countryId = $(`#${prefix}CountryId`).val();
        $(`#${prefix}CityId`).empty().trigger('change');
        $.ajax({
            url: '/Admin/Region/GetCities',
            type: 'GET',
            data: { CountryId: countryId },
            dataType: 'json',
            success: function (response) {
                var cityOptions; // Default option
                $.each(response, function (index, city) {
                    cityOptions += '<option value="' + city.value + '">' + city.text + '</option>';
                });
                $(`#${prefix}CityId`).html(cityOptions).trigger('change');
            }
        });
    }

    handleState = function (prefix) {
        var countryId = $(`#${prefix}CountryId`).val();
        $(`#${prefix}StateId`).empty().trigger('change');
        $.ajax({
            url: '/Admin/Region/GetState',
            type: 'GET',
            data: { CountryId: countryId },
            dataType: 'json',
            success: function (response) {
                var stateOptions;
                $.each(response, function (index, state) {
                    stateOptions += '<option value="' + state.value + '">' + state.text + '</option>';
                });
                $(`#${prefix}StateId`).html(stateOptions).trigger('change');
            }
        });
    }

    this.init = function () {
        $('.select2init').select2();
        countryChangeHandler("");
        handleCity("");
        handleState("");
        submitHandler();
        $(`#Destination_0__CountryId  `).trigger('change');
    }
}