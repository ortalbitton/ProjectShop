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
                    $('#Product').append(' <div class="col-md-3"><div class="product-item"><img src="/imagesweb/' + data[i].imgId + '"/><div class="product-info"><a href="/Productes/Details/' + data[i].id + '">' + data[i].productName + '</a><h6>' + data[i].price + '</h6><a href="#" class="site-btn btn-line">Add TO Mart</a></div></div></div>') // show response from the php script.

                }
            }
        });
    });

});

//לא גמור
var otherCheckbox = document.querySelector('input[value="color"]');

otherCheckbox.onchange = function () {
    if (otherCheckbox.checked) {        
        var id = $(this).attr("id")
        $.ajax({
            url: "ConnectTables/Index?id" + id,
            method: "post",
            async: false,
            data: { id: id },
            success: function (data) {
                $("#Product").html(data)
            }
        });

    } else {
        alert('llll');
    }
};







 




