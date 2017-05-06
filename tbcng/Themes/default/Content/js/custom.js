$(document).ready(function () {
    "use strict";
    //$.ajax({
    //    url: "/Home/LoadMenu",
    //    cache: false
    //}).done(function (html) {
    //    $("#menu > ul").html("" + html);
    //});

    //$.ajax({
    //    url: "/Home/LoadMenuMobile",
    //    cache: false
    //}).done(function (html) {
    //    $("#kode-responsive-navigation > ul").html("" + html);
    //});

    /*
	  ==============================================================
		   Banner Bx-Slider Script Start
	  ============================================================== */
    if ($('.banner_bxslider').length) {
        $('.banner_bxslider').bxSlider({
            auto: ($(".banner_bxslider li").length > 1) ? true : false,
            pager: ($(".banner_bxslider li").length > 1) ? true : false,
            pause: 5000
        });
    }

    /* ---------------------------------------------------------------------- */
    /*	DL Responsive Menu
	/* ---------------------------------------------------------------------- */
    if (typeof ($.fn.dlmenu) == 'function') {
        $('#kode-responsive-navigation').each(function () {
            $(this).find('.dl-submenu').each(function () {
                if ($(this).siblings('a').attr('href') && $(this).siblings('a').attr('href') != '#') {
                    var parent_nav = $('<li class="menu-item kode-parent-menu"></li>');
                    parent_nav.append($(this).siblings('a').clone());

                    $(this).prepend(parent_nav);
                }
            });
            $(this).dlmenu();
        });
    }

    /*
	  ==============================================================
		   Tab View Script Start
	  ============================================================== */

    if ($('#tabs').length) {
        $('#tabs').tab();
    }


    /* ==================================================================
					Number Count Up(WayPoints) Script
	  =================================================================	*/
    if ($('.counter').length) {
        $('.counter').counterUp({
            delay: 10,
            time: 1000
        });
    }

    /*
	  ==============================================================
		   Post Bx-Slider Script Start
	  ============================================================== */
    if ($('.countdown').length) {
        $('.countdown').downCount({ date: '08/08/2016 12:00:00', offset: +1 });
    }


    /*
	  ==============================================================
		   Recent Property and Our Agent Owl Carousel Script Start
	  ============================================================== */
    if ($('.kf_rent_property,.kf_agent_slider').length) {
        var owl = $(".kf_rent_property,.kf_agent_slider");
        owl.owlCarousel({
            autoPlay: 3000, //Set AutoPlay to 3 seconds
            itemsCustom: [
              [0, 1],
              [450, 1],
              [600, 1],
              [700, 2],
              [1000, 3],
              [1200, 4],
              [1400, 4],
              [1600, 4]
            ],
            navigation: true

        });
    }

    /*
	  ==============================================================
		   Latest Blog Post Owl Carousel Script Start
	  ============================================================== */
    if ($('.kf_blog_slider').length) {
        var owl = $(".kf_blog_slider");
        owl.owlCarousel({
            autoPlay: 3000, //Set AutoPlay to 3 seconds
            itemsCustom: [
              [0, 1],
              [450, 1],
              [600, 1],
              [700, 2],
              [1000, 3],
              [1200, 3],
              [1400, 3],
              [1600, 3]
            ],
            navigation: true

        });
    }

    /*
	  ==============================================================
		   Company List Owl Carousel Script Start
	  ============================================================== */
    if ($('.kf_company_slider').length) {
        var owl = $(".kf_company_slider");
        owl.owlCarousel({
            autoPlay: 3000, //Set AutoPlay to 3 seconds
            itemsCustom: [
              [0, 1],
              [450, 1],
              [600, 2],
              [700, 2],
              [1000, 3],
              [1200, 5],
              [1400, 5],
              [1600, 5]
            ],
            navigation: true

        });
    }

    /*
	  ==============================================================
		   Accordion Script Start
	  ============================================================== */
    if ($('.accordion').length) {
        //custom animation for open/close
        $.fn.slideFadeToggle = function (speed, easing, callback) {
            return this.animate({ opacity: 'toggle', height: 'toggle' }, speed, easing, callback);
        };

        $('.accordion').accordion({
            defaultOpen: 'section1',
            cookieName: 'nav',
            speed: 'slow',
            animateOpen: function (elem, opts) { //replace the standard slideUp with custom function
                elem.next().stop(true, true).slideFadeToggle(opts.speed);
            },
            animateClose: function (elem, opts) { //replace the standard slideDown with custom function
                elem.next().stop(true, true).slideFadeToggle(opts.speed);
            }
        });
    }

    /*
	  ==============================================================
		   Client Review Owl Carousel Script Start
	  ============================================================== */
    if ($('.kf_client_review_slider').length) {
        var owl = $(".kf_client_review_slider");
        owl.owlCarousel({
            autoPlay: 3000, //Set AutoPlay to 3 seconds
            itemsCustom: [
              [0, 1],
              [450, 1],
              [600, 2],
              [700, 2],
              [1000, 2],
              [1200, 2],
              [1400, 2],
              [1600, 2]
            ],
            navigation: true

        });
    }

    /*
	  ==============================================================
		   Twitter Bx-Slider Script Start
	  ============================================================== */
    if ($('.kf_twitter_slider,.kf_testimonial_slider').length) {
        $('.kf_twitter_slider,.kf_testimonial_slider').bxSlider({
            auto: true,
        });
    }

    /*
	  =======================================================================
		  		 Pretty Photo Script
	  =======================================================================
	*/
    if ($("a[data-rel^='prettyPhoto']").length) {
        $("a[data-rel^='prettyPhoto']").prettyPhoto();
    }

    /*
	  =======================================================================
		  		 Chosen Script Script
	  =======================================================================
	*/
    if ($(".chosen-select").length) {
        $(".chosen-select").chosen({
            no_results_text: "Không tìm thấy kết quả nào.",
            width: 'auto',
            allow_single_deselect: true
        })
    }


    var config = {
        '.chosen_select_width200': { no_results_text: "Không tìm thấy kết quả!", width: '200px', allow_single_deselect: true },
        '.chosen-select-deselect': { allow_single_deselect: true, no_results_text: "Không tìm thấy kết quả!" },
        '.chosen-select-no-single': { disable_search_threshold: 10 },
        '.chosen_select_no_results': { no_results_text: 'Không tìm thấy kết quả!', width: 'auto', allow_single_deselect: true },
        '.chosen-select-width': { width: "95%" }
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }


    /*
	  =======================================================================
		  	Listing Bx-Slider Pager Script Script
	  =======================================================================
	*/
    if ($('.kf_listing_slider').length) {
        $('.kf_listing_slider').bxSlider({
            pagerCustom: '.kf_blog_listing_pager',
            auto: true,
        });
    }

    /*
	  =======================================================================
		  	Listing Bx-Slider Pager Script Script
	  =======================================================================
	*/
    if ($('.property_pager_item').length) {
        $('.property_pager_item').bxSlider({
            pagerCustom: '#property_detail_pager',
            auto: true,
        });
    }

    /*
	  =======================================================================
		  		Filter Able Script Script
	  =======================================================================
	*/

    jQuery(window).load(function ($) {
        if (jQuery('#filterable-item-holder-1').length) {
            var filter_container = jQuery('#filterable-item-holder-1');

            filter_container.children().css('position', 'relative');
            filter_container.masonry({
                singleMode: true,
                itemSelector: '.filterable-item:not(.hide)',
                animate: true,
                animationOptions: { duration: 800, queue: false }
            });
            jQuery(window).resize(function () {
                var temp_width = filter_container.children().filter(':first')();
                filter_container.masonry({
                    columnWidth: temp_width,
                    singleMode: true,
                    itemSelector: '.filterable-item:not(.hide)',
                    animate: true,
                    animationOptions: { duration: 800, queue: false }
                });
            });
            jQuery('ul#filterable-item-filter-1 a').on('click', function (e) {

                jQuery(this).addClass("active");
                jQuery(this).parents("li").siblings().children("a").removeClass("active");
                e.preventDefault();

                var select_filter = jQuery(this).attr('data-value');

                if (select_filter == "All" || jQuery(this).parent().index() == 0) {
                    filter_container.children().each(function () {
                        if (jQuery(this).hasClass('hide')) {
                            jQuery(this).removeClass('hide');
                            jQuery(this).fadeIn();
                        }
                    });
                } else {
                    filter_container.children().not('.' + select_filter).each(function () {
                        if (!jQuery(this).hasClass('hide')) {
                            jQuery(this).addClass('hide');
                            jQuery(this).fadeOut();
                        }
                    });
                    filter_container.children('.' + select_filter).each(function () {
                        if (jQuery(this).hasClass('hide')) {
                            jQuery(this).removeClass('hide');
                            jQuery(this).fadeIn();
                        }
                    });
                }

                filter_container.masonry();

            });
        }
    });

    /*
	  =======================================================================
		  		Map Script Script
	  =======================================================================
	*/
    //if ($('#map-canvas').length) {
    //    google.maps.event.addDomListener(window, 'load', initialize);
    //}

    /*
	  =======================================================================
		  		Range Slider Script Script
	  =======================================================================
      if ($('.slider-range').length) {
        $(".slider-range").slider({
            range: true,
            min: 0,
            max: 500,            
            slide: function (event, ui) {
                $(".amount").html(ui.values[0] + "m<sup>2</sup>" + " - " + ui.values[1] + "m<sup>2</sup>");
                $('#dientich').val(ui.values[0] + "-" + ui.values[1]);
            }
        });
        $(".amount").html($(".slider-range").slider("values",0) +
		  "m<sup>2</sup> - " + $(".slider-range").slider("values",1) + "m<sup>2</sup>");
        $('#dientich').val($(".slider-range").slider("values", 0) +
		  "-" + $(".slider-range").slider("values", 1));
    }
	*/

    /*
    * =======================================================================
    *                   search_section em rất ít khi comment nha!
    * =======================================================================
    */

    if ($('#search_section').length) {
        $('html, body').animate({
            scrollTop: $("#search_section").offset().top
        }, 600);
    }
    
});

