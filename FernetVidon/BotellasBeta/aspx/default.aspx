﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="VidonVouchers._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans&display=swap" rel="stylesheet" />

    <script src="https://cdn.jsdelivr.net/npm/jquery@3.6.0/dist/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="css/Firma.css" rel="stylesheet" />

    <title>Vidón Bar</title>
</head>

<body style="background: url(Imagenes/fondoNC.png) no-repeat center center fixed; background-size: cover; background-position: center;">

    <form id="form1" runat="server">

        <div style="text-align: center; max-width: 550px; margin: auto;">
            <img src="Imagenes/Vidon bar ORIGINAL.png" style="max-width: 60%; height: auto;">
        </div>

        <div style="position: fixed; bottom: 90px; width: 100%;">
            <div class="container">
                <div class="row g-3" style="text-align: center; padding: 10px; display: flex; justify-content: center;">

                    <div class="col-sm-6" style="border: solid white 2px; padding: 10px; border-radius: 10px; width: auto; margin: 10px;">
                        <div>
                            <a href="VVouchers/defaultMenu.aspx" class="btn btn-success" type="button" style="width: 170px;">VVoucher</a>
                        </div>
                        <br />
                        <div>
                            <a href="VVouchers/menuValidar.aspx" class="btn btn-success" type="button" style="width: 170px;">Lector QR Vidon</a>
                        </div>
                    </div>

                    <div class="col-sm-6" style="border: solid white 2px; padding: 10px; border-radius: 10px; width: auto; margin: 10px;">
                        <div>
                            <asp:Button ID="btnBotellasGestor" runat="server" class="btn btn-success" Text="Gestor Botellas" OnClick="btnBotellasGestor_Click" Style="width: 170px;" />
                        </div>
                        <br />
                        <div>
                            <asp:Button ID="btnBotellasCarga" class="btn btn-success" runat="server" Text="Cargar Botellas" OnClick="btnBotellasCarga_Click" Style="width: 170px;" />
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div id="myModal" class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content mx-auto" style="width: 90%;">
                    <div class="modal-header">
                        <h3 class="modal-title fs-5" style="text-align: center;" id="staticBackdropLabel">
                            <asp:Label ID="lblNombre" runat="server" Text="Selecciona la sucursal en donde te encuentras:"></asp:Label></h3>
                    </div>
                    <div class="modal-body">

                        <asp:Panel ID="Panel1" HorizontalAlign="Center" runat="server">
                        </asp:Panel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

        <style>
            .btn-space {
                margin-bottom: 15px;
            }
        </style>


        <div id="Final" style="position: fixed; z-index: 9; bottom: 0; left: 0; right: 0; text-align: center;">
            <br />
            <hr align="center" id="re" />
            <br />
            <a href="https://www.linkedin.com/in/nacho-caceres/">
                <img src="Imagenes/Neptune.png" />
            </a>
            <p>Desarollado por <b>Nacho Caceres</b></p>
        </div>

    </form>
</body>
</html>
