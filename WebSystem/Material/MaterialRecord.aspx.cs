using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.IO;
using System.Drawing;
using System.Text;

public partial class WebSystem_Material_MaterialRecord : System.Web.UI.Page
{
    #region initial function/data
    FunctionWeb funcWeb = new FunctionWeb();
    ServiceMaster serviceMaster = new ServiceMaster();
    DataMaster dataMaster = new DataMaster();
    string localXml = String.Empty;
    string localString = String.Empty;
    int actionType = 0;

    string imgPath = ConfigurationSettings.AppSettings["MaterialImages"];

    string recType = "";
    int recStatus = 0;
    string[] textShow = null;
    string[] textOrder = { "Order No", "Order Date" };
    string[] textDelivery = { "Delivery No", "Delivery Date" };
    #endregion  initial function/data

    protected void Page_Load(object sender, EventArgs e)
    {
        recType = Page.RouteData.Values["recType"].ToString().ToLower();
        recStatus = (recType == "in") ? 800000 : 100000; //rec status
        textShow = (recType == "in") ? textDelivery : textOrder; //text show

        if (!IsPostBack)
        {
            DetailMaterialRecordList matDefault = new DetailMaterialRecordList();
            // matDefault.RecIDX = -1;
            matDefault.RecStatus = recStatus;
            dataMaster.MaterialRecordList = new DetailMaterialRecordList[1];
            dataMaster.MaterialRecordList[0] = matDefault;
            // litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));
            getGridData("materialrec", dataMaster, 20);
            fvMaterialRecList.Visible = false;
            setFormData(fvMaterialRecList, FormViewMode.Insert, null);
            ViewState["vsItemLists"] = null;
        }
    }

    #region grid command
    protected void gvRowCommand(object sender, GridViewCommandEventArgs e)
    {
        string cmdName = e.CommandName.ToString();
        string cmdArg = e.CommandArgument.ToString();

        switch (cmdName)
        {
            case "cmdInsert":
                break;
            case "cmdEdit":
                gvMaterialRecList.Visible = false;
                fvMaterialRecList.Visible = true;
                lbAddRec.Visible = false;

                DetailMaterialRecordList matRecord = new DetailMaterialRecordList();
                matRecord.RecIDX = int.Parse(cmdArg);
                dataMaster.MaterialRecordList = new DetailMaterialRecordList[1];
                dataMaster.MaterialRecordList[0] = matRecord;
                dataMaster = serviceMaster.ActionDataMaster("materialrec", dataMaster, 20);
                // litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster.MaterialRecordList[0]));
                ViewState["vsItemLists"] = dataMaster;//.MaterialRecordItemList;
                setFormData(fvMaterialRecList, FormViewMode.Edit, dataMaster.MaterialRecordList);

                //print document
                //litOrderNo.Text = dataMaster.MaterialRecordList[0].RecNo;
                //litOrderDate.Text = dataMaster.MaterialRecordList[0].RecDate;
                //litFromSite.Text = dataMaster.MaterialRecordList[0].RecFromSiteName;
                //litToSite.Text = dataMaster.MaterialRecordList[0].RecToSiteName;

                litRecComment.Text = dataMaster.MaterialRecordList[0].RecComment;

                if (dataMaster.MaterialRecordItemList != null)
                {
                    gvPrint.DataSource = dataMaster.MaterialRecordItemList;
                    gvPrint.DataBind();
                }
                else
                {
                    DetailMaterialRecordItemList itemLists = new DetailMaterialRecordItemList();
                    itemLists.RegIDX = 0;
                    dataMaster.MaterialRecordItemList = new DetailMaterialRecordItemList[1];
                    dataMaster.MaterialRecordItemList[0] = itemLists;
                    gvPrint.DataSource = dataMaster.MaterialRecordItemList;
                    gvPrint.DataBind();
                }

                //create row    
                GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

                ////spanned cell that will span the columns I don't want to give the additional header 
                //TableCell left = new TableHeaderCell();
                //left.ColumnSpan = 6;
                //row.Cells.Add(left);

                //spanned cell that will span the columns i want to give the additional header
                TableCell totals = new TableHeaderCell();
                totals.ColumnSpan = gvPrint.Columns.Count;
                totals.Text = "<table style=\"width: 100%;\" class=\"printDoc\">";
                totals.Text += "    <tbody>";
                totals.Text += "        <tr style=\"line-height: 30px;\">";
                totals.Text += "            <td style=\"width: 80%; text-align: center;\">THAI OBAYASHI CORP., LTD.</td>";
                totals.Text += "            <td style=\"width: 20%;\">No. ";
                totals.Text += dataMaster.MaterialRecordList[0].RecNo;
                totals.Text += "        </tr>";
                totals.Text += "        <tr style=\"line-height: 60px;\">";
                totals.Text += "           <td style=\"width: 80%; text-align: center;\">";
                totals.Text += "                <h3><b>ORDER SHEET</b></h3>";
                totals.Text += "            </td>";
                totals.Text += "            <td style=\"width: 20%;\">Date ";
                totals.Text += dataMaster.MaterialRecordList[0].RecDate;
                totals.Text += "        </tr>";
                totals.Text += "    </tbody>";
                totals.Text += "</table>";
                totals.Text += "<table style=\"width: 100%;\">";
                totals.Text += "    <tbody>";
                totals.Text += "        <tr style=\"line-height: 50px;\">";
                totals.Text += "            <td style=\"width: 50%;\">FROM : ";
                totals.Text += dataMaster.MaterialRecordList[0].RecFromSiteName;
                totals.Text += "            <td style=\"width: 50%;\">TO : ";
                totals.Text += dataMaster.MaterialRecordList[0].RecToSiteName;
                totals.Text += "        </tr>";
                totals.Text += "    </tbody>";
                totals.Text += "</table>";
                row.Cells.Add(totals);

                //Add the new row to the gridview as the master header row
                //A table is the only Control (index[0]) in a GridView
                ((Table)gvPrint.Controls[0]).Rows.AddAt(0, row);
                //print document
                break;
            case "cmdCancel":
                break;
        }
    }

