using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebsiteThuCungBento.Models;

namespace WebsiteThuCungBento.App_Start
{
    public class AdminPhanQuyen : AuthorizeAttribute
    {
        DBWebsiteThuCungBentoDataContext db = new DBWebsiteThuCungBentoDataContext();
        public string MaCN { set; get; }
        public override void OnAuthorization(AuthorizationContext context)
        {
            NhanVien quantri = (NhanVien)HttpContext.Current.Session["TaiKhoanAdmin"];

            if (quantri != null)
            {
                var count = db.PhanQuyens.Count(m => m.MaNV == quantri.MaNV & m.MaCN == MaCN);
                if (count != 0)
                {
                    return;
                }
                else
                {
                    var returnUrl = context.RequestContext.HttpContext.Request.RawUrl;
                    context.Result = new RedirectToRouteResult(new
                        RouteValueDictionary
                        (
                            new
                            {
                                controller = "BaoLoi",
                                action = "KhongCoQuyen",
                                area = "Admin",
                                returnUrl = returnUrl.ToString()
                            }
                        ));
                }
                return;
            }
            else
            {
                var returnUrl = context.RequestContext.HttpContext.Request.RawUrl;
                context.Result = new RedirectToRouteResult(new
                    RouteValueDictionary
                    (
                        new
                        {
                            controller = "Admin",
                            action = "Index",
                            area = "Admin",
                            returnUrl = returnUrl.ToString()
                        }
                    ));
            }
        }
    }
}