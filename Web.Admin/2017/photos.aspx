<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_photos.jpg');"></div>
    <div id="photoCredit"><strong>TODO: description</strong> &bull; Credit: TODO: credit</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <div class="col-md-2">
        <ul class="sidebarNav">
            <li><a href="javascript: loadAlbum('1129830643748930');" data-ng-click="title = 'Day 1'">Day 1</a></li>
            <li><a href="javascript: loadAlbum('1129832137082114');" data-ng-click="title = 'Day 2'">Day 2</a></li>
            <li><a href="javascript: loadAlbum('1129832763748718');" data-ng-click="title = 'Day 3'">Day 3</a></li>
            <li><a href="javascript: loadAlbum('1129832883748706');" data-ng-click="title = 'Day 4'">Day 4</a></li>
            <li><a href="javascript: loadAlbum('1129833057082022');" data-ng-click="title = 'Day 5'">Day 5</a></li>
            <li><a href="javascript: loadAlbum('1129833207082007');" data-ng-click="title = 'Day 6'">Day 6</a></li>
            <li><a href="javascript: loadAlbum('1129833423748652');" data-ng-click="title = 'Day 7'">Day 7</a></li>
            <li><a href="https://www.instagram.com/mbacnationals/" target="_blank">Instagram</a></li>
        </ul>
    </div>

    <div class="col-md-10">
        <h2>PHOTOS {{title || 'Day 1'}}</h2>
        <div id="galleria"></div>
    </div>
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script>window.jQuery || document.write('<script src="assets/js/vendor/jquery.min.js"><\/script>')</script>
    <script src="/Web.Public/galleria/galleria-1.4.2.min.js"></script>
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
        loadAlbum(1129830643748930);
    </script>
</asp:Content>
