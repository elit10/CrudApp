using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudApp.Models;
using CrudApp.DAL;

namespace CrudApp.Controllers
{
    public class AccreditationsController : Controller
    {
        private readonly AccreditationContext _AccreditationContext;

        public AccreditationsController(AccreditationContext mipDAL)
        {
            _AccreditationContext = mipDAL;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Accreditation> mips = new List<Accreditation>();
            try
            {
                mips = _AccreditationContext.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(mips);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Accreditation model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid";
                }
                bool result = _AccreditationContext.Insert(model);

                if (!result)
                {
                    TempData["errorMessage"] = "Unable to save the data";
                    return View();
                }
                TempData["successMessage"] = "Learning Path details saved";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Accreditation mip = _AccreditationContext.GetById(id);
                if (mip.MIPID == 0)
                {
                    TempData["errorMessage"] = $"Learning Path details not found with Id: {id}";
                    return RedirectToAction("Index");
                }
                return View(mip);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(Accreditation model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid";
                    return View();
                }
                bool result = _AccreditationContext.Update(model);

                if (!result)
                {
                    TempData["errorMessage"] = "Unable to save the data";
                    return View();
                }
                TempData["successMessage"] = "Learning Path details saved";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Accreditation mip = _AccreditationContext.GetById(id);
                if (mip.MIPID == 0)
                {
                    TempData["errorMessage"] = $"Learning Path details not found with Id: {id}";
                    return RedirectToAction("Index");
                }
                return View(mip);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Accreditation model)
        {
            try
            {
                bool result = _AccreditationContext.Delete(model.MIPID);

                if (!result)
                {
                    TempData["errorMessage"] = "Unable to delete the data";
                    return View();
                }
                TempData["successMessage"] = "Learning Path details deleted";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
