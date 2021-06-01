$(function () {
    $('#errorToast') && $('#errorToast').toast("show")
})

$(document).ready(function () {


    $("#UpdateAdmin").click(function () {
        $("#InputError").hide();
        $("#DataSaved").hide();
        $("#UnexpectedError").hide();
        $("#SamePassword").hide();

        if ($("#FormUpdateAdmin").valid()) {

            var myData = $("#FormUpdateAdmin").serialize();

            $.ajax({
                type: "POST",
                url: "/admin/UpdateData",
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

    $("#ChangePassword2").click(function () {
        $("#InputError").hide();
        $("#DataSaved").hide();
        $("#UnexpectedError").hide();
        $("#SamePassword").hide();

        if ($("#ChangePasswordForm").valid()) {

            var myData = $("#ChangePasswordForm").serialize();

            $.ajax({
                type: "POST",
                url: "/admin/UpdatePassword",
                data: myData,
                success: function (res) {
                    console.log(res);
                    if (res.res == 1) {
                        $("#DataSaved").show();
                    } else if (res.res == 2) {
                        $("#SamePassword").show();
                    }
                    else {
                        $("#SamePassword").text(res.res.Errors);
                        $("#SamePassword").show();
                    }
                    $("#ChangePasswordModel").modal('hide');

                }
            })

        } else {
            $("#InputError").show();

        }

    })
})