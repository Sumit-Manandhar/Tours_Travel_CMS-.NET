var AddClassOption = function () {
    var me = this;
    this.init = function () {

        $("#AddClassOptionForm").on("submit", function (e) {
            e.preventDefault();
            if ($("#AddClassOptionForm").valid()) {
                var formData = new FormData();
                var form = $("#AddClassOptionForm");
                var serializeForm = form.serializeArray()
                // Append form data
                $.each(serializeForm, function (index, field) {
                    formData.append(field.name, field.value);
                });

                
                $.ajax({
                    url: '/Admin/MasterData/AddUpdateClassOption',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        
                        if (response.succeeded)
                            showSuccessMessage(response.message, function () {

                                window.location.href = "/admin/Masterdata/ClassOption";
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