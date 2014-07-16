<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmPerfil.aspx.cs" Inherits="ITCR.Residencias.Interfaz.frmPerfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .btn-default {}
        #Text1 {
            width: 53px;
        }
        #inputEdificio {
            width: 54px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <asp:Image ID="imgPerfil" runat="server" CssClass="btn btn-default" Height="184px" ImageAlign="Left" ToolTip="Imagen Perfil" Width="215px" />
        <div class="col-xs-3">
            <section id="informacion">
                <label id="lblNombre" runat="server" style="color:gray">
                    Nombre: 
                </label>
                <br />
                <label id="lblApellido" runat="server" style="color:gray">
                    Apellido:
                </label>
                <br />
                <label id="lblCarne" runat="server" style="color:gray">
                    Carné:
                </label>
                <br />
                <label id="lblCorreo" runat="server" style="color:gray">
                    Correo:
                </label>
                <br />
                <label id="lblEdificio" runat="server" style="color:gray">
                    Edificio:
                </label>
                <br />
                <label id="lblCuarto" runat="server" style="color:gray">
                    Cuarto:
                </label>
            </section>
        </div>
        <div class="col-xs-3">
            <section id="informacion2">
                <asp:Button ID="btnSubir" runat="server" Text="Subir Foto" CssClass="btn btn-default" />
                <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-default" />
            </section>
        </div>
    </div>
</asp:Content>
