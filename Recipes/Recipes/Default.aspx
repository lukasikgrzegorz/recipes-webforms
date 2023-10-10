<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Recipes</title>
    <link rel="stylesheet" type="text/css" href="style.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,700;0,900;1,700;1,900&display=swap" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-style">
            <div> 
                <asp:TextBox ID="recipe_query" runat="server" CssClass="input-style"></asp:TextBox>
                <asp:Button ID="button"  runat="server" Text="Search" OnClick="Search_Recipes" CssClass="button-style" />
            </div>
            <asp:Label ID="result" runat="server" Text="" CssClass="result-style"></asp:Label>
        </div>
    </form>
</body>
</html>
