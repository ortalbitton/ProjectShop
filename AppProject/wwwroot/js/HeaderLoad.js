
$(document).ready(function () {
    $.ajax({
        url: "Categories/Index",
        method: "post",
        success: function (respon) {
            $("#Categories").html(respon)
        }
    });
});