function createMeasurement() {
    
    var confirmation = confirm("Are you sure you want to create this measurement?");

    if (confirmation) {
        var form = document.querySelector('form');
        var data = new FormData(form);

        fetch('/Operations/CreateMeasurementAction', {
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
    } else {
        return;
    }
}