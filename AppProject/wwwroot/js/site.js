// Write your Javascript code

$(document).ready(function () {
    //הצגת תת קטגוריה לפי קטגוריה מסוימת
    $(".Component").hide();

    $(".Department").hover(function () {
        $(this).find('.Component').show()
        $(this).css('width', 140 + 'px')
        $(this).css('height', 200 + 'px')
        //$(this).css('background-color','red');


    }, function () {
        $(this).find('.Component').hide()
        $(this).css('width', 140 + 'px')
        $(this).css('height', 37 + 'px')
        //$(this).css('background-color', 'yellow');
    });



    $(".Text").click(function () {
        var id = $(this).attr("id")
        $.ajax({
            url: "Productes/Index?id" + id,
            method: "post",
            async: false,
            data: { id: id },
            success: function (data) {
                $("#Product").html(data)
            }
        });
    });

    //הצגת מוצרים לפי שם מוצר מסוים בעת הקלדה
    $("#name").keyup(function () {
        var form = $('#myform')
        var url = form.attr('action')
        $.ajax({
            url: url,
            data: form.serialize(),
            success: function (data) {
                $('#Product').html('');
                for (var i = 0; i < data.length; i++) {
                    $('#Product').append(' <div class="col-md-4"><div class="product-item"><img src="/imagesweb/' + data[i].imgId + '"/><div class="product-info"><a href="/Productes/Details/' + data[i].id + '">' + data[i].productName + '</a><p>$' + data[i].price + '</p></div></div></div>') // show response from the php script.

                }
            }
        });
    });

    //var check = 0;
    //צריכה לשנות ששתיהם ילכו לטבלת connect   כי אחרת אין קשר ביניהם.
    $('input[value=color]').change(function () {
        if ($(this).prop("checked")) {
            //do the stuff that you would do when 'checked'
            var colorid = $(this).attr("id")
            $.ajax({
                url: "Colors/Search?id" + colorid,
                method: "post",
                async: false,
                data: { colorid: colorid },
                success: function (data) {
                    $("#Product").html(data)
                }
            });
            return;
        }

        //Here do the stuff you want to do when 'unchecked'
        $.ajax({
            url: "Productes/Index",
            method: "post",
            success: function (respon) {
                $("#Product").html(respon)
            }
        });
    });


    $('input[value=size]').change(function () {
        if ($(this).prop("checked")) {
            //do the stuff that you would do when 'checked'

            var sizeid = $(this).attr("id")
            $.ajax({
                url: "Sizes/Search?id" + sizeid,
                method: "post",
                async: false,
                data: { sizeid: sizeid },
                success: function (data) {
                    $("#Product").html(data)
                }
            });
            return;
        }

        //Here do the stuff you want to do when 'unchecked'
        $.ajax({
            url: "Productes/Index",
            method: "post",
            success: function (respon) {
                $("#Product").html(respon)
            }
        });
    });

});













 




