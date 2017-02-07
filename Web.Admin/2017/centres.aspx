<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_centres.jpg');"></div>
    <div id="photoCredit"><strong>TODO: description</strong> &bull; Credit: TODO: credit</div>
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

        <h4 id="centre1">Bowling Lanes - 1</h4>
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-sm-3 centreLogo">
                    <img src="images/centre1.jpg" alt="Clover Lanes" />
                </div>
                <div class="col-md-9 col-sm-9 centreDetails">
                    <p>
                        <strong>604-574-4601<br />
                            5814 - 176A Street, Surrey, BC<br />
                        </strong>
                        For more information, check out their website: <a href="http://surreybowling.com" target="_blank">http://surreybowling.com</a>
                    </p>
                </div>
            </div>
        </div>


        <h4 id="centre2">Bowling Lanes - 2</h4>
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-sm-3 centreLogo">
                    <img src="images/centre2.jpg" alt="Scottsdale Lanes" />
                </div>
                <div class="col-md-9 col-sm-9 centreDetails">
                    <p>
                        <strong>604-596-3924<br />
                            12033 - 84th Avenue, Surrey, BC<br />
                        </strong>
                        For more information, check out their website: <a href="http://www.scottsdalelanes.com" target="_blank">http://www.scottsdalelanes.com</a>
                    </p>

                </div>
            </div>
        </div>


        <h4 id="centre3">Bowling Lanes - 3</h4>
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-sm-3 centreLogo">
                    <img src="images/centre3.jpg" alt="Willowbrook Lanes" />
                </div>
                <div class="col-md-9 col-sm-9 centreDetails">
                    <p>
                        <strong>604-533-2695<br />
                            6350 - 196th Street, Langley, BC</strong><br />
                        For more information, check out their website: <a href="http://willowbrooklanes.ca" target="_blank">http://willowbrooklanes.ca</a>
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
