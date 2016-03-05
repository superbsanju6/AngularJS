using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime;
using System.Runtime.Serialization;
using DynamicControl.Controls.Enums;
using DynamicControl.Models;

namespace DynamicControl.Controls.Classes
{
    [Serializable]
    [DataContract]
    public class DCControl
    {

        [DataMember]
        public string Name;

        [DataMember]
        public string Title;


        [DataMember]
        public ControlType ControlType;

        [DataMember]
        public OptionValue SelectedValue;

        [DataMember]
        public string CascadeControl;

        [DataMember]
        public string ngModel;

        [DataMember]
        public string SelectedOption;
    
       
    }
    public class CustomerDropDown : DCControl
    {
        public CustomerDropDown()
        {
            ControlType = new ControlType { View = ControlTypeView.DropDown };
        }
        [DataMember]
        public OptionValue SelectedCustomerValue;

    }
    public class CountryDropDown : DCControl
    {
        public CountryDropDown()
        {
            ControlType = new ControlType { View = ControlTypeView.DropDown };
        }
        [DataMember]
        public OptionValue SelectedCustomerValue;
    }
    public class LanguageCheckBox : DCControl
    {
        public LanguageCheckBox()
        {
            ControlType = new ControlType { View = ControlTypeView.CheckBoxList };
        }
    }
    public class DateTimeBox : DCControl
    {
        public DateTimeBox()
        {
            ControlType = new ControlType { View = ControlTypeView.TextBox };
        }
    }
    public class  DDFactory
    {
        static DDFactory _instence;
        public static DDFactory Instence
        {
            get
            {
                if(_instence == null)
                {
                    _instence = new DDFactory();
                }
                return _instence;
            }
        }
        public List<DCControl> Create(Guid reportID)
        {
            ReportDatabaseEntities db = new ReportDatabaseEntities();
            var result = new List<DCControl>();
            var data = (from ctrl in db.WebReportParameters.ToList().OrderBy(k => k.OrderByControl).ToList()
                        join CP in db.WebReportParamCascadeMappings.ToList() on ctrl.RPId_PK equals CP.PRId_FK
                        where CP.ReportId_FK==reportID 
                        && ctrl.isActive== true
                        orderby CP.OrderByControl 
                        select new { Control = ctrl, CParam = CP});
            foreach(var d in data)
            {
                DCControl ddl;
                switch (d.Control.Name )
                {
                    case "Customer":
                    ddl = new CustomerDropDown();
                    ddl.Name = d.Control.Name;
                    ddl.Title = d.Control.Name;                    
                    ddl.ControlType.Options = (from c in db.Customers                                                    
                                               select new OptionValue
                                               {
                                                   DisplayUniqueID = c.CustomerId_PK,
                                                   DisplayValue = c.FirstName + " " + c.LastName,
                                                   ParentID =  (Guid) c.CountryID_FK

                                               }
                               ).ToList<OptionValue>();
                    ddl.SelectedValue  = ddl.ControlType.Options[0];
                    ddl.ngModel = ddl.SelectedValue.ToString();
                    break;
                    case "Country":
                    ddl = new CountryDropDown();
                    ddl.Name = d.Control.Name;
                    ddl.Title = d.Control.Name;                   
                    ddl.ControlType.Options = (from c in db.Countries                                                     
                                               select new OptionValue
                                               {
                                                   DisplayUniqueID = c.CountryId_PK,
                                                   DisplayValue = c.Name
                                                    
                                               }
                            ).ToList<OptionValue>();
                    ddl.SelectedValue = ddl.ControlType.Options[0];
                    ddl.ngModel = ddl.SelectedValue.ToString();
                    break;
                    case "State":
                    ddl = new CountryDropDown();
                    ddl.Name = d.Control.Name;
                    ddl.Title = d.Control.Name;
                    ddl.ControlType.Options = (from c in db.States
                                                     
                                             select new OptionValue
                                               {
                                                   DisplayUniqueID = c.StateId_PK,
                                                   DisplayValue = c.Name,
                                                   ParentID = (Guid) c.CountryID_FK 
                                               }
                            ).ToList<OptionValue>();
                    ddl.SelectedValue = ddl.ControlType.Options[0];
                    ddl.ngModel = ddl.SelectedValue.ToString();
                    break;
                    case "City":
                        ddl = new CountryDropDown();
                        ddl.Name = d.Control.Name;
                        ddl.Title = d.Control.Name;
                        ddl.ControlType.Options = (from c in db.Cities

                                                   select new OptionValue
                                                   {
                                                       DisplayUniqueID = c.CityId_PK,
                                                       DisplayValue = c.Name,
                                                       ParentID = (Guid)c.StateID_FK
                                                   }
                                ).ToList<OptionValue>();
                        ddl.SelectedValue = ddl.ControlType.Options[0];
                        ddl.ngModel = ddl.SelectedValue.ToString();
                        break;
                    case "Languages":
                    ddl = new LanguageCheckBox();
                    ddl.Name = d.Control.Name;
                    ddl.Title = d.Control.Name;
                    ddl.ControlType.Options = (from r in
                                                   ((from c in db.Languages
                                                     select new LanguageList
                                                     {
                                                         LanguageId = c.LanguageId_PK,
                                                         Name = c.Name,

                                                     }).ToList())
                                               select new OptionValue
                                               {
                                                   DisplayUniqueID = r.LanguageId,
                                                   DisplayValue = r.Name
                                               }
                            ).ToList<OptionValue>();
                    break;
                    case "DateOfJoining":
                    ddl = new DateTimeBox();
                    ddl.Name = d.Control.Name;
                    ddl.Title = d.Control.Name;
                    break;
                    case "DateOfBirth":
                    ddl = new DateTimeBox();
                    ddl.Name = d.Control.Name;
                    ddl.Title = d.Control.Name;
                    break;
                    default:
                        ddl = null;
                        break;
                }
                if (ddl != null)
                {
                    result.Add(ddl);
                }
                
            }
            return result;
        }

    }
}