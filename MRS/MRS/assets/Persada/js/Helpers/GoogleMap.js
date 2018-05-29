///* private variables */
//var geocoder;
//var map;
//var infowindow = new google.maps.InfoWindow();

///* function to initialize the map */
//function initialize() {
//    geocoder = new google.maps.Geocoder();
//    var latlng = new google.maps.LatLng(-34.397, 150.644);
//    var mapOptions = {
//        zoom: 10,
//        center: latlng,
//        mapTypeId: google.maps.MapTypeId.ROADMAP
//    }
//    map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);
//}

///* Geocoding based on address */
//function codeAddress(address, title, imageURL) {
//    geocoder.geocode({ 'address': address }, function (results, status) {
//        if (status == google.maps.GeocoderStatus.OK) {
//            map.setCenter(results[0].geometry.location);
//            var marker = new google.maps.Marker({ map: map, position: results[0].geometry.location, icon: imageURL, title: title });

//            /* Set onclick popup */
//            var infowindow = new google.maps.InfoWindow({ content: title });
//            google.maps.event.addListener(marker, 'click', function () { infowindow.open(marker.get('map'), marker); });
//        } else {
//            alert('Geocode was not successful for the following reason: ' + status);
//        }
//    });
//}

///* Geocoding based on latitude and longitude */
//function codeLatLng(latlng, title, imageURL) {
//    var latlngStr = latlng.split(',', 2);
//    var lat = parseFloat(latlngStr[0]);
//    var lng = parseFloat(latlngStr[1]);
//    var latlng = new google.maps.LatLng(lat, lng);
//    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
//        if (status == google.maps.GeocoderStatus.OK) {
//            if (results[1]) {
//                map.setZoom(11);
//                marker = new google.maps.Marker({ position: latlng, map: map, icon: imageURL, title: title, content: title });

//                /* Set onclick popup */
//                var infowindow = new google.maps.InfoWindow({ content: title });
//                google.maps.event.addListener(marker, 'click', function () { infowindow.open(marker.get('map'), marker); });

//            } else {
//                alert('No results found');
//            }
//        } else {
//            alert('Geocoder failed due to: ' + status);
//        }
//    });
//}

//var iconPath = "";
//initialize();

///* Geocoding based on address */
//codeAddress('Sydney NSW', 'Sydney NSW', iconPath + "icon1.png");
//codeAddress('Homebush NSW', 'Homebush NSW', iconPath + "icon2.png");
//codeAddress('Randwick NSW', 'Randwick NSW', iconPath + "icon3.png");
//codeAddress('Paramatta NSW', 'Paramatta NSW', iconPath + "icon4.png");
//codeAddress('Belavista NSW', 'Belavista NSW', iconPath + "icon5.png");

///* Geocoding based on latitude and longitude */
//codeLatLng('-33.971750, 150.894251', 'Glenfield NSW', iconPath + "icon2.png");
//codeLatLng('-33.775820, 151.119428', 'Macquarie Park NSW', iconPath + "icon3.png");
//codeLatLng('-33.830862, 151.087534', 'Rhodes NSW', iconPath + "icon4.png");
//codeLatLng('-33.951013, 151.082372', 'Beverly Hills NSW', iconPath + "icon5.png");



function initialize(map, lat, lng, address) {
    var latlng = new google.maps.LatLng(lat, lng);
    var map1 = new google.maps.Map(document.getElementById(map), {
        center: latlng,
        zoom: 13
    });
    var marker = new google.maps.Marker({
        map: map,
        position: latlng,
        draggable: false,
        anchorPoint: new google.maps.Point(0, -29)
    });
    var infowindow = new google.maps.InfoWindow();
    google.maps.event.addListener(marker, 'click', function () {
        var iwContent = '<div id="iw_container"><div class="iw_title"><b>Location</b> : ' + address + '</div></div>';
        // including content to the infowindow
        infowindow.setContent(iwContent);
        // opening the infowindow in the current map and at the current marker location

        infowindow.open(map1, marker);
    });
    
}

function CallMap(map, lat, lng, address) {
    google.maps.event.addDomListener(window, 'load', initialize(map, lat, lng, address));
}

function GetMap(addressObj) {
    var urlMap = "https://www.google.com/maps/embed/v1/place?key=AIzaSyC0RRMJrUWd9zwELK6LMfJmrU9ZyIOsnq8&q=" + addressObj;
    return urlMap;

}