/* ---------------------------------------------------------------------- */
/*	Google Map Function for Custom Style
/* ---------------------------------------------------------------------- */
//function initialize() {
//    var MY_MAPTYPE_ID = 'custom_style';
//    var map;
//    var brooklyn = new google.maps.LatLng(40.6743890, -73.9455);
//    var featureOpts = [
//        {
//            stylers: [
//              { hue: '#f9f9f9' },
//              { visibility: 'simplified' },
//              { gamma: 0.7 },
//              { saturation: -200 },
//              { lightness: 45 },
//              { weight: 0.6 }
//            ]
//        },
//        {
//            featureType: "road",
//            elementType: "geometry",
//            stylers: [
//              { lightness: 200 },
//              { visibility: "simplified" }
//            ]
//        },
//        {
//            elementType: 'labels',
//            stylers: [
//              { visibility: 'on' }
//            ]
//        },
//        {
//            featureType: 'water',
//            stylers: [
//              { color: '#ffffff' }
//            ]
//        }
//    ];

//    var mapOptions = {
//        zoom: 15,
//        scrollwheel: false,
//        center: brooklyn,
//        mapTypeControlOptions: {
//            mapTypeIds: [google.maps.MapTypeId.ROADMAP, MY_MAPTYPE_ID]
//        },
//        mapTypeId: MY_MAPTYPE_ID
//    };

