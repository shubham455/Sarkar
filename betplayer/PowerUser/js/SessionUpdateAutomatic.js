var config = {
    apiKey: "AIzaSyB2FBmOPhVzXCuy3WFWXeZFPHlKy5QyrAE",
    authDomain: "sarkar-73a30.firebaseapp.com",
    databaseURL: "https://sarkar-73a30.firebaseio.com",
    projectId: "sarkar-73a30",
    storageBucket: "sarkar-73a30.appspot.com",
    messagingSenderId: "957699320056"
};
var GlobalSessions;
var matchKey, activeSessionsLength;
var timer = setTimeout(function () { }, 0);
var sessionsDiv = document.getElementById('sessionsDiv');
var sessionsTable = sessionsDiv.appendChild(document.createElement("table")),
    thead = sessionsTable.createTHead(), // thead element
    thRow = thead.insertRow(), // trow element
    tbody = sessionsTable.createTBody(); // tbody element
thRow.insertCell(0).innerText = "Name";
thRow.insertCell(1).innerText = "Active";
thRow.insertCell(2).innerText = "Suspended";
thRow.insertCell(3).innerText = "Fancy Setting";
sessionsTable.classList.add("table")
document.addEventListener("DOMContentLoaded", function (event) {
    firebase.database().ref('/currentMatches/' + matchKey + "/sessions").on("value", // runs on page runder
        function (snapshot) {
            updateSessionTable(snapshot.val());
            UpdateSessionsTable(snapshot.val());

        });
});
(function () {
    firebase.initializeApp(config);
    var matchIdElement = document.getElementById("ContentPlaceHolder_firebasekey");
    console.log("firebase connecting to match: " + matchIdElement.value.toString());
    matchKey = matchIdElement.value;
    var timer = setTimeout(function () { }, 0);
    if (matchIdElement !== null) {
        firebase.database().ref('/currentMatches/' + matchKey).once("value", // runs on page runder
            function (snapshot) {
                var match = snapshot.val();
                document.getElementById('team1_name').text = match['team_1']['Name'];
                document.getElementById('team2_name').text = match['team_2']['Name'];
                var team = document.getElementById('team_selector').value;
                return match;
            }).then((match) => {
                document.onkeydown = function (event) {
                    console.log(event.keyCode);
                    if (111 < event.keyCode && event.keyCode < 124) {
                        event.preventDefault();
                        if (event.keyCode === 119) {
                            document.getElementById('ball_start').click();
                        }
                        if (event.keyCode === 120) {
                            f9PressEvent();
                        }
                        if (event.keyCode === 112) {
                            document.getElementById('btnclearrate').click();
                        }
                        if (event.keyCode === 113) {
                            document.getElementById('btnUnclearrate').click();
                        }
                    }
                }


                document.getElementById('NoRun').addEventListener("click", updateScore);
                document.getElementById('1run').addEventListener("click", updateScore);
                document.getElementById('2run').addEventListener("click", updateScore);
                document.getElementById('3run').addEventListener("click", updateScore);
                document.getElementById('four').addEventListener("click", updateScore);
                document.getElementById('six').addEventListener("click", updateScore);
                document.getElementById('out').addEventListener("click", updateScore);
                document.getElementById('wide').addEventListener("click", updateScore);
                document.getElementById('noball').addEventListener("click", updateScore);
                document.getElementById('freehit').addEventListener("click", updateScore);
                document.getElementById('wideplus4').addEventListener("click", updateScore);
                document.getElementById('timeout').addEventListener("click", updateScore);
                document.getElementById('thirdumpire').addEventListener("click", updateScore);
                document.getElementById('Review').addEventListener("click", updateScore);
                document.getElementById('Inningsbreak').addEventListener("click", updateScore);
                document.getElementById('ball_start').addEventListener("click", updateScore);
                document.getElementById('bet_open').addEventListener("click", updateScore);


                document.getElementById('bet_closed').addEventListener("click", (event) => {

                    firebase.database().ref('/currentMatches/' + matchKey + '/team_1').update({
                        Runner: {
                            Khai: "0.00",
                            Lagai: "0.00"
                        }
                    });
                    firebase.database().ref('/currentMatches/' + matchKey + '/team_2').update({
                        Runner: {
                            Khai: "0.00",
                            Lagai: "0.00"
                        }
                    });
                    firebase.database().ref('/currentMatches/' + matchKey + '/lastBall').update({
                        event: "Bet Closed"
                    });
                    firebase.database().ref('/currentMatches/' + matchKey + '/sessions').once("value", function (snapshot) {
                        var currentsessions = snapshot.val();
                        if (typeof (currentsessions) === "object") {
                            Object.keys(currentsessions).forEach(function (key) {
                                currentsessions[key]["not"] = "0.00";
                                currentsessions[key]["yes"] = "0.00";
                                currentsessions[key]["notRate"] = "0.00";
                                currentsessions[key]["yesRate"] = "0.00";
                            });
                        } else if (typeof (currentsessions) === "Array") {
                            currentsessions.forEach(function (session) {
                                session["not"] = "0.00";
                                session["yes"] = "0.00";
                                session["notRate"] = "0.00";
                                session["yesRate"] = "0.00";
                            });
                        }
                        firebase.database().ref('/currentMatches/' + matchKey).update({ sessions: currentsessions });
                    }).then(function () {
                        document.getElementById("not1").focus();
                    });
                });

                document.getElementById('btnclearrate').addEventListener("click", (event) => {

                    firebase.database().ref('/currentMatches/' + matchKey + '/team_1').update({
                        RunnerUnLocked: false
                    });
                    firebase.database().ref('/currentMatches/' + matchKey + '/team_2').update({
                        RunnerUnLocked: false
                    });
                    firebase.database().ref('/currentMatches/' + matchKey + '/lastBall').update({
                        event: "Bet Closed"
                    }).then(function () {
                        document.getElementById("not1").focus();
                    });
                });
                document.getElementById('btnUnclearrate').addEventListener("click", (event) => {

                    firebase.database().ref('/currentMatches/' + matchKey + '/team_1').update({
                        RunnerUnLocked: true
                    });
                    firebase.database().ref('/currentMatches/' + matchKey + '/team_2').update({
                        RunnerUnLocked: true
                    });
                    firebase.database().ref('/currentMatches/' + matchKey + '/lastBall').update({
                        event: "Bet Open"
                    }).then(function () {
                        document.getElementById("not1").focus();
                    });
                });
                document.getElementById('btnlocksession').addEventListener("click", (event) => {
                    firebase.database().ref('/currentMatches/' + matchKey + '/sessions').once("value", function (snapshot) {
                        var currentsessions = snapshot.val();
                        if (typeof (currentsessions) === "object") {
                            Object.keys(currentsessions).forEach(function (key) {
                                currentsessions[key]["not"] = "0.00";
                                currentsessions[key]["yes"] = "0.00";
                                currentsessions[key]["notRate"] = "0.00";
                                currentsessions[key]["yesRate"] = "0.00";
                            });
                        } else if (typeof (currentsessions) === "Array") {
                            currentsessions.forEach(function (session) {
                                if (session["fancy"] === false)
                                    session['manualLocked'] = false;
                            });
                        }
                        firebase.database().ref('/currentMatches/' + matchKey).update({ sessions: currentsessions });
                        firebase.database().ref('/currentMatches/' + matchKey + '/lastBall').update({
                            event: "Bet Closed"
                        }).then(function () {
                            document.getElementById("not1").focus();
                        });
                    });
                });


                document.getElementById('btnUnlocksession').addEventListener("click", (event) => {
                    firebase.database().ref('/currentMatches/' + matchKey + '/sessions').once("value", function (snapshot) {
                        var currentsessions = snapshot.val();
                        if (typeof (currentsessions) === "object") {
                            Object.keys(currentsessions).forEach(function (key) {
                                if (currentsessions[key]["fancy"] === false)
                                    currentsessions[key]["manualLocked"] = false;
                            });
                        } else if (typeof (currentsessions) === "Array") {
                            currentsessions.forEach(function (session) {
                                if (session["fancy"] === false)
                                    session['manualLocked'] = false;
                            });
                        }
                        firebase.database().ref('/currentMatches/' + matchKey).update({ sessions: currentsessions });
                        firebase.database().ref('/currentMatches/' + matchKey + '/lastBall').update({
                            event: "Bet Open"
                        }).then(function () {
                            document.getElementById("not1").focus();
                        });
                    });
                });



                document.getElementById('btnAdd').addEventListener("click", (event) => {
                    var team = document.getElementById('team_selector').value
                    var teamName = (document.getElementById('team1_name').value === team) ?
                        document.getElementById('team1_name').innerHTML :
                        document.getElementById('team2_name').innerHTML;
                    var Over = document.getElementById('Over').value;
                    var name = Over + " " + teamName;
                    console.log(Over);
                    if (GlobalSessions !== undefined)
                        var sessionExist = GlobalSessions.filter(function (session) { return session["name"] === name });
                    if (!sessionExist || sessionExist.length === 0) {
                        firebase.database().ref('/currentMatches/' + matchKey + '/sessions').push({
                            "id": "manual",
                            "name": name,
                            "yes": "",
                            "not": "",
                            "yesRate": "",
                            "notRate": "",
                            "active": true,
                            "fancy": false
                        },
                            function (error) {
                                if (error) alert("Session failed to be added to database");
                                else updateSessionTable(match['sessions']);
                            });
                        document.getElementById('Over').value = "";
                    } else {
                        alert("Session Already Exists");
                    }


                });
            });

    }
})()


