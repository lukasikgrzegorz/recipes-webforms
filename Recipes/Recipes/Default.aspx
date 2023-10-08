<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Recipes</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="recipe_query" runat="server"></asp:TextBox>
            <asp:Button ID="button"  runat="server" Text="Search" OnClick="Search_Recipes" />
            <asp:Label ID="result" runat="server" Text=""></asp:Label>
            <asp:HyperLink ID="recipeLink" runat="server" Text="" Target="_blank"></asp:HyperLink>
        </div>
    </form>
</body>
</html>
