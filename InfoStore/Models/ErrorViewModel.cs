using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InfoStore.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ModelStateDictionary Errors { get; set; }
    }
}