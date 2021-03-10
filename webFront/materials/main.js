function retrieveAllContracts() {
    var url = "https://api.jcdecaux.com/vls/v3/contracts?apiKey=" + document.getElementById("apiKey").value;
    var requestType = "GET";
    var request = new XMLHttpRequest();
    request.open(requestType, url, true);
    request.setRequestHeader("Accept", "application/json");
    request.onload = contractsRetrieved;
    request.send();
}

function contractsRetrieved() {
    // Let's parse the response:
    var response = JSON.parse(this.responseText);
    console.log(response);
}