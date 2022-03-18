using CoreApp.SharedAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreAppServices.Models
{

    [Serializable]
    public class ResponseModel<T>
    {
            /// <summary>
            /// Initializes a new instance of the <see cref="ResponseModel{T}"/> class.
            /// </summary>
            public ResponseModel()
            {
                InformationMessages = new List<string>();
                ErrorMessages = new List<string>();
                Items = new List<T>();
            }

            /// <summary>
            /// Gets or sets the response code.
            /// </summary>
            /// <value>
            /// The response code.
            /// </value>
            public Constant.ResponseCode ResponseCode { get; set; }

            /// <summary>
            /// Gets or sets the success message.
            /// </summary>
            /// <value>
            /// The success message.
            /// </value>
            public string SuccessMessage { get; set; }

            /// <summary>
            /// Gets or sets the information messages.
            /// </summary>
            /// <value>
            /// The information messages.
            /// </value>
            public List<string> InformationMessages { get; set; }

            /// <summary>
            /// Gets or sets the error messages.
            /// </summary>
            /// <value>
            /// The error messages.
            /// </value>
            public List<string> ErrorMessages { get; set; }

            /// <summary>
            /// Gets or sets the item.
            /// </summary>
            /// <value>
            /// The item.
            /// </value>
            public T Item { get; set; }

            /// <summary>
            /// Gets or sets the items.
            /// </summary>
            /// <value>
            /// The items.
            /// </value>
            public List<T> Items { get; set; }
        }
    }
