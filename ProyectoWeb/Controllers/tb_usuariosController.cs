using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [RoutePrefix("api/accountmanagement")]
    public class tb_usuariosController : ApiController
    {
        private proyectoEntities db = new proyectoEntities();

        // GET: api/tb_usuarios
        [Route("usuarios")]
        public IQueryable<tb_usuarios> Gettb_usuarios()
        {
            return db.tb_usuarios;
        }

        // GET: api/tb_usuarios/5
        [Route("usuarios/{id}")]
        [ResponseType(typeof(tb_usuarios))]
        public IHttpActionResult Gettb_usuarios(int id)
        {
            tb_usuarios tb_usuarios = db.tb_usuarios.Find(id);
            if (tb_usuarios == null)
            {
                return NotFound();
            }

            return Ok(tb_usuarios);
        }

        // PUT: api/tb_usuarios/5
        [Route("usuarios/{id}")]
        [ResponseType(typeof(void))]
        
        public IHttpActionResult Puttb_usuarios(int id, tb_usuarios tb_usuarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tb_usuarios.id_usuario)
            {
                return BadRequest();
            }

            tb_usuarios tb_user_update = db.tb_usuarios.Find(id);
            if (tb_user_update == null)
            {
                return NotFound();
            }
            tb_user_update.nombre = tb_usuarios.nombre;
            tb_user_update.apellido = tb_usuarios.apellido;
            tb_user_update.dpi = tb_usuarios.dpi;
            tb_user_update.tb_credenciales.correo = tb_usuarios.tb_credenciales.correo;

            db.Entry(tb_user_update).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tb_usuariosExists(id))
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


        // POST: api/tb_usuarios
        [ResponseType(typeof(tb_usuarios))]
        [Route("usuarios", Name = "CrearUsuario")]
        public IHttpActionResult PostCrearUsuario(tb_usuarios tb_usuarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tb_usuarios.Add(tb_usuarios);
            db.SaveChanges();

            return CreatedAtRoute("CrearUsuario", new { id = tb_usuarios.id_usuario }, tb_usuarios);
        }


        [Route("login")]
        [ResponseType(typeof(tb_credenciales))]
        public IHttpActionResult Postlogin(tb_credenciales tb_credenciales)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            tb_credenciales login=db.tb_credenciales.Where(x => x.correo == tb_credenciales.correo && x.contrasena == tb_credenciales.contrasena ).FirstOrDefault();

            if (login == null)
            {
                return NotFound();
            }
           // tb_usuarios informacion = db.tb_usuarios.Where(x => x.id_credencial == login.id_credencial).FirstOrDefault();
           // if (informacion == null)
           // {
           //     return NotFound();
           // }
           //login.id_credencial = informacion.id_usuario;

            return Ok(login);
        }

        // DELETE: api/tb_usuarios/5
        [Route("usuarios/{id}")]
        [ResponseType(typeof(tb_usuarios))]
        public IHttpActionResult Deletetb_usuarios(int id)
        {
            tb_usuarios tb_usuarios = db.tb_usuarios.Find(id);
            if (tb_usuarios == null)
            {
                return NotFound();
            }

            db.tb_usuarios.Remove(tb_usuarios);
            db.tb_credenciales.Remove(db.tb_credenciales.Find(tb_usuarios.id_credencial));
            db.SaveChanges();

            return Ok(tb_usuarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tb_usuariosExists(int id)
        {
            return db.tb_usuarios.Count(e => e.id_usuario == id) > 0;
        }
    }
}