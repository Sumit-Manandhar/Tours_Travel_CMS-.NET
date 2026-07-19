var PackageItinerary = function () {
    EditItinerary = function (packageId, Id) {
        $.ajax({
            type: 'Get',
            url: '/Admin/Package/AddUpdateItinerary',
            dataType: 'html',
            data: { PackageId: packageId, Id: Id },
            success: function (content) {
                $('body').append(content);
                $('#addUpdateModal').modal("show");
                initHtmlEditor("#Description");
                initHtmlEditor("#ACtextEdi")

                handleItinerarySubmit();

                tf = new tableForm("#tblBlockAirline", "Sumit", "AC");
                tf.onRowAdded = function (r) {
                    r.find(".requiredField").each(function () {
                        $(this).rules('add', 'required');
                    });

                    initHtmlEditor("#ACtextEdi")
                }
                //tf.onRowAdding = function (r) {

                //}
                tf.init();
            }
        });
    }
    handleItinerarySubmit = function () {
        $("#addUpdateItinerary").on("click", function (e) {
            e.preventDefault();
            if ($("#saveItineraryForm").valid()) {
                var form = $("#saveItineraryForm")
                $.ajax({
                    type: 'Post',
                    url: '/Admin/Package/AddUpdateItinerary',
                    data: form.serialize(),
                    dataType: 'json',
                    success: function (response) {
                        if (response.succeeded)
                            showSuccessMessage(response.message, function () {
                                location.reload();
                            })
                        else
                            showErrorMessage(response.message)
                    }
                });
            }
        });
    }

    this.init = function () {
        
    }
}