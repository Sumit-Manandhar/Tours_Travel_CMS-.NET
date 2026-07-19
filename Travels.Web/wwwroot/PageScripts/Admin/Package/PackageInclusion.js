var PackageInclusion = function () {
    EditInclusion = function (packageId, Id, isIncluded) {
        $.ajax({
            type: 'Get',
            url: '/Admin/Package/AddUpdateInclusion',
            dataType: 'html',
            data: { PackageId: packageId, Id: Id, isIncludes: isIncluded },
            success: function (content) {
                $('body').append(content);
                $('#addUpdateModal').modal("show");
                tf = new tableForm("#tblItti", "Inclusions", "AC");
                tf.onRowAdded = function (r) {
                    r.find(".requiredField").each(function () {
                        $(this).rules('add', 'required');
                    });

                }      
                tf.init();

                $("#addUpdateInclusion").on("click", function (e) {
                    e.preventDefault();
                    if (!tf.isTemplateRowEmpty(true)) { // skip select
                        tf.addTemplateRowToBody();
                    }
                    $("input[name^='AC.']").each(function () {
                        $(this).removeAttr('required');
                    });
                    if ($("#saveInclusionForm").valid()) {
                        var form = $("#saveInclusionForm")
                        $.ajax({
                            type: 'Post',
                            url: '/Admin/Package/AddUpdateInclusion',
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
        });
    }
    handleIncludionSubmit = function () {
       
    }

    this.init = function () {
        $('#btn-add').on('click', function () {
            var packageId = $('#packageDetailId').val()
            EditInclusion(packageId, 0,true);
        });
        $('#btn-add-ex').on('click', function () {
            var packageId = $('#packageDetailId').val()
            EditInclusion(packageId, 0, false);
        });
    }
}