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
    [RoutePrefix("api/cataloguemanagement")]
    public class GenerosController : ApiController
    {
        private proyectoEntities db = new proyectoEntities();

        // GET: api/generos
        [Route("generos")]
        public IQueryable<tb_generos> Getgeneros()
        {
            return db.tb_generos;
        }

        [Route("estadosmaritales")]
        public IQueryable<tb_estados_maritales> Getestadosmaritales()
        {
            return db.tb_estados_maritales;
        }

        [Route("roles")]
        public IQueryable<tb_roles> GetsRoles()
        {
            return db.tb_roles;
        }


      
    }
}