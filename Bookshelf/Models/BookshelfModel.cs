using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Internal;

using System.Collections.Generic;

namespace Bookshelf.Models
{
    public class BookshelfModel 
    {
        public BookshelfModel(string directory, IDirectoryContents contents, IDictionary<string, string> nameMap)
        {
            this.Contents = contents;
            this.Directory = directory;
            this.nameMap = nameMap;
        }

        public IDirectoryContents Contents { get; init; }


        public string Directory { get; init; }

        public IDictionary<string, string> nameMap { get; init; }
    }
}