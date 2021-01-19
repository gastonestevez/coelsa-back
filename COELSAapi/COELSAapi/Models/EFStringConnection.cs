using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Linq;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;

namespace COELSAapi.Models
{

	/// <summary>
	/// Establece la cadena de conexión a utilizar por el EF. Datos estáticos y globales de la aplicación.
	/// </summary>
	public static class EFStringConnection {

		//private static RijndaelEnhanced rijndael = new RijndaelEnhanced(Codigos.PassPhrase, Codigos.InitVector);
		private static ConnectionStringSettings defaultConnection;

		/// <summary>
		/// Contiene los settings del App.config (sección "AppSettings")
		/// </summary>
		private static NameValueCollection AppSettings;  // configuraciones del App.config

		/// <summary>
		/// Contiene la cadena de conexión correspondiente al EF tomada del stringConnections.config
		/// </summary>
		private static EntityConnectionStringBuilder EntityConnectionSB = new EntityConnectionStringBuilder();

		/// <summary>
		/// Inicializa la cadena de conexión apuntando a la BD Comunes.
		/// </summary>
		public static void InicializarConexion() {
			EFStringConnection.LoadConfig();
		}

		#region PROPIEDADES PUBLICAS
		
		/// <summary>
		/// Cadena de conexión tomada del stringConnection.config en el formato necesario para EF.
		/// </summary>
		public static string StringConnection {
			get {
				if (EFStringConnection.EntityConnectionSB.ConnectionString == null ||
					EFStringConnection.EntityConnectionSB.ConnectionString == string.Empty) 
				{
					//MyLog4Net.Instance.getCustomLog("EFStringConnection").Warn("StringConnection() --> LoadConfig");
					EFStringConnection.LoadConfig();
				}
				return EFStringConnection.EntityConnectionSB.ConnectionString;
			}
		}

		/// <summary>
		/// Devuelve el Server correspondiente a la Cadena de Conexión actual.
		/// </summary>
		public static string Server {
			get {
				var stringConnection = StringConnection.Split(';').ToList();
				var cadenaConDataSource = stringConnection.Find(s => s.Contains("Server"));
				return cadenaConDataSource == null ? string.Empty : cadenaConDataSource.Split('=').Last().Split('"').First().ToUpper();
			}
		}

		/// <summary>
		/// Devuelve la Base de Datos correspondiente a la Cadena de Conexión actual.
		/// </summary>
		public static string BaseDeDatos {
			get {
				var stringConnection = StringConnection.Split(';').ToList();
				var cadenaInitialCatalog = stringConnection.Find(s => s.Contains("Database"));
				return cadenaInitialCatalog == null ? string.Empty : cadenaInitialCatalog.Split('=').Last();
			}
		}

		/// <summary>
		/// Indica si se esta ejecutando en modo TEST (AtlasBaseTest) o PRODUCCION (AtlasBase).
		/// </summary>
		public static bool ModoTest {
			get { return EFStringConnection.defaultConnection.ConnectionString.Contains("Test"); }
		}

		#endregion

		#region LOAD CONFIG

		/// <summary>
		/// Carga las configuraciones del App.config y stringConnections para armar la cadena de conexión.
		/// </summary>
		private static void LoadConfig() {
			// seccion AppSettings del app.config de la aplicación
			ConfigurationManager.RefreshSection("appSettings");
			AppSettings = ConfigurationManager.AppSettings;

			//string clave = EFStringConnection.GetClave(Codigos.AppConfigConnectionEncryped);
			//var stringConnectionEncryped = clave != null && clave.Length != 0 && bool.Parse(clave);

			//// cadenas de Conexión (archivo stringConnections.config) según EMPRESA
			//ConfigurationManager.RefreshSection("connectionStrings");

			//EFStringConnection.defaultConnection = ConfigurationManager.ConnectionStrings["Comunes"];

			//EFStringConnection.EntityConnectionSB.ConnectionString = stringConnectionEncryped ?
			//	rijndael.Decrypt(EFStringConnection.defaultConnection.ConnectionString) :
			//	EFStringConnection.defaultConnection.ConnectionString;
		}

		/// <summary>
		/// Devuelve un string con la configuración correspondiente a la clave
		/// indicada. Busca la clave dentro de la sección "appSettings" de App.config
		/// </summary>
		public static string GetClave(string key) {
			return AppSettings != null ? AppSettings[key] : string.Empty;
		}

		#endregion


		#region ANALISIS de QUERYs

		/// <summary>
		/// Devuelve el query en formato EF SQL correspondiente a una consulta LinQ.
		/// </summary>
		/// <param name="query">Query a analizar (sin materializar).</param>
		/// <returns>Un string que representa el comando a ejecutar a través del data source.</returns>
		public static string GetTraceSQL(IQueryable query) {
			return ((ObjectQuery) query).ToTraceString();
		}

		#endregion

	}
}