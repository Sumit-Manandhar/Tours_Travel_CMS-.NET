var PackageImage = function () {
    var toDeleteImages = []

    handleFormSubmit = function () {
        $("#submitEditForm").on('click', function (e) {
            e.preventDefault();
            
            var packageId = $('#PackageDetailId').val();
            var formData = new FormData();
            formData.append("Id", packageId);
            var myDropzone = Dropzone.forElement("#imageUploadContainer");
            myDropzone.getAcceptedFiles().forEach(function (file, index) {
                
                formData.append('Images[' + index + ']', file);
            });
            toDeleteImages.forEach(function (item, index) {
                formData.append('toDeleteImages[]', item);
            });
            $.ajax({
                url: '/Admin/Package/AddUpdateImages',
                type: 'POST',
                data: formData,
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
        });
    }
    this.init = function () {
        $('.delete-img').on('click', function () {
            var id = $(this).data('id');
            showWarningMessage('Are you sure to delete this Image?', function () { toDeleteImages.push(id); $('#' + id).remove(); })
          
        })
        loadDropzoneWithCropper('imageUploadContainer', "image", "/admin", 10);
        handleFormSubmit();
    }
}