using CoreApp.Models.CoreModels.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Models.CoreModels.SharedEntities
{

    [System.Serializable]
    public class Employees : BaseEntity
    {


        /// <summary>
        /// Gets or sets the Employees identifier.
        /// </summary>
        /// <value>
        /// The associate identifier.
        /// </value>
        public long EmployeeId { get; set; }

            /// <summary>
            /// Gets or sets the club identifier.
            /// </summary>
            /// <value>
            /// The club identifier.
            /// </value>
            public long BranchId { get; set; }

            /// <summary>
            /// Gets or sets the user identifier.
            /// </summary>
            /// <value>
            /// The user identifier.
            /// </value>

            public long UserId { get; set; }

            /// <summary>
            /// Gets or sets the start date.
            /// </summary>
            /// <value>
            /// The start date.
            /// </value>
            public DateTime StartDate { get; set; }

            /// <summary>
            /// Gets or sets the photo identifier.
            /// </summary>
            /// <value>
            /// The photo identifier.
            /// </value>
            public long? PhotoId { get; set; }

            /// <summary>
            /// Gets or sets the is deleted.
            /// </summary>
            /// <value>
            /// The is deleted.
            /// </value>
            public bool IsDeleted { get; set; }

            /// <summary>
            /// Gets or sets the is active.
            /// </summary>
            /// <value>
            /// The is active.
            /// </value>
            public bool IsActive { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this instance is trainer.
            /// </summary>
            /// <value>
            /// <c>true</c> if this instance is trainer; otherwise, <c>false</c>.
            /// </value>
            public bool IsTrainer { get; set; }

            /// <summary>
            /// Gets or sets the user.
            /// </summary>
            /// <value>
            /// The user.
            /// </value>
            public virtual User User { get; set; }



        public virtual Branch Branch { get; set; }
    }
    }
