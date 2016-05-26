//------------------------------------------------------------------------------
// The contents of this file are subject to the nopCommerce Public License Version 1.0 ("License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at  http://www.nopCommerce.com/License.aspx. 
// 
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. 
// See the License for the specific language governing rights and limitations under the License.
// 
// The Original Code is nopCommerce.
// The Initial Developer of the Original Code is NopSolutions.
// All Rights Reserved.
// 
// Contributor(s): _______. 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.YandexMarket;

namespace NopSolutions.NopCommerce.Web.Administration.Modules
{
    public partial class YandexMarketControl : BaseNopAdministrationUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            cbAllowPublicYandexMarketAccess.Checked = SettingManager.GetSettingValueBoolean("YandexMarket.AllowPublicYandexMarketAccess");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    SettingManager.SetParam("YandexMarket.AllowPublicYandexMarketAccess", cbAllowPublicYandexMarketAccess.Checked.ToString());
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    //~~ string fileName = string.Format("YandexMarket_{0}_{1}.xml", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), CommonHelper.GenerateRandomDigitCode(4));
                    string fileName = string.Format("yml_feed.xml");
                    string filePath = string.Format("{0}files\\YandexMarket\\{1}", HttpContext.Current.Request.PhysicalApplicationPath, fileName);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {
                        YandexMarketService.GenerateFeed(fs);
                    }

                    string clickhereStr = string.Format("<a href=\"{0}files/YandexMarket/{1}\" target=\"_blank\">{2}</a>", CommonHelper.GetStoreLocation(false), fileName, GetLocaleResourceString("Admin.YandexMarket.ClickHere"));
                    string result = string.Format(GetLocaleResourceString("Admin.YandexMarket.SuccessResult"), clickhereStr);
                    lblResult.Text = result;
                }
                catch (Exception exc)
                {
                    ProcessException(exc);
                }
            }
        }
    }
}