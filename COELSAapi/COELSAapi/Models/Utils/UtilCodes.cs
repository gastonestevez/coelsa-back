﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COELSAapi.Models
{
    public class UtilCodes
    {
        #region Mail asuntos


        public const int IdAsuntoCheques = 1;
        public const int IdAsuntoDebitos = 2;
        public const int IdAsuntoTransferencias = 3;
        public const int IdAsuntoAccionistas = 4;
        public const int IdAsuntoPagoDirecto = 5;
        public const int IdAsuntoFeriadoLocal = 6;
        public const int IdAsuntoOtro = 0;

        public static string AsuntoCheques = "Cheques";
        public static string AsuntoDebitos = "Débitos";
        public static string AsuntoTransferencias = "Transferencias";
        public static string AsuntoAccionistas = "Accionistas";
        public static string AsuntoPagoDirecto = "Pago directo";
        public static string AsuntoFeriadoLocal = "Feriado local";
        public static string AsuntoOtro = "Sitio COELSA - Contacto";

        public static string DestinatarioEmail = string.Empty;
        public static string ClienteWeb = string.Empty;

        public enum Role
        {
            Admin = 1,
            NewsSetter = 2
        }
        public static string RoleAdmin = "Admin";
        public static string RoleNewsSetter = "Gestor de Novedades";

        public static string GetRoleName(int idRole)
        {
            switch (idRole)
            {
                case (int)Role.Admin:
                    return RoleAdmin;
                case (int)Role.NewsSetter:
                    return RoleNewsSetter;
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Devuelve la descripcion del asunto email
        /// </summary>
        /// <param name="idAsunto">Id Asunto</param>        
        public static string GetAsuntoEmail(int idAsunto)
        {
            switch (idAsunto)
            {
                case IdAsuntoCheques:
                    return AsuntoCheques;
                case IdAsuntoDebitos:
                    return AsuntoDebitos;
                case IdAsuntoTransferencias:
                    return AsuntoTransferencias;
                case IdAsuntoAccionistas:
                    return AsuntoAccionistas;
                case IdAsuntoPagoDirecto:
                    return AsuntoPagoDirecto;
                case IdAsuntoFeriadoLocal:
                    return AsuntoFeriadoLocal;
                case IdAsuntoOtro:
                    return AsuntoOtro;
                default:
                    return string.Empty;
            }
        }

        #endregion
    }
}