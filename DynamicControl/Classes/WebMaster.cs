using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using DynamicControl.Models;
using DynamicControl.Controls.Classes;

namespace DynamicControl
{
    [SerializableAttribute]
    [DataContract]
    public class WebMaster
    {
       

        [DataMember]
        public bool isChecked;

       
        [DataMember]
        public List<WebReportParameter> Parameters;

        [DataMember]
        public List<CountryList> Countries;

        [DataMember]
        public List<State> States;

        [DataMember]
        public List<CityList> Cities;

        [DataMember]
        public List<LanguageList> Languages;

        [DataMember]
        public List<CustomerList> Customers;

        [DataMember]
        public List<Customer_Languages> CustomerLanguages;

        [DataMember]
        public List<WebReportCatalogue> ReportCatalogues;

        [DataMember]
        public List<DCControl> Controls;
    }
}