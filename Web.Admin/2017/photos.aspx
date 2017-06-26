<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_photos.jpg');"></div>
    <div id="photoCredit"><strong>Regina</strong> &bull; Credit: Flickr Commons - Blake Handley</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <div class="col-md-2">
        <ul class="sidebarNav">
            <li><a href="javascript: loadAlbum('1525842230814434');" data-ng-click="title = 'Day 1'">Day 1</a></li>
            <li><a href="javascript: loadAlbum('1525845174147473');" data-ng-click="title = 'Day 2'">Day 2</a></li>
            <li><a href="javascript: loadAlbum('1525845647480759');" data-ng-click="title = 'Day 3'">Day 3</a></li>
            <li><a href="javascript: loadAlbum('1525846370814020');" data-ng-click="title = 'Day 4'">Day 4</a></li>
            <li><a href="javascript: loadAlbum('1525846740813983');" data-ng-click="title = 'Day 5'">Day 5</a></li>
            <li><a href="javascript: loadAlbum('1525847180813939');" data-ng-click="title = 'Day 6'">Day 6</a></li>
            <li><a href="javascript: loadAlbum('1525847940813863');" data-ng-click="title = 'Day 7'">Day 7</a></li>
            <li><a href="https://www.instagram.com/mbacnationals2017/" target="_blank">Instagram</a></li>
        </ul>
    </div>

    <div class="col-md-10">
        <h2>PHOTOS {{title || 'Day 1'}}</h2>
        <div class='embedsocial-album' data-ref="0a7b4fcba94829d6c9e0b1f84ff1a646d7226ec6"></div><script>(function(d, s, id){var js; if (d.getElementById(id)) {return;} js = d.createElement(s); js.id = id; js.src = "https://embedsocial.com/embedscript/ei.js"; d.getElementsByTagName("head")[0].appendChild(js);}(document, "script", "EmbedSocialScript"));</script>
    </div>
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script>window.jQuery || document.write('<script src="assets/js/vendor/jquery.min.js"><\/script>')</script>
<!--    <script src="/Web.Public/galleria/galleria-1.4.2.min.js"></script>
    <script src="/Web.Public/galleria/plugins/facebook/galleria.facebook.js"></script>
    <script>
        function loadAlbum(album) {
            Galleria.run('#galleria', {
                facebook: 'album:' + album,
                height: 550,
                lightbox: true,
                facebookOptions: {
                    max: 100,
                    facebook_access_token: '871325676235910|5640fa457799c71eeace0176717512b2'
                }
            });
        }

        Galleria.loadTheme('/Web.Public/galleria/themes/classic/galleria.classic.min.js');
        loadAlbum(1525842230814434);
    </script>-->
</asp:Content>
