using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace DynamicControl
{
    [SerializableAttribute]
    [DataContract]
    public class CityList
    {
        [DataMember]
        public System.Guid CityId;

        [DataMember]
        public string Name;
    }
}