using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DynamicControl.Models;

namespace DynamicControl
{
    [SerializableAttribute]
    [DataContract]
    public class CountryList
    {

        [DataMember]
        public System.Guid CountryId;

        [DataMember]
        public string Name;

        [DataMember]
        public ICollection<State> States;

    }
}