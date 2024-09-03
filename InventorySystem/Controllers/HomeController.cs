using InventorySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace InventorySystem.Controllers
{
    public class HomeController : Controller
    {
        private InventoryContext context; // private context set
        public HomeController(InventoryContext ctx) => context = ctx; // public homecontroller with context passed in

        public ViewResult Index(string id) // index method, with id passed in
        {
            // load current filters and data needed for filter drop downs in InventoryViewModel
            var model = new InventoryViewModel
            {
                Filters = new Filters(id), // sets filters based on id passed
                Categories = context.Categories.ToList(), // sets categories filters
                Statuses = context.Statuses.ToList(), // sets status filters to status list
                DateCheckedFilters = Filters.DateCheckedFilterValues // sets date checked filters
            };

            // get open tasks from database based on current filters
            IQueryable<Inventory> query = context.Inventories
                .Include(t => t.Category).Include(t => t.Status);

            if (model.Filters.HasCategory) // if filters has a category:
            {
                query = query.Where(t => t.CategoryId == model.Filters.CategoryId); // query results with matching category
            }
            if (model.Filters.HasStatus) // if filters have a status chosen:
            {
                query = query.Where(t => t.StatusId == model.Filters.StatusId); // query results with matching category
            }
            if (model.Filters.HasDue) // if filters have due
            {
                var today = DateTime.Today; // sets today to todays date
                if (model.Filters.IsYesterday) // if model is yesterday
                    query = query.Where(t => t.DateChecked == today.AddDays(-1)); // query dates from yesterday
                else if (model.Filters.IsLastMonth) // if model filter is last month
                    query = query.Where(t => t.DateChecked.Month == today.AddMonths(-1).Month); // query last month
                else if (model.Filters.IsLastYear) // if model is last year
                    query = query.Where(t => t.DateChecked.Year == today.AddYears(-1).Year); // query last year
            }
            model.Inventories = query.OrderBy(t => t.DateChecked).ToList(); // query ordered results based on date checked

            return View(model); // return model
        }

        [HttpGet]
        public ViewResult Add() // add method
        {
            var model = new InventoryViewModel // sets model to new inventoryview model
            {
                Categories = context.Categories.ToList(), // categories in list
                Statuses = context.Statuses.ToList(), // statuses in list
                CurrentProduct = new Inventory { StatusId = "accurate" }  // set default value for drop-down
            };
            return View(model); // return add view with model passed in
        }

        [HttpPost]
        public IActionResult Add(InventoryViewModel model) // add method post request
        {
            if (ModelState.IsValid) // if model is valid
            {
                context.Inventories.Add(model.CurrentProduct); // add current product
                context.SaveChanges(); // save changes to database
                return RedirectToAction("Index"); // redirect to home/main page
            }
            else // if not valid
            {
                model.Categories = context.Categories.ToList(); // categories in list
                model.Statuses = context.Statuses.ToList(); // statuses in list
                return View(model); // return view model
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter) // post request for filter results
        {
            string id = string.Join('-', filter); // sets id for filter
            return RedirectToAction("Index", new { ID = id }); // redirects to main page wiht filter chosen.
        }

        [HttpPost]
        public IActionResult MarkComplete([FromRoute] string id, Inventory selected) // mark complete post request
        {
            selected = context.Inventories.Find(selected.InventoryId)!;  // use null-forgiving operator to suppress null warning
            if (selected != null) // if selected isn't null
            {
                selected.StatusId = "accurate"; // set status to accurate
                context.SaveChanges(); // save to database
            }

            return RedirectToAction("Index", new { ID = id }); // redirect to main page 
        }

        [HttpPost]
        public IActionResult DeleteComplete(string id) // delete post request for completed items
        {
            var toDelete = context.Inventories
                .Where(t => t.StatusId == "accurate").ToList(); //delete context inventories if the status is accurate

            foreach (var task in toDelete) // foreach task in the list toDelete
            {
                context.Inventories.Remove(task); // remove the task
            }
            context.SaveChanges(); // save database

            return RedirectToAction("Index", new { ID = id }); // redirect to main page
        }
    }
}
