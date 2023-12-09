$('.select2').select2(
    {
        theme: 'bootstrap4',
    });
$('.select2-multi').select2(
    {
        multiple: true,
        theme: 'bootstrap4',
    });
function selectAll() {
    $("#padres > option").prop("selected", "selected");
    $("#padres").trigger("change");
}

function deselectAll() {
    $("#padres > option").prop("selected", false);
    $("#padres").trigger("change");
}
$('.drgpicker').daterangepicker(
    {
        singleDatePicker: true,
        timePicker: false,
        showDropdowns: true,
        locale:
        {
            format: 'DD/MM/YYYY',
            applyLabel: 'Aceptar',
            cancelLabel: 'Cancelar',
            monthNames: [
                'Enero',
                'Febrero',
                'Marzo',
                'Abril',
                'Mayo',
                'Junio',
                'Julio',
                'Agosto',
                'Septiembre',
                'Octubre',
                'Noviembre',
                'Diciembre'
            ],
            daysOfWeek: [
                'Dom',
                'Lun',
                'Mar',
                'Mier',
                'Jue',
                'Vie',
                'Sab'
            ],
        }
    });
$('.time-input').timepicker(
    {
        'scrollDefault': 'now',
        'zindex': '9999' /* fix modal open */
    });


$('#padres').change(function () {
    $('#asistentes').empty();

    var selectedValues = $(this).val();
    var text = $('#padres option:selected').toArray().map(item => item.text).join();
    var str_array = text.split(',');
    var row = '';
    for (var i = 0; i < str_array.length; i++) {
        // Trim the excess whitespace.
        str_array[i] = str_array[i].replace(/^\s*/, "").replace(/\s*$/, "");
        var titulo = "<div class=row align-items-center h-100 my-2><div class=col><li class=mb-0>" + str_array[i] + "</li></div></div>";
        row = row + titulo;
    }
    $('#asistentes').html(row);
});

// Example starter JavaScript for disabling form submissions if there are invalid fields
(function () {
    'use strict';
    window.addEventListener('load', function () {
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var forms = document.getElementsByClassName('needs-validation');
        // Loop over them and prevent submission
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();

