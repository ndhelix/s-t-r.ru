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
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Installation;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.BusinessLogic.Utils;
using NopSolutions.NopCommerce.Common.Utils;

namespace NopSolutions.NopCommerce.HttpModules
{
    /// <summary>
    /// nopCommerce membership module
    /// </summary>
    public class MembershipHttpModule : IHttpModule
    {
        #region Utilities
        /// <summary>
        /// Logout customer
        /// </summary>
        private void logout()
        {
            CustomerManager.Logout();
            string loginURL = string.Empty;
            if (NopContext.Current != null)
            {
                if (NopContext.Current.IsAdmin)
                    loginURL = SEOHelper.GetAdminAreaLoginPageUrl();
                else
                    loginURL = SEOHelper.GetLoginPageUrl();
                HttpContext.Current.Response.Redirect(loginURL);
            }
        }

        private bool IsAspxPageRequested()
        {
            HttpContext context = HttpContext.Current;
            HttpRequest request = context.Request;

            string requestPath = request.Url.AbsolutePath;
            string extension = System.IO.Path.GetExtension(requestPath);
            bool isAspx = extension.Equals(".aspx", StringComparison.InvariantCultureIgnoreCase);
            return isAspx;
        }

        /// <summary>
        /// Handlers the AuthenticateRequest event of the application
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (InstallerHelper.ConnectionStringIsSet())
            {
                bool authenticated = false;
                if (HttpContext.Current.User != null && HttpContext.Current.User.Identity != null)
                    authenticated = HttpContext.Current.User.Identity.IsAuthenticated;

                if (authenticated)
                {
                    Customer customer = null;
                    string name = HttpContext.Current.User.Identity.Name;
                    if (CustomerManager.UsernamesEnabled)
                    {
                        customer = CustomerManager.GetCustomerByUsername(name);
                    }
                    else
                    {
                        customer = CustomerManager.GetCustomerByEmail(name);
                    }
                    
                    if (customer != null)
                    {
                        var registeredCustomerSession = CustomerManager.GetCustomerSessionByCustomerId(customer.CustomerId);
                        if (registeredCustomerSession == null)
                        {
                            registeredCustomerSession = NopContext.Current.GetSession(true);
                            registeredCustomerSession.IsExpired = false;
                            registeredCustomerSession.LastAccessed = DateTime.UtcNow;
                            registeredCustomerSession.CustomerId = customer.CustomerId;
                            registeredCustomerSession = CustomerManager.SaveCustomerSession(registeredCustomerSession.CustomerSessionGuid, registeredCustomerSession.CustomerId, registeredCustomerSession.LastAccessed, registeredCustomerSession.IsExpired);
                        }

                        if (!String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)
                            && customer.Active
                            && !customer.Deleted && !customer.IsGuest)
                        {
                            NopContext.Current.User = customer;
                            NopContext.Current.Session = registeredCustomerSession;
                        }
                        else
                        {
                            logout();
                        }
                    }
                    else
                    {
                        logout();
                    }
                }
                else
                {
                    if (NopContext.Current.Session != null)
                    {
                        var guestCustomer = NopContext.Current.Session.Customer;
                        if (guestCustomer != null && guestCustomer.Active && !guestCustomer.Deleted && guestCustomer.IsGuest)
                        {
                            NopContext.Current.User = guestCustomer;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handlers the BeginRequest event of the application
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void Application_BeginRequest(object sender, EventArgs e)
        {
            if (InstallerHelper.ConnectionStringIsSet())
            {
                if (NopContext.Current.Session != null)
                {
                    var dtNow = DateTime.UtcNow;
                    if (NopContext.Current.Session.LastAccessed.AddMinutes(1.0) < dtNow)
                    {
                        NopContext.Current.Session.LastAccessed = dtNow;
                        NopContext.Current.Session = CustomerManager.SaveCustomerSession(
                            NopContext.Current.Session.CustomerSessionGuid, 
                            NopContext.Current.Session.CustomerId, 
                            NopContext.Current.Session.LastAccessed,
                            false);
                    }
                }

                var currentLanguage = NopContext.Current.WorkingLanguage;
                if (currentLanguage != null)
                {
                    NopContext.Current.SetCulture(new CultureInfo(currentLanguage.LanguageCulture));
                }
            }
        }

        /// <summary>
        /// Handlers the EndRequest event of the application
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void Application_EndRequest(object sender, EventArgs e)
        {
            if (InstallerHelper.ConnectionStringIsSet())
            {
                try
                {
                    bool sessionReseted = false;
                    if (NopContext.Current["Nop.SessionReseted"] != null)
                    {
                        sessionReseted = Convert.ToBoolean(NopContext.Current["Nop.SessionReseted"]);
                    }
                    if (!sessionReseted)
                    {
                        NopContext.Current.SessionSaveToClient();
                    }
                }
                catch (Exception exc)
                {
                    //LogManager.InsertLog(LogTypeEnum.Unknown, exc.Message, exc);
                    Debug.WriteLine(exc.Message);
                }
            }
        }

        /// <summary>
        /// Handlers the PostAcquireRequestState event of the application
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void Application_PostAcquireRequestState(object sender, EventArgs e)
        {
            if (InstallerHelper.ConnectionStringIsSet())
            {
                
            }
        }

        /// <summary>
        /// Handlers the PostRequestHandlerExecute event of the application
        /// </summary>        
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void application_PostRequestHandlerExecute(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handlers the PreSendRequestContent event of the application
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void application_PreSendRequestContent(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handlers the ReleaseRequestState event of the application
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void Application_ReleaseRequestState(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Methods
        /// <summary>
        ///  Initializes the NopCommerceFilter object
        /// </summary>
        /// <param name="application">The application</param>
        public void Init(HttpApplication application)
        {
            application.BeginRequest += new EventHandler(this.Application_BeginRequest);
            application.EndRequest += new EventHandler(this.Application_EndRequest);
            application.PostAcquireRequestState += new EventHandler(this.Application_PostAcquireRequestState);
            application.ReleaseRequestState += new EventHandler(this.Application_ReleaseRequestState);
            application.AuthenticateRequest += new EventHandler(this.Application_AuthenticateRequest);
            application.PreSendRequestContent += new EventHandler(this.application_PreSendRequestContent);
            application.PostRequestHandlerExecute += new EventHandler(this.application_PostRequestHandlerExecute);
        }

        /// <summary>
        /// Disposes the object
        /// </summary>
        public void Dispose()
        {
        }
        #endregion
    }
}
