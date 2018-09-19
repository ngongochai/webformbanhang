$(document).ready(function () {
    $(".addproduct").click(function () {
        $(".show-hide").toggle();
    });
    $("#addproduct").click(function () {
        var codeproduct = $("#codeproduct").val();
        var categoryid = $("#categoryid").val();
        var size = $("#size").val();
        var price = $("#priceadd").val();
        var imgname;
        var productname = $("#productname").val();
        var formData = new FormData();
        var totalFiles = document.getElementById("imgname").files.length;
        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementById("imgname").files[i];
            imgname= file.name;
            formData.append("imgname", file);
        }
        var seninfo = {
            masp: codeproduct,
            categoryid: categoryid,
            hinhanh: imgname,
            price: price,
            size:size,
            chitiet:productname,          
        }
        if (codeproduct == "" || categoryid == "" || price == "" || imgname == null || productname==""||size=="") {
			alert("không được rỗng")
        }
        else if (totalFiles > 0) {
            $.ajax({
                url: "/Admin/ManagerProduct/AddProduct",
                type: "POST",
                data: formData,
                contentType: false,
                cache: false,
                processData: false,
                success: function () {
                }
            })

            $.ajax({
                url: "/Admin/ManagerProduct/AddProduct",
                type: "POST",
                data: seninfo,
                dataType: "JSON",
                success: function (result) {
                    if (result == "0")
                        alert("Sản phẩm đã tồn tại")
                    else
                        alert("Thêm sản phẩm thành công")
                }
            });
        }
    });
    $("#delete").click(function () {
        $.ajax({
            url: "DeleteProduct",
            traditional: true,
            type: "POST",
            success: function (result) {
                if (result == 0) {
                    alert("khong lua chon")
                }
            }
        })
    })
    $("#save").click(function () {
        location.reload();
    });
    $("#delete").click(function () {
        location.reload();
    });
    $("#searchcode").focus(function () {
        var list = []
        var ac = 1;
        var codeproduct = $("#searchcode").val();
        $.ajax({
            url: "/Admin/ManagerProduct/SearchCode",
            type: "POST",
            data: { "searchcode": codeproduct, "ac": ac },
            success: function (listsearch) {
                $.each(listsearch, function (index, listsearch) {
                    //b["value"] = listsearch.masp;
                    //a[index] = b;
                    list.push({ value: listsearch.masp });
                })
                $("#searchcode").autocomplete({
                    lookup: list
                })
            }
        })
    });
    $("#buttonsearch").click(function () {
        var ac = 2;
        var codeproduct = $("#searchcode").val();
        $.ajax({
            url: "/Admin/ManagerProduct/SearchCode",
            type: "POST",
            data: { "searchcode": codeproduct, "ac": ac },
            success: function (listsearch) {
                $("#overflow").addClass("searchcode");
                $("#tbody").html(listsearch);
            }
        });
    });
});
function success(productid, codeproduct, categoryid, imgname, productname, price, cp) {
    $("#image_" + productid + " input").attr("id", "imgupload_"+ productid);
    var editlist;
    var size = $("#size_"+productid+" input").val();
    var imgnamefile=imgname;
    var formData = new FormData();
    var totalFiles = document.getElementById("imgupload_"+productid).files.length;
    for (var i = 0; i < totalFiles; i++) {
        var file = document.getElementById("imgupload_"+productid).files[i];
        imgnamefile = file.name;
        formData.append("imgupload_" + productid, file);
    }
    if (totalFiles > 0) {
        $.ajax({
            url: "/Admin/ManagerProduct/EditProduct",
            type: "POST",
            data: formData,
            contentType: false,
            cache: false,
            processData: false,
            success: function () {
            }
        })
    }
    if (productid == "" || codeproduct == "" || categoryid == "" || price == "" || productname == ""||size=="") {
        alert("Không được rỗng");
    }
    else {
        if (cp == codeproduct) {
            ac = 0;
            editlist = {
                productid: productid,
                masp: codeproduct,
                categoryid: categoryid,
                price: price,
                hinhanh:imgnamefile,
                chitiet: productname,
                size:size,
                ac: ac
            }
            $.ajax({
                url: "/Admin/ManagerProduct/EditProduct",
                type: "POST",
                dataType:"JSON",
                data: editlist,
                success: function (result) {
                    $("#edit_" + productid).html("sửa");
                    $("#success_" + productid).hide();
                    $("#codeproduct_" + productid).html(codeproduct);
                    $("#category_" + productid).html(categoryid);
                    $("#size_" + productid).html(size);
                    var tagimg = $("<img>", { src: "/Content/img/" + imgnamefile, style: "width:80px;" });
                    $("#imgname_" + productid + " #image").html(imgnamefile);
                    $("#image_" + productid).html(tagimg);
                    $("#productname_" + productid).html(productname);
                    $("#price_" + productid+" #priceid").html(price);
                }

            })
        }
        else {
            ac = 1;
            editlist = {
                productid: productid,
                masp: codeproduct,
                categoryid: categoryid,
                hinhanh: imgname,
                price: price,
                size: size,
                chitiet: productname,
                ac: ac
            }
            $.ajax({
                url: "/Admin/ManagerProduct/EditProduct",
                type: "POST",
                dataType: "JSON",
                data: editlist,
                success: function (result) {
                    if (result != 1) {
                        $("#edit_" + productid).html("sửa")
                        $("#success_" + productid).hide();
                        $("#codeproduct_" + productid).html(codeproduct);
                        $("#category_" + productid).html(categoryid);
                        var tagimg = $("<img>", { src: "/Content/img/" + imgnamefile, style: "width:80px;" });
                        $("#imgname_" + productid + " #image").html(imgnamefile);
                        $("#size_" + productid).html(size);
                        $("#image_" + productid).html(tagimg);
                        $("#productname_" + productid).html(productname);
                        $("#price_" + productid).html(price);
                    }
                    else {
                        alert("mã sản phẩm trùng")
                    }
                }

            })
        }

    }
};
function showdetails(id) {
    $("#htmlopacity").addClass("ui-widget-overlay");
    var name = $("#namecustomer_" + id).text();
    $("#loadingdetail").show();
    $.ajax({
        url: "/Admin/ManagerOrder/DetailsOrder",
        type: "POST",
        data: { "productid": id },
        success: function (html) {
            $("#showdetails").html(html);
            $("#customer").html(name);
            $("#loadingdetail").hide();
        }
    })
};
function hidedetail() {
    $("#htmlopacity").removeClass("ui-widget-overlay");
    $("#showdetails").html("");
}
function whritenote(customerid) {
    var note = $("<input>", { val: $("#notecustomer_" + customerid).text(), type: "text", id: "noteedited_" + customerid });
    $("#notecustomer_" + customerid).html(note);
}
function savenote(customerid) {
    var note = $("#noteedited_" + customerid).val();
    $.ajax({
        url: "/Admin/ManagerOrder/SaveNote",
        type: "POST",
        data: { "customerid": customerid, "note": note },
        success: function () {
            $("#notecustomer_" + customerid).html(note);
        }
    })
}