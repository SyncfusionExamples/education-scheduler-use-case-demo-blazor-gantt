using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Gantt;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Diagram;

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
        private string _statusStyleColor = string.Empty;
        private string _statusContentstyleColor = string.Empty;
        public string _subjectCode = string.Empty;
        public string _academicPeriod = string.Empty;
        public string _academicSemester = string.Empty;
        public string _professorName = string.Empty;
        private string _professorNameTooltip = string.Empty;
        public string _groupBy = "Default";
        public string[] _groupByName = new[] { "Default", "Professor", "Exams"};
        public string[] airlines { get; set; } = new[] { "First Year", "Second Year", "Third Year", "Final Year" };
        public string[] startPlaces { get; set; } = new[] { "Semester 1", "Semester 2", "Semester 3", "Semester 4", "Semester 5", "Semester 6", "Semester 7", "Semester 8" };
        public string[] professorNames { get; set; } = new[] 
        {
            "Arindama Singh",
            "Arti Dua",
            "Balaraman Ravindran",
            "Balakrishna Rao",
            "C.Balaji",
            "C.Chandra Sekhar",
            "Deleep R.Nair",
            "Dr.Abhijit P.Deshpande",
            "Himanshu Sinha",
            "Jayaraj",
            "John Augustine",
            "K.C.Sivaramakrishnan",
            "Kartik Nagar",
            "L.Sriramkumar",
            "Madhu Mutyam",
            "Meghana Nasre",
            "Muraleedharan VR",
            "Nishad Kothari",
            "Rupesh Nasre",
            "Sarang Sane",
            "Shweta Agrawal",
            "Sivakumar.M.S",
            "Swathi Sudhakar" 
        };
        private Query _query = new();
        private bool _isBatchVisible = true;
        private bool _isProfessorVisible = true;
        private bool _isPortionCompletionVisible = true;
        private bool _isDefaultbtn = true;
        private bool _isExamsbtn = false;
        private bool _isProfessorbtn = false;
        private string buttonColor = "red";
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
            segmentCollection = GetSegmentCollection(examCollection);
            await Task.CompletedTask;
        }
        public async Task PrepareProfessorCollection()
        {
            examCollection = GetProfessorCollection();
            segmentCollection = GetSegmentCollection(examCollection);
            _isBatchVisible = true;
            _isProfessorVisible = false;
            _isPortionCompletionVisible = true;
            _isDefaultbtn = false;
            _isProfessorbtn = true;
            _isExamsbtn = false;
            buttonColor = "#009688";
            await Task.CompletedTask;
        }

        public async Task ExamCollection()
        {
            examCollection = GetExamCollection();
            segmentCollection = GetSegmentCollection(examCollection);
            _isBatchVisible = false;
            _isProfessorVisible = false;
            _isPortionCompletionVisible = false;
            _isDefaultbtn = false;
            _isProfessorbtn = false;
            _isExamsbtn = true;
            buttonColor = "#009688";
            await Task.CompletedTask;
        }

        public async Task DefaultCollection()
        {
            examCollection = GetCourse();
            segmentCollection = GetSegmentCollection(examCollection);
            _isBatchVisible = true;
            _isProfessorVisible = true;
            _isPortionCompletionVisible = true;
            _isDefaultbtn = true;
            _isProfessorbtn = false;
            _isExamsbtn = false;
            buttonColor = "red";
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

        public async Task SearchHandler(string value)
        {
            await gantt.SearchAsync(value);
            await Task.CompletedTask;
        }

        public async Task ValueChangeHandler(ChangedEventArgs args)
        {
            if (args.PreviousValue is null)
            {
                return;
            }
            await SearchHandler(args.Value);
        }

        public void SubjectCodeHandler(string value)
        {
            _subjectCode = value;
        }
        
        public void AcademicPeriodHandler(string value)
        {
            _academicPeriod = value;
        }

        public void AcademicSemesterHandler(string value)
        {
            _academicSemester = value;
        }

        public void ProfessorNameHandler(string value)
        {
            _professorName = value;
        }

        public async void GroupByHandler(string value)
        {
            _groupBy = value;
            switch (value)
            {
                case "Default":
                    await DefaultCollection();
                    break;
                case "Professor":
                    await PrepareProfessorCollection(); 
                    break;
                case "Exams":
                    await ExamCollection();
                    break;
            }
        }
        public async Task FilterHandler(MouseEventArgs args)
        {
            List<WhereFilter> predicateList = new();
            var isProfessor = false;
            if (!string.IsNullOrEmpty(_subjectCode))
            {
                predicateList.Add(new WhereFilter()
                {
                    Field = "SubjectCode",
                    Operator = "contains",
                    Condition = "and",
                    value = _subjectCode,
                    IgnoreCase = true,
                });
            }
            if (!string.IsNullOrEmpty(_professorName))
            {
                isProfessor = true;
                predicateList.Add(new WhereFilter()
                {
                    Field = "Professor",
                    Operator = "contains",
                    Condition = "and",
                    value = _professorName,
                    IgnoreCase = true,
                });
            }

            if (!string.IsNullOrEmpty(_academicPeriod))
            {
                predicateList.Add(new WhereFilter()
                {
                    Field = "Department",
                    Operator = "contains",
                    Condition = "and",
                    value = _academicPeriod,
                    IgnoreCase = true,
                    IsComplex = isProfessor
                });
            }

            if (!string.IsNullOrEmpty(_academicSemester))
            {
                predicateList.Add(new WhereFilter()
                {
                    Field = "Department",
                    Operator = "contains",
                    Condition = "and",
                    value = _academicSemester,
                    IgnoreCase = true,
                    IsComplex = isProfessor

                });
            }
            
            _query = new Query().Where(WhereFilter.Or(predicateList));
            await Task.CompletedTask;
        }
        public async Task ClearFilterHandler(MouseEventArgs args)
        {
            _subjectCode = string.Empty;
            _academicSemester = string.Empty;
            _professorName = string.Empty;
            _academicPeriod = string.Empty;
            _query = new Query();
            await Task.CompletedTask;
        }

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
            public int Coverage { get; set; }
            public string? Professor { get; set; }
            public string? Predecessor { get; set; }
            public List<GanttIndicator>? Indicators { get; set; }
        }

        private static List<Course> GetCourse()
        {
            List<Course> courses = new List<Course>()
            {
                new Course(){ CourseId = 1, Department = "First Year" , Batch = "2023-2027",},
                new Course(){ CourseId = 2, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "MA1101", SubjectName = "Functions of Several Variables", Professor = "Arindama Singh", Coverage = 100, StartDate = new DateTime(2023, 06, 05), Duration = "74", ParentId = 1 },
                new Course(){ CourseId = 3, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "PH1010", SubjectName = "Physics. I", Professor = "L.Sriramkumar", Coverage = 100, StartDate = new DateTime(2023, 06, 12), Duration = "74", ParentId = 1 },
                new Course(){ CourseId = 4, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "PH1030", SubjectName = "Physics Lab.1", Professor = "L.Sriramkumar", Coverage = 100, StartDate = new DateTime(2023, 06, 12), Duration = "74", ParentId = 1 },
                new Course(){ CourseId = 5, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "AM1100", SubjectName = "Engineering Mechanics", Professor = "Swathi Sudhakar", Coverage = 100, StartDate = new DateTime(2023, 06, 19), Duration = "74", ParentId = 1 },
                new Course(){ CourseId = 6, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "CY1001", SubjectName = "Chemistry 1", Professor = "Arti Dua", Coverage = 100, StartDate = new DateTime(2023, 06, 26), Duration = "73", ParentId = 1 },
                new Course(){ CourseId = 7, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "CS1111", SubjectName = "Problem Solving using Computers", Professor = "Rupesh Nasre", Coverage = 100, StartDate = new DateTime(2023, 07, 03), Duration = "73", ParentId = 1 },
                new Course(){ CourseId = 8, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "GN1101", SubjectName = "Life Skills I", Professor = "Sivakumar .M.S", Coverage = 100, StartDate = new DateTime(2023, 07, 10), Duration = "73", ParentId = 1 },
                new Course(){ CourseId = 9, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "ID1200", SubjectName = "Ecology and Environment", Professor = "Dr.Abhijit P.Deshpande", Coverage = 100, StartDate = new DateTime(2023, 07, 17), Duration = "73", ParentId = 1 },

                new Course(){CourseId = 10, Department = "Partical Exams", StartDate = new DateTime(2023,11,01), Duration="3", Coverage = 100, ParentId = 1, Predecessor = "4" },
                new Course(){CourseId = 11, Department = "Theoretical Exams", StartDate = new DateTime(2023,11,06), Duration="15", Coverage = 100, ParentId = 1, Predecessor = "2,3,5,6,7,8,9"},

                new Course(){ CourseId = 12, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "MA1102", SubjectName = "Series and Matrices", Professor = "Arindama Singh", StartDate = new DateTime(2024, 01, 08), Duration = "72", Coverage = 90, ParentId = 1 },
                new Course(){ CourseId = 13, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "PH1020", SubjectName = "Physics. II", Professor = "L.Sriramkumar", StartDate = new DateTime(2024, 01, 15), Duration = "75", Coverage = 78, ParentId = 1 },
                new Course(){ CourseId = 14, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "CS1200", SubjectName = "Discrete Mathematics for CS", Professor = "Nishad Kothari" , StartDate = new DateTime(2024, 01, 22), Duration = "72", Coverage = 74, ParentId = 1 },
                new Course(){ CourseId = 15, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "ME1480", SubjectName = "Engineering Drawings", Professor = "Balakrishna Rao", StartDate = new DateTime(2024, 01, 29), Duration = "72", Coverage = 64, ParentId = 1 },
                new Course(){ CourseId = 16, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "CY1002", SubjectName = "Chemistry Lab", Professor = "Arnab Rit", StartDate = new DateTime(2024, 02, 05), Duration = "72", Coverage = 57, ParentId = 1 },
                new Course(){ CourseId = 17, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "EE1100", SubjectName = "Basic Electrical Engineering", Professor = "Jayaraj", StartDate = new DateTime(2024, 02, 12), Duration = "72", Coverage = 46, ParentId = 1 },
                new Course(){ CourseId = 18, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "GN1102", SubjectName = "Life Skills II",  Professor = "Sivakumar.M.S", StartDate = new DateTime(2024, 02, 19), Duration = "72", Coverage = 36, ParentId = 1 },

                new Course(){CourseId = 19, Department = "Partical Exams", StartDate = new DateTime(2024,05,08), Duration="3", Coverage = 0, ParentId = 1, Predecessor = "16" },
                new Course(){CourseId = 20, Department = "Theoretical Exams", StartDate = new DateTime(2024,05,13), Duration="15", Coverage = 0, ParentId = 1, Predecessor = "12,13,14,15,17,18"},

                new Course(){ CourseId = 21, Department = "Second Year", Batch = "2022-2026", },
                new Course(){ CourseId = 22, Department = "Semester 3" , Batch = "2022-2026", SubjectCode = "MA2130", SubjectName = "Basic Graph Theory",  Professor = "Arindama Singh", Coverage = 100, StartDate = new DateTime(2023, 06, 05), Duration = "74", ParentId = 21 },
                new Course(){ CourseId = 23, Department = "Semester 3" , Batch = "2022-2026", SubjectCode = "HS4060", SubjectName = "Humanities Elective 1", Professor = "C.Balaji", Coverage = 100,  StartDate = new DateTime(2023, 06, 12), Duration = "74", ParentId = 21 },
                new Course(){ CourseId = 24, Department = "Semester 3" , Batch = "2022-2026", SubjectCode = "CS2700", SubjectName = "Programming and Data Structures", Professor = "Rupesh Nasre", StartDate = new DateTime(2023, 06, 19), Duration = "74", Coverage = 100, ParentId = 21 },
                new Course(){ CourseId = 25, Department = "Semester 3" , Batch = "2022-2026", SubjectCode = "CS2710", SubjectName = "Programming and Data Structures Lab", Professor = "Meghana Nasre", StartDate = new DateTime(2023, 06, 26), Duration = "73", Coverage = 100, ParentId = 21 },
                new Course(){ CourseId = 26, Department = "Semester 3" , Batch = "2022-2026", SubjectCode = "CS2300", SubjectName = "Foundations of Computer Systems Design", Professor = "C.Chandra Sekhar", StartDate = new DateTime(2023, 07, 03), Duration = "73", Coverage = 100, ParentId = 21 },
                new Course(){ CourseId = 27, Department = "Semester 3" , Batch = "2022-2026", SubjectCode = "CS2310", SubjectName = "Foundations of Computer Systems Design Lab", Professor = "Madhu Mutyam", StartDate = new DateTime(2023, 07, 03), Duration = "73", Coverage = 100, ParentId = 21 },

                new Course(){CourseId = 28, Department = "Partical Exams", StartDate = new DateTime(2023,11,01), Duration="4", Coverage = 100, ParentId = 21, Predecessor = "24,26" },
                new Course(){CourseId = 29, Department = "Theoretical Exams", StartDate = new DateTime(2023,11,08), Duration="15", Coverage = 100, ParentId = 21, Predecessor = "21,22,23,25"},

                new Course(){ CourseId = 30, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "CS2200", SubjectName = "Languages, Machines, and Computations", Professor = "John Augustine", StartDate = new DateTime(2024, 01, 08), Duration = "72", Coverage = 90, ParentId = 21 },
                new Course(){ CourseId = 31, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "CS2800", SubjectName = "Design and Analysis of Algorithms", Professor = "Shweta Agrawal" , StartDate = new DateTime(2024, 01, 15), Duration = "75", Coverage = 78, ParentId = 21 },
                new Course(){ CourseId = 32, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "CS2600", SubjectName = "Computer Organization and Architecture", Professor = "C.Chandra Sekhar", StartDate = new DateTime(2024, 01, 22), Duration = "72", Coverage = 74, ParentId = 21 },
                new Course(){ CourseId = 33, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "CS2610", SubjectName = "Computer Organization and Architecture Lab", Professor = "C.Chandra Sekhar", StartDate = new DateTime(2024, 01, 29), Duration = "72", Coverage = 64, ParentId = 21 },
                new Course(){ CourseId = 34, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "CS2810", SubjectName = "Object-Oriented Algorithms Implementation and Analysis Lab", Professor = "Kartik Nagar", StartDate = new DateTime(2024, 02, 05), Duration = "72", Coverage = 57, ParentId = 21 },
                new Course(){ CourseId = 35, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "MA2040", SubjectName = "Probability, Stochastic Process and Statistics", Professor = "Sarang Sane",  StartDate = new DateTime(2024, 02, 12), Duration = "72", Coverage = 46, ParentId = 21 },

                new Course(){CourseId = 36, Department = "Partical Exams", StartDate = new DateTime(2024,05,08), Duration="4", Coverage = 0, ParentId = 21, Predecessor = "32,33" },
                new Course(){CourseId = 37, Department = "Theoretical Exams", StartDate = new DateTime(2024,05,15), Duration="15", Coverage = 0, ParentId = 21, Predecessor = "29,30,31,34"},

                new Course(){ CourseId = 38, Department = "Third Year" , Batch = "2021-2025", },
                new Course(){ CourseId = 39, Department = "Semester 5" , Batch = "2021-2025", SubjectCode = "CS3100", SubjectName = "Paradigms of Programming", Professor = "K.C.Sivaramakrishnan", StartDate = new DateTime(2023, 06, 05), Duration = "74", Coverage = 100, ParentId = 38 },
                new Course(){ CourseId = 40, Department = "Semester 5" , Batch = "2021-2025", SubjectCode = "CS3500", SubjectName = "Operating Systems", Professor = "Balaraman Ravindran", StartDate = new DateTime(2023, 06, 12), Duration = "74", Coverage = 100, ParentId = 38 },
                new Course(){ CourseId = 41, Department = "Semester 5" , Batch = "2021-2025", SubjectCode = "CS3300", SubjectName = "Compiler Design", Professor = "Rupesh Nasre", StartDate = new DateTime(2023, 06, 19), Duration = "74", Coverage = 100, ParentId = 38 },

                new Course(){CourseId = 42, Department = "Theoretical Exams", StartDate = new DateTime(2023,11,09), Duration="12", Coverage = 100, ParentId = 38, Predecessor = "38,39,40"},

                new Course(){ CourseId = 43, Department = "Semester 6" , Batch = "2021-2025", SubjectCode = "BT1010", SubjectName = "Life Sciences", Professor= "Himanshu Sinha", StartDate = new DateTime(2024, 01, 08), Duration = "72", Coverage = 90, ParentId = 38 },

                new Course(){CourseId = 44, Department = "Theoretical Exams", StartDate = new DateTime(2024,05,20), Duration="1", Coverage = 0, ParentId = 38, Predecessor = "43"},

                new Course(){ CourseId = 45, Department = "Final Year" , Batch = "2020-2024", },
                new Course(){ CourseId = 46, Department = "Semester 7" , Batch = "2020-2024", SubjectCode = "HS4210", SubjectName = "Literature and values", Professor = "C.Balaji", StartDate = new DateTime(2023, 06, 05), Duration = "74", Coverage = 100, ParentId = 45 },

                new Course(){CourseId = 47, Department = "Theoretical Exams", StartDate = new DateTime(2023,11,22), Duration="1", Coverage = 100, ParentId = 45, Predecessor = "46"},

                new Course(){ CourseId = 48, Department = "Semester 8" , Batch = "2020-2024", SubjectCode = "HS3050", SubjectName = "Professional Ethics", Professor = "Deleep R.Nair", StartDate = new DateTime(2024, 01, 08), Duration = "72", Coverage = 90, ParentId = 45 },
                new Course(){ CourseId = 49, Department = "Semester 8" , Batch = "2020-2024", SubjectCode = "HS3002", SubjectName = "Principle of Economics", Professor = "Muraleedharan VR", StartDate = new DateTime(2024, 01, 15), Duration = "75", Coverage = 78, ParentId = 45 },

                new Course(){CourseId = 50, Department = "Theoretical Exams", StartDate = new DateTime(2024,05,15), Duration="10", Coverage = 0, ParentId = 45, Predecessor = "48,49"},

            };
            return courses;
        }

        private List<Course> GetProfessorCollection()
        {
            var courses = new List<Course>()
            {
                new Course(){ CourseId = 1, Department = "Arindama Singh", Batch = "2023-2027" },
                new Course(){ CourseId = 2, Department = "First Year", Batch = "2023-2027", ParentId = 1 },
                new Course(){ CourseId = 3, Department = "Semester 1", Batch = "2023-2027", SubjectCode = "MA1101", SubjectName = "Functions of Several Variables", Professor = "Arindama Singh", Coverage = 100, StartDate = new DateTime(2023, 06, 05), Duration = "74", ParentId = 2 },
                new Course(){ CourseId = 4, Department = "Semester 2", Batch = "2023-2027", SubjectCode = "MA1102", SubjectName = "Series and Matrices", Professor = "Arindama Singh", StartDate = new DateTime(2024, 01, 08), Duration = "72", Coverage = 90, ParentId = 2 },
                new Course(){ CourseId = 5, Department = "Second Year", Batch = "2022-2026", ParentId = 1 },
                new Course(){ CourseId = 6, Department = "Semester 3", Batch = "2022-2026", SubjectCode = "MA2130", SubjectName = "Basic Graph Theory", Professor = "Arindama Singh", Coverage = 100, StartDate = new DateTime(2023, 06, 05), Duration = "74", ParentId = 5 },

                new Course(){ CourseId = 7, Department = "L.Sriramkumar", Batch = "2023-2027" },
                new Course(){ CourseId = 8, Department = "First Year", Batch = "2023-2027", ParentId = 7 },
                new Course(){ CourseId = 9, Department = "Semester 1", Batch = "2023-2027", SubjectCode = "PH1010", SubjectName = "Physics I", Professor = "L.Sriramkumar", Coverage = 100, StartDate = new DateTime(2023, 06, 12), Duration = "74", ParentId = 8 },
                new Course(){ CourseId = 10, Department = "Semester 1", Batch = "2023-2027", SubjectCode = "PH1030", SubjectName = "Physics Lab I", Professor = "L.Sriramkumar", Coverage = 100, StartDate = new DateTime(2023, 06, 12), Duration = "74", ParentId = 8 },
                new Course(){ CourseId = 11, Department = "Semester 2", Batch = "2023-2027", SubjectCode = "PH1020", SubjectName = "Physics II", Professor = "L.Sriramkumar", StartDate = new DateTime(2024, 01, 15), Duration = "75", Coverage = 78, ParentId = 8 },

                new Course(){ CourseId = 12, Department = "Arti Dua", Batch = "2023-2027" },
                new Course(){ CourseId = 13, Department = "First Year", Batch = "2023-2027", ParentId = 12 },
                new Course(){ CourseId = 14, Department = "Semester 1", Batch = "2023-2027", SubjectCode = "CY1001", SubjectName = "Chemistry 1", Professor = "Arti Dua", Coverage = 100, StartDate = new DateTime(2023, 06, 26), Duration = "73", ParentId = 13 },

                new Course(){ CourseId = 15, Department = "Swathi Sudhakar", Batch = "2023-2027" },
                new Course(){ CourseId = 16, Department = "First Year", Batch = "2023-2027", ParentId = 15 },
                new Course(){ CourseId = 17, Department = "Semester 1", Batch = "2023-2027", SubjectCode = "AM1100", SubjectName = "Engineering Mechanics", Professor = "Swathi Sudhakar", Coverage = 100, StartDate = new DateTime(2023, 06, 19), Duration = "74", ParentId = 16 },

                new Course(){ CourseId = 18, Department = "Rupesh Nasre", Batch = "2023-2027" },
                new Course(){ CourseId = 19, Department = "First Year", Batch = "2023-2027", ParentId = 18 },
                new Course(){ CourseId = 20, Department = "Semester 1", Batch = "2023-2027", SubjectCode = "CS1111", SubjectName = "Problem Solving using Computers", Professor = "Rupesh Nasre", Coverage = 100, StartDate = new DateTime(2023, 07, 03), Duration = "73", ParentId = 19 },
                new Course(){ CourseId = 21, Department = "Second Year", Batch = "2022-2026", ParentId = 18 },
                new Course(){ CourseId = 22, Department = "Semester 3", Batch = "2022-2026", SubjectCode = "CS2700", SubjectName = "Programming and Data Structures", Professor = "Rupesh Nasre", StartDate = new DateTime(2023, 06, 19), Duration = "74", Coverage = 100, ParentId = 21 },
                new Course(){ CourseId = 23, Department = "Third Year", Batch = "2021-2025", ParentId = 18 },
                new Course(){ CourseId = 24, Department = "Semester 5", Batch = "2021-2025", SubjectCode = "CS3300", SubjectName = "Compiler Design", Professor = "Rupesh Nasre", StartDate = new DateTime(2023, 06, 19), Duration = "74", Coverage = 100, ParentId = 23 },

                new Course(){ CourseId = 25, Department = "C.Balaji", Batch = "2022-2026" },
                new Course(){ CourseId = 26, Department = "Second Year", Batch = "2022-2026", ParentId = 25 },
                new Course(){ CourseId = 27, Department = "Semester 3", Batch = "2022-2026", SubjectCode = "HS4060", SubjectName = "Humanities Elective 1", Professor = "C.Balaji", Coverage = 100, StartDate = new DateTime(2023, 06, 12), Duration = "74", ParentId = 26 },
                new Course(){ CourseId = 28, Department = "Final Year", Batch = "2020-2024", ParentId = 25 },
                new Course(){ CourseId = 29, Department = "Semester 7", Batch = "2020-2024", SubjectCode = "HS4210", SubjectName = "Literature and values", Professor = "C.Balaji", StartDate = new DateTime(2023, 06, 05), Duration = "74", Coverage = 100, ParentId = 28 },

                new Course(){ CourseId = 30, Department = "C.Chandra Sekhar", Batch = "2022-2026" },
                new Course(){ CourseId = 31, Department = "Second Year", Batch = "2022-2026", ParentId = 30 },
                new Course(){ CourseId = 32, Department = "Semester 3", Batch = "2022-2026", SubjectCode = "CS2300", SubjectName = "Foundations of Computer Systems Design", Professor = "C.Chandra Sekhar", StartDate = new DateTime(2023, 07, 03), Duration = "73", Coverage = 100, ParentId = 31 },
                new Course(){ CourseId = 33, Department = "Semester 4", Batch = "2022-2026", SubjectCode = "CS2600", SubjectName = "Computer Organization and Architecture", Professor = "C.Chandra Sekhar", StartDate = new DateTime(2024, 01, 22), Duration = "72", Coverage = 100, ParentId = 31 },
                new Course(){ CourseId = 34, Department = "Final Year", Batch = "2020-2024", ParentId = 30 },
                new Course(){ CourseId = 35, Department = "Semester 7", Batch = "2020-2024", SubjectCode = "CS6025", SubjectName = "Deep Learning", Professor = "C.Chandra Sekhar", StartDate = new DateTime(2023, 06, 12), Duration = "74", Coverage = 100, ParentId = 34 },

                new Course(){ CourseId = 36, Department = "Balaraman Ravindran", Batch = "2023-2027" },
                new Course(){ CourseId = 37, Department = "Third Year", Batch = "2021-2025", ParentId = 36 },
                new Course(){ CourseId = 38, Department = "Semester 5" , Batch = "2021-2025", SubjectCode = "CS3500", SubjectName = "Operating Systems", Professor = "Balaraman Ravindran", StartDate = new DateTime(2023, 06, 12), Duration = "74", Coverage = 100, ParentId = 37 },

                new Course(){ CourseId = 39, Department = "Balakrishna Rao", Batch = "2023-2027" },
                new Course(){ CourseId = 40, Department = "First Year" , Batch = "2023-2027", ParentId = 39},
                new Course(){ CourseId = 41, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "ME1480", SubjectName = "Engineering Drawings", Professor = "Balakrishna Rao", StartDate = new DateTime(2024, 01, 29), Duration = "72", Coverage = 64, ParentId = 40 },

                new Course(){ CourseId = 42, Department = "Deleep R.Nair", Batch = "2023-2027" },
                new Course(){ CourseId = 43, Department = "Final Year" , Batch = "2020-2024", ParentId = 42 },
                new Course(){ CourseId = 44, Department = "Semester 8" , Batch = "2020-2024", SubjectCode = "HS3050", SubjectName = "Professional Ethics", Professor = "Deleep R.Nair", StartDate = new DateTime(2024, 01, 08), Duration = "72", Coverage = 90, ParentId = 43 },

                new Course(){ CourseId = 45, Department = "Dr.Abhijit P.Deshpande", Batch = "2023-2027" },
                new Course(){ CourseId = 46, Department = "First Year" , Batch = "2023-2027", ParentId = 45},
                new Course(){ CourseId = 47, Department = "Semester 1" , Batch = "2023-2027", SubjectCode = "ID1200", SubjectName = "Ecology and Environment", Professor = "Dr.Abhijit P.Deshpande", Coverage = 100, StartDate = new DateTime(2023, 07, 17), Duration = "73", ParentId = 46 },

                new Course(){ CourseId = 48, Department = "Himanshu Sinha", Batch = "2023-2027" },
                new Course(){ CourseId = 49, Department = "Third Year" , Batch = "2021-2025", ParentId = 48},
                new Course(){ CourseId = 50, Department = "Semester 6" , Batch = "2021-2025", SubjectCode = "BT1010", SubjectName = "Life Sciences", Professor= "Himanshu Sinha", StartDate = new DateTime(2024, 01, 08), Duration = "72", Coverage = 90, ParentId = 49 },

                new Course(){ CourseId = 51, Department = "Jayaraj", Batch = "2023-2027" },
                new Course(){ CourseId = 52, Department = "First Year" , Batch = "2023-2027", ParentId = 51},
                new Course(){ CourseId = 53, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "EE1100", SubjectName = "Basic Electrical Engineering", Professor = "Jayaraj", StartDate = new DateTime(2024, 02, 12), Duration = "72", Coverage = 46, ParentId = 52 },

                new Course(){ CourseId = 54, Department = "Jayaraj", Batch = "2023-2027" },
                new Course(){ CourseId = 55, Department = "Second Year" , Batch = "2022-2026", ParentId = 54},
                new Course(){ CourseId = 56, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "CS2200", SubjectName = "Languages, Machines, and Computations", Professor = "John Augustine", StartDate = new DateTime(2024, 01, 08), Duration = "72", Coverage = 90, ParentId = 55 },

                new Course(){ CourseId = 57, Department = "K.C.Sivaramakrishnan", Batch = "2023-2027" },
                new Course(){ CourseId = 58, Department = "Third Year" , Batch = "2021-2025", ParentId = 57},
                new Course(){ CourseId = 59, Department = "Semester 5" , Batch = "2021-2025", SubjectCode = "CS3100", SubjectName = "Paradigms of Programming", Professor = "K.C.Sivaramakrishnan", StartDate = new DateTime(2023, 06, 05), Duration = "74", Coverage = 100, ParentId = 58 },

                new Course(){ CourseId = 60, Department = "Kartik Nagar", Batch = "2023-2027" },
                new Course(){ CourseId = 61, Department = "Second Year" , Batch = "2022-2026", ParentId = 60},
                new Course(){ CourseId = 62, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "CS2810", SubjectName = "Object-Oriented Algorithms Implementation and Analysis Lab", Professor = "Kartik Nagar", StartDate = new DateTime(2024, 02, 05), Duration = "72", Coverage = 57, ParentId = 61 },

                new Course(){ CourseId = 63, Department = "Madhu Mutyam", Batch = "2023-2027" },
                new Course(){ CourseId = 64, Department = "Second Year" , Batch = "2022-2026", ParentId = 63},
                new Course(){ CourseId = 65, Department = "Semester 3" , Batch = "2022-2026", SubjectCode = "CS2310", SubjectName = "Foundations of Computer Systems Design Lab", Professor = "Madhu Mutyam", StartDate = new DateTime(2023, 07, 03), Duration = "73", Coverage = 100, ParentId = 64 },

                new Course(){ CourseId = 66, Department = "Meghana Nasre", Batch = "2023-2027" },
                new Course(){ CourseId = 67, Department = "Second Year" , Batch = "2022-2026", ParentId = 66},
                new Course(){ CourseId = 68, Department = "Semester 3" , Batch = "2022-2026", SubjectCode = "CS2710", SubjectName = "Programming and Data Structures Lab", Professor = "Meghana Nasre", StartDate = new DateTime(2023, 06, 26), Duration = "73", Coverage = 100, ParentId = 67 },

                new Course(){ CourseId = 69, Department = "Muraleedharan VR", Batch = "2023-2027" },
                new Course(){ CourseId = 70, Department = "Final Year" , Batch = "2020-2024", ParentId = 69},
                new Course(){ CourseId = 71, Department = "Semester 8" , Batch = "2020-2024", SubjectCode = "HS3002", SubjectName = "Principle of Economics", Professor = "Muraleedharan VR", StartDate = new DateTime(2024, 01, 15), Duration = "75", Coverage = 78, ParentId = 70 },

                new Course(){ CourseId = 72, Department = "Nishad Kothari", Batch = "2023-2027" },
                new Course(){ CourseId = 73, Department = "First Year" , Batch = "2023-2027", ParentId = 72},
                new Course(){ CourseId = 74, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "CS1200", SubjectName = "Discrete Mathematics for CS", Professor = "Nishad Kothari" , StartDate = new DateTime(2024, 01, 22), Duration = "72", Coverage = 74, ParentId = 73 },

                new Course(){ CourseId = 75, Department = "Sarang Sane", Batch = "2023-2027" },
                new Course(){ CourseId = 76, Department = "Second Year" , Batch = "2022-2026", ParentId = 75},
                new Course(){ CourseId = 77, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "MA2040", SubjectName = "Probability, Stochastic Process and Statistics", Professor = "Sarang Sane",  StartDate = new DateTime(2024, 02, 12), Duration = "72", Coverage = 46, ParentId = 76 },

                new Course(){ CourseId = 78, Department = "Shweta Agrawal", Batch = "2023-2027" },
                new Course(){ CourseId = 79, Department = "Second Year" , Batch = "2022-2026", ParentId = 78},
                new Course(){ CourseId = 80, Department = "Semester 4" , Batch = "2022-2026", SubjectCode = "CS2800", SubjectName = "Design and Analysis of Algorithms", Professor = "Shweta Agrawal" , StartDate = new DateTime(2024, 01, 15), Duration = "75", Coverage = 78, ParentId = 79 },

                new Course(){ CourseId = 81, Department = "Sivakumar.M.S", Batch = "2023-2027" },
                new Course(){ CourseId = 82, Department = "First Year" , Batch = "2023-2027", ParentId = 81},
                new Course(){ CourseId = 83, Department = "Semester 2" , Batch = "2023-2027", SubjectCode = "GN1102", SubjectName = "Life Skills II",  Professor = "Sivakumar.M.S", StartDate = new DateTime(2024, 02, 19), Duration = "72", Coverage = 36, ParentId = 82 }

            };

            return courses;
        }

        private List<Course> GetExamCollection()
        {
            var courses = new List<Course>()
            {
                new Course(){CourseId = 1, Department = "Partical Exams"},
                new Course(){CourseId = 2, Department = "First Year", ParentId = 1 },
                new Course(){CourseId = 3, Department = "Semester 1", SubjectName="Partical Exams", StartDate = new DateTime(2023,11,01), Duration="3", Coverage = 100, ParentId = 2 },
                new Course(){CourseId = 4, Department = "Semester 2", SubjectName="Partical  Exams",StartDate = new DateTime(2024,05,08), Duration="3", Coverage = 0, ParentId = 2 },
                new Course(){CourseId = 5, Department = "Second Year", ParentId = 1 },
                new Course(){CourseId = 6, Department = "Semester 3", SubjectName="Partical Exams",StartDate = new DateTime(2023,11,01), Duration="4", Coverage = 100, ParentId = 5 },
                new Course(){CourseId = 7, Department = "Semester 4", SubjectName = "Partical Exams", StartDate = new DateTime(2024,05,08), Duration="4", Coverage = 0, ParentId = 5 },

                new Course(){CourseId = 8, Department = "Theoretical Exams"},
                new Course(){CourseId = 9, Department = "First Year", ParentId = 8 },
                new Course(){CourseId = 10, Department = "Semester 1", SubjectName = "Theoretical Exams", StartDate = new DateTime(2023,11,06), Duration="15", Coverage = 100, ParentId = 9},
                new Course(){CourseId = 11, Department = "Semester 2", SubjectName = "Theoretical Exams", StartDate = new DateTime(2024,05,13), Duration="15", Coverage = 0, ParentId = 9},
                new Course(){CourseId = 12, Department = "Second Year", ParentId = 8 },
                new Course(){CourseId = 13, Department = "Semester 3", SubjectName = "Theoretical Exams", StartDate = new DateTime(2023,11,08), Duration="15", Coverage = 100, ParentId = 12},
                new Course(){CourseId = 14, Department = "Semester 4", SubjectName = "Theoretical Exams", StartDate = new DateTime(2024,05,15), Duration="15", Coverage = 0, ParentId = 12},
                new Course(){CourseId = 15, Department = "Third Year" , ParentId = 8 },
                new Course(){CourseId = 16, Department = "Semester 5", SubjectName = "Theoretical Exams", StartDate = new DateTime(2023,11,09), Duration="12", Coverage = 100, ParentId = 15},
                new Course(){CourseId = 17, Department = "Semester 6", SubjectName = "Theoretical Exams", StartDate = new DateTime(2024,05,20), Duration="1", Coverage = 0, ParentId = 15},
                new Course(){CourseId = 18, Department = "Final Year" , ParentId = 8 },
                new Course(){CourseId = 19, Department = "Semester 7", SubjectName = "Theoretical Exams", StartDate = new DateTime(2023,11,22), Duration="1", Coverage = 100, ParentId = 18},
                new Course(){CourseId = 20, Department = "Semester 8", SubjectName = "Theoretical Exams", StartDate = new DateTime(2024,05,15), Duration="10", Coverage = 0, ParentId = 18},
            };
            return courses;
        }

        private void GanttChartRowInfo(QueryChartRowInfoEventArgs<Course> args)
        {
            dynamic data = gantt.GetHierarchicalData(args.Data.CourseId);
            if(data.HasChildRecords != true && (args.Data.Department == "Theoretical Exams" || args.Data.SubjectName == "Theoretical Exams"))
            {
                args.Row.AddClass(new string[] {  "theoretical-exam" });
            }
            else if(data.HasChildRecords != true && (args.Data.Department == "Partical Exams" || args.Data.SubjectName == "Partical Exams"))
            {
                args.Row.AddClass(new string[] { "partical-exam" });
            }
            else if (data.HasChildRecords == true)
            {
                //args.Row.AddClass(new string[] { "customize-parent" });
            }
            else
            {
                args.Row.AddClass(new string[] { "customize-child" });
            }
        }

        private List<SegmentModel> GetSegmentCollection( List<Course> courses)
        {
            List<SegmentModel> segments = new List<SegmentModel>();
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

        private async Task ProfessorColumnMouseEnter(string value)
        {
            _professorNameTooltip = value;
            await Task.CompletedTask;
        }

        private string ProfessorImageHandler()
        {
            string[] professors = { "fullerking", "jack", "jackdavolio", "margaretbuchanan", "martintamer", "rosefuller", "vanjack" };
            Random random = new Random();
            int randomIndex = random.Next(professors.Length);
            string randomString = professors[randomIndex];
            return randomString;
        }
       
    }
}
