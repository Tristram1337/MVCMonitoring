$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})
function toggleMeasurements(stationId) {
    var measurementsDiv = document.getElementById('measurements-' + stationId);
    if (measurementsDiv.style.display === 'none' || measurementsDiv.style.display === '') {
        measurementsDiv.style.display = 'block';
    } else {
        measurementsDiv.style.display = 'none';
    }
}