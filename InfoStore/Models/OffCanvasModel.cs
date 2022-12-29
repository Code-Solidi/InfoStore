namespace InfoStore.Models
{
    public class OffCanvasModel
    {
        public OffCanvasModel(string title, string link)
        {
            this.Title = title;
            this.Link = link;
        }

        public string Title { get; init; }

        public string Link { get; init; }
    }
}