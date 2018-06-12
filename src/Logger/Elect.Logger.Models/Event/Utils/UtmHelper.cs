using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Elect.Logger.Models.Event.Utils
{
    internal class UtmHelper
    {
        public static UtmModel Get(HttpRequest httpRequest)
        {
            bool isHaveAnyUtmData = httpRequest.Headers.Any(x => x.Key.Contains("utm_"));

            if (!isHaveAnyUtmData)
            {
                return null;
            }
            
            UtmModel model = new UtmModel
            {
                WebSiteUrl = httpRequest.GetDisplayUrl(),
                CampaignName = httpRequest.Query[UtmConstant.Campaign],
                CampaignMedium = httpRequest.Query[UtmConstant.Medium],
                CampaignSource = httpRequest.Query[UtmConstant.Source],
                CampaignContent = httpRequest.Query[UtmConstant.Content],
                CampaignTerm = httpRequest.Query[UtmConstant.Term]
            };
            
            return model;
        }
    }
}