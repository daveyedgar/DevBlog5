jQuery(document).ready(function() {
    // for hover dropdown menu
    $('ul.nav li.dropdown').hover(function() {
        $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeIn(200);
    }, function() {
        $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut(200);
    });
    // slick slider call 
    $('.slick_slider').slick({
        dots: true,
        infinite: true,
        speed: 800,
        slidesToShow: 1,
        slide: 'div',
        autoplay: true,
        autoplaySpeed: 5000,
        cssEase: 'linear'
    });


    // latest post slider call 
    $('.latest_postnav').newsTicker({
        row_height: 64,
        speed: 800,
        prevButton: $('#prev-button'),
        nextButton: $('#next-button')
    });

// jquery easy ticker for recent comments
    $(document).ready(function () {

        $('.spost_nav_container').easyTicker({
            direction: 'up',
            easing: 'swing',
            speed: 'slow',
            interval: 2000,
            height: '320px',
            visible: 0,
            mousePause: true,
            controls: {
                up: '',
                down: '',
                toggle: '',
                playText: 'Play',
                stopText: 'Stop'
            },
            callbacks: {
                before: false,
                after: false
            }
        });

    });
// end ticker recent comments
// jquery easy ticker for recent categories
    $(document).ready(function () {

        $('.category_nav_container').easyTicker({
            direction: 'up',
            easing: 'swing',
            speed: 2000,
            interval: 5000,
            height: '320px',
            visible: 0,
            mousePause: true,
            controls: {
                up: '',
                down: '',
                toggle: '',
                playText: 'Play',
                stopText: 'Stop'
            },
            callbacks: {
                before: false,
                after: false
            }
        });

    });
// end ticker recent categories

    jQuery(".fancybox-buttons").fancybox({
        prevEffect: 'none',
        nextEffect: 'none',
        closeBtn: true,
        helpers: {
            title: {
                type: 'inside'
            },
            buttons: {}
        }
    });
    // jQuery('a.gallery').colorbox();
    //Check to see if the window is top if not then display button
    $(window).scroll(function() {
        if ($(this).scrollTop() > 300) {
            $('.scrollToTop').fadeIn();
        } else {
            $('.scrollToTop').fadeOut();
        }
    });
    //Click event to scroll to top
    $('.scrollToTop').click(function() {
        $('html, body').animate({
            scrollTop: 0
        }, 800);
        return false;
    });
    $('.tootlip').tooltip();
    //$("ul#ticker01").liScroll();
});

wow = new WOW({
    animateClass: 'animated',
    offset: 100
});
wow.init();


// preloader
//jQuery(window).load(function() { // makes sure the whole site is loaded
//    $('#status').fadeOut(); // will first fade out the loading animation
//    $('#preloader').delay(100).fadeOut('slow'); // will fade out the white DIV that covers the website.
//    $('body').delay(100).css({
//        'overflow': 'visible'
//    });
//})

