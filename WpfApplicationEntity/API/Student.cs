using System.ComponentModel.DataAnnotations;

namespace WFAEntity.API
{
    class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Patronymic { get; set; }
        [Required]
        public virtual Group Group { get; set; }
    }
}
