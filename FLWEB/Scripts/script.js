
jQuery(document).ready(function () {
    jQuery(window).scroll(function () {
        if (jQuery(this).scrollTop() > 100) {
            jQuery('.scrollup').fadeIn();
        } else {
            jQuery('.scrollup').fadeOut();
        }



    });
    // scroll-to-top animate
    jQuery('.scrollup').click(function () {
        jQuery("html, body").animate({ scrollTop: 0 }, 600);
        return false;
    });
});

$(document).ready(function () {
    
    $('span.togglelink').on('click', function (e) {
       
        if ($(this).html() == '- Hide') {
            $(this).html('+ View');
        }

        else {
            $(this).html('- Hide');
        }
        //$(this).html($(this).html() == '+ View' ? '+ View' : '- Hide');
        e.preventDefault();
        var elem = $(this).parent(".short-cut").next('.toggle');
        if (elem.is(":visible"))
            elem.hide();
        else
            elem.show();
    });
});

$(document).ready(function () {

    $('span.togglelink-itinerary').on('click', function (e) {

        $(this).html($(this).html() == '<span class="glyphicon glyphicon-minus"></span> Itinerary' ? '<span class="glyphicon glyphicon-plus"></span> Itinerary' : '<span class="glyphicon glyphicon-minus"></span> Itinerary');

        var color = $(this).parent(".short-cut-list").parent('.row').parent('.box');
        if ($(this).html() == '<span class="glyphicon glyphicon-minus"></span> Itinerary' && $(this).next('span.togglelink-fare-details').html() == '<span class=" glyphicon glyphicon-plus"></span> Fare Details and Rules') {
            color.removeClass('bl-blue');
        }
        else {
            color.addClass('bl-blue');

        }
        e.preventDefault();
        var elem = $(this).parent(".short-cut-list").next('.toggle-itinerary');

        if (elem.is(":visible"))

            elem.hide();
        else
            elem.show();
    });
});

$(document).ready(function () {
    $('span.togglelink-fare-details').on('click', function (e) {


        $(this).html($(this).html() == '<span class=" glyphicon glyphicon-minus"></span> Fare Details and Rules' ? '<span class=" glyphicon glyphicon-plus"></span> Fare Details and Rules' : '<span class=" glyphicon glyphicon-minus"></span> Fare Details and Rules');

        var color = $(this).parent(".short-cut-list").parent('.row').parent('.box');
        if ($(this).html() == '<span class=" glyphicon glyphicon-minus"></span> Fare Details and Rules' && $(this).prev('span.togglelink-itinerary').html() == '<span class="glyphicon glyphicon-minus"></span> Itinerary') {
            color.removeClass('bl-blue');
        }
        else {
            color.addClass('bl-blue');

        }

        e.preventDefault();
        var elem2 = $(this).parent(".short-cut-list").next('.toggle-itinerary').next('.toggle-fare-details');

        if (elem2.is(":visible"))

            elem2.hide();
        else
            elem2.show();

    });
});

$(document).ready(function () {
    $('span.c-it').on('click', function (e) {
        var elem3 = $(this).parent(".col-xs-12").parent('.row').parent('.toggle-itinerary');
        var elem4 = $(this).parent(".col-xs-12").parent('.row').parent('.toggle-itinerary').prev('.short-cut-list').children('.togglelink-itinerary');

        elem4.html(elem4.html() == '<span class="glyphicon glyphicon-plus"></span> Itinerary' ? '<span class="glyphicon glyphicon-minus"></span> Itinerary' : '<span class="glyphicon glyphicon-plus"></span> Itinerary');

        var color = $(this).parent(".col-xs-12").parent('.row').parent('.toggle-itinerary').prev(".short-cut-list").parent('.row').parent('.box');

        if ($(this).parent(".col-xs-12").parent('.row').parent('.toggle-itinerary').prev('.short-cut-list').children('span.togglelink-itinerary').html() == '<span class="glyphicon glyphicon-plus"></span> Itinerary' && $(this).parent(".col-xs-12").parent('.row').parent('.toggle-itinerary').prev('.short-cut-list').children('span.togglelink-fare-details').html() == '<span class=" glyphicon glyphicon-plus"></span> Fare Details and Rules') {
            color.removeClass('bl-blue');
        }
        else {
            color.addClass('bl-blue');

        }

        if (elem3.is(":visible"))
            elem3.hide();
        else
            elem3.show();
    });
});


$(document).ready(function () {
    $('span.c-fd').on('click', function (e) {

        var elem5 = $(this).parent(".col-xs-12").parent('.row').parent('.toggle-fare-details');
        var elem6 = $(this).parent(".col-xs-12").parent('.row').parent('.toggle-fare-details').prev('.toggle-itinerary').prev('.short-cut-list').children('.togglelink-fare-details');
        elem6.html(elem6.html() == '<span class=" glyphicon glyphicon-plus"></span> Fare Details and Rules' ? '<span class=" glyphicon glyphicon-minus"></span> Fare Details and Rules' : '<span class=" glyphicon glyphicon-plus"></span> Fare Details and Rules');

        var color = $(this).parent(".col-xs-12").parent('.row').parent('.toggle-fare-details').prev('.toggle-itinerary').prev(".short-cut-list").parent('.row').parent('.box');

        if ($(this).parent(".col-xs-12").parent('.row').parent('.toggle-fare-details').prev('.toggle-itinerary').prev('.short-cut-list').children('span.togglelink-itinerary').html() == '<span class="glyphicon glyphicon-plus"></span> Itinerary' && $(this).parent(".col-xs-12").parent('.row').parent('.toggle-fare-details').prev('.toggle-itinerary').prev('.short-cut-list').children('span.togglelink-fare-details').html() == '<span class=" glyphicon glyphicon-plus"></span> Fare Details and Rules') {
            color.removeClass('bl-blue');
        }
        else {
            color.addClass('bl-blue');

        }

        if (elem5.is(":visible"))
            elem5.hide();
        else
            elem5.show();
    });
});
