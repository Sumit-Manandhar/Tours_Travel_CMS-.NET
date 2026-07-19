var PackageDetail = function () {
   
    this.init = function () {
        $('#btn-add').on('click', function () {
            window.location.href = `/Admin/Package/Create?isGroup=${isGroupBooking}`;
        })
        $('#btn-search').on('click', function () {
            window.location.href = "/Admin/Package/Index?keyword=" + $('#Keyword').val();
        })
        $('.btn-sync').on('click', function () {
            var id = $(this).data('id');
            var depDate = $(this).data('depDat');
            showConfirmation("Warning", "Are you sure to Sync the Package?", function () {
                $.ajax({
                    url: '/Admin/Package/Sync',
                    type: 'GET',
                    data: { id: id },
                    success: function (response) {
                        $('body').append(response);
                        $('#DepDateModal').modal('show');

                        $('#SyncForm').on("submit", function (e) {
                            e.preventDefault();
                            if ($("#SyncForm").valid()) {
                                var formData = new FormData();
                                var form = $("#SyncForm");
                                var serializeForm = form.serializeArray()
                                // Append form data
                                $.each(serializeForm, function (index, field) {
                                    formData.append(field.name, field.value);
                                });
                                $.ajax({
                                    url: '/Admin/Package/SyncToGroupDeparture',
                                    type: 'POST',
                                    data: formData,
                                    processData: false,
                                    contentType: false,
                                    success: function (response) {
                                        if (response.succeeded) {
                                            $('#DepDateModal').modal('hide');
                                            $('#DepDateModal').remove();
                                            showSuccessMessage(response.message, function () {
                                                window.location.href = '/Admin/Package';
                                            })
                                        }
                                        else
                                            showErrorMessage(response.message)
                                    },
                                    error: function (xhr, status, error) {
                                        showErrorMessage(error)
                                    }
                                });
                            }
                        })
                    },
                    error: function (xhr, status, error) {
                        showErrorMessage(error)
                    }
                });
            });
        })
    }
}