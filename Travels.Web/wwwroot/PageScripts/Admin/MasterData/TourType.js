var CreateTours = function () {
    var me = this;
    this.init = function () {

        $("#AddTourTypesForm").on("submit", function (e) {
            e.preventDefault();
            if ($("#AddTourTypesForm").valid()) {
                var formData = new FormData();
                var form = $("#AddTourTypesForm");
                var serializeForm = form.serializeArray()
                // Append form data
                $.each(serializeForm, function (index, field) {
                    formData.append(field.name, field.value);
                });

                
                $.ajax({
                    url: '/Admin/MasterData/AddUpdateTour',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        
                        if (response.succeeded)
                            showSuccessMessage(response.message, function () {

                                window.location.href = "/admin/Masterdata/";
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