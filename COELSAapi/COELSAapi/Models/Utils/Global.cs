using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace COELSAapi.Models.Utils
{
    public class Global
    {
        public static NumberFormatInfo NumberFormatInfoEnUs = new CultureInfo("en-US", false).NumberFormat;
        public static NumberFormatInfo NumberFormatInfoEsAr = new CultureInfo("es-AR", false).NumberFormat;


        public static void InicializarUser(User usuarioLogueado)
        {
            Global.Usuario = usuarioLogueado;
        }

        #region DATOS del USUARIO Logueado

        public static User Usuario { get; private set; } = null;

        public static int IdUsuario
        {
            get { return Usuario.Id; }
        }

        #endregion
        
    }
}