<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Botellas-Carga.aspx.cs" Inherits="VidonVouchers.Botellas_Carga" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Botella fernetera</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</head>

<body>
    <header class="p-3" style="background-color: #0C8444; z-index: 9; left: 0; right: 0; text-align: center;">
        <div class="container">
            <div class="d-flex flex-wrap align-items-center justify-content-center" style="height: 50px;">
                <a href="https://www.instagram.com/vidonbar/" class="d-flex align-items-center mb-2 mb-lg-0 text-white text-decoration-none">
                    <img src="/Imagenes/Vidon bar ORIGINAL.png" alt="logo" style="width: 60px;">
                </a>
                &nbsp;&nbsp;&nbsp;
                <div class="text-end">
                    <h3 class="float-md-start mb-0" style="color: white;">VIDÓN BAR</h3>
                </div>
            </div>
            <hr />
            <span class="sucursal" style="color: #fdc030;">Sucursal:
    <label id="lblSucu" runat="server"></label>
            </span>
        </div>
    </header>


    <form id="form1" runat="server">
        <div class="text-center" style="padding-left: 50px; padding-right: 50px; margin-bottom: 60px;">

            <br />
            <h2 class="pb-2 border-bottom">Tu botella de Fernet!</h2>
            <p class="lead">. Completa tus datos y consigue tu número fernetero.</p>


            <div class="col-md">


                <div class="contenedor">
                    <div class="row g-3">


                        <!-- --------------------------------------------------------------------------------------------------------- -->

                        <div class="col-12">
                            <label for="" class="form-label">Documento</label>
                            <div class="input-group has-validation">
                                <asp:TextBox ID="dni" runat="server" class="form-control" required="true" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>


                        <!-- --------------------------------------------------------------------------------------------------------- -->

                        <div id="otrosCampos" class="col-12" runat="server" style="display: none;">

                            <div class="col-12">
                                <label for="Nombre" class="form-label">Nombre</label>
                                <asp:TextBox ID="nombre" runat="server" class="form-control"></asp:TextBox>
                            </div>


                            <!-- --------------------------------------------------------------------------------------------------------- -->

                            <div class="col-12">
                                <label for="" class="form-label">Apellido</label>
                                <asp:TextBox ID="apellido" runat="server" class="form-control"></asp:TextBox>
                            </div>

                            <!-- --------------------------------------------------------------------------------------------------------- -->

                            <div class="col-12">
                                <label for="" class="form-label">Número de teléfono</label>
                                <div class="input-group has-validation">
                                    <asp:TextBox ID="nroTelefono" runat="server" class="form-control" TextMode="Phone"></asp:TextBox>
                                </div>
                            </div>

                            <!-- --------------------------------------------------------------------------------------------------------- -->

                            <div class="col-12">
                                <label for="" class="form-label">Fecha de nacimiento</label>
                                <div class="input-group has-validation">
                                    <asp:TextBox ID="fechaNacimiento" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <!-- --------------------------------------------------------------------------------------------------------- -->

                        <div class="col-12">
                            <label for="" class="form-label">Mozo que lo atendió</label>
                            <div class="input-group has-validation">
                                <asp:TextBox ID="mozo" runat="server" class="form-control" required="true"></asp:TextBox>
                            </div>
                        </div>


                        <!-- --------------------------------------------------------------------------------------------------------- -->

                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:VVoucher2ConnectionString %>" InsertCommand="INSERT INTO Clientes.Cliente(dni, nombre, apellido, fechaNacimiento, numeroTelefono) VALUES (@dni, @nombre, @apellido, @fechaNacimiento, @nroTelefono)" SelectCommand="select * from Clientes.Cliente">
                            <InsertParameters>
                                <asp:Parameter Name="dni"></asp:Parameter>
                                <asp:Parameter Name="nombre"></asp:Parameter>
                                <asp:Parameter Name="apellido"></asp:Parameter>
                                <asp:Parameter Name="fechaNacimiento"></asp:Parameter>
                                <asp:Parameter Name="nroTelefono"></asp:Parameter>
                            </InsertParameters>
                        </asp:SqlDataSource>

                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:VVoucher2ConnectionString %>"
                            InsertCommand="INSERT INTO Clientes.Botellas (numeroBotella, quienGuardoMozo, fechaGuardado, fechaVencimiento, estado, idCliente, idSucursal)
                            VALUES (@numeroBotella, @mozo, @fechaGuardado, @fechaVencimiento, 'A', @idCliente, @idSucursal);"
                            SelectCommand="SELECT * FROM Clientes.Botellas">
                            <InsertParameters>
                                <asp:Parameter Name="numeroBotella" />
                                <asp:Parameter Name="mozo" />
                                <asp:Parameter Name="fechaGuardado" />
                                <asp:Parameter Name="fechaVencimiento" />
                                <asp:Parameter Name="idCliente" />
                                <asp:Parameter Name="idSucursal" />
                            </InsertParameters>
                        </asp:SqlDataSource>


                        <div class="col-12">
                            <br />
                            <asp:Button ID="btnAceptar" class="w-100 btn btn-lg" Style="background-color: #0c8444; color: white;" runat="server" Text="Guardar" OnClick="btnAceptar_Click" />
                        </div>


                        <!-- Modal -->
                        <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                            <div class="modal-dialog ">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h3 class="modal-title fs-5" id="staticBackdropLabel">
                                            <asp:Label ID="lblNombre" runat="server" Text="Label"></asp:Label></h3>
                                    </div>
                                    <div class="modal-body">
                                        <h1 style="font-size: 60px;">
                                            A<asp:Label ID="lblNumero" runat="server" Text="Label"></asp:Label></h1>
                                        <p>Recuerda darle este número al mozo que te atendio</p>
                                    </div>

                                    <div class="modal-footer">
                                        <a href="https://www.instagram.com/vidonbar/" class="btn btn-danger" id="cerrarBtn">Cerrar</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <script>
                            // Registra la hora de inicio de la visita en una cookie o almacenamiento local
                            localStorage.setItem('inicioVisita', new Date().getTime());

                            // Establece un temporizador para verificar el tiempo transcurrido
                            var tiempoTranscurrido = 0; // Declarar la variable fuera de la función
                            var tiempoLimite = 900000; // 10 minuto en milisegundos

                            var verificarTiempo = function () {
                                var inicioVisita = localStorage.getItem('inicioVisita');
                                tiempoTranscurrido = new Date().getTime() - inicioVisita;

                                if (tiempoTranscurrido >= tiempoLimite) {
                                    // Redirige a Instagram
                                    window.location.href = 'https://www.instagram.com/vidonbar/';
                                }
                            };

                            setInterval(verificarTiempo, 1000); // Verifica cada segundo

                            // En la redirección a Instagram, agrega un nuevo estado a la historia
                            window.history.pushState({}, document.title, 'Redirect.aspx');

                            // Para prevenir la navegación hacia atrás
                            window.onpopstate = function (event) {
                                if (window.location.href.indexOf('Redirect.aspx') === -1) {
                                    // Si intenta volver atrás, redirige nuevamente a Instagram
                                    window.location.href = 'https://www.instagram.com/vidonbar/';
                                }
                            };

                            // Imprime el tiempo transcurrido en la consola
                            setInterval(function () {
                                console.log(tiempoTranscurrido / 1000);
                            }, 1000);


                        </script>

                    </div>
                </div>
            </div>

        </div>

    </form>

</body>
</html>
