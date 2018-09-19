$(document).ready(function () {
    $("#searchproduct").keyup(function () {
        var keywork = $("#searchproduct").val();
        if (keywork.length > 1) {
            $("#loading").show();
            $.ajax({
                url: "/Home/SearchAuto",
                type: "POST",
                data: { "keywork": keywork },
                success: function (html) {
                    if (html==0) {
                        $(".showSearch").html("").hide();
                        $("#loading").hide();
                    }
                    else {
                        $(".showSearch").html(html).show();
                        $("#loading").hide();
                    }
                }
            })
        }
        else {
            $(".showSearch").html("").hide();
        }
    });
})
function selectquantity(productid) {
   $("#showinput_" + productid).toggle();
        var selectquantity = $("#selectquantity_"+productid).html();
        if (selectquantity == "") {
            $.ajax({
                url: "/Home/Selectquantity",
                type: "POST",
                data: { "productid": productid },
                success: function (html) {
                    $("#selectquantity_"+productid).html(html);
                }
            })
        }
        else {
            $("#selectquantity_"+productid).html("");
        }
    }
    function selectquantitysearch(productid) {
		$("#showinput_" + productid).toggle();
        var selectquantity = $("#selectquantitysearch_" + productid).html();
        if (selectquantity == "") {
            $.ajax({
                url: "/Home/Selectquantity",
                type: "POST",
                data: { "productid": productid },
                success: function (html) {
                    $("#selectquantitysearch_" + productid).html(html);
                }
            })
        }
        else {
            $("#selectquantitysearch_" + productid).html("");
        }
    }