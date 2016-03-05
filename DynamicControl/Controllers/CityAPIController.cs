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
    public class CityAPIController : ApiController
    {
        private ReportDatabaseEntities db = new ReportDatabaseEntities();

        // GET api/CityAPI
        public IQueryable<City> GetCities()
        {
            return db.Cities;
        }

        // GET api/CityAPI/5
        [ResponseType(typeof(City))]
        public IHttpActionResult GetCity(Guid id)
        {
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        // PUT api/CityAPI/5
        public IHttpActionResult PutCity(Guid id, City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != city.CityId_PK)
            {
                return BadRequest();
            }

            db.Entry(city).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST api/CityAPI
        [ResponseType(typeof(City))]
        public IHttpActionResult PostCity(City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cities.Add(city);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CityExists(city.CityId_PK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = city.CityId_PK }, city);
        }

        // DELETE api/CityAPI/5
        [ResponseType(typeof(City))]
        public IHttpActionResult DeleteCity(Guid id)
        {
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return NotFound();
            }

            db.Cities.Remove(city);
            db.SaveChanges();

            return Ok(city);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CityExists(Guid id)
        {
            return db.Cities.Count(e => e.CityId_PK == id) > 0;
        }
    }
}