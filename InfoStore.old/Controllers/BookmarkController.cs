using InfoStore.Data.Handlers;
using InfoStore.Models;
using InfoStore.UseCases.Commands;
using InfoStore.UseCases.Queries;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using OpenCqs;

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace InfoStore.Controllers
{
    public class BookmarkController : Controller
    {
        private readonly ILogger<BookmarkController> logger;
        readonly ICommandHandler<AddBookmarkCommand, CommandResult> addBookmark;
        readonly IQueryHandler<GetGroupsForSelectQuery, IEnumerable<BookmarkIndexModel.GroupSelect>> getGroupsForSelect;
        readonly ICommandHandler<AddGroupCommand, CommandResult> addGroup;
        readonly IQueryHandler<GetGroupsQuery, IEnumerable<GroupModel>> getGroups;
        readonly IQueryHandler<GetGroupByIdQuery, GroupModel> getGroupById;
        readonly ICommandHandler<UpdateGroupCommand, CommandResult> updateGroup;
        readonly IQueryHandler<GetBookmarksQuery, IEnumerable<BookmarkModel>> getBookmarks;
        readonly ICommandHandler<SetGroupCommand, CommandResult> setGroup;
        readonly IQueryHandler<GetBookmarkQuery, BookmarkModel> getBookmark;
        readonly ICommandHandler<UpdateBookmarkCommand, CommandResult> updateBookmark;
        readonly ICommandHandler<DeleteGroupCommand, CommandResult> deleteGroup;
        readonly ICommandHandler<DeleteBookmarkCommand, CommandResult> deleteBookmark;

        public BookmarkController(ILogger<BookmarkController> logger
            , IQueryHandler<GetGroupsForSelectQuery, IEnumerable<BookmarkIndexModel.GroupSelect>> getGroupsForSelect
            , IQueryHandler<GetGroupsQuery, IEnumerable<GroupModel>> getGroups
            , IQueryHandler<GetGroupByIdQuery, GroupModel> getGroupById
            , IQueryHandler<GetBookmarksQuery, IEnumerable<BookmarkModel>> getBookmarks
            , IQueryHandler<GetBookmarkQuery, BookmarkModel> getBookmark
            , ICommandHandler<AddBookmarkCommand, CommandResult> addBookmark
            , ICommandHandler<AddGroupCommand, CommandResult> addGroup
            , ICommandHandler<UpdateGroupCommand, CommandResult> updateGroup
            , ICommandHandler<SetGroupCommand, CommandResult> setGroup
            , ICommandHandler<UpdateBookmarkCommand, CommandResult> updateBookmark
            , ICommandHandler<DeleteBookmarkCommand, CommandResult> deleteBookmark
            , ICommandHandler<DeleteGroupCommand, CommandResult> deleteGroup)
        {
            this.deleteBookmark = deleteBookmark ?? throw new ArgumentNullException(nameof(deleteBookmark), $"{nameof(deleteBookmark)} is null.");
            this.deleteGroup = deleteGroup ?? throw new ArgumentNullException(nameof(deleteGroup), $"{nameof(deleteGroup)} is null.");
            this.updateBookmark = updateBookmark ?? throw new ArgumentNullException(nameof(updateBookmark), $"{nameof(updateBookmark)} is null.");
            this.getBookmark = getBookmark ?? throw new ArgumentNullException(nameof(getBookmark), $"{nameof(getBookmark)} is null.");
            this.setGroup = setGroup ?? throw new ArgumentNullException(nameof(setGroup), $"{nameof(setGroup)} is null.");
            this.getBookmarks = getBookmarks ?? throw new ArgumentNullException(nameof(getBookmarks), $"{nameof(getBookmarks)} is null.");
            this.updateGroup = updateGroup ?? throw new ArgumentNullException(nameof(updateGroup), $"{nameof(updateGroup)} is null.");
            this.getGroupById = getGroupById ?? throw new ArgumentNullException(nameof(getGroupById), $"{nameof(getGroupById)} is null.");
            this.getGroups = getGroups ?? throw new ArgumentNullException(nameof(getGroups), $"{nameof(getGroups)} is null.");
            this.addGroup = addGroup ?? throw new ArgumentNullException(nameof(addGroup), $"{nameof(addGroup)} is null.");
            this.getGroupsForSelect = getGroupsForSelect ?? throw new ArgumentNullException(nameof(getGroupsForSelect), $"{nameof(getGroupsForSelect)} is null.");
            this.addBookmark = addBookmark ?? throw new ArgumentNullException(nameof(addBookmark), $"{nameof(addBookmark)} is null.");
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActionResult Index(string group, string search)
        {
            var filter = new BookmarkIndexModel.BookmarkFilter(group, search);
            this.TempData["Title"] = "Bookmarks";
            return this.View(new BookmarkIndexModel(this.getBookmarks, this.getGroupsForSelect, filter));
        }

        [HttpPost]
        public IActionResult Index(BookmarkIndexModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.addBookmark.Handle(new AddBookmarkCommand(model.Title, model.Url, model.Description, model.GroupId));
                if (result.IsSuccess)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                this.ModelState.AddModelError(nameof(model.Url), result.Exception.Message);
            }

            return this.View(new BookmarkIndexModel(this.getBookmarks, this.getGroupsForSelect));
        }

        public IActionResult Edit(Guid id)
        {
            var model = this.getBookmark.Handle(new GetBookmarkQuery(id));
            return this.View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(BookmarkModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.updateBookmark.Handle(new UpdateBookmarkCommand(model.Id, model.Title, model.Url, model.Description));
                if (result.IsSuccess)
                {
                    return this.View();
                }

                this.ModelState.AddModelError(nameof(model.Url), result.Exception.Message);
            }

            return this.View(model);
        }

        [HttpPost/*, ValidateAntiForgeryToken*/]
        public IActionResult Delete(Guid id)
        {
            this.deleteBookmark.Handle(new DeleteBookmarkCommand(id));
            return this.Ok();
        }

        public IActionResult Groups(Guid id)
        {
            var model = this.getGroupById.Handle(new GetGroupByIdQuery(id));
            model.Groups = this.getGroups.Handle(new GetGroupsQuery());
            return this.View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Groups(GroupModel model)
        {
            var groups = this.getGroups.Handle(new GetGroupsQuery());
            if (this.ModelState.IsValid)
            {
                var message = string.Empty;
                if (model.Id != Guid.Empty)
                {
                    var result = this.updateGroup.Handle(new UpdateGroupCommand(model.Id, model.Name, model.Description));
                    if (result.IsSuccess)
                    {
                        model = new GroupModel { Groups = groups };
                        return this.View(model);
                    }

                    message = result.Exception.Message;
                }
                else
                {
                    var result = this.addGroup.Handle(new AddGroupCommand(model.Name, model.Description));
                    if (result.IsSuccess)
                    {
                        model = new GroupModel { Groups = groups };
                        return this.View(model);
                    }

                    message = result.Exception.Message;
                }

                this.ModelState.AddModelError(nameof(model.Name), message);
            }

            model.Groups = groups;
            return this.View(model);
        }

        [HttpPost/*, ValidateAntiForgeryToken*/]
        public IActionResult DeleteGroup(Guid id)
        {
            this.deleteGroup.Handle(new DeleteGroupCommand(id));
            return this.RedirectToAction(nameof(Groups));
        }

        [HttpPost]
        public IActionResult SetGroup(Guid id, Guid groupId)
        {
            var command = new SetGroupCommand(id, groupId);
            var result = this.setGroup.Handle(command);
            return result.IsSuccess ? this.Ok() : throw new Exception("Booom!");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, });
        }
    }
}