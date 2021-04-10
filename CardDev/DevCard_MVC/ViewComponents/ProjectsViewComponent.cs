using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevCard_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevCard_MVC.ViewComponents
{
    public class ProjectsViewComponent : ViewComponent
    {
        
        public IViewComponentResult Invoke(string name)
        {
            var projects = new List<Project>()
            {
                new Project(1, "تاکسی", "توضیحات پروژه تاکسی", "Mobin", "project-1.jpg"),
                new Project(2, "رستوران هوشمند", "توضیحات پروژه رستوران", "Mobin", "project-2.jpg"),
                new Project(3, "لیدرمن", "توضیحات پروژه لیدرمن", "Mobin", "project-3.jpg"),
                new Project(4, "مطالعان", "توضیحات پروژه مطالعان", "Mobin", "project-4.jpg"),
            };

            return View("_Projects", projects);
        }
    }
}