function f9PressEvent() {
    if (notinputindex === 4) notinputindex = 1;
    if (document.getElementById('Session' + notinputindex).value === "") {
        if (notinputindex !== 1) {
            notinputindex++;
            f9PressEvent();
        }
        else {
            document.getElementById('not' + notinputindex).focus();
            notinputindex++;
        }
    }
    else {
        document.getElementById('not' + notinputindex).focus();
        notinputindex++;
    }
}
function updateScore(event) {
    var team = document.getElementById('team_selector').value
    console.log(event.srcElement.value);
    firebase.database().ref('/currentMatches/' + matchKey + '/lastBall').update(
        {
            event: event.srcElement.innerHTML
        });
    var lastBall = event.srcElement.innerHTML;

    if (!(lastBall === "Bet Open" || lastBall === "No Run" || lastBall === "1 Run" || lastBall === "2 Run")) {
        firebase.database().ref('/currentMatches/' + matchKey + '/sessions').once("value", function (snapshot) {
            var currentsessions = snapshot.val();
            if (typeof (currentsessions) === "object") {
                Object.keys(currentsessions).forEach(function (key) {
                    currentsessions[key]['manualLocked'] = false;
                    currentsessions[key]["not"] = "0.00";
                    currentsessions[key]["yes"] = "0.00";
                    currentsessions[key]["notRate"] = "0.00";
                    currentsessions[key]["yesRate"] = "0.00";
                });
            } else if (typeof (currentsessions) === "Array") {
                currentsessions.forEach(function (session) {
                    session['manualLocked'] = false;
                    session["not"] = "0.00";
                    session["yes"] = "0.00";
                    session["notRate"] = "0.00";
                    session["yesRate"] = "0.00";
                });
            }

            firebase.database().ref('/currentMatches/' + matchKey).update({ sessions: currentsessions });
        });
    }
    document.getElementById("not1").focus();
}

