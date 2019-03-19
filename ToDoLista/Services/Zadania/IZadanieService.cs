using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoLista.Areas.Zadania.Models;

namespace ToDoLista.Services.Zadania
{
    public interface IZadanieService
    {
        IEnumerable<Task> GetAll();

        Task Get(int id);

        Task Add(Task item);

        bool Update(Task item);

        bool Delete(int id);
    }
}
