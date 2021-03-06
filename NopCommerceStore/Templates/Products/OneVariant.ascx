﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OneVariant.ascx.cs"
    Inherits="NopSolutions.NopCommerce.Web.Templates.Products.OneVariant" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductCategoryBreadcrumb" Src="~/Modules/ProductCategoryBreadcrumb.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductRating" Src="~/Modules/ProductRating.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductEmailAFriendButton" Src="~/Modules/ProductEmailAFriendButton.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductAddToCompareList" Src="~/Modules/ProductAddToCompareList.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductSpecs" Src="~/Modules/ProductSpecifications.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="RelatedProducts" Src="~/Modules/RelatedProducts.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductReviews" Src="~/Modules/ProductReviews.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductsAlsoPurchased" Src="~/Modules/ProductsAlsoPurchased.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="SimpleTextBox" Src="~/Modules/SimpleTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="NumericTextBox" Src="~/Modules/NumericTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductAttributes" Src="~/Modules/ProductAttributes.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductPrice" Src="~/Modules/ProductPrice.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="TierPrices" Src="~/Modules/TierPrices.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductTags" Src="~/Modules/ProductTags.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductShareButton" Src="~/Modules/ProductShareButton.ascx" %>

<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
    EnableScriptLocalization="true" ID="sm1" ScriptMode="Release" CompositeScript-ScriptMode="Release" />
<nopCommerce:ProductCategoryBreadcrumb ID="ctrlProductCategoryBreadcrumb" runat="server" />
<div class="clear">
</div>
<div class="product-details-page">
    <div class="product-essential product-details-info">

        <script language="javascript" type="text/javascript">
            function UpdateMainImage(url) {
                var imgMain = document.getElementById('<%=defaultImage.ClientID%>');
                imgMain.src = url;
            }
        </script>

        <div class="overview">
            <h3 class="productname">
                <asp:Literal ID="lProductName" runat="server" />
            </h3>
            <div class="clear">
            </div>
                <%-- ~~ --%>
            <div class="shortdescription" visible=false>
                <asp:Literal ID="lShortDescription" runat="server" Visible=false />
            </div>
                <%-- ~~ --%>
            <div class="product-collateral" visible=false>
                <nopCommerce:ProductRating ID="ctrlProductRating" runat="server" Visible=false />
                <br />
<h3>Цена предоставляется по запросу</h3>
                <div class="one-variant-price">
                    <nopCommerce:ProductPrice ID="ctrlProductPrice2" runat="server" visible=false />
                    <nopCommerce:NumericTextBox runat="server" ID="txtCustomerEnteredPrice" Value="1" visible=false 
                        RequiredErrorMessage="<% $NopResources:Products.CustomerEnteredPrice.EnterPrice %>"
                        MinimumValue="0" MaximumValue="999999" Width="100"></nopCommerce:NumericTextBox>
                </div>
                <div class="add-info">
                    <nopCommerce:NumericTextBox visible=false runat="server" ID="txtQuantity" Value="1" RequiredErrorMessage="<% $NopResources:Products.EnterQuantity %>"
                        RangeErrorMessage="<% $NopResources:Products.QuantityRange %>" MinimumValue="1"
                        MaximumValue="999999" Width="50" />
                    <asp:Button ID="btnAddToCart" visible=false runat="server" OnCommand="OnCommand" Text="<% $NopResources:Products.AddToCart %>"
                        CommandName="AddToCart" CommandArgument='<%#Eval("ProductVariantId")%>' CssClass="productvariantaddtocartbutton" />
                    <asp:Button ID="btnAddToWishlist" runat="server" OnCommand="OnCommand" Text="<% $NopResources:Wishlist.AddToWishlist %>"
                        CommandName="AddToWishlist" CommandArgument='<%#Eval("ProductVariantId")%>' CssClass="productvariantaddtowishlistbutton" />
