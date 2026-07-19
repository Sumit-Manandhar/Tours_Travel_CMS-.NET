var createHotel = function () {
    var addSubs = false;
    var me = this;
    this.init = function () {
        $('#CountryId').select2();
        $('#CityId').select2();
        $('#CountryId').on('change', function () {
            var countryId = $(this).val();
            if (countryId) {
                $('#CityId').empty().trigger('change');
                $.ajax({
                    url: '/Admin/Region/GetCities',
                    type: 'GET',
                    data: { CountryId: countryId },
                    dataType: 'json',
                    success: function (response) {

                        var cityOptions = '<option value="">Select City</option>'; // Default option
                        $.each(response, function (index, city) {
                            cityOptions += '<option value="' + city.value + '">' + city.text + '</option>';
                        });

                        // Append the city options and trigger change event to update Select2
                        $('#CityId').html(cityOptions).trigger('change');
                    }
                });
            } else {
                // If no country is selected, clear the city dropdown
                $('#CityId').empty().trigger('change');
            }
        });

        if ($('#DisplayImageUrl').val() == '' || $('#DisplayImageUrl').val() == undefined || $('#DisplayImageUrl').val()
            == null) {
            $('.image-container').hide()
        }
        $('#Image').change(function (event) {
            const file = event.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    $('#img').attr('src', e.target.result);
                    $('.image-container').show()

                }
                reader.readAsDataURL(file);
            }
        });
        $("#UpdateHotelForm").on("submit", function (e) {
            e.preventDefault();
            if ($("#UpdateHotelForm").valid()) {
                var formData = new FormData();
                var form = $("#UpdateHotelForm");
                var serializeForm = form.serializeArray()
                // Append form data
                $.each(serializeForm, function (index, field) {
                    formData.append(field.name, field.value);
                });
                var fileInput = $('#Image')[0];
                if (fileInput.files.length > 0) {
                    formData.append('Image', fileInput.files[0]);
                }
                
                $.ajax({
                    url: '/Admin/Hotel/Create',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        
                        if (response.succeeded)
                            showSuccessMessage(response.message, function () {
                                var RedirectUrl = $('#RedirectUrl').val();
                                if (RedirectUrl != null && RedirectUrl != undefined && RedirectUrl != '' && RedirectUrl.length > 0) {
                                     window.location.href = RedirectUrl

                                }
                                else {
                                    window.location.href = "/admin/Hotel/"

                                }
                            })
                        else
                            showErrorMessage(response.message)

                    },
                    error: function (xhr, status, error) {
                        showErrorMessage(error)

                    }
                });
            }
        });
    }
}