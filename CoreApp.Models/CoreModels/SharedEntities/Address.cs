using CoreApp.Models.CoreModels.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Models.CoreModels.SharedEntities
{
    public class Address :BaseEntity
    {

        /// <summary>
        /// Gets or sets the address identifier.
        /// </summary>
        /// <value>The address identifier.</value>
        public long AddressId { get; set; }
        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>The address1.</value>
        public string DoorNo { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }
        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>The state identifier.</value>
        public string State { get; set; }

        public string Country { get; set; }
     
        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>The zip code.</value>
        public string ZipCode { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        public bool IsDeleted { get; set; }
        ///// <summary>
        ///// Gets or sets the geometry lat.
        ///// </summary>
        ///// <value>The geometry lat.</value>
        //public string GeometryLat { get; set; }
        ///// <summary>
        ///// Gets or sets the geometry LNG.
        ///// </summary>
        ///// <value>The geometry LNG.</value>
        //public string GeometryLng { get; set; }
    }
}
