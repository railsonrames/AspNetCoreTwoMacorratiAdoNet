using AspNetCoreTwoMacorratiAdoNet.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTwoMacorratiAdoNet.Models
{
    public class AlunoBLL : IAlunoBLL
    {
        public List<Aluno> GetAlunos()
        {
            var configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            var conexaoString = configuration.GetConnectionString("ConexaoPadrao");

            var alunos = new List<Aluno>();

            try
            {
                using (SqlConnection conexao = new SqlConnection(conexaoString))
                {
                    SqlCommand comando = new SqlCommand("GetAlunos", conexao);
                    comando.CommandType = CommandType.StoredProcedure;
                    conexao.Open();
                    SqlDataReader leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        Aluno aluno = new Aluno();
                        aluno.Id = Convert.ToInt32(leitor["Id"]);
                        aluno.Nome = leitor["Nome"].ToString();
                        aluno.Sexo = leitor["Sexo"].ToString();
                        aluno.Email = leitor["Email"].ToString();
                        aluno.Nascimento = Convert.ToDateTime(leitor["Nascimento"]);
                        alunos.Add(aluno);
                    }
                }
                return alunos;
            }
            catch
            {
                throw;
            }
        }

        public void IncluirAluno(Aluno aluno)
        {
            var configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            var conexaoString = configuration.GetConnectionString("ConexaoPadrao");

            var alunos = new List<Aluno>();

            try
            {
                using (SqlConnection conexao = new SqlConnection(conexaoString))
                {
                    SqlCommand comando = new SqlCommand("IncluirAluno", conexao);
                    comando.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramNome = new SqlParameter();
                    paramNome.ParameterName = "@Nome";
                    paramNome.Value = aluno.Nome;
                    comando.Parameters.Add(paramNome);

                    SqlParameter paramSexo = new SqlParameter();
                    paramSexo.ParameterName = "@Sexo";
                    paramSexo.Value = aluno.Sexo;
                    comando.Parameters.Add(paramSexo);

                    SqlParameter paramEmail = new SqlParameter();
                    paramEmail.ParameterName = "@Email";
                    paramEmail.Value = aluno.Email;
                    comando.Parameters.Add(paramEmail);

                    SqlParameter paramNascimento = new SqlParameter();
                    paramNascimento.ParameterName = "@Nascimento";
                    paramNascimento.Value = aluno.Nascimento;
                    comando.Parameters.Add(paramNascimento);

                    conexao.Open();
                    comando.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
