using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Interface;
using Utils;

namespace Repositories
{
    public class Usuario
    {
        readonly SqlConnection conn;
        readonly SqlCommand cmd;
        readonly ICacheService cacheService;
        readonly string keyCache;
        public int CacheExpirationTime { get; set; }
        public Usuario(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            cmd.Connection = conn;
            keyCache = "treinosCache";
            CacheExpirationTime = 15;
            cacheService = new MemoryCacheService();
        }

        public async Task<List<Models.Usuario>> GetAllAsync()
        {
            List<Models.Usuario> listaUsuarios = new List<Models.Usuario>();
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Id, Nome, Senha, Tela_Home, Tela_Login, Tela_Usuario, Tela_Cadastro_Cliente, Tela_Relatorio_Contabil, Tela_Tarefa_Contabil, Tela_Tarefa_Legalizacao FROM Usuario ";
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        Models.Usuario usuario = new Models.Usuario();
                        MapperToDr(usuario, dr);
                        listaUsuarios.Add(usuario);
                    }
                }
            }
            return listaUsuarios;
        }

        public async Task<Models.Usuario> GetById(int id)
        {
            Models.Usuario usuario = new Models.Usuario();
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Id, Nome, Senha FROM Usuario WHERE Id = @Id";
                    cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int)).Value = id;
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    if (dr.Read())
                        MapperToDr(usuario, dr);
                }
            }
            return usuario;
        }

        public async Task<List<Models.Usuario>> GetByNameAsync(string nome)
        {
            List<Models.Usuario> listaUsuarios = new List<Models.Usuario>();
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Id, Nome, Senha FROM Usuario WHERE Nome LIKE @Nome";
                    cmd.Parameters.Add(new SqlParameter("@Nome", System.Data.SqlDbType.NVarChar)).Value = $"%{nome}%";
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        Models.Usuario usuario = new Models.Usuario();
                        MapperToDr(usuario, dr);
                        listaUsuarios.Add(usuario);
                    }
                }
            }
            return listaUsuarios;
        }

        public async Task<Models.Usuario> GetByNameAndPasswordAsync(string nome, string senha)
        {
            Models.Usuario usuario = new Models.Usuario();
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Id, Nome FROM Usuario WHERE Nome = @Nome AND Senha = @Senha";
                    cmd.Parameters.Add(new SqlParameter("@Nome", System.Data.SqlDbType.NVarChar)).Value = nome;
                    cmd.Parameters.Add(new SqlParameter("@Senha", System.Data.SqlDbType.NVarChar)).Value = senha;
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    if (dr.Read())
                    {
                        usuario.Id = (int)dr["Id"];
                        usuario.Nome = dr["Nome"].ToString();
                    }
                }
            }
            return usuario;
        }

        public async Task<bool> GetMasterUserAsync(string nome, string senha)
        {
            bool user = false;
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT S.Id, S.Nome, S.Senha FROM Usuario S INNER JOIN Usuario_Mestre UM ON S.Nome = UM.Nome WHERE S.Nome = @Nome AND S.Senha = @Senha";
                    cmd.Parameters.Add(new SqlParameter("@Nome", System.Data.SqlDbType.NVarChar)).Value = nome;
                    cmd.Parameters.Add(new SqlParameter("@Senha", System.Data.SqlDbType.NVarChar)).Value = senha;
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    if (dr.Read())
                        user = true;
                }
            }
            return user;
        }

        public async Task<bool> AddAsync(Models.Usuario usuario)
        {
            try
            {
                using (conn)
                {
                    await conn.OpenAsync();

                    using (cmd)
                    {
                        cmd.CommandText = "INSERT INTO Usuario VALUES (@Nome, @Senha); SELECT scope_identity() FROM Usuario";
                        MapperToParameter(usuario);
                        usuario.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddUsuarioMestreAsync(string nome)
        {
            try
            {
                using (conn)
                {
                    await conn.OpenAsync();

                    using (cmd)
                    {
                        cmd.CommandText = "INSERT INTO Usuario_Mestre VALUES (@Nome); SELECT scope_identity() FROM Usuario_Mestre";
                        cmd.Parameters.Add(new SqlParameter("@Nome", SqlDbType.NVarChar)).Value = nome;
                        int id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdatePasswordFromUserAsync(string nome, string senha)
        {
            int linhasAfetadas;
            try
            {
                using (conn)
                {
                    await conn.OpenAsync();

                    using (cmd)
                    {
                        cmd.CommandText = "UPDATE Usuario SET Senha = @Senha WHERE Nome = @Nome";
                        cmd.Parameters.Add(new SqlParameter("@Nome", SqlDbType.NVarChar)).Value = nome;
                        cmd.Parameters.Add(new SqlParameter("@Senha", SqlDbType.NVarChar)).Value = senha;
                        linhasAfetadas = Convert.ToInt32(await cmd.ExecuteNonQueryAsync());
                    }
                }
                if (linhasAfetadas == 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            int linhasAfetadas;
            using (conn)
            {
                await conn.OpenAsync();

                using (cmd)
                {
                    cmd.CommandText = "DELETE Usuario WHERE Id = @Id";
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = id;
                    linhasAfetadas = await cmd.ExecuteNonQueryAsync();
                }
            }
            if (linhasAfetadas == 0)
                return false;
            return true;
        }




        private void MapperToDr(Models.Usuario usuario, SqlDataReader dr)
        {
            usuario.Id = (int)dr["Id"];
            usuario.Nome = dr["Nome"].ToString();
            usuario.Senha = dr["Senha"].ToString();


        }

        private void MapperToParameter(Models.Usuario usuario)
        {
           cmd.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar)).Value = usuario.Nome == null ? (object)DBNull.Value : usuario.Nome;
           cmd.Parameters.Add(new SqlParameter("@Senha", SqlDbType.VarChar)).Value = usuario.Senha == null ? (object)DBNull.Value : usuario.Senha;
        }
    }
}
