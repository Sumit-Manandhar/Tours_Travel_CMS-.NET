var CreateDestination = function () {
    var toDeleteImages = []
    this.init = function () {
  
        $('.delete-img').on('click', function () {
            var id = $(this).data('id');
            showWarningMessage('Are you sure to delete this Image?', function () { toDeleteImages.push(id); $('#' + id).remove(); })          
        })
        //load drop zone

        loadDropzoneWithCropper('imageUploadContainer', "image", "/admin", 10);
        loadDropzoneWithCropperForSingleFile('displayImgContainer', "image", "/admin", 3 / 2,'#displayImgCont');
        //loadCropperForFileInput('DisplayImg',3 / 4)
        //initialize tinyMCE for description
    
        initHtmlEditor("#createDestinationForm #Description");
        initHtmlEditor("#createDestinationForm #TopDestinations");

        //select2 for City
        $('#CityId').select2({
            tags: true
        })

        //banner image handling
        if ($('#BannerImageUrl').val() == '' || $('#BannerImageUrl').val() == undefined || $('#BannerImageUrl').val()
            == null) {
            $('#image-container').hide()
        }
        //display image handling
        if ($('#DisplayImageUrl').val() == '' || $('#DisplayImageUrl').val() == undefined || $('#DisplayImageUrl').val()
            == null) {
            $('#displayImgCont').hide()
        }

        $('#BannerImg').change(function (event) {
            const file = event.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    $('#img-banner').attr('src', e.target.result);
                    $('#image-container').show()

                }
                reader.readAsDataURL(file);
            }
        });
        //$('#DisplayImg').change(function (event) {
        //    const file = event.target.files[0];
        //    if (file) {
        //        const reader = new FileReader();
        //        reader.onload = function (e) {
        //            $('#img-disp').attr('src', e.target.result);
        //            $('#displayImgCont').show()

        //        }
        //        reader.readAsDataURL(file);
        //    }
        //});
        $("#createDestinationForm").on("submit", function (e) {
            e.preventDefault();
            if ($("#createDestinationForm").valid()) {
                $('#SubDestinationName').val($('#CityId').val())

                var formData = new FormData();
                var form = $("#createDestinationForm");
                var serializeForm = form.serializeArray()
                // Append form data
                $.each(serializeForm, function (index, field) {
                    formData.append(field.name, field.value);
                });
                var fileInput = $('#BannerImg')[0];
                if (fileInput.files.length > 0) {
                    // Append the first file to FormData
                    formData.append('BannerImg', fileInput.files[0]);
                }
          
                var myDropzone = Dropzone.forElement("#displayImgContainer");
                myDropzone.getAcceptedFiles().forEach(function (file, index) {
                    formData.append('DisplayImg', file);


                });
                var myDropzone = Dropzone.forElement("#imageUploadContainer");
                myDropzone.getAcceptedFiles().forEach(function (file, index) {

                    formData.append('Images[' + index + ']', file);

                });
                toDeleteImages.forEach(function (item, index) {
                    formData.append('toDeleteImages[]', item); 
                });

                $.ajax({
                    url: '/Admin/SubDestination/CreateUpdateSubDestination',
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
                        showErrorMessage(error);
                    }
                });
            }
        });
    }
}
