﻿//VIEJO FUNCIONA

$(document).ready(function () {
    $("#myForm").submit(function (e) {
        e.preventDefault(); // Evita que el formulario se envíe normalmente

        $.ajax({
            url: "/Home/GuardarDatos",
            method: "POST",
            data: $(this).serialize(),
            success: function (data) {
                // Maneja la respuesta aquí, por ejemplo, muestra el número fernetero en el modal
                $("#staticBackdrop .modal-body h1").text(data.botellaId);
                $("#staticBackdrop .modal-header h1").text("Hola " + data.clienteName + "! Tu número fernetero" );

                // Abre el modal
                $('#staticBackdrop').modal('show');
            },
            error: function () {
                // Maneja errores si es necesario
                alert("Ocurrió un error al enviar el formulario.");
            }
        });
    });
});






//NUEVO REVISAR


$(document).ready(function () {
    $("#myForm").submit(function (e) {
        e.preventDefault(); // Evita que el formulario se envíe normalmente

        $.ajax({
            url: "/Home/GuardarDatos",
            method: "POST",
            data: $(this).serialize(),
            success: function (data) {
                if (data.clienteExiste) {
                    // Maneja la respuesta aquí, por ejemplo, muestra el número fernetero en el modal
                    $("#staticBackdrop .modal-body h1").text(data.botellaId);
                    $("#staticBackdrop .modal-header h1").text("Hola " + data.clienteName + "! Tu número fernetero");

                    // Abre el modal
                    $('#staticBackdrop').modal('show');
                } else {
                    document.getElementById("btnAceptar").value = "1"; // Cambia el estado del botón

                }
            },
            error: function () {
                // Maneja errores si es necesario
                alert("Ocurrió un error al enviar el formulario.");
            }
        });


    });
});