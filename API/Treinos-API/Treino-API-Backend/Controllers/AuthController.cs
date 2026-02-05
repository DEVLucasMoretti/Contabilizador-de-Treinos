using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using Utils;
using Utils.Interface;
using Repositories.Interface;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AuthLibrary;
using System.Reflection;

namespace Treinos_API_Backend.Controllers
{
    public class AuthController : ApiController
    {
        Utils.Logger logger;
        Repositories.Auth repository;
        private readonly JwtManager jwtManager;

        public AuthController()
        {
            logger = new Utils.Logger(Configurations.Config.GetLogPath());
            repository = new Repositories.Auth(Configurations.Config.GetConnectionString());
            repository.CacheExpirationTime = Configurations.Config.GetCacheExpiration("cacheExpirationTimeInSeconds");
            //jwtManager = new JwtManager(secretKey);
        }

        // POST: api/Auth
        public async Task<IHttpActionResult> Login([FromBody] Models.Usuario usuarioRecebido)
        {
            try
            {
                if (usuarioRecebido == null || string.IsNullOrEmpty(usuarioRecebido.Nome) || string.IsNullOrEmpty(usuarioRecebido.Senha))
                    return BadRequest("Credenciais inválidas.");

                var token = await repository.Login(usuarioRecebido);

                if (token == null)
                    return Unauthorized();
                
                return Ok(token);
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return Unauthorized();
            }
        }
    
    /*
    if (usuario == null)
        return BadRequest("Os dados do Usuário não foram preenchidos ");
    if (!ModelState.IsValid)
        return BadRequest(ModelState);
    try
    {
        await repository.Login(usuario);
        if (usuario.Id == 0)
            return BadRequest();
        return Ok(usuario);
    }
    catch (Exception ex)
    {
        await logger.Log(ex);
        return InternalServerError();
    }
    */



        // GET: api/Auth
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await repository.GetAll());
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // GET: api/Auth?nome=xxxxxx&senha=xxxxxx
        public async Task<IHttpActionResult> Get(Models.Usuario usuarioRecebido)
        {
            return Ok(await repository.GetByUser(usuarioRecebido));
        }


        // PUT: api/Auth
        public async Task<IHttpActionResult> Put([FromBody] Models.Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("Os dados do Usuário não foram preenchidos ");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (!await repository.Update(usuario))
                    return BadRequest();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // DELETE: api/Auth/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                if (!await repository.Delete(id))
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
