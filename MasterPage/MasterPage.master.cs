using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        setPanelTitle();
    }

    #region set panel title
    protected void setPanelTitle()
    {
        string path = Request.Url.AbsolutePath;
        string module = path.Substring(path.LastIndexOf('/') + 1).ToLower();
        string moduleName;

        switch (module)
        {
            case "dashboard":
                moduleName = "Dashboard";
                break;
            case "in":
                moduleName = "บันทึกวัสดุภายในคลัง(เข้า)";
                break;
            case "out":
                moduleName = "บันทึกวัสดุภายในคลัง(ออก)";
                break;
            case "materiallist":
                moduleName = "รายการวัสดุภายในคลัง";
                break;
            case "materialregister":
                moduleName = "ทะเบียนวัสดุภายในคลัง";
                break;
            case "kind":
                moduleName = "ประเภทวัสดุภายในคลัง(Kind)";
                break;
            case "asset":
                moduleName = "ประเภทวัสดุภายในคลัง(Asset)";
                break;
            case "materialunit":
                moduleName = "ประเภทหน่วยวัด(Unit)";
                break;
            case "sitename":
                moduleName = "รายชื่อไซต์งาน";
                break;
            case "materialreport":
                moduleName = "รายงานความเคลื่อนไหว";
                break;
            default:
                moduleName = "Dashboard";
                break;
        }

        lblPanelTitle.Text = moduleName;
    }
    #endregion set panel title
}
