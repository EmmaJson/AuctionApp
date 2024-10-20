using System;

namespace AuctionApp.Core.Exceptions
{
    public class AddBidToOwnAuctionException : InvalidOperationException
    {
        public AddBidToOwnAuctionException() 
            : base("Cannot place a bid on an auction that has already ended.") 
        {
        }

        public AddBidToOwnAuctionException(string message) 
            : base(message) 
        {
        }

        public AddBidToOwnAuctionException(string message, Exception inner) 
            : base(message, inner) 
        {
        }
    }
}