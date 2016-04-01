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
using TryCatch.Data;
using TryCatch.Models;

namespace TryCatch.Controllers
{
//    [Authorize]
    public class OrderController : ApiController
    {
        IRepository _repository;

        public OrderController(IRepository repository)
        {
            _repository = repository;
        }

        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Order
        public IQueryable<Order> GetOrders()
        {
            //return db.Orders;
            return _repository.Orders.AsQueryable();
        }

        // GET: api/Order/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            var order = _repository.Orders.Find(o => o.Id == id);
            //Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
            //return Ok(new Order());
        }

        // PUT: api/Order/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            */
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Order
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            db.SaveChanges();
            */
            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        // DELETE: api/Order/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            /*Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok(order);*/
            return Ok(new Order());
        }

        protected override void Dispose(bool disposing)
        {
            /*if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);*/
        }

        private bool OrderExists(int id)
        {
            //return db.Orders.Count(e => e.Id == id) > 0;
            return false;
        }
    }
}