$(function () {
    $('.station-button').on("click", function () {
        $('.station-button').removeClass('active');
        $(this).addClass('active');
    });
});

$(function () {
    $(".station-button").on("click", function () {
        var stationId = $(this).data("station-id");
        $.get("/StationGraph/GetGraphData?stationId=" + stationId, function (data) {
            if (data.length === 0) {
                alert('This station currently has no available data.');
                return;
            }

            var labels = data.map(function (measurement) {
                return new Date(measurement.dateTime);
            });

            var waterLevels = data.map(function (measurement) {
                return measurement.waterLevel;
            });

            var floodLevel = data[0].floodLevel;
            var droughtLevel = data[0].droughtLevel;

            var floodCount = data.filter(function (measurement) {
                return measurement.waterLevel > floodLevel;
            }).length;

            var droughtCount = data.filter(function (measurement) {
                return measurement.waterLevel < droughtLevel;
            }).length;
            var normalCount = data.length - floodCount - droughtCount;

            $('#myChart, #my2ndChart').remove();

            $('#graph-container').append('<canvas id="myChart" class="graph-canvas"></canvas>');
            $('#graph-container').append('<canvas id="my2ndChart" class="graph-canvas"></canvas>');

            createChart('myChart', labels, waterLevels, 'Water Level', '#2b83ba', floodLevel, droughtLevel);
            createPieChart('my2ndChart', ['Flood', 'Drought', 'Normal'], [floodCount, droughtCount, normalCount], ['#d7191c', '#fdae61', '#2b83ba']);
        });
    });
});

function createChart(canvasId, labels, data, label, color, floodLevel, droughtLevel) {
    var ctx = document.getElementById(canvasId).getContext('2d');
    var indexLabels = labels.map(function (value, index) {
        return index;
    });

    new Chart(ctx, {
        type: 'line',
        data: {
            labels: indexLabels,
            datasets: [{
                label: label,
                data: data,
                borderColor: color,
                fill: false
            },
            {
                label: 'Flood Level',
                data: floodLevel ? Array(data.length).fill(floodLevel) : [],
                borderColor: '#d7191c',
                borderWidth: 1,
                borderDash: [5, 5],
                fill: false,
                pointRadius: 1,
                pointHoverRadius: 3,
                showLine: true
            },
            {
                label: 'Drought Level',
                data: droughtLevel ? Array(data.length).fill(droughtLevel) : [],
                borderColor: '#FF5733',
                borderWidth: 1,
                borderDash: [5, 5],
                fill: false,
                pointRadius: 1,
                pointHoverRadius: 3,
                showLine: true
            }]
        },
        options: {
            responsive: true,
            tooltips: {
                mode: 'index',
                intersect: false,
            },
            hover: {
                mode: 'nearest',
                intersect: true
            },
            scales: {
                x: {
                    display: true,
                    title: {
                        display: true,
                        text: 'Index'
                    }
                },
                y: {
                    display: true,
                    title: {
                        display: true,
                        text: label
                    },
                    min: 0,
                    max: 100,
                    ticks: {
                        stepSize: 10
                    }
                }
            }
        }
    });
}

function createPieChart(canvasId, labels, data, colors) {
    var ctx = document.getElementById(canvasId).getContext('2d');
    new Chart(ctx, {
        type: 'pie',
        data: {
            labels: labels,
            datasets: [{
                data: data,
                backgroundColor: colors,
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            title: {
                display: true,
                text: 'Percentage of Measurements in Each Category'
            }
        }
    });
}