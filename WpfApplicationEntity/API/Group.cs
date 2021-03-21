using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WFAEntity.API
{
    class Group
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
