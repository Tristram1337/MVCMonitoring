function deleteStation() {
    var form = document.getElementById('deleteForm');
    var data = new FormData(form);

    fetch('/Operations/DeleteStationAction', {
        method: 'POST',
        body: data
    })
        .then(response => response.json())
        .then(data => {
            alert(data.message);
            location.reload();
        });
}