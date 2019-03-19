using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoLista.Areas.Zadania.Models;
using PagedList;
using ToDoLista.Services.Zadania;

namespace ToDoLista.Areas.Zadania.Controllers
{
    public class ZadanieController : Controller
    {
        private IZadanieService _zadanieService;
        public ZadanieController(IZadanieService zadanieService)
        {
            _zadanieService = zadanieService;
        }


        private static string messageForEdit = "";

       
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? pageSizeParm, int? currentPageSize, bool tiles = false)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.MessageForEdit = messageForEdit;
            messageForEdit = null; 
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (pageSizeParm != null)
            {
                ViewBag.CurrentPageSize = pageSizeParm;
                page = 1;
            }
            else
            {
                ViewBag.CurrentPageSize = currentPageSize;
                pageSizeParm = currentPageSize;
            }

            var tasks = _zadanieService.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(s => s.Action.Contains(searchString));
            }
            
            switch (sortOrder)
            {
                case "action":
                    tasks = tasks.OrderBy(s => s.Action);
                    break;
                case "topic":
                    tasks = tasks.OrderBy(s => s.Topic);
                    break;
                case "start":
                    tasks = tasks.OrderBy(s => s.Start);
                    break;
                case "end":
                    tasks = tasks.OrderBy(s => s.End);
                    break;
                case "status":
                    tasks = tasks.OrderBy(s => s.Status);
                    break;
                case "priority":
                    tasks = tasks.OrderBy(s => s.Priority);
                    break;
                case "progress":
                    tasks = tasks.OrderBy(s => s.Progress);
                    break;
                default:
                    tasks = tasks.OrderBy(s => s.TaskID);
                    break;
            }
            int pageSize = (pageSizeParm ?? 5);
            int pageNumber = (page ?? 1);

            if (tiles == true)
            {
                return View("Tiles", tasks.ToPagedList(pageNumber, pageSize));
            }
            return View(tasks.ToPagedList(pageNumber, pageSize));
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            int ID = id.Value;
            Task task = _zadanieService.Get(ID);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskID,Action,Topic,Start,End,Status,Priority,Progress,Description")] Task task)
        {
            if (ModelState.IsValid)
            {
                _zadanieService.Add(task);
                return RedirectToAction("Index");
            }

            return View(task);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                messageForEdit = "Zaznacz tylko jeden checkbox";
                return RedirectToAction("Index");
            }
            int ID = id.Value;
            Task task = _zadanieService.Get(ID);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskID,Action,Topic,Start,End,Status,Priority,Progress,Description")] Task task)
        {
            if (ModelState.IsValid)
            {
                _zadanieService.Update(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

    
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int ID = id.Value;
            Task task = _zadanieService.Get(ID);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _zadanieService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
