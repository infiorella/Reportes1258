/* CHART */
//InformesRecibidosPrincipal

$(document).ready(function () {


    $.ajax({
        type: "POST",
        url: "/Home/ChartClasificacion",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (chData) {
            var aData = chData;
            var aLabels = aData[0];
            var aDatasets1 = aData[1];
            var aDatasets2 = aData[2];

            //Reemplazar  los valores de texto

            var options = {
                series: [{
                    name: "Actas",
                    data: aDatasets1
                }, {
                    name: "Informes",
                    data: aDatasets2
                }],
                chart: {
                    type: "bar",
                    height: 350,
                    stacked: !1,
                    columnWidth: "70%",
                    zoom: {
                        enabled: !0
                    },
                    toolbar: {
                        show: !1
                    },
                    background: "transparent"
                },
                dataLabels: {
                    enabled: !1
                },
                theme: {
                    mode: colors.chartTheme
                },
                responsive: [{
                    breakpoint: 480,
                    options: {
                        legend: {
                            position: "bottom",
                            offsetX: -10,
                            offsetY: 0
                        }
                    }
                }],
                plotOptions: {
                    bar: {
                        horizontal: !1,
                        columnWidth: "40%",
                        radius: 30,
                        enableShades: !1,
                        endingShape: "rounded"
                    }
                },
                xaxis: {
                    type: "Text",
                    categories: aLabels,
                    labels: {
                        show: !0,
                        trim: !0,
                        minHeight: void 0,
                        maxHeight: 120,
                        style: {
                            colors: colors.mutedColor,
                            cssClass: "text-muted",
                            fontFamily: base.defaultFontFamily
                        }
                    },
                    axisBorder: {
                        show: !1
                    }
                },
                yaxis: {
                    labels: {
                        show: !0,
                        trim: !1,
                        offsetX: -10,
                        minHeight: void 0,
                        maxHeight: 120,
                        style: {
                            colors: colors.mutedColor,
                            cssClass: "text-muted",
                            fontFamily: base.defaultFontFamily
                        }
                    }
                },
                legend: {
                    position: "top",
                    fontFamily: base.defaultFontFamily,
                    fontWeight: 400,
                    labels: {
                        colors: colors.mutedColor,
                        useSeriesColors: !1
                    },
                    markers: {
                        width: 10,
                        height: 10,
                        strokeWidth: 0,
                        strokeColor: "#fff",
                        fillColors: [extend.primaryColor, extend.primaryColorLighter],
                        radius: 6,
                        customHTML: void 0,
                        onClick: void 0,
                        offsetX: 0,
                        offsetY: 0
                    },
                    itemMargin: {
                        horizontal: 10,
                        vertical: 0
                    },
                    onItemClick: {
                        toggleDataSeries: !0
                    },
                    onItemHover: {
                        highlightDataSeries: !0
                    }
                },
                fill: {
                    opacity: 1,
                    colors: [base.primaryColor, extend.primaryColorLighter]
                },
                grid: {
                    show: !0,
                    borderColor: colors.borderColor,
                    strokeDashArray: 0,
                    position: "back",
                    xaxis: {
                        lines: {
                            show: !1
                        }
                    },
                    yaxis: {
                        lines: {
                            show: !0
                        }
                    },
                    row: {
                        colors: void 0,
                        opacity: .5
                    },
                    column: {
                        colors: void 0,
                        opacity: .5
                    },
                    padding: {
                        top: 0,
                        right: 0,
                        bottom: 0,
                        left: 0
                    }
                }
            }
            var chart = new ApexCharts(document.querySelector("#ClasificacionInformesRecibidos"), options);
            chart.render();

            var sumaActas = 0;
            var sumaInformes = 0;
            for (var key in aDatasets1) {
                sumaActas += aDatasets1[key];
            };

            for (var key in aDatasets2) {
                sumaInformes += aDatasets2[key];
            };

            document.getElementById('cantActas').innerHTML = sumaActas;
            document.getElementById('cantInformes').innerHTML = sumaInformes;
        }, error: function (r) {

            alert(r.responseText);
        }
    });

    $.ajax({
        type: "POST",
        url: "/Home/ChartTipo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (chData) {
            var data = Object.values(chData);
            /*Segundo chart */
            var options = {
                series: data,
                chart: {
                    width: 380,
                    type: 'pie',
                },
                labels: ['Informe Asistencia', 'Informe Comportamiento', 'Informe Compromiso', 'Informe Notas', 'Acta Sesión Extraordinaria', 'Acta Sesión Ordinaria'],
                responsive: [{
                    breakpoint: 480,
                    options: {
                        chart: {
                            width: 200
                        },
                        legend: {
                            position: 'bottom'
                        }
                    }
                }]
            };

            var chart = new ApexCharts(document.querySelector("#TiposInformesGenerados"), options);
            chart.render();


        }, error: function (r) {

            alert(r.responseText);
        }
    });
});


//var options = {
//    series: [{
//        name: 'Actas',
//        data: [44, 55, 57, 56, 61, 58, 63, 60, 66]
//    }, {
//        name: 'Informes',
//        data: [76, 85, 101, 98, 87, 105, 91, 114, 94]
//    }],
//    chart: {
//        type: 'bar',
//        height: 350
//    },
//    plotOptions: {
//        bar: {
//            horizontal: false,
//            columnWidth: '55%',
//            endingShape: 'rounded'
//        },
//    },
//    dataLabels: {
//        enabled: false
//    },
//    stroke: {
//        show: true,
//        width: 2,
//        colors: ['transparent']
//    },
//    xaxis: {
//        categories: ['Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct'],
//    },
//    yaxis: {
//        title: {
//            text: 'Clasificación de informes'
//        }
//    },
//    fill: {
//        opacity: 1
//    },
//    tooltip: {
//        y: {
//            formatter: function (val) {
//                return val
//            }
//        }
//    }
//};

//var chart = new ApexCharts(document.querySelector("#ClasificacionInformesRecibidos"), options);
//chart.render();



///*Segundo chart */
//var options = {
//    series: [44, 55, 13, 43, 22],
//    chart: {
//        width: 380,
//        type: 'pie',
//    },
//    labels: ['Informe Asistencia', 'Informe Comportamiento', 'Informe Compromiso', 'Informe Notas', 'Acta Sesión Extraordinaria', 'Acta Sesión Ordinaria'],
//    responsive: [{
//        breakpoint: 480,
//        options: {
//            chart: {
//                width: 200
//            },
//            legend: {
//                position: 'bottom'
//            }
//        }
//    }]
//};

//var chart = new ApexCharts(document.querySelector("#TiposInformesGenerados"), options);
//chart.render();