<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_centres.jpg');"></div>
    <div id="photoCredit"><strong>Cascades, Thunder Bay Conservation Area</strong> &bull; Credit: By Frankavitz, Wikimedia Commons</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <div class="col-md-2">
        <ul class="sidebarNav">
            <li><a href="#centre1">Bowling Lanes - 1</a></li>
            <li><a href="#centre2">Bowling Lanes - 3</a></li>
            <li><a href="#centre3">Bowling Lanes - 3</a></li>
        </ul>

    </div>
    <div class="col-md-10">
        <h2>centres</h2>

        <h4 id="centre1">Mario's Bowl</h4>
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-sm-3 centreLogo">
                    <img src="images/centre1.jpg" alt="Marios Bowl" />
                </div>
                <div class="col-md-9 col-sm-9 centreDetails">
                    <p>
                        <strong>807-344-9644<br />
                            710 Memorial Ave, Thunder Bay<br />
                        </strong>
                        For more information, check out their website: <a href="http://www.mariosbowl.com/" target="_blank">www.mariosbowl.com/</a>
                    </p>
                </div>
            </div>
        </div>


        <h4 id="centre2">GALAXY LANES</h4>
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-sm-3 centreLogo">
                    <img src="images/centre2.jpg" alt="GALAXY LANES" />
                </div>
                <div class="col-md-9 col-sm-9 centreDetails">
                    <p>
                        <strong>807-577-6222<br />
                            636 West Arthur Street, Thunder Bay<br />
                        </strong>
                        For more information, check out their website: <a href="http://www.scottsdalelanes.com" target="_blank">http://www.scottsdalelanes.com</a>
                    </p>

                </div>
            </div>
        </div>


        <h4 id="centre3">SUPERIOR BOWLADROME</h4>
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-sm-3 centreLogo">
                    <img src="images/centre3.jpg" alt="SUPERIOR BOWLADROME" />
                </div>
                <div class="col-md-9 col-sm-9 centreDetails">
                    <p>
                        <strong>807-622-2515<br />
                            236 Cumming Street, Thunder Bay</strong><br />
                        For more information, check out their website: <a href="http://www.superiorbowl.ca" target="_blank">http://www.superiorbowl.ca</a>
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
