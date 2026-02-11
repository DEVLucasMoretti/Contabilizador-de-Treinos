using AuthLibrary;
using Models;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utils;
using Utils.Interface;

namespace Repositories
{
    public class Auth
    {
        readonly string connectionStringAux;
        readonly SqlConnection conn;
        readonly SqlCommand cmd;
        readonly ICacheService cacheService;
        readonly string keyCache;
        public int CacheExpirationTime { get; set; }

        private readonly JwtManager jwtManager;
        string secretKey = "testeJWTcompelosmenos32caracterqueficarcomplexoparaoalgoritmoHS256";



        public Auth(string connectionString)
        {
            connectionStringAux = connectionString;
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            cmd.Connection = conn;
            keyCache = "usuariosCache";
            CacheExpirationTime = 15;
            cacheService = new MemoryCacheService();
            jwtManager = new JwtManager(this.secretKey);
        }

        public async Task<List<Models.Usuario>> GetAll()
        {
            List<Models.Usuario> usuarios;
            usuarios = cacheService.Get<List<Models.Usuario>>(keyCache);

            if (usuarios != null)
                return usuarios;

            usuarios = new List<Models.Usuario>();
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Id, Nome, Senha FROM Usuario";
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();

                    while (dr.Read())
                    {
                        Models.Usuario usuario = new Models.Usuario();
                        MapperUsuarioToDr(usuario, dr);
                        usuarios.Add(usuario);
                    }
                }
            }
            cacheService.Set(keyCache, usuarios, CacheExpirationTime);
            return usuarios;
        }

        public async Task<Models.Usuario> GetByUser(Models.Usuario usuarioRecebido)
        {
            Models.Usuario usuario = new Models.Usuario();

            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "SELECT Id, Nome, Senha FROM Usuario WHERE Nome = @Nome AND Senha = @Senha";
                    MapperUsuarioToParameters(usuarioRecebido);
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();

                    if (dr.Read())
                    {
                        MapperUsuarioToDr(usuario, dr);
                        return usuario;
                    }
                }
            }
            return usuario;
        }

        public async Task<bool> VerifyToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            // Verifica se o token está válido e não expirou
            if (jwtToken == null || jwtToken.ValidTo < DateTime.UtcNow)
                return false;
            return true;
        }

        public async Task<string> Login(Models.Usuario usuarioRecebido)
        {
            var usuarioBanco = await GetByUser(usuarioRecebido);

            if (usuarioBanco.Id == 0)
            {
                return null;
            }

            return jwtManager.GenerateToken(usuarioBanco.Nome);
        }

        public async Task<List<string>> BuildPermissions(Models.Usuario usuarioRecebido)
        {
            var usuarioBanco = await GetByUser(usuarioRecebido);
            return await GetListPermission(usuarioBanco);
        }

        public async Task<List<string>> GetListPermission(Models.Usuario usuarioBanco)
        {
            List<string> permissions = new List<string>();
            using (var conn = new SqlConnection(connectionStringAux))
            {
                await conn.OpenAsync();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Tela FROM Tela_Permissao WHERE Id_Usuario = @Id AND Acessivel = 1 ";
                    cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int)).Value = usuarioBanco.Id;
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();

                    while (dr.Read())
                    {
                        string tela = dr["Tela"].ToString();
                        permissions.Add(tela);
                    }
                }
            }
            return permissions;
        }





        public async Task Add(Models.Usuario usuario)
        {
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "INSERT INTO Usuario (Nome, Senha) VALUES (@Nome,@Senha); SELECT scope_identity()";
                    MapperUsuarioToParameters(usuario);
                    usuario.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    cacheService.Remove(keyCache);
                }
            }
        }


        public async Task<bool> Update(Models.Usuario usuario)
        {
            int linhasAfetadas;
            using (conn)
            {
                await conn.OpenAsync();
                using (cmd)
                {
                    cmd.CommandText = "UPDATE Usuario SET Nome = @Nome, Senha  = @Senha WHERE Id = @Id";
                    cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int)).Value = usuario.Id;
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
                    cmd.CommandText = "DELETE Usuario WHERE Id = @Id";
                    cmd.Parameters.Add(new SqlParameter("@Id", System.Data.SqlDbType.Int)).Value = id;
                    linhasAfetadas = await cmd.ExecuteNonQueryAsync();
                }
            }
            if (linhasAfetadas == 0)
                return false;

            cacheService.Remove(keyCache);
            return true;

        }

        public void MapperUsuarioToDr(Models.Usuario usuario, SqlDataReader dr)
        {
            usuario.Id = (int)dr["Id"];
            usuario.Nome = dr["Nome"].ToString();
            usuario.Senha = dr["Senha"].ToString();
           
        }

        public void MapperUsuarioToParameters(Models.Usuario usuario)
        {
            cmd.Parameters.Add(new SqlParameter("@Nome", System.Data.SqlDbType.VarChar)).Value = usuario.Nome;
            cmd.Parameters.Add(new SqlParameter("@Senha", System.Data.SqlDbType.VarChar)).Value = usuario.Senha;

        }

    }
    }
