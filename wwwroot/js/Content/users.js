

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
        "drawCallback": function (settings) {
            Modal();
        },
        "lengthChange": false
    });



$(document).ready(function () {
    $('.dataTables_filter input[type="search"]').css(
        { 'width': '450px', 'display': 'inline-block' }
    );
});


function Modal() {
    var PlaceHolderElement = $('#PlaceHolderHere');
    $('[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        var decodeUrl = decodeURIComponent(url);
        $.get(decodeUrl).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })
    PlaceHolderElement.on('click', '[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var url = "/Users/" + actionUrl;
        var sendData = form.serialize();
        $.post(url, sendData).done(function (data) {
            PlaceHolderElement.find('.modal').modal('hide');
        })
    });
};


$('#dataTable').on('click', '#deleteUsers', function (event) {
    var runid = $(this).data('runid');
    Swal.fire({
        title: "\u00BFEst\u00E1s seguro que deseas eliminar el usuario?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Aceptar',
        cancelButtonText: 'Cancelar',
    }).then((result) => {
        if (result.isConfirmed) {
            // Make Ajax call to ActionResult.
            $.ajax({
                type: "POST",
                url: "/Users/Delete",
                data: { id: runid },
                dataType: "json",
                success: function (r) {
                    Swal.fire({
                        text: r.message,
                        icon: 'success'
                    });
                },
                error: function (jqXHR, exception) {
                    var msg = 'Uncaught Error.\n' + jqXHR.responseText;

                    Swal.fire({
                        text: 'Problema ' + msg,
                        icon: 'alert'
                    });
                }
            });
        }
        else {
            Swal.fire({
                text: 'Eliminaci\u00F3n Cancelada',
                icon: 'info'
            });
        }
        return false;
    })
});