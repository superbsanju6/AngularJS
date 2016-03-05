<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="DynamicControl.Report" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="http://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet />
    <link href="~/content/css/site.css" rel="stylesheet" />
    <style>
        .margin-report{margin-left:10px; margin-right:10px;}

    </style>
</head>
<body>
    <form id="form1" runat="server" class="container">
        <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
    </div>
        <div>
            <table>
                <tr>
                    <td class="margin-report"><a href="/"><i class="fa fa-home" style="font-size:35px;"></i></a></td>
                    <td class="margin-report"><asp:Button ID="btnExport" runat="server" Text="Export To PDF" OnClick="btnExport_Click" class="btn btn-primary"/></td>
                    <td class="margin-report"><asp:Button ID="btnExportExcel" runat="server" Text="Export To Excel" OnClick="btnExportExcel_Click" class="btn btn-primary"/></td>
                </tr>
            </table>
            
        </div>
        
    <div style="vertical-align: middle; text-align: center">
        <rsweb:ReportViewer style="border:1px solid black;" ID="rptViewerCustomerLanguage" Height="700" Width="650" runat="server"></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
