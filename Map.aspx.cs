using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Weston.EPA;

public partial class Mapping_Map : System.Web.UI.Page
{
    string _id;
    string _pageSize;
    string _page;
    string _sortColumn;
    string _whereClause;
    string _mappingType;

    public string GoogMapScriptSrc
    {
        get;
        set;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString.Count == 7)
        {
            _id = Request.QueryString["id"];
            _pageSize = Request.QueryString["pageSize"];
            _page = Request.QueryString["page"];
            _sortColumn = Request.QueryString["sortColumn"];
            _whereClause = Request.QueryString["whereClause"];
            _mappingType = Request.QueryString["mappingType"];

            thisID.Value = _id;
            pageSize.Value = _pageSize;
            currentPage.Value = _page;
            sortColumn.Value = _sortColumn;
            whereClause.Value = _whereClause;
            mappingType.Value = _mappingType;

            GoogMapScriptSrc =
                ConfigurationManager.AppSettings[ WebHubConstants.GOOGLE_MAP_SSCRIPT_SRC_APPSETTINGS_KEY ];
        }

        else
        {
            _id = Request.QueryString["id"];
            _mappingType = Request.QueryString["mappingType"];
            
            thisID.Value = _id;
            mappingType.Value = _mappingType;
        }
    }
}
