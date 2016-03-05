using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime;
using System.Runtime.Serialization;

namespace DynamicControl.Controls.Classes
{
    [Serializable]
    [DataContract]
    public class OptionValue
    {
        [DataMember]
        public int DisplayID;

        [DataMember]
        public Guid DisplayUniqueID;

        [DataMember]
        public string DisplayValue;

        [DataMember]
        public Guid ParentID;

        [DataMember]
        public string ParentName;

    }
}