<%@ Control Language="C#" AutoEventWireup="true" Inherits="NopSolutions.NopCommerce.Web.Modules.HeaderControl"
    CodeBehind="Header.ascx.cs" %>
<%@ Register TagPrefix="nopCommerce" TagName="CurrencySelector" Src="~/Modules/CurrencySelector.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="LanguageSelector" Src="~/Modules/LanguageSelector.ascx" %>
<%@ Register TagPrefix="nopCommerce" TagName="TaxDisplayTypeSelector" Src="~/Modules/TaxDisplayTypeSelector.ascx" %>

<div class="header">
    <div class="header-logo">
        <a href="<%=CommonHelper.GetStoreLocation()%>" class="logo">&nbsp; 
        <object classid=clsid:D27CDB6E-AE6D-11cf-96B8-444553540000 codebase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=4,0,2,0 >
        <param name=movie value="../images/logo.swf">
      	<param name="wmode" value="transparent" />
      	<param name="bgcolor" value="#ffffff" />
        <param name=quality value=high>
      	<param name="allowScriptAccess" value="sameDomain" />
      	<param name="allowFullScreen" value="false" />
        <embed src="../images/logo.swf" quality=high pluginspage=http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash type=application/x-shockwave-flash wmode="transparent" width=60 height=60>
        </embed>
        &nbsp; 
        </object>
        </a>
    </div>
    <!-- Yandex.Metrika counter -->
<script src="//mc.yandex.ru/metrika/watch.js" type="text/javascript"></script>
<div style="display:none;"><script type="text/javascript">
                               try { var yaCounter897977 = new Ya.Metrika({ id: 897977 }); }
                               catch (e) { }
</script></div>
<noscript><div><img src="//mc.yandex.ru/watch/897977" style="position:absolute; left:-9999px;" alt="" /></div></noscript>
<!-- /Yandex.Metrika counter -->

    <div class="header-links-wrapper">
        <div class="header-links">
            <ul>
                <asp:LoginView ID="topLoginView" runat="server">
<AnonymousTemplate>
<%--                   ~~ 
                        <li><a href="<%=Page.ResolveUrl("~/register.aspx")%>" class="ico-register">
                            <%=GetLocaleResourceString("Account.Register")%></a></li>
                        <li><a href="<%=Page.ResolveUrl("~/login.aspx")%>" class="ico-login">
                            <%=GetLocaleResourceString("Account.Login")%></a></li>--%>

                                <font color=white>
Факс: +7-495-979-3057.&nbsp;&nbsp;
Пишите нам: str@s-t-r.ru&nbsp;&nbsp;&nbsp;
</font>

                    </AnonymousTemplate>
                    <LoggedInTemplate>
                        <li>
                            <%=Page.User.Identity.Name %>
                        </li>
                        <li><a href="<%=Page.ResolveUrl("~/logout.aspx")%>" class="ico-logout">
                            <%=GetLocaleResourceString("Account.Logout")%></a> </li>
                        <% if (ForumManager.AllowPrivateMessages)
                           { %>
                        <li><a href="<%=Page.ResolveUrl("~/privatemessages.aspx")%>" class="ico-inbox">
                            <%=GetLocaleResourceString("PrivateMessages.Inbox")%></a>
                            <asp:Literal runat="server" ID="lUnreadPrivateMessages" />
                        </li>
                        <%} %>
                    </LoggedInTemplate>
                </asp:LoginView>
                <li><!--a href="<%=Page.ResolveUrl("~/shoppingcart.aspx")%>" class="ico-cart" visible=false-->
                    <!--%=GetLocaleResourceString("Account.ShoppingCart")%-->
                </a><!--a  visible=false href="<%=Page.ResolveUrl("~/shoppingcart.aspx")%>">(<%=ShoppingCartManager.GetCurrentShoppingCart(ShoppingCartTypeEnum.ShoppingCart).Count%>)</a-->
                </li>
                <% if (SettingManager.GetSettingValueBoolean("Common.EnableWishlist"))
                   { %>
                <li><a href="<%=Page.ResolveUrl("~/wishlist.aspx")%>" class="ico-wishlist">
                    <%=GetLocaleResourceString("Wishlist.Wishlist")%></a> <a href="<%=Page.ResolveUrl("~/wishlist.aspx")%>">
                        (<%=ShoppingCartManager.GetCurrentShoppingCart(ShoppingCartTypeEnum.Wishlist).Count%>)</a></li>
                <%} %>
                <% if (NopContext.Current.User != null && NopContext.Current.User.IsAdmin)
                   { %>
                <li><a href="<%=Page.ResolveUrl("~/administration/")%>" class="ico-admin">
                    <%=GetLocaleResourceString("Account.Administration")%></a> </li>
                <%} %>
            </ul>
        </div>
    </div>
    <p align=left>
      <h1>&nbsp;&nbsp;&nbsp;&nbsp;СпецТехноРесурс</h1>
     </p>
    <div class="header-selectors-wrapper">
        <div class="header-taxDisplayTypeSelector">
            <nopCommerce:TaxDisplayTypeSelector runat="server" ID="ctrlTaxDisplayTypeSelector">
            </nopCommerce:TaxDisplayTypeSelector>
        </div>
        <div class="header-currencyselector">
            <nopCommerce:CurrencySelector runat="server" ID="ctrlCurrencySelector"></nopCommerce:CurrencySelector>
        </div>
        <div class="header-languageSelector">
            <nopCommerce:LanguageSelector runat="server" ID="ctrlLanguageSelector"></nopCommerce:LanguageSelector>
        </div>
    </div>
</div>
