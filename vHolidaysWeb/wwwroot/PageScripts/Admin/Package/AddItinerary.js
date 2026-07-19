var PackageItinerary = function () {
   
  
    this.init = function () {
        initHtmlEditor(".upTextedt")
        initHtmlEditor("#ACDescription")
        tf = new tableForm("#tblItti", "Itineraries", "AC");
        tf.onRowAdded = function (r) {
            r.find(".requiredField").each(function () {
                $(this).rules('add', 'required');
            });
            initHtmlEditor(".txtEditor")

        }
        tf.onRowAdding = function (r) {
            r.find("textarea").each(function () {
                let editor = tinymce.get($(this).attr('id'));
                if (editor) {
                    editor.remove();
                }
            });
            return true;
        }
        var allEditor;
        tf.onRowCloning = function (r) {

            r.find("textarea").each(function () {
                let editor = $(this).attr('id');
                allEditor = editor;
                initHtmlEditor("#" + editor)
            });
            return true;
        }

        tf.init();
        $("#addUpdateItinerary").on("click", function (e) {
            e.preventDefault();
            if (!tf.isTemplateRowEmpty()) {
                tf.addTemplateRowToBody();
            }
            // this will remove validation from the footer inuts
            $("input[name^='AC.']").each(function () {
                $(this).removeAttr('required');
            });
            if ($("#saveItineraryForm").valid()) {
            //if ($("#saveItineraryForm").valid()== false) {
               
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
                    },
                    Error: function () {
                        showErrorMessage("Somthing went wrong")
                    }
                });
            }
        });


    }
}