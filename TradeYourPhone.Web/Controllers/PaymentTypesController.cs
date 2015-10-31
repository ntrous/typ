using System.Net;
using System.Web.Mvc;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Services.Interface;

namespace TradeYourPhone.Web.Controllers
{
    public class PaymentTypesController : Controller
    {
        private IQuoteService quoteService;

        public PaymentTypesController(IQuoteService quoteService)
        {
            this.quoteService = quoteService;
        }

        // GET: PaymentTypes
        [Authorize]
        public ActionResult Index()
        {
            return View(quoteService.GetAllPaymentTypes());
        }

        // GET: PaymentTypes/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentType paymentType = quoteService.GetPaymentTypeById((int)id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }
            return View(paymentType);
        }

        // GET: PaymentTypes/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PaymentTypeName")] PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                bool created = quoteService.CreatePaymentType(paymentType);
                if (!created)
                {
                    ModelState.AddModelError("PaymentType", "Payment Type already exists");
                    return View(paymentType);
                }
                return RedirectToAction("Index");
            }

            return View(paymentType);
        }

        // GET: PaymentTypes/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentType paymentType = quoteService.GetPaymentTypeById((int)id);
            if (paymentType == null)
            {
                return HttpNotFound();
            }
            return View(paymentType);
        }

        // POST: PaymentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PaymentTypeName")] PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                bool modified = quoteService.ModifyPaymentType(paymentType);
                if (!modified)
                {
                    ModelState.AddModelError("PaymentType", "Payment Type already exists");
                    return View(paymentType);
                }
                return RedirectToAction("Index");
            }
            return View(paymentType);
        }

        // GET: PaymentTypes/Delete/5
        //[Authorize]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PaymentType paymentType = quoteService.GetPaymentTypeById((int)id);
        //    if (paymentType == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(paymentType);
        //}

        //// POST: PaymentTypes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    quoteService.DeletePaymentTypeById(id);
        //    return RedirectToAction("Index");
        //}
    }
}
