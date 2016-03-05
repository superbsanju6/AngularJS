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
    public class CountryAPIController : ApiController
    {
        private ReportDatabaseEntities db = new ReportDatabaseEntities();

        // GET api/CountryAPI
        public IQueryable<Country> GetCountries()
        {
            return db.Countries;
        }

        // GET api/CountryAPI/5
        [ResponseType(typeof(Country))]
        public IHttpActionResult GetCountry(Guid id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // PUT api/CountryAPI/5
        public IHttpActionResult PutCountry(Guid id, Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != country.CountryId_PK)
            {
                return BadRequest();
            }

            db.Entry(country).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        // POST api/CountryAPI
        [ResponseType(typeof(Country))]
        public IHttpActionResult PostCountry(Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Countries.Add(country);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CountryExists(country.CountryId_PK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = country.CountryId_PK }, country);
        }

        // DELETE api/CountryAPI/5
        [ResponseType(typeof(Country))]
        public IHttpActionResult DeleteCountry(Guid id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            db.Countries.Remove(country);
            db.SaveChanges();

            return Ok(country);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountryExists(Guid id)
        {
            return db.Countries.Count(e => e.CountryId_PK == id) > 0;
        }
    }
}