using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using System.Net;
using System.IO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class PanController : ControllerBase
    {
        
            private readonly string AppDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            private static List<FileRecord> fileDB = new List<FileRecord>();
            FileDBContext dbContext = new FileDBContext();

            [HttpPost]
            [Consumes("multipart/form-data")]

            public async Task<HttpResponseMessage> PostAsync([FromForm] FileModel model)
            {
                //  try
                //{
                FileRecord file = await SaveFileAsync(model.MyFile);

                if (!string.IsNullOrEmpty(file.FilePath))
                {
                    //   file.AltText = model.AltText;
                    // file.Description = model.Description;
                    //Save to Inmemory object
                    //fileDB.Add(file);
                    //Save to SQL Server DB
                    SaveToDB(file);
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                //  }
                /* catch (Exception ex)
                 {
                     return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                     {
                         Content = new StringContent(ex.Message),
                     };
                 }*/
            }

            private async Task<FileRecord> SaveFileAsync(IFormFile myFile)
            {
                FileRecord file = new FileRecord();
                if (myFile != null)
                {
                    if (!Directory.Exists(AppDirectory))
                        Directory.CreateDirectory(AppDirectory);

                    var fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(myFile.FileName);
                    var path = Path.Combine(AppDirectory, fileName);

                    file.CustomerId = fileDB.Count() + 1;
                    file.FilePath = path;
                    file.FileName = fileName;
                    file.FileFormat = Path.GetExtension(myFile.FileName);
                    file.ContentType = myFile.ContentType;

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await myFile.CopyToAsync(stream);
                    }

                    return file;
                }
                return file;
            }

            private void SaveToDB(FileRecord record)
            {
                if (record == null)
                    throw new ArgumentNullException($"{nameof(record)}");

                PanCard fileData = new PanCard();
                fileData.FilePath = record.FilePath;
                fileData.FileName = record.FileName;
                fileData.FileExtension = record.FileFormat;
                fileData.MimeType = record.ContentType;

                dbContext.PanCard.Add(fileData);
                dbContext.SaveChanges();
            }

            [HttpGet]
            public List<FileRecord> GetAllFiles()
            {
                //getting data from inmemory obj
                //return fileDB;
                //getting data from SQL DB
                return dbContext.PanCard.Select(n => new FileRecord
                {
                    CustomerId = n.CustomerId,
                    ContentType = n.MimeType,
                    FileFormat = n.FileExtension,
                    FileName = n.FileName,
                    FilePath = n.FilePath
                }).ToList();
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> DownloadFile(int id)
            {
                if (!Directory.Exists(AppDirectory))
                    Directory.CreateDirectory(AppDirectory);

                //getting file from inmemory obj
                //var file = fileDB?.Where(n => n.Id == id).FirstOrDefault();
                //getting file from DB
                var file = dbContext.PanCard.Where(n => n.CustomerId == id).FirstOrDefault();

                var path = Path.Combine(AppDirectory, file?.FilePath);

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                var contentType = "APPLICATION/octet-stream";
                var fileName = Path.GetFileName(path);

                return File(memory, contentType, fileName);
            }
        }
    }

