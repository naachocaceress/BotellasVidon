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
                <a href="default.aspx" class="d-flex align-items-center mb-2 mb-lg-0 text-white text-decoration-none">
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
        <div class="text-center" style="padding-left: 50px; padding-right: 50px;">

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
                                <input id="dni" name="dni" type="text" runat="server" class="form-control" required>
                            </div>
                        </div>


                        <!-- --------------------------------------------------------------------------------------------------------- -->

                        <div id="otrosCampos" class="col-12" runat="server" style="display: none;">

                            <div class="col-sm-6" style="display: none;">
                                <label for="Nombre" class="form-label">Nombre</label>
                                <input id="nombre" name="Nombre" type="text" runat="server" class="form-control">
                            </div>


                            <!-- --------------------------------------------------------------------------------------------------------- -->

                            <div class="col-sm-6">
                                <label for="" class="form-label">Apellido</label>
                                <input id="apellido" name="Apellido" runat="server" type="text" class="form-control">
                            </div>

                            <!-- --------------------------------------------------------------------------------------------------------- -->

                            <div class="col-sm-6">
                                <label for="" class="form-label">Número de teléfono</label>
                                <div class="input-group has-validation">
                                    <input id="tel" runat="server" type="tel" name="NumeroTelefono" class="form-control">
                                </div>
                            </div>

                            <!-- --------------------------------------------------------------------------------------------------------- -->

                            <div class="col-sm-6">
                                <label for="" class="form-label">Fecha de nacimiento</label>
                                <div class="input-group has-validation">
                                    <input id="date" runat="server" type="date" name="FechaNacimiento" class="form-control" placeholder="Username" textmode="Date">
                                </div>
                            </div>

                        </div>

                        <!-- --------------------------------------------------------------------------------------------------------- -->

                        <div class="col-12">
                            <label for="" class="form-label">Mozo que lo atendió</label>
                            <div class="input-group has-validation">
                                <input id="mozo" runat="server" type="text" name="QuienGuardoMozo" class="form-control" required>
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

                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:VVoucher2ConnectionString %>" InsertCommand="INSERT INTO Clientes.Botellas (quienGuardoMozo, fechaGuardado, fechaVencimiento, estado, idCliente, idSucursal)
                            VALUES (@mozo, @fechaGuardado, @fechaVencimiento, 'A', @idCliente, @idSucursal);"
                            SelectCommand="select * from Clientes.Botellas">
                            <InsertParameters>
                                <asp:Parameter Name="mozo"></asp:Parameter>
                                <asp:Parameter Name="fechaGuardado"></asp:Parameter>
                                <asp:Parameter Name="fechaVencimiento"></asp:Parameter>
                                <asp:Parameter Name="idCliente"></asp:Parameter>
                                <asp:Parameter Name="idSucursal"></asp:Parameter>
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
                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    </div>
                                    <div class="modal-body">
                                        <h1 style="font-size: 60px;">
                                            <asp:Label ID="lblNumero" runat="server" Text="Label"></asp:Label></h1>
                                        <p>Recuerda darle este número al mozo que te atendio</p>
                                    </div>

                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
            </div>

        </div>

    </form>
</body>
</html>
