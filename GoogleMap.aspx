<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoogleMap.aspx.cs" Inherits="Mapping_GoogleMap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <style>
      /* Always set the map height explicitly to define the size of the div
       * element that contains the map. */
      #map {
        height: 100%;
      }
      /* Optional: Makes the sample page fill the window. */
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
        <asp:HiddenField ID="geoDataField" runat="server" /> 
    </form>
    <script>
        var map;
        function getParameterByName(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }
        
        var lat = getParameterByName('lat');
        var long = getParameterByName('long');
        var geoData = JSON.parse(document.getElementById('geoDataField').value);

        function parseGeoData(geoData) {
            var parsedData = new Array();
            for (var i = 0; i < geoData.length; i++) {
                    parsedData.push('lat: ' + geoData[i][1] + '');
                    parsedData.push('lng: ' + geoData[i][0] + '');
            }
            return parsedData;
        }
        var parsedData = JSON.stringify(parseGeoData(geoData));


        

        function initMap() {
            var map = new google.maps.Map(document.getElementById('map'), {
                zoom: 17,
                center: new google.maps.LatLng(geoData[0][1], geoData[0][0]),
                mapTypeId: 'terrain'
            });


            var latlon = [];
            for (var i = 0; i < geoData.length; i++) {
                latlon[i] = new google.maps.LatLng(geoData[i][1], geoData[i][0]);
            }

            var bermudaTriangle = new google.maps.Polygon({
                paths: latlon,
                strokeColor: '#FF0000',
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: '#FF0000',
                fillOpacity: 0.35
                });
            bermudaTriangle.setMap(map);
      }
    </script>
    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA6n1HbgrS6oGRNE8f7DDVrYidx1lt8wHw&callback=initMap">
    </script>
  </body>
</html>