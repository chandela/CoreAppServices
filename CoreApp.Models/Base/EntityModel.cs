using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using PropertyChanged;
using CoreApp.SharedAssets;

namespace CoreApp.Models.Base
{
  

   
        public abstract class EntityModel : INotifyPropertyChanged
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="EntityModel"/> class.
            /// </summary>
            public EntityModel()
            {
                ObjectState = ObjectStateModel.New;
                CreateUserId = UpdateUserId = CurrentUserSession == null ? 1 : CurrentUserSession.UserId;
                CreateDateTime = UpdateDateTime = CurrentUserSession == null ? DateTime.Now : DateTime.Now;
                InternalDataId = Guid.NewGuid();
            }

            /// <summary>
            /// Gets or sets the internal data identifier.
            /// </summary>
            /// <value>The internal data identifier.</value>
            public Guid InternalDataId { get; set; }

            /// <summary>
            /// Gets or sets the create date time.
            /// </summary>
            /// <value>The create date time.</value>
            public DateTime CreateDateTime { get; set; }

            /// <summary>
            /// Gets or sets the update date time.
            /// </summary>
            /// <value>The update date time.</value>
            public DateTime UpdateDateTime { get; set; }

            /// <summary>
            /// Gets or sets the create user identifier.
            /// </summary>
            /// <value>The create user identifier.</value>
            public long CreateUserId { get; set; }

            /// <summary>
            /// Gets or sets the update user identifier.
            /// </summary>
            /// <value>The update user identifier.</value>
            public long UpdateUserId { get; set; }

            /// <summary>
            /// Occurs when [property changed].
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            /// Gets the state of the object.
            /// </summary>
            /// <remarks>Identifies the state of the object with respect to the persistence layer (New, Modified, Deleted, Current).</remarks>
            public ObjectStateModel ObjectState
            {
                get; set;
            }

            /// <summary>
            /// Gets a value indicating whether this instance is new and not yet persisted.
            /// </summary>
            /// <remarks></remarks>    
            [DoNotNotify]
            public bool IsNew
            {
                get
                {
                    return ObjectState == ObjectStateModel.New;
                }
            }

            /// <summary>
            /// Gets a value indicating whether this instance is modified with respect to the persistence layer.
            /// </summary>
            /// <remarks></remarks>
            [DoNotNotify]
            public bool IsModified
            {
                get
                {
                    return ObjectState == ObjectStateModel.Modified;
                }
            }

            /// <summary>
            /// Gets a value indicating whether this instance is deleted and should be removed from the persistence layer..
            /// </summary>
            /// <remarks></remarks>]
            [DoNotNotify]
            public bool IsObjectDeleted
            {
                get
                {
                    return ObjectState == ObjectStateModel.Deleted;
                }
            }

            /// <summary>
            /// Gets a value indicating whether this instance is current and has not been modified with respect to the persistence layer.
            /// </summary>
            /// <remarks></remarks>        
            [DoNotNotify]
            public bool IsCurrent
            {
                get
                {
                    return ObjectState == ObjectStateModel.Current;
                }
            }

            /// <summary>
            /// Flags the object instance as New.
            /// </summary>
            /// <remarks>This method will also fire the event handlers associated with the state.</remarks>
            public virtual void SetNew()
            {
                SetNew(new EventArgs());
            }

            /// <summary>
            /// Flags the object instance as New.
            /// </summary>
            /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
            /// <remarks>This method will also fire the event handlers associated with the state.</remarks>
            public virtual void SetNew(EventArgs e)
            {
                ObjectState = ObjectStateModel.New;
            }

            /// <summary>
            /// Flags the object instance as Modified.
            /// </summary>
            /// <remarks>This method will also fire the event handlers associated with the state.</remarks>
            public virtual void SetModified()
            {
                SetModified(new EventArgs());
            }

            /// <summary>
            /// Flags the object instance as Modified.
            /// </summary>
            /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
            /// <remarks>This method will also fire the event handlers associated with the state.</remarks>
            public virtual void SetModified(EventArgs e)
            {
                if (ObjectState == ObjectStateModel.Current)
                {
                    ObjectState = ObjectStateModel.Modified;
                }
            }


            /// <summary>
            /// Flags the object instance as Deleted.
            /// </summary>
            /// <remarks>This method will also fire the event handlers associated with the state.</remarks>
            public virtual void SetDeleted()
            {
                SetDeleted(new EventArgs());
            }

            /// <summary>
            /// Flags the object instance as Deleted.
            /// </summary>
            /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
            /// <remarks>This method will also fire the event handlers associated with the state.</remarks>
            public virtual void SetDeleted(EventArgs e)
            {
                ObjectState = ObjectStateModel.Deleted;
            }

            /// <summary>
            /// Flags the object instance as Current.
            /// </summary>
            /// <remarks>This method will also fire the event handlers associated with the state.</remarks>
            public virtual void SetCurrent()
            {
                ObjectState = ObjectStateModel.Current;
            }

            /// <summary>
            /// Called when [property changed].
            /// </summary>
            /// <param name="propertyName">Name of the property.</param>
            /// <param name="before">The before.</param>
            /// <param name="after">The after.</param>
            public void OnPropertyChanged(string propertyName, object before, object after)
            {
                if (!propertyName.Equals("ObjectState") ||
                            (propertyName.Equals("ObjectState") &&
                            (ObjectStateModel)after != ObjectStateModel.Current && after != null))
                    SetModified();

                if (PropertyChanged != null)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            /// <summary>
            /// Gets the current user session.
            /// </summary>
            /// <value>
            /// The current user session.
            /// </value>
            [DoNotNotify]
            protected static UserSession CurrentUserSession
            {
                get
                {
                    return new UserSessionManagement.ThreadUserSessionManagement().CurrentUserSession;
                }
            }
        }
    }
