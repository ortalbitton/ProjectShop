// Write your Javascript code


$(document).ready(function () {

    //הצגת תת קטגוריה לפי קטגוריה מסוימת
    $(".Component").hide();

    $(".Department").hover(function () {
        $(this).find('.Component').show()
        $(this).css('width', 90 + 'px')
        $(this).css('height', 200 + 'px')
        //$(this).css('background-color','red');


    }, function () {
        $(this).find('.Component').hide()
        $(this).css('width', 90 + 'px')
        $(this).css('height', 37 + 'px')
        //$(this).css('background-color', 'yellow');
    });



    $(".Text").click(function () {
        var id = $(this).attr("id")
        $.ajax({
            url: "SubCategories/Index?id" + id,
            method: "post",
            async: false,
            data: { id: id },
            success: function (data) {
                $("#Product").html(data)
            }
        });
    });

//הצגת מוצרים לפי שם מוצר מסוים בעת לחיצה
                //$("#Search").click( function () {
        //    var value = $("#myInput").val().toLowerCase();
        //    $("#Product .col-md-3").filter(function () {
        //        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        //    });
        //});



        //$("#Search").click(function () {
        //    // Retrieve the input field text and reset the count to zero
        //    var filter = $("#myInput").val(),
        //        count = 0;

        //    // Loop through the comment list
        //    $('#Product .col-md-3').each(function () {


        //        // If the list item does not contain the text phrase fade it out
        //        if ($(this).text().search(new RegExp(filter, "i")) < 0) {
        //            $(this).hide();// MY CHANGE



        //            // Show the list item if the phrase matches and increase the count by 1
        //        } else {
        //            $(this).show();// MY CHANGE

        //            count++;

        //        }
               
        //    });

        //    $(this).show();
        //});



 });


    // בעת טעינה יציג לי את כל המוצרים
            $(window).load(function () {
 
                $.ajax({
                    url: "SubCategories/Index",
                    method: "post",
                    dataType: "html",
                    success: function (respon) {
                        $("#Product").html(respon)
                    }
                });

             

                $.ajax({
                    url: "Colors/Index",
                    method: "post",
                    dataType: "html",
                    success: function (respon) {
                        $("#Color").html(respon);
                    }
                });
});

