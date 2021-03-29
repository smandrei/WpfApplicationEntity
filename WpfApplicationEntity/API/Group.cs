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
        //[Required]
        public virtual ICollection<Student> Students { get; set; }
        public Group() { }
        public Group(string name, int Id = 0)
        {
            this.Name = name;
            this.Id = Id;
        }
    }
}
