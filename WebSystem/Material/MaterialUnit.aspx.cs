using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSystem_Material_MaterialUnit : System.Web.UI.Page
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
            getGridData("unit", dataMaster, 20);
            setFormData(fvUnitList, FormViewMode.Insert, null);
            fvUnitList.Visible = false;
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
                fvUnitList.Visible = true;
                lbAddUnit.Visible = false;
                gvUnitList.Visible = !gvUnitList.Visible;

                DetailUnitList typeDetail = new DetailUnitList();
                typeDetail.UnitIDX = int.Parse(cmdArg);
                dataMaster.UnitList = new DetailUnitList[1];
                dataMaster.UnitList[0] = typeDetail;
                dataMaster = serviceMaster.ActionDataMaster("unit", dataMaster, 20);

                setFormData(fvUnitList, FormViewMode.Edit, dataMaster.UnitList);
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
                DetailUnitList typeList = new DetailUnitList();
                typeList.UnitIDX = 0;
                typeList.UnitName = ((TextBox)fvUnitList.FindControl("tbName")).Text.Trim();
                typeList.UnitDesc = ((TextBox)fvUnitList.FindControl("tbDesc")).Text.Trim();

                dataMaster.UnitList = new DetailUnitList[1];
                dataMaster.UnitList[0] = typeList;

                actionType = int.Parse("1" + "1");
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));

                //execute data
                dataMaster = serviceMaster.ActionDataMaster("unit", dataMaster, actionType);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));
                //get return code
                localString = dataMaster.ReturnCode;

                //get current data
                if (localString.ToString() == "0")
                {
                    funcWeb.ShowAlert(this, "ดำเนินการเรียบร้อยแล้วค่ะ");
                    //clear form
                    setFormData(fvUnitList, FormViewMode.Insert, null);
                    fvUnitList.Visible = !fvUnitList.Visible;
                    lbAddUnit.Visible = !lbAddUnit.Visible;
                    gvUnitList.Visible = !gvUnitList.Visible;
                }
                else
                {
                    funcWeb.ShowAlert(this, localString);
                }
                setGridData(gvUnitList, dataMaster.UnitList);
                break;
            case "cmdUpdate":
                DetailUnitList typeListE = new DetailUnitList();
                typeListE.UnitIDX = int.Parse(((Label)fvUnitList.FindControl("lblUnitIDXE")).Text); ;
                typeListE.UnitName = ((TextBox)fvUnitList.FindControl("tbName")).Text.Trim();
                typeListE.UnitDesc = ((TextBox)fvUnitList.FindControl("tbDesc")).Text.Trim();
                typeListE.UnitStatus = int.Parse(((DropDownList)fvUnitList.FindControl("ddlStatusE")).SelectedValue);

                dataMaster.UnitList = new DetailUnitList[1];
                dataMaster.UnitList[0] = typeListE;

                actionType = int.Parse("1" + ((DropDownList)fvUnitList.FindControl("ddlStatusE")).SelectedValue);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));

                //execute data
                dataMaster = serviceMaster.ActionDataMaster("unit", dataMaster, actionType);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));
                //get return code
                localString = dataMaster.ReturnCode;

                //get current data
                if (localString.ToString() == "0")
                {
                    funcWeb.ShowAlert(this, "ดำเนินการเรียบร้อยแล้วค่ะ");
                    //clear form
                    setFormData(fvUnitList, FormViewMode.Insert, null);
                    fvUnitList.Visible = !fvUnitList.Visible;
                    lbAddUnit.Visible = !lbAddUnit.Visible;
                    gvUnitList.Visible = !gvUnitList.Visible;
                }
                else
                {
                    funcWeb.ShowAlert(this, localString);
                }
                setGridData(gvUnitList, dataMaster.UnitList);
                break;
            case "cmdReset":
                //clear form
                setFormData(fvUnitList, FormViewMode.Insert, null);
                fvUnitList.Visible = !fvUnitList.Visible;
                lbAddUnit.Visible = !lbAddUnit.Visible;
                gvUnitList.Visible = !gvUnitList.Visible;
                break;
            case "cmdCancel":
                //clear form
                setFormData(fvUnitList, FormViewMode.Insert, null);
                fvUnitList.Visible = !fvUnitList.Visible;
                lbAddUnit.Visible = !lbAddUnit.Visible;
                gvUnitList.Visible = !gvUnitList.Visible;
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
            case "cmdAddUnit":
                fvUnitList.Visible = true;
                lbAddUnit.Visible = false;
                gvUnitList.Visible = !gvUnitList.Visible;
                break;
        }
    }
    #endregion btn command

    #region bind data
    protected void getGridData(string dataName, DataMaster dataMaster, int actionType)
    {
        dataMaster = serviceMaster.ActionDataMaster(dataName, dataMaster, actionType);
        setGridData(gvUnitList, dataMaster.UnitList);
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
    }
    #endregion bind data
}
