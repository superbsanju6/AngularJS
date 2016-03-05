using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DynamicControl.Models;

namespace DynamicControl.Controllers
{
    public class WebReportParameterAPIController : ApiController
    {
        private ReportDatabaseEntities db = new ReportDatabaseEntities();

        // GET api/WebReportParameterAPI
        public IQueryable<WebReportParameter> GetWebReportParameters()
        {
            return db.WebReportParameters;
        }

        // GET api/WebReportParameterAPI/5
        [ResponseType(typeof(WebReportParameter))]
        public IHttpActionResult GetWebReportParameter(Guid id)
        {
            WebReportParameter webreportparameter = db.WebReportParameters.Find(id);
            if (webreportparameter == null)
            {
                return NotFound();
            }

            return Ok(webreportparameter);
        }

        // PUT api/WebReportParameterAPI/5
        public IHttpActionResult PutWebReportParameter(Guid id, WebReportParameter webreportparameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != webreportparameter.RPId_PK)
            {
                return BadRequest();
            }

            db.Entry(webreportparameter).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebReportParameterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/WebReportParameterAPI
        [ResponseType(typeof(WebReportParameter))]
        public IHttpActionResult PostWebReportParameter(WebReportParameter webreportparameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WebReportParameters.Add(webreportparameter);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (WebReportParameterExists(webreportparameter.RPId_PK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = webreportparameter.RPId_PK }, webreportparameter);
        }

        // DELETE api/WebReportParameterAPI/5
        [ResponseType(typeof(WebReportParameter))]
        public IHttpActionResult DeleteWebReportParameter(Guid id)
        {
            WebReportParameter webreportparameter = db.WebReportParameters.Find(id);
            if (webreportparameter == null)
            {
                return NotFound();
            }

            db.WebReportParameters.Remove(webreportparameter);
            db.SaveChanges();

            return Ok(webreportparameter);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WebReportParameterExists(Guid id)
        {
            return db.WebReportParameters.Count(e => e.RPId_PK == id) > 0;
        }
    }
}