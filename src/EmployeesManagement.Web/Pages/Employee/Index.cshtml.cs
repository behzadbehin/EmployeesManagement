using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using EmployeeManagement.FileSystems;
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
        private readonly IFileSystemAppService _fileService;
        public bool Saved { get; set; } = false;

        public Index(IEmployeeAppService employeeAppService,  IFileSystemAppService fileService)
        {
            _employeeService = employeeAppService;
            _fileService = fileService;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            var employeeServiceInput =  String.IsNullOrEmpty(UISaveEmployeeDto.Name) ?
            new EmployeeInput() :
            new EmployeeInput()
            {
                //create main object here
                Name = UISaveEmployeeDto.Name,
            };

            using (var memoryStream = new MemoryStream())
            {

                #region  manage fiels
                if (UISaveEmployeeDto.ProfilePic != null)
                {
                    await UISaveEmployeeDto.ProfilePic.CopyToAsync(memoryStream);
                    var newFileObject = new FileObject
                    {
                        FileName = UISaveEmployeeDto.ProfilePic.FileName,
                        FileExtension = Path.GetExtension(UISaveEmployeeDto.ProfilePic.FileName),
                        FileSize = UISaveEmployeeDto.ProfilePic.Length / 1024, //convert from Byte to KB
                        FileSubject = FileSubjects.Profile,
                        FileTitle = "Employee Profile",
                        FileContent = memoryStream.ToArray()
                    };
                    employeeServiceInput.Files.Add(newFileObject);
                }

                if (UISaveEmployeeDto.Evidence1 != null)
                {
                    await UISaveEmployeeDto.Evidence1.CopyToAsync(memoryStream);
                    var newFileObject = new FileObject
                    {
                        FileName = UISaveEmployeeDto.Evidence1.FileName,
                        FileExtension = Path.GetExtension(UISaveEmployeeDto.Evidence1.FileName),
                        FileSize = UISaveEmployeeDto.Evidence1.Length / 1024, //convert from Byte to KB
                        FileSubject = FileSubjects.Evidence,
                        FileTitle = "Employee Evidence1",
                        FileContent = memoryStream.ToArray()
                    };
                    employeeServiceInput.Files.Add(newFileObject);
                }

                if (UISaveEmployeeDto.Evidence2 != null)
                {
                    await UISaveEmployeeDto.Evidence2.CopyToAsync(memoryStream);
                    var newFileObject = new FileObject
                    {
                        FileName = UISaveEmployeeDto.Evidence2.FileName,
                        FileExtension = Path.GetExtension(UISaveEmployeeDto.Evidence2.FileName),
                        FileSize = UISaveEmployeeDto.Evidence2.Length / 1024, //convert from Byte to KB
                        FileSubject = FileSubjects.Evidence,
                        FileTitle = "Employee Evidence2",
                        FileContent = memoryStream.ToArray()
                    };
                    employeeServiceInput.Files.Add(newFileObject);
                }
                #endregion


            }
            var blobContainer = await _employeeService.CreatingAsync(employeeServiceInput);
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteContainer()
        {
            if(String.IsNullOrEmpty(UISaveEmployeeDto.Name)) return Page();
            _fileService.DeleteFilesAsync(UISaveEmployeeDto.Name);

            return Page();
        }

    }


    public class UISaveEmployeeDto
    {
       // [Required]
        [Display(Name = "File")]

        public IFormFile ProfilePic { get; set; }
        public IFormFile Evidence1 { get; set; }
        public IFormFile Evidence2 { get; set; }

       // [Required]
        [Display(Name = "Filename")]
        public string FileName { get; set; }
        public string Name { get; set; }
    }
    
}

