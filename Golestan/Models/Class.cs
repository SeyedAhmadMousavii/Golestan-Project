using System.ComponentModel.DataAnnotations;

public class Class
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }

    public int? TeacherId { get; set; }
    public Teacher Teacher { get; set; }

    [DataType(DataType.Time)]
    public TimeSpan StartTime { get; set; }

    [DataType(DataType.Time)]
    public TimeSpan EndTime { get; set; }

    public string Location { get; set; } // دانشکده مربوطه

    public ICollection<Student_Class> Student_Classes { get; set; }
}