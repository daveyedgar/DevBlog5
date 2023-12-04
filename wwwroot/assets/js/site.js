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
// **************************************************
// latest posts

var $allElements = $("#latestPosts").find("li").not($("div").has("ul"));
var curIndex = 0;


$("#scrollElDown").click(function () {
    //alert("current: " + curIndex + " of " + $allElements.length);
    let position = window.scrollY;
    if (curIndex >= $allElements.length - 4) {
        return;
    } else if (curIndex < $allElements.length - 1) {
        $(".selected").removeClass("selected");
        curIndex++;
        $allElements.eq(curIndex).addClass("selected");
        $(".selected").get(0).scrollIntoView({ behavior: 'smooth' });
        window.scrollTo(0, position + .965);
    }
});


$("#scrollElUp").click(function () {
    let position = window.scrollY;
    if (curIndex == 0) {
        return;
    } else {
        curIndex--;
        if (curIndex >= 0) {
            $(".selected").removeClass("selected");
            $allElements.eq(curIndex).addClass("selected");
            $(".selected").get(0).scrollIntoView({ behavior: 'smooth' });
            window.scrollTo(0, position + .965);
        }
    }
});

//function scrollPostDown() {
//    const div = document.getElementById("latestPosts");
//    let number = div.scrollTop;
//    div.scrollTo({
//        top: number + 74,
//        behavior: 'smooth'
//    });
//}


//function scrollPostUp() {
//    const div = document.getElementById("latestPosts");
//    let number = div.scrollTop;
//    div.scrollTo({
//        top: number - 80,
//        behavior: 'smooth'
//    });
//}

// latest comments
function scrollCommentDown() {
    const div = document.getElementById("latestComments");
    let number = div.scrollTop;
    div.scrollTo({
        top: number + 98,
        behavior: 'smooth'
    });
}


function scrollCommentUp() {
    const div = document.getElementById("latestComments");
    let number = div.scrollTop;
    div.scrollTo({
        top: number - 98,
        behavior: 'smooth'
    });
}



// latest categories
function scrollCategoryDown() {
    const div = document.getElementById("latestCategories");
    let number = div.scrollTop;
    div.scrollTo({
        top: number + 76,
        behavior: 'smooth'
    });
}


function scrollCategoryUp() {
    const div = document.getElementById("latestCategories");
    let number = div.scrollTop;
    div.scrollTo({
        top: number - 82,
        behavior: 'smooth'
    });
}

