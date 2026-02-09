using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class Usuario
    {
        readonly SqlConnection conn;
        readonly SqlCommand cmd;
        public Usuario(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            cmd.Connection = conn;
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
                    cmd.CommandText = "SELECT U.Nome ,U.Id, Tp.Id_Usuario, Tp.Home, Tp.ContabilizarTreino, Tp.Relatorio, Tp.Treino FROM Usuario U INNER JOIN Tela_Permissao Tp ON Tp.Id_Usuario = U.Id FROM Usuario WHERE Id = @Id";
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
                    cmd.CommandText = "SELECT Id, Nome, Senha, Tela_Home, Tela_Login, Tela_Usuario, Tela_Cadastro_Cliente, Tela_Relatorio_Contabil, Tela_Tarefa_Contabil, Tela_Tarefa_Legalizacao FROM Usuario WHERE Nome LIKE @Nome";
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
                    cmd.CommandText = "SELECT Id, Nome, Senha, Tela_Home, Tela_Login, Tela_Usuario, Tela_Cadastro_Cliente, Tela_Relatorio_Contabil, Tela_Tarefa_Contabil, Tela_Tarefa_Legalizacao FROM Usuario WHERE Nome = @Nome AND Senha = @Senha";
                    cmd.Parameters.Add(new SqlParameter("@Nome", System.Data.SqlDbType.NVarChar)).Value = nome;
                    cmd.Parameters.Add(new SqlParameter("@Senha", System.Data.SqlDbType.NVarChar)).Value = senha;
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    if (dr.Read())
                        MapperToDr(usuario, dr);
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
                        cmd.CommandText = "INSERT INTO Usuario VALUES (@Nome, @Senha, 'Sim','Sim','Não','Não','Não','Não','Não'); SELECT scope_identity() FROM Cliente";
                        cmd.Parameters.Add(new SqlParameter("@Nome", SqlDbType.NVarChar)).Value = usuario.Nome;
                        cmd.Parameters.Add(new SqlParameter("@Senha", SqlDbType.NVarChar)).Value = usuario.Senha;
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

        public async Task<bool> UpdatePermissionPageAsync(Models.Usuario usuario)
        {
            int linhasAfetadas;
            try
            {
                using (conn)
                {
                    await conn.OpenAsync();

                    using (cmd)
                    {
                        cmd.CommandText = "UPDATE Usuario SET Tela_Home = @Tela_Home, Tela_Login = @Tela_Login, Tela_Usuario = @Tela_Usuario, Tela_Cadastro_Cliente = @Tela_Cadastro_Cliente, Tela_Relatorio_Contabil = @Tela_Relatorio_Contabil, Tela_Tarefa_Contabil = @Tela_Tarefa_Contabil, Tela_Tarefa_Legalizacao = @Tela_Tarefa_Legalizacao WHERE Id = @Id";
                        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = usuario.Id;
                        MapperToParameter(usuario);
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
          //  usuario.IdUsuario = dr["Id_Usuario"].ToString();
          //  usuario.Home = dr["Home"].ToString();
          //  usuario.ContabilizarTreino = dr["ContabilizarTreino"].ToString();
          //  usuario.Relatorio = dr["Relatorio"].ToString();
          //  usuario.Treino = dr["Treino"].ToString();

        }

        private void MapperToParameter(Models.Usuario usuario)
        {
           // cmd.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar)).Value = usuario.Nome == null ? (object)DBNull.Value : usuario.Nome;
           // cmd.Parameters.Add(new SqlParameter("@Senha", SqlDbType.VarChar)).Value = usuario.Senha == null ? (object)DBNull.Value : usuario.Senha;
           // cmd.Parameters.Add(new SqlParameter("@Tela_Home", SqlDbType.VarChar)).Value = usuario.TelaHome == null ? (object)DBNull.Value : usuario.TelaHome;
           // cmd.Parameters.Add(new SqlParameter("@Tela_Login", SqlDbType.VarChar)).Value = usuario.TelaLogin == null ? (object)DBNull.Value : usuario.TelaLogin;
           // cmd.Parameters.Add(new SqlParameter("@Tela_Usuario", SqlDbType.VarChar)).Value = usuario.TelaUsuario == null ? (object)DBNull.Value : usuario.TelaUsuario;
            //cmd.Parameters.Add(new SqlParameter("@Tela_Cadastro_Cliente", SqlDbType.Char)).Value = usuario.TelaCadastroCliente == null ? (object)DBNull.Value : usuario.TelaCadastroCliente;
            //cmd.Parameters.Add(new SqlParameter("@Tela_Relatorio_Contabil", SqlDbType.VarChar)).Value = usuario.TelaRelatorioContabil == null ? (object)DBNull.Value : usuario.TelaRelatorioContabil;
            //cmd.Parameters.Add(new SqlParameter("@Tela_Tarefa_Contabil", SqlDbType.VarChar)).Value = usuario.TelaTarefaContabil == null ? (object)DBNull.Value : usuario.TelaTarefaContabil;
           // cmd.Parameters.Add(new SqlParameter("@Tela_Tarefa_Legalizacao", SqlDbType.VarChar)).Value = usuario.TelaTarefaLegalizacao == null ? (object)DBNull.Value : usuario.TelaTarefaLegalizacao;
        }
    }
}
