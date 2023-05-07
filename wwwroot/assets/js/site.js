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

// this takes the url route id from the BlogPostIndex url, which is created by the home/index link to the blog
// and puts it in the windows storage

function getId() {
    var myString = window.location;
    let blogId = /(?<=\/)[^\/?]*(?=[^\/]*$)/.exec(myString)[0];
    //alert(blogId);
    localStorage.setItem("blogId", blogId);
}

// this sets the BlogId input to the windows storage, which is the BlogId that was clicked on
// this needs to be passed into the create view to auto set the blogId it belongs to
function passId() {
    let blogId = localStorage.getItem("blogId");
    let blogIdInput = document.getElementById("BlogId");
    blogIdInput.value = blogId;
}

// this sets the route id of the create link in the PostsEmpty view
// so the getId() function picks it up from the url

function setRouteId() {
    const createLink = document.getElementById("createRoute");
    let blogId = localStorage.getItem("blogId");
    //alert(blogId);
    createLink.setAttribute("asp-route-id", blogId);
}