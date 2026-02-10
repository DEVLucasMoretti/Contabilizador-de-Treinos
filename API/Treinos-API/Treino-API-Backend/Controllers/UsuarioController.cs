using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Treinos_API_Backend.Controllers
{
    public class UsuarioController : ApiController
    {
        readonly Utils.Logger logger;
        readonly Repositories.Usuario repository;

        public UsuarioController()
        {
            logger = new Utils.Logger(Configurations.Config.GetLogPath());
            repository = new Repositories.Usuario(Configurations.Config.GetConnectionString());
            repository.CacheExpirationTime = Configurations.Config.GetCacheExpiration("cacheExpirationTimeInSeconds");
        }
        // GET: api/Usuario


        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await repository.GetAllAsync());
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // GET: api/Usuario/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                Models.Usuario usuario = await repository.GetById(id);
                if (usuario.Id == 0)
                    return NotFound();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        public async Task<IHttpActionResult> Get(string nome)
        {
            try
            {
                List<Models.Usuario> ListaUsuario = await repository.GetByNameAsync(nome);
                if (ListaUsuario.Count == 0)
                    return NotFound();
                return Ok(ListaUsuario);
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // GET: api/Usuario?nome=''&senha=''
        public async Task<IHttpActionResult> Get(string nome, string senha)
        {
            try
            {
                Models.Usuario usuario = await repository.GetByNameAndPasswordAsync(nome, senha);
                if (usuario.Id == 0)
                    return NotFound();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // GET: api/Usuario?Mestre?nome=''&senha=''
        [Route("api/Usuario/Mestre")]
        public async Task<IHttpActionResult> GetUsuarioMestre(string nome, string senha)
        {
            try
            {
                if (!await repository.GetMasterUserAsync(nome, senha))
                    return NotFound();
                return Ok();
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // POST: api/Usuario
        public async Task<IHttpActionResult> Post([FromBody] Models.Usuario usuario)
        {
            try
            {
                if (!await repository.AddAsync(usuario))
                    return BadRequest();
                return Ok();
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }


        // POST: api/Usuario
        [Route("api/Usuario/Mestre")]
        public async Task<IHttpActionResult> PostUsuarioMestre(string nome)
        {
            try
            {
                if (!await repository.AddUsuarioMestreAsync(nome))
                    return BadRequest();
                return Ok();
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }


        // PUT: api/Usuario?Nome=''&Senha=
        public async Task<IHttpActionResult> PutPassword(string nome, string senha)
        {
            try
            {
                if (!await repository.UpdatePasswordFromUserAsync(nome, senha))
                    return BadRequest();
                return Ok();
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // DELETE: api/Usuario/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                if (!await repository.DeleteByIdAsync(id))
                    return NotFound();
                return Ok();
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }
    }
}
