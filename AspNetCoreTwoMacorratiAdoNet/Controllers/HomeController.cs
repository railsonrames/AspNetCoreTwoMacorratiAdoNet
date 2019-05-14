using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTwoMacorratiAdoNet.Models;

namespace AspNetCoreTwoMacorratiAdoNet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            AlunoBLL _aluno = new AlunoBLL();

            List<Aluno> alunos = _aluno.GetAlunos().ToList();

            return View("Lista", alunos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Aluno aluno)
        {
            if (string.IsNullOrEmpty(aluno.Nome))
                ModelState.AddModelError("Nome", "O nome é obrigatório");

            if (string.IsNullOrEmpty(aluno.Sexo))
                ModelState.AddModelError("Sexo", "O sexo é obrigatório");

            if (string.IsNullOrEmpty(aluno.Email))
                ModelState.AddModelError("Email", "O email é obrigatório");

            if (aluno.Nascimento >= DateTime.Now.AddYears(-18))
                ModelState.AddModelError("Nascimento", "A data de nascimento é inválida");

            //if (aluno?.Nome == null || aluno.Sexo == null || aluno?.Email == null || aluno?.Nascimento == null)
            //{
            //    ViewBag.Erro = "Dados inválidos!";
            //return View();

            //}
            //else
            //{

            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            { 
                AlunoBLL _aluno = new AlunoBLL();
                _aluno.IncluirAluno(aluno);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
