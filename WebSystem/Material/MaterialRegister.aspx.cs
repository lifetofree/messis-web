using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.IO;
using System.Drawing;

public partial class WebSystem_Material_MaterialRegister : System.Web.UI.Page
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
            getGridData("materialreg", dataMaster, 20);
            setFormData(fvMaterialRegList, FormViewMode.Insert, null);
            fvMaterialRegList.Visible = false;
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
                fvMaterialRegList.Visible = true;
                lbAddMatReg.Visible = false;
                gvMaterialRegList.Visible = !gvMaterialRegList.Visible;

                DetailMaterialRegisterList matRegister = new DetailMaterialRegisterList();
                matRegister.RegIDX = int.Parse(cmdArg);
                dataMaster.MaterialRegisterList = new DetailMaterialRegisterList[1];
                dataMaster.MaterialRegisterList[0] = matRegister;
                dataMaster = serviceMaster.ActionDataMaster("materialreg", dataMaster, 20);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster.MaterialRegisterList[0]));
                setFormData(fvMaterialRegList, FormViewMode.Edit, dataMaster.MaterialRegisterList);
                DropDownList ddlMCodeE = (DropDownList)fvMaterialRegList.FindControl("ddlMCodeE");
                ddlMCodeE.SelectedValue = dataMaster.MaterialRegisterList[0].MIDX.ToString();

                Repeater rptImagesE = (Repeater)fvMaterialRegList.FindControl("rptImagesE");
                string dirName = imgPath + dataMaster.MaterialRegisterList[0].MCode + dataMaster.MaterialRegisterList[0].RCode;
                string[] filesindirectory = Directory.GetFiles(Server.MapPath(dirName));
                List<String> images = new List<string>(filesindirectory.Count());
                foreach (string item in filesindirectory)
                {
                    images.Add(String.Format(dirName + "/{0}", Path.GetFileName(item)));
                }
                rptImagesE.DataSource = images;
                rptImagesE.DataBind();
                break;
            case "cmdCancel":
                break;
        }
    }

    protected void gvPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gvName = (GridView)sender;

        gvName.PageIndex = e.NewPageIndex;
        getGridData("materialreg", dataMaster, 20);
        setFormData(fvMaterialRegList, FormViewMode.Insert, null);
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
                DetailMaterialRegisterList matRegister = new DetailMaterialRegisterList();
                matRegister.RegIDX = 0;
                matRegister.MIDX = int.Parse(((DropDownList)fvMaterialRegList.FindControl("ddlMCode")).SelectedValue);
                matRegister.MCode = ((DropDownList)fvMaterialRegList.FindControl("ddlMCode")).SelectedItem.Text;
                matRegister.RCode = ((TextBox)fvMaterialRegList.FindControl("tbRCode")).Text.Trim();
                matRegister.SerialNo = ((TextBox)fvMaterialRegList.FindControl("tbSerialNo")).Text.Trim();
                matRegister.PurDate = ((TextBox)fvMaterialRegList.FindControl("tbPurDate")).Text.Trim();
                matRegister.PurFrom = ((TextBox)fvMaterialRegList.FindControl("tbPurFrom")).Text.Trim();
                matRegister.Quantity = int.TryParse(((TextBox)fvMaterialRegList.FindControl("tbQuantity")).Text.Trim(), out dInt) ? int.Parse(((TextBox)fvMaterialRegList.FindControl("tbQuantity")).Text.Trim()) : dInt;
                matRegister.VoucherNo = ((TextBox)fvMaterialRegList.FindControl("tbVoucherNo")).Text.Trim();
                matRegister.UnitPrice = float.TryParse(((TextBox)fvMaterialRegList.FindControl("tbUnitPrice")).Text.Trim(), out dFloat) ? float.Parse(((TextBox)fvMaterialRegList.FindControl("tbUnitPrice")).Text.Trim()) : dFloat;
                matRegister.Amount = float.TryParse(((TextBox)fvMaterialRegList.FindControl("tbAmount")).Text.Trim(), out dFloat) ? float.Parse(((TextBox)fvMaterialRegList.FindControl("tbAmount")).Text.Trim()) : dFloat;
                matRegister.RegStatus = 1;
                dataMaster.MaterialRegisterList = new DetailMaterialRegisterList[1];
                dataMaster.MaterialRegisterList[0] = matRegister;

                actionType = int.Parse("1" + "1");
                // litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));

                //execute data
                dataMaster = serviceMaster.ActionDataMaster("materialreg", dataMaster, actionType);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));
                //get return code
                localString = dataMaster.ReturnCode;

                //get current data
                if (localString == "0")
                {
                    //directory name
                    string dirName = ((DropDownList)fvMaterialRegList.FindControl("ddlMCode")).SelectedItem.Text + ((TextBox)fvMaterialRegList.FindControl("tbRCode")).Text.Trim();
                    //set directory
                    setDir(imgPath + dirName);
                    //upload files
                    HttpFileCollection hfc = Request.Files;
                    if (hfc.Count > 0)
                    {
                        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath(imgPath + dirName));
                        int ocount = dir.GetFiles().Length;
                        for (int i = ocount; i < (hfc.Count + ocount); i++)
                        {
                            HttpPostedFile hpf = hfc[i-ocount];
                            if (hpf.ContentLength > 0)
                            {
                                string fileName = dirName + "-" + i;
                                string filePath = Server.MapPath(imgPath + dirName);
                                string extension = Path.GetExtension(hpf.FileName);
                                hpf.SaveAs(Path.Combine(filePath, fileName + extension));//filePath + "\\" + fileName + extension);
                            }
                        }
                    }

                    funcWeb.ShowAlert(this, "ดำเนินการเรียบร้อยแล้วค่ะ");
                    //clear form
                    setFormData(fvMaterialRegList, FormViewMode.Insert, null);
                }
                else
                {
                    funcWeb.ShowAlert(this, localString);
                }
                setGridData(gvMaterialRegList, dataMaster.MaterialRegisterList);
                break;
            case "cmdUpdate":
                //set data
                DetailMaterialRegisterList matRegisterE = new DetailMaterialRegisterList();
                matRegisterE.RegIDX = int.Parse(((Label)fvMaterialRegList.FindControl("lblRegIDXE")).Text);
                matRegisterE.MIDX = int.Parse(((DropDownList)fvMaterialRegList.FindControl("ddlMCodeE")).SelectedValue);
                matRegisterE.MCode = ((DropDownList)fvMaterialRegList.FindControl("ddlMCodeE")).SelectedItem.Text;
                matRegisterE.RCode = ((TextBox)fvMaterialRegList.FindControl("tbRCodeE")).Text.Trim();
                matRegisterE.SerialNo = ((TextBox)fvMaterialRegList.FindControl("tbSerialNoE")).Text.Trim();
                matRegisterE.PurDate = ((TextBox)fvMaterialRegList.FindControl("tbPurDateE")).Text.Trim();
                matRegisterE.PurFrom = ((TextBox)fvMaterialRegList.FindControl("tbPurFromE")).Text.Trim();
                matRegisterE.Quantity = int.TryParse(((TextBox)fvMaterialRegList.FindControl("tbQuantityE")).Text.Trim(), out dInt) ? int.Parse(((TextBox)fvMaterialRegList.FindControl("tbQuantityE")).Text.Trim()) : dInt;
                matRegisterE.VoucherNo = ((TextBox)fvMaterialRegList.FindControl("tbVoucherNoE")).Text.Trim();
                matRegisterE.UnitPrice = float.TryParse(((TextBox)fvMaterialRegList.FindControl("tbUnitPriceE")).Text.Trim(), out dFloat) ? float.Parse(((TextBox)fvMaterialRegList.FindControl("tbUnitPriceE")).Text.Trim()) : dFloat;
                matRegisterE.Amount = float.TryParse(((TextBox)fvMaterialRegList.FindControl("tbAmountE")).Text.Trim(), out dFloat) ? float.Parse(((TextBox)fvMaterialRegList.FindControl("tbAmountE")).Text.Trim()) : dFloat;
                matRegisterE.UseStatus = 0;
                matRegisterE.RegStatus = int.Parse(((DropDownList)fvMaterialRegList.FindControl("ddlRegStatusE")).SelectedValue); ;
                dataMaster.MaterialRegisterList = new DetailMaterialRegisterList[1];
                dataMaster.MaterialRegisterList[0] = matRegisterE;

                actionType = int.Parse("1" + ((DropDownList)fvMaterialRegList.FindControl("ddlRegStatusE")).SelectedValue);

                //execute data
                dataMaster = serviceMaster.ActionDataMaster("materialreg", dataMaster, actionType);
                //litTest.Text = HttpUtility.HtmlDecode(funcWeb.ConvertObjectToXml(dataMaster));
                //get return code
                localString = dataMaster.ReturnCode;

                //get current data
                if (localString == "0")
                {
                    //directory name
                    string dirName = ((DropDownList)fvMaterialRegList.FindControl("ddlMCodeE")).SelectedItem.Text + ((TextBox)fvMaterialRegList.FindControl("tbRCodeE")).Text.Trim();
                    //set directory
                    setDir(imgPath + dirName);
                    //upload files
                    HttpFileCollection hfc = Request.Files;
                    if (hfc.Count > 0)
                    {
                        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath(imgPath + dirName));
                        int ocount = dir.GetFiles().Length;
                        for (int i = ocount; i < (hfc.Count + ocount); i++)
                        {
                            HttpPostedFile hpf = hfc[i-ocount];
                            if (hpf.ContentLength > 0)
                            {
                                string fileName = dirName + "-" + i;
                                string filePath = Server.MapPath(imgPath + dirName);
                                string extension = Path.GetExtension(hpf.FileName);
                                hpf.SaveAs(Path.Combine(filePath, fileName + extension));//filePath + "\\" + fileName + extension);
                            }
                        }
                    }

                    funcWeb.ShowAlert(this, "ดำเนินการเรียบร้อยแล้วค่ะ");
                    //clear form
                    setFormData(fvMaterialRegList, FormViewMode.Insert, null);
                }
                else
                {
                    funcWeb.ShowAlert(this, localString);
                }
                setGridData(gvMaterialRegList, dataMaster.MaterialRegisterList);
               break;
            case "cmdReset":
                //clear form
                setFormData(fvMaterialRegList, FormViewMode.Insert, null);
                break;
            case "cmdCancel":
                //clear form
                setFormData(fvMaterialRegList, FormViewMode.Insert, null);
                break;
        }

        fvMaterialRegList.Visible = !fvMaterialRegList.Visible;
        lbAddMatReg.Visible = !lbAddMatReg.Visible;
        gvMaterialRegList.Visible = !gvMaterialRegList.Visible;
    }

    protected void ddlMCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlMCode = (DropDownList)fvMaterialRegList.FindControl("ddlMCode");
        DropDownList ddlMCodeE = (DropDownList)fvMaterialRegList.FindControl("ddlMCodeE");
        TextBox tbAsName = (TextBox)fvMaterialRegList.FindControl("tbAsName");
        TextBox tbMName = (TextBox)fvMaterialRegList.FindControl("tbMName");
        TextBox tbMDesc = (TextBox)fvMaterialRegList.FindControl("tbMDesc");
        TextBox tbKName = (TextBox)fvMaterialRegList.FindControl("tbKName");
        TextBox tbRUD = (TextBox)fvMaterialRegList.FindControl("tbRUD");

        int mIDX = -1;
        if (ddlMCode != null)
        {
            mIDX = int.Parse(ddlMCode.SelectedValue);
        }
        else
        {
            mIDX = int.Parse(ddlMCodeE.SelectedValue);
        }

        if (mIDX > 0)
        {
            DataMaster dLocal = new DataMaster();
            DetailMaterialList matDetail = new DetailMaterialList();
            matDetail.MIDX = mIDX;
            dLocal.MaterialList = new DetailMaterialList[1];
            dLocal.MaterialList[0] = matDetail;

            dLocal = serviceMaster.ActionDataMaster("materiallist", dLocal, 20);
            //get return code
            localString = dLocal.ReturnCode;
            
            //get current data
            if (localString == "0")
            {
                tbAsName.Text = dLocal.MaterialList[0].AsName;
                tbMName.Text = dLocal.MaterialList[0].MName;
                tbMDesc.Text = dLocal.MaterialList[0].MDesc;
                tbKName.Text = dLocal.MaterialList[0].KName;
                tbRUD.Text = dLocal.MaterialList[0].RUD.ToString();
            }
        }
        else //clear data
        {
            tbAsName.Text = String.Empty;
            tbMName.Text = String.Empty;
            tbMDesc.Text = String.Empty;
            tbKName.Text = String.Empty;
            tbRUD.Text = String.Empty;
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
            case "cmdAddMatReg":
                fvMaterialRegList.Visible = true;
                lbAddMatReg.Visible = false;
                gvMaterialRegList.Visible = !gvMaterialRegList.Visible;
                break;
        }
    }
    #endregion btn command

    #region bind data
    protected void getGridData(string dataName, DataMaster dataMaster, int actionType)
    {
        dataMaster = serviceMaster.ActionDataMaster(dataName, dataMaster, actionType);
        setGridData(gvMaterialRegList, dataMaster.MaterialRegisterList);
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
                setDdlMCodeList(fvName, "ddlMCode");
                //read only selected date
                TextBox tbPurDate = (TextBox)fvMaterialRegList.FindControl("tbPurDate");
                tbPurDate.Attributes.Add("readonly", "readonly");
                break;
            case FormViewMode.Edit:
                setDdlMCodeList(fvName, "ddlMCodeE");
                //read only selected date
                TextBox tbPurDateE = (TextBox)fvMaterialRegList.FindControl("tbPurDateE");
                tbPurDateE.Attributes.Add("readonly", "readonly");
                break;
        }
    }

    protected void setDdlMCodeList(FormView fvName, string ddlName)
    {
        DropDownList ddlList = (DropDownList)fvName.FindControl(ddlName);
        DataMaster dLocal = new DataMaster();
        dLocal = serviceMaster.ActionDataMaster("materiallist", dLocal, 21);
        ddlList.DataSource = dLocal.MaterialList;
        ddlList.DataTextField = "MCode";
        ddlList.DataValueField = "MIDX";
        ddlList.DataBind();
        ddlList.Items.Insert(0, new ListItem("-- Mat. Code --", "-1"));
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
}