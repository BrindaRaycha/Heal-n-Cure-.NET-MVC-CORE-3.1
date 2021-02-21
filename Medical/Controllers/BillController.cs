using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using Medical.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data.Entity;

namespace Medical.Controllers
{
    public class BillController : Controller
    {
        private readonly MedContext _context;
        public BillController(MedContext context)
        {
            _context = context;
        }
        public IActionResult Details(int? id)
        {
            if (HttpContext.Session.GetString("SessionID") == null)
            {
                return RedirectToAction("Create", "Login");
            }
            if (id == null)
            {
                return NotFound();
            }
            

            var vm = new ViewModel();
            var mdlist = _context.MEDICINETB.ToList();
            

            vm.bill = (from b in _context.BILLTB
                       join p in _context.PATIENTTB on b.Patient_ID equals p.Patient_ID
                       join o in _context.ORDERTB on b.Order_ID equals o.Order_ID
                       join s in _context.STATETB on b.State_ID equals s.State_ID
                       join c in _context.CITYTB on b.City_ID equals c.City_ID

                       select new Bill
                       {
                           Patient_ID = p.Patient_ID,
                           Patient_Name = p == null ? "" : p.Patient_Name,
                           City_ID = c.City_ID,
                           City_Name = c == null ? "" : c.City_Name,
                           State_ID = s.State_ID,
                           State_Name = s == null ? "" : s.State_Name,
                           Order_ID = o.Order_ID,
                           Order_Date = o == null ? "" : o.Order_Date,
                           Order_Status = o == null ? "" : o.Order_Status,
                           Order_Amount = o == null?"":o.Order_Amount,
                           Bill_ID = b.Bill_ID,
                           Bill_Address = b.Bill_Address,
                           Bill_Contact = b.Bill_Contact,
                           Bill_Email = b.Bill_Email,
                           Bill_Pincode = b.Bill_Pincode
                       }).FirstOrDefault(m => m.Bill_ID == id);

            vm.orderMedicalList = (from b in _context.BILLTB
                           join o in _context.ORDERTB on b.Order_ID equals o.Order_ID
                           join p in _context.PATIENTTB on b.Patient_ID equals p.Patient_ID
                           join om in _context.ORDER_MEDICALTB on o.Order_ID equals om.Order_ID
                           join m in _context.MEDICINETB on om.Medicine_ID equals m.Medicine_ID
                           where b.Bill_ID == id
                           select new OrderMedical
                           {
                               Bill_ID = b.Bill_ID,
                               //Patient_ID = p.Patient_ID,
                               Order_Medicine_ID = om.Order_Medicine_ID,

                               Medicine_ID = m.Medicine_ID,
                               Medicine_Name = m == null?"":m.Medicine_Name,
                               Order_ID = o.Order_ID,
                               Order_Qty = om.Order_Qty,
                               Price = om.Price,
                                Order_Amount = o == null ? "" : o.Order_Amount,
                               Medicine_Price = m == null ? "" : m.Medicine_Price,
                               Medicine_Image = m == null ?"":m.Medicine_Image,
                               //Medicine_Price = m == null ?"":m.Medicine_Price


                           }).ToList();
                           
            return View(vm);
        }

        
        

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("SessionID") == null)
            {
                return RedirectToAction("Create", "Login");
            }
            //ViewModel vm = new ViewModel();
            var bList = from b in _context.BILLTB
                        join o in _context.ORDERTB on b.Order_ID equals o.Order_ID
                        join p in _context.PAYMENTTB on o.Payment_Type_ID equals p.Payment_Type_ID
                        select new ViewModel
                        {
                            bill = b,
                            order = o,
                            payment = p
                        };
        
            return View(bList);
        }
    }
}
