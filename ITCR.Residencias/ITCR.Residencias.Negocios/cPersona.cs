#region Acerca de...
/////////////////////////////////////////////////////////////////////////////
// Instituto Tecnológico de Costa Rica
// Proyecto: Framework Residencias
// Descripción: Clase para el manejo de datos del tipo persona
// Fecha: Lunes, 23 de Junio de 2014, 02:07:00 a.m.
// De esta clase se derivan las clases cResidente y cCoordinador
////////////////////////////////////////////////////////////////////////////
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITCR.Residencias.Negocios
{
    public class cPersona
    {
        /* Atributos de la clase Persona */

        
        protected string sNombre;
        protected string sApellido;
        protected string sCedula;
        protected string sSexo;
        protected string sFechaNacimiento;
        protected string sDireccion;
        protected string sTipoSangre;
        protected string sEmail;
        protected string sTelefono;
        protected string sCelular;

        /* Constructor principal de la clase Persona */
        public cPersona() { }
        public cPersona(
            string pNombre,
            string pApellido,
            string pCedula,
            string pSexo,
            string pFechaNacimiento,
            string pDireccion,
            string pTipoSangre,
            string pEmail,
            string pTelefono,
            string pCelular)
        {
            setNombre(pNombre);
            setApellido(pApellido);
            setCedula(pCedula);
            setSexo(pSexo);
            setFechaNacimento(pFechaNacimiento);
            setDireccion(pDireccion);
            setTipoSangre(pTipoSangre);
            setEmail(pEmail);
            setTelefono(pTelefono);
            setCedula(pCelular);
        }

        private void setNombre(string pNombre)
        {
            this.sNombre = pNombre;
        }
        private void setApellido(string pApellido)
        {
            this.sApellido = pApellido;
        }
        private void setCedula(string pCedula)
        {
            this.sCedula = pCedula;
        }
        private void setSexo(string pSexo)
        {
            this.sSexo = pSexo;
        }
        private void setFechaNacimento(string pFecha)
        {
            this.sFechaNacimiento = pFecha;
        }
        private void setDireccion(string pDireccion)
        {
            this.sDireccion = pDireccion;
        }
        private void setTipoSangre(string pTipoSangre)
        {
            this.sTipoSangre = pTipoSangre;
        }
        private void setEmail(string pEmail)
        {
            this.sEmail = pEmail;
        }
        private void setTelefono(string pTelefono)
        {
            this.sTelefono = pTelefono;
        }
        private void setCelular(string pCelular)
        {
            this.sCedula = pCelular;
        }

    }
}
