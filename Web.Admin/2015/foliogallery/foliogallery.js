/*!
	folioGallery v1.3 - 2014-05-01
	(c) 2014 Harry Ghazanian - foliopages.com/php-jquery-ajax-photo-gallery-no-database
	This content is released under the http://www.opensource.org/licenses/mit-license.php MIT License.
*/
$(function() {		
				
	var folioGalleryDir = './'; // foliogallery folder relative path - absolute path like http://my_website.com/foliogallery may not work
		
	// find divs with class folioGallery and load album in it based on its id
	$('.folioGallery').each(function() {
		
		var targetDiv = (this.id); // id of div to load albums
		var numPerPage = $('#'+targetDiv+' div.numPerPage').prop('title');
								
		if(targetDiv=='folioGallery') {
			var fullAlbum = 1;
			var showAlb = ''; // empty will show full gallery
		} else {
			var fullAlbum = 0;
			var showAlb = $('#'+targetDiv).prop('title'); // title attribute of div - same as album folder
		}
		
		loadGallery(folioGalleryDir,targetDiv,showAlb,numPerPage,1,fullAlbum); // inital load
				
		// in gallery view, load album when thumb is clicked
		$(this).on('click', 'a.showAlb', function() {	
			var showAlb = $(this).prop('rel');
			loadGallery(folioGalleryDir,targetDiv,showAlb,numPerPage,1,fullAlbum);
			return false;
		});	
				
		// paginate albums and pics
		$(this).on('click', 'a.pag', function() {	
			var showAlb = $(this).prop('rel');
			var pageNum = $(this).prop('rev');
			loadGallery(folioGalleryDir,targetDiv,showAlb,numPerPage,pageNum,fullAlbum);
			return false;
		});
		
		// refresh div content
		$(this).on('click', 'a.refresh', function() {
		   loadGallery(folioGalleryDir,targetDiv,'',numPerPage,1,fullAlbum);
		   return false;
		});
		
				
		// colorbox
		$(this).on('click', 'a.albumpix', function() {$('#'+targetDiv+' .albumpix').colorbox({rel:targetDiv, maxWidth:'98%', maxHeight:'98%', slideshow:true, slideshowSpeed:3500, slideshowAuto:false}); });

	});
		
});

function loadGallery(folioGallerydir,targetdiv,album,numperpage,pagenum,fullalbum) {                    
	$.ajax
	({
		type: 'POST',
		url: folioGallerydir+'/foliogallery.php?'+album+'&p='+pagenum,
		data: {
			album: album,
			numperpage: numperpage,
			pagenum: pagenum,
			fullalbum: fullalbum
		},
		cache: false,
		success: function(msg)
		{
			$('#'+targetdiv).html(msg).hide().show();
		}
	});
	return false;
}