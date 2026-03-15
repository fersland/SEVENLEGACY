$(document).ready(function () {
    $("#btnImprimir").click(function () {
        console.log("Clic en imprimir detectado");
        var textoFiltro = $("#txtFiltro").val() || "";
        var url = "ReporteCliente.aspx?filtro=" + encodeURIComponent(textoFiltro);

        window.open(url, "_blank");
    });

    var timeout = null;

    $("#txtFiltro").on("input", function () {
        if ($(this).val().length > 0) {
            $("#btnClear").show();
        } else {
            $("#btnClear").hide();
        }

        clearTimeout(timeout);
        timeout = setTimeout(function () {
            cargarTabla();
        }, 300);
    });

    $("#btnClear").click(function () {
        $("#txtFiltro").val("");
        $(this).hide();
        $("#txtFiltro").focus();
        cargarTabla();
    });

    $("#txtFechaNac").datepicker({
        dateFormat: 'yy-mm-dd',
        changeMonth: true,
        changeYear: true,
        yearRange: "-100:-18",
        maxDate: "-18Y"
    });

    $("#txtCedula").on("input", function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    $("#txtCedula").on("keypress", function (e) {
        if (e.which < 48 || e.which > 57) {
            return false;
        }
    });

    $("#txtNombre").on("keypress", function (e) {
        var key = e.keyCode || e.which;
        var tecla = String.fromCharCode(key);
        var letras = " abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZáéíóúÁÉÍÓÚ";

        if (letras.indexOf(tecla) === -1) {
            return false;
        }
    });

    $("#btnLimpiar").click(function () {
        limpiarFormulario();
    });

    $(document).tooltip();

    listarEstadosCiviles();
    cargarTabla();

    function listarEstadosCiviles() {
        $.ajax({
            type: "POST",
            url: "Inicio.aspx/listarEstadosCiviles",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                var ddl = $("#ddlEstadoCivil");
                ddl.empty();
                $.each(r.d, function () {
                    ddl.append($("<option></option>").val(this.Id).html(this.Descripcion));
                });
            }
        });
    }

    $("#btnGuardar").click(function () {
        var idActual = $("#txtCedula").attr("data-id") || 0;
        var cedula = $("#txtCedula").val().trim();
        var nombre = $("#txtNombre").val().trim();
        var fechaNac = $("#txtFechaNac").val();
        var idEstadoCivil = $("#ddlEstadoCivil").val();

        var regexNombre = /^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$/;

        if (cedula.length !== 10 || isNaN(cedula)) {
            alert("La cédula debe tener 10 dígitos numéricos.");
            $("#txtCedula").focus();
            return;
        }

        if (nombre === "") {
            alert("Por favor, ingrese el nombre del cliente.");
            $("#txtNombre").focus();
            return;
        }

        if (!regexNombre.test(nombre)) {
            alert("El nombre solo debe contener letras y espacios.");
            $("#txtNombre").focus();
            return;
        }

        if (fechaNac === "") {
            alert("Debe seleccionar una fecha de nacimiento.");
            return;
        }

        var fechaSeleccionada = new Date(fechaNac);
        var hoy = new Date();
        var edad = hoy.getFullYear() - fechaSeleccionada.getFullYear();
        var mes = hoy.getMonth() - fechaSeleccionada.getMonth();

        if (mes < 0 || (mes === 0 && hoy.getDate() < fechaSeleccionada.getDate())) {
            edad--;
        }

        if (edad < 18) {
            alert("El cliente debe ser mayor de edad (mínimo 18 años).");
            return;
        }

        if (idEstadoCivil === null || idEstadoCivil === "0") {
            alert("Seleccione un estado civil válido.");
            return;
        }

        var idActual = $("#txtCedula").attr("data-id") || 0;

        var obj = {
            IdClie: parseInt(idActual),
            Cedula: cedula,
            Nombre: nombre,
            Genero: $("#ddlGenero").val(),
            FechaNac: fechaNac,
            IdEstadoCivil: parseInt(idEstadoCivil)
        };

        $.ajax({
            type: "POST",
            url: "Inicio.aspx/GuardarCliente",
            data: JSON.stringify({ obj: obj }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d === "OK") {
                    alert("Operación exitosa");
                    $("#txtCedula").removeAttr("data-id");
                    $("#btnGuardar").text("Guardar Registro");
                    limpiarFormulario();
                    cargarTabla();
                } else {
                    alert("Respuesta del servidor: " + response.d);
                }
            }
        });
    });

    function limpiarFormulario() {
        $("#txtCedula").val("");
        $("#txtNombre").val("");
        $("#txtFechaNac").val("");

        $("#ddlGenero").val("M");
        $("#ddlEstadoCivil").val($("#ddlEstadoCivil option:first").val());

        $("#txtCedula").removeAttr("data-id");
        $("#btnGuardar").text("Guardar Registro");

        $("#txtCedula").focus();
    }

    $("#btnCerrarSesion").click(function () {
        if (confirm("¿Desea cerrar la sesión actual?")) {
            $.ajax({
                type: "POST",
                url: "Inicio.aspx/Logout",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d === "OK") {
                        window.location.href = "Login.aspx";
                    }
                }
            });
        }
    });
});

