using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSystem_Material_MaterialList : System.Web.UI.Page
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
            getGridData("materiallist", dataMaster, 20);
            setFormData(fvMaterialList, FormViewMode.Insert, null);
            fvMaterialList.Visible = false;
            
            ViewState["listData"] = null;
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
                fvMaterialList.Visible = true;
                divAction.Visible = false;
                gvMaterialList.Visible = !gvMaterialList.Visible;

                DetailMaterialList matDetail = new DetailMaterialList();
                matDetail.MIDX = int.Parse(cmdArg);
                dataMaster.MaterialList = new DetailMaterialList[1];
                dataMaster.MaterialList[0] = matDetail;
                dataMaster = serviceMaster.ActionDataMaster("materiallist", dataMaster, 20);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster.MaterialList[0]));
                setFormData(fvMaterialList, FormViewMode.Edit, dataMaster.MaterialList);
                DropDownList ddlAsIDXE = (DropDownList)fvMaterialList.FindControl("ddlAsIDXE");
                ddlAsIDXE.SelectedValue = dataMaster.MaterialList[0].AsIDX.ToString();
                DropDownList ddlKIDXE = (DropDownList)fvMaterialList.FindControl("ddlKIDXE");
                ddlKIDXE.SelectedValue = dataMaster.MaterialList[0].KIDX.ToString();
                break;
        }
    }

    protected void gvPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gvName = (GridView)sender;

        gvName.PageIndex = e.NewPageIndex;
        getGridData("materiallist", dataMaster, 20);
        setFormData(fvMaterialList, FormViewMode.Insert, null);
    }
    #endregion grid command

    #region form commnd
    protected void fvCommand(object sender, CommandEventArgs e)
    {
        string cmdName = e.CommandName.ToString();
        string cmdArg = e.CommandArgument.ToString();
        float dFloat = float.Parse("0");

        switch (cmdName)
        {
            case "cmdInsert":
                //set data
                DetailMaterialList matDetail = new DetailMaterialList();
                matDetail.MIDX = 0;
                matDetail.MCode = ((TextBox)fvMaterialList.FindControl("tbMCode")).Text.Trim();
                matDetail.RMIDX = 0;
                matDetail.MNIDX = 0;
                matDetail.MName = ((TextBox)fvMaterialList.FindControl("tbMName")).Text.Trim();
                matDetail.TypeIDX = int.Parse(((DropDownList)fvMaterialList.FindControl("ddlTypeIDX")).SelectedValue);
                matDetail.UnitIDX = int.Parse(((DropDownList)fvMaterialList.FindControl("ddlUnitIDX")).SelectedValue);
                matDetail.AsIDX = int.Parse(((DropDownList)fvMaterialList.FindControl("ddlAsIDX")).SelectedValue);
                matDetail.MDesc = ((TextBox)fvMaterialList.FindControl("tbMDesc")).Text.Trim();
                matDetail.KIDX = int.Parse(((DropDownList)fvMaterialList.FindControl("ddlKIDX")).SelectedValue);
                matDetail.RUD = float.TryParse(((TextBox)fvMaterialList.FindControl("tbRUD")).Text.Trim(), out dFloat) ? float.Parse(((TextBox)fvMaterialList.FindControl("tbRUD")).Text.Trim()) : dFloat;
                matDetail.MStatus = 1;
                dataMaster.MaterialList = new DetailMaterialList[1];
                dataMaster.MaterialList[0] = matDetail;

                actionType = int.Parse("1" + "1");

                //execute data
                dataMaster = serviceMaster.ActionDataMaster("materiallist", dataMaster, actionType);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));
                //get return code
                localString = dataMaster.ReturnCode;

                //get current data
                if (localString == "0")
                {
                    funcWeb.ShowAlert(this, "ดำเนินการเรียบร้อยแล้วค่ะ");
                    //clear form
                    setFormData(fvMaterialList, FormViewMode.Insert, null);
                }
                else
                {
                    funcWeb.ShowAlert(this, localString);
                }
                setGridData(gvMaterialList, dataMaster.MaterialList);
                break;
            case "cmdUpdate":
                DetailMaterialList matDetailE = new DetailMaterialList();
                matDetailE.MIDX = int.Parse(((Label)fvMaterialList.FindControl("lblMIDXE")).Text);
                matDetailE.MCode = ((TextBox)fvMaterialList.FindControl("tbMCodeE")).Text.Trim();
                matDetailE.RMIDX = 0;
                matDetailE.MNIDX = int.Parse(((Label)fvMaterialList.FindControl("lblMNIDX")).Text);
                matDetailE.TypeIDX = int.Parse(((DropDownList)fvMaterialList.FindControl("ddlTypeIDXE")).SelectedValue);
                matDetailE.UnitIDX = int.Parse(((DropDownList)fvMaterialList.FindControl("ddlUnitIDXE")).SelectedValue);
                matDetailE.AsIDX = int.Parse(((DropDownList)fvMaterialList.FindControl("ddlAsIDXE")).SelectedValue);
                matDetailE.MName = ((TextBox)fvMaterialList.FindControl("tbMNameE")).Text.Trim();
                matDetailE.MDesc = ((TextBox)fvMaterialList.FindControl("tbMDescE")).Text.Trim();
                matDetailE.KIDX = int.Parse(((DropDownList)fvMaterialList.FindControl("ddlKIDXE")).SelectedValue);
                matDetailE.RUD = float.Parse(((TextBox)fvMaterialList.FindControl("tbRUDE")).Text.Trim());
                matDetailE.MStatus = int.Parse(((DropDownList)fvMaterialList.FindControl("ddlMStatusE")).SelectedValue);;
                dataMaster.MaterialList = new DetailMaterialList[1];
                dataMaster.MaterialList[0] = matDetailE;

                actionType = int.Parse("1" + ((DropDownList)fvMaterialList.FindControl("ddlMStatusE")).SelectedValue);

                //execute data
                dataMaster = serviceMaster.ActionDataMaster("materiallist", dataMaster, actionType);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));
                //get return code
                localString = dataMaster.ReturnCode;

                //get current data
                if (localString == "0")
                {
                    funcWeb.ShowAlert(this, "ดำเนินการเรียบร้อยแล้วค่ะ");
                    //clear form
                    setFormData(fvMaterialList, FormViewMode.Insert, null);
                }
                else
                {
                    funcWeb.ShowAlert(this, localString);
                }
                setGridData(gvMaterialList, dataMaster.MaterialList);
                break;
            case "cmdReset":
                //clear form
                setFormData(fvMaterialList, FormViewMode.Insert, null);
                break;
            case "cmdCancel":
                //clear form
                setFormData(fvMaterialList, FormViewMode.Insert, null);
                break;
        }

        fvMaterialList.Visible = !fvMaterialList.Visible;
        divAction.Visible = !divAction.Visible;
        gvMaterialList.Visible = !gvMaterialList.Visible;
    }
    #endregion form commnd

    #region btn command
    protected void btnCommand(object sender, CommandEventArgs e)
    {
        string cmdName = e.CommandName.ToString();
        string cmdArg = e.CommandArgument.ToString();

        switch (cmdName)
        {
            case "cmdAddMatList":
                fvMaterialList.Visible = true;
                divAction.Visible = false;
                gvMaterialList.Visible = !gvMaterialList.Visible;
                break;
            case "cmdSearchMatList":
                if (tbSearch.Text.Trim() != String.Empty)
                {
                    //set data
                    DetailMaterialList matSearch = new DetailMaterialList();
                    matSearch.MIDX = -1;
                    matSearch.MDesc = tbSearch.Text.Trim();

                    dataMaster.MaterialList = new DetailMaterialList[1];
                    dataMaster.MaterialList[0] = matSearch;

                    actionType = 23;
                    //execute data
                    dataMaster = serviceMaster.ActionDataMaster("materiallist", dataMaster, actionType);
                    ViewState["listData"] = dataMaster.MaterialList;
                    setGridData(gvMaterialList, ViewState["listData"]);
                }
                break;
            case "cmdSearchReset":
                ViewState["listData"] = null;
                tbSearch.Text = String.Empty;
                getGridData("materiallist", dataMaster, 20);
                break;
        }
    }
    #endregion btn command

    #region bind data
    protected void getGridData(string dataName, DataMaster dataMaster, int actionType)
    {
        if (ViewState["listData"] == null)
        {
            dataMaster = serviceMaster.ActionDataMaster(dataName, dataMaster, actionType);
            ViewState["listData"] = dataMaster.MaterialList;
        }
        
        setGridData(gvMaterialList, ViewState["listData"]);
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
                setDdlList(fvName, "ddlKIDX", "kind", "3");
                setDdlList(fvName, "ddlAsIDX", "asset", "1");
                setDdlList(fvName, "ddlTypeIDX", "materialtype", "1");
                setDdlList(fvName, "ddlUnitIDX", "unit", "1");
                break;
            case FormViewMode.Edit:
                HiddenField typeIDX = (HiddenField)fvName.FindControl("hfTypeIDXE");
                HiddenField unitIDX = (HiddenField)fvName.FindControl("hfUnitIDXE");
                HiddenField kIDX = (HiddenField)fvName.FindControl("hfKIDXE");
                HiddenField asIDX = (HiddenField)fvName.FindControl("hfAsIDXE");
                setDdlList(fvName, "ddlKIDXE", "kind", kIDX.Value);
                setDdlList(fvName, "ddlAsIDXE", "asset", asIDX.Value);
                setDdlList(fvName, "ddlTypeIDXE", "materialtype", typeIDX.Value);
                setDdlList(fvName, "ddlUnitIDXE", "unit", unitIDX.Value);
                break;
        }
    }

    protected void setDdlList(FormView fvName, string ddlName, string cText, string dSelect)
    {
        DropDownList ddlList = (DropDownList)fvName.FindControl(ddlName);
        DataMaster dLocal = new DataMaster();
        dLocal = serviceMaster.ActionDataMaster(cText, dLocal, 21);
        switch(cText)
        {
          case "kind":
              ddlList.DataSource = dLocal.KindList;
              ddlList.DataTextField = "KName";
              ddlList.DataValueField = "KIDX";
              break;
          case "asset":
              ddlList.DataSource = dLocal.AssetList;
              ddlList.DataTextField = "AsName";
              ddlList.DataValueField = "AsIDX";
              break;
          case "materialtype":
              ddlList.DataSource = dLocal.MaterialTypeList;
              ddlList.DataTextField = "TypeName";
              ddlList.DataValueField = "TypeIDX";
              break;
          case "unit":
              ddlList.DataSource = dLocal.UnitList;
              ddlList.DataTextField = "UnitName";
              ddlList.DataValueField = "UnitIDX";
              break;
        }
        ddlList.DataBind();
        //ddlList.Items.Insert(0, new ListItem("-- Select Kind --", "0"));
        ddlList.SelectedValue = dSelect;
    }

    // protected void setDdlKindList(FormView fvName, string ddlName)
    // {
    //     DropDownList ddlList = (DropDownList)fvName.FindControl(ddlName);
    //     DataMaster dLocal = new DataMaster();
    //     dLocal = serviceMaster.ActionDataMaster("kind", dLocal, 21);
    //     ddlList.DataSource = dLocal.KindList;
    //     ddlList.DataTextField = "KName";
    //     ddlList.DataValueField = "KIDX";
    //     ddlList.DataBind();
    //     //ddlList.Items.Insert(0, new ListItem("-- Select Kind --", "0"));
    //     ddlList.SelectedValue = "3";
    // }
    //
    // protected void setDdlAssetList(FormView fvName, string ddlName)
    // {
    //     DropDownList ddlList = (DropDownList)fvName.FindControl(ddlName);
    //     DataMaster dLocal = new DataMaster();
    //     dLocal = serviceMaster.ActionDataMaster("asset", dLocal, 21);
    //     ddlList.DataSource = dLocal.AssetList;
    //     ddlList.DataTextField = "AsName";
    //     ddlList.DataValueField = "AsIDX";
    //     ddlList.DataBind();
    //     //ddlList.Items.Insert(0, new ListItem("-- Select Asset Type --", "0"));
    //     ddlList.SelectedValue = "1";
    // }
    #endregion bind data
}
