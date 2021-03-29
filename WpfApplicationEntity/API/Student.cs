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
        public int IdGroup { get; set; }
        [Required]
        public virtual Group Group { get; set; }
        public Student() { }
        public Student(string name, string surname, string patronymic, Group group, int IdGroup = 0, int Id = 0)
        {
            this.Surname = surname;
            this.Name = name;
            this.Patronymic = patronymic;
            this.Group = group;
            this.IdGroup = IdGroup;
            this.Id = Id;
        }
    }
}
