using System.Data;
using AuctionApp.Core;
using AuctionApp.Core.Interfaces;
using AuctionApp.Models.Auctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.Controllers
{
    //[Authorize]
    public class AuctionsController : Controller
    {
        private IAuctionService _auctionService;

        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }
        
        // GET: AuctionsController
        public ActionResult Index()
        {
            List<Auction> auctions = _auctionService.GetAllActive();
            List<AuctionVm> auctionsVms = new List<AuctionVm>();
            foreach (Auction auction in auctions)
            {
                auctionsVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionsVms);
        }
        
        public ActionResult Pending()
        {
            List<Auction> auctions = _auctionService.GetUserActiveAuctions("julg@kth.se");
            //List<Auction> auctions = _auctionService.GetUserActiveAuctions("emmajoh2@kth.se");
            List<AuctionVm> auctionsVms = new List<AuctionVm>();
            foreach (Auction auction in auctions)
            {
                auctionsVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionsVms);
        }
        
        public ActionResult Won()
        {
            List<Auction> auctions = _auctionService.GetUserWonAuctions("lova@kth.se");
            List<AuctionVm> auctionsVms = new List<AuctionVm>();
            foreach (Auction auction in auctions)
            {
                auctionsVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionsVms);
        }
        
        // GET: AuctionsController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Auction auction = _auctionService.GetById(id);
                if (auction == null) return BadRequest();                           // HTTP 400
            
                AuctionDetailsVm detailsVm = AuctionDetailsVm.FromAuction(auction); // TODO: Här kan en mapper användas tydligen
                return View(detailsVm);
            }
            catch (DataException e)
            {
                return BadRequest(); 
            }
        }

        // GET: AuctionsController/Create
        public ActionResult CreateAuction()
        {
            return View();
        }
        
        // POST: AuctionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuction(CreateAuctionVm createAuctionVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string title = createAuctionVm.Title;
                    string description = createAuctionVm.Description;
                    DateTime endDate = createAuctionVm.EndDate;
                    double startingPrice = createAuctionVm.StartingPrice;
                    string auctionOwnerName = "emmajoh2@kth.se";                    // Dummy user
                    
                    _auctionService.Add(title, description, endDate, auctionOwnerName, startingPrice);
                    return RedirectToAction("Index");                               // Man kommer till indexSidan 
                }
                return View(createAuctionVm);
            }
            catch (DataException ex)
            {
                return View(createAuctionVm);                   // TODO: Kanske skicka med vilket fel använvdren gjort
            }
        }
        
        // GET: AuctionsController/CreateBid/5
        public ActionResult CreateBid(int id)
        {
            ViewBag.AuctionId = id; // Set the auction ID for the view
            return View();
        }
        
        // POST: AuctionsController/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBid(CreateBidVm createBidVm, int id)
        {
            try
            {
                double amount = createBidVm.Amount;
                string userName = "lovat@kth.se"; // Assuming user is authenticated
                Console.WriteLine($"Creating bid for auction ID: {id} by user: {userName} with amount: {amount}");
                if (ModelState.IsValid)
                {
                    // Attempt to add the bid
                    ViewBag.AuctionId = id; // Set the auction ID for the view
                    _auctionService.AddBid(id, userName, amount);
                    return RedirectToAction("Details", new { id = id });
                }
                return View(createBidVm); // Return view with validation errors if any
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(createBidVm);
            }
        }

        
/*
        // GET: AuctionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuctionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuctionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuctionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */
    }
}
