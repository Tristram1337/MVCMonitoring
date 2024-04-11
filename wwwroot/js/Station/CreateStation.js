function validateForm() {
    var floodLevel = document.getElementById('FloodLevel').value;
    var droughtLevel = document.getElementById('DroughtLevel').value;

    if (!isNumeric(floodLevel) || floodLevel < 0 || floodLevel > 100) {
        alert('Please enter a number between 0 and 100 for the flood level.');
        return false;
    }

    if (!isNumeric(droughtLevel) || droughtLevel < 0 || droughtLevel > 100) {
        alert('Please enter a number between 0 and 100 for the drought level.');
        return false;
    }

    return true;
}

function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

function createStation() {
    console.log("Create station function called");

    var form = document.querySelector('form');
    var data = new FormData(form);

    fetch('/Operations/CreateStationAction', {
        method: 'POST',
        body: data
    })
        .then(response => response.json())
        .then(data => {
            console.log("Response from server:", data);
            alert(data.message);
            location.reload();
        })
        .catch(error => {
            console.error("Error:", error);
        });
}
