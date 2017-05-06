<?php

     
    // EDIT THE 2 LINES BELOW AS REQUIRED
    $email_to = "crunchpress@info.com";
    $email_subject = "New Membership Form Submission";
	$error_message = '';

     

// echo "testing";
// print_r($_POST);

   // validation
    if(empty($_POST['name4']))
    {
     
      echo "Fields are not filled properly";
      die();
    }else if(empty($_POST['email4'])){
       echo "Fields are not filled properly";
        die();
    }else if(empty($_POST['subject4'])){
       echo "Fields are not filled properly";
       die();

    }else if(empty($_POST['password4'])){
       echo "Fields are not filled properly";
       die();

    }else if(empty($_POST['phone4'])){
       echo "Fields are not filled properly";
       die();

    }else if(empty($_POST['address4'])){
       echo "Fields are not filled properly";
       die();

    }else if(empty($_POST['postcode4'])){
       echo "Fields are not filled properly";
       die();

    }else if(empty($_POST['comment4'])){
       echo "Fields are not filled properly";
       die();
    }else{     
      
 

    $name = $_POST['name4']; // required
    $email = $_POST['email4']; // required
    $subject = $_POST['subject4']; // required
    $password = $_POST['password4']; // password
    $address = $_POST['address4']; // password
    $phone = $_POST['phone4']; // password
    $postcode = $_POST['postcode4']; // password
    $comments = $_POST['comment4'];
  
 
     
    $email_message = '<html>';
    $email_message = '<body>';
    $email_message = '<head>';
    $email_message = '<title>Your Details Are Below</title>';
    $email_message = '</head>';
    $email_message .= '<h1>Your Details Are Below</h1>';
    $email_message .= '<table rules="all" style="border-color: #666;" cellpadding="10">';
    $email_message .= "<tr style='background: #eee;'><td><strong>Name:</strong> </td><td>" . strip_tags($_POST['name4']) . "</td></tr>";
    $email_message .= "<tr><td><strong>Email:</strong> </td><td>" . strip_tags($_POST['email4']) . "</td></tr>";
    $email_message .= "<tr><td><strong>Subject:</strong> </td><td>" . strip_tags($_POST['subject4']) . "</td></tr>";
    $email_message .= "<tr><td><strong>Password:</strong> </td><td>" . strip_tags($_POST['password4']) . "</td></tr>";
    $email_message .= "<tr><td><strong>Phone:</strong> </td><td>" . strip_tags($_POST['phone4']) . "</td></tr>";
    $email_message .= "<tr><td><strong>Address:</strong> </td><td>" . strip_tags($_POST['address4']) . "</td></tr>";
    $email_message .= "<tr><td><strong>Postcode:</strong> </td><td>" . strip_tags($_POST['postcode4']) . "</td></tr>";
    $email_message .= "<tr><td><strong>Comment:</strong> </td><td>" . strip_tags($_POST['comment4']) . "</td></tr>";
    $email_message .= "</table>";
    $email_message .= "</body></html>"; 



    $headers = "From: " . $email . "\r\n";
    $headers .= "Reply-To: ". $email . "\r\n";
    $headers .= "CC: susan@example.com\r\n";
    $headers .= "MIME-Version: 1.0\r\n";
    $headers .= "Content-Type: text/html; charset=ISO-8859-1\r\n";


    mail($email_to, $email_subject, $email_message, $headers); 

  }



?>

<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>Rider HTML5 Responsive Template</title>
<!---Style CSS-->
<link href="css/style.css" rel="stylesheet">
<!---Bootstrap CSS-->
<link href="css/bootstrap.css" rel="stylesheet">
<!---Theme Responsive CSS-->
<link href="css/responsive.css" rel="stylesheet">
<!---Theme Color CSS-->
<link href="css/theme-color.css" rel="stylesheet">
<!---Owl Carousel CSS-->
<link href="css/owl.carousel.css" rel="stylesheet">
<!---PrettyPhoto CSS-->
<link href="css/prettyPhoto.css" rel="stylesheet">
<!---FontAwesome CSS-->
<link href="css/font-awesome.min.css" rel="stylesheet">
<!---IcoMoon CSS-->
<link href="css/icomoon.css" rel="stylesheet">
<!--Font Family Css Start-->
<link href='https://fonts.googleapis.com/css?family=Archivo+Narrow:400,400italic,700,700italic' rel='stylesheet' type='text/css'>
<link href='https://fonts.googleapis.com/css?family=Roboto+Slab:400,100,300,700' rel='stylesheet' type='text/css'>
<link href='https://fonts.googleapis.com/css?family=Roboto:400,500,700,300' rel='stylesheet' type='text/css'>
<link href='https://fonts.googleapis.com/css?family=Mrs+Saint+Delafield' rel='stylesheet' type='text/css'>
<!-- Css Files End -->
<!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
<![endif]-->
    
