using System.Security.Cryptography.X509Certificates;
using AppTarefas.Models;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;

namespace AppTarefas.Controllers
{
    public class TarefasController : Controller
    {
        //lista em memoria (grava as informações apenas quando a aplicação esta rodando)

        private static List<Tarefa> _Tarefa = new List<Tarefa>();
        private static int _proximoId = 1;

        public IActionResult Index()
        {
            return View(_Tarefa); // envia a lista de tarefas como parametro para a pag index
        }

        //get tarefas
        //pegar a pag e exibi, "cria" a view da pag
        public IActionResult Create()
        {
            return View();
        }

        //POST: tarefas/Create
        [HttpPost]// especifica que este metodo responde a requisições
        [ValidateAntiForgeryToken] //protege contra ataques 
        public IActionResult Create(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {

                tarefa.TarefaId = _proximoId++; // atribui o id da tarefa e incrementa o proximo id
                _Tarefa.Add(tarefa); // adiciona a tarefa na lista de tarefas
                return RedirectToAction(nameof(Index)); // redireciona para a pag index
            }
            return View(tarefa); // envia a tarefa como parametro para a pag create

        }

        public IActionResult Edit(int id)
        {
            var tarefa = _Tarefa.FirstOrDefault(t => t.TarefaId == id);
            return View(tarefa);
        }

        //post: tarefa / Edit/1

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Tarefa tarefaAtualizada)
        {
            var tarefa = _Tarefa.FirstOrDefault(t => t.TarefaId == id);
            tarefa.Titulo = tarefaAtualizada.Titulo;
            tarefa.Descricao = tarefaAtualizada.Descricao;
            tarefa.Concluida = tarefaAtualizada.Concluida;


            return RedirectToAction("Index");


        }

        public IActionResult Delete(int id)
        {
            var tarefa = _Tarefa.FirstOrDefault(t => t.TarefaId == id);
            if (tarefa != null)
            {
                _Tarefa.Remove(tarefa);
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Confirmação")]
        [ValidateAntiForgeryToken]
        public IActionResult Deleteconfired(int id)
        {
            var tarefa = _Tarefa.FirstOrDefault(t => t.TarefaId == id);
            if (tarefa != null)
            {
                _Tarefa.Remove(tarefa);
            }
            return RedirectToAction("Index");
        }



        public IActionResult Details(int id)
        {
            var tarefa = _Tarefa.FirstOrDefault(t => t.TarefaId == id);
            return View(tarefa); // envia a tarefa como parametro para a pag details
        }



    }
}
