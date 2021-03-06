﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.LanguageSelectorControl" CodeBehind="LanguageSelector.ascx.cs" %>

<asp:DropDownList runat="server" ID="ddlLanguages" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguages_OnSelectedIndexChanged" CssClass="languagelist" />

<asp:Repeater runat="server" ID="rptLanguages">
    <HeaderTemplate>
        <ul class="languagelist">
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <asp:ImageButton runat="server" ID="btnSelectLanguage" CssClass='<%#Eval("Class").ToString()%>' ImageUrl='<%#Eval("ImageUrl").ToString()%>' OnCommand="BtnSelectLanguage_OnCommand" CommandName="SelectLanguage" CommandArgument='<%#Eval("LanguageId").ToString()%>' />
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater> 
