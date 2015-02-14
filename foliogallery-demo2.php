<!DOCTYPE HTML>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Image Gallery By FolioPages.com</title>
<style type="text/css">
body {
background:#eee;
margin:0;
padding:0;
font:12px arial, Helvetica, sans-serif;
color:#222;
}
</style>
<link type="text/css" rel="stylesheet" href="http://nationals2014.manitobamasterbowlers.com/foliogallery/foliogallery.css" />
<link type="text/css" rel="stylesheet" href="http://nationals2014.manitobamasterbowlers.com/colorbox/colorbox.css" />
<script type="text/javascript" src="http://nationals2014.manitobamasterbowlers.com/foliogallery/jquery.1.11.js"></script>
<script type="text/javascript" src="http://manitobamasterbowlers.com/colorbox/jquery.colorbox-min.js"></script>
<script type="text/javascript">
$(document).ready(function(){
    // initiate colorbox
	$(".albumpix").colorbox({rel:'albumpix', maxWidth:'98%', maxHeight:'98%', slideshow:true, slideshowSpeed:3500, slideshowAuto:false});
});	
</script>
</head>
<body>

<br />
<br />

<?php $_REQUEST['fullalbum']=1; include 'http://manitobamasterbowlers.com/nationals2014/foliogallery.php'; ?>

<br />
<br />

<div align="center">folioGallery - Installation and instructions @ <a href="http://foliopages.com/php-jquery-ajax-photo-gallery-no-database">FolioPages.com</a></div>

<br />
<br />


</body>
</html>