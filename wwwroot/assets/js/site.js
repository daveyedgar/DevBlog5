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
    phone = '77' + '2-' + '494-1' + '018';
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
//              BUTTON VISITED CLASS
//
//      overrides the button visited style
//      there is no pseudo :visited class for buttons
//
// **************************************************


var btnActions = document.getElementsByClassName("btn-actions");

var myFunction = function () {
    //var attribute = this.getAttribute("data-myattribute");
    //alert(attribute);
    this.style.color = 'white';
};

for (var i = 0; i < btnActions.length; i++) {
    if (btnActions[i].tagName === "BUTTON") {
        btnActions[i].addEventListener('click', myFunction, false);
    }
}

// **************************************************
//
//
//          VERTICAL CAROUSEL SLIDER FOR RECENT POSTS/COMMENTS/CATEGORIES
//
//
//
// **************************************************
// latest posts
function scrollPostDown(){
    const div = document.getElementById("latestPosts");
    let number = div.scrollTop;
    div.scrollTo({
        top: number + 80,
        behavior: 'smooth'
    });
}


function scrollPostUp(){
    const div = document.getElementById("latestPosts");
    let number = div.scrollTop;
    div.scrollTo({
        top: number - 80,
        behavior: 'smooth'
    });
}

// latest comments
function scrollCommentDown() {
    const div = document.getElementById("latestComments");
    let number = div.scrollTop;
    div.scrollTo({
        top: number + 80,
        behavior: 'smooth'
    });
}


function scrollCommentUp() {
    const div = document.getElementById("latestComments");
    let number = div.scrollTop;
    div.scrollTo({
        top: number - 80,
        behavior: 'smooth'
    });
}



// latest categories
function scrollCategoryDown() {
    const div = document.getElementById("latestCategories");
    let number = div.scrollTop;
    div.scrollTo({
        top: number + 80,
        behavior: 'smooth'
    });
}


function scrollCategoryUp() {
    const div = document.getElementById("latestCategories");
    let number = div.scrollTop;
    div.scrollTo({
        top: number - 80,
        behavior: 'smooth'
    });
}

