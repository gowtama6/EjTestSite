<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div bg-color:"grey">
    
        TEST WEB FORM [<span style="color: rgb(85, 85, 85); font-family: arial, sans-serif; font-size: 12.8px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: nowrap; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">ejamerica</span>]<br />
        <br />
        <br />
        Please Select Your File Containing Word List Here:
        <asp:FileUpload ID="FileUpload2" runat="server" />
&nbsp;&nbsp;
        <asp:Button ID="process" runat="server" OnClick="uploadWordListFile_Click" style="width: 78px" Text="Compute" />
&nbsp;&nbsp; Status:&nbsp;
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
        <br />
        &nbsp;1st Largest&nbsp; Result : <asp:Label ID="Label2" runat="server"></asp:Label>
        <br />
        <br />
        &nbsp;2nd Largest Result :
        <asp:Label ID="Label3" runat="server"></asp:Label>
        <br />
        <br />
        How many combinations possible :
        <asp:Label ID="Label4" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
