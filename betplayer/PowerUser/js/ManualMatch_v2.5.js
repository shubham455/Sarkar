// Initialize Firebase
var config = {
    apiKey: "AIzaSyB2FBmOPhVzXCuy3WFWXeZFPHlKy5QyrAE",
    authDomain: "sarkar-73a30.firebaseapp.com",
    databaseURL: "https://sarkar-73a30.firebaseio.com",
    projectId: "sarkar-73a30",
    storageBucket: "sarkar-73a30.appspot.com",
    messagingSenderId: "957699320056"
};
firebase.initializeApp(config);
var matchIdElement = document.getElementById("ContentPlaceHolder_firebasekey");
console.log("firebase connecting to match: " + matchIdElement.value.toString());
var matchKey = matchIdElement.value;
document.addEventListener("DOMContentLoaded", function (event) {
    firebase.database().ref('/currentMatches/' + matchKey + "/sessions").on("value", // runs on page runder
        function (snapshot) {
            UpdateSessionsTable(snapshot.val());
        });
});
if (matchIdElement !== null) {
    firebase.database().ref('/currentMatches/' + matchKey).once("value", // runs on page runder
        function (snapshot) {
            var match = snapshot.val();
            document.getElementById('team1_name').text = match['team_1']['Name'];
            document.getElementById('team2_name').text = match['team_2']['Name'];
        }).then(() => {
            //Attching EventListner or Global Key presses
            document.onkeydown = function (event) {
                console.log(event.keyCode);
                if (111 < event.keyCode && event.keyCode < 124) {
                    event.preventDefault();
                    if (event.keyCode === 119) {
                        document.getElementById('ball_start').click();
                    }
                    if (event.keyCode === 120) {
                        document.getElementById('bet_open').click();
                    }
                    if (event.keyCode === 112) {
                        document.getElementById('btnclearrate').click();
                    }
                    if (event.keyCode === 113) {
                        document.getElementById('btnUnclearrate').click();
                    }
                }
            }

            //Attching EventListener on Input Element   
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

            document.getElementById('bet_open').addEventListener("click", (event) => {

                firebase.database().ref('/currentMatches/' + matchKey + '/team_1').update({
                    RunnerUnLocked: true
                });
                firebase.database().ref('/currentMatches/' + matchKey + '/team_2').update({
                    RunnerUnLocked: true
                });
                firebase.database().ref('/currentMatches/' + matchKey + '/lastBall').update({
                    event: "Bet Open"
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
                });
            });

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
                });
            });

            document.getElementById('btnmessage').addEventListener("click", (event) => {
                var team = document.getElementById('team_selector').value
                var message = document.getElementById('InputMessage').value
                console.log(message);
                firebase.database().ref('/currentMatches/' + matchKey).update(
                    {
                        message: message
                    });
            });
            document.getElementById('btnclearrate').addEventListener("click", (event) => {
                
                firebase.database().ref('/currentMatches/' + matchKey + '/team_1').update({
                    RunnerUnLocked: false
                });
                firebase.database().ref('/currentMatches/' + matchKey + '/team_2').update({
                    RunnerUnLocked: false
                });
            });
            document.getElementById('btnUnclearrate').addEventListener("click", (event) => {

                firebase.database().ref('/currentMatches/' + matchKey + '/team_1').update({
                    RunnerUnLocked: true
                });
                firebase.database().ref('/currentMatches/' + matchKey + '/team_2').update({
                    RunnerUnLocked: true
                });
            });

            firebase.database().ref('/currentMatches/' + matchKey).on("value", // runs on page runder
                function (snapshot) {
                    var match = snapshot.val();
                    var team = document.getElementById('team_selector').value;
                    document.getElementById('InputRun').value = match[team]["Runs"];
                    document.getElementById('InputWicket').value = match[team]["Wickets"];
                    document.getElementById('InputOver').value = match[team]["Overs"];
                });
        });

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
        })
    }
}

function updatetextvalue(event) {
    var toUpdate;
    var team = document.getElementById('team_selector').value
    console.log(event.srcElement.value);
    console.log(event.srcElement.name);
    if (event.srcElement.name === "Runs") toUpdate = { Runs: event.srcElement.value };
    else if (event.srcElement.name === "Wickets") toUpdate = { Wickets: event.srcElement.value };
    else if (event.srcElement.name === "Overs") toUpdate = { Overs: event.srcElement.value };
    firebase.database().ref('/currentMatches/' + matchKey + '/' + team.toString()).update(toUpdate);
}

function UpdateSessionsTable(sessions) {
    var sessionsTable = document.getElementById('Sessions');
    if (sessions) {
        console.log(sessions);
        if (typeof (sessions) === "object") {
            sessions = Object.keys(sessions).filter(function (key) { return sessions[key]['active']}).map(function (key) {
                Object.assign(sessions[key], { key: key });
                return [sessions[key]["key"], sessions[key]["name"], sessions[key]["suspended"], sessions[key]['manualLocked']];
            });
        } else if (typeof (sessions) === "Array") {
            sessions.filter(function (session) { return session['active'] }).map(function (session) {
                return [session["id"], session["name"], session["suspended"], session['manualLocked']]
            })
        }
        console.log(sessions);
        clearSessionsTable();
        for (i = 0; i < sessions.length; i++) {
            var nameCell = document.createElement("span");
            nameCell.innerHTML = sessions[i][1];
            sessionsTable.appendChild(nameCell);
            var suspendedCell = sessionsTable.appendChild(document.createElement("span"));
            var suspendedButton = suspendedCell.appendChild(document.createElement("a"));
            suspendedButton.className = "btn";
            suspendedButton.style = (sessions[i][3]) ? "background-color:red;width:100%;height:100%;color:white;" : "background-color:green;width:100%;height:100%;color:white;";
            suspendedButton.innerText = (sessions[i][3]) ? "Locked" : "Not Locked";
            suspendedButton.setAttribute("sessionID", sessions[i][0]);
            suspendedButton.setAttribute("sessionValue", sessions[i][3]);
            suspendedButton.addEventListener("click", function (event) {
                console.log(event.srcElement.getAttribute("sessionValue").toString());
                firebase.database().ref('/currentMatches/' + matchKey + '/sessions/' + event.srcElement.getAttribute("sessionID").toString()).update({
                    manualLocked: (event.srcElement.getAttribute("sessionValue").toString() === "true") ? false : true
                });
            });
        }

    }
}

function clearSessionsTable() {
    document.getElementById('Sessions').innerHTML = "<span>Name</span><span>Suspended</span >";
}

function focusNextElementOnEnterKeyPress(event) {
    if (event.keyCode === 13) {
        var toUpdate;
        var team = document.getElementById('team_selector').value
        console.log(event.srcElement.value);
        console.log(event.srcElement.name);
        if (event.srcElement.name === "Runs") {
            toUpdate = { Runs: event.srcElement.value };
            document.getElementById('InputWicket').focus(); 
        }
        else if (event.srcElement.name === "Wickets") {
            toUpdate = { Wickets: event.srcElement.value };
            document.getElementById('InputOver').focus(); 
        }
        else if (event.srcElement.name === "Overs") {
            toUpdate = { Overs: event.srcElement.value };
            document.getElementById('InputRun').focus();    
        }
        firebase.database().ref('/currentMatches/' + matchKey + '/' + team.toString()).update(toUpdate);
    }
}

