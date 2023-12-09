window.onload = function () {
    var area = document.getElementById("Area").value;
    var grado = document.getElementById("Grado").value;
    var seccion = document.getElementById("Seccion").value;
    /******Get Competencias***** */
    GetSkills(grado, seccion, area);

    DetallesNotasGeneral(grado, seccion, area);
    ObtenerTablaAsistencia(grado, seccion, area, "");

};


$('select').on('change', function (e) {
    var competencia = $(this).val();
    var area = document.getElementById("Area").value;
    var grado = document.getElementById("Grado").value;
    var seccion = document.getElementById("Seccion").value;

    DetallesNotas(grado, seccion, area, competencia);

    ObtenerTablaAsistencia(grado, seccion, area, competencia);
});

function DataTableGrades() {
    if ($('#tableGrades').length) {
        var table = $('#tableGrades').DataTable(
            {
                autoWidth: true,
                language: {
                    "decimal": "",
                    "emptyTable": "No hay información",
                    "info": "Mostrando _START_ a _END_ de _TOTAL_ Registros",
                    "infoEmpty": "Mostrando 0 to 0 of 0 Registros",
                    "infoFiltered": "(Filtrado de _MAX_ total registros)",
                    "infoPostFix": "",
                    "thousands": ",",
                    "lengthMenu": "Mostrar _MENU_ Registros",
                    "loadingRecords": "Cargando...",
                    "processing": "Procesando...",
                    "search": "Buscar:",
                    "zeroRecords": "Sin resultados encontrados",
                    "paginate": {
                        "first": "Primero",
                        "last": "Ultimo",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    },
                }, "lengthMenu": [
                    [15, 20, 25, -1],
                    [15, 20, 25, "All"]
                ]
            });
    } else {
    }
}
function GetSkills(grado, seccion, area) {
    select = document.getElementById('selectCompetencias');
    var option = document.createElement("option");
    option.value = "";
    option.text = "----Seleccionar---";
    select.appendChild(option);
    $.ajax({
        url: "/Grades/GetSkills",
        data: { Grado: grado, Seccion: seccion, Area: area },
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            $.each(response, function (i, value) {
                var option = document.createElement("option");
                option.value = value.competencia;
                option.text = value.competencia;
                select.appendChild(option);
            });
        },
        error: function (error) {
            var option = document.createElement("option");
            option.value = "";
            option.text = "----Seleccionar---";
            select.appendChild(option);
        }
    });
}
function DetallesNotas(grado, seccion, area, competencia) {
    $.ajax({
        url: "/Grades/DetailsGrades",
        data: { Grado: grado, Seccion: seccion, Area: area, Skill: competencia },
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            document.getElementById("AlumnosDesaprobados").innerHTML = response.item1;
            document.getElementById("AlumnosAprobados").innerHTML = response.item2;
            document.getElementById("LogroAlcanzado").innerHTML = response.item3;
        },
        error: function (error) {
            document.getElementById("AlumnosDesaprobados").innerHTML = "0";
            document.getElementById("AlumnosAprobados").innerHTML = "0";
            document.getElementById("LogroAlcanzado").innerHTML = "0";
        }
    });
}

function DetallesNotasGeneral(grado, seccion, area) {
    $.ajax({
        url: "/Grades/DetailsGradesGeneral",
        data: { Grado: grado, Seccion: seccion, Area: area},
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            document.getElementById("AlumnosDesaprobados").innerHTML = response.item1;
            document.getElementById("AlumnosAprobados").innerHTML = response.item2;
            document.getElementById("LogroAlcanzado").innerHTML = response.item3;
        },
        error: function (error) {
            document.getElementById("AlumnosDesaprobados").innerHTML = "0";
            document.getElementById("AlumnosAprobados").innerHTML = "0";
            document.getElementById("LogroAlcanzado").innerHTML = "0";
        }
    });
}

function ObtenerTablaAsistencia(grado, seccion, area, competencia) {
    $.ajax({
        url: "/Grades/GetTableGrades",
        data: { Grado: grado, Seccion: seccion, Area: area, Skill: competencia },
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            $("#tableGrades").DataTable().destroy();
            if (response === null || response === undefined) {
                $('#tableGrades tbody').html("<tr><td colspan=12>No hay registros</td></tr>");
            } else {
                var row = '';
                $.each(response, function (i, value) {
                    var rowdetail = '';
                    for (var k in value) {
                        switch (value[k]) {
                            case "AD":
                                rowdetail += '<td class="text-center"><span class="badge badge-pill badge-success">' + value[k] + '</span></td>'
                                break;
                            case "A":
                                rowdetail += '<td class="text-center"><span class="badge badge-pill badge-primary">' + value[k] + '</span></td>'
                                break;
                            case "B":
                                rowdetail += '<td class="text-center"><span class="badge badge-pill badge-secondary">' + value[k] + '</span></td>'
                                break;
                            case "C":
                                break;
                                rowdetail += '<td class="text-center"><span class="badge badge-pill badge-warning">' + value[k] + '</span></td>'
                            default:
                                rowdetail += '<td>' + value[k] + '</td>'
                                break;
                        }

                    }
                    row += '<tr>' + rowdetail + '</tr>';
                    $('#tableGrades tbody').html(row);

                }
                );
                DataTableGrades();
            }
           
        },
        error: function (error) {
            $('#tableGrades tbody').html("<tr><td>Error en el sistema, vuelva a intentar en unos momentos</td></tr>");
        }
    });
}