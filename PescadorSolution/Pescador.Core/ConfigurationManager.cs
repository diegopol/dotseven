using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pescador.Core.Database;

namespace Pescador.Core
{
    /// <summary>
    /// Contiene datos de configuración general de la aplicación
    /// </summary>
    public class ConfigurationManager
    {
        /// <summary>
        /// Obtener o Guardar la URl del servicio
        /// </summary>
        public string ServiceUrl 
        { 
            get
            {
                //Obtener de la BD el valor de ésta propiedad
                using (var db = new PescadorDBEntities())
                {
                    return db.Configurations.First().ServiceUrl;
                }
            } 
            set
            {
                //Persistir en la BD el valor de ésta propiedad
                using (var db = new PescadorDBEntities())
                {
                    db.Configurations.First().ServiceUrl = value;
                    db.SaveChanges();
                }                
            } 
        }

        /// <summary>
        /// Obtener o Guardar el Nombre de Usuario para autenticarse sobre el servicio
        /// </summary>
        public string ServiceUserName
        {
            get
            {
                //Obtener de la BD el valor de ésta propiedad
                using (var db = new PescadorDBEntities())
                {
                    return db.Configurations.First().ServiceUserName;
                }
            }
            set
            {
                //Persistir en la BD el valor de ésta propiedad
                using (var db = new PescadorDBEntities())
                {
                    db.Configurations.First().ServiceUserName = value;
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Obtener o Guardar la contraseña de usuario para autenticarse sobre el servicio
        /// </summary>
        public string ServicePassword
        {
            get
            {
                //Obtener de la BD el valor de ésta propiedad
                using (var db = new PescadorDBEntities())
                {
                    return db.Configurations.First().ServicePassword;
                }
            }
            set
            {
                //Persistir en la BD el valor de ésta propiedad
                using (var db = new PescadorDBEntities())
                {
                    db.Configurations.First().ServicePassword = value;
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Obtener o Guardar el Timeout del servicio
        /// </summary>
        public int ServiceTimeout
        {
            get
            {
                //Obtener de la BD el valor de ésta propiedad
                using (var db = new PescadorDBEntities())
                {
                    return db.Configurations.First().ServiceTimeout;
                }
            }
            set
            {
                //Persistir en la BD el valor de ésta propiedad
                using (var db = new PescadorDBEntities())
                {
                    db.Configurations.First().ServiceTimeout = value;
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Obtener o Guardar la cantidad de segundos que se deben esperar entre intento e intento
        /// </summary>
        public int ServiceRetrySeconds
        {
            get
            {
                //Obtener de la BD el valor de ésta propiedad
                using (var db = new PescadorDBEntities())
                {
                    return db.Configurations.First().ServiceRetrySeconds;
                }
            }
            set
            {
                //Persistir en la BD el valor de ésta propiedad
                using (var db = new PescadorDBEntities())
                {
                    db.Configurations.First().ServiceRetrySeconds = value;
                    db.SaveChanges();
                }
            }
        }
    }
}
