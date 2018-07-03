using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PreQualTool.Models;

namespace PreQualTool.Controllers
{
    public class ApplicationController : Controller
    {
        private PrequalDBEntitySettings db = new PrequalDBEntitySettings();

        // GET: /Application/
        public ActionResult Index()
        {
            var applicants = db.Applicants.Include(a => a.Card);
            return View(applicants.ToList());
        }

        // GET: /Application/ApplicationForm
        public ActionResult ApplicationForm()
        {
            ViewBag.CardID = new SelectList(db.Cards, "Id", "Name");
            return View();
        }

        // POST: /Application/ApplicationForm
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplicationForm([Bind(Include = "FirstName,SecondName,DateOfBirth,Income,CardID")] Applicant applicant)
        {

            int age = DateTime.Today.Year - applicant.DateOfBirth.Value.Year;
            if (DateTime.Now.Month < applicant.DateOfBirth.Value.Month || (DateTime.Now.Month == applicant.DateOfBirth.Value.Month && DateTime.Now.Day < applicant.DateOfBirth.Value.Day))
            {
                age--;
            }

            if (age >= 18)
            {
                if (applicant.Income.Value >= 30000)
                {
                    applicant.CardID = 1;
                }
                else
                {
                    applicant.CardID = 2;
                }
            }
            else
            {
                applicant.CardID = 0;
            }

            if (ModelState.IsValid)
            {                
                db.Applicants.Add(applicant);
                db.SaveChanges();
                return Redirect("~/Application/Card/" + applicant.CardID.Value);
            }

            ViewBag.CardID = new SelectList(db.Cards, "Id", "Name", applicant.CardID);
            return View();
        }

        // GET: /Application/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant applicant = db.Applicants.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }

        // POST: /Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Applicant applicant = db.Applicants.Find(id);
            db.Applicants.Remove(applicant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Card(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
