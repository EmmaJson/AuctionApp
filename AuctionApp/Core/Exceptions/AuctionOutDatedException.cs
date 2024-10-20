namespace AuctionApp.Core.Exceptions
{
    public class AuctionOutdatedException : InvalidOperationException
    {
        public AuctionOutdatedException() 
            : base("You cannot bid on your own auction.") 
        {
        }

        public AuctionOutdatedException(string message) 
            : base(message) 
        {
        }

        public AuctionOutdatedException(string message, Exception inner) 
            : base(message, inner) 
        {
        }
    }
}