    protected void gvPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gvName = (GridView)sender;
        gvName.PageIndex = e.NewPageIndex;
        getGridData("materialrec", dataMaster, 20);
        setFormData(fvMaterialRecList, FormViewMode.Insert, null);
    }
    #endregion grid command

    #region form command
    protected void fvCommand(object sender, CommandEventArgs e)
    {
        string cmdName = e.CommandName.ToString();
        string cmdArg = e.CommandArgument.ToString();
        float dFloat = float.Parse("0");
        int dInt = 1;

        switch (cmdName)
        {
            case "cmdInsert":
                //set data
                DetailMaterialRecordList matRecord = new DetailMaterialRecordList();
                matRecord.RecIDX = 0;
                matRecord.RecNo = ((TextBox)fvMaterialRecList.FindControl("tbOrderNo")).Text.Trim();
                matRecord.RecDate = ((TextBox)fvMaterialRecList.FindControl("tbOrderCreate")).Text.Trim();
                matRecord.RecFromSite = int.Parse(((DropDownList)fvMaterialRecList.FindControl("ddlFromSiteList")).SelectedValue);
                matRecord.RecToSite = int.Parse(((DropDownList)fvMaterialRecList.FindControl("ddlToSiteList")).SelectedValue);
                matRecord.RecComment = ((TextBox)fvMaterialRecList.FindControl("tbRecComment")).Text.Trim();
                matRecord.RecStatus = recStatus;

                dataMaster.MaterialRecordList = new DetailMaterialRecordList[1];
                dataMaster.MaterialRecordList[0] = matRecord;

                if (ViewState["vsItemLists"] != null)
                {
                    dataMaster.MaterialRecordItemList = ((DataMaster)ViewState["vsItemLists"]).MaterialRecordItemList;
                }

                actionType = int.Parse("1" + "1");
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));

                //execute data
                dataMaster = serviceMaster.ActionDataMaster("materialrec", dataMaster, actionType);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));
                //get return code
                localString = dataMaster.ReturnCode;
                ViewState["dataRecord"] = dataMaster;

                //get current data
                if (localString == "0")
                {
                    funcWeb.ShowAlert(this, "ดำเนินการเรียบร้อยแล้วค่ะ");
                    //clear form
                    setFormData(fvMaterialRecList, FormViewMode.Insert, null);
                    ViewState["vsItemLists"] = null;

                }
                else
                {
                    funcWeb.ShowAlert(this, localString);
                }
                setGridData(gvMaterialRecList, dataMaster.MaterialRecordList);
                break;
            case "cmdReset":
                //clear form
                setFormData(fvMaterialRecList, FormViewMode.Insert, null);
                ViewState["vsItemLists"] = null;
                break;
            case "cmdCancel":
                //clear form
                setFormData(fvMaterialRecList, FormViewMode.Insert, null);
                ViewState["vsItemLists"] = null;
                break;
            case "cmdPrint":
                //PrintAllPages(int.Parse(cmdArg));
                break;



            //case "cmdReturnItem":
            //    Label lblRecIDX = (Label)fvMaterialRecList.FindControl("lblRecIDX");

            //    //set data
            //    DetailMaterialRecordList matRecordR = new DetailMaterialRecordList();
            //    matRecordR.RecIDX = int.Parse(lblRecIDX.Text);
            //    matRecordR.RecStatus = 800000;

            //    dataMaster.MaterialRecordList = new DetailMaterialRecordList[1];
            //    dataMaster.MaterialRecordList[0] = matRecordR;

            //    if (ViewState["vsItemLists"] != null)
            //    {
            //        dataMaster.MaterialRecordItemList = ((DataMaster)ViewState["vsItemLists"]).MaterialRecordItemList;
            //    }

            //    actionType = int.Parse("1" + "1"); //***tempstatus
            //    // litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));

            //    //execute data
            //    dataMaster = serviceMaster.ActionDataMaster("materialrec", dataMaster, actionType);
            //    //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));
            //    //get return code
            //    localString = dataMaster.ReturnCode;

            //    //get current data
            //    if (localString == "0")
            //    {
            //        funcWeb.ShowAlert(this, "ดำเนินการเรียบร้อยแล้วค่ะ");
            //        //clear form
            //        setFormData(fvMaterialRecList, FormViewMode.Insert, null);
            //    }
            //    else
            //    {
            //        funcWeb.ShowAlert(this, localString);
            //    }
            //    setGridData(gvMaterialRecList, dataMaster.MaterialRecordList);
            //    break;
        }
        ViewState["vsItemLists"] = null;
        setFormData(fvMaterialRecList, FormViewMode.Insert, null);

        gvMaterialRecList.Visible = true;
        fvMaterialRecList.Visible = false;
        lbAddRec.Visible = true;
    }

    protected void ddlMCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlMCode = (DropDownList)fvMaterialRecList.FindControl("ddlMCode");
        DropDownList ddlMCodeE = (DropDownList)fvMaterialRecList.FindControl("ddlMCodeE");
        // TextBox tbAsName = (TextBox)fvMaterialRecList.FindControl("tbAsName");
        TextBox tbMName = (TextBox)fvMaterialRecList.FindControl("tbMName");
        TextBox tbMDesc = (TextBox)fvMaterialRecList.FindControl("tbMDesc");
        TextBox tbQuantity = (TextBox)fvMaterialRecList.FindControl("tbQuantity");
        TextBox tbRemark = (TextBox)fvMaterialRecList.FindControl("tbRemark");
        // TextBox tbKName = (TextBox)fvMaterialRecList.FindControl("tbKName");
        // TextBox tbRUD = (TextBox)fvMaterialRecList.FindControl("tbRUD");
        Label lblFMCode = (Label)fvMaterialRecList.FindControl("lblFMCode");
        Label lblSerialNo = (Label)fvMaterialRecList.FindControl("lblSerialNo");

        LinkButton lbAddItem = (LinkButton)fvMaterialRecList.FindControl("lbAddItem");

        int rIDX = -1;
        if (ddlMCode != null)
        {
            rIDX = int.Parse(ddlMCode.SelectedValue);
        }
        else
        {
            rIDX = int.Parse(ddlMCodeE.SelectedValue);
        }

        if (rIDX > 0)
        {
            DataMaster dLocal = new DataMaster();
            DetailMaterialRegisterList matDetail = new DetailMaterialRegisterList();
            matDetail.RegIDX = rIDX;
            dLocal.MaterialRegisterList = new DetailMaterialRegisterList[1];
            dLocal.MaterialRegisterList[0] = matDetail;

            dLocal = serviceMaster.ActionDataMaster("materialreg", dLocal, 20);
            //get return code
            localString = dLocal.ReturnCode;

            //get current data
            if (localString == "0")
            {
                // tbAsName.Text = dLocal.MaterialList[0].AsName;
                tbMName.Text = dLocal.MaterialRegisterList[0].MName;
                tbMDesc.Text = dLocal.MaterialRegisterList[0].MDesc;
                tbQuantity.Text = dLocal.MaterialRegisterList[0].Quantity.ToString();
                tbRemark.Text = String.Empty;
                // tbKName.Text = dLocal.MaterialList[0].KName;
                // tbRUD.Text = dLocal.MaterialList[0].RUD.ToString();
                lblFMCode.Text = dLocal.MaterialRegisterList[0].MCode + dLocal.MaterialRegisterList[0].RCode;
                lblSerialNo.Text = dLocal.MaterialRegisterList[0].SerialNo;

                lbAddItem.CommandArgument = dLocal.MaterialRegisterList[0].RegIDX.ToString();
            }
        }
        else //clear data
        {
            // tbAsName.Text = String.Empty;
            tbMName.Text = String.Empty;
            tbMDesc.Text = String.Empty;
            tbQuantity.Text = String.Empty;
            tbRemark.Text = String.Empty;
            // tbKName.Text = String.Empty;
            // tbRUD.Text = String.Empty;
            lblFMCode.Text = String.Empty;
            lblSerialNo.Text = String.Empty;

            lbAddItem.CommandArgument = "-1";
        }
    }
    #endregion form command

    #region btn command
    protected void btnCommand(object sender, CommandEventArgs e)
    {
        string cmdName = e.CommandName.ToString();
        string cmdArg = e.CommandArgument.ToString();

        switch (cmdName)
        {
            case "cmdAddItem":
                DataMaster vsLocal = new DataMaster();

                GridView gvMaterialItemList = (GridView)fvMaterialRecList.FindControl("gvMaterialItemList");
                TextBox tbMName = (TextBox)fvMaterialRecList.FindControl("tbMName");
                TextBox tbMDesc = (TextBox)fvMaterialRecList.FindControl("tbMDesc");
                TextBox tbQuantity = (TextBox)fvMaterialRecList.FindControl("tbQuantity");
                TextBox tbRemark = (TextBox)fvMaterialRecList.FindControl("tbRemark");
                Label lblFMCode = (Label)fvMaterialRecList.FindControl("lblFMCode");
                Label lblSerialNo = (Label)fvMaterialRecList.FindControl("lblSerialNo");
                DropDownList ddlMCode = (DropDownList)fvMaterialRecList.FindControl("ddlMCode");
                LinkButton lbAddItem = (LinkButton)fvMaterialRecList.FindControl("lbAddItem");

                DetailMaterialRecordItemList itemLists = new DetailMaterialRecordItemList();
                itemLists.RegIDX = int.Parse(cmdArg.ToString());
                itemLists.MName = tbMName.Text;
                itemLists.MDesc = tbMDesc.Text;
                itemLists.RecQty = int.Parse(tbQuantity.Text);
                itemLists.RecRemark = tbRemark.Text;
                itemLists.CMCode = lblFMCode.Text;
                itemLists.SerialNo = lblSerialNo.Text;

                if (ViewState["vsItemLists"] != null)
                {
                    var temp = (((DataMaster)ViewState["vsItemLists"])).MaterialRecordItemList.ToList();
                    var cRow = temp.Count;
                    vsLocal.MaterialRecordItemList = new DetailMaterialRecordItemList[cRow + 1];
                    for (int i = 0; i < cRow; i++)
                    {
                        vsLocal.MaterialRecordItemList[i] = ((DataMaster)ViewState["vsItemLists"]).MaterialRecordItemList[i];
                    }
                    vsLocal.MaterialRecordItemList[cRow] = itemLists;
                }
                else
                {
                    vsLocal.MaterialRecordItemList = new DetailMaterialRecordItemList[1];
                    vsLocal.MaterialRecordItemList[0] = itemLists;
                }

                ViewState["vsItemLists"] = vsLocal;

                gvMaterialItemList.DataSource = vsLocal.MaterialRecordItemList;
                gvMaterialItemList.DataBind();

                //clear data
                ddlMCode.SelectedIndex = -1;
                tbMName.Text = String.Empty;
                tbMDesc.Text = String.Empty;
                tbQuantity.Text = String.Empty;
                tbRemark.Text = String.Empty;
                lblFMCode.Text = String.Empty;
                lblSerialNo.Text = String.Empty;
                lbAddItem.CommandArgument = "-1";
                break;
            case "cmdAddRec":
                gvMaterialRecList.Visible = false;
                fvMaterialRecList.Visible = true;
                lbAddRec.Visible = false;
                break;
        }
    }
    #endregion btn command

    #region bind data
    protected void getGridData(string dataName, DataMaster dataMaster, int actionType)
    {
        dataMaster = serviceMaster.ActionDataMaster(dataName, dataMaster, actionType);
        ViewState["dataRecord"] = dataMaster;
        setGridData(gvMaterialRecList, dataMaster.MaterialRecordList);
    }

    protected void setGridData(GridView gvName, Object obj)
    {
        gvName.DataSource = obj;
        gvName.DataBind();
    }

    protected void setFormData(FormView fvName, FormViewMode fvMode, Object obj)
    {
        fvName.ChangeMode(fvMode);
        fvName.DataSource = obj;
        fvName.DataBind();

        //set kind ddl
        switch (fvMode)
        {
            case FormViewMode.Insert:
                switch (recStatus)
                {
                    case 100000:
                        setDdlSiteList(fvName, "ddlFromSiteList", 1);
                        setDdlSiteList(fvName, "ddlToSiteList", 2);
                        //create order no.
                        var culture = new System.Globalization.CultureInfo("en-US");
                        string curYear = DateTime.Now.ToString("yy", culture);
                        string curMonth = DateTime.Now.ToString("MM");
                        var orderNum = "001";
                        //get max of order no.
                        DataMaster dataTempGv = (DataMaster)ViewState["dataRecord"];
                        if (dataTempGv.MaterialRecordList != null)
                        {
                            var tData = dataTempGv.MaterialRecordList;
                            var linqMaxOrder = (from m in tData
                                                where m.RecNo.StartsWith(curYear + curMonth) && m.RecStatus == 100000
                                                orderby m.RecNo descending
                                                select m.RecNo).FirstOrDefault();//m.RecNo).Max(); //.FirstOrDefault();

                            orderNum = (int.Parse(linqMaxOrder.Substring(4)) + 1).ToString("D3");

                            //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataTempGv));
                        }


                        TextBox tbOrderNo = (TextBox)fvName.FindControl("tbOrderNo");
                        tbOrderNo.Text = curYear + curMonth + orderNum;
                        tbOrderNo.ReadOnly = true;
                        break;
                    case 800000:
                        setDdlSiteList(fvName, "ddlFromSiteList", 2);
                        setDdlSiteList(fvName, "ddlToSiteList", 1);
                        break;
                }
                //One way to fix this is to modify our LINQ to XML query so that we indicate that YearsAtCompany is a nullable integer.  We can do this by changing the explicit cast to be (int?) instead of (int)
                ////read only selected date
                //TextBox tbOrderCreate = (TextBox)fvName.FindControl("tbOrderCreate");
                //tbOrderCreate.Attributes.Add("readonly", "readonly");
                //material code
                setDdlMCodeList(fvName, "ddlMCode");
                break;
            case FormViewMode.Edit:
                if (ViewState["vsItemLists"] != null)
                {
                    GridView gvMaterialItemList = (GridView)fvName.FindControl("gvMaterialItemList");
                    gvMaterialItemList.DataSource = ((DataMaster)ViewState["vsItemLists"]).MaterialRecordItemList;
                    gvMaterialItemList.DataBind();
                }
                break;
        }
    }

    protected void setDdlSiteList(FormView fvName, string ddlName, int selectType)
    {
        DropDownList ddlList = (DropDownList)fvName.FindControl(ddlName);
        DataMaster dLocal = new DataMaster();
        dLocal = serviceMaster.ActionDataMaster("site", dLocal, 22);

        switch (selectType)
        {
            case 1: //bangping only
                var linq1 = from s
                            in dLocal.SiteList
                            where s.SiteCode.Contains("1031") || s.SiteCode.Contains("1032") || s.SiteCode.Contains("1033")
                            select s;
                ddlList.DataSource = linq1.ToList();
                break;
            case 2: //exception bangping
                var linq2 = from s
                            in dLocal.SiteList
                            where !s.SiteCode.Contains("1031") && !s.SiteCode.Contains("1032") && !s.SiteCode.Contains("1033")
                            select s;
                ddlList.DataSource = linq2.ToList();
                break;
        }
        //ddlList.DataSource = linqData.ToList(); //dLocal.SiteList;
        ddlList.DataTextField = "SiteCodeName";
        ddlList.DataValueField = "SiteIDX";
        ddlList.DataBind();
        ddlList.Items.Insert(0, new ListItem("-- Project --", "-1"));

        if (selectType == 1) //default for bangping
        {
            ddlList.SelectedIndex = 1;
        }
    }

    protected void setDdlMCodeList(FormView fvName, string ddlName)
    {
        DropDownList ddlList = (DropDownList)fvName.FindControl(ddlName);
        DataMaster dLocal = new DataMaster();
        dLocal = serviceMaster.ActionDataMaster("materialreg", dLocal, 22);
        ddlList.DataSource = dLocal.MaterialRegisterList;
        ddlList.DataTextField = "CMCode";
        ddlList.DataValueField = "RegIDX";
        ddlList.DataBind();
        ddlList.Items.Insert(0, new ListItem("-- Mat. Code | S/N --", "-1"));
    }

    protected string setCustomText(int indexIn)
    {
        return textShow[indexIn];
    }
    #endregion bind data

    #region files upload and images
    protected void setDir(string dirName)
    {
        if (!Directory.Exists(Server.MapPath(dirName)))
        {
            Directory.CreateDirectory(Server.MapPath(dirName));
        }
    }

    public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
    {
        var ratio = (double)maxHeight / image.Height;

        var newWidth = (int)(image.Width * ratio);
        var newHeight = (int)(image.Height * ratio);

        var newImage = new Bitmap(newWidth, newHeight);
        using (var g = Graphics.FromImage(newImage))
        {
            g.DrawImage(image, 0, 0, newWidth, newHeight);
        }
        return newImage;
    }
    #endregion files upload and images

    #region status text & css
    protected string textStatus(int status)
    {
        string statusText;
        switch (status)
        {
            case 100000:
                statusText = "รอจัดส่ง";
                break;
            case 200000:
                statusText = "จัดส่งเรียบร้อย";
                break;
            case 800000:
                statusText = "รับคืนเรียบร้อย";
                break;
            default:
                statusText = "error";
                break;
        }

        return statusText;
    }

    protected string cssStatus(int status)
    {
        string statusCss;
        switch (status)
        {
            case 100000:
                statusCss = "";
                break;
            case 800000:
                statusCss = "";
                break;
            default:
                statusCss = "";
                break;
        }

        return statusCss;
    }
    #endregion status text & css

    #region form print
    //protected void Print(object sender, EventArgs e)
    //{
    //    gvPrint.UseAccessibleHeader = true;
    //    gvPrint.HeaderRow.TableSection = TableRowSection.TableHeader;
    //    gvPrint.FooterRow.TableSection = TableRowSection.TableFooter;
    //    gvPrint.Attributes["style"] = "border-collapse:separate";
    //    foreach (GridViewRow row in gvPrint.Rows)
    //    {
    //        if (row.RowIndex % 10 == 0 && row.RowIndex != 0)
    //        {
    //            row.Attributes["style"] = "page-break-after:always;";
    //        }
    //    }
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter hw = new HtmlTextWriter(sw);
    //    gvPrint.RenderControl(hw);
    //    string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
    //    StringBuilder sb = new StringBuilder();
    //    sb.Append("<script type = 'text/javascript'>");
    //    sb.Append("window.onload = new function(){");
    //    sb.Append("var printWin = window.open('', '', 'left=0");
    //    sb.Append(",top=0,width=1000,height=600,status=0');");
    //    sb.Append("printWin.document.write(\"");
    //    string style = "<style type = 'text/css'>thead {display:table-header-group;} tfoot{display:table-footer-group;}</style>";
    //    sb.Append(style + gridHTML);
    //    sb.Append("\");");
    //    sb.Append("printWin.document.close();");
    //    sb.Append("printWin.focus();");
    //    sb.Append("printWin.print();");
    //    sb.Append("printWin.close();");
    //    sb.Append("};");
    //    sb.Append("</script>");
    //    ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
    //    gvPrint.DataBind();
    //}

    int tempcounter = 0;
    protected void gvPrint_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tempcounter = tempcounter + 1;
            if (tempcounter == 10)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    #endregion form print
}