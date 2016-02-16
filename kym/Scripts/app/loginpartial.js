$('document').ready(function () {

    $('#login_form').bValidator();

});
function login() {
    debugger;
    if ($('#login_form').data('bValidator').validate()) {

            return true;
        }
        event.preventDefault(); // if you want to disable the action
        return false;

   
}
