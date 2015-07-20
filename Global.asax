<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        RegisterRoutes(RouteTable.Routes);
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    void RegisterRoutes(RouteCollection routes)
    {
        //routes.MapPageRoute("xx", "xx", "~/xx/xx.aspx");
        //routes.MapPageRoute("xx", "Notice/{idx}/{type}", "~/xx/xx/xx.aspx", true, new RouteValueDictionary { { "idx", "0" }, { "type", "xxx" } });
        //routes.MapPageRoute("Default", "", "~/WebSystem/Login.aspx");
        routes.MapPageRoute("Default", "", "~/WebSystem/Dashboard.aspx");
        routes.MapPageRoute("Dashboard", "Dashboard", "~/WebSystem/Dashboard.aspx");

        routes.MapPageRoute("MaterialRecord", "MaterialRecord/{recType}", "~/WebSystem/Material/MaterialRecord.aspx", true, new RouteValueDictionary { { "recType", "In" } });
        routes.MapPageRoute("MaterialList", "MaterialList", "~/WebSystem/Material/MaterialList.aspx");
        routes.MapPageRoute("MaterialRegister", "MaterialRegister", "~/WebSystem/Material/MaterialRegister.aspx");
        routes.MapPageRoute("MaterialType", "Material/{matType}", "~/WebSystem/Material/MaterialType.aspx", true, new RouteValueDictionary { { "matType", "Kind" } });
        routes.MapPageRoute("MaterialTypeList", "MaterialTypeList", "~/WebSystem/Material/MaterialTypeList.aspx");
        routes.MapPageRoute("MaterialUnit", "MaterialUnit", "~/WebSystem/Material/MaterialUnit.aspx");
        routes.MapPageRoute("MaterialReport", "MaterialReport", "~/WebSystem/Material/MaterialReport.aspx");

        routes.MapPageRoute("SiteName", "SiteName", "~/WebSystem/Site/SiteName.aspx");
    }  
</script>
