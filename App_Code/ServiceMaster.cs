using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ServiceMaster
/// </summary>
public class ServiceMaster
{
    #region initial function/data
    FunctionWeb funcWeb = new FunctionWeb();
    FunctionDB funcDb = new FunctionDB();
    DataMaster dataMaster = new DataMaster();
    string localXml = String.Empty;
    #endregion  initial function/data

	public ServiceMaster()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataMaster ActionDataMaster(string dataName, DataMaster dataMaster, int actionType)
    {
        string dataConn = "messisConn";
        string spName = "";
        switch (dataName)
        {
            case "kind":
                spName = "KindAction";
                break;
            case "asset":
                spName = "AssetAction";
                break;
            case "materialtype":
                spName = "MaterialTypeAction";
                break;
            case "unit":
                spName = "UnitAction";
                break;
            case "site":
                spName = "SiteAction";
                break;
            case "materiallist":
                spName = "MaterialListAction";
                break;
            case "materialreg":
                spName = "MaterialRegAction";
                break;
            case "materialrec":
            spName = "MaterialRecAction";
                break;
            case "materialreport":
            spName = "MaterialReport";
                break;
        }

        if (spName != "")
        {
            //convert to xmlIn
            localXml = funcWeb.ConvertObjectToXml(dataMaster);
            //call db function
            localXml = funcDb.execSPXml(dataConn, spName, localXml, actionType);
            //convert to object
            dataMaster = (DataMaster)funcWeb.ConvertXmlToObject(typeof(DataMaster), localXml);
        }

        return dataMaster;
    }
}