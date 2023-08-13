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
    public class MIPController : Controller
    {
        private readonly MIPContext _MIPContext;

        public MIPController(MIPContext mipDAL)
        {
            _MIPContext = mipDAL;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<MIP> mips = new List<MIP>();
            try
            {
                mips = _MIPContext.GetAll();
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
        public IActionResult Create(MIP model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid";
                }
                bool result = _MIPContext.Insert(model);

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
                MIP mip = _MIPContext.GetById(id);
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
        public IActionResult Edit(MIP model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid";
                    return View();
                }
                bool result = _MIPContext.Update(model);

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
                MIP mip = _MIPContext.GetById(id);
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
        public IActionResult DeleteConfirmed(MIP model)
        {
            try
            {
                bool result = _MIPContext.Delete(model.MIPID);

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
