using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace DynamicControl
{
    [SerializableAttribute]
    [DataContract]
    public class States
    {
        [DataMember]
        public Guid StateId;

        [DataMember]
        public string Name;
    }
}