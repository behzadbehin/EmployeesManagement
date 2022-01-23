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
        public string FileTitle { get; set; }
        public FileSubjects FileSubject { get; set; }
        
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FileExtension { get; set; }
        public byte[] FileContent { get; set; }
        public Guid ContainerId { get; set; }
    }
    public enum FileSubjects
    {
        Profile = 1,
        Evidence = 2,
        ProductImage = 4,
        //add your new enum items here
        other = 100

    }
    public class EmployeeInput :  EntityDto<Guid>
    {
        public EmployeeInput()
        {
            Files= new List<FileObject>();
        }
        public string Name { get; set; }
        //public FileObject Profile { get; set; }
        public List<FileObject> Files { get; set; }
    }

    public interface IEmployeeAppService : ICrudAppService<EmployeeInput,Guid>
    {
        
    }
}