<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmDatos.aspx.cs" Inherits="ITCR.Residencias.Interfaz.frmDatos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 80%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style1">
        <tr>
            <td style="text-align: left">
                <asp:label id="lblTitulo" runat="server"
								CssClass="SubTituloPagina" Height="24px" Width="480px">Datos</asp:label>
                <br />
                <br />
                <asp:button id="cmdGuardar" runat="server" Width="80px" Text="Guardar" CssClass="BotonColor" onclick="cmdGuardar_Click"></asp:button>
                <br />
                <br />
                <asp:label id="Label2" runat="server" CssClass="Body">Código:</asp:label>
                &nbsp;<asp:textbox id="txtCodArea" runat="server" CssClass="CampoTexto" Width="81px"></asp:textbox>
                <br />
                <asp:label id="Label3" runat="server">Nombre:</asp:label>
				&nbsp;<asp:textbox id="txtNomArea" runat="server" CssClass="CampoTexto" Width="256px"></asp:textbox>
                &nbsp;<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" Height="16px" Width="120px" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtNomArea"
								CssClass="TextoError"> Dato requerido</asp:RequiredFieldValidator>
                <br />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
