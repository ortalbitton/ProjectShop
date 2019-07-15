
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

    $(".dropdown-item").click(function () {
        $.ajax({
            url: "Productes/GroupByPrice",
            method: "post",
            success: function (data) {
                $("#Product").html(data)
            }
        });
    });

});