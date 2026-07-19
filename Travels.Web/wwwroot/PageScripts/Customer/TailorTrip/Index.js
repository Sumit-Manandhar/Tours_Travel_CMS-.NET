var TailorTrip = function () {
    submitHandler = function () {
        $("#submitTailorMadeButton").on("click", function (e) {
            e.preventDefault();
            $.validator.unobtrusive.parse($('#createtailorTripForm'))
            var form = $("#createtailorTripForm")[0];
            if ($(form).valid()) {
                $.ajax({
                    type: "POST",
                    url: "/Customer/TailorTrip/Create",
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
            } else {
                $(`#${prefix}CityId`).empty().trigger('change');
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
    const showError = (input, msg, errorMsg) => {
        input.classList.add("error");
        errorMsg.innerHTML = msg;
        errorMsg.classList.remove("hide");

    };

    const handleChange = (input, fieldName, errorMsg, iti) => {

        let text;
        isValid = false;
        $(input).removeClass('is-valid').addClass('is-invalid');
        if (input.value) {
            text = iti.isValidNumber() ? "" : "Invalid number - please try again";
        } else {
            text = "Please enter a valid number";
        }
        showError(input, text, errorMsg);
        if (iti.isValidNumber()) {
            isValid = true;
            $(input).removeClass('is-invalid').addClass('is-valid');
            $(fieldName).val(iti.getSelectedCountryData().dialCode);
        }
    };
    this.init = function () {
        $('.select2init').select2();
        countryChangeHandler("");
        handleCity("");
        $('#PersonalDetail_CountryId').select2();
        $('#TravelDate').daterangepicker({
            singleDatePicker: true,
            opens: 'right'
        });
        submitHandler();
        $('#CountryId').trigger('change')

        let contactPhone = document.querySelector("#PersonalDetail_PhoneNumber");
        let errms = document.querySelector("#error-msg")
        iti = window.intlTelInput(contactPhone, {
            separateDialCode: true,
            strictMode: true,
            fixDropdownWidth: true,
            utilsScript: "https://cdn.jsdelivr.net/npm/intl-tel-input@23.1.0/build/js/utils.js",
        });
        $('#PersonalDetail_PhoneNumber').on('change', function () {

            handleChange(contactPhone, '#ContactPhoneCode', errms, iti)
        })
        $('#PersonalDetail_PhoneNumber').on('keyup', function () {

            handleChange(contactPhone, '#ContactPhoneCode', errms, iti)
        })
        $('.iti').css({
            'width': '100%'
        });
    }
}