using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Services.Interface;

namespace TradeYourPhone.Web.Controllers
{
    public class PhoneMakesController : Controller
    {
        private IPhoneService phoneService;

        public PhoneMakesController(IPhoneService phoneService)
        {
            this.phoneService = phoneService;
        }

        // GET: PhoneMakes
        [Authorize]
        public ActionResult Index()
        {
            return View(phoneService.GetAllPhoneMakes());
        }

        // GET: PhoneMakes/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneMake phoneMake = phoneService.GetPhoneMakeById((int)id);
            if (phoneMake == null)
            {
                return HttpNotFound();
            }
            return View(phoneMake);
        }

        // GET: PhoneMakes/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhoneMakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MakeName")] PhoneMake phoneMake)
        {
            if (ModelState.IsValid)
            {
                bool created = phoneService.CreatePhoneMake(phoneMake);
                if (!created)
                {
                    ModelState.AddModelError("PhoneMake", "Phone Make already exists");
                    return View(phoneMake);
                }
                return RedirectToAction("Index");
            }

            return View(phoneMake);
        }

        // GET: PhoneMakes/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhoneMake phoneMake = phoneService.GetPhoneMakeById((int)id);
            if (phoneMake == null)
            {
                return HttpNotFound();
            }
            return View(phoneMake);
        }

        // POST: PhoneMakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MakeName")] PhoneMake phoneMake)
        {
            if (ModelState.IsValid)
            {
                bool modified = phoneService.ModifyPhoneMake(phoneMake);
                if (!modified)
                {
                    ModelState.AddModelError("PhoneMake", "Phone Make already exists");
                    return View(phoneMake);
                }
                return RedirectToAction("Index");
            }
            return View(phoneMake);
        }

        // GET: PhoneMakes/Delete/5
        //[Authorize]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PhoneMake phoneMake = phoneService.GetPhoneMakeById((int)id);
        //    if (phoneMake == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(phoneMake);
        //}

        //// POST: PhoneMakes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    phoneService.DeletePhoneMakeById(id);
        //    return RedirectToAction("Index");
        //}

        /// <summary>
        /// Get all Phone Makes
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetPhoneMakes()
        {
            var phoneMakes = phoneService.GetAllPhoneMakes();
            var result = (from s in phoneMakes
                          select new
                          {
                              id = s.ID,
                              name = s.MakeName
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
