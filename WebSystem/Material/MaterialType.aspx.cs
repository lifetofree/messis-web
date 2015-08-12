using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSystem_Material_MaterialType : System.Web.UI.Page
{
    #region initial function/data
    FunctionWeb funcWeb = new FunctionWeb();
    ServiceMaster serviceMaster = new ServiceMaster();
    DataMaster dataMaster = new DataMaster();
    string localXml = String.Empty;
    string localString = String.Empty;
    int actionType = 0;

    string matType = "";
    string[] textShow = null;
    string[] textKind = { "Name", "Description" };
    string[] textAsset = { "Name", "Description" };
    #endregion  initial function/data

    protected void Page_Load(object sender, EventArgs e)
    {
        matType = Page.RouteData.Values["matType"].ToString().ToLower();
        textShow = (matType == "kind") ? textKind : textAsset; //text show
        setMaterialView(matType);

        if(!IsPostBack)
        {
            fvTypeList.Visible = false;
        }
    }

    #region grid command
    protected void gvRowCommand(object sender, GridViewCommandEventArgs e)
    {
        string cmdName = e.CommandName.ToString();
        string cmdArg = e.CommandArgument.ToString();

        switch (cmdName)
        {
            case "cmdEdit":
                fvTypeList.Visible = true;
                lbAddType.Visible = false;
                gvKindList.Visible = !gvKindList.Visible;
                gvAssetList.Visible = !gvAssetList.Visible;

                switch(matType)
                {
                    case "kind":
                        DetailKindList kindDetail = new DetailKindList();
                        kindDetail.KIDX = int.Parse(cmdArg);
                        dataMaster.KindList = new DetailKindList[1];
                        dataMaster.KindList[0] = kindDetail;
                        dataMaster = serviceMaster.ActionDataMaster(matType, dataMaster, 20);
                        //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster.SiteList[0]));
                        setFormData(fvTypeList, FormViewMode.Edit, dataMaster.KindList);
                        break;
                    case "asset":
                        DetailAssetList assetDetail = new DetailAssetList();
                        assetDetail.AsIDX = int.Parse(cmdArg);
                        dataMaster.AssetList = new DetailAssetList[1];
                        dataMaster.AssetList[0] = assetDetail;
                        dataMaster = serviceMaster.ActionDataMaster(matType, dataMaster, 20);
                        //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster.SiteList[0]));
                        setFormData(fvTypeList, FormViewMode.Edit, dataMaster.AssetList);
                        break;
                }
                break;
        }
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
                switch(matType)
                {
                    case "kind":
                        DetailKindList kindList = new DetailKindList();
                        kindList.KIDX = 0;
                        kindList.KName = ((TextBox)fvTypeList.FindControl("tbName")).Text.Trim();
                        kindList.KDesc = ((TextBox)fvTypeList.FindControl("tbDesc")).Text.Trim();

                        dataMaster.KindList = new DetailKindList[1];
                        dataMaster.KindList[0] = kindList;
                        break;
                    case "asset":
                        DetailAssetList assetList = new DetailAssetList();
                        assetList.AsIDX = 0;
                        assetList.AsName = ((TextBox)fvTypeList.FindControl("tbName")).Text.Trim();
                        assetList.AsDesc = ((TextBox)fvTypeList.FindControl("tbDesc")).Text.Trim();

                        dataMaster.AssetList = new DetailAssetList[1];
                        dataMaster.AssetList[0] = assetList;
                        break;
                }

                actionType = int.Parse("1" + "1");
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));

                //execute data
                dataMaster = serviceMaster.ActionDataMaster(matType, dataMaster, actionType);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));
                //get return code
                localString = dataMaster.ReturnCode;

                //get current data
                if (localString == "0")
                {
                    funcWeb.ShowAlert(this, "ดำเนินการเรียบร้อยแล้วค่ะ");
                    //clear form
                    setFormData(fvTypeList, FormViewMode.Insert, null);
                    fvTypeList.Visible = false;
                    lbAddType.Visible = true;
                    setMaterialView(matType);
                }
                else
                {
                    funcWeb.ShowAlert(this, localString);
                }
                break;
            case "cmdUpdate":
                switch (matType)
                {
                    case "kind":
                        DetailKindList kindList = new DetailKindList();
                        kindList.KIDX = int.Parse(((Label)fvTypeList.FindControl("lblTypeIDXE")).Text);
                        kindList.KName = ((TextBox)fvTypeList.FindControl("tbName")).Text.Trim();
                        kindList.KDesc = ((TextBox)fvTypeList.FindControl("tbDesc")).Text.Trim();
                        kindList.KStatus = int.Parse(((DropDownList)fvTypeList.FindControl("ddlStatusE")).SelectedValue);

                        dataMaster.KindList = new DetailKindList[1];
                        dataMaster.KindList[0] = kindList;
                        break;
                    case "asset":
                        DetailAssetList assetList = new DetailAssetList();
                        assetList.AsIDX = int.Parse(((Label)fvTypeList.FindControl("lblTypeIDXE")).Text); ;
                        assetList.AsName = ((TextBox)fvTypeList.FindControl("tbName")).Text.Trim();
                        assetList.AsDesc = ((TextBox)fvTypeList.FindControl("tbDesc")).Text.Trim();
                        assetList.AsStatus = int.Parse(((DropDownList)fvTypeList.FindControl("ddlStatusE")).SelectedValue);

                        dataMaster.AssetList = new DetailAssetList[1];
                        dataMaster.AssetList[0] = assetList;
                        break;
                }

                actionType = int.Parse("1" + ((DropDownList)fvTypeList.FindControl("ddlStatusE")).SelectedValue);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));

                //execute data
                dataMaster = serviceMaster.ActionDataMaster(matType, dataMaster, actionType);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));
                //get return code
                localString = dataMaster.ReturnCode;

                //get current data
                if (localString == "0")
                {
                    funcWeb.ShowAlert(this, "ดำเนินการเรียบร้อยแล้วค่ะ");
                    //clear form
                    setFormData(fvTypeList, FormViewMode.Insert, null);
                    fvTypeList.Visible = false;
                    lbAddType.Visible = true;
                    setMaterialView(matType);
                }
                else
                {
                    funcWeb.ShowAlert(this, localString);
                }
                break;
            case "cmdReset":
                //clear form
                setFormData(fvTypeList, FormViewMode.Insert, null);
                fvTypeList.Visible = false;
                lbAddType.Visible = true;
                break;
            case "cmdCancel":
                //clear form
                setFormData(fvTypeList, FormViewMode.Insert, null);
                fvTypeList.Visible = false;
                lbAddType.Visible = true;
                break;
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
            case "cmdAddType":
                fvTypeList.Visible = true;
                lbAddType.Visible = false;
                gvKindList.Visible = !gvKindList.Visible;
                gvAssetList.Visible = !gvAssetList.Visible;
                break;
        }
    }
    #endregion btn command

    #region set active view
    protected void setMaterialView(string matType)
    {
        switch (matType)
        {
            case "kind":
                setMultiViewActive(mvMaterialType, vMaterialKind);
                dataMaster = serviceMaster.ActionDataMaster(matType,dataMaster, 20);
                setGridData(gvKindList, dataMaster.KindList);
                gvKindList.Visible = true;
                break;
            case "asset":
                setMultiViewActive(mvMaterialType, vMaterialAsset);
                dataMaster = serviceMaster.ActionDataMaster(matType, dataMaster, 20);
                setGridData(gvAssetList, dataMaster.AssetList);
                gvAssetList.Visible = true;
                break;
        }
    }

    protected void setMultiViewActive(MultiView mvName, View vName)
    {
        mvName.SetActiveView(vName);
    }
    #endregion set active view

    #region bind data
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

        //set data
        switch (fvMode)
        {
            case FormViewMode.Insert:
                break;
            case FormViewMode.Edit:
                Label lblTypeIDXE = (Label)fvName.FindControl("lblTypeIDXE");
                TextBox tbName = (TextBox)fvTypeList.FindControl("tbName");
                TextBox tbDesc = (TextBox)fvTypeList.FindControl("tbDesc");
                DropDownList ddlStatusE = (DropDownList)fvName.FindControl("ddlStatusE");
                switch (matType)
                {
                    case "kind":
                        DetailKindList[] tmpK = (DetailKindList[])obj;
                        lblTypeIDXE.Text = tmpK[0].KIDX.ToString();
                        tbName.Text = tmpK[0].KName;
                        tbDesc.Text = tmpK[0].KDesc;
                        ddlStatusE.SelectedValue = tmpK[0].KStatus.ToString();
                        break;
                    case "asset":
                        DetailAssetList[] tmpA = (DetailAssetList[])obj;
                        lblTypeIDXE.Text = tmpA[0].AsIDX.ToString();
                        tbName.Text = tmpA[0].AsName;
                        tbDesc.Text = tmpA[0].AsDesc;
                        ddlStatusE.SelectedValue = tmpA[0].AsStatus.ToString();
                        break;
                }
                break;
        }
    }

    protected string setCustomText(int indexIn)
    {
        return textShow[indexIn];
    }
    #endregion bind data
}