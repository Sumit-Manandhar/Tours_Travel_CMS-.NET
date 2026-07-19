var EditPackage = function () {
    handleSubmit = function () {
        $("#submitEditForm").on("click", function (e) {
            e.preventDefault();
            if ($("#updateForm").valid()) {
                var formData = new FormData();
                var form = $("#updateForm");
                var serializeForm = form.serializeArray()
                $.each(serializeForm, function (index, field) {
                    formData.append(field.name, field.value);
                });
                var fileInput = $('#PackageImg')[0];
                if (fileInput.files.length > 0) {
                    formData.append('PackageImg', fileInput.files[0]);
                }
                $.ajax({
                    type: 'Post',
                    url: '/Admin/Package/Create',
                    data: formData,
                    dataType: 'json',
                    processData: false,
                    contentType: false,
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
        initHtmlEditor("#PackageSummary");
        initHtmlEditor("#TermsAndCondition");
        $('#TourTypeId').select2();
        $('#ClassOptionId').select2();
        handleSubmit();
    }
};