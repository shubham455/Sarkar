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
var matchIdElement = document.getElementById("ContentPlaceHolder1_firebasekey");
var matchKey = matchIdElement.value;
if (matchIdElement !== null) {
    firebase
        .database()
        .ref("/currentMatches/" + matchKey + "/team_1")
        .on(
            "value", // runs on change
            function (snapshot) {
                var team_1 = snapshot.val();
                var asterix = team_1.Status === "Batting" ? " *" : "";
                console.log(team_1);
                document.getElementById("LocalTeam").innerHTML =
                    team_1.Name +
                    " " +
                    team_1.Runs +
                    "/" +
                    team_1.Wickets +
                    " (" +
                    team_1.Overs +
                    ")" +
                    asterix;
            }
        );
    firebase
        .database()
        .ref("/currentMatches/" + matchKey + "/team_2")
        .on(
            "value", // runs on change
            function (snapshot) {
                var team_2 = snapshot.val();
                var asterix = team_2.Status === "Batting" ? " *" : "";
                console.log(team_2);
                document.getElementById("VisitorTeam").innerHTML =
                    team_2.Name +
                    " " +
                    team_2.Runs +
                    "/" +
                    team_2.Wickets +
                    " (" +
                    team_2.Overs +
                    ")" +
                    asterix;
            }
        );
    firebase
        .database()
        .ref("/currentMatches/" + matchKey + "/lastBall")
        .on(
            "value", // runs on change
            function (snapshot) {
                var lastBall = snapshot.val();
                console.log(lastBall);
                document.getElementById("LastBall").innerHTML = lastBall.event;
            }
        );
    firebase
        .database()
        .ref("/currentMatches/" + matchKey + "/status")
        .on(
            "value", // runs on change
            function (snapshot) {
                var status = snapshot.val();
                document.getElementById("Status").innerHTML = status;
            }
        );
    firebase
        .database()
        .ref("/currentMatches/" + matchKey + "/message")
        .on(
            "value", // runs on change
            function (snapshot) { }
        );
    firebase
        .database()
        .ref("/currentMatches/" + matchKey + "/team_1/Runner")
        .on(
            "value", // runs on change
            function (snapshot) {
                var runner = snapshot.val();

                document.getElementById("KRate1").value = runner.Khai.toString();
                document.getElementById("LRate1").value = runner.Lagai.toString();
                console.log(KRate1, LRate1);
            }
        );
    firebase
        .database()
        .ref("/currentMatches/" + matchKey + "/team_2/Runner")
        .on(
            "value", // runs on change
            function (snapshot) {
                var runner = snapshot.val();
                console.log("RUNNER tEAM");
                document.getElementById("KRate2").value = runner.Khai.toString();
                document.getElementById("LRate2").value = runner.Lagai.toString();
                console.log(KRate2, LRate2);
            }
        );
    firebase
        .database()
        .ref("/currentMatches/" + matchKey + "/sessions")
        .on(
            "value", // runs on change
            function (snapshot) {
                updateSessionTable(snapshot.val());
            }
        );
}
var timmerRunning = false,
    doneClicked = false,
    timer;
var team, runnerSession, betType, betValue, betRate, betAmount, Session;
var seconds = 10;
function enableBetting(event, teamValue, runnerSessionValue, betTypeValue) {
    if (event.srcElement.value === "0.00") {
        alert("No Rate Found.");
    } else if (!timmerRunning) {
        seconds = 10;
        team = teamValue;
        runnerSession = runnerSessionValue;
        betType = betTypeValue;
        betValue = event.srcElement.value;
        var elementId = event.srcElement.id;
        if (runnerSessionValue === "Session") {
            betRate = document.getElementById(
                elementId.substring(0, event.srcElement.id.length - 1) +
                "rate" +
                elementId.charAt(event.srcElement.id.length - 1)
            ).value;
            Session = document.getElementById(
                "Session" + elementId.charAt(event.srcElement.id.length - 1)
            ).value;
        }
        document.getElementById("matchAmount").disabled = false;
        document.getElementById("timerLabel").innerHTML = seconds;
        document.getElementById("timerLabel").style.visibility = "visible";
        matchAmountInput = document.getElementById("matchAmount");
        matchAmountInput.value = "";
        matchAmountInput.focus();
        timer = setInterval(function () {
            timmerRunning = true;
            if (seconds === 0) clearTimer();
            if (seconds === 8)
                document.getElementById("btnSubmit").style.visibility = "visible";
            document.getElementById("timerLabel").innerHTML = seconds;
            seconds--;
        }, 1000);
    }
}

