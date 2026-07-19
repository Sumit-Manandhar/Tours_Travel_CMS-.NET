var Createbranches = function () {
    var addSubs = false;
    this.init = function () {
        $('#BranchCountry').select2();

        $('#btn-agent').on('click', function () {
            addSubs = true;
            $('#UpdateContactForm').submit();
        })
        //select2 for Country
        $('#CountryId').select2()

        //banner image handling
        if ($('#ImageURL').val() == '' || $('#ImageURL').val() == undefined || $('#ImageURL').val()
            == null) {
            $('.image-container').hide()
        }
        $('#Image').change(function (event) {
            const file = event.target.files[0];
            if (file) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    $('#img').attr('src', e.target.result);
                    $('.image-container').show()

                }
                reader.readAsDataURL(file);
            }
        });
        $("#UpdateContactForm").on("submit", function (e) {
            e.preventDefault();
            if ($("#UpdateContactForm").valid()) {
                var formData = new FormData();
                var form = $("#UpdateContactForm");
                var serializeForm = form.serializeArray()
                // Append form data
                $.each(serializeForm, function (index, field) {
                    formData.append(field.name, field.value);
                });
                var fileInput = $('#Image')[0];
                if (fileInput.files.length > 0) {
                    formData.append('Image', fileInput.files[0]);
                }
                
                $.ajax({
                    url: '/Admin/ContactBranches/SaveBranches',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        
                        if (response.succeeded)
                            showSuccessMessage(response.message, function () {
                                if (addSubs) {
                                    
                                    window.location.href = "/admin/ContactBranches/CreateAgents/?ContactBranchId=" + response.data
                                }
                                else
                                    window.location.href = "/admin/ContactBranches/"
                            })
                        else
                            showErrorMessage(response.message)
                       
                    },
                    error: function (xhr, status, error) {
                        showErrorMessage(error)

                    }
                });
            }
        });
    }
}