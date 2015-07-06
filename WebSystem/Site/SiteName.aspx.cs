using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSystem_Site_SiteName : System.Web.UI.Page
{
    #region initial function/data
    FunctionWeb funcWeb = new FunctionWeb();
    ServiceMaster serviceMaster = new ServiceMaster();
    DataMaster dataMaster = new DataMaster();
    string localXml = String.Empty;
    string localString = String.Empty;
    int actionType = 0;
    #endregion  initial function/data

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getGridData("site", dataMaster, 20);
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
                DetailSiteList siteDetail = new DetailSiteList();
                siteDetail.SiteIDX = int.Parse(cmdArg);
                dataMaster.SiteList = new DetailSiteList[1];
                dataMaster.SiteList[0] = siteDetail;
                dataMaster = serviceMaster.ActionDataMaster("site", dataMaster, 20);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster.SiteList[0]));
                setFormData(fvSiteList, FormViewMode.Edit, dataMaster.SiteList);

                // //read only selected date
                // TextBox tbSiteStartE = (TextBox)fvSiteList.FindControl("tbSiteStartE");
                // TextBox tbSiteEndE = (TextBox)fvSiteList.FindControl("tbSiteEndE");
                // tbSiteStartE.Attributes.Add("readonly", "readonly");
                // tbSiteEndE.Attributes.Add("readonly", "readonly");
                break;
            case "cmdCancel":
                break;
        }
    }

    protected void gvPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gvName = (GridView)sender;

        gvName.PageIndex = e.NewPageIndex;
        getGridData("site", dataMaster, 20);
        setFormData(fvSiteList, FormViewMode.Insert, null);
    }
    #endregion grid command

    #region form commnd
    protected void fvCommand(object sender, CommandEventArgs e)
    {
        string cmdName = e.CommandName.ToString();
        string cmdArg = e.CommandArgument.ToString();

        switch (cmdName)
        {
            case "cmdInsert":
                //set data
                DetailSiteList siteDetail = new DetailSiteList();
                siteDetail.SiteIDX = 0;
                siteDetail.SiteCode = ((TextBox)fvSiteList.FindControl("tbSiteCode")).Text.Trim();
                siteDetail.RSiteIDX = 0;
                siteDetail.ProjectName = ((TextBox)fvSiteList.FindControl("tbProjectName")).Text.Trim();
                siteDetail.ManagerName = ((TextBox)fvSiteList.FindControl("tbManagerName")).Text.Trim();
                siteDetail.StaffName = ((TextBox)fvSiteList.FindControl("tbStaffName")).Text.Trim();
                siteDetail.SiteLocation = ((TextBox)fvSiteList.FindControl("tbSiteLocation")).Text.Trim();
                siteDetail.SiteStart = ((TextBox)fvSiteList.FindControl("tbSiteStart")).Text.Trim();
                siteDetail.SiteEnd = ((TextBox)fvSiteList.FindControl("tbSiteEnd")).Text.Trim();
                siteDetail.SiteStatus = 1;
                dataMaster.SiteList = new DetailSiteList[1];
                dataMaster.SiteList[0] = siteDetail;

                actionType = int.Parse("1" + "1");

                //execute data
                dataMaster = serviceMaster.ActionDataMaster("site", dataMaster, actionType);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));
                //get return code
                localString = dataMaster.ReturnCode;
                //get current data
                if (localString == "0")
                {
                    funcWeb.ShowAlert(this, "ดำเนินการเรียบร้อยแล้วค่ะ");
                    //clear form
                    setFormData(fvSiteList, FormViewMode.Insert, null);
                }
                else
                {
                    funcWeb.ShowAlert(this, localString);
                }
                setGridData(gvSiteList, dataMaster.SiteList);
                break;
            case "cmdUpdate":
                //set data
                DetailSiteList siteDetailE = new DetailSiteList();
                siteDetailE.SiteIDX = int.Parse(((Label)fvSiteList.FindControl("lblSiteIDXE")).Text);
                siteDetailE.SiteCode = ((TextBox)fvSiteList.FindControl("tbSiteCodeE")).Text.Trim();
                siteDetailE.RSiteIDX = int.Parse(((Label)fvSiteList.FindControl("lblRSiteIDXE")).Text);
                siteDetailE.ProjectName = ((TextBox)fvSiteList.FindControl("tbProjectNameE")).Text.Trim();
                siteDetailE.ManagerName = ((TextBox)fvSiteList.FindControl("tbManagerNameE")).Text.Trim();
                siteDetailE.StaffName = ((TextBox)fvSiteList.FindControl("tbStaffNameE")).Text.Trim();
                siteDetailE.SiteLocation = ((TextBox)fvSiteList.FindControl("tbSiteLocationE")).Text.Trim();
                siteDetailE.SiteStart = ((TextBox)fvSiteList.FindControl("tbSiteStartE")).Text.Trim();
                siteDetailE.SiteEnd = ((TextBox)fvSiteList.FindControl("tbSiteEndE")).Text.Trim();
                siteDetailE.SiteStatus = int.Parse(((DropDownList)fvSiteList.FindControl("ddlSiteStatusE")).SelectedValue);
                dataMaster.SiteList = new DetailSiteList[1];
                dataMaster.SiteList[0] = siteDetailE;

                actionType = int.Parse("1" + ((DropDownList)fvSiteList.FindControl("ddlSiteStatusE")).SelectedValue);
                //execute data
                dataMaster = serviceMaster.ActionDataMaster("site", dataMaster, actionType);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));
                //get return code
                localString = dataMaster.ReturnCode;
                //get current data
                if (localString == "0")
                {
                    funcWeb.ShowAlert(this, "ดำเนินการเรียบร้อยแล้วค่ะ");
                    //clear form
                    setFormData(fvSiteList, FormViewMode.Insert, null);
                }
                else
                {
                    funcWeb.ShowAlert(this, localString);
                }
                setGridData(gvSiteList, dataMaster.SiteList);
                break;
            case "cmdReset":
                //clear form
                setFormData(fvSiteList, FormViewMode.Insert, null);
                break;
            case "cmdCancel":
                //clear form
                setFormData(fvSiteList, FormViewMode.Insert, null);
                break;
        }
    }
    #endregion form commnd

    #region bind data
    protected void getGridData(string dataName, DataMaster dataMaster, int actionType)
    {
        dataMaster = serviceMaster.ActionDataMaster(dataName, dataMaster, actionType);
        setGridData(gvSiteList, dataMaster.SiteList);
    }

    protected void setGridData(GridView gvName, Object obj)
    {
        gvName.DataSource = obj;
        gvName.DataBind();
        //FunctionWeb func = new FunctionWeb();
        //func.MakeGridViewPrinterFriendly(gvName);

        // //read only selected date
        // TextBox tbSiteStart = (TextBox)fvSiteList.FindControl("tbSiteStart");
        // TextBox tbSiteEnd = (TextBox)fvSiteList.FindControl("tbSiteEnd");
        // tbSiteStart.Attributes.Add("readonly", "readonly");
        // tbSiteEnd.Attributes.Add("readonly", "readonly");
    }

    protected void setFormData(FormView fvName, FormViewMode fvMode, Object obj)
    {
        fvName.ChangeMode(fvMode);
        fvName.DataSource = obj;
        fvName.DataBind();
    }
    #endregion bind data
}