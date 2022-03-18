using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreApp.SharedAssets.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Common;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.Practices.ServiceLocation;
using CoreApp.Models.CoreModels.BaseEntities;
using Microsoft.Ajax.Utilities;


//using CoreApp.SharedAssets.;

namespace CoreApp.Infrastructure.DBContext
{
    public class CoreAppDbContext : DbContext, IDbContext
    {
        public CoreAppDbContext()
        {
            var ensureDLLIsCopied =
                System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            var reader = ServiceLocator.Current.GetInstance<IConfigurationReader>();
            var userSession = new SharedAssets.UserSessionManagement.ThreadUserSessionManagement().CurrentUserSession;
            base.Database.Connection.ConnectionString = reader.Read("ConnectionString",(1).ToString());
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public bool AutoDetectChangesEnabled
        {
            get
            {
                return Configuration.AutoDetectChangesEnabled;
            }

            set
            {
                Configuration.AutoDetectChangesEnabled = value;
            }
        }

        public bool ProxyCreationEnabled
        {
            get
            {
                return Configuration.ProxyCreationEnabled;
            }

            set
            {
                Configuration.ProxyCreationEnabled = value;
            }
        }

        /// <summary>
        /// Detach an entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        public void Detach(object entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
        }

        /// <summary>
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql">The command string</param>
        /// <param name="doNotEnsureTransaction">false - the transaction creation is not ensured; true - the transaction creation is ensured.</param>
        /// <param name="timeout">Timeout value, in seconds. A null value indicates that the default value of the underlying provider will be used</param>
        /// <param name="parameters">The parameters to apply to the command string.</param>
        /// <returns>
        /// The result returned by the database after executing the command.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = default(int?), params object[] parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Execute stores procedure and load a list of entities at the end
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="commandText">Command text</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>
        /// Entities
        /// </returns>
        /// <exception cref="System.Exception">Not support parameter type</exception>
        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : class, new()
        {
            try
            {
                if (parameters != null && parameters.Length > 0)
                {
                    for (int i = 0; i <= parameters.Length - 1; i++)
                    {
                        var p = parameters[i] as DbParameter;
                        if (p == null)
                            throw new Exception("Not support parameter type");

                        commandText += i == 0 ? " " : ", ";

                        commandText += "@" + p.ParameterName;
                        if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                        {
                            //output parameter
                            commandText += " output";
                        }
                    }
                }

                var result = this.Database.SqlQuery<TEntity>(commandText, parameters).ToList();
                return result;
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  The type can be any type that has properties that match the names of the columns returned from the query, or can be a simple primitive type. The type does not have to be an entity type. The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>
        /// Result
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<TElement> SqlQuery<TElement>(string commandText, params object[] parameters)
        {
            try
            {
                if (parameters != null && parameters.Length > 0)
                {
                    for (int i = 0; i <= parameters.Length - 1; i++)
                    {
                        var p = parameters[i] as DbParameter;
                        if (p == null)
                            throw new Exception("Not support parameter type");

                        commandText += i == 0 ? " " : ", ";

                        commandText += "@" + p.ParameterName;
                        if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                        {
                            //output parameter
                            commandText += " output";
                        }
                    }
                }

                var result = this.Database.SqlQuery<TElement>(commandText, parameters).ToList();
                return result;
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        IDbSet<TEntity> IDbContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Get full error
        /// </summary>
        /// <param name="exc">Exception</param>
        /// <returns>Error</returns>
        protected string GetFullErrorText(DbEntityValidationException exc)
        {
            var msg = string.Empty;
            foreach (var validationErrors in exc.EntityValidationErrors)
                foreach (var error in validationErrors.ValidationErrors)
                    msg += string.Format("Property: {0} Error: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
            return msg;
        }

        /// <summary>
        /// Persists the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="isHardDelete">if set to <c>true</c> [is hard delete].</param>
        /// <exception cref="System.Exception"></exception>
        public void Persist<TEntity>(TEntity entity, bool isHardDelete = false) where TEntity : BaseEntity
        {
            try
            {
                var item = entity as BaseEntity;
                switch (item.ObjectState)
                {
                    case ObjectState.New:
                        Entry<TEntity>(entity).State = EntityState.Added;
                        Set<TEntity>().Add(entity);
                        break;
                    case ObjectState.Modified:
                        Set<TEntity>().Attach(entity);
                        Entry<TEntity>(entity).State = EntityState.Modified;
                        break;
                    case ObjectState.Deleted:
                        if (isHardDelete)
                        {
                            Set<TEntity>().Attach(entity);
                            Entry<TEntity>(entity).State = EntityState.Deleted;
                        }
                        else
                        {
                            Set<TEntity>().Attach(entity);
                            Entry<TEntity>(entity).State = EntityState.Modified;
                        }
                        break;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of state entries written to the underlying database. This can include
        /// state entries for entities and/or relationships. Relationship state entries are created for
        /// many-to-many relationships and relationships where there is no foreign key property
        /// included in the entity class (often referred to as independent associations).
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public override int SaveChanges()
        {
            try
            {
                var threadUserSessionManagement = new SharedAssets.UserSessionManagement.ThreadUserSessionManagement();
                ChangeTracker.Entries().ForEach(entity =>
                {
                    var baseDto = entity.Entity as BaseEntity;
                    if (baseDto == null) return;
                    switch (baseDto.ObjectState)
                    {
                        case ObjectState.Current:
                            baseDto.CreateDateTime = baseDto.UpdateDateTime = DateTime.Now;
                            //Due to foreacherror above removed
                            //baseDto.CreateDateTime = baseDto.UpdateDateTime = DateTime.Now.UtcNowByTimeZone();
                            baseDto.CreateUserId = baseDto.UpdateUserId = threadUserSessionManagement.CurrentUserSession == null ? 1 : threadUserSessionManagement.CurrentUserSession.UserId;
                            break;
                        case ObjectState.New:
                            baseDto.CreateDateTime = baseDto.UpdateDateTime = DateTime.Now;
                            //Due to foreacherror above removed
                            //baseDto.CreateDateTime = baseDto.UpdateDateTime = DateTime.Now.UtcNowByTimeZone();
                            baseDto.CreateUserId = baseDto.UpdateUserId = threadUserSessionManagement.CurrentUserSession == null ? 1 : threadUserSessionManagement.CurrentUserSession.UserId;
                            break;
                        case ObjectState.Deleted:
                        case ObjectState.Modified:
                            baseDto.UpdateUserId = threadUserSessionManagement.CurrentUserSession == null ? 1 : threadUserSessionManagement.CurrentUserSession.UserId;
                            baseDto.UpdateDateTime = DateTime.Now;
                            break;
                    }
                });
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                throw new Exception(GetFullErrorText(dbEx), dbEx);
            }
        }

        /// <summary>
        /// Tables this instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        public IDbSet<TEntity> Table<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(typeof(CoreAppDbContext).Assembly);
        }

    }
}
