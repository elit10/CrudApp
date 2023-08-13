using CrudApp.DAL;
using CrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CrudApp.Controllers
{
    public class LearningPathModelsController : Controller
    {
        private readonly LearningPathContext _learningPathDAL;

        public LearningPathModelsController(LearningPathContext learningPathDAL)
        {
            _learningPathDAL = learningPathDAL;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<LearningPathModel> learningPaths = new List<LearningPathModel>();
            try
            {
                learningPaths = _learningPathDAL.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(learningPaths);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LearningPathModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid";
                }
                bool result = _learningPathDAL.Insert(model);

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
                LearningPathModel learningPath = _learningPathDAL.GetById(id);
                if (learningPath.LPID == 0)
                {
                    TempData["errorMessage"] = $"Learning Path details not found with Id: {id}";
                    return RedirectToAction("Index");
                }
                return View(learningPath);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(LearningPathModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid";
                    return View();
                }
                bool result = _learningPathDAL.Update(model);

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
                LearningPathModel learningPath = _learningPathDAL.GetById(id);
                if (learningPath.LPID == 0)
                {
                    TempData["errorMessage"] = $"Learning Path details not found with Id: {id}";
                    return RedirectToAction("Index");
                }
                return View(learningPath);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(LearningPathModel model)
        {
            try
            {
                bool result = _learningPathDAL.Delete(model.LPID);

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
