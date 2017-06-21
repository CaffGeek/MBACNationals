<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_photos.jpg');"></div>
    <div id="photoCredit"><strong>Regina</strong> &bull; Credit: Flickr Commons - Blake Handley</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <div class="col-md-2">
        <ul class="sidebarNav">
            <li>Newsletter</li>
        </ul>
    </div>

    <div class="col-md-10">
        <h2>Newsletter Signup</h2>
        <!-- SendinBlue Signup Form HTML Code --><iframe src="https://my.sendinblue.com/users/subscribe/js_id/2dew5/id/1" frameborder="0" scrolling="auto" allowfullscreen style="display: block;"></iframe><!-- End : SendinBlue Signup Form HTML Code -->

    </div>
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script>window.jQuery || document.write('<script src="assets/js/vendor/jquery.min.js"><\/script>')</script>
    <script src="/Web.Public/galleria/galleria-1.4.2.min.js"></script>
    <script src="/Web.Public/galleria/plugins/facebook/galleria.facebook.js"></script>
</asp:Content>
