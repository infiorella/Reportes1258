init()

function init() {
    $("#loginEye").show()
    $("#loginEyeClosed").hide()
    $("#password").attr("type", "password");
}

$("#loginEye").on("click", function () {
    $("#loginEye").hide()
    $("#loginEyeClosed").show()
    $("#password").attr("type", "text");
})

$("#loginEyeClosed").on("click", function () {
    $("#loginEye").show()
    $("#loginEyeClosed").hide()
    $("#password").attr("type", "password");
})