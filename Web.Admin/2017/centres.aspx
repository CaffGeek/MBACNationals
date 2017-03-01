<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_centres.jpg');"></div>
    <div id="photoCredit"><strong>Wascana Lake Shore</strong> &bull; Credit: Flickr Commons - daryl_mitchell</div>
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

        <h4 id="centre1">Glencairn Bowlodrome</h4>
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-sm-3 centreLogo">
                    <img src="images/centre1.jpg" alt="Glencairn Bowlodrome" />
                </div>
                <div class="col-md-9 col-sm-9 centreDetails">
                    <p>
                        <strong>1-306-789-0066<br />
                            1620 9th Avenue East, Regina<br />
                        </strong>
                        For more information, check out their website: <a href="glencairnbolodrome.com" target="_blank">glencairnbolodrome.com</a>
                    </p>
                </div>
            </div>
        </div>


        <h4 id="centre2">Golden Mile Bowling Lanes</h4>
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-sm-3 centreLogo">
                    <img src="images/centre2.jpg" alt="Golden Mile Bowling Lanes" />
                </div>
                <div class="col-md-9 col-sm-9 centreDetails">
                    <p>
                        <strong>1-306-586-2626<br />
                            3806 Albert Street, Regina<br />
                        </strong>
                        For more information, check out their website: <a href="goldenmilebowl.com" target="_blank">goldenmilebowl.com</a>
                    </p>

                </div>
            </div>
        </div>


        <h4 id="centre3">Nortown Lanes</h4>
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-sm-3 centreLogo">
                    <img src="images/centre3.jpg" alt="Nortown Lanes" />
                </div>
                <div class="col-md-9 col-sm-9 centreDetails">
                    <p>
                        <strong>1-306-525-2776<br />
                            6831 Rochdale Boulevard, Regina</strong><br />
                        For more information, check out their website: <a href="glencairnbolodrome.com" target="_blank">glencairnbolodrome.com</a>
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
