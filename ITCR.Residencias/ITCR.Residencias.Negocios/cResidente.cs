#region Acerca de...
/////////////////////////////////////////////////////////////////////////////
// Instituto Tecnológico de Costa Rica
// Proyecto: Framework Residencias
// Descripción: Clase para el manejo de datos del tipo Residente
// Fecha: Lunes, 23 de Junio de 2014, 02:30:00 a.m.
////////////////////////////////////////////////////////////////////////////
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITCR.Residencias.Negocios
{
    public class cResidente : cPersona
    {
        protected string sCarne;
        protected string sEdificio;
        protected int iCuarto;
        protected int iAla;
        protected int iCama;
        protected int iConcesiones;

        


        
        public cResidente(string pCarne, string pEdificio, int pCuarto, int pAla, int pCama, int pConcesiones)
        {
            setCarne(pCarne);
            setEdificio(pEdificio);
            setCuarto(pCuarto);
            setAla(pAla);
            setCama(pCama);
            setConcesiones(pConcesiones);
        }
        

        private void setCarne(string pCarne)
        {
            this.sCarne = pCarne;
        }
        private void setEdificio(string pEdificio)
        {
            this.sEdificio = pEdificio;
        }
        private void setCuarto(int pCuarto)
        {
            this.iCuarto = pCuarto;
        }
        private void setAla(int pAla)
        {
            this.iAla = pAla;
        }
        private void setCama(int pCama)
        {
            this.iCama = pCama;
        }
        private void setConcesiones(int pConcesiones)
        {
            this.iConcesiones = pConcesiones;
        }
    }
}
