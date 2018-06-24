// Write your JavaScript code.

$(document).ready(function () {
    var access_token = $("#access_token").html();
    var id_token = $("#id_token").html();

    var decoded_at = JSON.stringify(jwt_decode(access_token), undefined, 2);
    var decoded_it = JSON.stringify(jwt_decode(id_token), undefined, 2);

    $("#access_token_decoded").html(decoded_at);
    $("#id_token_decoded").html(decoded_it);

    var response = $("#resource_server_response").html();

    var new_response = JSON.stringify(response, undefined, 2);

    $("#resource_server_response").html(new_response);

});


