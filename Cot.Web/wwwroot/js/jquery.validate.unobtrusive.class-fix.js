$(document).ready(function () {
    $('.input-validation-error').addClass('is-invalid').removeClass('input-validation-error');
});
var settings = {
    validClass: "is-valid",
    errorClass: "is-invalid"
};
$.validator.setDefaults(settings);
$.validator.unobtrusive.options = settings;