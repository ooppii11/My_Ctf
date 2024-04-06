
function checkUserAndPassInput(username, password, errorMessageId) {
    username = htmlEncode(username);
    password = htmlEncode(password);

    if (username === "" || password === "") {
        document.getElementById(errorMessageId).innerText = "Please enter both username and password.";
        return false;
    }

    let strongPasswordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
    if (!strongPasswordRegex.test(password)) {
        document.getElementById(errorMessageId).innerText = "Password must be at least 8 English characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.";
        return false;
    }


    return true;
}

function htmlEncode(value) {
    var encodedValue = value.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;");
    return encodedValue;
}
//    <script src="<%= ResolveUrl("~/js/script.js") %>"></script>
