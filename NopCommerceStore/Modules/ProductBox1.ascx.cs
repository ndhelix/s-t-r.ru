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
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Localization;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.BusinessLogic.Tax;
using NopSolutions.NopCommerce.Common.Utils;

namespace NopSolutions.NopCommerce.Web.Modules
{
    public partial class ProductBox1Control : BaseNopUserControl
    {
        Product product = null;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public override void DataBind()
        {
            base.DataBind();
            this.BindData();
        }

        private void BindData()
        {
            if (product != null)
            {
                string productURL = SEOHelper.GetProductUrl(product);

                hlProduct.NavigateUrl = productURL;
                hlProduct.Text = Server.HtmlEncode(product.Name);

                ProductPicture productPicture = product.DefaultProductPicture;
                if(productPicture != null)
                {
                    hlImageLink.ImageUrl = PictureManager.GetPictureUrl(productPicture.Picture, this.ProductImageSize, true);
                    hlImageLink.ToolTip = String.Format(GetLocaleResourceString("Media.Product.ImageLinkTitleFormat"), product.Name);
                    hlImageLink.Text = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.Name);
                }
                else
                {
                    hlImageLink.ImageUrl = PictureManager.GetDefaultPictureUrl(this.ProductImageSize);
                    hlImageLink.ToolTip = String.Format(GetLocaleResourceString("Media.Product.ImageLinkTitleFormat"), product.Name);
                    hlImageLink.Text = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.Name);
                }
                hlImageLink.NavigateUrl = productURL;

                lShortDescription.Text = product.ShortDescription;

                var productVariantCollection = product.ProductVariants;
                if (productVariantCollection.Count > 0)
                {
                    if (!product.HasMultipleVariants)
                    {
                        var productVariant = productVariantCollection[0];
                        btnAddToCart.Visible = (!productVariant.DisableBuyButton);

                        if (!SettingManager.GetSettingValueBoolean("Common.HidePricesForNonRegistered") ||
                            (NopContext.Current.User != null &&
                            !NopContext.Current.User.IsGuest))
                        {
                            if (productVariant.CustomerEntersPrice)
                            {
                                lblOldPrice.Visible = false;
                                lblPrice.Visible = false;
                            }
                            else
                            {
                                decimal oldPriceBase = TaxManager.GetPrice(productVariant, productVariant.OldPrice);
                                decimal finalPriceWithoutDiscountBase = TaxManager.GetPrice(productVariant, PriceHelper.GetFinalPrice(productVariant, false));

                                decimal oldPrice = CurrencyManager.ConvertCurrency(oldPriceBase, CurrencyManager.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                                decimal finalPriceWithoutDiscount = CurrencyManager.ConvertCurrency(finalPriceWithoutDiscountBase, CurrencyManager.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);

                                if (finalPriceWithoutDiscountBase != oldPriceBase && oldPriceBase != decimal.Zero)
                                {
                                    lblOldPrice.Text = PriceHelper.FormatPrice(oldPrice);
                                    lblPrice.Text = PriceHelper.FormatPrice(finalPriceWithoutDiscount);
                                }
                                else
                                {
                                    lblOldPrice.Visible = false;
                                    lblPrice.Text = PriceHelper.FormatPrice(finalPriceWithoutDiscount);
                                }
                            }
                        }
                        else
                        {
                            lblOldPrice.Visible = false;
                            lblPrice.Visible = false;
                            btnAddToCart.Visible = false;
                        }
                    }
                    else
                    {
                        var productVariant = product.MinimalPriceProductVariant;
                        if (productVariant != null)
                        {
                            if (!SettingManager.GetSettingValueBoolean("Common.HidePricesForNonRegistered") ||
                                (NopContext.Current.User != null &&
                                !NopContext.Current.User.IsGuest))
                            {
                                if (productVariant.CustomerEntersPrice)
                                {
                                    lblOldPrice.Visible = false;
                                    lblPrice.Visible = false;
                                }
                                else
                                {
                                    decimal fromPriceBase = TaxManager.GetPrice(productVariant, PriceHelper.GetFinalPrice(productVariant, false));
                                    decimal fromPrice = CurrencyManager.ConvertCurrency(fromPriceBase, CurrencyManager.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency);
                                    lblOldPrice.Visible = false;
                                    lblPrice.Text = String.Format(GetLocaleResourceString("Products.PriceRangeFromText"), PriceHelper.FormatPrice(fromPrice));
                                }
                            }
                            else
                            {
                                lblOldPrice.Visible = false;
                                lblPrice.Visible = false;
                                btnAddToCart.Visible = false;
                            }
                        }

                        btnAddToCart.Visible = false;
                    }
                }
                else
                {
                    lblOldPrice.Visible = false;
                    lblPrice.Visible = false;
                    btnAddToCart.Visible = false;
                }
            }
        }

        protected void btnProductDetails_Click(object sender, CommandEventArgs e)
        {
            int productId = Convert.ToInt32(e.CommandArgument);
            string productURL = SEOHelper.GetProductUrl(productId);
            Response.Redirect(productURL);
        }

        protected void btnAddToCart_Click(object sender, CommandEventArgs e)
        {
            int productId = Convert.ToInt32(e.CommandArgument);
            int productVariantId = 0;
            if (ProductManager.DirectAddToCartAllowed(productId, out productVariantId))
            {
                var addToCartWarnings = ShoppingCartManager.AddToCart(ShoppingCartTypeEnum.ShoppingCart,
                    productVariantId, string.Empty, decimal.Zero, 1);
                if (addToCartWarnings.Count == 0)
                {
                    Response.Redirect("~/shoppingcart.aspx");
                }
                else
                {
                    string productURL = SEOHelper.GetProductUrl(productId);
                    Response.Redirect(productURL);
                }
            }
            else
            {
                string productURL = SEOHelper.GetProductUrl(productId);
                Response.Redirect(productURL);
            }
        }

        public Product Product
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
            }
        }

        public int ProductImageSize
        {
            get
            {
                if (ViewState["ProductImageSize"] == null)
                    return SettingManager.GetSettingValueInteger("Media.Product.ThumbnailImageSize", 125);
                else
                    return (int)ViewState["ProductImageSize"];
            }
            set
            {
                ViewState["ProductImageSize"] = value;
            }
        }
    }
}