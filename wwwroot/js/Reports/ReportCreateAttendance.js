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
