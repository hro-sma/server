mapboxgl.accessToken = 'pk.eyJ1IjoiaDQwOTk5NzkiLCJhIjoiY2pueDVybHk1MG56YjNwcnpjcjc2cThhaCJ9.DDkVVzYzBw-1SA2o6hkXug';

var lat = 52;
var long = 4.5;

if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(showPosition);
}

function showPosition(position) {
    lat = position.coords.latitude;
    long = position.coords.longitude;
    marker.setLngLat([long, lat]);
    $('#Longitude').val(long);
    $('#Latitude').val(lat);
}

var map = new mapboxgl.Map({
    container: 'map',
    style: 'mapbox://styles/mapbox/streets-v9',
    center: [5, 52],
    zoom: 7
});

var marker = new mapboxgl.Marker({ draggable: true })
    .setLngLat([long, lat])
    .addTo(map);

function onDragEnd() {
    var lngLat = marker.getLngLat();

    $('#Longitude').val(lngLat.lng);
    $('#Latitude').val(lngLat.lat);
}

marker.on('dragend', onDragEnd);