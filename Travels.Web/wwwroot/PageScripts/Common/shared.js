var showSuccessMessage = function (message, callback) {
    Swal.fire({
        title: "Success",
        text: message,
        icon: "success",
        showCancelButton: false,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "OK"
    }).then((result) => {

        if (result.isConfirmed && callback) {
            callback();
        }
    });
}
var showWarningMessage = function (message, callback) {
    Swal.fire({
        title: "Warning!",
        text: message,
        icon: "warning",
        showCancelButton: false,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "OK"
    }).then((result) => {

        if (result.isConfirmed && callback) {
            callback();
        }
    });
}
var showErrorMessage = function (message, callback) {
    Swal.fire({
        title: "Error",
        text: message,
        icon: "error",
        showCancelButton: false,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "OK"
    }).then((result) => {

        if (result.isConfirmed && callback) {
            callback();
        }
    });
}
var showConfirmation = function (title, text, callback, cancelCallback) {
    Swal.fire({
        title: title,
        text: text,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes",
        cancelButtonText: "No",
    }).then((result) => {

        if (result.isConfirmed && callback) {
            callback();
        } else if (!result.isConfirmed && cancelCallback) {
            cancelCallback();
        }
    });
}
function initHtmlEditor(selector, invalidEle) {
    tinymce.remove(selector);
    tinymce.init({
        selector: selector,
        invalid_elements: invalidEle,
        extended_valid_elements: 'i[class]', // Allow <i> tags with class attributes
        valid_children: '+div[i]',
        forced_root_block: false, // Prevent TinyMCE from forcing paragraph tags
        verify_html: false, // Disable HTML validation
        valid_elements: '*[*]', // Allows all tags and attributes
        height: 300,
        browser_spellcheck: true,
        plugins: [
            "advlist autolink lists link image charmap print preview anchor",
            "searchreplace visualblocks code fullscreen",
            "insertdatetime media table paste"
        ],
        toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
        setup: function (editor) {
            editor.on('focus', function () {
                $(document).trigger('focusin');
            });
            editor.on('change', function () {
                editor.save();
            });
        }
    });
}

function loadDropzoneWithCropper(imageUploadContainerId, paraName, url, maxFile) {

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

function loadDropzoneWithCropperForSingleFile(imageUploadContainerId, paraName, url,asptRation, hidecont ) {
    maxFile = 1;
    $("#" + imageUploadContainerId).dropzone({
        //paramName: "image", // The name that will be used to transfer the file
        //url: '/ImageMedia/UploadImage',
        paramName: paraName,
        url: url,
        maxFilesize: 10, // MB
        maxFiles: maxFile,
        timeout: 1800000,
        parallelUploads: 5,
        dictDefaultMessage: 'Drop your files here to upload. '  + 10 + ' MB max for a file. Only image file is supported',
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
                    $(hidecont).hide()
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
                        aspectRatio: asptRation,
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

var initDatePicker= function(selector)
{
    $(selector).daterangepicker({
        singleDatePicker: true,
        opens: 'right'
    }, function (start, label) {
        $(selector).val(start.format('YYYY-MM-DD'));
    });
}
var accordionInitializer = function () {
    if ($(".vietjet-accrodion").length) {
        var accrodionGrp = $(".vietjet-accrodion");
        accrodionGrp.each(function () {
            var accrodionName = $(this).data("grp-name");
            var Self = $(this);
            var accordion = Self.find(".accrodion");
            Self.addClass(accrodionName);
            Self.find(".accrodion .accrodion-content").hide();
            Self.find(".accrodion.active").find(".accrodion-content").show();
            accordion.each(function () {
                $(this)
                    .find(".accrodion-title")
                    .on("click", function () {
                        if ($(this).parent().hasClass("active") === false) {
                            $(".vietjet-accrodion." + accrodionName)
                                .find(".accrodion")
                                .removeClass("active");
                            $(".vietjet-accrodion." + accrodionName)
                                .find(".accrodion")
                                .find(".accrodion-content")
                                .slideUp();
                            $(this).parent().addClass("active");
                            $(this).parent().find(".accrodion-content").slideDown();
                        }
                    });
            });
        });
    }
}