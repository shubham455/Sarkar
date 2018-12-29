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
                document.getElementById("Team1").value = team_1.Name
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
                document.getElementById("Team2").value = team_2.Name
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
        .ref("/currentMatches/" + matchKey + "/team_c")
        .on(
            "value", // runs on change
            function (snapshot) {
                var team_c = snapshot.val();

                console.log(team_c);
                document.getElementById("Teamc").value = team_c.Name
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
                document.getElementById("LastBall").src = "/superagent/img/LastBall/" + lastBall.event.replace(" ", "_") + ".jpg";
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
        .ref("/currentMatches/" + matchKey + "/team_c/Runner")
        .on(
            "value", // runs on change
            function (snapshot) {
                var runner = snapshot.val();
                console.log("RUNNER tEAM");
                document.getElementById("KRate3").value = runner.Khai.toString();
                document.getElementById("LRate3").value = runner.Lagai.toString();
                console.log(KRate3, LRate3);
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

function updateSessionTable(sessions) {
    if (sessions) {
        if (typeof (sessions) === "object") {
            sessions = Object.keys(sessions).map(function (key) {
                Object.assign(sessions[key], { key: key });
                return sessions[key];
            });
        }
        console.log(sessions);
        var displayableSessions = sessions.filter(function (session) { return session['active'] });
        clearSessionTable();
        for (var i = 1; i <= displayableSessions.length; i++) {
            var session = displayableSessions[i - 1];
            console.log(session);
            document.getElementById("Session" + i).value = session['name'];

            document.getElementById("not" + i).value =
                session["not"] !== "" ? session["not"] : "0.00";
            document.getElementById("yes" + i).value =
                session["yes"] !== "" ? session["yes"] : "0.00";
            document.getElementById("notrate" + i).value =
                session["notRate"] !== "" ? session["notRate"] : "0.00";
            document.getElementById("yesrate" + i).value =
                session["yesRate"] !== "" ? session["yesRate"] : "0.00";
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