function cargarTabla() {
    var filtro = $("#txtFiltro").val();

    if (filtro.length > 0) {
        $("#tablaClientes tbody").html("<tr><td colspan='6' style='text-align:center;'>Buscando...</td></tr>");
    }

    $.ajax({
        type: "POST",
        url: "Inicio.aspx/Listar",
        data: JSON.stringify({ filtro: filtro }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var clientes = response.d;
            var html = "";

            $.each(clientes, function (i, item) {
                var fecha = new Date(parseInt(item.FechaNac.substr(6)));
                var fechaStr = fecha.toLocaleDateString();

                html += "<tr>";
                html += "<td>" + item.Cedula + "</td>";
                html += "<td>" + item.Nombre + "</td>";
                html += "<td>" + (item.Genero == 'M' ? 'Masculino' : 'Femenino') + "</td>";
                html += "<td>" + fechaStr + "</td>";
                html += "<td>" + (item.EstadoCivil || "N/A") + "</td>";
                html += "<td>" +
                    "<button type='button' onclick='editar(" + JSON.stringify(item) + ")'>Editar</button> " +
                    "<button type='button' onclick='eliminar(" + item.IdClie + ")'>Eliminar</button>" +
                    "</td>";
                html += "</tr>";
            });

            $("#tablaClientes tbody").html(html);
        }
    });
}

function editar(item) {
    console.log("Editando:", item);
    $("#txtCedula").val(item.Cedula).attr("data-id", item.IdClie);
    $("#txtNombre").val(item.Nombre);
    $("#ddlGenero").val(item.Genero);

    var milisegundos = parseInt(item.FechaNac.replace(/\/Date\((.*?)\)\//, '$1'));
    var fecha = new Date(milisegundos);
    var yyyy = fecha.getFullYear();
    var mm = String(fecha.getMonth() + 1).padStart(2, '0');
    var dd = String(fecha.getDate()).padStart(2, '0');
    $("#txtFechaNac").val(yyyy + "-" + mm + "-" + dd);

    $("#ddlEstadoCivil").val(item.IdEstadoCivil);
    $("#btnGuardar").text("Actualizar Registro");
}

function eliminar(id) {
    if (confirm("¿Está seguro de que desea eliminar este cliente?")) {
        $.ajax({
            type: "POST",
            url: "Inicio.aspx/EliminarCliente",
            data: JSON.stringify({ id: id }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d === "OK") {
                    alert("Registro eliminado correctamente");
                    cargarTabla();
                } else {
                    alert("Error: " + response.d);
                }
            },
            error: function (xhr) {
                alert("Error al intentar eliminar: " + xhr.responseText);
            }
        });
    }
}