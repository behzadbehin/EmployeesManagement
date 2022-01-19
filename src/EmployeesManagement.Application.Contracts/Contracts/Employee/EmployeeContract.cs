using System;
using System.Collections.Generic;
using System.IO;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;


namespace EmployeesManagement.Employees
{
    public class SaveEmployeeDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
    public class FileObject
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FileExtension { get; set; }
        public byte[] FileContent { get; set; }
    }

    public class EmployeeInput :  EntityDto<Guid>
    {
        public EmployeeInput()
        {
            Evidences= new List<FileObject>();
        }
        public string EmployeeName { get; set; }
        public FileObject Profile { get; set; }
        public List<FileObject> Evidences { get; set; }
    }

    public interface IEmployeeAppService : ICrudAppService<SaveEmployeeDto,Guid>
    {
        
    }
}