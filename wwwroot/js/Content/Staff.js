$(function () {
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
        var url = "/Staff/" + actionUrl;
        var sendData = form.serialize();
        $.post(url, sendData).done(function (data) {
            PlaceHolderElement.find('.modal').modal('hide');
        })
    })
})


function Delete(IdPersonal) {
    Swal.fire({
        title: "\u00BFEst\u00E1s seguro que deseas eliminar el registro?",
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
                url: "/Staff/Delete",
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
}

window.onload = function () {
    if (document.getElementById('Rol')) {
        showDiv(document.getElementById('Rol').value);
    }
}
function showDiv(select) {
    if (select.value == 1) {
        document.getElementById('hidden-grado').style.display = "block";
        document.getElementById('hidden-seccion').style.display = "block";
    } else {
        document.getElementById('hidden-grado').style.display = "none";
        document.getElementById('hidden-seccion').style.display = "none";
    }
} 