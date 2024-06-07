using Syncfusion.Blazor.Gantt;

namespace EducationSchedule.Components.Pages
{
    public partial class EducationScheduleManagement
    {
        private SfGantt<Course>? gantt;
        private List<Course>? examCollection { get; set; }
        private List<SegmentModel>? segmentCollection { get; set; }
        private DateTime oddSemSupplimentExamStart = new DateTime(2023, 07, 24);
        private DateTime oddSemSupplimentExamEnd = new DateTime(2023, 07, 29);
        private DateTime evenSemSupplimentExamStart = new DateTime(2024, 01, 08);
        private DateTime evenSemSupplimentExamEnd = new DateTime(2024, 01, 13);

        protected override async Task OnInitializedAsync()
        {
            examCollection = GetCourse();
            foreach (Course course in examCollection)
            {
                if (course.Duration != null && course.Department != "Partical Exams" && course.Department != "Theoretical Exams")
                {
                    DateTime startDate = course.StartDate;
                    int duration = int.Parse(course.Duration);
                    DateTime endDate = startDate.AddDays(duration);
                    DateTime indicatorDate;
                    if (course.SubjectName == "Engineering Drawings" || course.SubjectName == "Computer Organization and Architecture Lab")
                    {
                        indicatorDate = endDate.AddDays(6);
                    }
                    else if (endDate.DayOfWeek == DayOfWeek.Wednesday || endDate.DayOfWeek == DayOfWeek.Thursday)
                    {
                        indicatorDate = endDate.AddDays(3);
                    }
                    else
                    {
                        indicatorDate = endDate.AddDays(5);
                    }


                    course.Indicators = new List<GanttIndicator>
                {
                    new GanttIndicator
                    {
                        IconClass = "e-btn-icon e-bookmark e-icons e-icon-left e-gantt e-bookmark::before",
                        Date = indicatorDate,
                        Tooltip = $"Professor : {course.Professor}"
                    }
                };
                }
            }
            segmentCollection = GetSegmentCollection();
            await Task.CompletedTask;
        }
        public class SegmentModel
        {
            public int Id { get; set; }
            public int CourseId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
        public class Holiday
        {
            public int Id { get; set; }
            public string? HolidayName { get; set; }
            public DateTime Date { get; set; }
        }
        public static List<Holiday> HolidayCollection = new List<Holiday>()
        {
            new Holiday(){ Id = 1, HolidayName = "Muharram", Date = new DateTime(2023, 07, 29) },
            new Holiday(){ Id = 2, HolidayName = "Independency Day", Date = new DateTime(2023, 08, 15) },
            new Holiday(){ Id = 3, HolidayName = "Gandhi's Birthday", Date = new DateTime(2023, 10, 02) },
            new Holiday(){ Id = 4, HolidayName = "Dussehra", Date = new DateTime(2023, 10, 24) },
            new Holiday(){ Id = 5, HolidayName = "Deepavali", Date = new DateTime(2023, 11, 12) },
            new Holiday(){ Id = 6, HolidayName = "Guru Nanak's Birthday", Date = new DateTime(2023, 11, 27) },
            new Holiday(){ Id = 7, HolidayName = "Christmas", Date = new DateTime(2023, 12, 25) },
            new Holiday(){ Id = 8, HolidayName = "New Year's Day", Date = new DateTime(2024, 01, 01) },
            new Holiday(){ Id = 9, HolidayName = "Pongal", Date = new DateTime(2024, 01, 15) },
            new Holiday(){ Id = 10, HolidayName = "Republic Day", Date = new DateTime(2024, 01, 26) },
            new Holiday(){ Id = 11, HolidayName = "Maha Shivaratri", Date = new DateTime(2024, 03, 08) },
            new Holiday(){ Id = 12, HolidayName = "Good Friday", Date = new DateTime(2024, 03, 29) },
            new Holiday(){ Id = 13, HolidayName = "Ramzan", Date = new DateTime(2024, 04, 11) },
            new Holiday(){ Id = 14, HolidayName = "Buddha Purnima", Date = new DateTime(2024, 05, 23) },
            new Holiday(){ Id = 15, HolidayName = "Bakrid", Date = new DateTime(2024, 06, 17) }
        };


