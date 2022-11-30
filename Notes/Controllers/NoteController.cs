using Microsoft.AspNetCore.Mvc;

using Notes.Data.Handlers;
using Notes.Models;
using Notes.UseCases.Commands;
using Notes.UseCases.Queries;

using OpenCqs;

using System;
using System.Collections.Generic;

namespace Notes.Controllers
{
    public class NoteController : Controller
    {
        private readonly IQueryHandler<GetNotesQuery, IEnumerable<NoteModel>> getNotes;
        private readonly IQueryHandler<GetNoteQuery, NoteModel> getNote;
        private readonly ICommandHandler<AddNoteCommand, CommandResult> addNote;
        private readonly ICommandHandler<UpdateNoteCommand, CommandResult> updateNote;
        private readonly ICommandHandler<DeleteNoteCommand, CommandResult> deleteNote;

        public NoteController(IQueryHandler<GetNotesQuery, IEnumerable<NoteModel>> getNotes
            , IQueryHandler<GetNoteQuery, NoteModel> getNote
            , ICommandHandler<AddNoteCommand, CommandResult> addNote
            , ICommandHandler<UpdateNoteCommand, CommandResult> updateNote
            , ICommandHandler<DeleteNoteCommand, CommandResult> deleteNote)
        {
            this.getNotes = getNotes ?? throw new ArgumentNullException(nameof(getNotes), $"{nameof(getNotes)} is null.");
            this.getNote = getNote ?? throw new ArgumentNullException(nameof(getNote), $"{nameof(getNote)} is null.");
            this.deleteNote = deleteNote ?? throw new ArgumentNullException(nameof(deleteNote), $"{nameof(deleteNote)} is null.");
            this.updateNote = updateNote ?? throw new ArgumentNullException(nameof(updateNote), $"{nameof(updateNote)} is null.");
            this.addNote = addNote ?? throw new ArgumentNullException(nameof(addNote), $"{nameof(addNote)} is null.");
        }

        public IActionResult Index()
        {
            var model = new NoteIndexModel(this.getNotes);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(NoteIndexModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.addNote.Handle(new AddNoteCommand(model.Title, model.Content));
                if (result.IsSuccess)
                {
                    return this.RedirectToAction(nameof(Index));
                }
            }

            return this.View(new NoteIndexModel(this.getNotes));
        }

        public IActionResult Edit(Guid id)
        {
            var model = this.getNote.Handle(new GetNoteQuery(id));
            return this.View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(NoteModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.updateNote.Handle(new UpdateNoteCommand(model.Id, model.Title, model.Content));
                if (result.IsSuccess)
                {
                    return this.View();
                }
            }

            return this.View(model);
        }

        [HttpPost/*, ValidateAntiForgeryToken*/]
        public IActionResult Delete(Guid id)
        {
            this.deleteNote.Handle(new DeleteNoteCommand(id));
            return this.Ok();
        }
    }
}