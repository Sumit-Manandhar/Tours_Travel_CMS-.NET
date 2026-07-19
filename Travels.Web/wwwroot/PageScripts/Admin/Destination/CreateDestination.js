var CreateDestination = function () {
    var addSubs = false;
    this.init = function () {


        initHtmlEditor("#createDestinationForm #Description");
        initHtmlEditor("#createDestinationForm #Overview");
        //select2 for Country
        $('#CountryId').select2()

        //banner image handling
        if ($('#BannerImgUrl').val() == '' || $('#BannerImgUrl').val() == undefined || $('#BannerImgUrl').val()
            == null) {
            $('.image-container').hide()
        }
        $('#btn-addsubs').on('click', function () {
            addSubs = true;
            $("#createDestinationForm").submit();
        })
        $('#BannerImg').change(function (event) {
            const file = event.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    $('#img-banner').attr('src', e.target.result);
                    $('.image-container').show()

                }
                reader.readAsDataURL(file);
            }
        });
        $("#createDestinationForm").on("submit", function (e) {
            e.preventDefault();
            if ($("#createDestinationForm").valid()) {
                var formData = new FormData();
                var form = $("#createDestinationForm");
                var serializeForm = form.serializeArray()
                // Append form data
                $.each(serializeForm, function (index, field) {
                    formData.append(field.name, field.value);
                });
                var desc = tinymce.get('Description').getContent();
                var over = tinymce.get('Overview').getContent();
                var fileInput = $('#BannerImg')[0];
                if (fileInput.files.length > 0) {
                    // Append the first file to FormData
                    formData.append('BannerImg', fileInput.files[0]);
                }
                formData.append('Overview', over);
                formData.append('Description', desc);
                
                $.ajax({
                    url: '/Admin/Destination/CreateUpdateDestination',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        
                        showSuccessMessage(response.message, function () {
                            if (addSubs) {
                                
                                window.location.href = "/admin/SubDestination/create/?DestinationId=" + response.id
                            }
                            else
                                location.reload();
                        })
                    },
                    error: function (xhr, status, error) {
                        showErrorMessage(error)
                    }
                });
            }
        });
    }
}