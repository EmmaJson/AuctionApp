using AuctionApp.Core;
using AuctionApp.Persistence;
using AutoMapper;

namespace AuctionApp.Mappers;

public class BidProfile : Profile
{
    public BidProfile()
    {
        CreateMap<BidDb, Bid>().ReverseMap();
    }
}