<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Botellas-Gestion.aspx.cs" Inherits="VidonVouchers.Botellas_Gestion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gestion de botellas</title>
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
            <h2 class="pb-2 border-bottom">Gestión de botellas guardadas</h2>

            <div class="col-md">

                <div class="contenedor">
                    <div class="row g-3">

                        <br />

                        <div class="table-responsive">

                            <asp:GridView ID="gvVouchersCreados" runat="server" class="table table-striped table-hover" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gvVouchersCreados_PageIndexChanging" PageSize="50">
                                <Columns>
                                    <asp:BoundField DataField="idVouchers" HeaderText="ID VCH" />
                                    <asp:BoundField DataField="nombre" HeaderText="Cliente" />
                                    <asp:BoundField DataField="monto" HeaderText="Monto" HeaderStyle-CssClass="right-align-header" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="saldo" HeaderText="Saldo" HeaderStyle-CssClass="right-align-header" DataFormatString="{0:N0}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechaVen" HeaderText="Fecha Ven." HeaderStyle-CssClass="right-align-header" DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="sucursal_valida" HeaderText="Sucursal" />
                                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                                    <asp:BoundField DataField="creado_por" HeaderText="Creado por" />
                                    <asp:BoundField DataField="fechaCreacion" HeaderText="Fecha creado" />
                                    <asp:BoundField DataField="horaCreacion" HeaderText="Hora creado" />
                                </Columns>
                            </asp:GridView>

                        </div>



                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
