mapboxgl.accessToken = 'pk.eyJ1IjoiaDQwOTk5NzkiLCJhIjoiY2pueDVybHk1MG56YjNwcnpjcjc2cThhaCJ9.DDkVVzYzBw-1SA2o6hkXug';

var map = new mapboxgl.Map({
    container: 'map',
    style: 'mapbox://styles/mapbox/streets-v9',
    center: [5, 52],
    zoom: 7
});

var marker = new mapboxgl.Marker({
        draggable: true
    })
    .setLngLat([4.5, 52])
    .addTo(map);

function onDragEnd() {
    var lngLat = marker.getLngLat();


    $('#Longitude').val(lngLat.lng);
    $('#Latitude').val(lngLat.lat);

}

marker.on('dragend', onDragEnd);