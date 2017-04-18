<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_photos.jpg');"></div>
    <div id="photoCredit"><strong>Regina</strong> &bull; Credit: Flickr Commons - Blake Handley</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <div class="col-md-2">
        <ul class="sidebarNav">
            <li>instructions</li>
        </ul>
    </div>

<div class="col-md-10" id="souvenirs">
<h2>Souvenirs</h2>
<form method="post">
<div class="form-group ">

<div class="row">
  <div class="col-md-5"><img class="souvenirs" src="http://mbacnationals.com/2017/images/souvenirs/MorenoShirt.jpg" alt="moreno shirt" /></div>
  <div class="col-md-5">

<!-- Form Name -->
<legend>Moreno Shirt - $40</legend>

<!-- Multiple Radios (inline) -->
<div class="row form-group">
  <label class="col-md-4 control-label" for="Mens" style="margin-top:10px;">Mens</label>
  <div class="col-md-4"> 
    <div class="checkbox">
    <label for="mens-0">
      <input type="checkbox" name="morenoMens" id="mens-0" value="mens">
      Mens
    </label>
	</div>
  </div>
</div>

<!-- Select Basic -->
<div class="row form-group">
  <label class="col-md-4 control-label" for="menssize" style="margin-top:10px;">Mens Size</label>
  <div class="col-md-4">
    <select id="menssize" name="morenoMenssize" class="form-control">
      <option value="S">S</option>
      <option value="M">M</option>
      <option value="L">L</option>
      <option value="XL">XL</option>
      <option value="XXL">XXL</option>
      <option value="XXXL">XXXL</option>
      <option value="XXXXL">XXXXL</option>
      <option value="XXXXXL">XXXXXL</option>
    </select>
  </div>
</div>
<!-- Multiple Radios (inline) -->
<div class="row form-group">
  <label class="col-md-4 control-label" for="ladies" style="margin-top:10px;">Ladies</label>
  <div class="col-md-4"> 
    <div class="checkbox">
    <label for="ladies-0">
      <input type="checkbox" name="morenoLadies" id="ladies-0" value="Ladies">
      Ladies
    </label>
	</div>
  </div>
</div>

<!-- Select Basic -->
<div class="row form-group">
  <label class="col-md-4 control-label" for="ladiessize" style="margin-top:10px;">Ladies Size</label>
  <div class="col-md-4">
    <select id="ladiessize" name="morenoLadiessize" class="form-control">
      <option value="XS">XS</option>
      <option value="S">S</option>
      <option value="M">M</option>
      <option value="L">L</option>
      <option value="XL">XL</option>
      <option value="XXL">XXL</option>
      <option value="XXXL">XXXL</option>
    </select>
  </div>
</div>

<!-- Select Basic -->
<div class="row form-group">
  <label class="col-md-4 control-label" for="colour" style="margin-top:10px;">Colour</label>
  <div class="col-md-4">
    <select id="colour" name="morenoColour" class="form-control">
      <option value="White">White</option>
      <option value="Stone">Stone</option>
      <option value="Mocha">Mocha</option>
      <option value="Pink">Pink Zircon - Ladies Only</option>
      <option value="Red">Red</option>
      <option value="Maroon">Maroon</option>
      <option value="Chill">Chill</option>
      <option value="OlympicBlue">Olympic Blue</option>
      <option value="Navy">Navy</option>
      <option value="ForestGreen">Forest Green</option>    
      <option value="GreenTea">Green Tea</option>
      <option value="SteelGrey">Steel Grey</option>
      <option value="Black">Black</option>
      </select>
  </div>
</div>

</div>

<hr />

<div class="row">
  <div class="col-md-5"><img  class="souvenirs"  src="http://mbacnationals.com/2017/images/souvenirs/L00660L1.jpg" alt="hoodies" /></div>
  <div class="col-md-5">

<!-- Form Name -->
<legend>Bunny Hug - $45</legend>

<!-- Multiple Radios (inline) -->
<div class="row form-group">
  <label class="col-md-4 control-label" for="Mens" style="margin-top:10px;">Mens</label>
  <div class="col-md-4"> 
    <div class="checkbox">
    <label for="mens-0">
      <input type="checkbox" name="morenoMens" id="mens-0" value="mens">
      Mens
    </label>
	</div>
  </div>
</div>

<!-- Select Basic -->
<div class="row form-group">
  <label class="col-md-4 control-label" for="menssize" style="margin-top:10px;">Mens Size</label>
  <div class="col-md-4">
    <select id="menssize" name="morenoMenssize" class="form-control">
      <option value="S">S</option>
      <option value="M">M</option>
      <option value="L">L</option>
      <option value="XL">XL</option>
      <option value="XXL">XXL</option>
      <option value="XXXL">XXXL</option>
      <option value="XXXXL">XXXXL</option>
      <option value="XXXXXL">XXXXXL</option>
    </select>
  </div>
</div>
<!-- Multiple Radios (inline) -->
<div class="row form-group">
  <label class="col-md-4 control-label" for="ladies" style="margin-top:10px;">Ladies</label>
  <div class="col-md-4"> 
    <div class="checkbox">
    <label for="ladies-0">
      <input type="checkbox" name="morenoLadies" id="ladies-0" value="Ladies">
      Ladies
    </label>
	</div>
  </div>
</div>

<!-- Select Basic -->
<div class="row form-group">
  <label class="col-md-4 control-label" for="ladiessize" style="margin-top:10px;">Ladies Size</label>
  <div class="col-md-4">
    <select id="ladiessize" name="morenoLadiessize" class="form-control">
      <option value="XS">XS</option>
      <option value="S">S</option>
      <option value="M">M</option>
      <option value="L">L</option>
      <option value="XL">XL</option>
      <option value="XXL">XXL</option>
      <option value="XXXL">XXXL</option>
    </select>
  </div>
</div>

<!-- Select Basic -->
<div class="form-group">
  <label class="col-md-4 control-label" for="colour">Colour</label>
  <div class="col-md-4">
    <select id="colour" name="colour" class="form-control">
      <option value="Black">Black</option>
      <option value="Red">Red</option>
      <option value="Blue">Blue</option>
      <option value="Navy">Navy</option>
      <option value="Grey">Grey</option>
      </select>
  </div>
</div>

</div>

<hr />

</div>
</form>
</div>
</asp:Content>
