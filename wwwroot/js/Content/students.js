window.addEventListener('load', function () {
    if (document.getElementById("StudentId")) {
        var Id = document.getElementById("StudentId").value;

        $.ajax({
            type: "POST",
            url: "/Students/GetChart",
            data: { id: Id },
            dataType: "json",
            success: function (data) {
                document.getElementById("LogroNoAlcanzado").innerHTML = data.item1;
                document.getElementById("LogroAlcanzado").innerHTML = data.item2;
                document.getElementById("PorcentajeLogroAlcanzado").innerHTML = data.item3;

            }, error: function (r) {

                alert(r.responseText);
            }
        });
    }
   
});


$(document).ready(function () {
    $('.dataTables_filter input[type="search"]').css(
        { 'width': '450px', 'display': 'inline-block' }
    );
    if ($('#DataTableStudents').length) {
        var table = $('.datatables').DataTable(
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
                    }
                }, "lengthMenu": [
                    [15, 20, 25, -1],
                    [15, 20, 25, "All"]
                ],
                "lengthChange": false
            });
    }
});