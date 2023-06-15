// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function mail() {
    address = 'davi' + 'd@' + 'davidbe' + 'llerose.com';
    document.getElementsByClassName('showEmail')[0].title = address;
    navigator.clipboard.writeText(address);
    alert(address + "\n\nhas been copied to your clipboard");
}


function sendContact() {

    // email send script
    document.getElementById("contactForm").action = "https://formsubmit.co/" + address;
    // end email send script
    alert(`Your message has been sent.`);
}

function phoneNumber() {
    phone = '91' + '7-' + '497-7' + '553';
    document.getElementsByClassName('showPhone')[0].title = phone;
    alert(phone);
}


// **************************************************
//
//              BACK TO TOP BUTTON
//
//      move above footer when scrolled to bottom
//
//
// **************************************************



$(document).ready(function () {

    $(document).on('scroll', function () {
        var distanceFromBottom = $(document).height() - ($(document).scrollTop() + $(window).height());

        if (distanceFromBottom < 180) {
            $('#back-top').addClass("shift");
        } else {
            $('#back-top').removeClass("shift");
        }
    });
});



// **************************************************
//
//              COOKIE MODAL
//
// **************************************************
function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

// Setup GTM dataLayer -- load
window.dataLayer = window.dataLayer || [];

// see if cookie exists, if not show cookiebar -- recommended to do this in head of page
if (!getCookie("CookieConsent")) {

    document.getElementsByClassName("js-cookiebar")[0].style.display = "block";

    window.dataLayer = [{
        'event': 'NoCookieExist'
    }];
} else {
    var cookieValue =
        "{" + '"cookieValue"' + ":" + getCookie("CookieConsent") + "}";
    var jsonData = JSON.parse(cookieValue);
    pushCookieValueToDataLayer(jsonData.cookieValue, "CookieBarStatus", false);
}

// get Cookie by name - credits to stackoverflow
function getCookie(cname) {
    var name = cname + "=",
        decodedCookie = decodeURIComponent(document.cookie),
        ca = decodedCookie.split(";");

    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == " ") {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

// push cookieValue to dataLayer - set array, set eventName
function pushCookieValueToDataLayer(arr, eventName, boolean) {
    var x = arr.filter(function (v) {
        return v.value === true;
    });
    dataLayerType(x, eventName, boolean);
}

function dataLayerType(x, e, b) {
    if (b) {
        for (var i = 0; i < x.length; i++) {
            window.dataLayer.push(_defineProperty({}, x[i].name, x[i].value));
        }
        window.dataLayer.push({
            'event': e
        });
    } else {
        var dataInit = [];
        for (var i = 0; i < x.length; i++) {
            dataInit.push(_defineProperty({}, x[i].name, x[i].value));
        };
        dataInit.push({
            'event': e
        });
        window.dataLayer = dataInit;
    }
}

// set cookie on click this is the accept button in the modal
function onCookieButtonClick() {
    var basicValue = document.getElementById("basic_chkbx").checked,
        preferencesValue = document.getElementById("preferences_chkbx").checked,
        statisticsValue = document.getElementById("statistics_chkbx").checked,
        marketingValue = document.getElementById("marketing_chkbx").checked,
        options = [
            { name: "basic", value: basicValue },
            { name: "preferences", value: preferencesValue },
            { name: "statistics", value: statisticsValue },
            { name: "marketing", value: marketingValue }
        ],
        data = JSON.stringify(options),
        date = new Date();

    date.setTime(date.getTime() + 3650 * 24 * 60 * 60 * 1000);
    var expires = "expires=" + date.toUTCString();

    // set cookie
    document.cookie = "CookieConsent=" + data + ";" + expires + ";";
    pushCookieValueToDataLayer(options, "CookiePreferencesChange", true);

    document.getElementsByClassName("js-cookiebar")[0].style.display = "none";
}

// show cookiebar on click
var changeCookie = document.getElementById("open-cookie");

// Get cookievalue, set checkboxes to right value and show cookiebar
changeCookie.addEventListener(
    "click",
    function () {
        if (getCookie("CookieConsent")) {
            var cookieValue =
                "{" + '"cookieValue"' + ":" + getCookie("CookieConsent") + "}";
            var jsonData = JSON.parse(cookieValue);
            var x = jsonData.cookieValue.filter(function (v) {
                return v.value === true;
            });

            for (let i = 0; i < x.length; i++) {
                let y = x[i].name + "_chkbx";
                document.getElementById(y).checked = true;
            }
        }

        document.getElementsByClassName("js-cookiebar")[0].style.display = "block";
    },
    false
);
