using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;
using System.Linq;

namespace TechJobs.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            
            return View();
        }

        public IActionResult Results()
        {
            var jobs = new List<Dictionary<string, string>>();
            var searchTerm = Request.Query["searchTerm"].ToString();
            var searchType = Request.Query["searchType"].ToString();
            ViewBag.searchTerm = searchTerm;

            foreach (var job in JobData.FindAll())
            {
                if (searchType == "all")
                {
                    if (job.Values.Any(value => value.ToUpper().Contains(searchTerm.ToUpper())))
                    {
                        jobs.Add(job);
                    }
                }
                else
                {
                    if (job.ContainsKey(searchType) && job[searchType].ToUpper().Contains(searchTerm.ToUpper()))
                    {
                        jobs.Add(job);
                    }
                }
            }

            ViewBag.jobs = jobs;
                        
            return View();
        }

        // TODO #1 - Create a Results action method to process 
        // search request and display results

    }
}
