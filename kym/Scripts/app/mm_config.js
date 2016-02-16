var mmAPPURL = "http://localhost:49775";
var mmServiceUrl = "http://localhost:49951";

function getServerData(service_name, param1, param2, param3, param4, param5, param6, param7, param8, param9) {
    switch (service_name) {
        case 'SAVE_USER_DATA':
            return mmServiceUrl + '/Service/Service.svc/SaveUserDetails/' + param1 + '/' + param2 + '/' + param3 + '/' + param4 + '/' + param5 + '/' + param6 + '/' + param7;
            break;
        case 'Get_User_List':
            
            break;
    }
}