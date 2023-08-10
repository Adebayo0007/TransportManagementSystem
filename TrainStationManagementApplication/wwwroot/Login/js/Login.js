var email = document.getElementById('txtEmail');
var password = document.getElementById('txtPass');
var msg = document.getElementById('message');
var mainmsg = document.getElementById('msg-div');

// lets write expression now
var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
//passing must be more than 8 character with atleast two alphabets
var filter2 = /^(?=(.*[a-zA-Z]){1,})(?=(.*[0-9]){2,}).{8,}$/;

function validate() {
    if (filter.test(email.value) && filter2.test(password.value)) {
        mainmsg.style.display = "block";
        msg.innerHTML = "Login Success!";
        mainmsg.style.cssText += "background:#9c41a8;border-left:5px solid #fff";
    }
    else {
        mainmsg.style.display = "block";
        msg.innerHTML = "Email And Password Not Correct";
        mainmsg.style.cssText += "background:#ff0d72;border-left:5px solid #fff;"
    }
}