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
    public class StateAPIController : ApiController
    {
        private ReportDatabaseEntities db = new ReportDatabaseEntities();

        // GET api/StateAPI
        public IQueryable<State> GetStates()
        {
            return db.States;
        }

        // GET api/StateAPI/5
        [ResponseType(typeof(State))]
        public IHttpActionResult GetState(Guid id)
        {
            State state = db.States.Find(id);
            if (state == null)
            {
                return NotFound();
            }

            return Ok(state);
        }

        // PUT api/StateAPI/5
        public IHttpActionResult PutState(Guid id, State state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != state.StateId_PK)
            {
                return BadRequest();
            }

            db.Entry(state).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
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

        // POST api/StateAPI
        [ResponseType(typeof(State))]
        public IHttpActionResult PostState(State state)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.States.Add(state);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StateExists(state.StateId_PK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = state.StateId_PK }, state);
        }

        // DELETE api/StateAPI/5
        [ResponseType(typeof(State))]
        public IHttpActionResult DeleteState(Guid id)
        {
            State state = db.States.Find(id);
            if (state == null)
            {
                return NotFound();
            }

            db.States.Remove(state);
            db.SaveChanges();

            return Ok(state);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StateExists(Guid id)
        {
            return db.States.Count(e => e.StateId_PK == id) > 0;
        }
    }
}