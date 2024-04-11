function deleteStation() {
    var form = document.getElementById('form');
    var data = new FormData(form);

    fetch('/Operations/DeleteStationAction', {
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