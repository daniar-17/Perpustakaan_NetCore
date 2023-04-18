using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Perpustakaan.Models;
using System.IO;
using ClosedXML.Excel;

namespace Perpustakaan.Controllers
{
    public class AnggotaController : Controller
    {
        private readonly PerpustakaanDBContext _db;
        public AnggotaController(PerpustakaanDBContext db)
        {
            this._db = db;
        }

        [HttpGet("anggota")]
        public async Task<IActionResult> Index(int status = 0)
        {
            TempData["StatusSave"] = status;
            List<Anggota> listAnggota = new List<Anggota>();
            listAnggota = _db.Anggotas.FromSqlRaw($"select * from Anggota").ToList();
            ViewData["AnggotaList"] = listAnggota;
            return View("~/Views/Anggota/Index.cshtml");
        }

        [HttpGet("anggota/add")]
        public IActionResult AddAnggotaForm()
        {
            try
            {
                ViewData["Title"] = "Tambah Anggota";
                ViewData["Anggota"] = new Anggota();
                return View("~/Views/Anggota/AddData.cshtml");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpGet("anggota/add_multiple")]
        public IActionResult AddMultipleAnggotaForm()
        {
            try
            {
                ViewData["Title"] = "Tambah Anggota Multiple";
                ViewData["Anggota"] = new Anggota();
                return View("~/Views/Anggota/AddMultipleData.cshtml");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpPost("anggota/save")]
        public Task<IActionResult> SaveAnggotaForm(Anggota anggota)
        {
            try
            {
                int statusInfo = 1;
                int result = anggota.SaveDetails();
                return Index(statusInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpPost("anggota/save_multiple")]
        public Task<IActionResult> SaveMultipleAnggotaForm(List<Anggota> addAnggotas)
        {
            try
            {
                foreach(Anggota anggota in addAnggotas)
                {
                    int result = anggota.SaveDetails();
                }
                int statusInfo = 1;
                return Index(statusInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpPost("anggota/edit")]
        public Task<IActionResult> EditAnggotaForm(Anggota anggota)
        {
            try
            {
                int statusInfo = 2;
                int result = anggota.EditDetails();
                return Index(statusInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpPost]
        public Task<IActionResult> DeleteAnggotaForm(Anggota anggota)
        {
            try
            {
                int statusInfo = 3;
                int result = anggota.DeleteDetails();
                return Index(statusInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        [HttpPost("upload/anggota")]
        public Task<IActionResult> UploadData(IFormFile fromFiles)
        {
            try
            {
                var fileextension = Path.GetExtension(fromFiles.FileName);
                var filename = Guid.NewGuid().ToString() + fileextension;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", filename);
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    fromFiles.CopyTo(fs);
                }
                int rowno = 1;
                XLWorkbook workbook = XLWorkbook.OpenFromTemplate(filepath);
                var sheets = workbook.Worksheets.First();
                var rows = sheets.Rows().ToList();
                foreach (var row in rows)
                {
                    if (rowno != 1)
                    {
                        var test = row.Cell(1).Value.ToString();
                        if (string.IsNullOrWhiteSpace(test) || string.IsNullOrEmpty(test))
                        {
                            break;
                        }
                        Anggota anggota = new Anggota();
                        anggota.id_anggota = row.Cell(1).Value.ToString();
                        anggota.nama = row.Cell(2).Value.ToString();
                        anggota.jenis_kelamin = row.Cell(3).Value.ToString();
                        anggota.alamat = row.Cell(4).Value.ToString();
                        int result = anggota.SaveDetails();
                    }
                    rowno += 1;
                }
                int statusInfo = 1;
                return Index(statusInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        //Last Line
    }
}
