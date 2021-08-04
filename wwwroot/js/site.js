
//------------------- Menu Toggle -------------------//

$('body').on('click', '.navbar-menu-button', function () {
    $(this).find('.navbar-hamburger').toggleClass('open');
    $('.navbar-menu').toggle(500);
});

$(document).click(function (event) {
    var $target = $(event.target);
    if (!$target.closest('.navbar-menu-button').length && !$target.closest('.navbar-menu').length && $('.navbar-menu').is(':visible')) {
        $('.navbar-hamburger').removeClass('open');
        $('.navbar-menu').toggle(500);
    }
});

//------------------- Scroll to top -------------------//

$('body').on('click', '.btn-top', function () {
    window.scrollTo({ top: 0, behavior: 'smooth' });
});

//------------------- Dependent Dropdown -------------------//

$('body').on('change', '.load-dropdown', function () {
    let url = $(this).attr('attr-url');
    let input = $(this).attr('attr-for');
    let param = $(this).val();

    $.ajax({
        type: 'POST',
        url: url,
        data: { param },
        success: function (result) {
            $('#' + input).empty();
            $.each(result, function (i, element) {
                $('#' + input).append($('<option></option>').val(element.storeID).html(element.storeName));
            });
        }
    });
});

//------------------- Update partial -------------------//

$('body').on('submit', '.load-partial', function () {
    let form = $(this);
    let partialContainer = form.attr('attr-partial');
    let callback = form.attr('attr-callback');

    $.ajax({
        type: form.attr('method'),
        url: form.attr('action'),
        data: form.serialize(),
        success: function (result) {
            $('#' + partialContainer).empty().html(result);
            form[0].reset();

            if (callback != null) {
                window[callback]();
            }

        }
    });

    return false;
});

//------------------- Form on change -------------------//

$('body').on('change', '.submit-onchange', function () {
    let form = $(this).closest('form');
    form.submit();
});

//------------------- Number inputs -------------------//

$('body').on('input', 'input[inputmode="numeric"]', function () {
    var $input = $(this);
    $input.val($input.val().replace(/[^\d]+/g, ''));
});

$('body').on('keydown', 'input[inputmode="numeric"]', function (e) {
    let input = $(this);
    let val = input.val();

    if (e.keyCode == 38) {
        val = Number(input.val()) + Number(1);
    }
    if (e.keyCode == 40) {
        val = Number(input.val()) - Number(1);
        val = val < 0 ? 0 : val;
    }
    input.focus().val(val);

});

//------------------- Button Behaviour -------------------//

$('body').on('click', 'button[load]', function () {

    let partial = $(this).attr('partial');
    let url = $(this).attr('load');
    let callback = $(this).attr('attr-callback');

    if (typeof partial !== 'undefined' && partial !== false) {

        $.ajax({
            type: 'POST',
            url: url,
            success: function (result) {
                $('#' + partial).empty().html(result);

                if (callback != null) {
                    window[callback]();
                }
            }
        });

    } else {
        window.location = url;
    }
});

$('body').on('click', '.account-menu button', function () {
    $('.account-menu button').each(function () {
        $(this).removeClass('selected');
    });

    $(this).toggleClass('selected');
})

//------------------- Profile Picture -------------------//

let canvas, context;

function LoadProfilePicPartial() {
    $.ajax({
        type: 'POST',
        url: "/Account/ProfileImage",
        success: function (result) {
            $('#accountContainer').empty().html(result);
            SetupCanvas();
        }
    });
}

function SetupCanvas() {
    canvas = $("#Image");
    context = canvas.get(0).getContext("2d");
}

$('body').on('change', '#ProfilePicture_LoadImage', function () {
    if (this.files && this.files[0]) {
        if (this.files[0].type.match(/^image\//)) {

            var reader = new FileReader();
            reader.onload = function (evt) {
                var img = new Image();
                img.onload = function () {
                    context.canvas.height = img.height;
                    context.canvas.width = img.width;
                    context.drawImage(img, 0, 0);
                    var cropper = canvas.cropper({
                        aspectRatio: 1
                    });

                    $('body').on('click', '#CropImage', function () {
                        let form = $('#ProfileImageForm');
                        let data = new FormData(document.getElementById('ProfileImageForm'));

                        var croppedImageDataURL = getRoundedCanvas(canvas.cropper('getCroppedCanvas')).toDataURL("image/png", 0.5);
                        var blob = dataURItoBlob(croppedImageDataURL);
                        data.append('ProfileImage', blob);

                        $.ajax({
                            type: form.attr('method'),
                            url: form.attr('action'),
                            data: data,
                            contentType: false,
                            processData: false,
                            success: function () {
                                location.reload();
                            },
                            error: function () {
                                LoadProfilePicPartial();
                            }
                        });

                        $(form).trigger("reset");
                    });
                };

                $('#BtnUpload').remove();

                img.src = evt.target.result;
            };

            reader.readAsDataURL(this.files[0]);
            $('.upload-reveal').show();
        }
    }
});

function getRoundedCanvas(sourceCanvas) {
    var canvas = document.createElement('canvas');
    var context = canvas.getContext('2d');
    var width = sourceCanvas.width;
    var height = sourceCanvas.height;

    canvas.width = width;
    canvas.height = height;
    context.imageSmoothingEnabled = true;
    context.drawImage(sourceCanvas, 0, 0, width, height);
    context.globalCompositeOperation = 'destination-in';
    context.beginPath();
    context.arc(width / 2, height / 2, Math.min(width, height) / 2, 0, 2 * Math.PI, true);
    context.fill();
    return canvas;
}

function dataURItoBlob(dataURI) {
    // convert base64/URLEncoded data component to raw binary data held in a string
    var byteString;
    if (dataURI.split(',')[0].indexOf('base64') >= 0)
        byteString = atob(dataURI.split(',')[1]);
    else
        byteString = unescape(dataURI.split(',')[1]);

    // separate out the mime component
    var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];

    // write the bytes of the string to a typed array
    var ia = new Uint8Array(byteString.length);
    for (var i = 0; i < byteString.length; i++) {
        ia[i] = byteString.charCodeAt(i);
    }

    return new Blob([ia], {
        type: mimeString
    });
}
