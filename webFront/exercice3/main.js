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