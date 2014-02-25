using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CPI_Sample.Models;

namespace CPI_Sample.Controllers
{
    public class EmployeeController : Controller
    {
        private CPIDbContext db = new CPIDbContext();

        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        //
        // GET: /Employee/Details/5

        public ActionResult Details(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.employeeInitials = employee.employeeInitials.ToUpper();

                foreach (var e in db.Employees)
                {
                    if (employee.employeeInitials == e.employeeInitials)
                    {
                        return Content(String.Format("Employee with initials {0} already exists!", e.employeeInitials.ToUpper()));
                    }
                }


                employee.employeeId = db.Employees.Max(x => x.employeeId) + 1;
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Employee employee = db.Employees.SingleOrDefault(e => e.employeeId == id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.employeeInitials = employee.employeeInitials.ToUpper();

                db.Entry(employee).State = EntityState.Modified;

                foreach (var e in db.Employees)
                {
                    if (employee.employeeInitials == e.employeeInitials && e.employeeId != employee.employeeId)
                    {
                        return Content(String.Format("Employee with initials {0} already exists!", e.employeeInitials.ToUpper()));
                    }
                }

                db.SaveChanges(); 
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}