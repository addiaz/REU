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

                    <asp:DropDownList ID="drpActividades" runat="server"></asp:DropDownList>

                    <select id="Select1" runat="server" class="btn btn-group">
                        <option></option>

                    </select>

                        <asp:Button ID="btnMostrarEstudiantes" runat="server" Text="Mostrar Residentes" CssClass="btn btn-default" 
                                    type="submit" onclick="btnMostrarEstudiantes_Click"/>

                    </section>

                    <asp:Panel ID="PanelInicio" runat="server" Visible ="false">
                        <br />
                        <label> Capitán:
                            <asp:TextBox ID="txtCapitan" runat="server" Text="Yo"  CssClass="form-control" Enabled="false"></asp:TextBox>
                        </label>

                        <br />
                        <label> Nombre de Equipo:
                            <asp:TextBox ID="txtNombreEquipo" runat="server" Text = ""  CssClass="form-control" Enabled="true"></asp:TextBox>
                        </label>
                    </asp:Panel>

                    <asp:Panel ID="PanelResidentes" runat="server" Visible="false">
                        <br />
                        <h5> Residentes </h5>

                        <asp:CheckBoxList ID="chklEstudiantes" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>

                <asp:Panel ID="PanelPorEdificio" runat="server" Visible="false">

                        <h5> Residentes A </h5>
                        <asp:CheckBoxList ID="chklEdificioA" runat="server">
                        </asp:CheckBoxList>

                        <h5> Residentes B </h5>
                        <asp:CheckBoxList ID="chklEdificioB" runat="server">
                        </asp:CheckBoxList>

                        <h5> Residentes C </h5>
                        <asp:CheckBoxList ID="chklEdificioC" runat="server">
                        </asp:CheckBoxList>

                        <h5> Residentes Casita </h5>
                        <asp:CheckBoxList ID="chklCasita" runat="server">
                        </asp:CheckBoxList>
                        
                        <h5> Residentes D </h5>
                        <asp:CheckBoxList ID="chklEdificioD" runat="server">
                        </asp:CheckBoxList>

                    </asp:Panel>

                <asp:Button ID="btnMatricular" runat="server" Text="Matricular" CssClass="btn btn-default" type="submit" Enabled="false" OnClick="btnMatricular_Click"/>
                
            </div>

            <div class="col-sm-4">
                <br />
                <div class="table table-responsive">
                    <h2>Equipo </h2>
                        <table id="tableEquipo" class="table table-responsive table-bordered" runat="server" style="margin-left:20px">
                            <thead>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>

                <label> Seleccionar </label>

                <asp:CheckBoxList ID="chklEstudiantesEquipo" runat="server">
                </asp:CheckBoxList>
                    
                <asp:Button ID="btnDesmatricular" runat="server" Text="Desmatricular" Enabled="false" CssClass="btn btn-default" ToolTip="Desmatricular"
                       OnClick="btn_Desmatricular_Click"  type="button"/>
            </div>


        </div>
    </div>
</asp:Content>
