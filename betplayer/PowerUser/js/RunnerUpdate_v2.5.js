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
var timer = setTimeout(function () { }, 0);
if (matchIdElement !== null) {
    firebase.database().ref('/currentMatches/' + matchKey).once("value", // runs on page runder
        function (snapshot) {
            var match = snapshot.val();
            document.getElementById('team1_name').text = match['team_1']['Name'];
            document.getElementById('team2_name').text = match['team_2']['Name'];
            document.getElementById('team1_name1').text = match['team_1']['Name'];
            document.getElementById('team2_name2').text = match['team_2']['Name'];
        }).then(() => {
            firebase.database().ref('/currentMatches/' + matchKey).on("value", // runs on page runder
                function (snapshot) {
                    var match = snapshot.val();

                })
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
                }).then(function () {
                    document.getElementById("team_Lagai").focus();
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
                }).then(function () {
                    document.getElementById("team_Lagai").focus();
                });
            });


            document.getElementById('btnclearrate').addEventListener("click", (event) => {

                firebase.database().ref('/currentMatches/' + matchKey + '/team_1').update({
                    RunnerUnLocked: false
                }).then(function () {
                    firebase.database().ref('/currentMatches/' + matchKey + '/team_2').update({
                        RunnerUnLocked: false
                    }).then(function () {
                        console.log("In Lock.");
                    });
                });
                firebase.database().ref('/currentMatches/' + matchKey + '/sessions').once("value", function (snapshot) {
                    var currentsessions = snapshot.val();
                    if (typeof (currentsessions) === "object") {
                        Object.keys(currentsessions).forEach(function (key) {
                            if (currentsessions[key]["fancy"] === false)
                                currentsessions[key]["manualLocked"] = true;
                        });
                    } else if (typeof (currentsessions) === "Array") {
                        currentsessions.forEach(function (session) {
                            if (session['manualLocked'] === false)
                                currentsessions["fancy"] = true;
                        });
                    }
                    firebase.database().ref('/currentMatches/' + matchKey).update({ sessions: currentsessions });
                });

                firebase.database().ref('/currentMatches/' + matchKey + '/sessions').once("value", function (snapshot) {
                    var currentsessions = snapshot.val();
                    if (typeof (currentsessions) === "object") {
                        Object.keys(currentsessions).forEach(function (key) {
                            currentsessions[key]["manualLocked"] = true;
                        });
                    } else if (typeof (currentsessions) === "Array") {
                        currentsessions.forEach(function (session) {
                            session['manualLocked'] = true;
                        });
                    }
                    firebase.database().ref('/currentMatches/' + matchKey).update({ sessions: currentsessions });
                });

            });
            document.getElementById('btnUnclearrate').addEventListener("click", (event) => {

                firebase.database().ref('/currentMatches/' + matchKey + '/team_1').update({
                    RunnerUnLocked: true
                }).then(function () {
                    firebase.database().ref('/currentMatches/' + matchKey + '/team_2').update({
                        RunnerUnLocked: true
                    }).then(function () {
                        console.log("In Lock.");
                    });
                });
                firebase.database().ref('/currentMatches/' + matchKey + '/sessions').once("value", function (snapshot) {
                    var currentsessions = snapshot.val();
                    if (typeof (currentsessions) === "object") {
                        Object.keys(currentsessions).forEach(function (key) {
                            currentsessions[key]["manualLocked"] = false;
                        });
                    } else if (typeof (currentsessions) === "Array") {
                        currentsessions.forEach(function (session) {
                            session['manualLocked'] === false;
                        });
                    }
                    firebase.database().ref('/currentMatches/' + matchKey).update({ sessions: currentsessions });
                });

            });

            document.getElementById('btnteamupdate').addEventListener("click", (event) => {
                var team = document.getElementById('team_selector').value;
                var KhaiElement = document.getElementById('team_Khai');
                var LagaiElement = document.getElementById('team_Lagai');
                var Khai = parseFloat(KhaiElement.value);
                var Lagai = parseFloat(LagaiElement.value);
                console.log(Lagai < Khai);
                if (Lagai < Khai) {
                    Khai = window.Money.div(Khai.toString().concat(".00"), "100.00");
                    Lagai = window.Money.div(Lagai.toString().concat(".00"), "100.00");
                    console.log(team, Khai, Lagai);
                    firebase.database().ref('/currentMatches/' + matchKey + '/' + team.toString()).update({
                        Runner: {
                            Khai: Khai.toString(),
                            Lagai: Lagai.toString()
                        }
                    }).then(function () {
                        document.getElementById('team_Lagai').value = "";
                        document.getElementById('team_Khai').value = "";
                        document.getElementById('team_Lagai').focus();
                    });

                } else alert("Lagai is greater than Khai.");
            });
            document.getElementById('btnLock').addEventListener("click", (event) => {


                firebase.database().ref('/currentMatches/' + matchKey + '/team_1').update({
                    RunnerUnLocked: false
                }).then(function () {
                    firebase.database().ref('/currentMatches/' + matchKey + '/team_2').update({
                        RunnerUnLocked: false
                    }).then(function () {
                        console.log("In Lock.");
                    });
                });
            });
            document.getElementById('btnUnLock').addEventListener("click", (event) => {

                firebase.database().ref('/currentMatches/' + matchKey + '/team_1').update({
                    RunnerUnLocked: true
                }).then(function () {
                    firebase.database().ref('/currentMatches/' + matchKey + '/team_2').update({
                        RunnerUnLocked: true
                    }).then(function () {
                        console.log("In UnLock.");
                    });
                });
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
        }).then(function () {
            document.getElementById("team_Lagai").focus();
        });
    }
}



function lagaiKeyPress(event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        document.getElementById('team_Khai').focus();
    }
}
function khaiKeyPress(event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        document.getElementById('btnteamupdate').focus();
    }
}
function minBetKeyPress(event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        document.getElementById('maxBet').focus();
    }
}
function maxBetKeyPress(event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        document.getElementById('btnminmaxUpdate').focus();
    }
}

function sessionMinBetKeyPress(event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        document.getElementById('sessionMaxBet').focus();
    }
}
function sessionMaxBetKeyPress(event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        document.getElementById('btnsessionMinMaxUpdate').focus();
    }
}