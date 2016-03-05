using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using DynamicControl.Models;
using DynamicControl.Controls.Enums;
using DynamicControl.Controls.Classes;

namespace DynamicControl.Controllers
{
    public class WebParameterController : ApiController
    {
        private ReportDatabaseEntities db = new ReportDatabaseEntities();

        public WebMaster GetWebReportParameters()
        {
            WebMaster objMaster = new WebMaster
            {
                //Parameters = db.WebReportParameters.ToList().OrderBy(k => k.OrderByControl).ToList(),
                //Countries = (from c in db.Countries
                //             select new CountryList
                //             {
                //                 CountryId = c.CountryId_PK,
                //                 Name = c.Name,
                //                 States = c.States
                //             }
                //             ).ToList(),
                //States = db.States.ToList(),
                //Cities = (from c in db.Cities

                //          select new CityList
                //          {
                //              CityId = c.CityId_PK,
                //              Name = c.Name
                //          }
                //              ).ToList(),
                //Customers = ((from c in db.Customers
                //              select new CustomerList
                //                  {
                //                      CustomerId = c.CustomerId_PK,
                //                      FirstName = c.FirstName,
                //                      FullName = c.FirstName + " " + c.LastName,
                //                      CountryID_FK = c.CountryID_FK
                //                  }).ToList()),
                // CustomerLanguages = db.Customer_Languages.ToList(),
                ReportCatalogues = db.WebReportCatalogues.ToList(),
                Languages = ((from l in db.Languages
                              select new LanguageList
                              {
                                  LanguageId = l.LanguageId_PK,
                                  Name = l.Name
                              }).ToList()),
//                Controls = DDFactory.Instence.Create(db),
               
            };
            return objMaster;
        }

        [Route("api/populateParameters/{id}")]
        [HttpGet]
        public List<DCControl> populateParameters(Guid id)
        {           
            return DDFactory.Instence.Create(id);
        }

        [Route("api/GetStates/{id}")]
        [HttpGet]
        public List<States> GetStates(Guid id)
        {
            List<State> states = db.States.Where(i => i.CountryID_FK == id).ToList();
            return (from s in states
                    select new States
                    {
                        StateId = s.StateId_PK,
                        Name = s.Name
                    }).ToList();
            //return states;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/GetCountries/{id}")]
        [HttpGet]
        public List<CountryList> GetCountries(Guid id)
        {
            // List<State> states = db.States.Where(i => i.CountryID_FK == id).ToList();
            Guid countryId = db.Customers.Where(k => k.CustomerId_PK == id).ToList().SingleOrDefault().Country.CountryId_PK;
            return (from c in db.Countries.Where(cnt => cnt.CountryId_PK == countryId).ToList()
                    select new CountryList
                    {
                        CountryId = c.CountryId_PK,
                        Name = c.Name,
                        States = c.States
                    }
                   ).ToList();

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/GetCities/{id}")]
        [HttpGet]
        public List<CityList> GetCities(Guid id)
        {
            return (from c in db.Cities.Where(ct => ct.StateID_FK == id).ToList()
                    select new CityList
                    {
                        CityId = c.CityId_PK,
                        Name = c.Name
                    }
                    ).ToList();

        }

        [Route("api/GenerateReport/{id}")]
        [HttpPut]
        public HttpResponseMessage GenerateReport(Guid id)
        {
            // return Redirect("ReportView.aspx");
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri("http://www.google.com");
            return response;
            //return states;
        }

    }




}