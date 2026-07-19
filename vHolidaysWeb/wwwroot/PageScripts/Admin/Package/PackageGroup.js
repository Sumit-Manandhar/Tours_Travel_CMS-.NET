var order = [];
var PackageGroup = function () {
    submitGroupOrder = function () {
        $("#submitGroupOrderButton").on('click', function () {
            showConfirmation("Warning", "Are you sure to save the order?", function () {
                $.ajax({
                    url: '/Admin/Package/SaveGroupOrder',
                    type: 'POST',
                    data: { model: order },
                    success: function (response) {
                        if (response.succeeded)
                            showSuccessMessage(response.message, function () {
                                window.location.reload();
                            })
                        else
                            showErrorMessage(response.message)
                    },
                    error: function (xhr, status, error) {
                        showErrorMessage(error)
                    }
                });
            });
        });
    }
    EditGroup = function (packageId, Id) {
        $.ajax({
            type: 'Get',
            url: '/Admin/Package/AddUpdateGroup',
            dataType: 'html',
            data: { PackageId: packageId, Id: Id },
            success: function (content) {
                $('body').append(content);
                $('body').append(content);
                $('#addUpdateModal').modal("show");
                initHtmlEditor("#Description");
                handleGroupSubmit();
            }
        });
    }
    handleGroupSubmit = function () {
        $("#addUpdateGroup").on("click", function (e) {
            e.preventDefault();
            if ($("#saveGroupForm").valid()) {
                var form = $("#saveGroupForm")
                $.ajax({
                    type: 'Post',
                    url: '/Admin/Package/AddUpdateGroup',
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
        $('#btn-add').on('click', function () {
            var packageId = $('#packageDetailId').val()
            EditGroup(packageId, 0);
        });
        $("#sortable").sortable({
            update: function (event, ui) {
                order = []
                $("#sortable tr").each(function (index, element) {
                    order.push({
                        id: $(element).data("id"),
                        ordering: index + 1
                    });
                });
                $("#submitOrderDiv").show();
            }
        });
        submitGroupOrder();
    }
}