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
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using NopSolutions.NopCommerce.BusinessLogic.Caching;
using NopSolutions.NopCommerce.BusinessLogic.CustomerManagement;
using NopSolutions.NopCommerce.BusinessLogic.Messages;
using NopSolutions.NopCommerce.BusinessLogic.Profile;
using NopSolutions.NopCommerce.Common;
using NopSolutions.NopCommerce.DataAccess;
using NopSolutions.NopCommerce.DataAccess.Promo.Campaigns;
 

namespace NopSolutions.NopCommerce.BusinessLogic.Promo.Campaigns
{
    /// <summary>
    /// Message campaign manager
    /// </summary>
    public partial class CampaignManager
    {
        #region Utilities
        private static CampaignCollection DBMapping(DBCampaignCollection dbCollection)
        {
            if (dbCollection == null)
                return null;

            var collection = new CampaignCollection();
            foreach (var dbItem in dbCollection)
            {
                var item = DBMapping(dbItem);
                collection.Add(item);
            }

            return collection;
        }

        private static Campaign DBMapping(DBCampaign dbItem)
        {
            if (dbItem == null)
                return null;

            var item = new Campaign();
            item.CampaignId = dbItem.CampaignId;
            item.Name = dbItem.Name;
            item.Subject = dbItem.Subject;
            item.Body = dbItem.Body;
            item.CreatedOn = dbItem.CreatedOn;

            return item;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets a campaign by campaign identifier
        /// </summary>
        /// <param name="campaignId">Campaign identifier</param>
        /// <returns>Message template</returns>
        public static Campaign GetCampaignById(int campaignId)
        {
            if (campaignId == 0)
                return null;

            var dbItem = DBProviderManager<DBCampaignProvider>.Provider.GetCampaignById(campaignId);
            var campaign = DBMapping(dbItem);
            return campaign;
        }

        /// <summary>
        /// Deletes a campaign
        /// </summary>
        /// <param name="campaignId">Campaign identifier</param>
        public static void DeleteCampaign(int campaignId)
        {
            DBProviderManager<DBCampaignProvider>.Provider.DeleteCampaign(campaignId);
        }

        /// <summary>
        /// Gets all campaigns
        /// </summary>
        /// <returns>Campaign collection</returns>
        public static CampaignCollection GetAllCampaigns()
        {
            var dbCollection = DBProviderManager<DBCampaignProvider>.Provider.GetAllCampaigns();
            var campaigns = DBMapping(dbCollection);
            return campaigns;
        }

        /// <summary>
        /// Inserts a campaign
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="subject">The subject</param>
        /// <param name="body">The body</param>
        /// <param name="createdOn">The date and time of instance creation</param>
        /// <returns>Campaign</returns>
        public static Campaign InsertCampaign(string name,
            string subject, string body, DateTime createdOn)
        {
            createdOn = DateTimeHelper.ConvertToUtcTime(createdOn);

            var dbItem = DBProviderManager<DBCampaignProvider>.Provider.InsertCampaign(name, 
                subject, body, createdOn);
            var campaign = DBMapping(dbItem);
            return campaign;
        }

        /// <summary>
        /// Updates the campaign
        /// </summary>
        /// <param name="campaignId">The campaign identifier</param>
        /// <param name="name">The name</param>
        /// <param name="subject">The subject</param>
        /// <param name="body">The body</param>
        /// <param name="createdOn">The date and time of instance creation</param>
        /// <returns>Campaign</returns>
        public static Campaign UpdateCampaign(int campaignId,
            string name, string subject, string body, DateTime createdOn)
        {
            createdOn = DateTimeHelper.ConvertToUtcTime(createdOn);

            var dbItem = DBProviderManager<DBCampaignProvider>.Provider.UpdateCampaign(campaignId, 
                name, subject, body, createdOn);
            var campaign = DBMapping(dbItem);
            return campaign;
        }

        /// <summary>
        /// Sends a campaign to specified emails
        /// </summary>
        /// <param name="campaignId">Campaign identifier</param>
        /// <param name="subscriptions">Subscriptions</param>
        /// <returns>Total emails sent</returns>
        public static int SendCampaign(int campaignId, 
            NewsLetterSubscriptionCollection subscriptions)
        {
            int totalEmailsSent = 0;
            var campaign = GetCampaignById(campaignId);

            if(campaign == null)
            {
                throw new NopException("Campaign could not be loaded");
            }

            foreach (var subscription in subscriptions)
            {
                string subject = MessageManager.ReplaceMessageTemplateTokens(subscription, campaign.Subject);
                string body = MessageManager.ReplaceMessageTemplateTokens(subscription, campaign.Body);
                var from = new MailAddress(MessageManager.AdminEmailAddress, MessageManager.AdminEmailDisplayName);
                var to = new MailAddress(subscription.Email);
                MessageManager.InsertQueuedEmail(3, from, to, string.Empty, string.Empty, subject, body, DateTime.Now, 0, null);
                totalEmailsSent++;
            }
            return totalEmailsSent;
        }

        /// <summary>
        /// Sends a campaign to specified email
        /// </summary>
        /// <param name="campaignId">Campaign identifier</param>
        /// <param name="email">Email</param>
        public static void SendCampaign(int campaignId, string email)
        {
            var campaign = GetCampaignById(campaignId);
            if(campaign == null)
            {
                throw new NopException("Campaign could not be loaded");
            }

            string subject = campaign.Subject;
            string body = campaign.Body;
            var from = new MailAddress(MessageManager.AdminEmailAddress, MessageManager.AdminEmailDisplayName);
            var to = new MailAddress(email);
            MessageManager.SendEmail(subject, body, from, to);
        }
        #endregion
    }
}
