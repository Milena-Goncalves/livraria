using System;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace projecto_iefp.Models;

public class LivrosContext
{
    public string ConnectionString { get; set; }


    public LivrosContext()
    {
        this.ConnectionString = "server=localhost;port=3306;database=bd_leitura;user=root;password=123456";
    }

    private MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }

    public List<Livro> GetAllLivros()
    {
        List<Livro> livroList = new List<Livro>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM livros ORDER BY titulo ASC", conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    livroList.Add(new Livro()
                    {
                        Titulo = reader.GetString("titulo"),
                        Autor = reader.GetString("autor"),
                        Editora = reader.GetString("editora"),
                        Genero = reader.GetString("genero"),
                        Capa = reader.GetString("capa"),
                        Sinopse = reader.GetString("sinopse")
                    });

                }
            }
        }
        return livroList;
    }

    //Operação GetGeneros dos livros

    public List<Livro> GetAllGenero(string genero)
    {
        List<Livro> livroList = new List<Livro>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();

         
            //Criei uma variável com @ no MySql, mas não sabia como usar a variável, então voltei ao storage normal
            MySqlCommand cmd = new MySqlCommand("CALL Genero (@genero);", conn);

            cmd.Parameters.AddWithValue("@genero", genero);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    livroList.Add(new Livro()
                    {
                        Titulo = reader.GetString("titulo"),
                        Autor = reader.GetString("autor"),
                        Editora = reader.GetString("editora"),
                        Genero = reader.GetString("genero"),
                        Capa = reader.GetString("capa"),
                        Sinopse = reader.GetString("sinopse")
                    });

                }
            }
        }
        return livroList;
    }

    public List<string> GetAllTitulos()
    {
        List<string> titulos = new List<string>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT titulo FROM livros", conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    titulos.Add(reader.GetString("titulo"));
                }
            }
        }

        return titulos;
    }

    public List<string> GetFavoritos(int usuarioId)
{
    List<string> favoritos = new List<string>();

    using (MySqlConnection conn = GetConnection())
    {
        conn.Open();

        string query = @"
            SELECT l.titulo
            FROM livros_favoritos lf
            JOIN livros l ON lf.livro_id = l.id
            WHERE lf.usuario_id = @UsuarioId";

        MySqlCommand cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                // Assumindo que 'titulo' é uma coluna na tabela 'livros'
                favoritos.Add(reader.GetString("titulo"));
            }
        }
    }

    return favoritos;
}


    public void CreateComentario(Comentario comentarios)
    {

        using (MySqlConnection conn = GetConnection())
        {
            //Abrir a ligação
            conn.Open();

            //Query
            MySqlCommand cmd = new MySqlCommand("INSERT INTO comentario (usuario_id, livros_id, livro_titulo, nome, comentario) VALUES (@usuarioId, @livrosId, @livroTitulo, @nome, @comentario);", conn);


            cmd.Parameters.AddWithValue("@usuarioId", comentarios.UsuarioId);
            cmd.Parameters.AddWithValue("@livrosId", comentarios.LivrosId);
            cmd.Parameters.AddWithValue("@livroTitulo", comentarios.LivroTitulo);
            cmd.Parameters.AddWithValue("@nome", comentarios.Nome);
            cmd.Parameters.AddWithValue("@comentario", comentarios.ComentarioTexto);

            cmd.ExecuteNonQuery();

            conn.Close();

        }
    }

    public void CreatePost(Post post)
    {

        using (MySqlConnection conn = GetConnection())
        {
            //Abrir a ligação
            conn.Open();

            //Query
            MySqlCommand cmd = new MySqlCommand("INSERT INTO post (usuario_id, conteudo, editado) VALUES (@UsuarioId, @Conteudo, @Editora);", conn);


            cmd.Parameters.AddWithValue("@UsuarioId", post.UsuarioID);
            cmd.Parameters.AddWithValue("@Conteudo", post.Conteudo);
            cmd.Parameters.AddWithValue("@Editora", post.Editado);

            cmd.ExecuteNonQuery();

            conn.Close();

        }
    }

    public string SenhaHash(string senha)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
    public void CreatePerfil(Perfil perfil)
    {

        using (MySqlConnection conn = GetConnection())
        {
            //Abrir a ligação
            conn.Open();

            string senha = SenhaHash(perfil.Senha);
            //Query
            MySqlCommand cmd = new MySqlCommand("INSERT INTO usuarios (nome, email, senha, imagem) VALUES (@nome, @email, @senha, @imagem);", conn);



            cmd.Parameters.AddWithValue("@nome", perfil.Nome);
            cmd.Parameters.AddWithValue("@email", perfil.Email);
            cmd.Parameters.AddWithValue("@senha", senha);
            cmd.Parameters.AddWithValue("@imagem", perfil.Imagem);

            cmd.ExecuteNonQuery();

            conn.Close();

        }
    }

    public bool Login(string email, string senha)
    {

        using (MySqlConnection conn = GetConnection())
        {
            //Abrir a ligação
            conn.Open();

            //Query
            MySqlCommand cmd = new MySqlCommand("SELECT senha FROM usuarios WHERE email = @email;", conn);
            cmd.Parameters.AddWithValue("@email", email);

        using (MySqlDataReader reader = cmd.ExecuteReader()){
            
            if (reader.Read())
            {
                string carregaSenha = reader["senha"].ToString();

                string verifiSenha = SenhaHash(senha);  
                
                if (verifiSenha == carregaSenha){
                    return true;
                }
            }
        }
            conn.Close();

        }
        return false;
    }

    /*public List<Comentario> GetAllComentarios()
    {
        List<Comentario> ComentarioList = new List<Comentario>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM comentario", conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComentarioList.Add(new Comentario()
                    {
                        LivroTitulo = reader.GetString("livro_titulo"),
                        Nome = reader.GetString("nome"),
                        ComentarioTexto = reader.GetString("comentario")
                    });

                }
            }
        }
        return ComentarioList;
    }*/

    public List<Comentario> SearchComentario(string titulo)
    {

        List<Comentario> comentariosList = new List<Comentario>();

        
        using (MySqlConnection conn = GetConnection())
        {
            //Abrir a ligação
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM comentario WHERE livro_titulo LIKE CONCAT('%',@livroTitulo,'%');", conn);
            cmd.Parameters.AddWithValue("@livroTitulo", titulo);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    comentariosList.Add(new Comentario()
                    {
                        LivroTitulo = reader.GetString("livro_titulo"),
                        Nome = reader.GetString("nome"),
                        ComentarioTexto = reader.GetString("comentario")
                    });
                    
                }
            }
        }

        return comentariosList;
    }

    public void CreateSugestao(Pedido pedidos)
    {

        using (MySqlConnection conn = GetConnection())
        {
            //Abrir a ligação
            conn.Open();

            //Query
            MySqlCommand cmd = new MySqlCommand("INSERT INTO pedido (usuario_id, titulo, sinopse, capa, autor, editora, genero) VALUES (@usuarioID, @livroTitulo, @livroSinopse, @livroCapa, @livroAutor, @livroEditora, @livroGenero);", conn);

            cmd.Parameters.AddWithValue("@usuarioID", pedidos.UsuarioId);
            cmd.Parameters.AddWithValue("@livroTitulo", pedidos.LivroTitulo);
            cmd.Parameters.AddWithValue("@livroSinopse", pedidos.LivroSinopse);
            cmd.Parameters.AddWithValue("@livroCapa", pedidos.LivroCapa);
            cmd.Parameters.AddWithValue("@livroAutor", pedidos.LivroAutor);
            cmd.Parameters.AddWithValue("@livroEditora", pedidos.LivroEditora);
            cmd.Parameters.AddWithValue("@livroGenero", pedidos.LivroGenero);

            cmd.ExecuteNonQuery();

            conn.Close();

        }
    }

    

}