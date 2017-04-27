<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Map.aspx.cs" Inherits="Mapping_Map" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>Untitled Page</title>
<meta http-equiv="content-type" content="text/html; charset=utf-8"/>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script src="<%=GoogMapScriptSrc%>" type="text/javascript">
    </script>
</telerik:RadCodeBlock>
</script>
<script type="text/javascript" src="../Includes/common.js"></script>
</head>
<body onload="initializeMap()" onunload="GUnload()">
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
<Services>
<asp:ServiceReference Path="~/Mapping/RmMappingService.asmx" />
</Services>
</asp:ScriptManager>
<div>
<div id="map_canvas" style="width: 500px; height: 500px"></div>
</div>
<asp:HiddenField ID="cid" runat="server" />
<asp:HiddenField ID="thisID" runat="server" />
<asp:HiddenField ID="pageSize" runat="server" />
<asp:HiddenField ID="currentPage" runat="server" />
<asp:HiddenField ID="sortColumn" runat="server" />
<asp:HiddenField ID="whereClause" runat="server" />
<asp:HiddenField ID="mappingType" runat="server" />
</form>
</body>
</html>
