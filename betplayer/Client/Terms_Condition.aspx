<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Terms_Condition.aspx.cs" Inherits="betplayer.Client.Terms_Condition" %>

<!DOCTYPE html>

<html>
<head>
    <title>Terms &amp; Conditions</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link href="css/login/style_v1.css" rel="stylesheet" type="text/css" media="all">
    <link href="css/login/clndr.css" rel="stylesheet" type="text/css" media="all">

    <style>
        /* The container */
        .container {
            display: block;
            position: relative;
            padding-left: 35px;
            margin-bottom: 12px;
            cursor: pointer;
            font-size: 14px;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            color: #E9E9E9;
        }

            /* Hide the browser's default checkbox */
            .container input {
                position: absolute;
                opacity: 0;
            }

        /* Create a custom checkbox */
        .checkmark {
            position: absolute;
            top: 0;
            left: 0;
            height: 25px;
            width: 25px;
            background-color: #eee;
        }

        /* On mouse-over, add a grey background color */
        .container:hover input ~ .checkmark {
            background-color: #ccc;
        }

        /* When the checkbox is checked, add a blue background */
        .container input:checked ~ .checkmark {
            background-color: #2196F3;
        }

        /* Create the checkmark/indicator (hidden when not checked) */
        .checkmark:after {
            content: "";
            position: absolute;
            display: none;
        }

        /* Show the checkmark when checked */
        .container input:checked ~ .checkmark:after {
            display: block;
        }

        /* Style the checkmark/indicator */
        .container .checkmark:after {
            left: 9px;
            top: 5px;
            width: 5px;
            height: 10px;
            border: solid white;
            border-width: 0 3px 3px 0;
            -webkit-transform: rotate(45deg);
            -ms-transform: rotate(45deg);
            transform: rotate(45deg);
        }
    </style>

  

</head>

<body>
    <form name="KhaiLagai" method="post" action="MainMenu.aspx">
        <br>

        <!---start-wrap---->
        <div class="wrap" style="margin-left: 10px;">

            <!----- Content ------------->
            <div class="content_top">
                <div class="content_top-grid3"></div>
                <div class="clear"></div>
            </div>
            <div class="skills">
                <h3 style="align-content:center">Terms &amp; Conditions</h3>
            </div>


            <div class="drag_drop">

                <br>
                <h4 align="left" style="color: #000; font-family: Verdana, Geneva, sans-serif; font-size: 12px;">1. लोगिन करने के बाद अपना पासवर्ड बदल लें। </h4>
                <br>
                <h4 align="left" style="color: #000; font-family: Verdana, Geneva, sans-serif; font-size: 12px;">2. प्रत्येक गेम के लिए 100/- Coins चार्ज रहेगा।</h4>
                <br>
                <h4 align="left" style="color: #000; font-family: Verdana, Geneva, sans-serif; font-size: 12px;">3. टेस्ट गेम में प्रतिदिन 50/- coins  चार्ज रहेगा।</h4>
                <br>
                <h4 align="left" style="color: #000; font-family: Verdana, Geneva, sans-serif; font-size: 12px;">4. वनडे और टी-20 के एडवांस सौदे मैच शुरू होने के आधे घंटे पहले से ही लिए जायेंगे। </h4>
                <br>
               <h4 align="left"  style="color:#000;   font-family:Verdana, Geneva, sans-serif;  font-size:12px;">5. टेस्ट मैच के एडवांस सौदे मैच शुरू होने के 45 मिनिट पहले से ही लिए जायेंगे। </h4>
                <br>
                <h4 align="left" style="color: #000; font-family: Verdana, Geneva, sans-serif; font-size: 12px;">6. गेम रद्द या टाई होने पर मैच के सभी सौदे रद्द माने जायेंगे और जो सेशन पुरे हो चुके हे, उनके हिसाब से ही Coins कम या ज्यादा होंगे  ।  </h4>
                <br>
                <h4 align="left" style="color: #000; font-family: Verdana, Geneva, sans-serif; font-size: 12px;">7. एक बार में मैच का सौदा कम से कम 500 Coins एवं ज्यादा से ज्यादा 1,00,000 Coins और सेशन का सौदा कम से कम 500 Coins एवं ज्यादा से ज्यादा 50,000 Coins तक का ही  लिया जायेगा। </h4>
                <br>
                <h4 align="left" style="color: #000; font-family: Verdana, Geneva, sans-serif; font-size: 12px;">8. मैच के दौरान भाव को देख व समझ के ही सौदा करे। किये गए किसी भी सौदे को हटाया या बदला नहीं जाएगा। सभी सौदे के लिए स्वयं आप ही जिम्मेदार होंगे।</h4>
                <br>
                <h4 align="left" style="color: #000; font-family: Verdana, Geneva, sans-serif; font-size: 12px;">9. मैच या सेशन भाव गलत चलने पर जो भी मैच या सेशन के सौदे हुए हे वह स्वतः हट जायेंगे।</h4>
                
                <br>

                <h4 align="left" style="color: #000; font-family: Verdana, Geneva, sans-serif; font-size: 12px;">नोट : सर्वर या वेबसाईट में किसी तरह की  खराबी आने या बंद हो जाने पर  केवल किये गए सौदे  ही मान्य होंगे। ऐसी स्थिति में  किसी भी तरह का वाद-विवाद मान्य नहीं होगा।</h4>
                <br>
            </div>
            <br>
            <br>
            <div class="block">
                <div class="content">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           
                    <label class="container" style="color:black">
                        मैंने सभी नियम व शर्तों को अच्छे से पढ़ लिया है और मुझे सभी नियम व शर्ते मंजूर है।
                             
                        <input type="checkbox" name="Terms" id="Terms">
                        <span class="checkmark"></span>
                    </label>

                </div>

            </div>
            <br>

            <div class="menu" id="menu" align="center">
                <ul class="nav">
                    <li class="active"><a href="MainMenu.aspx" style="font-size: 14px !important; background-color:#56baec; line-height: 25px;">NEXT =&gt; MAIN MENU</a></li>
                </ul>
            </div>

            <br>


            <div class="content_bottom">
                <div class="clear"></div>
            </div>
        </div>
        <br>
        <br>
        <br>
        <br>
        <br>


        <h4 align="center" style="color: #FFF; font-family: Verdana, Geneva, sans-serif; font-size: 12px;">Copy Right @ Khai Lagai</h4>

        <br>
        <br>

        <!----- End Content ------------->

        <!---//End-wrap---->
        <input type="hidden" name="TCChk">
    </form>



</body>
</html>
