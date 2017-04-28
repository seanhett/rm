<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoogleMap.aspx.cs" Inherits="Mapping_GoogleMap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <style>
       #map {
        height: 100%;
      }
      html, body {
        height: 100%;
        margin: 0;
        padding: 0;
      }
    </style>
  </head>
  <body>
    <div id="map"></div>
    <form runat="server">
        <!-- Fields used to collect data from serverside -->
        <asp:HiddenField ID="geoDataField" runat="server" /> 
        <asp:HiddenField ID="latitudeField" runat="server" /> 
        <asp:HiddenField ID="longitudeField" runat="server" />
        <asp:HiddenField ID="propertyNameField" runat="server" />
    </form>
    <script>

        //Puts takes geoData object and puts into an properly formatted array of lat/long coordinates to be used by Google Maps
        function parseGeoData(geoData) {
            var parsedData = new Array();
            for (var i = 0; i < geoData.length; i++) {
                parsedData.push('lat: ' + geoData[i][1] + '');
                parsedData.push('lng: ' + geoData[i][0] + '');
            }
            return parsedData;
        }

        //Set lat/long/propertyname values
        var lat = document.getElementById('latitudeField').value;
        var long = document.getElementById('longitudeField').value;
        var propertyName = document.getElementById('propertyNameField').value;
        

        //Try to create JSON data of geoData if it exists
        try {
            var geoData = JSON.parse(document.getElementById('geoDataField').value);
            var parsedData = JSON.stringify(parseGeoData(geoData));
        }
        catch (err) {
            console.log('geoData not parsed: ' + err);
        }
        
        //Google Maps API init function
        function initMap() {


            //If geoData exists, create a map with a polygon object 
            if (geoData != undefined) {

                //Map definition
                var map = new google.maps.Map(document.getElementById('map'), {
                    zoom: 17,
                    center: new google.maps.LatLng(geoData[0][1], geoData[0][0]),
                    mapTypeId: 'terrain'
                });

                //Create polygon path and boundary
                var latlon = [];
                var bounds = new google.maps.LatLngBounds();
                for (var i = 0; i < geoData.length; i++) {
                    latlon[i] = new google.maps.LatLng(geoData[i][1], geoData[i][0]);
                    bounds.extend(latlon[i]);
                }
                
                //Create polygon object
                var polygon = new google.maps.Polygon({
                    paths: latlon,
                    strokeColor: '#FF0000',
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: '#FF0000',
                    fillOpacity: 0.35,
                    label: propertyName
                });


                var polygonCenter = bounds.getCenter();
                
                //Create marker to put in center of the polygon
                var marker = new google.maps.Marker({
                    position: polygonCenter,
                    map: map,
                    title: propertyName,            
                    });
                
                //Set map within polygon boundary
                map.fitBounds(bounds);

                //Place polygon on map
                polygon.setMap(map);
            }

            //If lat/long values are passed instead of geoData, create map with point
            else if(lat != undefined && long != undefined) {
                
                //Lat/Long object
                var latLng = new google.maps.LatLng(lat, long);
                
                //Map definition    
                var map = new google.maps.Map(document.getElementById('map'), {
                    zoom: 13,
                    center: latLng,
                    mapTypeId: 'terrain'
                });

                //Marker definition
                var marker = new google.maps.Marker({
                    position: latLng,
                    map: map,
                    title: propertyName,            
                    });
            }

                
            
      }
    </script>
    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA6n1HbgrS6oGRNE8f7DDVrYidx1lt8wHw&callback=initMap">        //Sean Hettinger API Key: AIzaSyA6n1HbgrS6oGRNE8f7DDVrYidx1lt8wHw
    </script>
  </body>
</html>