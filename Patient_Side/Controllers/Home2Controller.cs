using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Medical.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Patient_Side.Models;
using Microsoft.AspNetCore.Http;

namespace Patient_Side.Controllers
{
    public class Home2Controller : Controller
    {
        private readonly MedContext _context;

        public Home2Controller(MedContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            
            ViewModel vm = new ViewModel();
            vm.doctorList = _context.DOCTORTB.Take(4).ToList();


            TempData["OrderCount"] = _context.ORDERTB.Count();
            TempData["DoctorCount"] = _context.DOCTORTB.Count();
            TempData["MedicineCount"] = _context.MEDICINETB.Count();
            TempData["PatientCount"] = _context.PATIENTTB.Count();

            ViewBag.OrderCount = TempData["OrderCount"];
            ViewBag.DoctorCount = TempData["DoctorCount"];
            ViewBag.MedicineCount = TempData["MedicineCount"];
            ViewBag.PatientCount = TempData["PatientCount"];

            TempData.Keep("OrderCount");
            TempData.Keep("DoctorCount");
            TempData.Keep("MedicineCount");
            TempData.Keep("PatientCount");


            var rm = from r in _context.REVIEWTB
                     join p in _context.PATIENTTB on r.Patient_ID equals p.Patient_ID
                     select new Review
                     {
                         Review_ID = r.Review_ID,
                         Review_Msg = r.Review_Msg,
                         Patient_ID = r.Patient_ID,
                         Patient_Name = p.Patient_Name,
                         Patient_Profile = p.Patient_Profile
                     };
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
                                 City_Name = s == null ? "" : s.City_Name,

                                 Patient_Name = p == null ? "" : p.Patient_Name

                             }).ToList();

            vm.cartList = (from c in _context.CARTTB
                              join p in _context.PATIENTTB on c.Patient_ID equals p.Patient_ID
                              join m in _context.MEDICINETB on c.Medicine_ID equals m.Medicine_ID
                           where c.Patient_ID.Equals(@TempData["SessionID"])
                              select new Cart
                              {
                                  Cart_ID = c.Cart_ID,
                                  Medicine_ID = m.Medicine_ID,
                                  Patient_ID = p.Patient_ID,
                                  Medicine_Name = m == null ?"":m.Medicine_Name,
                                  Medicine_Image = m == null?"":m.Medicine_Image,
                                  Medicine_Price = m == null?"":m.Medicine_Price,
                                  Cart_Qty = c.Cart_Qty
                              }).ToList();
            TempData["count"] = vm.cartList.Count();
            TempData.Keep("SessionID");
            return View(vm);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
