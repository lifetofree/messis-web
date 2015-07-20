using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSystem_Material_MaterialTypeList : System.Web.UI.Page
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
            getGridData("materialtype", dataMaster, 20);
            setFormData(fvTypeList, FormViewMode.Insert, null);
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
                gvTypeList.Visible = !gvTypeList.Visible;

                DetailMaterialTypeList typeDetail = new DetailMaterialTypeList();
                typeDetail.TypeIDX = int.Parse(cmdArg);
                dataMaster.MaterialTypeList = new DetailMaterialTypeList[1];
                dataMaster.MaterialTypeList[0] = typeDetail;
                dataMaster = serviceMaster.ActionDataMaster("materialtype", dataMaster, 20);

                setFormData(fvTypeList, FormViewMode.Edit, dataMaster.MaterialTypeList);
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
                DetailMaterialTypeList typeList = new DetailMaterialTypeList();
                typeList.TypeIDX = 0;
                typeList.TypeName = ((TextBox)fvTypeList.FindControl("tbName")).Text.Trim();
                typeList.TypeDesc = ((TextBox)fvTypeList.FindControl("tbDesc")).Text.Trim();

                dataMaster.MaterialTypeList = new DetailMaterialTypeList[1];
                dataMaster.MaterialTypeList[0] = typeList;

                actionType = int.Parse("1" + "1");
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));

                //execute data
                dataMaster = serviceMaster.ActionDataMaster("materialtype", dataMaster, actionType);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));
                //get return code
                localString = dataMaster.ReturnCode;

                //get current data
                if (localString.ToString() == "0")
                {
                    funcWeb.ShowAlert(this, "ดำเนินการเรียบร้อยแล้วค่ะ");
                    //clear form
                    setFormData(fvTypeList, FormViewMode.Insert, null);
                    fvTypeList.Visible = !fvTypeList.Visible;
                    lbAddType.Visible = !lbAddType.Visible;
                    gvTypeList.Visible = !gvTypeList.Visible;
                }
                else
                {
                    funcWeb.ShowAlert(this, localString);
                }
                setGridData(gvTypeList, dataMaster.MaterialTypeList);
                break;
            case "cmdUpdate":
                DetailMaterialTypeList typeListE = new DetailMaterialTypeList();
                typeListE.TypeIDX = int.Parse(((Label)fvTypeList.FindControl("lblTypeIDXE")).Text); ;
                typeListE.TypeName = ((TextBox)fvTypeList.FindControl("tbName")).Text.Trim();
                typeListE.TypeDesc = ((TextBox)fvTypeList.FindControl("tbDesc")).Text.Trim();
                typeListE.TypeStatus = int.Parse(((DropDownList)fvTypeList.FindControl("ddlStatusE")).SelectedValue);

                dataMaster.MaterialTypeList = new DetailMaterialTypeList[1];
                dataMaster.MaterialTypeList[0] = typeListE;

                actionType = int.Parse("1" + ((DropDownList)fvTypeList.FindControl("ddlStatusE")).SelectedValue);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));

                //execute data
                dataMaster = serviceMaster.ActionDataMaster("materialtype", dataMaster, actionType);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));
                //get return code
                localString = dataMaster.ReturnCode;

                //get current data
                if (localString.ToString() == "0")
                {
                    funcWeb.ShowAlert(this, "ดำเนินการเรียบร้อยแล้วค่ะ");
                    //clear form
                    setFormData(fvTypeList, FormViewMode.Insert, null);
                    fvTypeList.Visible = !fvTypeList.Visible;
                    lbAddType.Visible = !lbAddType.Visible;
                    gvTypeList.Visible = !gvTypeList.Visible;
                }
                else
                {
                    funcWeb.ShowAlert(this, localString);
                }
                setGridData(gvTypeList, dataMaster.MaterialTypeList);
                break;
            case "cmdReset":
                //clear form
                setFormData(fvTypeList, FormViewMode.Insert, null);
                fvTypeList.Visible = !fvTypeList.Visible;
                lbAddType.Visible = !lbAddType.Visible;
                gvTypeList.Visible = !gvTypeList.Visible;
                break;
            case "cmdCancel":
                //clear form
                setFormData(fvTypeList, FormViewMode.Insert, null);
                fvTypeList.Visible = !fvTypeList.Visible;
                lbAddType.Visible = !lbAddType.Visible;
                gvTypeList.Visible = !gvTypeList.Visible;
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
                gvTypeList.Visible = !gvTypeList.Visible;
                break;
        }
    }
    #endregion btn command

    #region bind data
    protected void getGridData(string dataName, DataMaster dataMaster, int actionType)
    {
        dataMaster = serviceMaster.ActionDataMaster(dataName, dataMaster, actionType);
        setGridData(gvTypeList, dataMaster.MaterialTypeList);
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