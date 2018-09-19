function addcart(productid, quantity, size) {
    var postsize = $("#postsize_" + productid).val();
    if (postsize == "") {
        alert("vui lòng nhập kích thước")
        $("#postsize_" + productid).css("border", "2px solid red");
        $("#postsize_" + productid).focus();
    }
    else {
        $.ajax({
            url: "/Cart/AddItem",
            type: "POST",
            data: { "productid": productid, "quantity": quantity, "size": size },
            success: function () {
                window.location = "/gio-hang-ga-chong-tham";
            }
        });
    }
};
function home(){
	window.location = "/";
}
function deleteitem(productid,quantity,size,count) {
    $.ajax({
        url: "/Cart/RemoveItem",
        type: "POST",
        data: { "productid": productid,"quantity":quantity,"size":size},
        success: function (result) {
            $("#item_" + count).html("");
            var vnd = "  VND";
            var tongtien1 = numeral(result.tongtien2).format('0,000 ');
            $("#total2").html(tongtien1 + vnd);
            if (result.count == 0) {
                window.location = "/";
            }
        }
    });
}
function changequantity(productid, quantity, size) {
    $.ajax({
        url: "../Cart/updatequantity",
        type: "POST",
        data: { "productid": productid, "quantity": quantity, "size": size },
        success: function (result) {
            var vnd = "  VND";
            var tongtien = numeral(result.tongtien1).format('0,000');
            var tongtien1 = numeral(result.tongtien2).format('0,000 ');
            $(".total1_" + productid).html(tongtien);
            $("#total2").html(tongtien1 + vnd);

        }
    });
};
function updatesize(productid, size,oldsize,idproduct) {
    var textaction = $("#edit_" + productid).text();
    if (textaction == "Sửa kích thước") {
        var valueedit = $("<input>", { val: size, type: "text" });
        $("#sizeeidt_" + productid).html(valueedit);
        $("#edit_" + productid).html("xong");
        $("#sizeeidt_" + productid + " input").attr("class", "inputsizeedit");
    } else {
        var sizeedited = $("#sizeeidt_" + productid + " input").val();
        if (sizeedited == "") {
            alert("Vui lòng nhập kích thước");
            $("#sizeeidt_" + productid + " input").focus();
        } else {
            $.ajax({
                url: "/Cart/UpdateSize",
                type: "POST",
                data: { "productid": idproduct, "newsize": sizeedited, "oldsize": oldsize },
                success: function (result) {
                    $("#sizeeidt_" + productid).html(result.size);
                    $("#edit_" + productid).html("Sửa kích thước");
                }
            });
        }
    }

}
$(document).ready(function () {
    $("#postorder").click(function () {
        var name = $("#namecustomer").val();
        var phone = $("#phone").val();
        var address = $("#address").val();
        var city = $("#city").val();
        var email = $("#email").val();
        var data = {
            namecustomer: name,
            phone: phone,
            address: address,
            city: city,
            email:email
        }
        if (name == "" || phone == "" || address == "" || city == "") {
            alert("không được phép trống")
        }
        else {
            $.ajax({
                url: "Order/Order",
                type: "POST",
                data: data,
                dataType: "JSON",
                success: function (html) {
                    $("#inforcustomer").html(html);
                    $("#hovaten").html(name);
                    $("#dienthoai").html(phone);
                    $("#diachi").html(address);
                    $("#tinhthanhpho").html(city);
                    $("#mail").html(email);
                },
                error: function () {
                    location.reload();
                }
            })
        }
    });
    $("#orders").click(function () {
        window.location = "/dat-hang-ga-chong-tham";
    });
    $("#buymore").click(function () {
        window.location = "/";
    });
    $("#backcart").click(function () {
        window.location = "/gio-hang-ga-chong-tham";
    })
})