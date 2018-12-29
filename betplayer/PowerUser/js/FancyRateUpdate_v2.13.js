// Initialize Firebase
var config = {
    apiKey: "AIzaSyB2FBmOPhVzXCuy3WFWXeZFPHlKy5QyrAE",
    authDomain: "sarkar-73a30.firebaseapp.com",
    databaseURL: "https://sarkar-73a30.firebaseio.com",
    projectId: "sarkar-73a30",
    storageBucket: "sarkar-73a30.appspot.com",
    messagingSenderId: "957699320056"
};

var matchKey, activeSessionsLength;
var timer = setTimeout(function () { }, 0);
var GlobalSessions;
var notinputindex = 1;

document.addEventListener("DOMContentLoaded", function (event) {
    firebase.database().ref('/currentMatches/' + matchKey + "/sessions").on("value", // runs on page runder
        function (snapshot) {
            updateSessionTable(snapshot.val());
        });
});
(function () {
    firebase.initializeApp(config);
    var matchIdElement = document.getElementById("ContentPlaceHolder_firebasekey");
    console.log("firebase connecting to match: " + matchIdElement.value.toString());
    matchKey = matchIdElement.value;
    var timer = setTimeout(function () { }, 0);
    var notinputindex = 1;
    if (matchIdElement !== null) {
        firebase.database().ref('/currentMatches/' + matchKey).once("value", // runs on page runder
            function (snapshot) {
                var match = snapshot.val();
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

                document.getElementById('ball_start').addEventListener("click", updateScore);
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
                        document.getElementById('not1').focus();
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
                    }); firebase.database().ref('/currentMatches/' + matchKey + '/sessions').once("value", function (snapshot) {
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
                        document.getElementById('not1').focus();
                    });
                });



                document.getElementById('btnlockfancy').addEventListener("click", (event) => {
                    firebase.database().ref('/currentMatches/' + matchKey + '/sessions').once("value", function (snapshot) {
                        var currentsessions = snapshot.val();
                        if (typeof (currentsessions) === "object") {
                            Object.keys(currentsessions).forEach(function (key) {
                                if (currentsessions[key]["fancy"] === true)
                                    currentsessions[key]["manualLocked"] = true;
                            });
                        } else if (typeof (currentsessions) === "Array") {
                            currentsessions.forEach(function (session) {
                                if (session["fancy"] = true)
                                    session['manualLocked'] === false;
                            });
                        }
                        firebase.database().ref('/currentMatches/' + matchKey).update({ sessions: currentsessions });
                        firebase.database().ref('/currentMatches/' + matchKey + '/lastBall').update({
                            event: "Bet Closed"
                        });
                    }).then(function () {
                        document.getElementById('not1').focus();
                    });
                });
                document.getElementById('btnUnlockfancy').addEventListener("click", (event) => {
                    firebase.database().ref('/currentMatches/' + matchKey + '/sessions').once("value", function (snapshot) {
                        var currentsessions = snapshot.val();
                        if (typeof (currentsessions) === "object") {
                            Object.keys(currentsessions).forEach(function (key) {
                                if (currentsessions[key]["fancy"] === true)
                                    currentsessions[key]["manualLocked"] = false;
                            });
                        } else if (typeof (currentsessions) === "Array") {
                            currentsessions.forEach(function (session) {
                                if (session["fancy"] === true)
                                    session['manualLocked'] = false;
                            });
                        }
                        firebase.database().ref('/currentMatches/' + matchKey).update({ sessions: currentsessions });
                        firebase.database().ref('/currentMatches/' + matchKey + '/lastBall').update({
                            event: "Bet Open"
                        });
                    }).then(function () {
                        document.getElementById('not1').focus();
                    });
                });

                document.getElementById('btnAdd').addEventListener("click", (event) => {
                    var Over = document.getElementById('Over').value;
                    console.log(Over);
                    if (GlobalSessions !== undefined)
                        var sessionExist = GlobalSessions.filter(function (session) { return session["name"] === Over });
                    if (!sessionExist || sessionExist.length === 0) {
                        firebase.database().ref('/currentMatches/' + matchKey + '/sessions').push({
                            "id": "manual",
                            "name": Over,
                            "yes": "",
                            "not": "",
                            "yesRate": "",
                            "notRate": "",
                            "active": true,
                            "fancy": true
                        },
                            function (error) {
                                if (error) alert("Session failed to be added to database");
                                else updateSessionTable(match['session']);
                            });
                        document.getElementById('Over').value = "";
                    } else {
                        alert("Fancy Already Exists");
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
function focusNextElementOnEnterKeyPress(event) {
    if (event.keyCode === 13) {
        var sessionKey = event.srcElement.id.charAt(event.srcElement.id.length - 1);
        var elementType = event.srcElement.id.substring(0, event.srcElement.id.length - 1);
        if (elementType === "not") document.getElementById("yes" + sessionKey).focus();
        if (elementType === "yes" &&
            parseInt(document.getElementById('not' + sessionKey).value) <
            parseInt(document.getElementById('yes' + sessionKey).value)) {
            document.getElementById('notrate' + sessionKey).value = "1.00";
            document.getElementById('yesrate' + sessionKey).value = "1.00";
            document.getElementById("UpdateButton" + sessionKey).focus();
        }
        else if (elementType === "yes" &&
            document.getElementById('not' + sessionKey).value === document.getElementById('yes' + sessionKey).value) {
            document.getElementById('notrate' + sessionKey).readOnly = false;
            document.getElementById('yesrate' + sessionKey).readOnly = false;
            document.getElementById('notrate' + sessionKey).value = "1.10";
            document.getElementById('yesrate' + sessionKey).value = "0.90";
            document.getElementById("yesrate" + sessionKey).focus();
        }
        else if (elementType === "yes") document.getElementById("yesrate" + sessionKey).focus();
        if (elementType === "yesrate") document.getElementById("notrate" + sessionKey).focus();
        if (elementType === "notrate") document.getElementById("UpdateButton" + sessionKey).focus();
    }
}
function updateScore(event) {
    var lastBall = event.srcElement.innerHTML;
    console.log(lastBall);
    firebase.database().ref('/currentMatches/' + matchKey + '/lastBall').update(
        {
            event: lastBall
        });

    if (!(lastBall === "Bet Open" || lastBall === "No Run" || lastBall === "1 Run" || lastBall === "2 Run")) {
        firebase.database().ref('/currentMatches/' + matchKey + '/sessions').once("value", function (snapshot) {
            var currentsessions = snapshot.val();
            if (typeof (currentsessions) === "object") {
                Object.keys(currentsessions).forEach(function (key) {
                    currentsessions[key]['manualLocked'] = false;
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
    document.getElementById('not1').focus();
}

function focusDeclareElementOnEnterKeyPress(event) {
    if (event.keyCode === 13) {
        document.getElementById('btnDeclear').focus();
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
        var fancySessions = displayableSessions.filter(function (session) { return session['fancy'] });
        activeSessionsLength = fancySessions.length;
        clearSessionTable();
        clearSessionSelector();
        for (var i = 1; i <= activeSessionsLength && i <= 3; i++) {
            var session = fancySessions[i - 1];
            console.log(session);
            appendToSessionSelector(session['name'], session['key'], selectedSessionSelectorValue, deleteSelectedSessionSelectorValue);
            if (i < 5) {
                document.getElementById("Session" + i).value = session['name'];
                document.getElementById("UpdateButton" + i).setAttribute("customData", session['key']);
                document.getElementById("not" + i).value =
                    session["not"] !== "0.00" ? session["not"] : "";
                document.getElementById("yes" + i).value =
                    session["yes"] !== "0.00" ? session["yes"] : "";
                document.getElementById("notrate" + i).value =
                    session["notRate"] !== "0.00" ? session["notRate"] : "";
                document.getElementById("yesrate" + i).value =
                    session["yesRate"] !== "0.00" ? session["yesRate"] : "";
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
}

function clearSessionTable() {
    for (var i = 1; i <= 3; i++) {
        document.getElementById('Session' + i).value = "";
        document.getElementById('not' + i).value = "";
        document.getElementById('yes' + i).value = "";
        document.getElementById('notrate' + i).value = "";
        document.getElementById('yesrate' + i).value = "";
    }
}



function sessionTeamChanged() {
    var selector = document.getElementById('team_selector');
    // updateSessionTable(selector.value);
}

function updateSession(srcElement, sessionId) {
    var sessionKey = srcElement.getAttribute("customData");
    console.log(sessionKey);

    if (sessionKey !== "") {
        if (parseInt(document.getElementById('not' + sessionId).value) <=
            parseInt(document.getElementById('yes' + sessionId).value)) {
            firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + sessionKey).update({
                "not": document.getElementById('not' + sessionId).value,
                "yes": document.getElementById('yes' + sessionId).value,
                "notRate": (document.getElementById('notrate' + sessionId).value),
                "yesRate": (document.getElementById('yesrate' + sessionId).value)
            }).then(function () {
                firebase.database().ref('/currentMatches/' + matchKey + '/lastBall').update(
                    {
                        event: "Bet Open"
                    });
                document.getElementById('notrate' + sessionId).readOnly = true;
                document.getElementById('yesrate' + sessionId).readOnly = true;
                document.getElementById('not1').focus();
                clearTimeout(timer);
                timer = setTimeout(function () {
                    console.log("Running timer");
                    document.getElementById('yes' + sessionId).focus();
                    firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + sessionKey).update({
                        "not": "0.00",
                        "yes": "0.00",
                        "notRate": "0.00",
                        "yesRate": "0.00"
                    }).then(function () {
                        document.getElementById('not1').focus();
                    });
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

    var result = confirm("Want to declare?");
    if (result) {
        var matchId = document.getElementById("ContentPlaceHolder_apiID");
        var fk = document.getElementById("ContentPlaceHolder_firebasekey");
        var sessionkey = document.getElementById('session_selector').value;
        var sessionName = document.getElementById('session_selector').selectedOptions[0].text;
        firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + sessionkey.toString()).update({
            active: false
        });
        var params = {
            sessionKey: sessionName,
            declareValue: document.getElementById('Declear').value,
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
                firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + sessionkey).update({
                    active: false,
                    declared: true
                });
                document.getElementById('Declear').value = "";
                alert("Declared Successfully");

            }
            else alert("Session Already Declared !!!");
        }).then(function () {

        }).catch(function (err) {
            console.log(err);
        });
    }
}
function deleteSession() {
    var result = confirm("Want to delete?");
    if (result) {
        var matchId = document.getElementById("ContentPlaceHolder_apiID");
        var sessionkey = document.getElementById('session_selectordelete').value;
        var sessionName = document.getElementById('session_selectordelete').selectedOptions[0].text;
        firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + sessionkey.toString()).update({
            active: false
        });
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
                firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + sessionkey).remove();
                document.getElementById('Declear').value = "";
                alert("Fancy Delete  Successfully");

            }
            else alert("Fancy Already Deleted !!!");
        }).then(function () {

        }).catch(function (err) {
            console.log(err);
        });
    }
}


