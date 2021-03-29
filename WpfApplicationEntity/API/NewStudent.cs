using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFAEntity.API
{
    class NewStudent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int IdGroup { get; set; }
        public string Group { get; set; }
        public NewStudent(int Id, string Name, string Surname, string Patronymic, int IdGroup, string Group)
        {
            this.Id = Id;
            this.Name = Name;
            this.Surname = Surname;
            this.Patronymic = Patronymic;
            this.IdGroup = IdGroup;
            this.Group = Group;
        }
    }
}
