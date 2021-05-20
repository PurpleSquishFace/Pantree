﻿
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

            if (callback !== null) {
                window[callback]();
            }

        }
    });

    return false;
});