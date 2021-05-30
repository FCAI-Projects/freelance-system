$(function () {
    $('#errorToast') && $('#errorToast').toast("show")
})

$(document).ready(function () {


    $("#UpdateAdmin").click(function () {
        $("#InputError").hide();
        $("#DataSaved").hide();
        $("#UnexpectedError").hide();
        if ($("#FormUpdateAdmin").valid()) {

            var myData = $("#FormUpdateAdmin").serialize();

            $.ajax({
                type: "POST",
                url: "/admin/Update",
                data: myData,
                success: function (res) {
                    if (res.res) {
                        $("#DataSaved").show();
                    } else {
                        $("#UnexpectedError").show();
                    }
                }
            })

        } else {
            $("#InputError").show();
        }

    })
})