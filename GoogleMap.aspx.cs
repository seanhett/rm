using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

/// <summary>
/// GoogleMap Class
/// Created by: Sean Hettinger on 04/27/17
/// When called, the class creates a Google Map window using the following Session State Values:
/// ["Latitude"] - A latitude value
/// ["Longitude"] - A longitude value
/// ["PropertyName"] - The name of the property to be displayed
/// ["GeoData"] - A string of GeoData from SQL formatted using the STAsText() function
/// </summary>

public partial class Mapping_GoogleMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Grabbing stored Lat/Long, property name, or GeoData values 
        string latitude = (string)Session["Latitude"];
        string longitude = (string)Session["Longitude"];
        string geoDataString = (string)Session["GeoData"];
        propertyNameField.Value = (string)Session["PropertyName"];


        //If there is a GeoData value, parses the values into a JSON value that is passed into a hiddenfield client-side
        if(!string.IsNullOrEmpty(geoDataString)){
        
            List<string[]> data = new List<string[]>();
            string[][] newArray = null;
        
            data = parseGeoData(geoDataString);

            //If geoData is more than a point, turn the array into JSON object and pass into the hiddenfield
            if (data != null)
            {
                newArray = data.ToArray();
                string json = new JavaScriptSerializer().Serialize(newArray);
                geoDataField.Value = json;
            }
        }
        
        
        //If there are latitude or longitude values, passes them into hiddenfield
        if (!string.IsNullOrEmpty(latitude) && !string.IsNullOrEmpty(latitude)) 
        {
            latitudeField.Value = latitude;
            longitudeField.Value = longitude;
        }
         
    }


    //Parses the GeoData string from SQL server
    public List<string[]> parseGeoData(string geoDataString) 
    {
        string[] polygonData = geoDataString.Split('(', ')');  
        string geoDataType = polygonData[0];                   //i.e. Polygon, MultiPolygon, MultiPoint, Point, LineString, MultiLineString
        string[] coordinates;                                  //An array to store coordinate values             
        List<string[]> returnData = new List<string[]>();      //List of coordinates to be returned

        //If the geoData value is a point, set the lat/long clientside hidden fields
        if (geoDataType == "POINT ")
        {
            coordinates = polygonData[1].Split(' ');
            latitudeField.Value = coordinates[1].ToString();
            longitudeField.Value = coordinates[0].ToString();
            return null;
        }

        //If the geoData is a polygon, separate points and add to a list of strings to be converted into JSON
        if (geoDataType == "POLYGON ")
        {
            coordinates = polygonData[2].Split(',');
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
        }
        
        if (geoDataType == "MULTIPOLYGON "){}
        if (geoDataType == "MULTIPOINT ") { }
        if (geoDataType == "MULTILINESTRING ") { }
        if (geoDataType == "LINESTRING "){}
        
        return returnData;
    
    }
}