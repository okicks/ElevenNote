﻿using ElevenNote.Data;
using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Category
        public ActionResult Index()
        {
            var service = CreateCategoryService();
            var model = service.GetCategory();

            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCategoryService();

            if (service.CreateCategory(model))
            {
                TempData["SaveResult"] = "Your category was created.";
                return RedirectToAction("Index");
            };

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateCategoryService();
            var model = svc.GetCategoryById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateCategoryService();
            var detail = service.GetCategoryById(id);
            var model =
                new CategoryEdit
                {
                    CategoryId = detail.CategoryId,
                    Name = detail.Name
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CategoryId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCategoryService();

            if (service.UpdateCategory(model))
            {
                TempData["SaveResult"] = "Your category was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your category could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCategoryService();
            var model = svc.GetCategoryById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateCategoryService();

            service.DeleteCategory(id);

            TempData["SaveResult"] = "Your category was deleted";

            return RedirectToAction("Index");
        }

        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CategoryService(userId);
            return service;
        }
    }
}