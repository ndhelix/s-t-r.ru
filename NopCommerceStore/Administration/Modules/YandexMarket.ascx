<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Administration.Modules.YandexMarketControl"
    CodeBehind="YandexMarket.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="NumericTextBox" Src="NumericTextBox.ascx" %>
<div class="section-header">
    <div class="title">
        <img src="Common/ico-promotions.png" alt="<%=GetLocaleResourceString("Admin.YandexMarket.Title")%>" />
        <%=GetLocaleResourceString("Admin.YandexMarket.Title")%>
    </div>
    <div class="options">
        <asp:Button runat="server" Text="<% $NopResources:Admin.YandexMarket.SaveButton.Text %>"
            CssClass="adminButtonBlue" ID="btnSave" ValidationGroup="CategorySettings" OnClick="btnSave_Click"
            ToolTip="<% $NopResources:Admin.YandexMarket.SaveButton.ToolTip%>" />
        <asp:Button runat="server" Text="<% $NopResources:Admin.YandexMarket.GenerateButton.Text %>"
            CssClass="adminButtonBlue" ID="btnGenerate" ValidationGroup="GenerateYandexMarket"
            OnClick="btnGenerate_Click" ToolTip="<% $NopResources:Admin.YandexMarket.GenerateButton.Tooltip %>" />
    </div>
</div>
<table width="100%">
    <tr>
        <td class="adminTitle">
            <%=GetLocaleResourceString("Admin.YandexMarket.AllowPublicAccess")%>
        </td>
        <td class="adminData">
            <asp:CheckBox runat="server" ID="cbAllowPublicYandexMarketAccess" />
        </td>
    </tr>
</table>
<p>
</p>
<br />
<asp:Label runat="server" ID="lblResult" EnableViewState="false" />
