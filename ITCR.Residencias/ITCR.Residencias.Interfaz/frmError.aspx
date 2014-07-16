<%@ Page language="c#" Codebehind="frmError.aspx.cs" AutoEventWireup="false" Inherits="ITCR.Residencias.Interfaz.frmError" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>frmError</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<LINK href="css/itcr.css" type="text/css" rel="stylesheet">
        <script src="Scripts/jquery-1.7.1.js" type="text/javascript"></script>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script type="text/javascript" src="scripts/Interfaz.js"></script>
        <script>
            $("imCerrar").click(function cerrar() { alert("si"); });
        </script>

	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
            <IMG alt="Cerrar" id="imCerrar" style="Z-INDEX: 106; LEFT: 300px; CURSOR: hand; POSITION: absolute; TOP: 8px"
				    src="imagenes/salir.gif" onclick="self.close();">
			<asp:TextBox id="txtMensajeError" style="Z-INDEX: 107; LEFT: 132px; POSITION: absolute; TOP: 64px"
				runat="server" Height="60px" Width="216px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
			<asp:TextBox id="txtGeneradoPor" style="Z-INDEX: 108; LEFT: 132px; POSITION: absolute; TOP: 132px"
				runat="server" Height="20px" Width="216px" ReadOnly="True"></asp:TextBox>&nbsp;<asp:label id="Label3" style="Z-INDEX: 104; LEFT: 12px; POSITION: absolute; TOP: 172px" runat="server"
				Height="18px" Width="64px" CssClass="TextoError">Detalles:</asp:label>
			<asp:label id="Label2" style="Z-INDEX: 102; LEFT: 12px; POSITION: absolute; TOP: 68px" runat="server"
				Height="18px" Width="76px" CssClass="TextoError">Mensaje:</asp:label>
			<asp:label id="Label1" style="Z-INDEX: 103; LEFT: 12px; POSITION: absolute; TOP: 136px" runat="server"
				Height="18px" Width="104px" CssClass="TextoError">Generado por:</asp:label>
			<asp:label id="lblTitulo" style="Z-INDEX: 101; LEFT: 48px; POSITION: absolute; TOP: 12px" runat="server"
				Height="27px" Width="188px" CssClass="TituloPagina">Ocurrió un error</asp:label>
			<asp:TextBox id="txtDetallesError" style="Z-INDEX: 105; LEFT: 12px; POSITION: absolute; TOP: 196px"
				runat="server" Height="72px" Width="336px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox><IMG style="Z-INDEX: 109; LEFT: 12px; POSITION: absolute; TOP: 12px" src="imagenes/error.gif">
		</form>
	    
	</body>
</HTML>