<h3>На заказ, 2-3 недели.</h3>
<h4>Если у вас есть вопрос по данному товару, напишите нам на
str@s-t-r.ru, и мы вам обязательно ответим.</h4>
                </div>
                <asp:Panel runat="server" ID="pnlDownloadSample" Visible="false" CssClass="one-variant-download-sample">
                    <span class="downloadsamplebutton">
                        <asp:HyperLink runat="server" ID="hlDownloadSample" Text="<% $NopResources:Products.DownloadSample %>" />
                    </span>
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlStockAvailablity" runat="server" class="stock">
                    <asp:Label ID="lblStockAvailablity" runat="server" />
                </asp:Panel>
                <br />
                <nopCommerce:ProductEmailAFriendButton ID="ctrlProductEmailAFriendButton" runat="server" />
                <nopCommerce:ProductAddToCompareList ID="ctrlProductAddToCompareList" runat="server" />
                <div class="clear">
                </div>
                <nopCommerce:ProductShareButton ID="ctrlProductShareButton" runat="server" />
            </div>
        </div>
    </div>
    
            <div class="picture" style="width:10">
            <asp:Image ID="defaultImage" runat="server" />
            <asp:ListView ID="lvProductPictures" runat="server" GroupItemCount="3">
                <LayoutTemplate>
                    <table style="margin-top: 10px;">
                        <asp:PlaceHolder runat="server" ID="groupPlaceHolder"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <GroupTemplate>
                    <tr>
                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td align="left">
                        <a href="<%#PictureManager.GetPictureUrl((int)Eval("PictureId"))%>" rel="lightbox-p"
                            title="<%= lProductName.Text%>">
                            <img src="<%#PictureManager.GetPictureUrl((int)Eval("PictureId"), 70)%>" alt="Product image" /></a>
                    </td>
                </ItemTemplate>
            </asp:ListView>
        </div>

    <div class="clear">
    </div>
    <div class="product-collateral">
        <div class="product-variant-line">
            <asp:Label runat="server" ID="lblError" EnableViewState="false" CssClass="error" />
            <div class="clear">
            </div>
            <nopCommerce:TierPrices ID="ctrlTierPrices" runat="server" />
            <div class="clear">
            </div>
            <div class="attributes">
                <nopCommerce:ProductAttributes ID="ctrlProductAttributes" runat="server" />
            </div>
            <div class="clear">
            </div>
            <asp:Panel ID="pnlGiftCardInfo" runat="server" class="giftCard">
                <dl>
                    <dt>
                        <asp:Label runat="server" ID="lblRecipientName" Text="<% $NopResources:Products.GiftCard.RecipientName %>"
                            AssociatedControlID="txtRecipientName"></asp:Label></dt>
                    <dd>
                        <asp:TextBox runat="server" ID="txtRecipientName"></asp:TextBox></dd>
                    <dt>
                        <asp:Label runat="server" ID="lblRecipientEmail" Text="<% $NopResources:Products.GiftCard.RecipientEmail %>"
                            AssociatedControlID="txtRecipientEmail"></asp:Label></dt>
                    <dd>
                        <asp:TextBox runat="server" ID="txtRecipientEmail"></asp:TextBox></dd>
                    <dt>
                        <asp:Label runat="server" ID="lblSenderName" Text="<% $NopResources:Products.GiftCard.SenderName %>"
                            AssociatedControlID="txtSenderName"></asp:Label></dt>
                    <dd>
                        <asp:TextBox runat="server" ID="txtSenderName"></asp:TextBox></dd>
                    <dt>
                        <asp:Label runat="server" ID="lblSenderEmail" Text="<% $NopResources:Products.GiftCard.SenderEmail %>"
                            AssociatedControlID="txtSenderEmail"></asp:Label></dt>
                    <dd>
                        <asp:TextBox runat="server" ID="txtSenderEmail"></asp:TextBox></dd>
                    <dt>
                        <asp:Label runat="server" ID="lblGiftCardMessage" Text="<% $NopResources:Products.GiftCard.Message %>"
                            AssociatedControlID="txtGiftCardMessage"></asp:Label></dt>
                    <dd>
                        <asp:TextBox runat="server" ID="txtGiftCardMessage" TextMode="MultiLine" Height="100px"
                            Width="300px"></asp:TextBox></dd>
                </dl>
            </asp:Panel>
           <%-- <div class="clear">
            </div>
            <div class="price" style="margin: 10px 0px 10px 0px; float: left;">
                <nopCommerce:ProductPrice ID="ctrlProductPrice" runat="server" />
            </div>--%>
            <div class="clear">
            </div>
            <div class="fulldescription">
                <asp:Literal ID="lFullDescription" runat="server" />
            </div>
        </div>
        <div class="clear">
        </div>
        <div>
            <nopCommerce:ProductsAlsoPurchased ID="ctrlProductsAlsoPurchased" runat="server" />
        </div>
        <div class="clear">
        </div>
        <div>
            <nopCommerce:RelatedProducts ID="ctrlRelatedProducts" runat="server" />
        </div>
        <div class="clear">
        </div>
        <ajaxToolkit:TabContainer runat="server" ID="ProductsTabs" ActiveTabIndex="1" CssClass="grey">
            <ajaxToolkit:TabPanel runat="server" ID="pnlProductReviews" HeaderText="<% $NopResources:Products.ProductReviews %>">
                <ContentTemplate>
                    <nopCommerce:ProductReviews ID="ctrlProductReviews" runat="server" ShowWriteReview="true" />
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" ID="pnlProductSpecs" HeaderText="<% $NopResources:Products.ProductSpecs %>">
                <ContentTemplate>
                    <nopCommerce:ProductSpecs ID="ctrlProductSpecs" runat="server" />
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" ID="pnlProductTags" HeaderText="<% $NopResources:Products.ProductTags %>">
                <ContentTemplate>
                    <nopCommerce:ProductTags ID="ctrlProductTags" runat="server" />
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </div>
</div>
