using Microsoft.AspNetCore.Mvc;

namespace CropVista_Backend.Common
{
    public class Result<T> : IActionResult
    {
        public T result { get; set; }
        public bool success { get; set; }
        public string message { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(this)
            {
                StatusCode = this.success ? 200 : 400
            };
            await objectResult.ExecuteResultAsync(context);
        }
    }
}
