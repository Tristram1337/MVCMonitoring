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

setInterval(function () {
    $.get('/api/get-all-measurements', function (data) {
        var stations = data;

        stations.forEach(function (station) {
            var lastMeasurement = station.Measurements[station.Measurements.length - 1];
            var timeoutExceeded = (Date.now() - new Date(lastMeasurement.DateTime)) / (1000 * 60) > station.TimeOutInMinutes;
            var timeoutSpan = document.getElementById('timeout-' + station.Id);
            if (timeoutExceeded) {
                timeoutSpan.style.color = 'red';
            } else {
                timeoutSpan.style.color = '';
            }
        });
    });
}, 60000);