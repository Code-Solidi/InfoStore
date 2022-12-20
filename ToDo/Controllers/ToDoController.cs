using Microsoft.AspNetCore.Mvc;

using OpenCqs;

using System;
using System.Collections.Generic;

using ToDos.Code;
using ToDos.Data.Handlers;
using ToDos.Models;
using ToDos.UseCases.Commands;
using ToDos.UseCases.Queries;

namespace ToDos.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IQueryHandler<GetToDosQuery, IEnumerable<ToDoModel>> getToDos;
        private readonly ICommandHandler<AddToDoCommand, CommandResult> addToDo;
        private const string Email = "achristov@hotmail.com";
        readonly IQueryHandler<GetToDoQuery, ToDoModel> getToDo;
        readonly ICommandHandler<UpdateToDoCommand, CommandResult> updateToDo;
        readonly ICommandHandler<SetToDoCheckedComplete, CommandResult> setToDoChecked;
        readonly ICommandHandler<DeleteToDoCommand, CommandResult> deleteToDo;
        readonly ToDoNotifier toDoNotifier;

        public ToDoController(IQueryHandler<GetToDosQuery, IEnumerable<ToDoModel>> getToDos
            , IQueryHandler<GetToDoQuery, ToDoModel> getToDo
            , ICommandHandler<AddToDoCommand, CommandResult> addToDo
            , ICommandHandler<UpdateToDoCommand, CommandResult> updateToDo
            , ICommandHandler<SetToDoCheckedComplete, CommandResult> setToDoChecked
            , ICommandHandler<DeleteToDoCommand, CommandResult> deleteToDo
            , ToDoNotifier toDoNotifier)
        {
            this.toDoNotifier = toDoNotifier ?? throw new ArgumentNullException(nameof(toDoNotifier), $"{nameof(toDoNotifier)} is null.");
            this.deleteToDo = deleteToDo ?? throw new ArgumentNullException(nameof(deleteToDo), $"{nameof(deleteToDo)} is null.");
            this.setToDoChecked = setToDoChecked ?? throw new ArgumentNullException(nameof(setToDoChecked), $"{nameof(setToDoChecked)} is null.");
            this.updateToDo = updateToDo ?? throw new ArgumentNullException(nameof(updateToDo), $"{nameof(updateToDo)} is null.");
            this.getToDos = getToDos ?? throw new ArgumentNullException(nameof(getToDos), $"{nameof(getToDos)} is null.");
            this.getToDo = getToDo ?? throw new ArgumentNullException(nameof(getToDo), $"{nameof(getToDo)} is null.");
            this.addToDo = addToDo ?? throw new ArgumentNullException(nameof(addToDo), $"{nameof(addToDo)} is null.");
        }

        public IActionResult Index()
        {
            var model = new ToDoIndexModel(this.getToDos, Email);
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

            return this.View(new ToDoIndexModel(this.getToDos, Email));
        }

        public IActionResult Edit(Guid id)
        {
            var model = this.getToDo.Handle(new GetToDoQuery(id, Email));
            return this.View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(ToDoModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.updateToDo.Handle(new UpdateToDoCommand(model.Id
                    , model.Text
                    , model.Done
                    , model.DueDateTime
                    , model.EMail
                    , model.Remind
                    , model.TimeUnit
                    , model.Repeat));
                if (result.IsSuccess)
                {
                    this.toDoNotifier.HasNew = true;
                    return this.RedirectToAction(nameof(Index));
                }
            }

            return this.View(model);
        }

        [HttpPost/*, ValidateAntiForgeryToken*/]
        public IActionResult Check(Guid id, bool @checked)
        {
            this.setToDoChecked.Handle(new SetToDoCheckedComplete(id, @checked));
            return this.Ok();
        }

        [HttpPost/*, ValidateAntiForgeryToken*/]
        public IActionResult Delete(Guid id)
        {
            this.deleteToDo.Handle(new DeleteToDoCommand(id));
            return this.Ok();
        }
    }
}