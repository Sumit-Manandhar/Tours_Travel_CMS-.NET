var contacts =  function () {
    var me = this;

    this.init = function(){

        $("#UpdateContactForm").on("submit", function (e) {
            e.preventDefault();
            if ($("#UpdateContactForm").valid()) {
                var formData = new FormData();
                var form = $("#UpdateContactForm");
                var serializeForm = form.serializeArray()
                // Append form data
                $.each(serializeForm, function (index, field) {
                    formData.append(field.name, field.value);
                });
                
                $.ajax({
                    url: '/Admin/Contact/SaveContact',
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