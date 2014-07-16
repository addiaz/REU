//Region Global Variables
var pValorRetorno = new Array();
var Valores; //arreglo de valores de retorno de las ventanas de busqueda

//EndRegion

function imSalir_onclick() {
	window.parent.close();
}
function mostrarError() {
	wVentanaError = window.showModalDialog("frmError.aspx",null,"dialogLeft=250px; dialogTop=137px; dialogWidth=370px; dialogHeight=310px; edge: Raised; center: Yes; help: No; resizable: No; status: No;");
}

function mostrarBusqueda(pURL, pTitulo, pOpciones) {
	var valRetorno = window.showModalDialog(pURL, pTitulo, pOpciones);
	return valRetorno;
}

function mostrarAlert(p_sMensaje)
{
	window.alert(p_sMensaje);
}
function AbrirVentanaReporte()
{
	window.open("reportes/frmMostrarReporte.aspx","Reporte", "dependent,height=400,menubar,resizable,status,width=700,left=5,top=5");	
}