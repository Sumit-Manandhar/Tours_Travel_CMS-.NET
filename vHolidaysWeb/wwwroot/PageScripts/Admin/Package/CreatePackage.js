var CreatePackage = function () {
    handleSubmit = function () {
        $("#createForm").on("submit", function (e) {

            e.preventDefault();
            if ($("#createForm").valid()) {
                var formData = new FormData();
                var form = $("#createForm");
                var serializeForm = form.serializeArray()
                $.each(serializeForm, function (index, field) {
                    formData.append(field.name, field.value);
                });

                var fileInput = $('#PackageImg')[0];
                if (fileInput.files.length > 0) {
                    formData.append('PackageImg', fileInput.files[0]);
                }
                $.ajax({
                    url: '/Admin/Package/Create',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        if (response.succeeded) {
                            var type = $('#PackageType').val();
                            let controller = type == '2' ? 'GroupBooking' : 'Package';
                            showSuccessMessage(response.message, function () {
                                window.location.href = `/Admin/${controller}/Edit/${response.data}`
                            })
                        } else
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
        $('#StartDate').daterangepicker({
            singleDatePicker: true,
            opens: 'right'
        });
        handleSubmit();
    }
}