function focusNextElementOnEnterKeyPress(event) {
    if (event.keyCode === 13) {
        var sessionKey = event.srcElement.id.charAt(event.srcElement.id.length - 1);
        var elementType = event.srcElement.id.substring(0, event.srcElement.id.length - 1);
        if (elementType === "not") document.getElementById("yes" + sessionKey).focus();
        if (elementType === "yes" &&
            parseInt(document.getElementById('not' + sessionKey).value) <
            parseInt(document.getElementById('yes' + sessionKey).value)) {
            document.getElementById('notrate' + sessionKey).value = "100";
            document.getElementById('yesrate' + sessionKey).value = "100";
            document.getElementById("UpdateButton" + sessionKey).focus();
        }
        else if (elementType === "yes" &&
            document.getElementById('not' + sessionKey).value === document.getElementById('yes' + sessionKey).value) {
            document.getElementById('notrate' + sessionKey).readOnly = false;
            document.getElementById('yesrate' + sessionKey).readOnly = false;
            document.getElementById('notrate' + sessionKey).value = "110";
            document.getElementById('yesrate' + sessionKey).value = "90";
            document.getElementById("yesrate" + sessionKey).focus();
        }
        else if (elementType === "yes") document.getElementById("yesrate" + sessionKey).focus();
        if (elementType === "yesrate") document.getElementById("notrate" + sessionKey).focus();
        if (elementType === "notrate") document.getElementById("UpdateButton" + sessionKey).focus();
    }
}

