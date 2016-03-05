using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using POCReportApp.Classes;
using System.Reflection;
using System.IO;
using System.Xml;

namespace POCReportApp
{
    public partial class ReportView : System.Web.UI.Page
    {
        #region Private Variables
        private string rptName = "CustomerLanguageReport";
        private string paramValue = "<customerlanguage> <countryid>E1C6B9B5-5E8A-47D9-AF3F-C0878101F863</countryid> <stateid>5062FEA6-06C7-4E52-9C72-03925561BE3F</stateid><city></city> <languages><language>EAA89234-2044-4C48-BBEA-4BE5DE2BAFC6</language><language>06970083-1493-472B-A266-11FDAC48EF42</language></languages></customerlanguage>";
        #endregion

        #region Page Load Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateReport();
            }
        }

        #endregion 

        #region Button Export Events

        protected void btnExport_Click(object sender, EventArgs e)
        {
            // ExportReport(HttpContext.Current.Request.Url.AbsoluteUri);

            ExportToPDFStream();

        }


        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
            ExportToExcelStream();
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        private void GenerateReport()
        {
            var xm = new XmlDocument();
            ConvertToXml(ref xm);
            var xml = xm.InnerXml.ToString();
            rptViewerCustomerLanguage.Visible = true;

            rptViewerCustomerLanguage.ProcessingMode = ProcessingMode.Remote;
            rptViewerCustomerLanguage.ServerReport.ReportServerUrl = new Uri("http://server/ReportServer/");
            rptViewerCustomerLanguage.ServerReport.ReportServerCredentials = new CustomReportCredentials("sanjeev", "sanjeev", "server");


            rptViewerCustomerLanguage.ServerReport.ReportPath = "/WebPOCReportProject/CustomerLanguageReport";
            //rptViewerCustomerLanguage.LocalReport.ReportPath = string.Format(@"/WebPOCReportProject/{0}.rdl", rptName);

            // rptViewerCustomerLanguage.LocalReport.ReportPath = "Reports/CustomerLanguageReport.rdl";



            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add(new SqlParameter("@cParams", paramValue));

            string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            cmd.Connection = conn;

            cmd.CommandText = "Get_CustomerReportLanguages";

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            ReportDataSource dataSource = new ReportDataSource(rptName, ds.Tables[0]);

            //rptViewerCustomerLanguage.ServerReport.datas.Clear();
            //rptViewerCustomerLanguage.LocalReport.DataSources.Add(dataSource);

            System.Collections.Generic.List<ReportParameter> lstParams = new List<ReportParameter>();

            lstParams.Add(new ReportParameter("cParams", xml));



            rptViewerCustomerLanguage.ServerReport.SetParameters(lstParams);

            //Assembly assembly = Assembly.LoadFrom("MyReports.dll");
            //Stream stream = assembly.GetManifestResourceStream("Reports.MyReport.rdlc");
            //rptViewerCustomerLanguage.LocalReport.LoadReportDefinition(stream);

            rptViewerCustomerLanguage.ShowParameterPrompts= false;
            rptViewerCustomerLanguage.ShowPrintButton=true;


            rptViewerCustomerLanguage.ServerReport.Refresh();
        }


        private void ConvertToXml(ref XmlDocument xm)
        {
            //string countryid="E1C6B9B5-5E8A-47D9-AF3F-C0878101F863";
            //const string header = @"<customerlanguage>";
            //string strenvelopes;
            ////"<customerlanguage> <countryid>E1C6B9B5-5E8A-47D9-AF3F-C0878101F863</countryid> <stateid>5062FEA6-06C7-4E52-9C72-03925561BE3F</stateid><city></city> <languages><language>EAA89234-2044-4C48-BBEA-4BE5DE2BAFC6</language><language>06970083-1493-472B-A266-11FDAC48EF42</language></languages></customerlanguage>";
           
            //        strenvelopes += //@"<ADDRESS>" +
            //         strenvelopes += @"<countryid>" + "<![CDATA[" + ((object[])countryid) + "]]>" + "</countryid>" +
            //        "<CONTACT>" + "<![CDATA[" + ((object[])(item.DataItem))[1].ToString() + "]]>" + "</CONTACT>" +
            //        "<STREET>" + "<![CDATA[" + ((object[])(item.DataItem))[3].ToString() + "]]>" + "</STREET>" +
            //        "<SUBURBSTATE>" + "<![CDATA[" + ((object[])(item.DataItem))[4].ToString() + "]]>" + "</SUBURBSTATE>" +
            //        "</ADDRESS>";
            //    }
            //}

            //const string footer = @"</customerlanguage>";
          string strenvelopes=   "<customerlanguage> <countryid>E1C6B9B5-5E8A-47D9-AF3F-C0878101F863</countryid> <stateid>5062FEA6-06C7-4E52-9C72-03925561BE3F</stateid><city></city> <languages><language>EAA89234-2044-4C48-BBEA-4BE5DE2BAFC6</language><language>06970083-1493-472B-A266-11FDAC48EF42</language></languages></customerlanguage>";
          //  var envelopes = header + strenvelopes + footer;
            xm.LoadXml(strenvelopes);
        }

        

        public string ExportReport(string url)
        {
            string pdfpath = @"C:\xx.pdf";
            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            req.Credentials = System.Net.CredentialCache.DefaultCredentials;
            req.Method = "Get";
            System.Net.WebResponse Res = req.GetResponse();

            FileStream fs = new FileStream(pdfpath, FileMode.Create);
            Stream stream = Res.GetResponseStream();
            byte[] buf = new byte[1023];
            int len = stream.Read(buf,0,1024);
            while (len > 0)
            {
                fs.Write(buf, 0, len);
                len = stream.Read(buf, 0, 1024);

            }
            stream.Close();
            fs.Close();
            return pdfpath;
        }

     

        protected void ExportToPDF()
        {
            string path = @"C:\xx.pdf";

            byte[] Bytes = rptViewerCustomerLanguage.ServerReport.Render(format: "PDF", deviceInfo: "");
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                stream.Write(Bytes, 0, Bytes.Length);
            }
        }

        protected void ExportToPDFStream()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType, encoding, extension;

            byte[] Bytes = rptViewerCustomerLanguage.ServerReport.Render("PDF", string.Empty, out mimeType, out encoding, out extension, out streamids, out warnings);
           
            using (MemoryStream memoryStream = new MemoryStream(Bytes))
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "inline; filename=CustomerLanguageReport.pdf");
                Response.AddHeader("content-length", Bytes.Length.ToString());
                Response.BinaryWrite(memoryStream.ToArray());
                Response.Flush();
                Response.End();
            }
        }

        protected void ExportToExcel()
        {
            string path = @"C:\xx.xlsx";

            byte[] Bytes = rptViewerCustomerLanguage.ServerReport.Render(format: "Excel", deviceInfo: "");
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                stream.Write(Bytes, 0, Bytes.Length);
            }
        }
        protected void ExportToExcelStream()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType, encoding, extension;

            byte[] Bytes = rptViewerCustomerLanguage.ServerReport.Render("Excel", string.Empty, out mimeType, out encoding, out extension, out streamids, out warnings);

            using (MemoryStream memoryStream = new MemoryStream(Bytes))
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/xlsx";
                Response.AddHeader("content-disposition", "inline; filename=CustomerLanguageReport.xlsx");
                Response.AddHeader("content-length", Bytes.Length.ToString());
                Response.BinaryWrite(memoryStream.ToArray());
                Response.Flush();
                Response.End();
            }
        }

        #endregion


        //        Public Shared Function ExportReport(ByVal URL As String) As String
 
// Dim path As String = "C:\xx.pdf"
 
// Try
// Dim Req As System.Net.HttpWebRequest = DirectCast(System.Net.WebRequest.Create(URL), System.Net.HttpWebRequest)
// Req.Credentials = System.Net.CredentialCache.DefaultCredentials
// Req.Method = "Get"

// Dim objResponse As System.Net.WebResponse = Req.GetResponse()
// Dim fs As New System.IO.FileStream(path, System.IO.FileMode.Create)
// Dim stream As System.IO.Stream = objResponse.GetResponseStream()
// Dim buf As Byte() = New Byte(1023) {}
// Dim len As Integer = stream.Read(buf, 0, 1024)
// While len > 0
//  fs.Write(buf, 0, len)
//  len = stream.Read(buf, 0, 1024)
// End While
// stream.Close()
// fs.Close()
// Catch e As Exception
//  return e.Message
// End Try
 
// return path
//End Function

    }
}