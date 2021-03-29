using System.Collections.Generic;
using System.Linq;

namespace WFAEntity.API
{
    static class DatabaseRequest
    {
        /*public struct NewStudent
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
        }*/
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
        public static Group GetGroupById(MyDBContext objectMyDBContext, int ID)
        {
            return (from tempGroup in objectMyDBContext.Groups.ToList<Group>()
                   where tempGroup.Id == ID
                   select tempGroup).SingleOrDefault();
        }
        public static Student GetStudentById(MyDBContext objectMyDBContext, int ID)
        {
            return (from tempStudent in objectMyDBContext.Students.ToList<Student>()
                    where tempStudent.Id == ID
                    select tempStudent).First();
        }
        public static void CreateDefaultDataBase()
        {
            /*Group objectGroup2 = new Group();
            objectGroup1.Name = "G2";
            Group objectGroup3 = new Group();
            objectGroup1.Name = "G3";
            Group objectGroup4 = new Group();
            objectGroup1.Name = "G4";
            Group objectGroup5 = new Group();
            objectGroup1.Name = "G5";*/
            using (WFAEntity.API.MyDBContext objectMyDBContext = new WFAEntity.API.MyDBContext())
            {
                Group objectGroup1 = new Group("G 1");
                Group objectGroup2 = new Group("G 2");
                Group objectGroup3 = new Group("G 3");
                Group objectGroup4 = new Group("G 4");
                Group objectGroup5 = new Group("G 5");
                objectMyDBContext.Groups.Add(objectGroup1);
                objectMyDBContext.SaveChanges();
                objectMyDBContext.Groups.Add(objectGroup2);
                objectMyDBContext.SaveChanges();
                objectMyDBContext.Groups.Add(objectGroup3);
                objectMyDBContext.SaveChanges();
                objectMyDBContext.Groups.Add(objectGroup4);
                objectMyDBContext.SaveChanges();
                objectMyDBContext.Groups.Add(objectGroup5);
                objectMyDBContext.SaveChanges();

                Student objectStudent1 = new Student("S1", "N1", "P1", objectGroup1, 1);
                Student objectStudent2 = new Student("S2", "N2", "P2", objectGroup2, 2);
                Student objectStudent3 = new Student("S3", "N3", "P3", objectGroup3, 3);
                Student objectStudent4 = new Student("S4", "N4", "P4", objectGroup4, 4);
                Student objectStudent5 = new Student("S5", "N5", "P5", objectGroup5, 5);
                objectMyDBContext.Students.Add(objectStudent1);
                objectMyDBContext.SaveChanges();
                objectMyDBContext.Students.Add(objectStudent2);
                objectMyDBContext.SaveChanges();
                objectMyDBContext.Students.Add(objectStudent3);
                objectMyDBContext.SaveChanges();
                objectMyDBContext.Students.Add(objectStudent4);
                objectMyDBContext.SaveChanges();
                objectMyDBContext.Students.Add(objectStudent5);
                objectMyDBContext.SaveChanges();
            }
        }
        public static void ChangeDefaultDataBase()
        {
            int w = 0;
            using (WFAEntity.API.MyDBContext objectMyDBContext = new WFAEntity.API.MyDBContext())
            {
                Student objectStudent5 = WFAEntity.API.DatabaseRequest.GetStudentById(objectMyDBContext, 2);
                Group gr = WFAEntity.API.DatabaseRequest.GetGroupById(objectMyDBContext, 2);
                Group newGroup = new Group("G3*******", 2);
                Student newStudent = new Student(objectStudent5.Name, objectStudent5.Surname, objectStudent5.Patronymic, newGroup, 2, 2);
                objectMyDBContext.Entry(objectStudent5).CurrentValues.SetValues(newStudent);
                objectMyDBContext.Entry(gr).CurrentValues.SetValues(newGroup);
                objectMyDBContext.SaveChanges();
            }
        }
    }
}

