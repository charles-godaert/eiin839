var stations;

function retrieveAllContracts() {
    //var url = "https://api.jcdecaux.com/vls/v3/contracts?apiKey=" + document.getElementById("apiKey").value; 2dae8548640ca7455930eb98ca0508a102ac8c1a
    var url = "https://api.jcdecaux.com/vls/v3/contracts?apiKey=2dae8548640ca7455930eb98ca0508a102ac8c1a";

    var requestType = "GET";
    var request = new XMLHttpRequest();
    request.open(requestType, url, true);
    request.setRequestHeader("Accept", "application/json");
    request.onload = contractsRetrieved;
    request.send();
}

function contractsRetrieved() {
    var response = JSON.parse(this.responseText);
    response.forEach(contract => addOption(contract.name));
}

function retrieveContractStations() {

    var contract = document.getElementById("contracts-choice").value;
    //var url = "https://api.jcdecaux.com/vls/v3/stations?apiKey=" + document.getElementById("apiKey").value + "&contract=" + contract;
    var url = "https://api.jcdecaux.com/vls/v3/stations?apiKey=2dae8548640ca7455930eb98ca0508a102ac8c1a&contract=" + contract;

    var requestType = "GET";
    var request = new XMLHttpRequest();
    request.open(requestType, url, true);
    request.setRequestHeader("Accept", "application/json");
    request.onload = stationsRetrieved;
    request.send();
}

function stationsRetrieved() {
    var response = JSON.parse(this.responseText);
    stations = response;
    console.log(response);
}

function addOption(content) {
    var newOption = document.createElement("option");
    newOption.setAttribute("value", content);

    var currentDiv = document.getElementById('contracts');
    currentDiv.appendChild(newOption, currentDiv);
}


function retrieveClosestStation() {
    var latitude = document.getElementById("latitude-input").value;
    var longitude = document.getElementById("longitude-input").value;

    var closestDistance = 0;
    var closestStation;
    var stationLat;
    var stationLong;

    stations.forEach(function (station) {

        stationLat = station.position.latitude;
        stationLong = station.position.longitude;

        if (getDistanceFrom2GpsCoordinates(latitude, longitude, stationLat, stationLong) < closestDistance || closestDistance == 0) {
            closestStation = station;
            closestDistance = getDistanceFrom2GpsCoordinates(latitude, longitude, stationLat, stationLong);
        }
    });

    console.log("Closest location of lat " + latitude + " and long " + longitude);
    console.log(closestStation);
}


function getDistanceFrom2GpsCoordinates(lat1, lon1, lat2, lon2) {
    // Radius of the earth in km
    var earthRadius = 6371;
    var dLat = deg2rad(lat2 - lat1);
    var dLon = deg2rad(lon2 - lon1);
    var a =
        Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
        Math.sin(dLon / 2) * Math.sin(dLon / 2)
        ;
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = earthRadius * c; // Distance in km
    return d;
}

function deg2rad(deg) {
    return deg * (Math.PI / 180)
}