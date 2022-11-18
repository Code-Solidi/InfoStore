using InfoStore.Data.Handlers;
using InfoStore.Models;
using InfoStore.UseCases.Commands;
using InfoStore.UseCases.Queries;

using Microsoft.AspNetCore.Mvc;

using OpenCqs;

using System;
using System.Collections.Generic;

namespace InfoStore.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IQueryHandler<GetToDosQuery, IEnumerable<ToDoModel>> getTodos;
        private readonly ICommandHandler<AddToDoCommand, CommandResult> addToDo;
        private const string Email = "achristov@hotmail.com";

        public ToDoController(IQueryHandler<GetToDosQuery, IEnumerable<ToDoModel>> getTodos
            , ICommandHandler<AddToDoCommand, CommandResult> addToDo)
        {
            this.addToDo = addToDo ?? throw new ArgumentNullException(nameof(addToDo), $"{nameof(addToDo)} is null.");
            this.getTodos = getTodos ?? throw new ArgumentNullException(nameof(getTodos), $"{nameof(getTodos)} is null.");
        }

        public IActionResult Index()
        {
            var model = new ToDoIndexModel(this.getTodos, ToDoController.Email);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(ToDoIndexModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.addToDo.Handle(new AddToDoCommand(model.Text));
                if (result.IsSuccess)
                {
                    return this.RedirectToAction(nameof(Index));
                }
            }

            return this.View(new ToDoIndexModel(this.getTodos, ToDoController.Email));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Reminder(ToDoModel model)
        {
            if (this.ModelState.IsValid)
            {
                //var result = this.addToDo.Handle(new AddToDoCommand(model.Text));
                //if (result.IsSuccess)
                //{
                //    return this.RedirectToAction(nameof(Index));
                //}
            }

            return this.RedirectToAction(nameof(Index));// View(new ToDoIndexModel(this.getTodos, ToDoController.Email));
        }
    }
}