function updateSessionTable(sessions) {
    if (sessions) {
        console.log(sessions);
        var displayableSessions = sessions.filter(function (session) { return session['active'] });
        clearSessionTable();
        for (var i = 1; i <= displayableSessions.length; i++) {
            var session = displayableSessions[i - 1];
            console.log(session);
            document.getElementById("Session" + i).value = session['name'];
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

function clearSessionTable() {
    for (var i = 1; i <= 4; i++) {
        document.getElementById("Session" + i).value = "NONE";
        document.getElementById("not" + i).value = "0.00";
        document.getElementById("yes" + i).value = "0.00";
        document.getElementById("notrate" + i).value = "0.00";
        document.getElementById("yesrate" + i).value = "0.00";
    }
}

function doneClick() {
    if (!doneClicked) {
        console.log(team + " " + runnerSession + " " + betType);
        if (runnerSession === "Runner") {
            if (!matchKey) alert("No FireBase Match.");
            else {
                doneClicked = true;
                firebase
                    .database()
                    .ref("/currentMatches/" + matchKey)
                    .once(
                        "value", // runs on change
                        function (snapshot) {
                            var match = snapshot.val();
                            console.log(betValue);
                            betAmount = parseInt(
                                document.getElementById("matchAmount").value
                            );
                            if (betValue !== match[team][runnerSession][betType]) {
                                alert("Bet Not Accepted Because Rate Not Found");
                                clearTimer();
                            } else if (2000 >= betAmount || betAmount >= 100000) {
                                alert(
                                    "The Amount Should not be less than 2000 or greater than 1 Lakh."
                                );
                                clearTimer();
                            } else {
                                console.log(snapshot.val());
                                updatePosition(betValue, betAmount, team, betType);
                                var TeamName = match[team]["Name"];
                                if (betType === "Khai") {
                                    Mode = "K";
                                } else if (betType === "Lagai") {
                                    Mode = "L";
                                }
                                var params = {
                                    Amount: betAmount,
                                    Rate: betValue,
                                    Mode: Mode,
                                    Team: TeamName,
                                    MatchID: matchIdElement.value,

                                    Team1Position: document.getElementById(
                                        "ContentPlaceHolder1_PositionTeam1"
                                    ).innerHTML,
                                    Team2Position: document.getElementById(
                                        "ContentPlaceHolder1_PositionTeam2"
                                    ).innerHTML
                                };
                                var formBody = [];
                                for (var property in params) {
                                    var encodedKey = encodeURIComponent(property);
                                    var encodedValue = encodeURIComponent(params[property]);
                                    formBody.push(encodedKey + "=" + encodedValue);
                                }
                                formBody = formBody.join("&");

                                fetch("/Client/AddDataToRunner.ashx", {
                                    credentials: "same-origin",
                                    method: "POST",
                                    headers: {
                                        "Content-Type":
                                            "application/x-www-form-urlencoded;charset=UTF-8"
                                    },
                                    body: formBody
                                })
                                    .then(function (responce) {
                                        return responce.json();
                                    })
                                    .then(function (data) {
                                        console.log(data);
                                        if (data.status === "success") {
                                            console.log(betValue, betAmount, team, betType);
                                            alert("Bet Made Successfully");
                                            location.reload();
                                        } else if (data.status === "unsuccess") {
                                            alert("You Do not have Coins For this Bet");
                                            location.reload();
                                        } else alert("Bet Rejected!!!");
                                    })
                                    .then(function () {
                                        clearTimer();
                                    })
                                    .catch(function (err) {
                                        console.log(err);
                                    });
                            }
                        }
                    );
            }
        } else if (runnerSession === "Session") {
            if (!matchKey) alert("No FireBase Match.");
            else {
                doneClicked = true;
                firebase
                    .database()
                    .ref("/currentMatches/" + matchKey)
                    .once(
                        "value", // runs on change
                        function (snapshot) {
                            var match = snapshot.val();
                            console.log(betValue);
                            betAmount = parseInt(
                                document.getElementById("matchAmount").value
                            );
                            if (500 >= betAmount || betAmount >= 100000) {
                                alert(
                                    "The Amount Should not be less than 500 or greater than 1 Lakh."
                                );
                                clearTimer();
                            } else {
                                console.log(snapshot.val());
                                updatePosition(betValue, betAmount, team, betType);
                                var TeamName = match[team]["Name"];
                                if (betType === "Not") {
                                    Mode = "N";
                                } else if (betType === "Yes") {
                                    Mode = "Y";
                                }
                                var params = {
                                    Session: Session,
                                    Amount: betAmount,
                                    Run: betValue,
                                    Rate: betRate,
                                    Mode: Mode,
                                    Team: TeamName,
                                    MatchID: matchIdElement.value
                                };
                                var formBody = [];
                                for (var property in params) {
                                    var encodedKey = encodeURIComponent(property);
                                    var encodedValue = encodeURIComponent(params[property]);
                                    formBody.push(encodedKey + "=" + encodedValue);
                                }
                                formBody = formBody.join("&");

                                fetch("/Client/AddDataToSession.ashx", {
                                    credentials: "same-origin",
                                    method: "POST",
                                    headers: {
                                        "Content-Type":
                                            "application/x-www-form-urlencoded;charset=UTF-8"
                                    },
                                    body: formBody
                                })
                                    .then(function (responce) {
                                        return responce.json();
                                    })
                                    .then(function (data) {
                                        console.log(data);
                                        if (data.status === "success") {
                                            console.log(betValue, betAmount, team, betType);
                                            alert("Bet Made Successfully");
                                            location.reload();
                                        } else if (data.status === "unsuccess") {
                                            alert("You Do not have Coins For this Bet");
                                        } else alert("Bet Rejected!!!");
                                    })
                                    .then(function () {
                                        clearTimer();
                                    })
                                    .catch(function (err) {
                                        console.log(err);
                                    });
                            }
                        }
                    );
            }
        }
    }
}

function clearTimer() {
    timmerRunning = false;
    doneClicked = false;
    clearInterval(timer);
    document.getElementById("btnSubmit").style.visibility = "hidden";
    document.getElementById("matchAmount").disabled = true;
    document.getElementById("matchAmount").value = "0.00";
    document.getElementById("timerLabel").style.visibility = "hidden";
}

function updatePosition(Bet, Amount, Team, Type) {
    var bet = parseFloat(Bet);
    var amount = parseInt(Amount);
    Team1Position = document.getElementById("ContentPlaceHolder1_PositionTeam1");
    Team2Position = document.getElementById("ContentPlaceHolder1_PositionTeam2");

    Team1PositionValue =
        Team1Position.innerHTML === "" ? 0 : parseInt(Team1Position.innerHTML);
    Team2PositionValue =
        Team2Position.innerHTML === "" ? 0 : parseInt(Team2Position.innerHTML);
    console.log(Team1PositionValue, Team2PositionValue, bet, amount);
    if (Team === "team_1" && Type === "Lagai") {
        Team1Position.innerHTML = Team1PositionValue + bet * amount;
        Team2Position.innerHTML = Team2PositionValue + -1 * amount;
    } else if (Team === "team_1" && Type === "Khai") {
        Team1Position.innerHTML = Team1PositionValue - bet * amount;
        Team2Position.innerHTML = Team2PositionValue - -1 * amount;
    } else if (Team === "team_2" && Type === "Lagai") {
        Team1Position.innerHTML = Team1PositionValue + -1 * amount;
        Team2Position.innerHTML = Team2PositionValue + bet * amount;
    } else if (Team === "team_2" && Type === "Khai") {
        Team1Position.innerHTML = Team1PositionValue - -1 * amount;
        Team2Position.innerHTML = Team2PositionValue - bet * amount;
    }
}
