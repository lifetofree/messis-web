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
    #endregion  initial function/data

    protected void Page_Load(object sender, EventArgs e)
    {
        string matType = Page.RouteData.Values["matType"].ToString().ToLower();
        setMaterialView(matType);
        
    }

    #region set active view
    protected void setMaterialView(string matType)
    {
        switch (matType)
        {
            case "kind":
                setMultiViewActive(mvMaterialType, vMaterialKind);
                dataMaster = serviceMaster.ActionDataMaster(matType,dataMaster, 20);
                setGridData(gvKindList, dataMaster.KindList);
                break;
            case "asset":
                setMultiViewActive(mvMaterialType, vMaterialAsset);
                dataMaster = serviceMaster.ActionDataMaster(matType, dataMaster, 20);
                setGridData(gvAssetList, dataMaster.AssetList);
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
    #endregion bind data
}