</head>

 <body>
<!--Wrapper Start-->
<div class="cp-wrapper" id="cp-rider"> 
  <!-- Header Start -->
  <header class="cp-header cp-header-inner" id="cp_header-v1"> 
     <div id="cp-slide-menu" class="cp_side-navigation">
         <ul class="navbar-nav">
            <li id="close"><a href="#"><i class="fa fa-close"></i></a></li>
           <li class="dropdown"> <a href="index.html" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Home <i class="fa fa-angle-down"></i></a>
              <ul class="dropdown-menu">
                  <li><a href="index.html">Rider With Video</a></li>
                  <li><a href="index-rider-slider.html">Rider With Slider</a></li>
                  <li><a href="index-shop.html">Rider Shop</a></li>
                  <li><a href="index-repair-shop.html">Rider Repair Shop</a></li>
                  <li><a href="index-store.html">Rider Single Store</a></li>
              </ul>
            </li>
            <li><a href="about.html">About</a></li>
            <li class="dropdown"> <a href="contact.html" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Shop <i class="fa fa-angle-down"></i></a>
              <ul class="dropdown-menu">
                <li><a href="product.html">Products</a></li>
                <li><a href="product-detail.html">Product Detail</a></li>
                <li><a href="product-sidebar.html">Products with Sidebar</a></li>
                <li><a href="product-listing.html">Products listing</a></li>
                <li><a href="cart.html">Cart</a></li>
                <li><a href="cart-empty.html">Cart Empty</a></li>
                <li><a href="checkout.html">Checkout</a></li>
              </ul>
            </li>
            <li class="dropdown"> <a href="contact.html" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Instructor <i class="fa fa-angle-down"></i></a>
              <ul class="dropdown-menu">
                <li><a href="team1.html">Our instructor 1</a></li>
                <li><a href="team2.html">Our instructor 2</a></li>
                <li><a href="team-details.html">Instructor Detail</a></li>
              </ul>
            </li>
            <li class="dropdown"> <a href="events.html" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Our Events <i class="fa fa-angle-down"></i></a>
              <ul class="dropdown-menu">
                <li><a href="event-list.html">Event List</a></li>
                <li><a href="event-grid.html">Event Grid</a></li>
                <li><a href="event-box.html">Event Box</a></li>
                <li><a href="event-calendar.html">Event Calendar</a></li>
                <li><a href="event-sidebar.html">Past Events</a></li>
                <li><a href="event-upcoming.html">Upcoming Events</a></li>
                <li><a href="events-details.html">Event Details Page</a></li>
              </ul>
            </li>
            <li class="dropdown"> <a href="blog-post.html" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Blog <i class="fa fa-angle-down"></i></a>
              <ul class="dropdown-menu">
                <li><a href="blog.html">Blog</a></li>
                <li><a href="blog-details.html">Blog Detail</a></li>
                <li><a href="blog-full.html">Blog Full Width</a></li>
                <li><a href="blog-full-detail.html">Blog Full Width Detail</a></li>
                <li><a href="blog-medium.html">Blog Medium</a></li>
                <li><a href="blog-grid.html">Blog Grid Page</a></li>
                <li><a href="blog-masonary.html">Blog Masonary</a></li>
              </ul>
            </li>
            <li class="dropdown"> <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Pages <i class="fa fa-angle-down"></i></a>
              <ul class="dropdown-menu">
                <li><a href="p404-1.html">404-1</a></li>
                <li><a href="p404-2.html">404-2</a></li>
                <li><a href="gallery-1.html">Gallery one column</a></li>
                <li><a href="gallery-2.html">Gallery two column</a></li>
                <li><a href="gallery-3.html">Gallery three column</a></li>
                <li><a href="gallery-4.html">Gallery four column</a></li>
                <li><a href="gallery-masonary.html">Gallery masonary</a></li>
              </ul>
            </li>
            <li class="dropdown"> <a href="contact.html" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Contact <i class="fa fa-angle-down"></i></a>
              <ul class="dropdown-menu">
                <li><a href="contact.html">Contact</a></li>
                <li><a href="contact-v2.html">Contact 2</a></li>
              </ul>
            </li>
        </ul>
    </div>
    <!-- Navigation Row Start -->
    <div class="cp-navigation-row">
      <div class="container">
        <div class="cp-sidemenu-btn">
           <div id="push" class="cp-sidemenu"><a href="#"><i class="fa fa-align-justify"></i></a></div>
        </div>
        <div class="cp-nav-holder">
          <strong class="cp-sm-logo">
            <a href="index.html"><img src="images/cp-logo.png" alt=""></a>
          </strong>
          <nav class="navbar navbar-default"> 
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header">
              <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false"> <span class="sr-only">Menu</span> <span class="icon-bar"></span> <span class="icon-bar"></span> <span class="icon-bar"></span> </button>
            </div>
            
            <!-- Collect the nav links, forms, and other content for toggling -->
            <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
              <ul class="nav navbar-nav">
                <li class="dropdown"> <a href="index.html" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Home <span class="caret"></span></a>
                 <ul class="dropdown-menu">
                  <li><a href="index.html">Rider With Video</a></li>
                  <li><a href="index-rider-slider.html">Rider With Slider</a></li>
                  <li><a href="index-shop.html">Rider Shop</a></li>
                  <li><a href="index-repair-shop.html">Rider Repair Shop</a></li>
                  <li><a href="index-store.html">Rider Single Store</a></li>
                </ul>
                </li>
                <li><a href="about.html">About</a></li>
                <li class="dropdown"> <a href="contact.html" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Shop <span class="caret"></span></a>
                  <div class="cp-mega-menu mega-col-1">
                    <ul class="dropdown-menu">
                      <li><a href="product.html">Products</a></li>
                      <li><a href="product-detail.html">Product Detail</a></li>
                      <li><a href="product-sidebar.html">Products with Sidebar</a></li>
                      <li><a href="product-listing.html">Products listing</a></li>
                      <li><a href="cart.html">Cart</a></li>
                      <li><a href="cart-empty.html">Cart Empty</a></li>
                      <li><a href="checkout.html">Checkout</a></li>
                    </ul>
                    <div class="cp-mega-listed">
                      <div class="cp-mega-item">
                        <div class="cp-mega-img">
                          <img src="images/mega-cyecle-img.jpg" alt="">
                        </div>
                        <div class="cp-text">
                          <h6>Propel Advanced SL 0</h6>
                          <span>$1580.00</span>
                        </div>
                      </div>
                    </div>
                    <div class="cp-mega-listed">
                      <div class="cp-mega-item">
                        <div class="cp-mega-img">
                          <img src="images/mega-cyecle-img.jpg" alt="">
                        </div>
                        <div class="cp-text">
                          <h6>Propel Advanced SL 0</h6>
                          <span>$1580.00</span>
                        </div>
                      </div>
                    </div>
                    <div class="cp-mega-listed">
                      <div class="cp-mega-item">
                        <div class="cp-mega-img">
                          <img src="images/mega-cyecle-img.jpg" alt="">
                        </div>
                        <div class="cp-text">
                          <h6>Propel Advanced SL 0</h6>
                          <span>$1580.00</span>
                        </div>
                      </div>
                    </div>
                  </div>
                </li>
                <li class="dropdown"> <a href="contact.html" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Instructor <span class="caret"></span></a>
                  <ul class="dropdown-menu">
                    <li><a href="team1.html">Our instructor 1</a></li>
                    <li><a href="team2.html">Our instructor 2</a></li>
                    <li><a href="team-details.html">Instructor Detail</a></li>
                  </ul>
                </li>
                <li class="dropdown"> <a href="events.html" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Our Events <span class="caret"></span></a>
                  <div class="cp-mega-menu mega-event">
                    <ul class="dropdown-menu">
                      <li><a href="event-list.html">Event List</a></li>
                      <li><a href="event-grid.html">Event Grid</a></li>
                      <li><a href="event-box.html">Event Box</a></li>
                      <li><a href="event-calendar.html">Event Calendar</a></li>
                      <li><a href="event-sidebar.html">Past Events</a></li>
                      <li><a href="event-upcoming.html">Upcoming Events</a></li>
                      <li><a href="events-details.html">Event Details Page</a></li>
                    </ul>
                  </div>
                </li>
                <li class="dropdown"> <a href="blog-post.html" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Blog <span class="caret"></span></a>
                  <div class="cp-mega-menu mega-blog">
                    <ul class="dropdown-menu">
                      <li><a href="blog.html">Blog</a></li>
                      <li><a href="blog-details.html">Blog Detail</a></li>
                      <li><a href="blog-full.html">Blog Full Width</a></li>
                      <li><a href="blog-full-detail.html">Blog Full Width Detail</a></li>
                      <li><a href="blog-medium.html">Blog Medium</a></li>
                      <li><a href="blog-grid.html">Blog Grid Page</a></li>
                      <li><a href="blog-masonary.html">Blog Masonary</a></li>
                    </ul>
                  </div>
                </li>
                <li class="dropdown"> <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Pages <span class="caret"></span></a>
                  <div class="cp-mega-menu mega-col-2">
                    <ul class="dropdown-menu">
                      <li><a href="p404-1.html">404-1</a></li>
                      <li><a href="p404-2.html">404-2</a></li>
                      <li><a href="gallery-1.html">Gallery one column</a></li>
                      <li><a href="gallery-2.html">Gallery two column</a></li>
                      <li><a href="gallery-3.html">Gallery three column</a></li>
                      <li><a href="gallery-4.html">Gallery four column</a></li>
                      <li><a href="gallery-masonary.html">Gallery masonary</a></li>

                    </ul>
                  </div>
                </li>
                <li class="dropdown"> <a href="contact.html" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Contact <span class="caret"></span></a>
                  <ul class="dropdown-menu">
                    <li><a href="contact.html">Contact</a></li>
                    <li><a href="contact-v2.html">Contact 2</a></li>
                  </ul>
                </li>
              </ul>
              
            </div>
            <!-- /.navbar-collapse --> 
          </nav>
          <!--Nav Right Holder Start-->
          <div class="cp-nav-right">
            <ul>
              <li class="cp-cart-holder">
                <span class="icon-shopping-cart icomoon"></span>
                <span class="cart-item">2</span>
                <div class="cp-cart-inner">
                  <ul class="cart-listed">
                    <li>
                      <img src="images/cart-img.jpg" alt="">
                      <div class="cart-text">
                        <strong>Primis in faucibus</strong>
                        <span>1 x$100.00</span>
                        <div class="cart-icons">
                          <a href="#"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></a>
                          <a href="#"><i class="fa fa-times" aria-hidden="true"></i></a>
                        </div>
                      </div>
                    </li>
                     <li>
                      <img src="images/cart-img.jpg" alt="">
                      <div class="cart-text">
                        <strong>Primis in faucibus</strong>
                        <span>1 x$100.00</span>
                        <div class="cart-icons">
                          <a href="#"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></a>
                          <a href="#"><i class="fa fa-times" aria-hidden="true"></i></a>
                        </div>
                      </div>
                    </li>
                  </ul>
                </div>
              </li>
              <li class="cp-search-holder">
                <button id="trigger-overlay" type="button"><i class="fa fa-search" aria-hidden="true"></i></button>
              </li>
            </ul>
          </div><!--Nav Right Holder End-->
        </div>
      </div>
    </div><!--Navigation Row End-->
	
	 <!--Search Bar Holder Start-->
    <div class="overlay overlay-contentscale">
      <button type="button" class="overlay-close">Close</button>
      <!--Search Bar Inner Start-->
      <div class="cp-search-inner">
        <form method="get">
          <input type="text" placeholder="Enter your search" required="required">
          <button class="submit"><i class="fa fa-search"></i></button>
        </form>
      </div><!--Search Bar Inner End-->
    </div>
    <!--Search Bar Holder End-->

  </header>
  <!-- Header End --> 
  
  <!-- Inner Banner Start -->
  <div class="cp-inner-banner">
    <div class="container">
      <div class="cp-inner-banner-outer">
        <h2>Form</h2>
          <!--Breadcrumb Start-->
          <ul class="breadcrumb">
            <li><a href="index.html">Home</a></li>
            <li class="active">Our Form</li>
          </ul><!--Breadcrumb End-->
        </div>
      </div>
  </div><!-- Inner Banner End -->  
 
  <!--Main Content Start--> 
 <div class="cp-main-content"> 

    <!-- Start of Thank -->
    <section id="content_Wrapper" class="pd-tb100">
      <section class="container container-fluid">
        <div class="row">
          <div class="col-md-12 error-page">
            <div class="holder">
              <h2>Thank You</h2>
              <p>Thank you for your form submission, as soon as we get this we will get back to you shortly.</p>
            </div>
          </div>
        </div>
      </section>
    </section>
    <!-- End of Thank --> 
  </div>

  <!--Footer Content Start-->
  <footer class="cp-footer mt-0">

   <!--Clients Section Start-->
   <section class="cp-clients-section pd-tb100">
     <div class="container">
       <!--Client Listed Start-->
       <ul class="cp-clients-listed">
         <li><a href="#"><img alt="" src="images/client-logo1.png"></a></li>
         <li><a href="#"><img alt="" src="images/client-logo2.png"></a></li>
         <li><a href="#"><img alt="" src="images/client-logo3.png"></a></li>
         <li><a href="#"><img alt="" src="images/client-logo4.png"></a></li>
         <li><a href="#"><img alt="" src="images/client-logo5.png"></a></li>
       </ul>
       <!--Client Listed End-->
     </div>
   </section><!--Clients Section End-->

   <!--Instagram Section Start-->
   <section class="cp-instagram-section">
    <div class="container-fluid">
       <div class="row">
         <div class="col-md-2 col-sm-2">
           <div class="cp-instagram-text">
             <i class="fa fa-instagram" aria-hidden="true"></i>
             <div class="cp-bottom">
               <span>images from</span>
               <h3>Gallery</h3>
             </div>
           </div>
         </div>
         <div class="col-md-10 col-sm-10">
           <ul class="cp-instagram-listed">
             <li>
               <figure class="cp-thumb">
                 <a href="#"><img src="images/flickr-img1.jpg" alt=""></a>
               </figure>
             </li>
            <li>
               <figure class="cp-thumb">
                 <a href="#"><img src="images/flickr-img2.jpg" alt=""></a>
               </figure>
             </li>
            <li>
               <figure class="cp-thumb">
                 <a href="#"><img src="images/flickr-img3.jpg" alt=""></a>
               </figure>
             </li>
             <li>
               <figure class="cp-thumb">
                 <a href="#"><img src="images/flickr-img4.jpg" alt=""></a>
               </figure>
             </li>
            <li>
               <figure class="cp-thumb">
                 <a href="#"><img src="images/flickr-img5.jpg" alt=""></a>
               </figure>
             </li>
            <li>
               <figure class="cp-thumb">
                 <a href="#"><img src="images/flickr-img6.jpg" alt=""></a>
               </figure>
             </li>
           </ul>
         </div>
       </div>
     </div>
   </section> <!--Instagram Section End-->

   <!--Footer Bottom Section Start-->
   <section class="cp-ft-bottom-section">
     <div class="container-fluid">
       <div class="row">
         <div class="col-md-2 col-sm-2">
           
         </div>
         <div class="col-md-10 col-sm-10">
            <div class="row">
               <div class="col-md-3 col-sm-4">
                 <!--Widget Start-->
                 <div class="widget widget-about">
                    <h3>Social Networks</h3>
                    <ul class="cp-social-links">
                      <li><a href="#"><i class="fa fa-facebook" aria-hidden="true"></i></a></li>
                      <li><a href="#"><i class="fa fa-flickr" aria-hidden="true"></i></a></li>
                      <li><a href="#"><i class="fa fa-linkedin" aria-hidden="true"></i></a></li>
                      <li><a href="#"><i class="fa fa-instagram" aria-hidden="true"></i></a></li>
                    </ul>
                    <span>+ 800 1234560</span>
                 </div>
               </div>
               <div class="col-md-9 col-sm-8">
                 <!--Widget Start-->
                 <div class="widget widget-search">
                   <h3>Newsletter Subscription</h3>
                   <form method="post">
                     <input type="text" placeholder="Enter a valid E-mail address">
                     <button class="btn-submit" type="submit">subscribe us</button>
                   </form>
                 </div>
               </div>
             </div>
         </div>
       </div>
     </div>
   </section><!--Footer Bottom Section End-->

   <!--Footer Copyright Row Start-->
   <div class="cp-copyright-row">
     <div class="container">
       <p>Copyright © 2016 Rider Theme . All rights reserved. </p>
     </div>
  </div><!--CopyRight Row End-->

  </footer><!--Footer Content End-->

</div>
<!--Wrapper End--> 

<!---JQuery-1.11.3.js-->
<script src="js/jquery-1.11.3.min.js"></script>
<!---Bootstrap.js-->
<script src="js/bootstrap.min.js"></script>
<!---Owl Carousel.js-->  
<script src="js/owl.carousel.min.js"></script>
<!---Migrate.js--> 
<script src="js/migrate.js"></script>
<!---PrettyPhoto.js--> 
<script src="js/jquery.prettyPhoto.js"></script>
<!---Countdown.js--> 
<script src="js/jquery.countdown.min.js"></script>
<!---Modernizr Script.js-->
<script src="js/modernizr.custom.js"></script>
<!---Search Script.js-->
<script src="js/search-script.js"></script>
<!---Custom Script.js-->
<script src="js/custom.js"></script>


</body>
</html>
