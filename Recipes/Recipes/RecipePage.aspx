﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecipePage.aspx.cs" Inherits="RecipePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Recipe Details</title>
    <link rel="stylesheet" type="text/css" href="style.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,700;0,900;1,700;1,900&display=swap" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Recipe Details</h1>
            <asp:Label ID="recipeDetailsLabel" runat="server" Text=""></asp:Label> 
        </div>
    </form>
</body>
</html>
