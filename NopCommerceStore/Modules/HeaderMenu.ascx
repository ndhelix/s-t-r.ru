<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.HeaderMenuControl"
    CodeBehind="HeaderMenu.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="SearchBox" Src="~/Modules/SearchBox.ascx" %>
<div class="headermenu">
    <div class="searchbox">
        <nopCommerce:SearchBox runat="server" ID="ctrlSearchBox">
        </nopCommerce:SearchBox>
    </div>
    <ul>
        <li><a href="<%=CommonHelper.GetStoreLocation()%>">
            <%=GetLocaleResourceString("Content.HomePage")%></a> </li>
        <% if (ProductManager.RecentlyAddedProductsEnabled)
           { %>
        <li><a href="<%=Page.ResolveUrl("~/recentlyaddedproducts.aspx")%>">
            <%=GetLocaleResourceString("Products.NewProducts")%></a> </li>
        <%} %>
            <li><a href="<%=Page.ResolveUrl("~/aboutus.aspx")%>">
                <%=GetLocaleResourceString("Content.AboutUs")%></a></li>
        <li><a href="<%=Page.ResolveUrl("~/search.aspx")%>">
            <%=GetLocaleResourceString("Search.Search")%></a> </li>
<%-- ~~        <li><a href="<%=Page.ResolveUrl("~/account.aspx")%>">
            <%=GetLocaleResourceString("Account.MyAccount")%></a> </li>
--%>        <% if (BlogManager.BlogEnabled)
           { %>
        <li><a href="<%=Page.ResolveUrl("~/blog.aspx")%>">
            <%=GetLocaleResourceString("Blog.Blog")%></a> </li>
        <%} %>
        <% if (ForumManager.ForumsEnabled)
           { %>
        <li><a href="<%= SEOHelper.GetForumMainUrl()%> ">
            <%=GetLocaleResourceString("Forum.Forums")%></a></li>
        <%} %>
        <li><a href="<%=Page.ResolveUrl("~/contactus.aspx")%>">��������</a> </li>
                        <li><a href="<%=Page.ResolveUrl("~/shippinginfo.aspx")%>">
                <%=GetLocaleResourceString("Content.Shipping&Returns")%></a>
        </li>

        <li><a href="http://s-t-r.ru/topic/11--.aspx">���� �������</a> </li>

    </ul>
</div>
