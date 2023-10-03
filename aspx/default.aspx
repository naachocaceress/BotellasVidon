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

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-Zenh87qX5JnK2Jl0vWa8Ck2rdkQ2Bzep5IDxbcnCeuOxjzrPF/et3URy9Bv1WTRi" crossorigin="anonymous" />
    <link href="css/Firma.css" rel="stylesheet" />

    <title>Vidon Bar</title>
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
                            <a href="defaultMenu.aspx" class="btn btn-success" type="button" style="width: 170px;">VVoucher</a>
                        </div>
                        <br />
                        <div>
                            <a href="menuValidar.aspx" class="btn btn-success" type="button" style="width: 170px;">Lector QR Vidon</a>
                        </div>
                    </div>

                    <div class="col-sm-6" style="border: solid white 2px; padding: 10px; border-radius: 10px; width: auto; margin: 10px;">
                        <div>
                            <a class="btn btn-success" type="button" id="btnBotellasGestor" style="width: 170px;">Gestor Botellas</a>
                        </div>
                        <br />
                        <div>
                            <a class="btn btn-success" id="btnBotellasCarga" type="button" style="width: 170px;">Cargar Botellas</a>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <script>
            var botella = "";
       

            document.getElementById("btnBotellasGestor").addEventListener("click", function () {
                // Abre el modal aquí
                var modal = document.getElementById("myModal");
                modal.style.display = "block";
                botella = "Gestion";
            });

            document.getElementById("btnBotellasCarga").addEventListener("click", function () {
                // Abre el modal aquí
                var modal = document.getElementById("myModal");
                modal.style.display = "block";
                botella = "Carga";
            });




            function seleccionarSucursal(nombreSucursal) {

                if (nombreSucursal === "2") {
                    window.location.href = "Botellas-" + botella + ".aspx?sucursal=2";

                } else if (nombreSucursal === "3") {
                    window.location.href = "Botellas-" + botella + ".aspx?sucursal=3";

                } else if (nombreSucursal === "4") {
                    window.location.href = "Botellas-" + botella + ".aspx?sucursal=4";

                } else if (nombreSucursal === "5") {
                    window.location.href = "Botellas-" + botella + ".aspx?sucursal=5";

                } else if (nombreSucursal === "6") {
                    window.location.href = "Botellas-" + botella + ".aspx?sucursal=6";

                } else if (nombreSucursal === "7") {
                    window.location.href = "Botellas-" + botella + ".aspx?sucursal=7";

                }
            }
        </script>

        <div id="myModal" class="modal" tabindex="11">
            <div class="modal-dialog modal-dialog-centered mx-auto" style="width: 90%;">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" style="text-align: center;">Selecciona la sucursal en donde te encuentras:</h5>
                    </div>
                    <div class="modal-body">
                        <p style="text-align: center;">
                            <br />
                            <button type="button" class="btn" style="width: 80%; background-color: #0c8444; color: white;" onclick="seleccionarSucursal('2')">Alta Córdoba</button><br />
                            <br />
                            <button type="button" class="btn" style="width: 80%; background-color: #0c8444; color: white;" onclick="seleccionarSucursal('3')">Achaval Rodriguez</button><br />
                            <br />
                            <button type="button" class="btn" style="width: 80%; background-color: #0c8444; color: white;" onclick="seleccionarSucursal('4')">Cerro</button><br />
                            <br />
                            <button type="button" class="btn" style="width: 80%; background-color: #0c8444; color: white;" onclick="seleccionarSucursal('5')">Barrio Jardin</button><br />
                            <br />
                            <button type="button" class="btn" style="width: 80%; background-color: #0c8444; color: white;" onclick="seleccionarSucursal('6')">Rondeau</button><br />
                            <br />
                            <button type="button" class="btn" style="width: 80%; background-color: #0c8444; color: white;" onclick="seleccionarSucursal('7')">Villa Allende</button><br />
                        </p>
                    </div>
                </div>
            </div>
        </div>



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
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-OERcA2EqjJCMA+/3y+gxIOqMEjwtxJY7qPCqsdltbNJuaOe923+mo//f6V8Qbsw3" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.2/dist/js/bootstrap.min.js" integrity="sha384-IDwe1+LCz02ROU9k972gdyvl+AESN10+x7tBKgc9I5HFtuNz0wWnPclzo6p9vxnk" crossorigin="anonymous"></script>
    <script src="js/bootstrap.min.js"></script>
</body>
</html>
