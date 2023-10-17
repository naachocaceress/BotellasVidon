<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Botellas-Gestion.aspx.cs" Inherits="VidonVouchers.Botellas_Gestion" EnableEventValidation="false" %>

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


                        <div class="d-sm-flex justify-content-between flex-wrap">
                            <div class="component-container">
                                <label class="component-label">Buscar por id botella</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtId" runat="server" class="form-control" Style="padding: 5px; border: 1px solid #ccc; border-radius: 4px;" placeholder="Ingresar id" TextMode="Number"></asp:TextBox>
                                    &nbsp;&nbsp;
                                        <div class="input-group-append">
                                            <asp:Button ID="btnId" class="btn btn-primary" runat="server" Text="Buscar" OnClick="btnId_Click" />
                                        </div>
                                </div>
                            </div>


                            <div class="component-container">
                                <label class="component-label">Buscar por documento</label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtDni" runat="server" class="form-control" Style="padding: 5px; border: 1px solid #ccc; border-radius: 4px;" placeholder="Ingresar documento" TextMode="Number"></asp:TextBox>
                                    &nbsp;&nbsp;
                                        <div class="input-group-append">
                                            <asp:Button ID="btnDni" class="btn btn-primary" runat="server" Text="Buscar" OnClick="btnDni_Click" />
                                        </div>
                                </div>
                            </div>

                        </div>



                        <div class="d-sm-flex justify-content-between flex-wrap">
                            <div class="d-flex flex-column flex-md-row align-items-md-center">
                                <asp:Button ID="btnExportExcelBotellas" runat="server" Text="Exportar a Excel" class="btn btn-outline-success" OnClick="btnExportExcelBotellas_Click" />
                            </div>
                            &nbsp;
                            <div class="d-flex align-items-center">
                                <span style="white-space: nowrap; margin-right: 5px;">Cantidad de resultados:</span>

                                <asp:DropDownList ID="ddlPageSizeBotellas" runat="server" class="form-select ml-2" Style="max-width: 80px;" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSizeBotellas_SelectedIndexChanged" Disabled="true">
                                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                    <asp:ListItem Text="200" Value="200"></asp:ListItem>
                                    <asp:ListItem Text="300" Value="300"></asp:ListItem>
                                    <asp:ListItem Text="400" Value="400"></asp:ListItem>
                                    <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>


                        <div class="table-responsive">

                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>

                                    <asp:GridView ID="gvBotellas" runat="server" class="table table-striped table-hover" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gvBotellas_PageIndexChanging" PageSize="50" DataKeyNames="idBotella">
                                        <Columns>
                                            <asp:BoundField DataField="idBotella" HeaderText="ID Botella" />
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                            <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                                            <asp:BoundField DataField="dni" HeaderText="Documento" HeaderStyle-CssClass="right-align-header" DataFormatString="{0:N0}"></asp:BoundField>
                                            <asp:BoundField DataField="fechaGuar" HeaderText="Fecha Guardado" HeaderStyle-CssClass="right-align-header" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                                            <asp:BoundField DataField="mozo" HeaderText="Guardado por" />

                                            <asp:TemplateField HeaderText="Estado">
                                                <ItemTemplate>
                                                    <asp:Button runat="server" ID="btnEstado" OnClick="btnEstado_Click"
                                                        Style="width: 80px;" Text='<%# Eval("estado").ToString() == "B" ? "Inactivo" : "Activo" %>'
                                                        CssClass='<%# Eval("estado").ToString() == "B" ? "btn btn-danger" : "btn btn-success" %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                    </div>

                              </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                </div>
            </div>
    </form>


    <%--<footer style="background-color: #f4f4f4; padding: 10px; text-align: center; width: 100%; position: absolute; bottom: 0;">
        <span class="version" style="font-style: italic;">Botellas - Versión 1.0</span>&nbsp;<span class="brand" style="font-weight: bold;">&copy;Vidónbar</span>
    </footer>--%>

</body>
</html>
