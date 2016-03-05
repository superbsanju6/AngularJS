using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DynamicControl
{
    [SerializableAttribute]
    [DataContract]
    public class CustomerList
    {
        [DataMember]
        public Guid CustomerId;

        [DataMember]
        public string FirstName;

        [DataMember]
        public string FullName;

        [DataMember]
        public Nullable<System.Guid> CountryID_FK;

       


    }
}