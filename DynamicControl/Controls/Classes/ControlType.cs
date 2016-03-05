using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime;
using System.Runtime.Serialization;
using DynamicControl.Controls.Enums;

namespace DynamicControl.Controls.Classes
{
    public class ControlType
    {

        [DataMember]
        public ControlTypeView View;

        [DataMember]
        public List<OptionValue> Options;
    }
}