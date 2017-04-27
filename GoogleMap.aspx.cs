using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;


public partial class Mapping_GoogleMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<string[]> data = new List<string[]>();
        string geoDataString = null;
        string latitude = null;
        string longitude = null;
        string[][] newArray = null;
        try
        {
            geoDataString = Request.QueryString["geoData"].ToString();
            data = parseGeoData(geoDataString);
            newArray = data.ToArray();
            string json = new JavaScriptSerializer().Serialize(newArray);
            geoDataField.Value = json;

        }
        catch { }
        
        
 
        
    }
    public List<string[]> parseGeoData(string geoDataString) 
    {
        string[] polygonData = geoDataString.Split('(', ')');
        string geoDataType = polygonData[0];
        string[] coordinates = polygonData[2].Split(',');
        List<string[]> returnData = new List<string[]>();
        for (int i = 0; i < coordinates.Length; i++)
        {
            if (coordinates[i].Split(' ').Length > 2)
            {
                string[] myArray = coordinates[i].Split(' ');
                myArray = myArray.Where(w => w != myArray[0]).ToArray();
                returnData.Add(myArray);
            }
            else
                returnData.Add(coordinates[i].Split(' '));
        }


        if (geoDataType == "POLYGON") { }
        if(geoDataType == "MULTIPOLYGON"){}
        if(geoDataType == "POINT"){}
        if (geoDataType == "MULTIPOINT") { }
        if (geoDataType == "MULTILINESTRING") { }
        if(geoDataType == "LINESTRING"){}
        if(geoDataType == "CIRCULARSTRING"){}
        if(geoDataType == "COMPOUNDCURVE"){}
        if(geoDataType == "CURVEPOLYGON"){}
        if(geoDataType == "GEOMETRYCOLLECTION"){}
        
        return returnData;
    
    }
}