function focusDeclareElementOnEnterKeyPress(event) {
    if (event.keyCode === 13) {
        document.getElementById('btnDeclear').focus();
    }
}

function UpdateSessionsTable(sessions, dataTable) {
    if (sessions) {
        console.log(sessions);
        if (typeof (sessions) === "object") {
            sessions = Object.keys(sessions).map(function (key) {
                Object.assign(sessions[key], { key: key });
                return [sessions[key]["key"], sessions[key]["name"], sessions[key]["active"], sessions[key]["suspended"], sessions[key]["fancy"]];
            });
        } else if (typeof (sessions) === "Array") {
            sessions.map(function (session) {
                return [session["id"], session["name"], session["active"], session["suspended"], sessions["fancy"]]
            })
        }
        console.log(sessions);
        clearSessionsTable();
        for (i = 0; i < sessions.length; i++) {
            var row = tbody.insertRow();
            var nameCell = row.insertCell(0);
            var activeCell = row.insertCell(1);
            var suspendedCell = row.insertCell(2);
            var fancyCell = row.insertCell(3);
            var activeButton = activeCell.appendChild(document.createElement("a"));
            activeButton.style = (sessions[i][2]) ? "background-color:red;width:100%;height:100%;color:white;" : "background-color:green;width:100%;height:100%;color:white;";
            activeButton.innerText = (sessions[i][2]) ? "Active" : "Inactive";
            activeButton.setAttribute("sessionID", sessions[i][0]);
            activeButton.setAttribute("sessionValue", sessions[i][2]);
            activeButton.addEventListener("click", function (event) {
                console.log(activeSessionsLength);
                if (parseInt(activeSessionsLength) < 8 || (event.srcElement.getAttribute("sessionValue").toString() === "true"))
                    firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + event.srcElement.getAttribute("sessionID").toString()).update({
                        active: (event.srcElement.getAttribute("sessionValue").toString() === "true") ? false : true
                    });
                else alert("Active Sessions cannot be More than 8!!")
            });
            var suspendedButton = suspendedCell.appendChild(document.createElement("a"));
            suspendedButton.style = (sessions[i][3]) ? "background-color:red;width:100%;height:100%;color:white;" : "background-color:green;width:100%;height:100%;color:white;";
            suspendedButton.innerText = (sessions[i][3]) ? "Suspended" : "Not Suspended";
            suspendedButton.setAttribute("sessionID", sessions[i][0]);
            suspendedButton.setAttribute("sessionValue", sessions[i][3]);
            suspendedButton.addEventListener("click", function (event) {
                console.log(event.srcElement.getAttribute("sessionValue").toString());
                firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + event.srcElement.getAttribute("sessionID").toString()).update({
                    suspended: (event.srcElement.getAttribute("sessionValue").toString() === "true") ? false : true
                });
            });
            var fancycheckbox = document.createElement("input");
            fancycheckbox.setAttribute("type", "checkbox");
            fancycheckbox.setAttribute("id", "fancycheckbox" + i);
            fancycheckbox.setAttribute("sessionID", sessions[i][0]);
            if (sessions[i][4]) fancycheckbox.setAttribute("checked", "true");
            var sessioncheckbox = document.createElement("input");
            sessioncheckbox.setAttribute("type", "checkbox");
            sessioncheckbox.setAttribute("id", "sessioncheckbox" + i);
            sessioncheckbox.setAttribute("sessionID", sessions[i][0]);
            if (!sessions[i][4]) sessioncheckbox.setAttribute("checked", "true");
            fancycheckbox.addEventListener("click", function (event) {
                var elementID = event.srcElement.getAttribute("id");
                var i = elementID.substr(13, elementID.length - 1);
                document.getElementById("sessioncheckbox" + i).checked = !document.getElementById("fancycheckbox" + i).checked;
                firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + event.srcElement.getAttribute("sessionID").toString()).update({
                    fancy: document.getElementById("fancycheckbox" + i).checked
                });
            });
            sessioncheckbox.addEventListener("click", function (event) {
                var elementID = event.srcElement.getAttribute("id");
                var i = elementID.substr(15, elementID.length - 1);
                document.getElementById("fancycheckbox" + i).checked = !document.getElementById("sessioncheckbox" + i).checked;
                firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + event.srcElement.getAttribute("sessionID").toString()).update({
                    fancy: !document.getElementById("sessioncheckbox" + i).checked
                });
            });
            var fancypara = document.createElement("label");
            fancypara.innerHTML = "Fancy";
            fancypara.setAttribute("for", "fancycheckbox" + i);
            var sessionpara = document.createElement("label");
            sessionpara.innerHTML = "Session";
            sessionpara.setAttribute("for", "sessioncheckbox" + i);
            fancyCell.appendChild(fancycheckbox);
            fancyCell.appendChild(fancypara);
            fancyCell.appendChild(sessioncheckbox);
            fancyCell.appendChild(sessionpara);
            nameCell.innerHTML = sessions[i][1];
        }

    }
}

