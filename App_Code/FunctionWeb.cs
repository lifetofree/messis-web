using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json;

/// <summary>
/// Summary description for FunctionWeb
/// </summary>
public class FunctionWeb
{
    public FunctionWeb()
    {
    }

    #region random, MD5
    public string GetRandomPasswordUsingGuid(int length)
    {
        // Get the GUID
        string guidResult = System.Guid.NewGuid().ToString();

        // Remove the hyphens
        guidResult = guidResult.Replace("-", string.Empty);

        // Make sure length is valid
        if (length <= 0 || length > guidResult.Length)
            throw new ArgumentException("Length must be between 1 and " + guidResult.Length);

        // Return the first length bytes
        return guidResult.Substring(0, length);
    }

    public string GetMd5Sum(string str)
    {
        byte[] input = ASCIIEncoding.ASCII.GetBytes(str);
        byte[] output = MD5.Create().ComputeHash(input);
        var sb = new StringBuilder();
        foreach (byte t in output)
        {
            sb.Append(t.ToString("X2").ToLower());
        }
        return sb.ToString();
    }
    #endregion

    #region display alert
    public void ShowAlert(Control control, string strMessage)
    {
        if (!control.Page.ClientScript.IsClientScriptBlockRegistered(Guid.NewGuid().ToString()))
        {
            var script = String.Format("<script type='text/javascript' language='javascript'>alert('{0}')</script>", strMessage);
            control.Page.ClientScript.RegisterClientScriptBlock(control.Page.GetType(), "PopupScript", script);
        }
    }
    #endregion

    #region display other javascript
    public void ShowOtherJavaScript(Control control, string strMessage)
    {
        if (!control.Page.ClientScript.IsClientScriptBlockRegistered(Guid.NewGuid().ToString()))
        {
            var script = String.Format("<script type='text/javascript' language='javascript'>{0}</script>", strMessage);
            control.Page.ClientScript.RegisterClientScriptBlock(control.Page.GetType(), "PopupScriptII", script);
        }
    }
    #endregion

    #region convert data
    public DateTime GetSmallDateTime(string dateIn)
    {
        DateTime dateDefault;

        DateTime dateOut = DateTime.TryParse(dateIn, out dateDefault) ? DateTime.Parse(dateIn) : dateDefault;

        return dateOut;
    }

    public string ConvToDateOnly(string dateIn)
    {
        string[] dateOut = dateIn.Split(' ');

        return dateOut[0];
    }
    #endregion

    #region gridview display
    public void MakeGridViewPrinterFriendly(GridView gridView)
    {
        if (gridView.Rows.Count > 0)
        {
            gridView.UseAccessibleHeader = true;
            gridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    public void EmptyGridviewFix(GridView grdView)
    {
        // normally executes after a grid load method
        if (grdView.Rows.Count == 0 &&
            grdView.DataSource != null)
        {
            DataTable dt = null;

            // need to clone sources otherwise it will be indirectly adding to 
            // the original source

            var set = grdView.DataSource as DataSet;
            if (set != null)
            {
                dt = set.Tables[0].Clone();
            }
            else
            {
                var table = grdView.DataSource as DataTable;
                if (table != null)
                {
                    dt = table.Clone();
                }
            }

            if (dt == null)
            {
                return;
            }

            dt.Rows.Add(dt.NewRow()); // add empty row
            grdView.DataSource = dt;
            grdView.DataBind();

            // hide row
            grdView.Rows[0].Visible = false;
            grdView.Rows[0].Controls.Clear();
        }

        // normally executes at all postbacks
        if (grdView.Rows.Count == 1 &&
            grdView.DataSource == null)
        {
            bool bIsGridEmpty = true;

            // check first row that all cells empty
            for (int i = 0; i < grdView.Rows[0].Cells.Count; i++)
            {
                if (grdView.Rows[0].Cells[i].Text != string.Empty)
                {
                    bIsGridEmpty = false;
                }
            }
            // hide row
            if (bIsGridEmpty)
            {
                grdView.Rows[0].Visible = false;
                grdView.Rows[0].Controls.Clear();
            }
        }
    }
    #endregion

    #region upload FTP
    public string UploadToFtp(string uploadUrl, string uploadUserName, string uploadPassword, string fileToUpload, string fileName)
    {
        try
        {
            var ftpRequest = (FtpWebRequest)WebRequest.Create(uploadUrl + @"/" + fileName);
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            ftpRequest.UseBinary = true;
            ftpRequest.Proxy = null;
            ftpRequest.UsePassive = false;

            ftpRequest.Credentials = new NetworkCredential(uploadUserName, uploadPassword);

            var ff = new FileInfo(fileToUpload);
            var fileContents = new byte[ff.Length];

            using (FileStream fr = ff.OpenRead())
            {
                fr.Read(fileContents, 0, Convert.ToInt32(ff.Length));
            }

            using (Stream writer = ftpRequest.GetRequestStream())
            {
                writer.Write(fileContents, 0, fileContents.Length);
            }

            var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            string strResult = ftpResponse.StatusDescription;
            ftpResponse.Close();

            return strResult;
        }
        catch (Exception ex)
        {
            return ex.Message;
            //return "Error!!!";
        }
    }
    #endregion

    #region clear control
    public void ClearControl(Control control)
    {
        switch (control.GetType().Name)
        {
            case "TextBox":
                var txtBox = (TextBox)control;
                txtBox.Text = String.Empty;
                break;
            case "DropDownList":
                var ddl = (DropDownList)control;
                ddl.SelectedIndex = 0;
                break;
            case "CheckBox":
                var chk = (CheckBox)control;
                chk.Checked = false;
                break;
            case "CheckBoxList":
                var chkList = (CheckBoxList)control;
                foreach (ListItem li in chkList.Items)
                    li.Selected = false;
                break;
            case "Panel":
                ClearFields((Panel)control);
                break;
            case "RadioButtonList":
                var rbl = (RadioButtonList)control;
                rbl.SelectedIndex = -1;
                break;
        }
    }

    public void ClearFields(Panel container)
    {
        foreach (Control control in container.Controls)
        {
            ClearControl(control);
        }
    }

    public void ClearFields(View container)
    {
        foreach (Control control in container.Controls)
        {
            ClearControl(control);
        }
    }
    #endregion

    #region convert custom array object
    public string ConvertObjectToXml(Object objData)
    {
        try
        {
            var xmlDoc = new XmlDocument(); //Represents an XML document, 
            // Initializes a new instance of the XmlDocument class.          
            var xmlSerializer = new XmlSerializer(objData.GetType());
            // Create empty namespace
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            // Creates a stream whose backing store is memory. 
            using (var xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, objData, namespaces);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                foreach (XmlNode node in xmlDoc)
                {
                    if (node.NodeType == XmlNodeType.XmlDeclaration)
                    {
                        xmlDoc.RemoveChild(node);
                    }
                }
                return xmlDoc.InnerXml;
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }

    public Object ConvertXmlToObject(Type dataName, string xmlText)
    {
        try
        {
            var deserializer = new XmlSerializer(dataName);
            TextReader reader = new StringReader(xmlText);
            Object retData = deserializer.Deserialize(reader);
            reader.Close();

            return retData;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region XML to JSON and JSON to XML
    public string ConvertXmlToJson(string dataXml)
    {
        try
        {
            var doc = new XmlDocument();
            doc.LoadXml(dataXml);
            return JsonConvert.SerializeXmlNode(doc);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public string ConvertJsonToXml(string dataJson)
    {
        try
        {
            XNode node = JsonConvert.DeserializeXNode(dataJson);
            return node.ToString();
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
}