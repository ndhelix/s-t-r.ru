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
using System.Text;


namespace NopSolutions.NopCommerce.DataAccess.Orders
{
    /// <summary>
    /// Represents an order status
    /// </summary>
    public partial class DBOrderStatus : BaseDBEntity
    {
        #region Ctor
        /// <summary>
        /// Creates a new instance of the DBOrderStatus class
        /// </summary>
        public DBOrderStatus()
        {
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the order status identifier
        /// </summary>
        public int OrderStatusId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        #endregion 
    }

}