//    map = new google.maps.Map(document.getElementById('map-canvas'),
//          mapOptions);

//    var styledMapOptions = {
//        name: 'Custom Style'
//    };

//    var customMapType = new google.maps.StyledMapType(featureOpts, styledMapOptions);

//    map.mapTypes.set(MY_MAPTYPE_ID, customMapType);
//}

function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function Validatephonenumber(inputtxt)  
{  
    var phoneno = /^\d{10}$/;  
    if(inputtxt.value.match(phoneno))  
    {
        return true;  
    }  
    else  
    {  
        return false;  
    }  
}

/**
* Add a URL parameter (or changing it if it already exists)
* @param {search} string  this is typically document.location.search
* @param {key}    string  the key to set
* @param {val}    string  value 
*/
var addUrlParam = function (search, key, val) {
    var newParam = key + '=' + val,
        params = '?' + newParam;

    // If the "search" string exists, then build params from it
    if (search) {
        // Try to replace an existance instance
        params = search.replace(new RegExp('([?&])' + key + '[^&]*'), '$1' + newParam);

        // If nothing was replaced, then add the new param to the end
        if (params === search) {
            params += '&' + newParam;
        }
    }

    return params;
};

function gup(name, url) {
    if (!url) url = location.href;
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(url);
    return results == null ? null : results[1];
}
//gup('q', 'hxxp://example.com/?q=abc')

$(document).ready(function () {
    /**
     * Search panel
     */
    if ($('#search_header_btn').length) {
        $('#search_header_btn').on('click', function (e) {
            e.preventDefault();
            if ($(this).siblings('#search_header_input').val() === "") {
                alert('Vui lòng nhập từ khóa tìm kiếm');
            } else {
                var myValues = $(this).siblings('#search_header_input').val();
                var url_2 = "/search/van-phong/?keyword=" + myValues;
                location.href = url_2;
            }
        })
    }

    // scroll top
    //==========================================    
    $(window).scroll(function () {
        $(this).scrollTop() > 500 ? $('.totop').fadeIn() : $('.totop').fadeOut();
    });
    if ($('.totop').length) {
        $('.totop').click(function () {
            $('html,body').animate({ scrollTop: 0 }, 500);
            return false;
        })
    }

});