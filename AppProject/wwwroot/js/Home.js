
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


    $('#alert').hide();

    $('#ShoppingCart').hover(function () {
       
        $(this).find('#alert').show()
        
        //$(this).css('width', 90 + 'px')
        //$(this).css('height', 200 + 'px')
        //$(this).css('background-color','red');


    }, function () {
        $(this).find('#alert').hide()
        //$(this).css('width', 90 + 'px')
        //$(this).css('height', 37 + 'px')
        //$(this).css('background-color', 'yellow');
    });


});