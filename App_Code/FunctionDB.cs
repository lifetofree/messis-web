using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

/// <summary>
/// Summary description for FunctionDB
/// </summary>
public class FunctionDB
{
    #region initial function/data
    FunctionWeb funcWeb = new FunctionWeb();
    #endregion  initial function/data

	public FunctionDB()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public SqlConnection strDBConn(string connName)
    {
        string strConn = WebConfigurationManager.ConnectionStrings[connName].ConnectionString; ;
        SqlConnection retDBConn = new SqlConnection(strConn);

        return retDBConn;
    }

    public string execSPXml(string connName, string spName, string xmlIn, int actionType)
    {
        var retXml = "";

        SqlConnection conn = strDBConn(connName);
        SqlCommand cmd = new SqlCommand(spName, conn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@XmlIn", SqlDbType.Xml).Value = xmlIn;
        cmd.Parameters.Add("@ActionType", SqlDbType.Int).Value = actionType;
        cmd.Parameters.Add("@XmlOut", SqlDbType.Xml).Direction = ParameterDirection.Output;

        try
        {
            conn.Open();
            cmd.ExecuteNonQuery();
            retXml = cmd.Parameters["@XmlOut"].Value.ToString();
        }
        catch (Exception ex)
        {
            retXml = ex.Message;
        }
        finally
        {
            conn.Close();
        }

        return retXml.ToString();
    }
}