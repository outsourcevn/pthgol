jQuery(document).ready(function($) {
    "use strict";
	
	/* PrettyPhoto Script
    ======================================================*/
    $('a[data-rel]').each(function () {
        $(this).attr('rel', $(this).data('rel'));
        $(".pretty-gallery a[rel^='prettyPhoto']").prettyPhoto();
    });

	
	//STICKY HEADER
    if($('#cp_video-banner').length){
	videojs('cp_video-banner', {
    "autoplay": true,
	"loop": true,
	});
	}


    //STICKY HEADER
    if($('#cp_header-v1').length){
        // grab the initial top offset of the navigation 
        var stickyNavTop = $('#cp_header-v1').offset().top;
        // our function that decides weather the navigation bar should have "fixed" css position or not.
        var stickyNav = function(){
            var scrollTop = $(window).scrollTop(); // our current vertical position from the top
            // if we've scrolled more than the navigation, change its position to fixed to stick to top,
            // otherwise change it back to relative
            if (scrollTop > stickyNavTop) { 
                $('#cp_header-v1').addClass('cp_sticky');
            } else {
                $('#cp_header-v1').removeClass('cp_sticky'); 
            }
        };

        stickyNav();
        // and run it again every time you scroll
        $(window).scroll(function() {
            stickyNav();
        });
    }

    //STICKY HEADER For Header 3 Sticky
    if($('#cp_header-v3').length){
        // grab the initial top offset of the navigation 
        var stickyNavTop = $('#cp_header-v3').offset().top;
        // our function that decides weather the navigation bar should have "fixed" css position or not.
        var stickyNav = function(){
            var scrollTop = $(window).scrollTop(); // our current vertical position from the top
            // if we've scrolled more than the navigation, change its position to fixed to stick to top,
            // otherwise change it back to relative
            if (scrollTop > stickyNavTop) { 
                $('#cp_header-v3').addClass('cp_sticky');
            } else {
                $('#cp_header-v3').removeClass('cp_sticky'); 
            }
        };

        stickyNav();
        // and run it again every time you scroll
        $(window).scroll(function() {
            stickyNav();
        });
    }

    /* Side Bar Menu Js
    ======================================================*/
    if ($('#push, #close').length) {
        $('#push, #close').on('click', function(){
            var $navigacia = $('body, #cp-slide-menu'),
            val = $navigacia.css('left') === '300px' ? '0px' : '300px';
            $navigacia.animate({
            left: val
            }, 500)
        });
    }

    /* Owl Slider For Banner 1
    ======================================================*/
    if ($('#home-slider1').length) {
        $('#home-slider1').owlCarousel({
            loop:true,
            dots: false,
            nav:true,
            navText:'',
            items:1,
            autoplay: false,
            smartSpeed:2000,

        });
    }

    /* Owl Slider For Banner Rider Store Page
    ======================================================*/
    if ($('#home-slider2').length) {
        $('#home-slider2').owlCarousel({
            loop:true,
            dots: false,
            nav:false,
            navText:'',
            items:1,
            autoplay: false,
            smartSpeed:2000,

        });
    }


    /* Owl Slider For Banner Rider Repair Shop Page
    ======================================================*/
    if ($('#home-slider3').length) {
        $('#home-slider3').owlCarousel({
            loop:true,
            dots: false,
            nav:true,
            navText:'',
            items:1,
            autoplay: false,
            smartSpeed:2000,

        });
    }

     /* Owl Slider For Banner Rider Shop Page
    ======================================================*/
    if ($('#home-slider4').length) {
        $('#home-slider4').owlCarousel({
            loop:true,
            dots: false,
            nav:true,
            navText:'',
            items:1,
            autoplay: false,
            smartSpeed:2000,

        });
    }

    /* Owl Slider For Featured Section
    ======================================================*/
    if ($('#cp-time-slider').length) {
        $('#cp-time-slider').owlCarousel({
            loop:true,
            dots: false,
            nav:false,
            navText:'',
            items:4,
            smartSpeed:500,
            margin:0,
            responsiveClass:true,
            responsive:{
                0:{
                    items:1,
                },
                768:{
                    items:2,
                },
                1199:{
                    items:4,
                }
            }
        });
    }
	
	/* Owl Slider For Our Upcoming Event
    ======================================================*/
    if ($('.upevents-slider').length) {
        $('.upevents-slider').owlCarousel({
            loop:true,
            dots: false,
            nav:true,
            navText:'',
            items:2,
            smartSpeed:500,
            margin:30,
            responsiveClass:true,
            responsive:{
                0:{
                    items:1,
                },
                768:{
                    items:2,
                },
                1199:{
                    items:2,
                }
            }
        });
    }

    /* Owl Slider For Our Upcoming Event
    ======================================================*/
    if ($('#cp-team-slider').length) {
        $('#cp-team-slider').owlCarousel({
            loop:true,
            dots: false,
            nav:true,
            navText:'',
            items:2,
            smartSpeed:500,
            margin:30,
            responsiveClass:true,
            responsive:{
                0:{
                    items:1,
                },
                768:{
                    items:2,
                },
                1199:{
                    items:2,
                }
            }
        });
    }

    /* Owl Slider For Testimonial
    ======================================================*/
    if ($('#cp-testimonial-slider').length) {
        $('#cp-testimonial-slider').owlCarousel({
            loop:true,
            dots: true,
            nav:false,
            navText:'',
            items:1,
            autoplay: false,
            smartSpeed:1500,
        });
    }


    /* Owl Slider For Testimonial
    ======================================================*/
    if ($('#cp-testimonial-slider2').length) {
        $('#cp-testimonial-slider2').owlCarousel({
            loop:true,
            dots: true,
            nav:false,
            navText:'',
            items:1,
            autoplay: false,
            smartSpeed:1500,
        });
    }


    /* Owl Slider For Blog
    ======================================================*/
    if ($('#cp-blog-slider').length) {
        $('#cp-blog-slider').owlCarousel({
            loop:true,
            dots: false,
            nav:true,
            navText:'',
            items:2,
            smartSpeed:500,
            padding:0,
            margin: 30,
            responsiveClass:true,
            responsive:{
                0:{
                    items:1,
                },
                768:{
                    items:1,
                },
                992:{
                    items:2,
                },
                1199:{
                    items:2,
                }
            }
        });
    }

     /* Owl Slider For Blog Page
    ======================================================*/
    if ($('#cp-blog-slider2').length) {
        $('#cp-blog-slider2').owlCarousel({
            loop:true,
            dots: false,
            nav:true,
            navText:'',
            items:1,
            smartSpeed:500,
            padding:0,
            margin: 30,
            responsiveClass:true,
            responsive:{
                0:{
                    items:1,
                },
                768:{
                    items:1,
                },
                1199:{
                    items:1,
                }
            }
        });
    }
    

     /* Owl Slider For Blog
    ======================================================*/
    //  if ($('#cp-blog-slider').length) {
    //     $('#cp-blog-slider').owlCarousel({
    //         loop:true,
    //         dots: false,
    //         nav:true,
    //         navText:'',
    //         items:1,
    //         autoplay: false,
    //         smartSpeed:1500,
    //     });
    // }

    /* Owl Slider For Blog Post
    ======================================================*/
     if ($('#cp-testimonial-slider').length) {
        $('#cp-testimonial-slider').owlCarousel({
            loop:true,
            dots: true,
            nav:false,
            navText:'',
            items:1,
            autoplay: false,
            smartSpeed:1500,
        });
    }

    /* Owl Slider For Review
    ======================================================*/
     if ($('#cp-review-slider').length) {
        $('#cp-review-slider').owlCarousel({
            loop:true,
            dots: false,
            nav:true,
            navText:'',
            items:1,
            autoplay: false,
            smartSpeed:1500,
        });
    }

     /* Owl Slider For Product Detail Page
    ======================================================*/
    if ($('#cp-product-slider1').length) {
        $('#cp-product-slider1').owlCarousel({
            loop:true,
            dots: false,
            nav:true,
            navText:'',
            items:3,
            smartSpeed:500,
            padding:0,
            margin: 30,
            responsiveClass:true,
            responsive:{
                0:{
                    items:1,
                },
                768:{
                    items:1,
                },
                992:{
                    items:2,
                },
                1199:{
                    items:3,
                }
            }
        });
    }

    /* Owl Slider For Widget Post
    ======================================================*/
     if ($('#cp-widget-slider').length) {
        $('#cp-widget-slider').owlCarousel({
            loop:true,
            dots: true,
            nav:false,
            navText:'',
            items:1,
            autoplay: false,
            smartSpeed:1500,
        });
    }

	/* BX Slider For Product Detail
    ======================================================*/
    if ($('#cp_product-slider').length) {
        $('#cp_product-slider').bxSlider({
            auto: true,
            pagerCustom: '#bx-pager',
            navigation: false,
        });
    }

    /* BX Slider For Home Page
    ======================================================*/
    if ($('#cp_product-slider2').length) {
        $('#cp_product-slider2').bxSlider({
            auto: true,
            pagerCustom: '#bx-pager2',
            navigation: false,
        });
    }

	/* Event Countdown Scipts Start
    ======================================================*/
	if ($('.eventcountdown').length) {
		$(function () {
		var austDay = new Date();
		austDay = new Date(austDay.getFullYear() + 1, 1 - 1, 26);
		$('.eventcountdown').countdown({until: austDay});
		$('#year').text(austDay.getFullYear());
		});
	}
    
    /* CounterUp For Facts Start
    ======================================================*/
   if ($('.counter').length) {
       $('.counter').counterUp({
           delay: 10,
           time: 1000
       });
   }

    /* Countdown For Banner
    ======================================================*/
    if ($('.cp-countdown').length) {
        $('.cp-countdown').final_countdown({
            'start': 1362139200,
            'end': 1388461320,
            'now': 1387461319
        });
    }

    /* Isotope For Portfolio
    ======================================================*/
    if ($(".cp-gallery-masonary .isotope").length) {
        var $container = $('.cp-gallery-masonary .isotope');
		$container.imagesLoaded( function(){
			$container.isotope({
				itemSelector: '.cp-masonary-item',
				transitionDuration: '0.6s',
				masonry: {
					columnWidth: $container.width() / 12
				},
				layoutMode: 'masonry'
			});
		});

        $(window).resize(function() {
            $container.isotope({
                masonry: {
                    columnWidth: $container.width() / 12
                }
            });
        });
    }

    /* Isotope For Blog Masonary
    ======================================================*/
    if ($(".cp-blog-masonary .isotope").length) {
        var $container = $('.cp-blog-masonary .isotope');
        $container.imagesLoaded( function(){
            $container.isotope({
                itemSelector: '.cp-masonary-item',
                transitionDuration: '0.6s',
                masonry: {
                    columnWidth: $container.width() / 12
                },
                layoutMode: 'masonry'
            });
        });

        $(window).resize(function() {
            $container.isotope({
                masonry: {
                    columnWidth: $container.width() / 12
                }
            });
        });

    }


     /* FILTERABLE Masonary For Gallery
    ======================================================*/
    if ($('.cp_portfolioGallery').length) {
        var $container = $('.cp_portfolioGallery');
        $container.imagesLoaded( function(){
            $container.isotope({
                filter: '*',
                animationOptions: {
                    duration: 750,
                    easing: 'linear',
                    queue: false
                }
            });
        });

        $('.cp_galleryFilter a').on('click', function() {
            $('.cp_galleryFilter .current').removeClass('current');
            $(this).addClass('current');

            var selector = $(this).attr('data-filter');
            $container.isotope({
                filter: selector,
                animationOptions: {
                    duration: 750,
                    easing: 'linear',
                    queue: false
                }
            });
            return false;
        });
    }


    /* Filterable For Gallery
    ======================================================*/
    if ($('.galleryContainer').length) {
        var $container = $('.galleryContainer');
		
		$container.imagesLoaded( function(){
		
			$container.isotope({
				filter: '*',
				animationOptions: {
					duration: 750,
					easing: 'linear',
					queue: false
				}

			});
		
		});
    }
    
    if ($('.galleryFilter a').length) {
        $('.galleryFilter a').on('click', function(){
            $('.galleryFilter .current').removeClass('current');
            $(this).addClass('current');
     
            var selector = $(this).attr('data-filter');
            $container.isotope({
                filter: selector,
                animationOptions: {
                    duration: 750,
                    easing: 'linear',
                    queue: false
                }
             });
             return false;
        }); 
    }

    /* Map For Contact Us
    ======================================================*/
    if ($('#cp-map_contact').length) {
        var map;
        var myLatLng = new google.maps.LatLng(48.85661, 2.35222);
        //Initialize MAP
        var myOptions = {
            zoom: 12,
            center: myLatLng,
            //disableDefaultUI: true,
            zoomControl: true,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            mapTypeControl: false,
            styles: [{
                saturation: -50,
                lightness: 10

            }],
        }
        map = new google.maps.Map(document.getElementById('cp-map_contact'), myOptions);
        //End Initialize MAP
        //Set Marker
        var marker = new google.maps.Marker({
            position: map.getCenter(),
            map: map,
            icon: 'images/map-icon.png'
        });
        marker.getPosition();
        //End marker

        //Set info window
        //var infowindow = new google.maps.InfoWindow({
            //content: '',
            //position: myLatLng
        //});
        //infowindow.open(map);
    }

   /* JQuery UI Range Slider For Product
    ======================================================*/
    if ($('#cp-range-slider').length) {
        $( "#cp-range-slider" ).slider({
            range: true,
            min: 0,
            max: 500,
            values: [ 75, 300 ],
            slide: function( event, ui ) {
                $( "#amount" ).val( "$" + ui.values[ 0 ] + " - $" + ui.values[ 1 ] );
            }
        });
        
        $( "#amount" ).val( "$" + $( "#cp-range-slider" ).slider( "values", 0 ) +
        " - $" + $( "#cp-range-slider" ).slider( "values", 1 ) );
    };

   /* JQuery Calendar for Event Page
    ======================================================*/
    
    if ($('#cp-calendar').length) {
        
        $('#cp-calendar').fullCalendar({
            defaultDate: '2016-05-12',
            editable: true,
            eventLimit: true, // allow "more" link when too many events
            events: [
                {
                    title: 'All Day Event',
                    start: '2016-05-01'
                },
                {
                    title: 'Long Event',
                    start: '2016-05-07',
                    end: '2016-05-10'
                },
                {
                    id: 999,
                    title: 'Repeating Event',
                    start: '2016-05-09T16:00:00'
                },
                {
                    id: 999,
                    title: 'Repeating Event',
                    start: '2016-05-16T16:00:00'
                },
                {
                    title: 'Conference',
                    start: '2016-05-11',
                    end: '2016-05-13'
                },
                {
                    title: 'Meeting',
                    start: '2016-05-12T10:30:00',
                    end: '2016-05-12T12:30:00'
                },
                {
                    title: 'Lunch',
                    start: '2016-05-12T12:00:00'
                },
                {
                    title: 'Meeting',
                    start: '2016-05-12T14:30:00'
                },
                {
                    title: 'Happy Hour',
                    start: '2016-05-12T17:30:00'
                },
                {
                    title: 'Dinner',
                    start: '2016-05-12T20:00:00'
                },
                {
                    title: 'Birthday Party',
                    start: '2016-05-13T07:00:00'
                },
                {
                    title: 'Click for Google',
                    url: 'http://google.com/',
                    start: '2016-05-28'
                }
            ]
        });
    };


    //Skills Scroll For Team Detail
	if ($('.circlestat').length) {
        $('.circlestat').circliful();
    };

   /* JQuery Calendar for DateTimePicker
    ======================================================*/
    if ($('.datepicker').length){
       $(".datepicker").datepicker();
    };


    /* JQuery Load on Pop Up Show
    ======================================================*/
   $(document).ready(function(){
     $('#popup-overlay-home').fadeIn(500,function(){
        $('#popup').show();
         $('#popup').css({"top":"200px","opacity":"1","visibility":"visible"});
     });

     $("#popup-close").on('click', function() {
        $('#popup-overlay-home').css({"display":"none","visibility":"hidden"});
        
     });
  });

});
//Huynv add, thêm vào giỏ hàng
function addToCart(product_id) {
    $.ajax({
        url: '/Products/addToCart',
        type: 'POST',
        datatype: 'text',
        data: "product_id=" + product_id,
        success: function (data) {
            if (data != "0") {
                //alert(data);
                var obj = JSON.parse(data);
                //alert(obj.product_photos);
                //add item
                var item="<li>";
                item += "<img src=\"" + obj.product_photos + "\" alt=\"\">";
                item+="<div class=\"cart-text\">";
                item += "<strong>" + obj.product_name + "</strong>";
                item += "<span>" + obj.quantity + "x" + obj.product_price + "</span>";
                item+="<div class=\"cart-icons\">";
                item+="<a href=\"#\"><i class=\"fa fa-pencil-square-o\" aria-hidden=\"true\"></i></a>";
                item+="<a href=\"#\"><i class=\"fa fa-times\" aria-hidden=\"true\"></i></a>";
                item+="</div>";
                item+="</div>";
                item+= "</li>";
                $("#cart-listed").append(item);
                //alert(item);
                if (document.getElementById("totalcartitems")) {
                    var total = parseInt($("#totalcartitems").html());
                    total++;
                    $("#totalcartitems").html(total);
                } else {
                    $("#totalcartitems").html("1");
                }
                if (document.getElementById("chkshowpopupdv").checked==false) $("#showpopupdv").show();
                
            };
        },
        error: function (jqXHR, exception) {
            alert(exception.toString());
        }
    });
}
//Huynv add, bỏ vào giỏ hàng
function removeCartItem(id) {
    $.ajax({
        url: '/Products/removeCartItem',
        type: 'POST',
        datatype: 'text',
        data: "id=" + id,
        success: function (data) {
            if (data == "1") {
                $("#cartitem_" + id).hide();
                var total = parseInt($("#totalcartitems").html());
                total--;
                if (total>=0) $("#totalcartitems").html(total);
            };
        },
        error: function (jqXHR, exception) {
            alert(exception.toString());
        }
    });
}
