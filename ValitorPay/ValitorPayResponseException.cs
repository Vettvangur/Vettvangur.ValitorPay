using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay
{
    public class ValitorPayResponseException : Exception
    {
        public string ValitorResponseRaw { get; set; }

        /// <summary>
        /// Don't depend on this, likely null
        /// </summary>
        public ValitorResponseBase ValitorResponse { get; set; }

        public ValitorPayResponseException()
            : this("ValitorPay response exception")
        {
        }

        public ValitorPayResponseException(string message) : base(message)
        {
        }

        public ValitorPayResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValitorPayResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
