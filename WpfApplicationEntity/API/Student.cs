using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        //[Required]
        public virtual Group Group { get; set; }
        public Student() { }
        /// <summary>
        /// Студент
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="patronymic">Отчество</param>
        /// <param name="group">Группа</param>
        /// <param name="IdGroup"></param>
        /// <param name="Id"></param>
        public Student(string name, string surname, string patronymic, Group group, int IdGroup = 0, int Id = 0)
        {
            this.Surname = surname;
            this.Name = name;
            this.Patronymic = patronymic;
            //this.Group = group;
            this.GroupId = group.Id;
            this.Id = Id;
        }
    }
}
