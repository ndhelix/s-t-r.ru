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

namespace NopSolutions.NopCommerce.DataAccess.Products.Specs
{
    /// <summary>
    /// Represents a localized specification attribute
    /// </summary>
    public partial class DBSpecificationAttributeLocalized : BaseDBEntity
    {
        #region Ctor
        /// <summary>
        /// Creates a new instance of the DBSpecificationAttributeLocalized class
        /// </summary>
        public DBSpecificationAttributeLocalized()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the localized specification attribute identifier
        /// </summary>
        public int SpecificationAttributeLocalizedId { get; set; }

        /// <summary>
        /// Gets or sets the specification attribute identifier
        /// </summary>
        public int SpecificationAttributeId { get; set; }

        /// <summary>
        /// Gets or sets the language identifier
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}
