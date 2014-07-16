<%@ Page Title="Inicio" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ITCR.Residencias.Interfaz._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <script src="js/boostrap-responsive.min.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class ="row-responsive">
        <div class ="col-xs-5">
            <div class="table">
            <h2 style="animation:normal"> LA REU </h2>

            <table class="table table-responsive table-bordered table-hover" id="table_inicio" runat="server">
                <thead>
                    <tr>
                        <th id ="txtNumero">#</th>
                        <th id ="txtActividad">Actividad</th>
                        <th id ="txtTaller">Taller</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="table-responsive">
                        <td>1</td>
                        <td>Campeonato Futbolín</td>
                        <td>Muralismo</td>
                    </tr>
                    <tr class="table-responsive">
                       <td>2</td>
                        <td>Campeonato de Fútbol, Equipo A</td>
                    </tr>
                </tbody>
            </table>
        </div>
        </div>
    </div>
</asp:Content>

