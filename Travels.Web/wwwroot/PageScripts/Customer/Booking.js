var Booking = function () {
    this.init = function () {
        initDatePicker('input[name="ArrivalDate"]');
        initDatePicker('input[name="DepartureDate"]');
        

        $("#BookNowForm").on("submit", function (e) {
            e.preventDefault();
            if ($("#BookNowForm").valid()) {
                var formData = new FormData();
                var form = $("#BookNowForm");
                var serializeForm = form.serializeArray()
                // Append form data
                $.each(serializeForm, function (index, field) {
                    formData.append(field.name, field.value);
                });

                $.ajax({
                    url: '/Customer/Package/SubmitBooking',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        
                        showSuccessMessage(response.message, function () {
                            location.reload();
                        })
                    },
                    error: function (xhr, status, error) {
                        showErrorMessage(error)
                    }
                });
            }
        });
    }
}