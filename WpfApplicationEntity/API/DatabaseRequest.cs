using System.Collections.Generic;
using System.Linq;

namespace WFAEntity.API
{
    static class DatabaseRequest
    {
        public struct NewStudent
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic  { get; set; }
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
        static DatabaseRequest()
        {
        }
        public static bool IsUser(MyDBContext objectMyDBContext, string login, string password)
        {
            var tmp = (
                from tmpUser in objectMyDBContext.Users.ToList<User>()
                where tmpUser.Login.CompareTo(login) == 0 && tmpUser.Password.CompareTo(password) == 0
                select tmpUser
                      ).ToList();
            if (tmp.Count == 1)
                return true;
            return false;
        }
        public static IEnumerable<NewStudent> GetStudentsWithGroups(MyDBContext objectMyDBContext)
        {
            return (
                from tmpStudent in objectMyDBContext.Students.ToList<Student>()
                from tmpGroup in objectMyDBContext.Groups.ToList<Group>()
                where tmpStudent.Id == tmpGroup.Id
                select (
                new NewStudent(tmpStudent.Id, tmpStudent.Name, tmpStudent.Surname, tmpStudent.Patronymic, tmpGroup.Id, tmpGroup.Name)
                )
                       ).ToList();
        }
        public static IEnumerable<Group> GetGroups(MyDBContext objectMyDBContext)
        {
            return objectMyDBContext.Groups.ToList();
        }
    }
}

