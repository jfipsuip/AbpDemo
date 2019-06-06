using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Auditing;

namespace WebApp.Application.Sessions
{
    public class SessionAppService : ApplicationService
    {
        [DisableAuditing]
        //[Abp.Authorization.AbpAuthorize]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                //Application = new ApplicationInfoDto
                //{
                //    Version = AppVersionHelper.Version,
                //    ReleaseDate = AppVersionHelper.ReleaseDate,
                //    Features = new Dictionary<string, bool>
                //    {
                //        { "SignalR", SignalRFeature.IsAvailable },
                //        { "SignalR.AspNetCore", SignalRFeature.IsAspNetCore }
                //    }
                //}
            };

            if (AbpSession.UserId.HasValue)
            {
                //output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
            }

            return output;
        }
    }
}
