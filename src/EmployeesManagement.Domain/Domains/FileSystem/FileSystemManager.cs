// using System;
// using System.Threading.Tasks;
// using Newtonsoft.Json;
// using Volo.Abp.BlobStoring.Database;
// using Volo.Abp.Domain.Services;

// namespace EmployeesManagement.FileSystem
// {
//     public class FileSystemManager : IDomainService
//     {
//         public async Task<DatabaseBlob> CreateBlob(byte[] content)
//         {
//             var blob = new DatabaseBlob(id: Guid.NewGuid(),containerId:Guid.NewGuid(),"behzad",content);
//             return null;
//         }
//     }
// }