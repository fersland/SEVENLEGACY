$(document).ready(function () {
    $("#btnIngresar").click(function () {
        var user = $("#txtusername").val();
        var pass = $("#txtpassword").val();

        $.ajax({
            type: "POST",
            url: "Login.aspx/ProcesarLogin",
            data: JSON.stringify({ usuario: user, password: pass }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                console.log("Respuesta del servidor:", response.d);
                if (response.d === "OK") {
                    window.location.href = "Inicio.aspx";
                } else {
                    alert("Mensaje: " + response.d);
                }
            },
            error: function (xhr, status, error) {
                alert("Error: " + xhr.responseText);
                console.log(xhr.responseText);
            }
        });
    });
});