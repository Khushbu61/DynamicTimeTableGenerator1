using Microsoft.AspNetCore.Mvc;
using DynamicTimeTableGenerator.ViewModels;
using DynamicTimeTableGenerator.Repositories;

namespace DynamicTimeTableGenerator.Controllers
{
    public class TimetableController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;

        public TimetableController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnterSubjects(TimetableInputViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            int totalHours = model.WorkingDays * model.SubjectsPerDay;
            TempData["TotalHours"] = totalHours;
            TempData["TotalSubjects"] = model.TotalSubjects;
            TempData["WorkingDays"] = model.WorkingDays;
            TempData["SubjectsPerDay"] = model.SubjectsPerDay;
            TempData.Keep();
            return RedirectToAction("EnterSubjects");
        }

        public IActionResult EnterSubjects()
        {
            int totalSubjects = Convert.ToInt32(TempData["TotalSubjects"]);
            ViewBag.TotalHours = Convert.ToInt32(TempData["TotalHours"]);
            TempData.Keep();
            return View(new List<string>(new string[totalSubjects])); // Empty subjects
        }

        [HttpPost]
        public IActionResult GenerateTimetable(List<string> Subjects, List<int> Hours)
        {
            // Retrieve stored values from TempData
            if (TempData["TotalHours"] == null || TempData["WorkingDays"] == null || TempData["SubjectsPerDay"] == null)
            {
                return RedirectToAction("Index"); // If TempData is lost, restart process
            }

            int totalHours = Convert.ToInt32(TempData["TotalHours"]);
            int workingDays = Convert.ToInt32(TempData["WorkingDays"]);
            int subjectsPerDay = Convert.ToInt32(TempData["SubjectsPerDay"]);

            // Validate total hours
            if (Hours.Sum() != totalHours)
            {                
                ViewBag.Message = "Total hours must match the calculated total!";
                return View("EnterSubjects");

            }

            // Generate the Timetable
            List<List<string>> timetable = GenerateDynamicTimetable(Subjects, Hours, workingDays, subjectsPerDay);
            return View("GenerateTimetable", timetable);
        }


        private List<List<string>> GenerateDynamicTimetable(List<string> subjects, List<int> hours, int workingDays, int subjectsPerDay)
        {
            // Create a subject pool based on assigned hours
            List<string> subjectPool = new List<string>();
            for (int i = 0; i < subjects.Count; i++)
            {
                for (int j = 0; j < hours[i]; j++)
                {
                    subjectPool.Add(subjects[i]);
                }
            }

            // Shuffle the subject pool to ensure randomness
            Random rand = new Random();
            subjectPool = subjectPool.OrderBy(x => rand.Next()).ToList();

            // Ensure correct subject allocation
            List<List<string>> timetable = new List<List<string>>();
            int index = 0;

            for (int i = 0; i < subjectsPerDay; i++)
            {
                var row = new List<string>();
                for (int j = 0; j < workingDays; j++)
                {
                    if (index >= subjectPool.Count) index = 0; // Reset index if it exceeds pool size
                    row.Add(subjectPool[index]);
                    index++;
                }
                timetable.Add(row);
            }

            return timetable;
        }

    }
}
