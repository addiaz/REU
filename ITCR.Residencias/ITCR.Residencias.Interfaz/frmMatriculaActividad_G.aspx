<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmMatriculaActividad_G.aspx.cs" Inherits="ITCR.Residencias.Interfaz.frmMatriculaActividad_G" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="PanelCentral" class="centro" runat="server">
        <div class="row">
            <div class="col-sm-1"></div>
            <div class="col-sm-4">
                <section>
                    <div class="table table-responsive">
                        <h2>Actividades</h2>
                        <table id="tablaActividades" class="table table-responsive table-bordered" runat="server" style="margin-left:20px">
                            <thead>
                                <tr class="table">
                                    <th>#</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
            
                    <label> Actividades: </label>

                    <asp:DropDownList ID="ddlActividades" runat="server" CssClass="btn btn-default">
                    </asp:DropDownList>

                    <select id="selectActividades" runat="server" class="btn btn-default">
                        <option></option>

                    </select>

                        <asp:Button ID="btnMostrarEstudiantes" runat="server" Text="Mostrar Residentes" CssClass="btn btn-default" 
                                    type="submit" onclick="btnMostrarEstudiantes_Click"/>

                    </section>

                    <asp:Panel ID="panelEstudiante" runat="server" Visible ="false">
                        <br />
                        <label> Capitán:
                            <asp:TextBox ID="txtCapitan" runat="server" Text="Yo"  CssClass="form-control" Enabled="false"></asp:TextBox>
                        </label>

                        <br />
                        <label> Nombre de Equipo:
                            <asp:TextBox ID="txtNombreEquipo" runat="server" Text = ""  CssClass="form-control" Enabled="true"></asp:TextBox>
                        </label>

                        <br />
                        <h2> Residentes </h2>

                        <asp:CheckBoxList ID="chklEstudiantes" runat="server">
                        </asp:CheckBoxList>

                        <asp:Button ID="btnMatricular" runat="server" Text="Matricular" CssClass="btn btn-default" type="submit" OnClick="btnMatricular_Click"/>
                    </asp:Panel>
                
            </div>

            <div class="col-sm-4">
                <h2>Equipo </h2>
                <br />
                <label> Seleccionar </label>
                <asp:CheckBoxList ID="chklEstudiantesEquipo" runat="server">
                </asp:CheckBoxList>
                    
                <asp:Button ID="btnDesmatricular" runat="server" Text="Desmatricular" Enabled="false" CssClass="btn btn-default" ToolTip="Desmatricular"
                       OnClick="btn_Desmatricular_Click"  type="button"/>
            </div>


        </div>
    </div>
</asp:Content>
