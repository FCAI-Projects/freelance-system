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

            var frm = $('#FormUpdateAdmin');
            var myData = new FormData(frm[0]);
            myData.append('file', $('input[type=file]')[0].files[0]);

            $.ajax({
                type: "POST",
                url: "/admin/UpdateData",
                data: myData,
                processData: false,
                contentType: false,
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


    $("#UpdateClient").click(function () {

        $("#InputError").hide();
        $("#DataSaved").hide();
        $("#UnexpectedError").hide();
        $("#SamePassword").hide();
        if ($("#FormUpdateClient").valid()) {

            var frm = $('#FormUpdateClient');
            var myData = new FormData(frm[0]);
            myData.append('file', $('input[type=file]')[0].files[0]);

            $.ajax({
                type: "POST",
                url: "/client/UpdateData",
                data: myData,
                processData: false,
                contentType: false,
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

    $("#ChangePasswordClient").click(function () {
        $("#InputError").hide();
        $("#DataSaved").hide();
        $("#UnexpectedError").hide();
        $("#SamePassword").hide();

        if ($("#ChangePasswordForm").valid()) {

            var myData = $("#ChangePasswordForm").serialize();

            $.ajax({
                type: "POST",
                url: "/client/UpdatePassword",
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
                    $("#ChangePasswordModelClient").modal('hide');

                }
            })

        } else {
            $("#InputError").show();

        }

    })
})