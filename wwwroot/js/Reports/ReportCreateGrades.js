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
    $("#asistentes > option").prop("selected", "selected");
    $("#asistentes").trigger("change");
}

function deselectAll() {
    $("#asistentes > option").prop("selected", false);
    $("#asistentes").trigger("change");
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

