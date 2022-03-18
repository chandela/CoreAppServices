using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.SharedAssets
{
    
        public class CoreAppException : ApplicationException
        {
            public void ThrowIfHasMessages()
            {
                if (Messages.Count != 0)
                    throw this;
            }

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CoreAppException" /> class.
        /// </summary>
        public CoreAppException()
                : this(string.Empty, null)
            {
            }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CoreAppException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CoreAppException(string message)
                : this(message, null)
            {
            }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CoreAppException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CoreAppException(string message, Exception innerException)
                : base(message, innerException)
            {
                Messages = new List<string>();

                // add constrcutor message if present
                if (message != null && message.Length != 0)
                    Messages.Add(message);
            }

            /// <summary>
            ///     Initializes a new instance of the <see cref="CoreAppException" /> class.
            /// </summary>
            /// <param name="info">The object that holds the serialized object data.</param>
            /// <param name="context">The contextual information about the source or destination.</param>
            protected CoreAppException(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                // must deserialize since we handle serialization ourselves
                Messages = info.GetValue("mMessages", typeof(List<string>)) as List<string>;
            }

            /// <summary>
            ///     When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with
            ///     information about the exception.
            /// </summary>
            /// <param name="info">
            ///     The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object
            ///     data about the exception being thrown.
            /// </param>
            /// <param name="context">
            ///     The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual
            ///     information about the source or destination.
            /// </param>
            /// <exception cref="T:System.ArgumentNullException">
            ///     The <paramref name="info" /> parameter is a null reference (Nothing in
            ///     Visual Basic).
            /// </exception>
            /// <PermissionSet>
            ///     <IPermission
            ///         class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
            ///         version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
            ///     <IPermission
            ///         class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
            ///         version="1" Flags="SerializationFormatter" />
            /// </PermissionSet>
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                base.GetObjectData(info, context);

                // must serialize since we handle serialization ourselves
                info.AddValue("mMessages", Messages);
            }

            /// <summary>
            ///     Gets the messages.
            /// </summary>
            /// <value>
            ///     The messages.
            /// </value>
            public List<string> Messages { get; private set; }

            /// <summary>
            ///     Gets a message that describes the current exception.
            /// </summary>
            /// <returns>The error message that explains the reason for the exception, or an empty string("").</returns>
            public override string Message
            {
                get
                {
                    // concatenate all messages into one big message
                    var messageBuilder = new StringBuilder();

                    foreach (var message in Messages)
                        messageBuilder.Append(message);

                    return messageBuilder.ToString();
                }
            }

            #endregion
        }
    }
