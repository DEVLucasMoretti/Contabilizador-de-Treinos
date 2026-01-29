using Models;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.Interface;

namespace Repositories
{
    public class Treino : IRepository<Models.Treino>  
    {
        readonly SqlConnection conn;
        readonly SqlCommand cmd;
        readonly ICacheService cacheService;
        readonly string keyCache;
        public int CacheExpirationTime { get; set; }

        public Treino(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            cmd.Connection = conn;
            keyCache = "treinosCache";
            CacheExpirationTime = 15;
            cacheService = new MemoryCacheService();
        }

        public async Task<List<Models.Treino>> GetAll()
        {
            List<Models.Treino> treinos;
            treinos = cacheService.Get<List<Models.Treino>>(keyCache);

            if (treinos != null)
                return treinos;

            treinos = new List<Models.Treino>();
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Id, Data, Dia_Da_Semana, Treino_Do_Dia, Quantidade_Caloria FROM Treino";
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();

                    while (dr.Read())
                    {
                        Models.Treino treino = new Models.Treino();
                        MapperTreinoToDr(treino, dr);
                        treinos.Add(treino);
                    }
                }
            }
            cacheService.Set(keyCache, treinos, CacheExpirationTime);
            return treinos;
        }

        

        public async Task<List<Models.Treino>> GetByPeriodOfTime(DateTime dataInicio, DateTime dataFim)
        {
            List<Models.Treino> treinos = new List<Models.Treino>();
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Id, Data, Dia_Da_Semana, Treino_Do_Dia, Quantidade_Caloria FROM Treino WHERE Data BETWEEN @DataInicio AND @DataFim ";
                    cmd.Parameters.Add(new SqlParameter("@DataInicio", System.Data.SqlDbType.VarChar)).Value = dataInicio;
                    cmd.Parameters.Add(new SqlParameter("@DataFim", System.Data.SqlDbType.VarChar)).Value = dataInicio;
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();

                    while (dr.Read())
                    {
                        Models.Treino treino = new Models.Treino();
                        MapperTreinoToDr(treino, dr);
                        treinos.Add(treino);
                    }
                }
            }
            return treinos;
        }

        public async Task<int> GetByAllTimeTrainingDays()
        {
            int diasTreinadosNoTotal = 0;
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Count(Id) Dias_Treinados_Total FROM Treino";
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();

                    if (dr.Read())
                        diasTreinadosNoTotal = (int)dr["Dias_Treinados_Total"];

                }
            }
            return diasTreinadosNoTotal;
        }

        public async Task<Models.Treino> GetByDateToVerifyUpdateOrAdd(DateTime data)
        {
            Models.Treino treino = new Models.Treino();
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT * FROM Treino WHERE Data = @Data";
                    cmd.Parameters.Add(new SqlParameter("@Data", System.Data.SqlDbType.Date)).Value = data ;
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();

                    if (dr.Read())//UPDATE else POST
                        MapperTreinoToDr(treino, dr);                
                }
            }
            return treino;
        }

        public async Task Add(Models.Treino treino)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "INSERT INTO Treino (Data, Treino_Do_Dia, Dia_Da_Semana, Quantidade_Caloria ) VALUES (@Data,@Treino_Do_Dia, @Dia_Da_Semana, @Quantidade_Caloria ); SELECT scope_identity()";
                    TakeDayOfWeek(treino);
                    MapperTreinoToParameters(treino);
                    treino.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    cacheService.Remove(keyCache);
                }
            }
        }

        public async  Task<bool> Update(Models.Treino treino)
        {
            int linhasAfetadas;
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "UPDATE Treino SET Data = @Data, Treino_Do_Dia  = @Treino_Do_Dia, Dia_Da_Semana = @Dia_Da_Semana , Quantidade_Caloria = @Quantidade_Caloria WHERE Id = @Id";
                    cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int)).Value = treino.Id;
                    MapperTreinoToParameters(treino);
                    linhasAfetadas = await cmd.ExecuteNonQueryAsync();
                }
            }
            if (linhasAfetadas == 0)
                return false;

            cacheService.Remove(keyCache);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            int linhasAfetadas;
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "DELETE Treino WHERE Id = @Id";
                    cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int)).Value = id;
                    linhasAfetadas = await cmd.ExecuteNonQueryAsync();
                }
            }
            if (linhasAfetadas == 0)
                return false;

            cacheService.Remove(keyCache);
            return true;

        }

        public async Task<List<Models.Treino>> GetProgressoSemana()
        {
            List<Models.Treino> treinos;

            treinos = new List<Models.Treino>();
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Id, Data, Dia_Da_Semana, Treino_Do_Dia, Quantidade_Caloria FROM Treino WHERE Data BETWEEN @DataIncio AND @DataFim ";

                    DateTime dataInicio = DateTime.Now;
                    DateTime dataAtualDoComputador = DateTime.Now;

                    dataInicio = TakeInitialDateToVerifyWeeklyProgress(dataAtualDoComputador, dataInicio);

                    cmd.Parameters.Add(new SqlParameter("@DataIncio", System.Data.SqlDbType.Date)).Value = dataInicio; 
                    cmd.Parameters.Add(new SqlParameter("@DataFim", System.Data.SqlDbType.Date)).Value = dataAtualDoComputador; 

                    SqlDataReader dr = await cmd.ExecuteReaderAsync();

                    while (dr.Read())
                    {
                        Models.Treino treino = new Models.Treino();
                        MapperTreinoToDr(treino, dr);
                        treinos.Add(treino);
                    }
                }
            }
            return treinos;
        }


        public void MapperTreinoToDr(Models.Treino treino, SqlDataReader dr)
        {
            treino.Id = (int) dr["Id"];
            treino.TreinoDoDia = dr["Treino_Do_Dia"].ToString();
            treino.DiaDaSemana = dr["Dia_Da_Semana"].ToString();
            treino.QuantidadeCaloria = Convert.ToInt32(dr["Quantidade_Caloria"]);
            treino.Data = Convert.ToDateTime(dr["Data"]);
          }

        public void MapperTreinoToParameters(Models.Treino treino)
        {
            cmd.Parameters.Add(new SqlParameter("@Treino_Do_Dia", System.Data.SqlDbType.VarChar)).Value = treino.TreinoDoDia;
            cmd.Parameters.Add(new SqlParameter("@Dia_Da_Semana", System.Data.SqlDbType.VarChar)).Value = treino.DiaDaSemana;
            cmd.Parameters.Add(new SqlParameter("@Quantidade_Caloria", System.Data.SqlDbType.Decimal)).Value = treino.QuantidadeCaloria;
            cmd.Parameters.Add(new SqlParameter("@Data", System.Data.SqlDbType.Date)).Value = treino.Data;
        }

        public void TakeDayOfWeek(Models.Treino treino)
        {
            switch (treino.Data.DayOfWeek)
            {
                case DayOfWeek.Monday: treino.DiaDaSemana = "Segunda-Feira"; break;
                case DayOfWeek.Tuesday: treino.DiaDaSemana = "Terça-Feira"; break;
                case DayOfWeek.Wednesday: treino.DiaDaSemana = "Quarta-Feira"; break;
                case DayOfWeek.Thursday: treino.DiaDaSemana = "Quinta-Feira"; break;
                case DayOfWeek.Friday: treino.DiaDaSemana = "Sexta-Feira"; break;
                case DayOfWeek.Saturday: treino.DiaDaSemana = "Sábado"; break;
                case DayOfWeek.Sunday: treino.DiaDaSemana = "Domingo"; break;
            }
        }

        public DateTime TakeInitialDateToVerifyWeeklyProgress(DateTime dataAtualDoComputador, DateTime dataInicio)
        {
            switch (dataAtualDoComputador.DayOfWeek)
            {
                case DayOfWeek.Monday: break;
                case DayOfWeek.Tuesday: return dataInicio.AddDays(-1);
                case DayOfWeek.Wednesday: return dataInicio.AddDays(-2); 
                case DayOfWeek.Thursday: return dataInicio.AddDays(-3);
                case DayOfWeek.Friday: return dataInicio.AddDays(-4); 
                case DayOfWeek.Saturday: return dataInicio.AddDays(-5); 
                case DayOfWeek.Sunday: return dataInicio.AddDays(-6);      
            }
            return dataInicio;
        }


    }
}
