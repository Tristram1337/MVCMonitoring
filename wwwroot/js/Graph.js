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
            var labels = data.map(function (measurement) {
                return new Date(measurement.dateTime);
            });

            var waterLevels = data.map(function (measurement) {
                return measurement.waterLevel;
            });

            var floodCount = data.filter(function (measurement) {
                return measurement.waterLevel > measurement.floodLevel;
            }).length;

            var droughtCount = data.filter(function (measurement) {
                return measurement.waterLevel < measurement.droughtLevel;
            }).length;
            var normalCount = data.length - floodCount - droughtCount;

            $('#myChart, #my2ndChart').remove();

            $('#graph-container').append('<canvas id="myChart" class="graph-canvas"></canvas>');
            $('#graph-container').append('<canvas id="my2ndChart" class="graph-canvas"></canvas>');

            createChart('myChart', labels, waterLevels, 'Water Level', '#2b83ba');
            createPieChart('my2ndChart', ['Flood', 'Drought', 'Normal'], [floodCount, droughtCount, normalCount], ['#d7191c', '#fdae61', '#2b83ba']);

        });
    });
});

function createChart(canvasId, labels, data, label, color) {
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
                xAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: 'Index'
                    }
                }],
                yAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: label
                    }
                }]
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