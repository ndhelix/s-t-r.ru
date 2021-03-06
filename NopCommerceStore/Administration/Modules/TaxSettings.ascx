<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Administration.Modules.TaxSettingsControl"
    CodeBehind="TaxSettings.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="DecimalTextBox" Src="DecimalTextBox.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="ToolTipLabel" Src="ToolTipLabelControl.ascx" %>
<div class="section-header">
    <div class="title">
        <img src="Common/ico-configuration.png" alt="<%=GetLocaleResourceString("Admin.TaxSettings.Title")%>" />
        <%=GetLocaleResourceString("Admin.TaxSettings.Title")%>
    </div>
    <div class="options">
        <asp:Button runat="server" Text="<% $NopResources:Admin.TaxSettings.SaveButton.Text %>"
            CssClass="adminButtonBlue" ID="btnSave" OnClick="btnSave_Click" ToolTip="<% $NopResources:Admin.TaxSettings.SaveButton.Tooltip %>" />
    </div>
</div>
<table width="100%" class="adminContent">
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblPricesIncludeTax" Text="<% $NopResources:Admin.TaxSettings.PricesIncludeTax %>"
                ToolTip="<% $NopResources:Admin.TaxSettings.PricesIncludeTax.Tooltip %>" ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbPricesIncludeTax" runat="server"></asp:CheckBox>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblAllowCustomersToSelectTaxDisplayType"
                Text="<% $NopResources:Admin.TaxSettings.AllowSelectTaxDisplayType %>" ToolTip="<% $NopResources:Admin.TaxSettings.AllowSelectTaxDisplayType.Tooltip %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbAllowCustomersToSelectTaxDisplayType" runat="server" AutoPostBack="true"
                OnCheckedChanged="cbAllowCustomersToSelectTaxDisplayType_CheckedChanged"></asp:CheckBox>
        </td>
    </tr>
    <tr runat="server" id="pnlTaxDisplayType">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblTaxDisplayType" Text="<% $NopResources:Admin.TaxSettings.TaxDisplayType %>"
                ToolTip="<% $NopResources:Admin.TaxSettings.TaxDisplayType.Tooltip %>" ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlTaxDisplayType" runat="server" CssClass="adminInput">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblDisplayTaxSuffix" Text="<% $NopResources:Admin.TaxSettings.DisplayTaxSuffix %>"
                ToolTip="<% $NopResources:Admin.TaxSettings.DisplayTaxSuffix.Tooltip %>" ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbDisplayTaxSuffix" runat="server"></asp:CheckBox>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblHideZeroTax" Text="<% $NopResources:Admin.TaxSettings.HideZeroTax %>"
                ToolTip="<% $NopResources:Admin.TaxSettings.HideZeroTax.Tooltip %>" ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbHideZeroTax" runat="server"></asp:CheckBox>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblHideTaxInOrderSummary" Text="<% $NopResources:Admin.TaxSettings.HideTaxInOrderSummary %>"
                ToolTip="<% $NopResources:Admin.TaxSettings.HideTaxInOrderSummary.Tooltip %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbHideTaxInOrderSummary" runat="server"></asp:CheckBox>
        </td>
    </tr>
    <tr class="adminSeparator">
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblTaxBasedOn" Text="<% $NopResources:Admin.TaxSettings.TaxBasedOn %>"
                ToolTip="<% $NopResources:Admin.TaxSettings.TaxBasedOn.Tooltip %>" ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlTaxBasedOn" runat="server" CssClass="adminInput">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblTaxDefaultCountry" Text="<% $NopResources:Admin.TaxSettings.TaxDefaultCountry %>"
                ToolTip="<% $NopResources:Admin.TaxSettings.TaxDefaultCountry.Tooltip %>" ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlTaxDefaultCountry" AutoPostBack="True" runat="server" CssClass="adminInput"
                OnSelectedIndexChanged="ddlTaxDefaultCountry_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblTaxDefaultStateProvinceTitle" Text="<% $NopResources:Admin.TaxSettings.TaxDefaultState %>"
                ToolTip="<% $NopResources:Admin.TaxSettings.TaxDefaultState.Tooltip %>" ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlTaxDefaultStateProvince" runat="server" CssClass="adminInput">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblTaxDefaultZipPostalTitle" Text="<% $NopResources:Admin.TaxSettings.TaxDefaultZip %>"
                ToolTip="<% $NopResources:Admin.TaxSettings.TaxDefaultZip.Tooltip %>" ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:TextBox runat="server" ID="txtTaxDefaultZipPostalCode" CssClass="adminInput">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="adminSeparator">
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblShippingIsTaxable" Text="<% $NopResources:Admin.TaxSettings.ShippingIsTaxable %>"
                ToolTip="<% $NopResources:Admin.TaxSettings.ShippingIsTaxable.Tooltip %>" ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbShippingIsTaxable" runat="server" AutoPostBack="true" OnCheckedChanged="cbShippingIsTaxable_CheckedChanged">
            </asp:CheckBox>
        </td>
    </tr>
    <tr runat="server" id="pnlShippingPriceIncludesTax">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblShippingPriceIncludesTax" Text="<% $NopResources:Admin.TaxSettings.ShippingPriceIncludesTax %>"
                ToolTip="<% $NopResources:Admin.TaxSettings.ShippingPriceIncludesTax.Tooltip %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbShippingPriceIncludesTax" runat="server"></asp:CheckBox>
        </td>
    </tr>
    <tr runat="server" id="pnlShippingTaxClass">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblShippingTaxClass" Text="<% $NopResources:Admin.TaxSettings.ShippingTaxClass %>"
                ToolTip="<% $NopResources:Admin.TaxSettings.ShippingTaxClass.Tooltip %>" ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlShippingTaxClass" runat="server" CssClass="adminInput">
            </asp:DropDownList>
        </td>
    </tr>
    <tr class="adminSeparator">
        <td colspan="2">
            <hr />
        </td>
    </tr>
    <tr>
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblPaymentMethodAdditionalFeeIsTaxable"
                Text="<% $NopResources:Admin.TaxSettings.PaymentMethodFeeIsTaxable %>" ToolTip="<% $NopResources:Admin.TaxSettings.PaymentMethodFeeIsTaxable.Tooltip %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbPaymentMethodAdditionalFeeIsTaxable" runat="server" AutoPostBack="true"
                OnCheckedChanged="cbPaymentMethodAdditionalFeeIsTaxable_CheckedChanged"></asp:CheckBox>
        </td>
    </tr>
    <tr runat="server" id="pnlPaymentMethodAdditionalFeeIncludesTax">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblPaymentMethodAdditionalFeeIncludesTax"
                Text="<% $NopResources:Admin.TaxSettings.PaymentMethodFeeIncludesTax %>" ToolTip="<% $NopResources:Admin.TaxSettings.PaymentMethodFeeIncludesTax.Tooltip %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:CheckBox ID="cbPaymentMethodAdditionalFeeIncludesTax" runat="server"></asp:CheckBox>
        </td>
    </tr>
    <tr runat="server" id="pnlPaymentMethodAdditionalFeeTaxClass">
        <td class="adminTitle">
            <nopCommerce:ToolTipLabel runat="server" ID="lblPaymentMethodAdditionalTaxClass"
                Text="<% $NopResources:Admin.TaxSettings.PaymentMethodTaxClass %>" ToolTip="<% $NopResources:Admin.TaxSettings.PaymentMethodTaxClass.Tooltip %>"
                ToolTipImage="~/Administration/Common/ico-help.gif" />
        </td>
        <td class="adminData">
            <asp:DropDownList ID="ddlPaymentMethodAdditionalFeeTaxClass" runat="server" CssClass="adminInput">
            </asp:DropDownList>
        </td>
    </tr>
</table>
