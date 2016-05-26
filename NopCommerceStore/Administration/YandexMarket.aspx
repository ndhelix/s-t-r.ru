<%@ Page Language="C#" MasterPageFile="~/Administration/main.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.Administration.Administration_YandexMarket"
    CodeBehind="YandexMarket.aspx.cs" %>

<%@ Register TagPrefix="nopCommerce" TagName="YandexMarket" Src="Modules/YandexMarket.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="server">
    <nopCommerce:YandexMarket runat="server" ID="ctrlYandexMarket" />
</asp:Content>
