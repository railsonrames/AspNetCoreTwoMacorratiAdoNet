using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTwoMacorratiAdoNet.Models
{
    public interface IAlunoBLL
    {
        List<Aluno> GetAlunos();
        void IncluirAluno(Aluno aluno);
    }
}
