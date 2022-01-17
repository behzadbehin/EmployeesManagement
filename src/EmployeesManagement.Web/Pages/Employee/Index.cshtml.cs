using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using EmployeesManagement.Employees;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EmployeesManagement.Web.Pages.Employees
{
    public class Index : AbpPageModel
    {
        // [BindProperty]
        // public UploadFileDto UploadFileDto { get; set; }

        // private readonly IFileAppService _fileAppService;
        [BindProperty]
        public UISaveEmployeeDto UISaveEmployeeDto { get; set; }
        
        private readonly IEmployeeAppService _employeeService;
        public bool Saved { get; set; } = false;

        public Index(IEmployeeAppService employeeAppService)
        {
            _employeeService = employeeAppService;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            using (var memoryStream = new MemoryStream())
            {
                await UISaveEmployeeDto.File.CopyToAsync(memoryStream);
                await _employeeService.CreateAsync(
                    new SaveEmployeeDto
                    {
                        Name = UISaveEmployeeDto.Name,
                        ProfilePicture = memoryStream.ToArray()
                      //  FileName = SaveEmployeeDto.FileName
                    }
                );
                // await _fileAppService.SaveBlobAsync(
                //     new SaveBlobInputDto
                //     {
                //         Name = UploadFileDto.Name,
                //         Content = memoryStream.ToArray()
                //     }
                // );
            }
            

            return Page();
        }
    }

    public class UISaveEmployeeDto
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile File { get; set; }

        [Required]
        [Display(Name = "Filename")]
        public string FileName { get; set; }
        public string Name { get; set; }
    }
}

