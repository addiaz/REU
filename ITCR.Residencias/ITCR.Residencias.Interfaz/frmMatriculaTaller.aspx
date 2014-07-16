<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmMatriculaTaller.aspx.cs" Inherits="ITCR.Residencias.Interfaz.frmMatricula" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="PanelCentral" class="centro" runat="server" style="text-align:center">
        <div class="row">
            <div class="col-xs-2>"></div>        
            <div class="col-sm-6">
                <section>
                    <div class="table table-responsive">
                        <h2>Talleres</h2>
                        <table id="tableTalleres" class="table table-responsive table-bordered table-hover" runat="server" style="margin-left:20px">
                            <thead>
                                <tr class="table">
                                    <th id ="txtNumero">#</th>
                                </tr>
                            </thead>
                            <tbody>
                            

                            </tbody>
                        </table>
                    </div>
            
                    <asp:DropDownList ID="ddlListaTalleres" runat="server" CssClass=" btn btn-default">
                        </asp:DropDownList>

                        <asp:Button ID="btnMatricular" runat="server" Text="Matricular" CssClass="btn btn-default"
                                            type="submit"  onclick="btnMatricular_Click"/>
                </section>
            </div>

            <div class="col-sm-5">
            <!-- Segunda Columna -->
                    <br /><br /><br />
                    <asp:Image ID="Image1" runat="server" CssClass="img-responsive" ImageUrl="~/imagenes/REU.JPG" Height="128px" Width="254px"/>
                    <br />
                    <h2>
                        Taller Matriculado
                    </h2>
                    <br />
                    <asp:DropDownList ID="ddlListaTalleresMatriculados" runat="server" Enabled="false" CssClass=" btn btn-default">
                        </asp:DropDownList>
                    
                        <asp:Button ID="btnDesmatricular" runat="server" Text="Desmatricular" Enabled="false" CssClass="btn btn-default" ToolTip="Desmatricular"
                            type="button" onclick="btnDesmatricular_Click"/>

            </div>
        </div>
    </div>

    
    

</asp:Content>