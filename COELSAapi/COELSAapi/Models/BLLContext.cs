using COELSAapi.Models;
using System;


namespace COELSAapi.Models
{
	/// <summary>
	/// Clase Abstracta que provee acceso al EF Object Context. Permite utilizar un context para POCOs.
	/// </summary>
	/// <author>Ramiro</author>
	public abstract class BLLContext : IDisposable
	{
		/// <summary>
		/// Contexto principal de Comunes.
		/// </summary>
		protected PocoCOELSA_APIEntities Context;

		/// <summary>
		/// Constructor principal. Instancia el Contexto según la cadena de conexión: EFStringConnection.StringConnection.
		/// </summary>
		public BLLContext() {
			this.Context = new PocoCOELSA_APIEntities(EFStringConnection.StringConnection);
		}

		#region Miembros de IDisposable

		/// <summary>
		/// Cierra el contexto.
		/// </summary>
		public void Dispose() {
			this.Context.Dispose();
		}

		#endregion

	}
}