


$(document).ready(function () {
    $("#myForm").submit(function (e) {
        e.preventDefault();

        $.ajax({
            url: "/Home/GuardarDatos",
            method: "POST",
            data: $(this).serialize(),
            dataType: "json",
            success: function (data) {

                if (data.botellaId == 0) {
                    var divOtrosCampos = document.getElementById("otrosCampos");
                    divOtrosCampos.style.display = "block";

                } else {

                    // Maneja la respuesta aquí, por ejemplo, muestra el número fernetero en el modal
                    $("#staticBackdrop .modal-body h1").text(data.botellaId);
                    $("#staticBackdrop .modal-header h1").text("Hola " + data.clienteName + "! Tu número fernetero");

                    // Abre el modal
                    $('#staticBackdrop').modal('show');
                }
            },
            error: function () {
                // Maneja errores si es necesario
                alert("Ocurrió un error al enviar el formulario.");
            }
        });


    });
});

