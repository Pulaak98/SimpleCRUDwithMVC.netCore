using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMvc.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly  ICategoryRepository _categoryRepo;
        public CategoriesController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            List<Category>objjCategoryList = _categoryRepo.GetAll().ToList();
            return View(objjCategoryList);
        }

        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name.ToLower() == "test") 
            {
                ModelState.AddModelError("name", "test is an invalid name");
            }

            if(ModelState.IsValid)
            {
                _categoryRepo.Add(category);
                _categoryRepo.Save();
                TempData["success"] = "category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null||id==0) 
            {
                return NotFound();
            }
            Category? categoryFromDb=_categoryRepo.Get(c=>c.CategoryId == id);
            if (categoryFromDb == null) 
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
          

            if (ModelState.IsValid)
            {
                _categoryRepo.Update(category);
                _categoryRepo.Save();
                TempData["success"] = "category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(c => c.CategoryId == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? deletecategory= _categoryRepo.Get(c => c.CategoryId == id);

            if (deletecategory == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(deletecategory);
            _categoryRepo.Save();
            TempData["success"] = "category deleted successfully";
            return RedirectToAction("Index");
            
        }
    }
}
