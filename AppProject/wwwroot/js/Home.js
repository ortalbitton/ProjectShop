
$(document).ready(function () {

    $.ajax({
        url: "Productes/Index",
        method: "post",
        success: function (respon) {
            $("#Product").html(respon)
        }
    });

    $.ajax({
        url: "Colors/Index",
        method: "post",
        success: function (respon) {
            $("#Color").html(respon)
        }
    });

    $.ajax({
        url: "Sizes/Index",
        method: "post",
        success: function (respon) {
            $("#Size").html(respon)
        }
    });


});