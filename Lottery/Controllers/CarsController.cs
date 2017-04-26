using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lottery.Models;

namespace Lottery.Controllers
{// Declares the DbContext class
public class CarsController : Controller
  {
    // Declares the DbContext class
    private CarsContext dataContext;
    // The instance of DbContext is passed via dependency injection
    public CarsController(CarsContext context)
    {
      this.dataContext=context;
    }
    // GET: /<controller>/
    // Return the list of cars to the caller view
    public IActionResult Index()
    {
      return View(this.dataContext.Cars.ToList());
    }
    public IActionResult Create()
    {
      return View();
    }
    // Add a new object via a POST request
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Cars car)
    {
      // If the data model is in a valid state ...
      if (ModelState.IsValid)
      {
        // ... add the new object to the collection
        dataContext.Cars.Add(car);
        // Save changes and return to the Index method
        dataContext.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(car);
    }
    [ActionName("Delete")]
    public IActionResult Delete(int? id)
    {
      
      Cars car = dataContext.Cars.Single(m => m.Id == id);
     
      return View(car);
    }
    // POST: Cars/Delete/5
    // Delete an object via a POST request
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
      Cars car = dataContext.Cars.SingleOrDefault(m => m.Id == id);
      // Remove the car from the collection and save changes
      dataContext.Cars.Remove(car);
      dataContext.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}