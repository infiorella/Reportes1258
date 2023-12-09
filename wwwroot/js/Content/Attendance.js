
$('#meses').change(function () {
    var mes = $(this).val();
    var año = new Date().getFullYear();
    var grado = document.getElementById("Grado").value;
    var seccion = document.getElementById("Seccion").value;
    if (!mes) {
        mes = new Date().getMonth();
    }

    DetallesAsistencia(grado, seccion, mes, año);

    ObtenerDias(año, mes);

    ObtenerTablaAsistencia(año, mes, grado, seccion);
});

function DataTableAttendance() {
    if ($('#tableAttendance').length) {
        var table = $('#tableAttendance').DataTable(
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
                ],
                "lengthChange": false,
                "columnDefs": [
                    { "visible": false, "targets": 2 }
                ]
            });
    } else {
    }
}

window.onload = function () {
    var mes = new Date().getMonth();
    var año = new Date().getFullYear();
    var grado = document.getElementById("Grado").value;
    var seccion = document.getElementById("Seccion").value;

    DetallesAsistencia(grado, seccion, mes, año);

    ObtenerDias(año, mes);

    ObtenerTablaAsistencia(año, mes, grado, seccion);
};



function DetallesAsistencia(grado, seccion, mes, año) {
    $.ajax({
        url: "/Attendance/DetallesAsistencia",
        data: { Grado: grado, Seccion: seccion, Month: mes, Year: año },
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            document.getElementById("FaltasNoJustificadas").innerHTML = response.item1;
            document.getElementById("FaltasJustificadas").innerHTML = response.item2;
            document.getElementById("TardanzasNoJustificadas").innerHTML = response.item3;
            document.getElementById("TardanzasJustificadas").innerHTML = response.item4;
        },
        error: function (error) {
            document.getElementById("FaltasNoJustificadas").innerHTML = "0";
            document.getElementById("FaltasJustificadas").innerHTML = "0";
            document.getElementById("TardanzasNoJustificadas").innerHTML = "0";
            document.getElementById("TardanzasJustificadas").innerHTML = "0";
        }
    });
}

function ObtenerDias(año, mes) {
    $.ajax({
        url: "/Attendance/ObtenerDias",
        data: { year: año, month: mes },
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            var row = '';
            var titulo = "<th>Alumno</th>";
            row = row + titulo;
            $.each(response, function (i, value) {
                row += '<th> Día ' + value + '</th>';
            }
            );
            $('#tableAttendance thead tr').html(row);
        },
        error: function (error) {
            $('#tableAttendance thead tr').html("<tr><td colspan=20>Error en el sistema, vuelva a intentar en unos momentos</td></tr>");
        }
    });
}

function ObtenerTablaAsistencia(año, mes, grado, seccion) {
    $.ajax({
        url: "/Attendance/ObtenerAsistenciaScript",
        data: { year: año, month: mes, Grado: grado, Seccion: seccion },
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response === null || response === undefined) {
                $('#tableAttendance tbody').html("<tr><td colspan=12>No hay registros</td></tr>");
            } else {

                var row = '';

                $.each(response, function (i, value) {
                    var rowdetail = '';
                    for (var k in value) {
                        rowdetail += '<td>' + value[k] + '</td>'
                    }
                    row += '<tr>' + rowdetail + '</tr>';
                    $('#tableAttendance tbody').html(row);
                }
                );

                DataTableAttendance();
            }
        },
        error: function (error) {
            $('#tableAttendance tbody').html("<tr><td colspan=20>Error en el sistema, vuelva a intentar en unos momentos</td></tr>");
        }
    });
}