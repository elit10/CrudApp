﻿using CrudApp.DAL;
using CrudApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrudApp.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly Employee_DAL _dal;

		public EmployeeController(Employee_DAL dal)
		{
			_dal = dal;
		}

		[HttpGet]
		public IActionResult Index()
		{
			List<Employee> employees = new List<Employee>();
			try
			{
				employees = _dal.GetAll();
			}
			catch (Exception ex)
			{
				TempData["errorMessage"] = ex.Message;
			}
			return View(employees);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Employee model)
		{
			try
			{

				if (!ModelState.IsValid)
				{
					TempData["errorMessage"] = "Model data is invalid";
				}
				bool result = _dal.Insert(model);

				if (!result)
				{
					TempData["errorMessage"] = "Unable to save the data";
					return View();
				}
				TempData["successMessage"] = "Engineer details saved";
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
                Employee employee = _dal.GetById(id);
                if (employee.Id == 0)
                {
                    TempData["errorMessage"] = $"Engineer details not found with Id: {id}";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(Employee model)
        {

            try
            {
				if (!ModelState.IsValid)
				{
					TempData["errorMessage"] = "Model data is invalid";
					return View(); 
				}
				bool result = _dal.Update(model);

				if (!result)
				{

                    TempData["errorMessage"] = "Unable to save the data";
                    return View();
                }
                TempData["successMessage"] = "Engineer details saved";
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
                Employee employee = _dal.GetById(id);
                if (employee.Id == 0)
                {
                    TempData["errorMessage"] = $"Engineer details not found with Id: {id}";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Employee model)
        {

            try
            {
            
                bool result = _dal.Delete(model.Id);

                if (!result)
                {

                    TempData["errorMessage"] = "Unable to delete the data";
                    return View();
                }
                TempData["successMessage"] = "Engineer details deleted";
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