        public class Course
        {
            public int CourseId { get; set; }
            public string? Department { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string? Duration { get; set; }
            public string? SubjectCode { get; set; }
            public string? SubjectName { get; set; }
            public string? Batch { get; set; }
            public int? ParentId { get; set; }
            public int PortionCompletion { get; set; }
            public string? Professor { get; set; }
            public string? Predecessor { get; set; }
            public List<GanttIndicator>? Indicators { get; set; }
        }
        private static List<Course> GetCourse()
        {
            List<Course> courses = new List<Course>()
            {
                new Course(){ CourseId = 1, Department = "First Year" , Batch = "2023-2027",},
                new Course(){ CourseId = 2, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "MA1101", SubjectName = "Functions of Several Variables", Professor = "Arindama Singh", PortionCompletion = 100, StartDate = new DateTime(2023, 06, 05), Duration = "74", ParentId = 1 },
                new Course(){ CourseId = 3, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "PH1010", SubjectName = "Physics. I", Professor = "L.Sriramkumar", PortionCompletion = 100, StartDate = new DateTime(2023, 06, 12), Duration = "74", ParentId = 1 },
                new Course(){ CourseId = 4, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "PH1030", SubjectName = "Physics Lab.1", Professor = "L.Sriramkumar", PortionCompletion = 100, StartDate = new DateTime(2023, 06, 12), Duration = "74", ParentId = 1 },
                new Course(){ CourseId = 5, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "AM1100", SubjectName = "Engineering Mechanics", Professor = "Swathi Sudhakar", PortionCompletion = 100, StartDate = new DateTime(2023, 06, 19), Duration = "74", ParentId = 1 },
                new Course(){ CourseId = 6, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "CY1001", SubjectName = "Chemistry 1", Professor = "Arti Dua", PortionCompletion = 100, StartDate = new DateTime(2023, 06, 26), Duration = "73", ParentId = 1 },
                new Course(){ CourseId = 7, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "CS1111", SubjectName = "Problem Solving using Computers", Professor = "Rupesh Nasre", PortionCompletion = 100, StartDate = new DateTime(2023, 07, 03), Duration = "73", ParentId = 1 },
                new Course(){ CourseId = 8, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "GN1101", SubjectName = "Life Skills I", Professor = "Sivakumar .M.S", PortionCompletion = 100, StartDate = new DateTime(2023, 07, 10), Duration = "73", ParentId = 1 },
                new Course(){ CourseId = 9, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "ID1200", SubjectName = "Ecology and Environment", Professor = "Dr.Abhijit P.Deshpande", PortionCompletion = 100, StartDate = new DateTime(2023, 07, 17), Duration = "73", ParentId = 1 },

                new Course(){CourseId = 10, Department = "Partical Exams", StartDate = new DateTime(2023,11,01), Duration="3", PortionCompletion = 100, ParentId = 1, Predecessor = "4" },
                new Course(){CourseId = 11, Department = "Theoretical Exams", StartDate = new DateTime(2023,11,06), Duration="15", PortionCompletion = 100, ParentId = 1, Predecessor = "2,3,5,6,7,8,9"},

                new Course(){ CourseId = 12, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "MA1102", SubjectName = "Series and Matrices", Professor = "Arindama Singh", StartDate = new DateTime(2024, 01, 08), Duration = "72", PortionCompletion = 90, ParentId = 1 },
                new Course(){ CourseId = 13, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "PH1020", SubjectName = "Physics. II", Professor = "L.Sriramkumar", StartDate = new DateTime(2024, 01, 15), Duration = "75", PortionCompletion = 78, ParentId = 1 },
                new Course(){ CourseId = 14, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "CS1200", SubjectName = "Discrete Mathematics for CS", Professor = "Nishad Kothari" , StartDate = new DateTime(2024, 01, 22), Duration = "72", PortionCompletion = 74, ParentId = 1 },
                new Course(){ CourseId = 15, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "ME1480", SubjectName = "Engineering Drawings", Professor = "Balakrishna Rao", StartDate = new DateTime(2024, 01, 29), Duration = "72", PortionCompletion = 64, ParentId = 1 },
                new Course(){ CourseId = 16, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "CY1002", SubjectName = "Chemistry Lab", Professor = "Arnab Rit", StartDate = new DateTime(2024, 02, 05), Duration = "72", PortionCompletion = 57, ParentId = 1 },
                new Course(){ CourseId = 17, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "EE1100", SubjectName = "Basic Electrical Engineering", Professor = "Jayaraj", StartDate = new DateTime(2024, 02, 12), Duration = "72", PortionCompletion = 46, ParentId = 1 },
                new Course(){ CourseId = 18, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "GN1102", SubjectName = "Life Skills II",  Professor = "Sivakumar.M.S", StartDate = new DateTime(2024, 02, 19), Duration = "72", PortionCompletion = 36, ParentId = 1 },

                new Course(){CourseId = 19, Department = "Partical Exams", StartDate = new DateTime(2024,05,08), Duration="3", PortionCompletion = 0, ParentId = 1, Predecessor = "16" },
                new Course(){CourseId = 20, Department = "Theoretical Exams", StartDate = new DateTime(2024,05,13), Duration="15", PortionCompletion = 0, ParentId = 1, Predecessor = "12,13,14,15,17,18"},

                new Course(){ CourseId = 21, Department = "Second Year", Batch = "2022-2026", },
                new Course(){ CourseId = 22, Department = "Semester 3" , Batch = "2022-2026", SubjectCode = "MA2130", SubjectName = "Basic Graph Theory",  Professor = "Arindama Singh", PortionCompletion = 100, StartDate = new DateTime(2023, 06, 05), Duration = "74", ParentId = 21 },
                new Course(){ CourseId = 22, Department = "Semester 3" , Batch = "2022-2026", SubjectCode = "HS4060", SubjectName = "Humanities Elective 1", Professor = "C.Balaji", PortionCompletion = 100,  StartDate = new DateTime(2023, 06, 12), Duration = "74", ParentId = 21 },
                new Course(){ CourseId = 23, Department = "Semester 3" , Batch = "2022-2026", SubjectCode = "CS2700", SubjectName = "Programming and Data Structures", Professor = "Rupesh Nasre", StartDate = new DateTime(2023, 06, 19), Duration = "74", PortionCompletion = 100, ParentId = 21 },
                new Course(){ CourseId = 24, Department = "Semester 3" , Batch = "2022-2026", SubjectCode = "CS2710", SubjectName = "Programming and Data Structures Lab", Professor = "Meghana Nasre", StartDate = new DateTime(2023, 06, 26), Duration = "73", PortionCompletion = 100, ParentId = 21 },
                new Course(){ CourseId = 25, Department = "Semester 3" , Batch = "2022-2026", SubjectCode = "CS2300", SubjectName = "Foundations of Computer Systems Design", Professor = "C.Chandra Sekhar", StartDate = new DateTime(2023, 07, 03), Duration = "73", PortionCompletion = 100, ParentId = 21 },
                new Course(){ CourseId = 26, Department = "Semester 3" , Batch = "2022-2026", SubjectCode = "CS2310", SubjectName = "Foundations of Computer Systems Design Lab", Professor = "Madhu Mutyam", StartDate = new DateTime(2023, 07, 03), Duration = "73", PortionCompletion = 100, ParentId = 21 },

                new Course(){CourseId = 27, Department = "Partical Exams", StartDate = new DateTime(2023,11,01), Duration="4", PortionCompletion = 100, ParentId = 21, Predecessor = "24,26" },
                new Course(){CourseId = 28, Department = "Theoretical Exams", StartDate = new DateTime(2023,11,08), Duration="15", PortionCompletion = 100, ParentId = 21, Predecessor = "21,22,23,25"},

                new Course(){ CourseId = 29, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "CS2200", SubjectName = "Languages, Machines, and Computations", Professor = "John Augustine", StartDate = new DateTime(2024, 01, 08), Duration = "72", PortionCompletion = 90, ParentId = 21 },
                new Course(){ CourseId = 30, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "CS2800", SubjectName = "Design and Analysis of Algorithms", Professor = "Shweta Agrawal" , StartDate = new DateTime(2024, 01, 15), Duration = "75", PortionCompletion = 78, ParentId = 21 },
                new Course(){ CourseId = 31, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "CS2600", SubjectName = "Computer Organization and Architecture", Professor = "C.Chandra Sekhar", StartDate = new DateTime(2024, 01, 22), Duration = "72", PortionCompletion = 74, ParentId = 21 },
                new Course(){ CourseId = 32, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "CS2610", SubjectName = "Computer Organization and Architecture Lab", Professor = "C.Chandra Sekhar", StartDate = new DateTime(2024, 01, 29), Duration = "72", PortionCompletion = 64, ParentId = 21 },
                new Course(){ CourseId = 33, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "CS2810", SubjectName = "Object-Oriented Algorithms Implementation and Analysis Lab", Professor = "Kartik Nagar", StartDate = new DateTime(2024, 02, 05), Duration = "72", PortionCompletion = 57, ParentId = 21 },
                new Course(){ CourseId = 34, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "MA2040", SubjectName = "Probability, Stochastic Process and Statistics", Professor = "Sarang Sane",  StartDate = new DateTime(2024, 02, 12), Duration = "72", PortionCompletion = 46, ParentId = 21 },

                new Course(){CourseId = 35, Department = "Partical Exams", StartDate = new DateTime(2024,05,08), Duration="4", PortionCompletion = 0, ParentId = 21, Predecessor = "32,33" },
                new Course(){CourseId = 36, Department = "Theoretical Exams", StartDate = new DateTime(2024,05,15), Duration="15", PortionCompletion = 0, ParentId = 21, Predecessor = "29,30,31,34"},

                new Course(){ CourseId = 37, Department = "Third Year" , Batch = "2021-2025", },
                new Course(){ CourseId = 38, Department = "Semester 5" , Batch = "2021-2025", SubjectCode = "CS3100", SubjectName = "Paradigms of Programming", Professor = "K.C.Sivaramakrishnan", StartDate = new DateTime(2023, 06, 05), Duration = "74", PortionCompletion = 100, ParentId = 37 },
                new Course(){ CourseId = 39, Department = "Semester 5" , Batch = "2021-2025", SubjectCode = "CS3500", SubjectName = "Operating Systems", Professor = "Balaraman Ravindran", StartDate = new DateTime(2023, 06, 12), Duration = "74", PortionCompletion = 100, ParentId = 37 },
                new Course(){ CourseId = 40, Department = "Semester 5" , Batch = "2021-2025", SubjectCode = "CS3300", SubjectName = "Compiler Design", Professor = "Rupesh Nasre", StartDate = new DateTime(2023, 06, 19), Duration = "74", PortionCompletion = 100, ParentId = 37 },

                new Course(){CourseId = 42, Department = "Theoretical Exams", StartDate = new DateTime(2023,11,09), Duration="12", PortionCompletion = 100, ParentId = 37, Predecessor = "38,39,40"},

                new Course(){ CourseId = 43, Department = "Semester 6" , Batch = "2021-2025", SubjectCode = "BT1010", SubjectName = "Life Sciences", Professor= "Himanshu Sinha", StartDate = new DateTime(2024, 01, 08), Duration = "72", PortionCompletion = 90, ParentId = 37 },

                new Course(){CourseId = 44, Department = "Theoretical Exams", StartDate = new DateTime(2024,05,20), Duration="1", PortionCompletion = 0, ParentId = 37, Predecessor = "43"},

                new Course(){ CourseId = 45, Department = "Final Year" , Batch = "2020-2024", },
                new Course(){ CourseId = 46, Department = "Semester 7" , Batch = "2020-2024", SubjectCode = "HS4210", SubjectName = "Literature and values", Professor = "C.Balaji", StartDate = new DateTime(2023, 06, 05), Duration = "74", PortionCompletion = 100, ParentId = 45 },

                new Course(){CourseId = 47, Department = "Theoretical Exams", StartDate = new DateTime(2023,11,22), Duration="1", PortionCompletion = 100, ParentId = 45, Predecessor = "46"},

                new Course(){ CourseId = 48, Department = "Semester 8" , Batch = "2020-2024", SubjectCode = "HS3050", SubjectName = "Professional Ethics", Professor = "Deleep R.Nair", StartDate = new DateTime(2024, 01, 08), Duration = "72", PortionCompletion = 90, ParentId = 45 },
                new Course(){ CourseId = 49, Department = "Semester 8" , Batch = "2020-2024", SubjectCode = "HS3002", SubjectName = "Principle of Economics", Professor = "Muraleedharan VR", StartDate = new DateTime(2024, 01, 15), Duration = "75", PortionCompletion = 78, ParentId = 45 },

                new Course(){CourseId = 50, Department = "Theoretical Exams", StartDate = new DateTime(2024,05,15), Duration="10", PortionCompletion = 0, ParentId = 45, Predecessor = "48,49"},

            };
            return courses;
        }
        private List<SegmentModel> GetSegmentCollection()
        {
            List<SegmentModel> segments = new List<SegmentModel>();
            List<Course> courses = GetCourse();
            var count = 0;
            foreach (var course in courses)
            {
                if (course.Duration != null)
                {
                    DateTime currentStartDate = course.StartDate;
                    DateTime courseEndDate = course.StartDate.AddDays(double.Parse(course.Duration));

                    var minusDay = 1;
                    while (currentStartDate < courseEndDate)
                    {
                        DateTime currentEndDate = currentStartDate.AddDays(1);

                        bool isHoliday = HolidayCollection.Any(h => h.Date.Date == currentStartDate.Date);
                        if (isHoliday)
                        {
                            currentStartDate = currentEndDate;
                            continue;
                        }
                        while (currentEndDate < courseEndDate)
                        {
                            bool isNextDateHoliday = HolidayCollection.Any(h => h.Date.Date.AddDays(-minusDay) == currentEndDate.Date);
                            if (isNextDateHoliday)
                            {
                                count++;
                                minusDay++;
                                segments.Add(new SegmentModel() { Id = count, CourseId = course.CourseId, StartDate = currentStartDate, EndDate = currentEndDate });
                                currentStartDate = currentEndDate;
                                break;
                            }

                            currentEndDate = currentEndDate.AddDays(1);
                        }

                        if (currentEndDate >= courseEndDate)
                        {
                            count++;
                            segments.Add(new SegmentModel() { Id = count, CourseId = course.CourseId, StartDate = currentStartDate, EndDate = courseEndDate });
                            break;
                        }
                    }
                }

            }

            return segments;
        }
    }
}
