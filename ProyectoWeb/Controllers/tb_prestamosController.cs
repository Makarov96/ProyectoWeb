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
using ProyectoWeb;

namespace ProyectoWeb.Controllers
{

    [RoutePrefix("api/loanmanagement")]
    public class tb_prestamosController : ApiController
    {
        private proyectoEntities db = new proyectoEntities();

        // GET: apitb_prestamos
        [Route("getAll")]
        public IQueryable<tb_prestamos> Gettb_prestamos()
        {
            return db.tb_prestamos;
        }

        // GET: api/tb_prestamos/5
        [Route("getwithId")]
        [ResponseType(typeof(tb_prestamos))]
        public IHttpActionResult Gettb_prestamos(int id)
        {
            tb_prestamos tb_prestamos = db.tb_prestamos.Find(id);
            if (tb_prestamos == null)
            {
                return NotFound();
            }

            return Ok(tb_prestamos);
        }

        // con este metodo se paga una cantidad 
        //probelmos
        [Route("paywhitput")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttb_pagarprestamo(int id, tb_prestamos tb_prestamo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tb_prestamos tb_prestamos = db.tb_prestamos.Find(id);
            if (tb_prestamos == null)
            {
                return NotFound();
            }
            tb_prestamos.estado = tb_prestamo.estado;

            db.Entry(tb_prestamos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tb_prestamosExists(id))
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


        // POST: api/tb_prestamos
     
        [Route("generate")]
        [ResponseType(typeof(tb_prestamos))]
        public IHttpActionResult Posttb_prestamos(tb_prestamos tb_prestamos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tb_prestamos.Add(tb_prestamos);
            db.SaveChanges();

            
            return Ok(tb_prestamos);
        }

        //este es para pagar el pago  api/loanmanagement
        [Route("paypost")]
        [ResponseType(typeof(tb_pagos))]
        public IHttpActionResult Post_pagarpagos(tb_pagos tb_pagos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tb_pagos.Add(tb_pagos);
            db.SaveChanges();


            return Ok(tb_pagos);
        }

        // DELETE: api/tb_prestamos/5
        [Route("deletepres")]
        [ResponseType(typeof(tb_prestamos))]
        public IHttpActionResult Deletetb_prestamos(int id)
        {
            tb_prestamos tb_prestamos = db.tb_prestamos.Find(id);
            if (tb_prestamos == null)
            {
                return NotFound();
            }

            db.tb_prestamos.Remove(tb_prestamos);
            db.SaveChanges();

            return Ok(tb_prestamos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tb_prestamosExists(int id)
        {
            return db.tb_prestamos.Count(e => e.id_prestamo == id) > 0;
        }
    }
}