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
            var queryResult = await unitOfWork.CampaignRepository.GetAgentValidCampaignsAsync(userId, new CampaignQuery());

            if (queryResult.TotalItems == 0)
                return;

            foreach (var userCampaign in queryResult.Items)
            {
                foreach (var campaignDetail in userCampaign.CampaignDetails)
                {
                    var matchCall = callLogs.FirstOrDefault(cd => cd.CallNumberFormatted.Equals(campaignDetail.Customer.CellPhone));

                    if (matchCall != null)
                    {
                        campaignDetail.TotalCallsNumber += GetCallsNumber(campaignDetail, matchCall);

                        var isValidCallByDuration = Convert.ToInt32(matchCall.CallDuration) > Constants.MinimunCallDurationValue;
                        var isValidCallByCallsNumber = IsValidCallByCallsNumber(campaignDetail, matchCall);

                        if (isValidCallByDuration || isValidCallByCallsNumber)
                        {
                            campaignDetail.LastValidCallDate = matchCall.CallDate;
                            campaignDetail.LastValidCallDuration = matchCall.CallDurationFormat;
                            campaignDetail.State = CampaignStatus.Called.ToString();
                        }

                        campaignDetail.LastCallDate = matchCall.CallDate;
                        campaignDetail.LastCallDuration = matchCall.CallDurationFormat;

                        unitOfWork.CampaignDetailRepository.Update(campaignDetail);
                    }
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