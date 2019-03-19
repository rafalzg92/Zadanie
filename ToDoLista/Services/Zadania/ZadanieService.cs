using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoLista.Areas.Zadania.Models;
using System.Data.Entity;

namespace ToDoLista.Services.Zadania
{
    public class ZadanieService : IZadanieService
    {
        private TaskDBContext db = new TaskDBContext();

        public IEnumerable<Task> GetAll()
        {
            var tasks = from s in db.Tasks
                        select s;
            return tasks;
        }

        public Task Get(int id)
        {
            Task task = db.Tasks.Find(id);
            return task;
        }

        public Task Add(Task task)
        {
            db.Tasks.Add(task);
            db.SaveChanges();
            return task;
        }

        public bool Update(Task task)
        {
            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return true;
        }
    }
}