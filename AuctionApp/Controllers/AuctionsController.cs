using System.Data;
using AuctionApp.Core;
using AuctionApp.Core.Exceptions;
using AuctionApp.Core.Interfaces;
using AuctionApp.Models.Auctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.Controllers
{
    [Authorize]
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
            List<Auction> auctions = _auctionService.GetUserActiveAuctions(User.Identity.Name);
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
            List<Auction> auctions = _auctionService.GetUserWonAuctions(User.Identity.Name);
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
                /*foreach (var Bid in auction.Bids)
                {
                    Console.WriteLine(Bid.BidDate);
                }
                */
                if (auction == null) return BadRequest();                           // HTTP 400
            
                AuctionDetailsVm detailsVm = AuctionDetailsVm.FromAuction(auction); // TODO: Här kan en mapper användas tydligen
                return View(detailsVm);
            }
            catch (DataException e)
            {
                return BadRequest(); 
            }
        }

        // GET: AuctionsController/CreateAuction
        public ActionResult CreateAuction()
        {
            return View();
        }
        
        // POST: AuctionsController/CreateAuction
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
                    string auctionOwnerName = User.Identity.Name;                    // Dummy user
                    
                    _auctionService.Add(title, description, endDate, auctionOwnerName, startingPrice);
                    TempData["Success"] = "Auction was created";
                    return RedirectToAction("Index");                               // Man kommer till indexSidan 
                }
                return View(createAuctionVm);
            }
            catch (DataException ex)
            {
                TempData["ErrorMessage"] = "A problem occured while creating an auction. Please try again.";
                return RedirectToAction("Index");           
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
                string userName = User.Identity.Name; // Assuming user is authenticated
                Console.WriteLine($"Creating bid for auction ID: {id} by user: {userName} with amount: {amount}");
                if (ModelState.IsValid)
                {
                    // Attempt to add the bid
                    ViewBag.AuctionId = id; // Set the auction ID for the view
                    _auctionService.AddBid(id, userName, amount);
                    TempData["Success"] = "Bid was created";
                }
                return RedirectToAction("Details", new { id = id });
            }
            catch (AuctionOutdatedException ex)
            {
                TempData["ErrorMessage"] = "You cant place a bid on a closed auction."; // Store the error message
                return RedirectToAction("Details", new { id });
            }
            catch (AddBidToOwnAuctionException ex)
            {
                TempData["ErrorMessage"] = "You can not bid on your own auction."; // Store the error message
                return RedirectToAction("Details", new { id });
            }
            catch (ToLowBidException ex)
            {
                TempData["ErrorMessage"] = "Your bid does not exceed current price"; // Store the error message
                return RedirectToAction("Details", new { id });
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] =
                    "An invalid operation occurred. Please try again later."; // General error message
                return RedirectToAction("Details", new { id });
            }
            catch (DataException ex)
            {
                TempData["ErrorMessage"] = "There was a problem with the data. Please try again."; // Data error message
                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] =
                    "An unexpected error occurred. Please try again later."; // Generic error message
                return RedirectToAction("Details", new { id });
            }
        }
        
        // GET: AuctionsController/Description/5
        public ActionResult CreateDescription(int id)
        {
            ViewBag.AuctionId = id; // Set the auction ID for the view
            return View();
        }
        
        // POST: AuctionsController/Description/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDescription(CreateDescriptionVm createDescriptionVm, int id)
        {
            try
            {
                string userName = User.Identity.Name;
                if (ModelState.IsValid)
                {
                    // Attempt to add the bid
                    ViewBag.AuctionId = id; // Set the auction ID for the view
                    _auctionService.UpdateDescription(id, userName, createDescriptionVm.Description);
                    TempData["Success"] = "Auction description updated";
                    return RedirectToAction("Index"); 
                }
                return View(createDescriptionVm); // Return view with validation errors if any
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = "Only owner can change the description.";
                return RedirectToAction("Index"); 
            }
            catch (DataException ex)
            {
                TempData["ErrorMessage"] = "A problem occured while editing the description. Please try again.";
                return RedirectToAction("Index"); 
            }
        }
    }
}
