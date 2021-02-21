﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doctor_Side.Models;
using Medical.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Doctor_Side.Controllers
{
    public class DoctorRegController : Controller
    {
        private readonly DoctorContext _context;
        private readonly IWebHostEnvironment _env;

        public DoctorRegController(DoctorContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Logout()
        {
           
            HttpContext.Session.Clear();
            return RedirectToAction("DoctorReg", "Login");
        }

        public IActionResult Login()
        {
            
            TempData.Keep("CountAppo");
            TempData.Keep("CountPat");
            TempData.Keep("CountReview");
            return View(new DoctorReg());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(DoctorReg doctorReg)
        {
            try
            {
                var user = _context.DOCTORTB.Where(u => u.Doctor_Email == doctorReg.Doctor_Email && u.Doctor_Password == doctorReg.Doctor_Password).FirstOrDefault();
                if (user != null)
                {
                    //HttpContext.Session.SetString("SessionName", user.Doctor_Name);
                    //ViewBag.DocName = HttpContext.Session.GetString("SessionName");

                    HttpContext.Session.SetInt32("SessionID", user.Doctor_ID);
                    ViewBag.SID = HttpContext.Session.GetInt32("SessionID");

                    HttpContext.Session.SetString("SessionImg", user.Doctor_Profile);
                    ViewBag.DoctorImg = HttpContext.Session.GetString("SessionImg");

                    //for edit data
                    HttpContext.Session.SetString("SessionPassword", user.Doctor_Password);
                    ViewBag.DoctorPassword = HttpContext.Session.GetString("SessionPassword");

                    HttpContext.Session.SetString("SessionName", user.Doctor_Name);
                    ViewBag.DoctorName = HttpContext.Session.GetString("SessionName");

                    TempData["Sessionid"] = HttpContext.Session.GetInt32("SessionID");
                    TempData["SessionImg"] = HttpContext.Session.GetString("SessionImg");
                    TempData["SessionName"] = HttpContext.Session.GetString("SessionName");

                    TempData.Keep("CountAppo");
                    TempData.Keep("CountPat");
                    TempData.Keep("CountReview");

                    //return View("../Home1/Index");
                    return RedirectToAction("Index", "Home1");
                    //return RedirectToAction("Create","DocClinic");
                }
                else
                {
                    return RedirectToAction("LoginFail");
                }
            }
            catch (Exception e)
            {
                return View(e);
            }
        }

        public IActionResult Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("SessionID") == null)
            {
                ViewBag.SID = HttpContext.Session.GetInt32("SessionID");
                return RedirectToAction("Login", "DoctorReg");
            }
            //var doctorReg = await _context.DOCTORTB
            //    .FirstOrDefaultAsync(m => m.Doctor_ID == id);

            var doctorReg = (from d in _context.DOCTORTB
                             join ct in _context.CATEGORYTB on d.Category_ID equals ct.Category_ID
                             //join cl in _context.CLINICTB on d.Clinic_ID equals cl.Clinic_ID

                             select new DoctorReg
                             {
                                 Doctor_ID = d.Doctor_ID,
                                 Doctor_Name = d.Doctor_Name,
                                 Doctor_Email = d.Doctor_Email,
                                 Doctor_Gender = d.Doctor_Gender,
                                 Doctor_Contact = d.Doctor_Contact,
                                 //Doctor_IsActive = d.Doctor_IsActive,
                                 Category_ID = d.Category_ID,
                                 Doctor_Degree = d.Doctor_Degree,
                                 Doctor_Education = d.Doctor_Education,
                                 Doctor_Experience = d.Doctor_Experience,
                                 //Clinic_ID = d.Clinic_ID,
                                 Doctor_Profile = d.Doctor_Profile,

                                 Category_Name = ct == null ? "" : ct.Category_Name,
                                 //Clinic_Name = cl == null ? "" : cl.Clinic_Name
                             })
                            .FirstOrDefault(m => m.Doctor_ID == id);

            if (doctorReg == null)
            {
                return NotFound();
            }
            ViewBag.DoctorName = TempData["SessionName"];
            TempData.Keep("SessionName");
            ViewBag.DoctorImg = TempData["SessionImg"];
            TempData.Keep("SessionImg");
            ViewBag.SID = TempData["Sessionid"];
            TempData.Keep("Sessionid");
            TempData.Keep("CountAppo");
            TempData.Keep("CountPat");
            TempData.Keep("CountReview");
            return View(doctorReg);
        }


        // Registration page
        public IActionResult Create()
        {
           
            loadCategory();


            ViewBag.DoctorName = TempData["SessionName"];
            TempData.Keep("SessionName");
            ViewBag.DoctorImg = TempData["SessionImg"];
            TempData.Keep("SessionImg");
            ViewBag.SID = TempData["Sessionid"];
            TempData.Keep("Sessionid");
            TempData.Keep("CountAppo");
            TempData.Keep("CountPat");
            TempData.Keep("CountReview");
            return View();
        }
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Doctor_ID,Doctor_Name,Doctor_Email,Doctor_Gender,Doctor_Password,Doctor_Contact,Doctor_IsActive,Category_ID,Doctor_Degree,Doctor_Profile,Doctor_Experience,Doctor_Education")] DoctorReg doctorReg, IFormCollection formCollection, IFormFile fileobj)
        {
            

            loadCategory();
            var imgext = Path.GetExtension(fileobj.FileName);

            if (imgext == ".jpg" || imgext == ".png")
            {
                var uploadimg = Path.Combine("../Medical/wwwroot", "Doctor_Image", fileobj.FileName);
                var stream = new FileStream(uploadimg, FileMode.Create);

                await fileobj.CopyToAsync(stream);
                stream.Close();

                //mi.Medicine_ID = 1;
                doctorReg.Doctor_Profile = fileobj.FileName;
                doctorReg.Doctor_IsActive = false;
                
                await _context.DOCTORTB.AddAsync(doctorReg);
                await _context.SaveChangesAsync();
                ViewBag.DoctorName = TempData["SessionName"];
                TempData.Keep("SessionName");
                ViewBag.DoctorImg = TempData["SessionImg"];
                TempData.Keep("SessionImg");
                ViewBag.SID = TempData["Sessionid"];
                TempData.Keep("Sessionid");
                TempData.Keep("CountAppo");
                TempData.Keep("CountPat");
                TempData.Keep("CountReview");
                return RedirectToAction("Login","DoctorReg");
            }


         
            return View(doctorReg);
        }

        

        // Profile Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorReg = await _context.DOCTORTB.FindAsync(id);
            if (doctorReg == null)
            {
                return NotFound();
            }
            

            loadCategory();
            ViewBag.DoctorName = TempData["SessionName"];
            TempData.Keep("SessionName");
            ViewBag.DoctorImg = TempData["SessionImg"];
            TempData.Keep("SessionImg");
            ViewBag.SID = TempData["Sessionid"];
            TempData.Keep("Sessionid");
            TempData.Keep("CountAppo");
            TempData.Keep("CountPat");
            TempData.Keep("CountReview");
            return View(doctorReg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile fileobj, [Bind("Doctor_ID,Doctor_Name,Doctor_Email,Doctor_Gender,Doctor_Password,Doctor_Contact,Doctor_IsActive,Category_ID,Doctor_Degree,Doctor_Profile,Doctor_Experience,Doctor_Education")] DoctorReg doctorReg, string fname, int id)
        {

            var getimg = await _context.DOCTORTB.FindAsync(id);
            _context.DOCTORTB.Remove(getimg);
            fname = Path.Combine("../Medical/wwwroot", "Doctor_Image", getimg.Doctor_Profile);
            FileInfo fi = new FileInfo(fname);
            if (fi.Exists)
            {
                System.IO.File.Delete(fname);
                fi.Delete();
            }
            var imgext = Path.GetExtension(fileobj.FileName);
                if (imgext == ".jpg" || imgext == ".png")
                {
                    var uploadimg = Path.Combine("../Medical/wwwroot", "Doctor_Image", fileobj.FileName);
                    var stream = new FileStream(uploadimg, FileMode.Create);

                    await fileobj.CopyToAsync(stream);
                    stream.Close();

                var Pass = HttpContext.Session.GetString("SessionPassword");
                //mi.Medicine_ID = 1;
                doctorReg.Doctor_Profile = fileobj.FileName;
                    doctorReg.Doctor_IsActive = false;
                doctorReg.Doctor_Password = Pass;
                    _context.Update(doctorReg);
                    await _context.SaveChangesAsync();
                    
                }

            ViewBag.DoctorName = TempData["SessionName"];
            TempData.Keep("SessionName");
            ViewBag.DoctorImg = TempData["SessionImg"];
            TempData.Keep("SessionImg");
            ViewBag.SID = TempData["Sessionid"];
            TempData.Keep("Sessionid");
            TempData.Keep("CountAppo");
            TempData.Keep("CountPat");
            TempData.Keep("CountReview");
            return RedirectToAction("Index", "Home1");
        }

        public IActionResult LoginFail()
        {
            return View();
        }

        public IActionResult PasswordChangeMsg()
        {
            ViewBag.DoctorName = TempData["SessionName"];
            TempData.Keep("SessionName");
            ViewBag.DoctorImg = TempData["SessionImg"];
            TempData.Keep("SessionImg");
            ViewBag.SID = TempData["Sessionid"];
            TempData.Keep("Sessionid");
            TempData.Keep("CountAppo");
            TempData.Keep("CountPat");
            TempData.Keep("CountReview");
            return View();
        }

        // GET: DoctorReg/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorReg = await _context.DOCTORTB
                .FirstOrDefaultAsync(m => m.Doctor_ID == id);
            if (doctorReg == null)
            {
                return NotFound();
            }
            ViewBag.DoctorName = TempData["SessionName"];
            TempData.Keep("SessionName");
            ViewBag.DoctorImg = TempData["SessionImg"];
            TempData.Keep("SessionImg");
            ViewBag.SID = TempData["Sessionid"];
            TempData.Keep("Sessionid");
            TempData.Keep("CountAppo");
            TempData.Keep("CountPat");
            TempData.Keep("CountReview");
            return View(doctorReg);
        }

        // POST: DoctorReg/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed( int id)
        {

            var doctorReg = await _context.DOCTORTB.FindAsync(id);
            _context.DOCTORTB.Remove(doctorReg);
            await _context.SaveChangesAsync();

            ViewBag.DoctorName = TempData["SessionName"];
            TempData.Keep("SessionName");
            ViewBag.DoctorImg = TempData["SessionImg"];
            TempData.Keep("SessionImg");
            ViewBag.SID = TempData["Sessionid"];
            TempData.Keep("Sessionid");
            TempData.Keep("CountAppo");
            TempData.Keep("CountPat");
            TempData.Keep("CountReview");
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ChangePassword(int? id)
        {
            var item = _context.DOCTORTB.Where(x => x.Doctor_ID == id).First();

            ViewBag.DoctorName = TempData["SessionName"];
            TempData.Keep("SessionName");
            ViewBag.DoctorImg = TempData["SessionImg"];
            TempData.Keep("SessionImg");
            ViewBag.SID = TempData["Sessionid"];
            TempData.Keep("Sessionid");
            TempData.Keep("CountAppo");
            TempData.Keep("CountPat");
            TempData.Keep("CountReview");
            return View(item);
        }
        [HttpPost]
        public ActionResult ChangePassword(int id,DoctorReg model)
        {
            var item = _context.DOCTORTB.Where(x => x.Doctor_ID == model.Doctor_ID).First();
            item.Doctor_Password = model.Doctor_Password;
            //_context.Update(model);
            _context.SaveChanges();

            ViewBag.DoctorName = TempData["SessionName"];
            TempData.Keep("SessionName");
            ViewBag.DoctorImg = TempData["SessionImg"];
            TempData.Keep("SessionImg");
            ViewBag.SID = TempData["Sessionid"];
            TempData.Keep("Sessionid");
            TempData.Keep("CountAppo");
            TempData.Keep("CountPat");
            TempData.Keep("CountReview");
            return RedirectToAction("PasswordChangeMsg");
        }

        public ActionResult ConfirmPassword(int id)
        {
            var item = _context.DOCTORTB.Where(x => x.Doctor_ID == id).First();

            ViewBag.DoctorName = TempData["SessionName"];
            TempData.Keep("SessionName");
            ViewBag.DoctorImg = TempData["SessionImg"];
            TempData.Keep("SessionImg");
            ViewBag.SID = TempData["Sessionid"];
            TempData.Keep("Sessionid");
            TempData.Keep("CountAppo");
            TempData.Keep("CountPat");
            TempData.Keep("CountReview");
            return View(item);
        }
        [HttpPost]
        public ActionResult ConfirmPassword(DoctorReg model)
        {
            try
            {
                var user = _context.DOCTORTB.SingleOrDefault(
                    u => u.Doctor_ID.Equals(model.Doctor_ID) &&
                    u.Doctor_Password.Equals(model.Doctor_Password)
                    );
                if(user != null)
                {

                    ViewBag.DoctorName = TempData["SessionName"];
                    TempData.Keep("SessionName");
                    ViewBag.DoctorImg = TempData["SessionImg"];
                    TempData.Keep("SessionImg");
                    ViewBag.SID = TempData["Sessionid"];
                    TempData.Keep("Sessionid");
                    TempData.Keep("CountAppo");
                    TempData.Keep("CountPat");
                    TempData.Keep("CountReview");
                    return View("ChangePassword");
                }
                else
                {
                   
                    return RedirectToAction("Not");
                }
            }
            catch(Exception e)
            {
                return View(e);
            }
           // return RedirectToAction("../Home1/Index");
        }

        private bool DoctorRegExists(int id)
        {
            return _context.DOCTORTB.Any(e => e.Doctor_ID == id);
        }


        
        private void loadCategory()
        {
            
                List<Category> CatList = new List<Category>();
                CatList = _context.CATEGORYTB.ToList();

                //CatList.Insert(0, new Category { Category_ID =0, Category_Name = "Select Category" });
                ViewBag.CatList = CatList;


            
        }
    }
}
