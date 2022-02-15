// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#btnLaunch").on("click", () => {

    $.ajax({
        url: '/Home/Launch',
        data: {
            'Command': $("#Command").val()
        }
    }).then(function (data) {
        $("#taResult").append(data.output);
    });
});