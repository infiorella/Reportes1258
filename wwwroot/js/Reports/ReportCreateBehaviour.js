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
    $("#alumnos > option").prop("selected", "selected");
    $("#alumnos").trigger("change");
}

function deselectAll() {
    $("#alumnos > option").prop("selected", false);
    $("#alumnos").trigger("change");
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


function getSelectValues(select) {
    var result = [];
    var options = select && select.options;
    var opt;

    for (var i = 0, iLen = options.length; i < iLen; i++) {
        opt = options[i];

        if (opt.selected) {
            result.push(opt.value || opt.text);
        }
    }
    return result;
}