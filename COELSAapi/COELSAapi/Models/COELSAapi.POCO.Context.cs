
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
//
//     Este template ha sido modificado para generar ObjectSet de clases 
//     POCO a partir de un EDMX.
//     Ramiro F. Guasti
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace COELSAapi.Models
{
    public partial class PocoCOELSA_APIEntities : ObjectContext
    {
        public const string ConnectionString = "name=COELSA_APIEntities";
        public const string ContainerName = "COELSA_APIEntities";
    
        #region Constructors
    
        public PocoCOELSA_APIEntities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public PocoCOELSA_APIEntities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public PocoCOELSA_APIEntities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<New> News
        {
            get { return _news  ?? (_news = CreateObjectSet<New>("News")); }
        }
        private ObjectSet<New> _news;
    
        public ObjectSet<User> Users
        {
            get { return _users  ?? (_users = CreateObjectSet<User>("Users")); }
        }
        private ObjectSet<User> _users;

        #endregion

    }
}