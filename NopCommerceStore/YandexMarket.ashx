<%@ WebHandler Language="C#" Class="YandexMarket" %>
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.YandexMarket;

public class YandexMarket : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/rss+xml";
        context.Response.ContentEncoding = System.Text.Encoding.UTF8;

        context.Response.Cache.SetCacheability(HttpCacheability.Public);
        context.Response.Cache.SetExpires(DateTime.Now.AddHours(1));

        if (SettingManager.GetSettingValueBoolean("YandexMarket.AllowPublicYandexMarketAccess"))
        {
            YandexMarketService.GenerateFeed(context.Response.OutputStream);
        }
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}