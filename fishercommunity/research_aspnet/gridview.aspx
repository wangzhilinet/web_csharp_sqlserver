<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gridview.aspx.cs" Inherits="testaspnet_gridview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function aaa() {
            alert("000");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" ForeColor="#333333">
            <Columns>
                <asp:BoundField DataField="ID" FooterText="编号" HeaderText="编号" />
                <asp:BoundField DataField="en_word" FooterText="英文" HeaderText="英文" />
                <asp:BoundField DataField="cn_word" FooterText="中文" HeaderText="中文" />
                <asp:BoundField DataField="Remard" FooterText="备注" HeaderText="备注" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
