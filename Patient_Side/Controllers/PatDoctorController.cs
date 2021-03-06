﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Patient_Side.Models;
using Medical.Models;
using System.IO;


namespace Patient_Side.Controllers
{
    public class PatDoctorController : Controller
    {
        private readonly MedContext _context;

        public PatDoctorController(MedContext context)
        {
            _context = context;
        }
        public IActionResult DoctorList()
        {
            ViewModel vm = new ViewModel();
            var doctorList = _context.DOCTORTB.Where(x=>x.Doctor_IsActive == true);
            vm.doctorList = doctorList.ToList();
            vm.reviewList = (from r in _context.REVIEWTB
                            join p in _context.PATIENTTB on r.Patient_ID equals p.Patient_ID
                            join s in _context.CITYTB on p.City_ID equals s.City_ID

                            select new Review
                            {
                                Review_ID = r.Review_ID,
                                Review_Msg = r.Review_Msg,
                                Patient_ID = r.Patient_ID,
                                //Patient_Name = p.Patient_Name,
                                Patient_Profile = p == null ? "" : p.Patient_Profile,
                                City_Name = s == null ? "": s.City_Name,

                                Patient_Name = p == null ? "" : p.Patient_Name

                            }).ToList();

            ViewBag.SID = TempData["Sessionid"];
            TempData.Keep("SessionID");
            return View(vm);
            //var docList = _context.DOCTORTB.ToList();
            //return View(docList);
        }

        public IActionResult DoctorDetails(int? id)
        {
            if (id == null)
            {
                TempData.Keep("SessionID");
                return NotFound();
            }
            ViewModel vm = new ViewModel();
            //var doc = (from d in _context.DOCTORTB
            //           join ct in _context.CATEGORYTB on d.Category_ID equals ct.Category_ID
            //           join cl in _context.CLINICTB on d.Doctor_ID equals cl.Doctor_ID
            //           join s in _context.STATETB on cl.State_ID equals s.State_ID
            //           join cty in _context.CITYTB on cl.City_ID equals cty.City_ID
            //           join a in _context.APPOINTMENTTB on d.Doctor_ID equals a.Doctor_ID
            //           where d.Doctor_ID == id

            //           select new ViewModel
            //           {
            //               Doctor_Name = d.Doctor_Name,
            //               Doctor_Education = d.Doctor_Education

            //               doctor = d,
            //               category = ct,
            //               clinic = cl,
            //               state = s,
            //               city = cty,
            //               appointment = a
            //           })
            //             .FirstOrDefault(m => m.doctor.Doctor_ID == id);
            vm.doctor = (from d in _context.DOCTORTB
                             join ct in _context.CATEGORYTB on d.Category_ID equals ct.Category_ID
                             join cl in _context.CLINICTB on d.Doctor_ID equals cl.Doctor_ID
                             join s in _context.STATETB on cl.State_ID equals s.State_ID
                             join cty in _context.CITYTB on cl.City_ID equals cty.City_ID
                             select new Doctor
                             {
                                 Doctor_ID = d.Doctor_ID,
                                 Category_ID = ct.Category_ID,
                                 Clinic_ID = cl.Clinic_ID,
                                 State_ID = s.State_ID,
                                 City_ID = cty.City_ID,
                                 State_Name = s == null ?"":s.State_Name,
                                 City_Name = cty == null ?"":cty.City_Name,
                                 Category_Name = ct == null ?"":ct.Category_Name,
                                 Doctor_Contact = d.Doctor_Contact,
                                 Doctor_Degree = d.Doctor_Degree,
                                 Doctor_Education = d.Doctor_Education,
                                 Doctor_Email = d.Doctor_Email,
                                 Doctor_Experience=d.Doctor_Experience,
                                 Doctor_Fees =d.Doctor_Fees,
                                 Doctor_Gender = d.Doctor_Gender,
                                 Doctor_Name =d.Doctor_Name,
                                 Doctor_Profile = d.Doctor_Profile,
                                 Clinic_Address = cl == null ?"":cl.Clinic_Address,
                                 Clinic_Contact = cl == null?"":cl.Clinic_Contact,
                                 Clinic_Name = cl == null ? "" : cl.Clinic_Name,
                                 Clinic_Pincode = cl == null ? "" : cl.Clinic_Pincode,
                                 Clinic_Time = cl == null ? "" : cl.Clinic_Time
                               


                             }).FirstOrDefault(m => m.Doctor_ID == id);
            //return View(docList);
            //if (docList == null)
            //{
            //    return View("NotFound");
            //}
            //vm.doctor = doc;
            //vm.doctorList = _context.DOCTORTB.Take(4).ToList();

            ViewBag.SID = TempData["Sessionid"];
            TempData.Keep("SessionID");
            return View(vm);
        }
        [HttpGet]
        public IActionResult DoctorReviewPartial()
        {
            ViewModel vm = new ViewModel();
            //vm.reviewList = _context.REVIEWTB.ToList();

            var pt = from r in _context.REVIEWTB
                     join p in _context.PATIENTTB on r.Patient_ID equals p.Patient_ID
                     //join s in _context.CITYTB on p.City_ID equals s.City_ID
                     select new Review
                     {
                         Review_ID = r.Review_ID,
                         Review_Msg = r.Review_Msg,
                         Patient_ID = r.Patient_ID,
                         //Patient_Name = p.Patient_Name,
                         Patient_Profile = p == null ?"": p.Patient_Profile,
                         //City_Name = s == null ? "": s.City_Name,

                         Patient_Name = p == null ? "" : p.Patient_Name

                     };
            vm.reviewList = pt.ToList();
            ViewBag.SID = TempData["Sessionid"];
            TempData.Keep("SessionID");
            return View(vm);
        }
        [HttpGet]
        public IActionResult TakeAppointment()
        {
            ViewBag.SID = TempData["Sessionid"];
            TempData.Keep("SessionID");
            return View();
        }
        [HttpPost]
        public IActionResult TakeAppointment(Appointment appointment)
        {
            if (HttpContext.Session.GetString("SessionID") == null)
            {

                return RedirectToAction("Login", "PatientReg");
            }

            //ViewBag.SID = HttpContext.Session.GetString("SessionID");
            //var d = HttpContext.Session.GetString("SessionID");
            //TempData["SessionID"] = HttpContext.Session.GetString("SessionID");

            int ID = Convert.ToInt32(Request.Form["Id"]);
            appointment.Doctor_ID = ID;

            var catID = _context.DOCTORTB.Find(ID);
            appointment.Category_ID = catID.Category_ID;

            appointment.Patient_ID = (int)TempData["SessionID"];
            appointment.Appointment_Status = "Requested";
            _context.APPOINTMENTTB.Add(appointment);
            _context.SaveChanges();

            ViewBag.SID = TempData["Sessionid"];
            TempData.Keep("SessionID");
            return RedirectToAction("DoctorList", "PatDoctor");
        }

        public IActionResult DocCategoryList()
        {
            ViewModel vm = new ViewModel();
            vm.categoryList = _context.CATEGORYTB.ToList();
            return View(vm);
        }
        public IActionResult DocCategory(int? id)
        {
            ViewModel vm = new ViewModel();
            vm.category = _context.CATEGORYTB.Find(id);
            vm.categoryList = _context.CATEGORYTB.ToList();
            vm.doctorList = _context.DOCTORTB.Where(x => x.Category_ID == id).ToList();
            return View(vm);
        }
    }
}
