using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace DynamicControl
{
    [SerializableAttribute]
    [DataContract]
    public class LanguageList
    {

        [DataMember]
        public System.Guid LanguageId;

        [DataMember]
        public string Name;

    }
}