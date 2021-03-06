﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Audit;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Orders;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.Products.Attributes;
using NopSolutions.NopCommerce.Common.Utils;
using NopSolutions.NopCommerce.Web.Modules;

namespace NopSolutions.NopCommerce.Web.Templates.Products
{
    public partial class OneVariant : BaseNopUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                BindData();
            }
        }

        protected void BindData()
        {
            var product = ProductManager.GetProductById(this.ProductId);
            if(product == null || product.ProductVariants.Count == 0)
            {
                Response.Redirect(CommonHelper.GetStoreLocation());
            }
            //ctrlProductRating.Visible = product.AllowCustomerRatings;
          // ~~
            ctrlProductRating.Visible = false;
            BindProductVariantInfo(ProductVariant);
            BindProductInfo(product);
        }

        protected void BindProductInfo(Product product)
        {
            lProductName.Text = Server.HtmlEncode(product.Name);
            lShortDescription.Text = product.ShortDescription;
            lFullDescription.Text = product.FullDescription;

            var productPictures = product.ProductPictures;
            if(productPictures.Count > 1)
            {
                defaultImage.ImageUrl = PictureManager.GetPictureUrl(productPictures[0].PictureId, SettingManager.GetSettingValueInteger("Media.Product.DetailImageSize", 300));
                defaultImage.ToolTip = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.Name);
                defaultImage.AlternateText = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.Name);
                lvProductPictures.DataSource = productPictures;
                lvProductPictures.DataBind();
            }
            else if(productPictures.Count == 1)
            {
                defaultImage.ImageUrl = PictureManager.GetPictureUrl(productPictures[0].PictureId, SettingManager.GetSettingValueInteger("Media.Product.DetailImageSize", 300));
                defaultImage.ToolTip = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.Name);
                defaultImage.AlternateText = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.Name);
                lvProductPictures.Visible = false;
            }
            else
            {
              //~~
              //defaultImage.Visible = false;
                defaultImage.ImageUrl = "../images/noDefaultImage.gif";// PictureManager.GetDefaultPictureUrl(SettingManager.GetSettingValueInteger("Media.Product.DetailImageSize", 300));
                defaultImage.ToolTip = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.Name);
                defaultImage.AlternateText = String.Format(GetLocaleResourceString("Media.Product.ImageAlternateTextFormat"), product.Name);
                lvProductPictures.Visible = false;
            }
        }
        
        protected void BindProductVariantInfo(ProductVariant productVariant)
        {
            btnAddToWishlist.Visible = SettingManager.GetSettingValueBoolean("Common.EnableWishlist");

            ctrlTierPrices.ProductVariantId = productVariant.ProductVariantId;
            ctrlProductAttributes.ProductVariantId = ProductVariant.ProductVariantId;
            //ctrlProductPrice.ProductVariantId = productVariant.ProductVariantId;
            ctrlProductPrice2.ProductVariantId = productVariant.ProductVariantId;

            //stock
            if(pnlStockAvailablity != null && lblStockAvailablity != null)
            {
                if(productVariant.ManageInventory == (int)ManageInventoryMethodEnum.ManageStock
                        && productVariant.DisplayStockAvailability)
                {
                    if(productVariant.StockQuantity > 0 || productVariant.AllowOutOfStockOrders)
                    {
                        lblStockAvailablity.Text = string.Format(GetLocaleResourceString("Products.Availability"), GetLocaleResourceString("Products.InStock"));
                    }
                    else
                    {
                        lblStockAvailablity.Text = string.Format(GetLocaleResourceString("Products.Availability"), GetLocaleResourceString("Products.OutOfStock"));
                    }
                }
                else
                {
                    pnlStockAvailablity.Visible = false;
                }
            }

            //gift cards
            if(pnlGiftCardInfo != null)
            {
                if(productVariant.IsGiftCard)
                {
                    pnlGiftCardInfo.Visible = true;
                    if(NopContext.Current.User != null && !NopContext.Current.User.IsGuest)
                    {
                        txtSenderName.Text = NopContext.Current.User.FullName;
                        txtSenderEmail.Text = NopContext.Current.User.Email;
                    }
                }
                else
                {
                    pnlGiftCardInfo.Visible = false;
                }
            }
            
            //price entered by a customer
            if (productVariant.CustomerEntersPrice)
            {
                int minimumCustomerEnteredPrice = Convert.ToInt32(Math.Ceiling(CurrencyManager.ConvertCurrency(productVariant.MinimumCustomerEnteredPrice, CurrencyManager.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency)));
                int maximumCustomerEnteredPrice = Convert.ToInt32(Math.Truncate(CurrencyManager.ConvertCurrency(productVariant.MaximumCustomerEnteredPrice, CurrencyManager.PrimaryStoreCurrency, NopContext.Current.WorkingCurrency)));
                txtCustomerEnteredPrice.Visible = true;
                txtCustomerEnteredPrice.ValidationGroup = string.Format("ProductVariant{0}", productVariant.ProductVariantId);
                txtCustomerEnteredPrice.Value = minimumCustomerEnteredPrice;
                txtCustomerEnteredPrice.MinimumValue = minimumCustomerEnteredPrice.ToString();
                txtCustomerEnteredPrice.MaximumValue = maximumCustomerEnteredPrice.ToString();
                txtCustomerEnteredPrice.RangeErrorMessage = string.Format(GetLocaleResourceString("Products.CustomerEnteredPrice.Range"), minimumCustomerEnteredPrice, maximumCustomerEnteredPrice);
            }
            else
            {
                txtCustomerEnteredPrice.Visible = false;
            }

            //buttons
            if(!productVariant.DisableBuyButton)
            {
                txtQuantity.ValidationGroup = string.Format("ProductVariant{0}", productVariant.ProductVariantId);
                btnAddToCart.ValidationGroup = string.Format("ProductVariant{0}", productVariant.ProductVariantId);
                btnAddToWishlist.ValidationGroup = string.Format("ProductVariant{0}", productVariant.ProductVariantId);

                txtQuantity.Value = productVariant.OrderMinimumQuantity;
            }
            else
            {
                txtQuantity.Visible = false;
                btnAddToCart.Visible = false;
                btnAddToWishlist.Visible = false;
            }

            //samle downlaods
            if(pnlDownloadSample != null && hlDownloadSample != null)
            {
                if(productVariant.IsDownload && productVariant.HasSampleDownload)
                {
                    pnlDownloadSample.Visible = true;
                    hlDownloadSample.NavigateUrl = DownloadManager.GetSampleDownloadUrl(productVariant);
                }
                else
                {
                    pnlDownloadSample.Visible = false;
                }
            }

            //final check - hide prices for non-registered customers
            if (!SettingManager.GetSettingValueBoolean("Common.HidePricesForNonRegistered") ||
                    (NopContext.Current.User != null &&
                    !NopContext.Current.User.IsGuest))
            {
                //
            }
            else
            {
                txtCustomerEnteredPrice.Visible = false;
                txtQuantity.Visible = false;
                btnAddToCart.Visible = false;
                btnAddToWishlist.Visible = false;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            pnlProductReviews.Visible = ctrlProductReviews.Visible;
            pnlProductSpecs.Visible = ctrlProductSpecs.Visible;
            pnlProductTags.Visible = ctrlProductTags.Visible;
            ProductsTabs.Visible = pnlProductReviews.Visible ||
                pnlProductSpecs.Visible ||
                pnlProductTags.Visible;

            //little hack here
            if (pnlProductTags.Visible)
                ProductsTabs.ActiveTab = pnlProductTags;
            if (pnlProductSpecs.Visible)
                ProductsTabs.ActiveTab = pnlProductSpecs;
            if (pnlProductReviews.Visible)
                ProductsTabs.ActiveTab = pnlProductReviews;

            BindJQuery();

            string slimBox = CommonHelper.GetStoreLocation() + "Scripts/slimbox2.js";
            Page.ClientScript.RegisterClientScriptInclude(slimBox, slimBox);

            base.OnPreRender(e);
        }

        public int ProductId
        {
            get
            {
                return CommonHelper.QueryStringInt("ProductId");
            }
        }

        public ProductVariant ProductVariant
        {
            get
            {
                Product product = ProductManager.GetProductById(this.ProductId);
                if(product == null && product.ProductVariants.Count == 0)
                {
                    return null;
                }
                return product.ProductVariants[0];
            }
        }

        protected void OnCommand(object source, CommandEventArgs e)
        {
            var pv = ProductVariant;
            if(pv == null)
            {
                return;
            }

            string attributes = ctrlProductAttributes.SelectedAttributes;
            decimal customerEnteredPrice = txtCustomerEnteredPrice.Value;
            decimal customerEnteredPriceConverted = CurrencyManager.ConvertCurrency(customerEnteredPrice, NopContext.Current.WorkingCurrency, CurrencyManager.PrimaryStoreCurrency);
            int quantity = txtQuantity.Value;

            //gift cards
            if(pv.IsGiftCard)
            {
                string recipientName = txtRecipientName.Text;
                string recipientEmail = txtRecipientEmail.Text;
                string senderName = txtSenderName.Text;
                string senderEmail = txtSenderEmail.Text;
                string giftCardMessage = txtGiftCardMessage.Text;

                attributes = ProductAttributeHelper.AddGiftCardAttribute(attributes, recipientName, recipientEmail, senderName, senderEmail, giftCardMessage);
            }

            try
            {
                if(e.CommandName == "AddToCart")
                {
                    string sep = "<br />";
                    var addToCartWarnings = ShoppingCartManager.AddToCart(
                        ShoppingCartTypeEnum.ShoppingCart,
                        pv.ProductVariantId, 
                        attributes,
                        customerEnteredPriceConverted,
                        quantity);
                    if(addToCartWarnings.Count == 0)
                    {
                        Response.Redirect("~/shoppingcart.aspx");
                    }
                    else
                    {
                        var addToCartWarningsSb = new StringBuilder();
                        for(int i = 0; i < addToCartWarnings.Count; i++)
                        {
                            addToCartWarningsSb.Append(Server.HtmlEncode(addToCartWarnings[i]));
                            if(i != addToCartWarnings.Count - 1)
                            {
                                addToCartWarningsSb.Append(sep);
                            }
                        }
                        string errorFull = addToCartWarningsSb.ToString();
                        lblError.Text = errorFull;
                        if (SettingManager.GetSettingValueBoolean("Common.ShowAlertForProductAttributes"))
                        {
                            this.DisplayAlertMessage(errorFull.Replace(sep, "\\n"));
                        }
                    }
                }

                if(e.CommandName == "AddToWishlist")
                {
                    string sep = "<br />";
                    var addToCartWarnings = ShoppingCartManager.AddToCart(
                        ShoppingCartTypeEnum.Wishlist,
                        pv.ProductVariantId, 
                        attributes,
                        customerEnteredPriceConverted,
                        quantity);
                    if(addToCartWarnings.Count == 0)
                    {
                        Response.Redirect("~/wishlist.aspx");
                    }
                    else
                    {
                        var addToCartWarningsSb = new StringBuilder();
                        for(int i = 0; i < addToCartWarnings.Count; i++)
                        {
                            addToCartWarningsSb.Append(Server.HtmlEncode(addToCartWarnings[i]));
                            if(i != addToCartWarnings.Count - 1)
                            {
                                addToCartWarningsSb.Append(sep);
                            }
                        }
                        string errorFull = addToCartWarningsSb.ToString();
                        lblError.Text = errorFull;
                        if (SettingManager.GetSettingValueBoolean("Common.ShowAlertForProductAttributes"))
                        {
                            this.DisplayAlertMessage(errorFull.Replace(sep, "\\n"));
                        }
                    }
                }
            }
            catch(Exception exc)
            {
                LogManager.InsertLog(LogTypeEnum.CustomerError, exc.Message, exc);
                lblError.Text = Server.HtmlEncode(exc.Message);
            }
        }
    }
}