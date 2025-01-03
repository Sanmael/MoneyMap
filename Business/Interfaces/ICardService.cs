using Business.Models;
using Business.Requests.Card;
using Business.Response;

namespace Business.Interfaces
{
    public interface ICardService
    {
        public Task<BaseResponse> AddCardAsync(InsertCardRequest card);
        public Task<BaseResponse> GetCardAsync(GetCardRequest card);
        public Task<BaseResponse> UpdateBalanceCardAsync(UpdateBalanceCardRequest card);
    }
}