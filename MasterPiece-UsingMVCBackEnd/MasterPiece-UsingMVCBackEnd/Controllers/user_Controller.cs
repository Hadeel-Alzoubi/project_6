using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MasterPiece_UsingMVCBackEnd.Models;

namespace MasterPiece_UsingMVCBackEnd.Controllers
{
    public class user_Controller : Controller
    {
        private MasterPieceEntities db = new MasterPieceEntities();

        // GET: user_
        public ActionResult Index()
        {
            return View(db.user_.ToList());
        }

        // GET: user_/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_ user_ = db.user_.Find(id);
            if (user_ == null)
            {
                return HttpNotFound();
            }
            return View(user_);
        }

        // GET: user_/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: user_/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,product_id,pass,name,email,adress,phoneNo")] user_ user_)
        {
            if (ModelState.IsValid)
            {
                db.user_.Add(user_);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_);
        }

        // GET: user_/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_ user_ = db.user_.Find(id);
            if (user_ == null)
            {
                return HttpNotFound();
            }
            return View(user_);
        }

        // POST: user_/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,product_id,pass,name,email,adress,phoneNo")] user_ user_)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_);
        }

        // GET: user_/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_ user_ = db.user_.Find(id);
            if (user_ == null)
            {
                return HttpNotFound();
            }
            return View(user_);
        }

        // POST: user_/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user_ user_ = db.user_.Find(id);
            db.user_.Remove(user_);
            db.SaveChanges();
            return RedirectToAction("Index");
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
