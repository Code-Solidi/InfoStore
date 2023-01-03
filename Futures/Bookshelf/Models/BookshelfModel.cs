using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Internal;

namespace Bookshelf.Models
{
    public class BookshelfModel 
    {
        public BookshelfModel(string directory, IDirectoryContents contents) 
        {
            this.Contents = contents;
            this.Directory = directory;
        }

        public IDirectoryContents Contents { get; init; }


        public string Directory { get; init; }
    }
}