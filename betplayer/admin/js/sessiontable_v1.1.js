// Initialize Firebase
var config = {
    apiKey: "AIzaSyB2FBmOPhVzXCuy3WFWXeZFPHlKy5QyrAE",
    authDomain: "sarkar-73a30.firebaseapp.com",
    databaseURL: "https://sarkar-73a30.firebaseio.com",
    projectId: "sarkar-73a30",
    storageBucket: "sarkar-73a30.appspot.com",
    messagingSenderId: "957699320056"
};
var app = firebase.initializeApp(config);
var matchIdElement = document.getElementById("ContentPlaceHolder_firebasekey");
console.log("firebase connecting to match: " + matchIdElement.value.toString());
matchKey = matchIdElement.value;
var matchKey, activeSessionsLength;
var timer = setTimeout(function () { }, 0);
var sessionsDiv = document.getElementById('sessionsDiv');
var sessionsTable = sessionsDiv.appendChild(document.createElement("table")),
    thead = sessionsTable.createTHead(), // thead element
    thRow = thead.insertRow(), // trow element
    tbody = sessionsTable.createTBody(); // tbody element
thRow.insertCell(0).innerText = "Name";
thRow.insertCell(1).innerText = "Undeclare";

sessionsTable.classList.add("table")
document.addEventListener("DOMContentLoaded", function (event) {
    firebase.database().ref('/currentMatches/' + matchKey + "/sessions").on("value", // runs on page runder
        function (snapshot) {
            UpdateSessionsTable(snapshot.val());

        });
});


function UpdateSessionsTable(sessions, dataTable) {
    if (sessions) {
        console.log(sessions);
        if (typeof (sessions) === "object") {
            sessions = Object.keys(sessions).map(function (key) {
                Object.assign(sessions[key], { key: key });
                return [sessions[key]["key"], sessions[key]["name"], sessions[key]["active"], sessions[key]["declared"]];
            });
        } else if (typeof (sessions) === "Array") {
            sessions.map(function (session) {
                return [session["id"], session["name"], session["active"], session["declared"]]
            });
        }
        console.log(sessions);
        clearSessionsTable();
        var displayableSessions = sessions.filter(function (session) { return (!session[2] && session[3]) });
        
        for (i = 0; i < displayableSessions.length; i++) {
            var row = tbody.insertRow();
            var nameCell = row.insertCell(0);
            var activeCell = row.insertCell(1);
            
            var activeButton = activeCell.appendChild(document.createElement("a"));
            activeButton.style = "background-color:red;width:100%;height:100%;color:white;";
            activeButton.innerText = "Undeclare";
            activeButton.setAttribute("sessionID", displayableSessions[i][0]);
            activeButton.setAttribute("sessionName", displayableSessions[i][1]);
            activeButton.addEventListener("click", function (event) {
                //console.log(activeSessionsLength);
                //if (parseInt(activeSessionsLength) < 8 || (event.srcElement.getAttribute("sessionValue").toString() === "true"))
                //    firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + event.srcElement.getAttribute("sessionID").toString()).update({
                //        active: (event.srcElement.getAttribute("sessionValue").toString() === "true") ? false : true
                //    });
                //else alert("Active Sessions cannot be More than 8!!")
                UndeclareSession(event.srcElement.getAttribute("sessionName").toString(), event.srcElement.getAttribute("sessionID").toString())
            });
            nameCell.innerHTML = displayableSessions[i][1];
        }

    }
}
function UndeclareSession(sessionName, sessionkey) {

    var result = confirm("Want to Undeclare?");
    if (result) {
        var matchId = document.getElementById("ContentPlaceHolder_apiID");
        var params = {
            sessionKey: sessionName,
            MatchID: matchId.value
        };

        var formBody = [];
        for (var property in params) {
            var encodedKey = encodeURIComponent(property);
            var encodedValue = encodeURIComponent(params[property]);
            formBody.push(encodedKey + "=" + encodedValue);
        }
        formBody = formBody.join("&");

        fetch('/Admin/Undeclaresessionpost.ashx', {
            credentials: 'same-origin',
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;charset=UTF-8'
            },
            body: formBody
        }).then(function (responce) {
            return responce.json();
        }).then(function (data) {
            console.log(data);
            if (data.status) {
                firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + sessionkey.toString()).update({
                    active: true,
                    declared: false
                }).then(function () {
                    alert("Undeclared Successfully");
                });
            }
            else alert(data.error);
        }).catch(function (err) {
            console.log(err);
        });
    }
}

function clearSessionsTable() {
    tbody.innerHTML = "";
}









