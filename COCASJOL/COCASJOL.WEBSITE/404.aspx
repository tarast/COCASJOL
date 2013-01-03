﻿<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        ExtNet.Msg.Show(new MessageBoxConfig
        {
            Title = "Error 404", 
            Message = "La pagina a la que usted quiere acceder no existe.",
            Closable = false,
            Buttons = MessageBox.Button.OK,
            Handler = "window.parent.location = 'Default.aspx'"
        });
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Colinas</title>
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon"/>
    <link rel="icon" href="favicon.ico" type="image/x-icon"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server"  DisableViewState="true" >
        </ext:ResourceManager>
    </div>
    </form>
</body>
</html>
