<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.ProductBox2Control"
    CodeBehind="ProductBox2.ascx.cs" %>
<div class="product-item">
    <h2 class="product-title">
        <asp:HyperLink ID="hlProduct" runat="server" />
    </h2>
    <div class="picture">
        <asp:HyperLink ID="hlImageLink" runat="server" />
    </div>
    <div class="description">
        <asp:Literal runat="server" ID="lShortDescription"></asp:Literal>
    </div>
    <div class="prices-wrapper">
        <div class="prices">
            <asp:Label ID="lblOldPrice" visible=false runat="server" CssClass="oldproductPrice" />
            <br />
            <asp:Label ID="lblPrice" visible=false runat="server" CssClass="productPrice" />
        </div>
        <div class="buttons">
            <asp:Button runat="server" ID="btnProductDetails" OnCommand="btnProductDetails_Click"
                Text="<% $NopResources:Products.ProductDetails %>" ValidationGroup="ProductDetails"
                CommandArgument='<%# Eval("ProductId") %>' CssClass="productlistproductdetailbutton" />
            <br />
            <asp:Button runat="server" ID="btnAddToCart" OnCommand="btnAddToCart_Click" Text=""
                ValidationGroup="ProductDetails" CommandArgument='<%# Eval("ProductId") %>' CssClass="productlistaddtocartbutton" visible=false />
        </div>
    </div>
</div>
