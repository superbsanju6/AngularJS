using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DynamicControl.Controls.Classes;

namespace DynamicControl.Controls.Interfaces
{
    public interface IDropDown
    {
        List<OptionValue> AddOptions();
    }
}