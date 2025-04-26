using System.Collections.Generic;
using System.Security.Claims;
using CTIN.Abp.DependencyInjection;
using CTIN.Abp.Security.Claims;

namespace CSP_NET.Work.Security;

[Dependency(ReplaceServices = true)]
public class FakeCurrentPrincipalAccessor : ThreadCurrentPrincipalAccessor
{
    protected override ClaimsPrincipal GetClaimsPrincipal()
    {
        return GetPrincipal();
    }

    private ClaimsPrincipal GetPrincipal()
    {
        return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new Claim(AbpClaimTypes.UserId, "2e701e62-0953-4dd3-910b-dc6cc93ccb0d"),
            new Claim(AbpClaimTypes.UserName, "admin"),
            new Claim(AbpClaimTypes.Email, "admin@abp.io")
        }));
    }
}
