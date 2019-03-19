using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Foolproof;

namespace ToDoLista.Areas.Zadania.Models
{
    public class Task
    {
        public int TaskID { get; set; }

        [StringLength(255)]
        [Display(Name = "Czynność")]
        public string Action { get; set; }

        [StringLength(255)]
        [Display(Name = "Temat")]
        public string Topic { get; set; }

        [Display(Name = "Data rozpoczęcia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }

        [GreaterThanOrEqualTo("Start")]
        [Display(Name = "Data zakończenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }

        public string Status { get; set; }

        [Display(Name = "Priorytet")]
        public string Priority { get; set; }

        [Range(0,100)]
        [Display(Name = "% zakończenia")]
        public int Progress { get; set; }

        [StringLength(255)]
        [Display(Name = "Opis")]
        public string Description { get; set; }
    }

    public class TaskDBContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
    }
}