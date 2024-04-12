function deleteMeasurement() {

    var confirmation = confirm("Are you sure you want to delete this measurement?");

    if (confirmation) {
        var form = document.getElementById('form');
        var data = new FormData(form);

        fetch('/Operations/DeleteMeasurementAction', {
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