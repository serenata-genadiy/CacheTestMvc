using Cache;
using CacheTestMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CacheTestMVC.Controllers
{
    public class CarController : Controller
    {


        CarRepository repo = new CarRepository();
       

        public ActionResult Cache()
        {            
            return View(CacheHelper.GetCacheDetails());
        }
        public ActionResult Purge()
        {
            CacheHelper.Purge("AllCars");
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            var model = repo.GetCars();
            return View(model);
        }

        //
        // GET: /Car/Details/5

        public ActionResult Details(int id)
        {
            var car = repo.GetCars().First(x => x.Id == id);
            return View(car);
        }

        //
        // GET: /Car/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Car/Create

        [HttpPost]
        public ActionResult Create(Car model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repo.InsertCar(model);
                }                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Car/Edit/5

        public ActionResult Edit(int id)
        {
            var car = repo.GetCars().First(x => x.Id == id);
            return View(car);
        }

        //
        // POST: /Car/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Car/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Car/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
