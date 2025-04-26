using Microsoft.AspNetCore.Mvc;
using CTIN.Abp.AspNetCore.Mvc;

namespace CSP_NET.Work.Controllers;

public class HomeController : AbpController
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }
}
