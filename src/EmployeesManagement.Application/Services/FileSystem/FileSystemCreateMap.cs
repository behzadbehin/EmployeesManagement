

using System;
using AutoMapper;
using EmployeesManagement.Employees;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.Guids;

namespace EmployeesManagement.FileSystems
{
    public class FileSystemCreateMap : Profile
    {
        public FileSystemCreateMap()
        {
            CreateMap<DatabaseBlob, FileObject>();
            CreateMap<FileObject, DatabaseBlob>()
            .ForMember(d=>d.Content, opt=>opt.MapFrom(s=>s.FileContent))
            .ForMember(d=>d.Name, opt=>opt.MapFrom(s=>s.FileName))
            // .ForMember(d=>d.ExtraProperties, opt=>opt.MapFrom(s=>s.FileName))
            // .ForMember(d=>d.ContainerId, opt=>opt.MapFrom(s=>s.FileName))
            ;
        }
    }

}