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
    public class CustomerController : Controller
    {
        private CPIDbContext db = new CPIDbContext();

        //
        // GET: /Customer/

        public ActionResult Index()
        {
            var customers = db.Customers.ToList();

            foreach (var c in customers)
            {
                c.employee = db.Employees.FirstOrDefault(e => e.employeeId == c.repId);
            }

            return View(db.Customers.ToList());
        }

        //
        // GET: /Customer/Details/5

        public ActionResult Details(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var customers = db.Customers.ToList();

            foreach (var c in customers)
            {
                c.employee = db.Employees.FirstOrDefault(e => e.employeeId == c.repId);
            }

            return View(customer);
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customer/Create

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (db.Employees.Find(customer.repId) == null)
                {
                    return Content("Rep Id is invalid");
                }

                foreach (var c in db.Customers)
                {
                    if (customer.customerAccountNumber == c.customerAccountNumber.Trim())
                    {
                        return Content(String.Format("Customer with Account {0} already exists!", c.customerAccountNumber));
                    }
                }

                customer.customerId = db.Customers.Max(x => x.customerId) + 1;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        //
        // GET: /Customer/Edit/5

        public ActionResult Edit(int id)
        {
            Customer customer = db.Customers.Find(id);

            var customers = db.Customers.ToList();
           
            foreach (var c in customers)
            {
                c.employee = db.Employees.FirstOrDefault(e => e.employeeId == c.repId);
            }

if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;

                if (db.Employees.Find(customer.repId) == null)
                {
                    return Content("Rep Id is invalid");
                }

                foreach (var c in db.Customers)
                {
                    if (customer.customerAccountNumber == c.customerAccountNumber && c.customerAccountNumber != customer.customerAccountNumber)
                    {
                        return Content(String.Format("Customer with Account {0} already exists!", c.customerAccountNumber));
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        //
        // GET: /Customer/Delete/5

        public ActionResult Delete(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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