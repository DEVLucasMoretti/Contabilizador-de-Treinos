using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace  Treinos_API_Backend.Controllers
{
    public class TreinoController : ApiController
    {
        Utils.Logger logger;
        Repositories.Treino repository;
        public TreinoController()
        {
            logger = new Utils.Logger(Configurations.Config.GetLogPath());
            repository = new Repositories.Treino(Configurations.Config.GetConnectionString());
            repository.CacheExpirationTime = Configurations.Config.GetCacheExpiration("cacheExpirationTimeInSeconds");
        }
        // GET: api/Treino
        [Route("api/Treino")]
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

        // GET: api/Treino?dataInicio=2025-01-01&dataFim=2025-01-01
        [Route("api/Treino")]
        public async Task<IHttpActionResult> Get(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                List<Models.Treino> Treinos = await repository.GetByPeriodOfTime(dataInicio, dataFim);
                if(Treinos.Count == 0)
                    return NotFound();
                return Ok(Treinos);
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // GET: api/Treino?data=2025-01-01
        [Route("api/Treino")]
        public async Task<IHttpActionResult> GetUpdateOrPost(DateTime data)
        {
            try
            {
                Models.Treino treino = await repository.GetByDateToVerifyUpdateOrAdd(data);
                if(treino.Id != 0)
                    return Ok(treino);
                return NotFound();

            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }


        // GET: api/Treino/TotalDeDiasTreinados
        [Route("api/Treino/TotalDeDiasTreinados")]
        public async Task<IHttpActionResult> GetTotalDiasTreinados()
        {
            try
            {
                return Ok(await repository.GetByAllTimeTrainingDays());
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // GET: api/Treino/TotalDeDiasTreinados
        [Route("api/Treino/ProgressoSemana")]
        public async Task<IHttpActionResult> GetSemana()
        {
            try
            {
                return Ok(await repository.GetProgressoSemana());
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // POST: api/Treino
        [Route("api/Treino")]
        public async Task<IHttpActionResult> Post([FromBody]Models.Treino Treino)
        {
            if (Treino == null)
                return BadRequest("Os dados do Treino não foram preenchidos ");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await repository.Add(Treino);
                if(Treino.Id == 0)
                    return BadRequest();
                return Ok(Treino);
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // PUT: api/Treino
        [Route("api/Treino")]
        public async Task<IHttpActionResult> Put([FromBody]Models.Treino treino)
        {
            if (treino == null)
                return BadRequest("Os dados do Treino não foram preenchidos ");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (!await repository.Update(treino))
                    return BadRequest();
                return Ok(treino);
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }

        // DELETE: api/Treino/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
               if(!await repository.Delete(id))
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
