using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UseStaticFileAfterAuthorizationPolicy.Pages
{
    [Authorize]
    public class GetFileWithAuthModel : PageModel
    {

        private readonly IHostEnvironment _env;

        public GetFileWithAuthModel(IHostEnvironment env)
        {
            this._env = env;
        }
     
        public PhysicalFileResult OnGet()
        {
            var filePath = Path.Combine(
                    _env.ContentRootPath, "MyStaticFiles", "Images", "flower.jpg");

            return PhysicalFile(filePath, "image/jpeg");
        }
    }
}
