<%@ Page Title="" Language="C#" MasterPageFile="~/SinAutenticar.Master" AutoEventWireup="true" CodeBehind="frmAutenticacion.aspx.cs" 
    Inherits="ITCR.Residencias.Interfaz.frmAutenticacion" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
    

    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />

    <link href="css/boostrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.css" rel="stylesheet" type="text/css" />

    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/menu.css" rel="stylesheet" type="text/css" />
    <link href="css/itcr.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" href="http://netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap.min.css" type="text/css"/>
    <link rel="stylesheet" href="http://netdna.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.css" type="text/css"/>

    <script src="js/modal.js" type="text/javascript"></script>
    <script src="js/boostrap.min.js" type="text/javascript"></script>
    <script src="js/boostrap-responsive.min.js" type="text/javascript"></script>
    <script src="js/boostrap.js" type="text/javascript"></script>

    <script type="text/javascript" src="js/boostrap.js"></script>
    <script type="text/javascript">
        function entrar() {
            // con jQuery
//            $("#MainContent_lblMensajeError").css("display:none");
//            $("#ManContent_imgProcesando").css("visibility", "visible");

            //      sin jQuery
            document.getElementById("MainContent_lblMensajeError").style.cssText = "display:none";
            document.getElementById("ManContent_imgProcesando").style.cssText = "visibility:visible";
        }

        function modal() {
            $(document).ready(function () {
                $("#usuarioModal").modal("show")
            }
        );}
     </script>


     <style type="text/css">
         div.centro
         {
             text-align:center;
         }

         div.centro_ex {
            text-align:center;
            width: 265px;
            height: 315px;
            padding: 0px;
            border: 0px solid #00386b;
            margin: 0px;
        }
         table.centro
         {
             margin-left:auto;
             
         }
         .style3
         {
             height: 38px;
             width: 301px;
         }
         .style4
         {
             height: 2px;
             width: 301px;
         }
         .style5
         {
             width: 95px;
             height: 39px;
         }
         .style6
         {
             width: 152px;
             height: 39px;
         }
         .auto-style3 {
             color: #FFFFFF;
         }
         #login_area {
             height: 150px;
    }
         #txtDia {
             width: 29px;
         }
         #txtMes {
             width: 33px;
         }
         #txtAno {
             width: 38px;
         }
     </style>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div style="text-align:center" class="centro_ex">
        
        <section>
        <div id="login_area">
            <br />
                    <img class="img-responsive"  alt="TEC" src="imagenes/TECLOGO.png" style="height: 52px; width: 233px;"/>
                    <br /><br /><br />

            <asp:Panel ID="panelSignIn" runat="server">

            
                    <label for="username">
                        <asp:TextBox id="txtUsuario" CssClass ="form-control" runat="server"  onFocus="javascript:this.select()" autofocus=autofocus></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" 
                            CssClass="TextoError" ErrorMessage="Identificación de usuario" ControlToValidate="txtUsuario">*</asp:RequiredFieldValidator>
                       </label> 
            <br />
                    <label for="password">
                        <asp:TextBox id="txtPassword" CssClass ="form-control" placeholder="password" runat="server" TextMode="Password" onFocus="javascript:this.select()"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" 
                            CssClass="TextoError" ErrorMessage="Contraseña del usuario" ControlToValidate="txtPassword">*</asp:RequiredFieldValidator>
                        </label>
                    
                    <div class="form-button">
                        <asp:ImageButton ID="btnEntrar" CssClass="btn btn-default" type="submit" runat="server" CausesValidation="False" 
                            ImageUrl="~/imagenes/login_peq.jpeg" onclick="btnEntrar_Click" onclientclick="entrar()" />

                    </div>
                    <div class="form-button">
                        
                        <asp:Button ID="btnCrearContra" runat="server" Text="Crear Usuario" CssClass="btn btn-link" type="submit"  CausesValidation="False" 
                            OnClick="btnCrearContra_Click"/>
                    </div>

                    <asp:ValidationSummary id="ValidationSummary1" runat="server" 
				        CssClass="CSSClass" HeaderText="Los siguientes campos son requeridos:" 
				        ForeColor="Red"></asp:ValidationSummary>
			        <asp:Label id="lblMensajeError" runat="server" CssClass="TextoError" 
				        ForeColor="Red"></asp:Label>
			        <img id="ManContent_imgProcesando" alt="Procesando" style="width: 22px; height: 22px; visibility:hidden;" 
				        src="../imagenes/Procesando.gif" />
        
                <div id="alertExito" runat="server" class="alert alert-success" role="alert" visible="false"> Registro Correcto </div>

                </asp:Panel>

            <asp:Panel ID="panelCrearUsuario" runat="server" Visible="false">
                
                <label for="carne">
                    <asp:TextBox id="txtCarne" CssClass ="form-control" placeholder="Carné" runat="server"  onFocus="javascript:this.select()" autofocus=autofocus></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" 
                            CssClass="TextoError" ErrorMessage="Carné del usuario" ControlToValidate="txtCarne">*</asp:RequiredFieldValidator>
                </label>
                <br />
                <label for="pin">
                    <asp:TextBox id="txtPin" CssClass ="form-control" placeholder="password" runat="server" TextMode="Password" onFocus="javascript:this.select()"></asp:TextBox>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" 
                            CssClass="TextoError" ErrorMessage="Contraseña del usuario" ControlToValidate="txtPin" ToolTip="Contraseña de 6 o más dígitos">*</asp:RequiredFieldValidator>
                </label>
                
                <div class="form-button">
                    <asp:Button ID="btnCrearUsuario" runat="server" Text="Crear" CssClass="btn btn-default" type="button"  CausesValidation="False" 
                        OnClick="btnCrearUsuario_Click"/>

                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-default" type="button"  CausesValidation="False" 
                        OnClick="btnCancelar_Click"/>
                </div>

                <asp:ValidationSummary id="ValidationSummary2" runat="server" 
				        CssClass="CSSClass" HeaderText="Los siguientes campos son requeridos:" 
				        ForeColor="Red"></asp:ValidationSummary>
			        <asp:Label id="lblMensajeError2" runat="server" CssClass="TextoError" 
				        ForeColor="Red"></asp:Label>

                
            </asp:Panel>

			</div>
			</section>
           

    </div>

    <br />
    <br />
    <br />

    


</asp:Content>