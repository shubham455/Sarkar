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
        }).then(() => {
            firebase.database().ref('/currentMatches/' + matchKey).on("value", // runs on page runder
                function (snapshot) {
                    var match = snapshot.val();
                    document.getElementById('minBet').value = match["minBet"];
                    document.getElementById('maxBet').value = match["maxBet"];
                    document.getElementById('sessionMinBet').value = match["sessionMinBet"];
                    document.getElementById('sessionMaxBet').value = match["sessionMaxBet"];
                    document.getElementById('fancyMinBet').value = match["fancyMinBet"]
                    document.getElementById('fancyMaxBet').value = match["fancyMaxBet"];
                });
            document.getElementById('btnminmaxUpdate').addEventListener("click", (event) => {

                var minBet = document.getElementById('minBet').value;
                var maxBet = document.getElementById('maxBet').value;
                firebase.database().ref('/currentMatches/' + matchKey).update({
                    minBet: minBet,
                    maxBet: maxBet
                }).then(function () {
                    console.log("Updated MaTch Min Max.");
                });
            });
            document.getElementById('btnsessionMinMaxUpdate').addEventListener("click", (event) => {

                var minBet = document.getElementById('sessionMinBet').value;
                var maxBet = document.getElementById('sessionMaxBet').value;
                firebase.database().ref('/currentMatches/' + matchKey).update({
                    sessionMinBet: minBet,
                    sessionMaxBet: maxBet
                }).then(function () {
                    console.log("Updating Session Rate.");
                });
            });
            document.getElementById('btnfancyMinMaxUpdate').addEventListener("click", (event) => {

                var minBet = document.getElementById('fancyMinBet').value;
                var maxBet = document.getElementById('fancyMaxBet').value;
                firebase.database().ref('/currentMatches/' + matchKey).update({
                    fancyMinBet: minBet,
                    fancyMaxBet: maxBet
                }).then(function () {
                    console.log("Updating Fancy Rate.");
                });
            });

        });
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
function fancyMinBetKeyPress(event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        document.getElementById('fancyMaxBet').focus();
    }
}
function fancyMaxBetKeyPress(event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        document.getElementById('btnfancyMinMaxUpdate').focus();
    }
}