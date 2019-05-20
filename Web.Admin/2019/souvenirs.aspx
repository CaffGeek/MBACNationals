<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_contingents.jpg');"></div>
    <div id="photoCredit"><strong>Casino du Lac-Leamy Fireworks</strong> &bull; Credit: fw42 - Flickr Commons</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">    
    <div class="col-md-2">
        <ul class="sidebarNav">
            
        </ul>
    </div>

    <div class="col-md-10" id="souvenirs">
        <h2>Souvenirs</h2>
        Download the <a href="docs/SouvenirOrderFormFinal.pdf">order form here</a>
        or the <a href="docs/SouvenirOrderFormBeerMugTowel.pdf">Beer and Towels form here.</a>
        <iframe src="docs/SouvenirOrderFormFinal.pdf" style="width: 100%; height: 600px;"></iframe>
        <iframe src="docs/SouvenirOrderFormBeerMugTowel.pdf" style="width: 100%; height: 600px;"></iframe>
    </div>
</asp:Content>