<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Boot_Park.View.Public.test.index" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<script src="/socket.io/socket.io.js"></script>
<script >
  var socket = io.connect('http://192.168.1.44:3000');
  socket.emit('open', { my: 'derecha' });

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>CIRCULACION</title>
</head>
<body>
    <ext:ResourceManager runat="server" />
    <form id="FINDEX" runat="server">
        <div>
            <ext:Window runat="server">
                <Items>
                    
                </Items>
            </ext:Window>
        </div>
    </form>
</body>
</html>