function updateSessionTable(sessions) {
    if (sessions) {
        if (typeof (sessions) === "object") {
            sessions = Object.keys(sessions).map(function (key) {
                Object.assign(sessions[key], { key: key });
                return sessions[key];
            });
        }
        console.log(sessions);
        GlobalSessions = sessions;
        var selectedSessionSelectorValue = getSessionSelectorValue();
        var deleteSelectedSessionSelectorValue = getDeleteSessionSelectorValue();
        var displayableSessions = sessions.filter(function (session) { return session['active'] });
        activeSessionsLength = displayableSessions.length;
        clearSessionTable();
        clearSessionSelector();
        for (var i = 1; i <= activeSessionsLength && i <= 8; i++) {
            var session = displayableSessions[i - 1];
            console.log(session);
            appendToSessionSelector(session['name'], session['key'], selectedSessionSelectorValue, deleteSelectedSessionSelectorValue);
            if (i < 5) {
                document.getElementById("Session" + i).value = session['name'];
                document.getElementById("UpdateButton" + i).setAttribute("customData", session['key']);
                if (session['suspended'] === false) {
                    document.getElementById("not" + i).value =
                        session["not"] !== "" ? session["not"] : "0.00";
                    document.getElementById("yes" + i).value =
                        session["yes"] !== "" ? session["yes"] : "0.00";
                    document.getElementById("notrate" + i).value =
                        session["notRate"] !== "" ? session["notRate"] : "0.00";
                    document.getElementById("yesrate" + i).value =
                        session["yesRate"] !== "" ? session["yesRate"] : "0.00";
                } else {
                    document.getElementById("not" + i).value = "0.00";
                    document.getElementById("yes" + i).value = "0.00";
                    document.getElementById("notrate" + i).value = "0.00";
                    document.getElementById("yesrate" + i).value = "0.00";
                }

            }
        }
    }
}
function getSessionSelectorValue() {
    return document.getElementById("session_selector").value;
}

function getDeleteSessionSelectorValue() {
    return document.getElementById("session_selectordelete").value;
}
function appendToSessionSelector(sessionName, sessionValue, selectedValue, deleteSelectedValue) {
    var option = document.createElement("option");
    var deleteOption = document.createElement("option");
    option.text = sessionName;
    deleteOption.text = sessionName;
    option.value = sessionValue;
    deleteOption.value = sessionValue;
    option.style = "font-size: large;";
    if (sessionValue === selectedValue)
        option.selected = true;
    if (sessionValue === deleteSelectedValue)
        deleteOption.selected = true;
    var select = document.getElementById("session_selector");
    var selectdelete = document.getElementById("session_selectordelete");

    select.appendChild(option);
    selectdelete.appendChild(deleteOption);

}

function clearSessionSelector() {
    document.getElementById("session_selector").innerHTML = "";
    document.getElementById("session_selectordelete").innerHTML = "";
}

function clearSessionTable() {
    for (var i = 1; i <= 4; i++) {
        document.getElementById('Session' + i).value = "";
        document.getElementById('not' + i).value = "";
        document.getElementById('yes' + i).value = "";
        document.getElementById('notrate' + i).value = "";
        document.getElementById('yesrate' + i).value = "";
    }
}

function clearSessionsTable() {
    tbody.innerHTML = "";
}

