namespace AuctionApp.Core.Exceptions
{
    public class ToLowBidException : InvalidOperationException
    {
        public ToLowBidException()
            : base("The bid amount must be greater than the starting price.")
        {
        }

        public ToLowBidException(string message) 
            : base(message) 
        {
        }

        public ToLowBidException(string message, Exception inner) 
            : base(message, inner) 
        {
        }
    }
}