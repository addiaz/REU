<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmMantenimiento.aspx.cs" Inherits="ITCR.Residencias.Interfaz.frmMantenimiento" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
		<asp:label id="Label1" CssClass="SubTituloPagina" Height="24px" Width="336px" runat="server">Plantilla de Mantenimiento</asp:label>
	</p>
	<p>
		<asp:LinkButton ID="lnkbNuevo" runat="server">Nuevo...</asp:LinkButton>
	</p>
	<p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		<asp:TextBox ID="txtCol1" runat="server" Width="100px"></asp:TextBox>
		<asp:textbox id="txtCol2" runat="server" Width="175px"></asp:textbox>
		<asp:textbox id="txtCol3" runat="server" Width="130px"></asp:textbox>
		<asp:ImageButton ID="btnBuscar" runat="server" 
            ImageUrl="~/imagenes/BotonBuscar.gif" onclick="btnBuscar_Click" />
	</p>
	<p>
		<asp:GridView ID="gvDatos" runat="server" CellPadding="4" ForeColor="#333333" 
			BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
			AutoGenerateColumns="False" HorizontalAlign="Center" ShowFooter="True" 
			style="text-align: left" AllowPaging="True" 
            onpageindexchanging="gvDatos_PageIndexChanging" 
            onrowcreated="gvDatos_RowCreated" Width="497px">
			<AlternatingRowStyle BackColor="White" ForeColor="#284775" />
			<Columns>
				<asp:BoundField DataField="UNICOD" HeaderText="Código" >
                <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="DescripcionCF" HeaderText="Nombre" >
                <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="CCOSTO" HeaderText="C. Costo" >
				<ItemStyle Width="150px" />
                </asp:BoundField>
				<asp:TemplateField>
					<ItemTemplate>
						<asp:ImageButton ID="ImageButton1" runat="server" 
							ImageUrl="~/imagenes/BotonEditar2.gif" />
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField>
					<ItemTemplate>
						<asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar" 
							ImageUrl="~/imagenes/BotonElim.gif" />
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
			<EditRowStyle BackColor="#999999" />
			<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
			<HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" 
                HorizontalAlign="Center" />
			<PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
			<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
			<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
			<SortedAscendingCellStyle BackColor="#E9E7E2" />
			<SortedAscendingHeaderStyle BackColor="#506C8C" />
			<SortedDescendingCellStyle BackColor="#FFFDF8" />
			<SortedDescendingHeaderStyle BackColor="#6F8DAE" />
		</asp:GridView>
	</p>
</asp:Content>