function sessionTeamChanged() {
    var selector = document.getElementById('team_selector');
    // updateSessionTable(selector.value);
}

function updateSession(srcElement, sessionId) {
    var sessionKey = srcElement.getAttribute("customData");
    console.log(sessionKey);
    var Team = document.getElementById('team_selector').value;
    if (sessionKey !== "") {
        if (parseInt(document.getElementById('not' + sessionId).value) <=
            parseInt(document.getElementById('yes' + sessionId).value)) {
            firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + sessionKey).update({
                "not": document.getElementById('not' + sessionId).value,
                "yes": document.getElementById('yes' + sessionId).value,
                "notRate": toCorrectFormat(document.getElementById('notrate' + sessionId).value),
                "yesRate": toCorrectFormat(document.getElementById('yesrate' + sessionId).value)
            }).then(function () {
                document.getElementById('notrate' + sessionId).readOnly = true;
                document.getElementById('yesrate' + sessionId).readOnly = true;
                document.getElementById('not' + sessionId).focus();
                clearTimeout(timer);
                timer = setTimeout(function () {
                    console.log("Running timer");
                    firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + sessionKey).update({
                        "not": "0.00",
                        "yes": "0.00",
                        "notRate": "0.00",
                        "yesRate": "0.00"
                    })
                }, 10000)
            }).catch(function (err) {
                console.log(err);
                alert("Session Not Updated.");
            });
        } else alert("Not is greater than Yes.");
    } else {
        alert('No session key present.');
        document.getElementById('not' + sessionId).value = "";
        document.getElementById('yes' + sessionId).value = "";
        document.getElementById('notrate' + sessionId).value = "";
        document.getElementById('yesrate' + sessionId).value = "";
    }
}

function toCorrectFormat(value) {
    if (parseInt(value) === 100) return "1.00";
    var floatValue = parseFloat(value);
    floatValue /= 100;
    floatValue = (parseInt(value) % 10 === 0) ? floatValue.toString().concat('0') : floatValue.toString();
    return floatValue;
}

function declareSession() {
    if (document.getElementById('Declear').value === "") {
        alert("Please Fill Any Value in TextBox");
    } else {
        var result = confirm("Want to declare?");
        if (result) {
            var matchId = document.getElementById("ContentPlaceHolder_apiID");
            var fk = document.getElementById("ContentPlaceHolder_firebasekey");
            var sessionkey = document.getElementById('session_selector').value;
            var sessionName = document.getElementById('session_selector').selectedOptions[0].text;
            var team = document.getElementById('team_selector').value;
            
            var params = {
                sessionKey: sessionName,
                declareValue: document.getElementById('Declear').value,
                team: team,
                MatchID: matchId.value

            };

            var formBody = [];
            for (var property in params) {
                var encodedKey = encodeURIComponent(property);
                var encodedValue = encodeURIComponent(params[property]);
                formBody.push(encodedKey + "=" + encodedValue);
            }
            formBody = formBody.join("&");

            fetch('/PowerUser/Session.ashx', {
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
                        active: false,
                        declared : true
                    });
                    firebase.database().ref('/currentMatches/' + matchKey + '/' + team.toString() + '/Session/' + sessionkey).remove().then(function () {
                        updateSessionTable(team);
                    });
                    document.getElementById('Declear').value = "";
                    alert("Declared Successfully");

                }
                else if (data.status === "unsuccess") {
                    alert("Your Session Expire...");
                    Location.reload();
                }
                else alert("Session Already Declared !!!");
            }).then(function () {

            }).catch(function (err) {
                console.log(err);
            });
        }
    }
}
function deleteSession() {
    var result = confirm("Want to delete?");
    if (result) {
        var matchId = document.getElementById("ContentPlaceHolder_apiID");
        var sessionkey = document.getElementById('session_selectordelete').value;
        var sessionName = document.getElementById('session_selectordelete').selectedOptions[0].text;

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

        fetch('/PowerUser/sessiondelete.ashx', {
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
                console.log(sessionkey);
                firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + sessionkey).remove();
                document.getElementById('Declear').value = "";

                alert("Delete Successfully");
                location.reload();
            }
            else alert("Session Already Declared !!!");
        }).then(function () {

        }).catch(function (err) {
            console.log(err);
        });
    }
}