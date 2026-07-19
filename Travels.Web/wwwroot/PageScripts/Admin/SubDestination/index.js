var Dest = function () {
    var me = this;
    var po = null;
    this.DestinationId = null;
    var addUpdate = function (id) {
        $.ajax({
            type: 'Get',
            url: '/Admin/SubDestination/AddUpdate',
            dataType: 'html',
            data: { id: id, DestinationId: me.DestinationId },
            success: function (content, strStatus) {

                $('body').append(content);

                tinymce.init({
                    selector: 'textarea#Description,#Overview' ,
                                    advcode_inline: true,

                });
                loadDropzoneWithCropper('imageUploadContainer', "image", "/admin",10);
                $('#modal-addUpdate').modal("show");

                $('#chk-IsActive').on('change', function () {
                    $('#IsActive').val($(this).is(':checked'))
                })
                $('#chk-IsPublished').on('change', function () {
                    $('#IsPublished').val($(this).is(':checked'))
                })
                $('#chk-IsDeleted').on('change', function () {
                    $('#IsDeleted').val($(this).is(':checked'))
                })
                $('#saveDestinationfrm').submit(function (e) {
                    e.preventDefault(); // Prevent default form submission
                    var formData = new FormData($(this)[0]);
                    var myDropzone = Dropzone.forElement("#imageUploadContainer");
                    myDropzone.getAcceptedFiles().forEach(function (file, index) {
                       
                            formData.append('Images[' + index + ']', file);

                    });

                    // Now make the AJAX request
                    $.ajax({
                        url: '/Admin/SubDestination/SaveSubDestination', // Change this to your server-side script
                        type: 'POST',
                        data: formData,
                        processData: false, // Important: Do not process the data
                        contentType: false, // Important: Set contentType to false to handle multipart/form-data
                        success: function (response) {
                            console.log('Files and form data submitted successfully');
                            $("#modal-addUpdate").hide();
                            $("#modal-addUpdate").remove();
                            location.reload();
                        },
                        error: function (xhr, status, error) {
                            console.log('Upload failed: ', error);
                        }
                    });
                });


                $("#modal-addUpdate").modal({ show: true, backdrop: 'static' })
                    .on('hidden.bs.modal', function (e) {
                        $("#modal-addUpdate").remove();
                    });

            }
        });

    }
    this.init = function () {
        $('#btn-search').on('click', function () {
            window.location.href = "/Admin/SubDestination/Index?DestinationId=" + me.DestinationId +"&keyword=" + $('#Keyword').val();
        })
        $('#btn-add').on('click', function () {
            window.location.href = "/Admin/SubDestination/Create/?DestinationId=" + me.DestinationId;
        })
        //$('.btn-edit').on('click', function () {
        //    var Id = $(this).data("id")
        //    addUpdate(Id)
        //});
        $('.btn-delete').on('click', function () {
            var Id = $(this).data("id")
            $.ajax({
                type: 'POST',
                url: '/Admin/SubDestination/SwitchDelete',
                dataType: 'JSON',
                data: { id: Id },
                success: function (content, strStatus) {
                    showSuccessMessage(response.message, function () {
                        location.reload();
                    });
                },
                error: function (xhr, status, error) {
                    showErrorMessage(error);
                }
            });
        });
    }
}


function loadDropzoneWithCropper(imageUploadContainerId, paraName, url,maxFile) {

    $("#" + imageUploadContainerId).dropzone({
        //paramName: "image", // The name that will be used to transfer the file
        //url: '/ImageMedia/UploadImage',
        paramName: paraName,
        url: url,
        maxFilesize: 10, // MB
        maxFiles: maxFile,
        timeout: 1800000,
        parallelUploads: 5,
        dictDefaultMessage: 'Drop files here to upload. ' + maxFile + ' files max for each upload. ' + 10 + ' MB max for each file. Only image file is supported',
        autoProcessQueue: false,
        autoDiscover: true,
        addRemoveLinks: true,
        init: function () {

            var myDropzone = Dropzone.forElement("#" + imageUploadContainerId);
            myDropzone.on('thumbnail', function (file) {

                // ignore files which were already cropped and re-rendered
                // to prevent infinite loop
                if (file.cropped) {
                    return;
                }

                // cache filename to re-assign it to cropped file
                var cachedFilename = file.name;
                // remove not cropped file from dropzone (we will replace it later)
                myDropzone.removeFile(file);

                var editor = document.createElement('div');
                editor.style.position = 'fixed';
                editor.style.left = 0;
                editor.style.right = 0;
                editor.style.top = 0;
                editor.style.bottom = 0;
                editor.style.zIndex = 9999;
                editor.style.backgroundColor = '#000';

                var para = document.createElement('P');
                para.innerHTML = 'In order to crop you can drag the cursor.';
                para.style.position = 'absolute';
                para.style.left = '10px';
                para.style.top = '10px';
                para.style.zIndex = 9999;
                para.style.maxWidth = '100%';
                para.style.maxHeight = '200px';
                para.style.color = '#000';
                para.style.backgroundColor = 'white';

                var confirm = document.createElement('button');
                confirm.style.position = 'absolute';
                confirm.style.left = '10px';
                confirm.style.top = '120px';
                confirm.style.zIndex = 9999;
                confirm.textContent = 'Confirm';
                confirm.addEventListener('click', function () {
                    var blob = $img.cropper('getCroppedCanvas').toDataURL();
                    // transform it to Blob object
                    var newFile = dataURItoBlob(blob);
                    // set 'cropped to true' (so that we don't get to that listener again)
                    newFile.cropped = true;
                    // assign original filename
                    newFile.name = cachedFilename;

                    // add cropped file to dropzone
                    myDropzone.addFile(newFile);

                    editor.parentNode.removeChild(editor);
                });

                var cancel = document.createElement('button');
                cancel.style.position = 'absolute';
                cancel.style.left = '10px';
                cancel.style.top = '160px';
                cancel.style.zIndex = 9999;
                cancel.style.color = 'red';
                cancel.textContent = 'Cancel';
                cancel.addEventListener('click', function () {
                    editor.parentNode.removeChild(editor);
                });

                editor.appendChild(para);
                editor.appendChild(confirm);
                editor.appendChild(cancel);
                //var image = document.createElement('img');
                var $img = $('<img />');
                // initialize FileReader which reads uploaded file
                var reader = new FileReader();
                reader.onloadend = function () {
                    // add uploaded and read image to modal

                    $img.attr('src', reader.result).appendTo(editor);

                    // initialize cropper for uploaded image
                    $img.cropper({
                        aspectRatio: 16 / 9,
                        autoCropArea: 1,
                        movable: false,
                        cropBoxResizable: true,
                        minContainerWidth: 850
                    });

                    document.body.appendChild(editor);
                };
                // read uploaded file (triggers code above)
                reader.readAsDataURL(file);

            });
        }
    });
}
var dataURItoBlob = function (dataURI) {
    var byteString = atob(dataURI.split(',')[1]);
    var ab = new ArrayBuffer(byteString.length);
    var ia = new Uint8Array(ab);
    for (var i = 0; i < byteString.length; i++) {
        ia[i] = byteString.charCodeAt(i);
    }
    return new Blob([ab], { type: 'image/jpeg' });
};