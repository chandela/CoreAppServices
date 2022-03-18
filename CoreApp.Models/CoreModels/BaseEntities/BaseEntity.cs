using System;
using CoreApp.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Models.CoreModels.BaseEntities
{
    [System.Serializable]

    public abstract class BaseEntity
    {
       
            /// <summary>
            /// Initializes a new instance of the <see cref="BaseEntity"/> class.
            /// </summary>
            public BaseEntity()
            {
#pragma warning disable CS0436 // Type conflicts with imported type
            ObjectState = ObjectState.Current;
#pragma warning restore CS0436 // Type conflicts with imported type
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
            /// Gets or sets the state of the object.
            /// </summary>
            /// <value>The state of the object.</value>
            [NotMapped]
            public ObjectState ObjectState { get; set; }

        }

        public enum ObjectState
        {
            Current = 0,
            New = 1,
            Deleted = 2,
            Modified = 4
        }
    }
