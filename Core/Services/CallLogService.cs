using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Controllers.Enumerations;
using Kaizen.Controllers.Common;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;

namespace Kaizen.Core.Services
{
    public class CallLogService : ICallLogService
    {
        private readonly IUnitOfWork unitOfWork;

        public CallLogService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task SynchronizeTodayCalls(string userId, IEnumerable<CallLog> callLogs)
        {
            var offers = await unitOfWork.CampaignDetailRepository.GetAgentOfersAsync(userId);

            if (offers == null)
                return;

            foreach (var offer in offers)
            {
                var matchCall = callLogs.FirstOrDefault(cd => cd.CallNumberFormatted.Equals(offer.Customer.CellPhone));

                if (matchCall != null)
                {
                    offer.TotalCallsNumber += GetCallsNumber(offer, matchCall);

                    var isValidCallByDuration = Convert.ToInt32(matchCall.CallDuration) > Constants.MinimunCallDurationValue;
                    var isValidCallByCallsNumber = IsValidCallByCallsNumber(offer, matchCall);

                    if (isValidCallByDuration || isValidCallByCallsNumber)
                    {
                        offer.LastValidCallDate = matchCall.CallDate;
                        offer.LastValidCallDuration = matchCall.CallDurationFormat;
                        offer.State = CampaignStatus.Called.ToString();
                    }

                    offer.LastCallDate = matchCall.CallDate;
                    offer.LastCallDuration = matchCall.CallDurationFormat;
                    offer.Customer = null;

                    unitOfWork.CampaignDetailRepository.Update(offer);
                }
            }

            await unitOfWork.SaveChangesAsync();
        }

        private static bool IsValidCallByCallsNumber(CampaignDetail campaignDetail, CallLog matchCall)
        {
            return (matchCall.TotalCallsNumber > Constants.MinimunTotalCallsNumberValue
                    && campaignDetail.LastValidCallDuration
                    .Equals(Constants.CallDurationDefaultValue));
        }

        private static int GetCallsNumber(CampaignDetail campaignDetail, CallLog matchCall)
        {
            return (matchCall.CallDate > campaignDetail.LastCallDate) ?
                matchCall.TotalCallsNumber - campaignDetail.TotalCallsNumber : 0;
        }

    }
}