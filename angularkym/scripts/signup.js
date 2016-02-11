function SaveUserDetails()
{
    if ($('#signupdiv').data('bValidator').validate())
 {
 alert('success');
 }
}
$('document').ready(function () {
    $('#signupdiv').bValidator();
});