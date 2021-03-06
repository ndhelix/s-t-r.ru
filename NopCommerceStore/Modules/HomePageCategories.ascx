<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.HomePageCategories"
    CodeBehind="HomePageCategories.ascx.cs" %>
<div class="home-page-category-grid">
<%-- ~~ RepeatColumns="3" --%>
    <asp:DataList ID="dlCategories" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
        RepeatLayout="Table" OnItemDataBound="dlCategories_ItemDataBound" ItemStyle-CssClass="item-box">
        <ItemTemplate>
            <div class="category-item">
                <h2 class="title">
                    <asp:HyperLink ID="hlCategory" runat="server" Text='<%#Server.HtmlEncode(Eval("Name").ToString()) %>' />
                    </h2>
                    <br/>
                <div class="picture">
                    <asp:HyperLink ID="hlImageLink" runat="server" />
                </div>
            </div>
        </ItemTemplate>
    </asp:DataList>
</div>
