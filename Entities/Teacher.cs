namespace UniversityProject.Entities
{
    public class Teacher
    {
        public  Guid Id { get; set; }

        public  User User { get; set; }
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public int  TeacherCode { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
       
    }
}
