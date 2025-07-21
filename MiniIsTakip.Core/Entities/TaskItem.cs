using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniIsTakip.Core.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Açıklama zorunludur.")]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;

        [Required(ErrorMessage = "Çalışan seçimi zorunludur.")]
        public int AssignedPersonId { get; set; }

        /*public Person AssignedPerson { get; set; }*/
        public Person? AssignedPerson { get; set; }






    }
}
