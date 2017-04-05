<%@ Page Language="C#" AutoEventWireup="true" CodeFile="instructions.aspx.cs" Inherits="testaspnet_encapsulation_instructions" %>

<%@ Register Src="~/research_aspnet/encapsulation/UcWebLogin.ascx" TagName="UcWebFoot"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>封装说明</title>
    <script src="common.js" type="text/javascript"></script>
    <script src="jquery.js" type="text/javascript"></script>
    <link href="css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a href="~/research_aspnet/encapsulation/UcWebLogin.ascx">~/research_aspnet/encapsulation/UcWebLogin.ascx</a>
    </div>
    <div id="TopIndex">
        <div id="TopIndexCon">
            <!--展会倒计时-->
            <script type="text/javascript">
                function setDate() {
                    var date = new Date();
                    var year = date.getFullYear();
                    var month = date.getMonth() + 1;
                    var day = date.getDate();
                    var str = year + "年" + month + "月" + day + "日";

                    var y = "2017";
                    var m = "7";
                    var d = "20";

                    var End = new Date(y, m, d);
                    var Begin = new Date(year, month, day);
                    var diff = End - Begin;
                    var diffTime = (diff) / (3600 * 24 * 1000);
                    if (diffTime <= 0) {
                        diffTime = 0;
                    }
                    document.getElementById("sYt").innerHTML = y;
                    document.getElementById("sM").innerHTML = m;
                    document.getElementById("sD").innerHTML = d;

                    document.getElementById("showDate").innerHTML = diffTime;
                }
            </script>
            <div id="TopTime">
                距离<span id="sYt"></span>年<span id="sM"></span>月<span id="sD"></span>日展会开幕 还有<b><span
                    id="showDate"></span></b>天
            </div>
            <!--快速登录-->
            <div id="TopLogin">
                <ul>
                    <li class="style222" onmouseover="this.className='style111'" onmouseout="this.className='style222'">
                        快速登录
                        <div class="list">
                            <a href="../CustomerCenter/CustomerLogin.aspx">展商登陆</a><br />
                            <a href="../MediaCenter/MediaLogin.aspx">媒体登陆</a><br />
                            <a href="../MemberCenter/MemberLogin.aspx">观众登陆</a><br />
                        </div>
                    </li>
                </ul>
            </div>
            <!--加入收藏-->
            <div id="TopAddFav">
                <a href="/">首页</a>&nbsp; ▏&nbsp; <a href="http://www.ccbn.cn" title="CCBN" rel="sidebar"
                    onclick="javascript:window.external.AddFavorite('http://www.ccbn.cn', 'CCBN');return false;">
                    加入收藏</a> &nbsp; ▏&nbsp; <a href="http://www.ccbn.cn">中文</a>&nbsp; ▏&nbsp; <a href="http://www.ccbn.tv">
                        English</a>
            </div>
        </div>
    </div>
    <div class="top1">
        <!--插入flash-->
        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000 codebase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=4,0,2,0"
            height="150" width="980">
            <param name="movie" value="../flash/LogoFlash1.swf">
            <param name="quality" value="high">
            <param name="wmode" value="transparent">
            <embed src="../flash/LogoFlash1.swf" quality="high" pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash"
                type="application/x-shockwave-flash" width="980" height="150" wmode="transparent">
</embed>
        </object>
    </div>
    </form>
</body>
</html>
