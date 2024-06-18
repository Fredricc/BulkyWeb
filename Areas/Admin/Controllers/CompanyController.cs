using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
           
        }

        public IActionResult Index()
        {
            List<Company> objCompaniesList = _unitOfWork.Company.GetAll().ToList();

           
            return View(objCompaniesList);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company companyObj = _unitOfWork.Company.Get(u =>u.Id == id);
                return View(companyObj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
           
            if (ModelState.IsValid)
            {
                if(CompanyObj.Id ==0)
                {
                _unitOfWork.Company.Add(CompanyObj);

                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index");
            }
            else
            {
            return View(CompanyObj);
            }

        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompaniesList = _unitOfWork.Company.GetAll().ToList();

            return Json(new { data = objCompaniesList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message ="Error while deleting" });
            }

            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }

}

