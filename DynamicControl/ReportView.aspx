<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportView.aspx.cs" Inherits="POCReportApp.ReportView" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
    </div>
        <div>
            <table>
                <tr>
                    <td><asp:Button ID="btnExport" runat="server" Text="Export To PDF" OnClick="btnExport_Click" /></td>
                    <td><asp:Button ID="btnExportExcel" runat="server" Text="Export To Excel" OnClick="btnExportExcel_Click" /></td>
                </tr>
            </table>
            
        </div>
        
    <div>
        <rsweb:ReportViewer ID="rptViewerCustomerLanguage" Height="800" Width="800" runat="server"></rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
