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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using NopSolutions.NopCommerce.BusinessLogic;
using NopSolutions.NopCommerce.BusinessLogic.Configuration.Settings;
using NopSolutions.NopCommerce.BusinessLogic.Directory;
using NopSolutions.NopCommerce.BusinessLogic.Categories;
using NopSolutions.NopCommerce.BusinessLogic.Measures;
using NopSolutions.NopCommerce.BusinessLogic.Media;
using NopSolutions.NopCommerce.BusinessLogic.Products;
using NopSolutions.NopCommerce.BusinessLogic.SEO;
using NopSolutions.NopCommerce.BusinessLogic.Utils;
using NopSolutions.NopCommerce.DataAccess.Categories;
//using NopSolutions.NopCommerce.Shipping;

namespace NopSolutions.NopCommerce.YandexMarket
{
    /// <summary>
    /// YandexMarket service
    /// </summary>
    public static partial class YandexMarketService
    {
        /// <summary>
        /// Generate YandexMarket feed
        /// </summary>
        /// <param name="stream">Stream</param>
        public static void GenerateFeed(Stream stream)
        {
            const string googleBaseNamespace = "http://base.google.com/ns/1.0";

            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.GetEncoding(1251),
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n"
            };
            using (var writer = XmlWriter.Create(stream, settings))
            {
              
              writer.WriteStartDocument();
//                writer.WriteDocType("yml_catalog", null, null, "SYSTEM \"shops.dtd\"");
                writer.WriteDocType("yml_catalog", null, "shops.dtd", null);
                //writer.WriteDocType("book", null, null, "<!ENTITY h \"hardcover\">");
                writer.WriteStartElement("yml_catalog");
                writer.WriteAttributeString("date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
//                writer.WriteAttributeString("version", "1.0");
                //writer.WriteAttributeString("xmlns", "g", null, googleBaseNamespace);
                writer.WriteStartElement("shop");
                writer.WriteElementString("name", string.Format("{0}", SettingManager.StoreName));
                writer.WriteElementString("company",  SettingManager.CompanyFullName);
                writer.WriteElementString("url", "http://s-t-r.ru");

              writer.WriteStartElement("currencies");
              writer.WriteStartElement("currency");
              writer.WriteAttributeString("id", "RUR");
              writer.WriteAttributeString("rate", "1");
              writer.WriteEndElement();//currency
              writer.WriteEndElement();//currencies

              writer.WriteStartElement("categories");
              CategoryCollection cc = CategoryManager.GetEverythingCategories();
              foreach (Category c in cc)
              {
                //writer.WriteElementString("category", c.Name);
                writer.WriteStartElement("category");
                writer.WriteAttributeString("id", c.CategoryId.ToString());
                if (c.ParentCategoryId > 0)
                  writer.WriteAttributeString("parentId", c.ParentCategoryId.ToString());
                writer.WriteString(c.Name);
                writer.WriteEndElement();//category
              }
              writer.WriteEndElement(); //categories

              writer.WriteElementString("local_delivery_cost", SettingManager.GetSettingValueDecimalNative("ShippingRateComputationMethod.FixedRate.Rate").ToString());

              writer.WriteStartElement("offers");

                var products = ProductManager.GetAllProducts(false);
                foreach (var product in products)
                {
                    var productVariants = ProductManager.GetProductVariantsByProductId(product.ProductId, false);

                    foreach (var productVariant in productVariants)
                    {
                      if (!productVariant.AllowYandexMarket)
                      {
                        continue;
                      }
                      if (product.Deleted)
                      {
                        continue;
                      }
                      writer.WriteStartElement("offer");
//                      writer.WriteAttributeString("AllowYandexMarket", productVariant.AllowYandexMarket.ToString());

                      writer.WriteAttributeString("id", productVariant.ProductVariantId.ToString());
                      writer.WriteAttributeString("available", "false");

                      writer.WriteElementString("url", SEOHelper.GetProductUrl(product));
                      writer.WriteElementString("price", productVariant.Price.ToString("F", new CultureInfo("en-US", false)));
                      writer.WriteElementString("currencyId", "RUR");
                      writer.WriteElementString("categoryId", product.ProductCategories[0].CategoryId.ToString());
                      writer.WriteElementString("delivery", "true");

                      writer.WriteElementString("name", productVariant.FullProductName);
                      writer.WriteElementString("sales_notes", "Только предоплата.");
//                      writer.WriteElementString("sales_notes", "Только предоплата. Минимальный заказ: 10000 руб.");
                      //writer.WriteStartElement("description");
                      //string description = productVariant.Description;
                      //if (String.IsNullOrEmpty(description))
                      //    description = product.FullDescription;
                      //if (String.IsNullOrEmpty(description))
                      //    description = product.ShortDescription;
                      //if (String.IsNullOrEmpty(description))
                      //    description = product.Name;
                      //writer.WriteCData(description);
                      //writer.WriteEndElement(); // description
                      
                      //writer.WriteStartElement("g", "brand", googleBaseNamespace);
                      //writer.WriteFullEndElement(); // g:brand
                      //writer.WriteElementString("g", "condition", googleBaseNamespace, "new");
                      //writer.WriteElementString("g", "expiration_date", googleBaseNamespace, DateTime.Now.AddDays(28).ToString("yyyy-MM-dd"));
                      //writer.WriteElementString("g", "id", googleBaseNamespace, productVariant.ProductVariantId.ToString());
                      //string imageUrl = string.Empty;
                      //var productPictures = product.ProductPictures;
                      //if (productPictures.Count > 0)
                      //    imageUrl = PictureManager.GetPictureUrl(productPictures[0].Picture, SettingManager.GetSettingValueInteger("Media.Product.ThumbnailImageSize"), true);
                      //writer.WriteElementString("g", "image_link", googleBaseNamespace, imageUrl);
                      //decimal price = productVariant.Price;
                      
                      //uncomment and set your product_type attribute
                      //writer.WriteStartElement("g", "product_type", googleBaseNamespace);
                      //writer.WriteCData("Clothing & Accessories > Clothing Accessories > Hair Accessories > Hair Pins & Clips");
                      //writer.WriteFullEndElement(); // g:brand


                      //if (productVariant.Weight != decimal.Zero)
                      //{
                      //    writer.WriteElementString("g", "weight", googleBaseNamespace, string.Format(CultureInfo.InvariantCulture, "{0} {1}", productVariant.Weight.ToString(new CultureInfo("en-US", false).NumberFormat), MeasureManager.BaseWeightIn.SystemKeyword));
                      //}
                      writer.WriteEndElement(); // offer
                    }
                }

                writer.WriteEndElement();//offers

              writer.WriteEndElement(); // shop
                writer.WriteEndElement(); // yml_catalog
                writer.WriteEndDocument();
            }
        }
    }
}
