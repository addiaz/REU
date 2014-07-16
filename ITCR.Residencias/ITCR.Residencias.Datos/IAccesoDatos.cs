#region Acerca de...
/////////////////////////////////////////////////////////////////////////////
// Instituto Tecnológico de Costa Rica
// Proyecto: Framework Matricula
// Descripción: Interface de acceso a datos de la cual heredan todas las clases.
// Fecha: Viernes, 20 de Junio de 2014, 02:30:00 p.m.
////////////////////////////////////////////////////////////////////////////
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ITCR.Residencias.Datos
{
    public interface IAccesoDatos
    {
        bool Insertar();
        bool Actualizar();
        bool Eliminar();
        DataTable SeleccionarUno();
        DataTable SeleccionarTodos();
        DataTable Buscar();

    }
}
