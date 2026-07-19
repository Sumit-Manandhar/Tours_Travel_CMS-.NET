var PackageFlight = function () {
   
  
    this.init = function () {
        tf = new tableForm("#tblItti", "Flights", "AC");
        tf.onRowAdded = function (r) {
            r.find(".requiredField").each(function () {
                $(this).rules('add', 'required');
            });

        }      

        tf.init();
        $("#addUpdateItinerary").on("click", function (e) {
            e.preventDefault();
            if (!tf.isTemplateRowEmpty(true)) { // skip select
                tf.addTemplateRowToBody();
            }
            // this will remove validation from the footer inuts
            $("select[name$='.IsReturn']").each(function () {
                // Find the nearest input with name containing 'ReturnDate'
                var selectedOp = $(this).val();
                var returnDateInput = $(this).closest("tr").find("input[name*='ReturnDate']");

                if (selectedOp == 'True') {

                    $(returnDateInput).prop("required", true);

                }
                else {

                    $(returnDateInput).removeAttr('required');

                }
            });
            $("input[name^='AC.']").each(function () {
                $(this).removeAttr('required');
            });
            if ($("#saveItineraryForm").valid()) {
            //if ($("#saveItineraryForm").valid()== false) {
               
                var form = $("#saveItineraryForm")
                $.ajax({
                    type: 'Post',
                    url: '/Admin/Package/AddUpdateFlight',
                    data: form.serialize(),
                    dataType: 'json',
                    success: function (response) {
                        if (response.succeeded)
                            showSuccessMessage(response.message, function () {
                                location.reload();
                            })
                        else
                            showErrorMessage(response.message)
                    },
                    Error: function () {
                        showErrorMessage("Somthing went wrong")
                    }
                });
            }
        });


    }
}