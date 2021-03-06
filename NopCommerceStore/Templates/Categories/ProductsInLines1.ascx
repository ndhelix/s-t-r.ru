<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Templates.Categories.ProductsInLines1"
    CodeBehind="ProductsInLines1.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductBox2" Src="~/Modules/ProductBox2.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductBox1" Src="~/Modules/ProductBox1.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="PriceRangeFilter" Src="~/Modules/PriceRangeFilter.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ProductSpecificationFilter" Src="~/Modules/ProductSpecificationFilter.ascx" %>
<div class="category-page">
    <div class="breadcrumb">
        <a href='<%=CommonHelper.GetStoreLocation()%>'>
            <%=GetLocaleResourceString("Breadcrumb.Top")%></a> /
        <asp:Repeater ID="rptrCategoryBreadcrumb" runat="server">
            <ItemTemplate>
                <a href='<%#SEOHelper.GetCategoryUrl(Convert.ToInt32(Eval("CategoryId"))) %>'>
                    <%#Server.HtmlEncode(Eval("Name").ToString()) %></a>
            </ItemTemplate>
            <SeparatorTemplate>
                /
            </SeparatorTemplate>
        </asp:Repeater>
        <br />
    </div>
    <div class="clear">
    </div>
    <div class="category-description">
        <asp:Literal runat="server" ID="lDescription"></asp:Literal>
    </div>
    <div class="clear">
    </div>
    <div class="sub-category-list">
        <asp:Repeater ID="rptrSubCategories" runat="server" OnItemDataBound="rptrSubCategories_ItemDataBound">
            <ItemTemplate>
                <asp:HyperLink ID="hlCategory" runat="server" />
            </ItemTemplate>
            <SeparatorTemplate>
                <br />
            </SeparatorTemplate>
        </asp:Repeater>
    </div>
    <div class="clear">
    </div>
    <asp:Panel runat="server" ID="pnlFeaturedProducts" class="featured-product-grid">
        <div class="title">
            <%=GetLocaleResourceString("Products.FeaturedProducts")%>
        </div>
        <div>
            <asp:DataList ID="dlFeaturedProducts" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                RepeatLayout="Table" ItemStyle-CssClass="item-box">
                <ItemTemplate>
                    <nopcommerce:productbox1 id="ctrlProductBox" product='<%# Container.DataItem %>'
                        runat="server" />
                </ItemTemplate>
            </asp:DataList>
        </div>
    </asp:Panel>
    <div class="clear">
    </div>
    <asp:Panel runat="server" ID="pnlSorting" CssClass="product-sorting">
        <%=GetLocaleResourceString("ProductSorting.SortBy")%>
        <asp:DropDownList ID="ddlSorting" runat="server" OnSelectedIndexChanged="ddlSorting_SelectedIndexChanged"
            AutoPostBack="true" />
    </asp:Panel>
    <div class="clear">
    </div>
    <asp:Panel runat="server" ID="pnlFilters" CssClass="product-filters">
        <div class="filter-title">
            <asp:Label runat="server" ID="lblProductFilterTitle">
                <%=GetLocaleResourceString("Products.FilterOptionsTitle")%>
            </asp:Label>
        </div>
        <div class="filter-item">
            <nopcommerce:pricerangefilter id="ctrlPriceRangeFilter" runat="server" />
        </div>
        <div class="filter-item">
            <nopcommerce:productspecificationfilter id="ctrlProductSpecificationFilter" runat="server" />
        </div>
    </asp:Panel>
    <div class="clear">
    </div>
    <div class="product-list1">
        <asp:ListView ID="lvCatalog" runat="server">
            <LayoutTemplate>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
            </LayoutTemplate>
            <ItemTemplate>
                <div class="item-box">
                    <nopcommerce:productbox2 id="ctrlProductBox" product='<%# Container.DataItem %>'
                        runat="server" />
                </div>
            </ItemTemplate>
        </asp:ListView>
        <div class="product-pager">
            <nopcommerce:pager runat="server" id="catalogPager" firstbuttontext="<% $NopResources:Pager.First %>"
                lastbuttontext="<% $NopResources:Pager.Last %>" nextbuttontext="<% $NopResources:Pager.Next %>"
                previousbuttontext="<% $NopResources:Pager.Previous %>" currentpagetext="Pager.CurrentPage" />
        </div>
    </div>
</div>
