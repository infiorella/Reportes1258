window.onload = function () {
    if ($('#ListReports').length) {
        $('.datatables').DataTable(
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

    $('.dataTables_filter input[type="search"]').css(
        { 'width': '400px', 'display': 'inline-block' }
    );

}

function ApproveReport(IdReport) {
    Swal({
        title: 'Agrege un comentario',
        html: '<textarea type="text" id="comentario" name="name" class="swal2-input"></textarea>',
    }).then(function (Comentario) {

        $.ajax({
            type: "POST",
            url: "/Reports/Approve",
            data: { id: IdReport, comentario: Comentario },
            dataType: "json",
            success: function (r) {
                Swal.fire({
                    text: r.message,
                    icon: 'success'
                });
            },
            error: function (jqXHR, exception) {
                var msg = jqXHR.responseText;

                Swal.fire({
                    text: 'Problema ' + msg,
                    icon: 'alert'
                });
            }
        });
    });   
}

function DisapproveReport(IdReport) {
    Swal({
        title: 'Agrege un comentario',
        html: '<textarea type="text" id="comentario" name="name" class="swal2-input"></textarea>',
    }).then(function (Comentario) {
        $.ajax({
            type: "POST",
            url: "/Reports/Disapprove",
            data: { id: IdReport, comentario: Comentario },
            dataType: "json",
            success: function (r) {
                Swal.fire({
                    text: r.message,
                    icon: 'success'
                });
            },
            error: function (jqXHR, exception) {
                var msg = jqXHR.responseText;

                Swal.fire({
                    text: 'Problema ' + msg,
                    icon: 'alert'
                });
            }
        });
    });    
}