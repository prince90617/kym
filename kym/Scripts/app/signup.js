jQuery.support.cors = true;
function SaveUserDetails() {
    if ($('#signupdiv').data('bValidator').validate()) {
        var domemail = "";
        var trailEmail = "";
        var fname = document.getElementById('fname').value;
         var lname = document.getElementById('lname').value;
         var gender = document.getElementById('gender').value;
         var mobileno = document.getElementById('mobileno').value;
        var newemail = document.getElementById('email').value;
        var splitteddata = newemail.split('@');
        var splitteddata2 = splitteddata[1].split('.');
        var splitteddata3 = splitteddata[0].split('.');
        if (splitteddata3.length > 1) {
            for (var i = 0; i < splitteddata3.length; i++) {
                if (i === (splitteddata3.length - 1))
                    domemail += splitteddata3[i];
                else
                    domemail += splitteddata3[i] + '-';
            }
        }
        else
            domemail = splitteddata3[0];

        if (splitteddata2.length > 1) {
            for (var j = 0; j < splitteddata2.length; j++) {
                if (j === (splitteddata2.length - 1))
                    trailEmail += splitteddata2[j];
                else
                    trailEmail += splitteddata2[j] + '-';
            }
        }
                var email = domemail + '%40' + trailEmail;
                var password = document.getElementById('password').value;
                var businesstype = document.getElementById('businesstype').value;

        var UserData = {
            "fname": fname,
            "lname": lname,
            "gender": gender,
            "mobileno":mobileno,
            "email": email,
            "password": password,
            "businesstype":businesstype
        };
        var Udata = JSON.stringify(UserData);
        var resturl = getServerData('SAVE_USER_DATA', fname,lname, gender,mobileno, email, password,businesstype);
        // KXLog("repoadmin.js KxService URL to insert the UserData:-" + resturl);
        $.ajax({
            type: "POST",
            url: resturl,
            data: Udata,
            dataType: "json",
            contentType: "application/json",
            crossDomain: true,
            processData:false,
            success: function (data) {
                if (data.errortype != undefined)
                    redirectToKxError(data);
                else {
                    if (data == true) {
                      //  KXLog("signup.js: success of SaveUser Data method");

                        ShowNotice('MentalMates', 'The User has been added successfully.');
                        $(".btn").colorbox.close();
                    }
                    else {
                        //KXLog("signup.js: Failure of SaveUser Data method");
                        $('#signupdiv').data('bValidators').first.showMsg($('#txtemail'), 'This User already exists');
                        $('#txtemail').select();
                    }
                }
            },
            error: function (jqxhr, textStatus, error) {
            }
        });
    }
}
$('document').ready(function () {
    $('#signupdiv').bValidator();
});

