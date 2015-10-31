using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Services.Interface;

namespace TradeYourPhone.Web.Controllers
{
    public class QuoteStatusController : Controller
    {
        private IQuoteService quoteService;

        public QuoteStatusController(IQuoteService quoteService)
        {
            this.quoteService = quoteService;
        }

        // GET: QuoteStatus
        [Authorize]
        public ActionResult Index()
        {
            return View(quoteService.GetAllQuoteStatuses());
        }

        // GET: QuoteStatus/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuoteStatus quoteStatus = quoteService.GetQuoteStatusById((int)id);
            if (quoteStatus == null)
            {
                return HttpNotFound();
            }
            return View(quoteStatus);
        }

        // GET: QuoteStatus/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuoteStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,QuoteStatusName")] QuoteStatus quoteStatus)
        {
            if (ModelState.IsValid)
            {
                bool created = quoteService.CreateQuoteStatus(quoteStatus);
                if (!created)
                {
                    ModelState.AddModelError("QuoteStatus", "Quote Status already exists");
                    return View(quoteStatus);
                }
                return RedirectToAction("Index");
            }

            return View(quoteStatus);
        }

        // GET: QuoteStatus/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuoteStatus quoteStatus = quoteService.GetQuoteStatusById((int)id);
            if (quoteStatus == null)
            {
                return HttpNotFound();
            }
            return View(quoteStatus);
        }

        // POST: QuoteStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,QuoteStatusName")] QuoteStatus quoteStatus)
        {
            if (ModelState.IsValid)
            {
                bool modified = quoteService.ModifyQuoteStatus(quoteStatus);
                if (!modified)
                {
                    ModelState.AddModelError("QuoteStatus", "Quote Status already exists");
                    return View(quoteStatus);
                }
                return RedirectToAction("Index");
            }
            return View(quoteStatus);
        }

        //// GET: QuoteStatus/Delete/5
        //[Authorize]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    QuoteStatus quoteStatus = quoteService.GetQuoteStatusById((int)id);
        //    if (quoteStatus == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(quoteStatus);
        //}

        //// POST: QuoteStatus/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    quoteService.DeleteQuoteStatusById(id);
        //    return RedirectToAction("Index");
        //}
    }
}
