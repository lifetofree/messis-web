using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.IO;
using System.Drawing;

public partial class WebSystem_Material_MaterialReport : System.Web.UI.Page
{
    #region initial function/data
    FunctionWeb funcWeb = new FunctionWeb();
    ServiceMaster serviceMaster = new ServiceMaster();
    DataMaster dataMaster = new DataMaster();
    string localXml = String.Empty;
    string localString = String.Empty;
    int actionType = 0;

    string imgPath = ConfigurationSettings.AppSettings["MaterialImages"];
    #endregion  initial function/data

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getGridData("materialreport", dataMaster, 20);
        }
    }

    #region bind data
    protected void getGridData(string dataName, DataMaster dataMaster, int actionType)
    {
        dataMaster = serviceMaster.ActionDataMaster(dataName, dataMaster, actionType);
        setGridData(gvMaterialReport, dataMaster.MaterialRegisterList);
    }

    protected void setGridData(GridView gvName, Object obj)
    {
        gvName.DataSource = obj;
        gvName.DataBind();
    }
    #endregion bind data
}