using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Perpustakaan.Models;

namespace Perpustakaan.Controllers
{
    public class AnggotaController : Controller
    {
        private PerpustakaanDBContext _db;
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

        [HttpPost("anggota/save")]
        public Task<IActionResult> SaveAnggotaForm(Anggota anggota)
        {
            try
            {
                TempData["StatusSave"] = 1;
                int result = anggota.SaveDetails();
                return Index(1);
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
                TempData["StatusSave"] = 1;
                int result = anggota.EditDetails();
                return Index(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
