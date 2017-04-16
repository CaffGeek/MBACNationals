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

    <div class="col-md-10">
        <h2>Souvenirs</h2>
        <div class="row">
        	<div class="col-md-5"><img src="http://mbacnationals.com/2017/images/souvenirs/MorenoShirt.jpg" alt="moreno shirt" /></div>
        	<div class="col-md-5">
        	<form class="form-horizontal">
<fieldset>

<!-- Form Name -->
<legend>Moreno Shirt - $40</legend>

<!-- Multiple Radios (inline) -->
<div class="form-group">
  <label class="col-md-4 control-label" for="Mens">Mens</label>
  <div class="col-md-4"> 
    <label class="radio-inline" for="Mens-0">
      <input type="radio" name="Mens" id="Mens-0" value="Mens" checked="checked">
      Mens
    </label>
  </div>
</div>

<!-- Select Basic -->
<div class="form-group">
  <label class="col-md-4 control-label" for="menssize">Mens Size</label>
  <div class="col-md-4">
    <select id="menssize" name="menssize" class="form-control">
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
<div class="form-group">
  <label class="col-md-4 control-label" for="ladies">Ladies</label>
  <div class="col-md-4"> 
    <label class="radio-inline" for="ladies-0">
      <input type="radio" name="ladies" id="ladies-0" value="Ladies" checked="checked">
      Ladies
    </label>
  </div>
</div>

<!-- Select Basic -->
<div class="form-group">
  <label class="col-md-4 control-label" for="ladiessize">Ladies Size</label>
  <div class="col-md-4">
    <select id="ladiessize" name="ladiessize" class="form-control">
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

</fieldset>
</form>

        </div>	
        </div>
        <div class="row">
        	<div class="col-md-5"><img src="http://mbacnationals.com/2017/images/souvenirs/L00660L1.jpg" alt="hoodies" /></div>
        	<div class="col-md-5">
        	<form class="form-horizontal">
<fieldset>

<!-- Form Name -->
<legend>Bunny Hug - $45</legend>

<!-- Multiple Radios (inline) -->
<div class="form-group">
  <label class="col-md-4 control-label" for="Mens">Mens</label>
  <div class="col-md-4"> 
    <label class="radio-inline" for="Mens-0">
      <input type="radio" name="Mens" id="Mens-0" value="Mens" checked="checked">
      Mens
    </label>
  </div>
</div>

<!-- Select Basic -->
<div class="form-group">
  <label class="col-md-4 control-label" for="menssize">Mens Size</label>
  <div class="col-md-4">
    <select id="menssize" name="menssize" class="form-control">
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
<div class="form-group">
  <label class="col-md-4 control-label" for="ladies">Ladies</label>
  <div class="col-md-4"> 
    <label class="radio-inline" for="ladies-0">
      <input type="radio" name="ladies" id="ladies-0" value="Ladies" checked="checked">
      Ladies
    </label>
  </div>
</div>

<!-- Select Basic -->
<div class="form-group">
  <label class="col-md-4 control-label" for="ladiessize">Ladies Size</label>
  <div class="col-md-4">
    <select id="ladiessize" name="ladiessize" class="form-control">
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
</fieldset>
</form>
		</div>
        </div>
        <div class="row">
        	<div class="col-md-5"><img src="http://mbacnationals.com/2017/images/souvenirs/L00671L1.jpg" alt="jackets" /></div>
        	<div class="col-md-5">
        	<form class="form-horizontal">
<fieldset>

<!-- Form Name -->
<legend>Jackets - $55</legend>

<!-- Multiple Radios (inline) -->
<div class="form-group">
  <label class="col-md-4 control-label" for="Mens">Mens</label>
  <div class="col-md-4"> 
    <label class="radio-inline" for="Mens-0">
      <input type="radio" name="Mens" id="Mens-0" value="Mens" checked="checked">
      Mens
    </label>
  </div>
</div>

<!-- Select Basic -->
<div class="form-group">
  <label class="col-md-4 control-label" for="menssize">Mens Size</label>
  <div class="col-md-4">
    <select id="menssize" name="menssize" class="form-control">
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
<div class="form-group">
  <label class="col-md-4 control-label" for="ladies">Ladies</label>
  <div class="col-md-4"> 
    <label class="radio-inline" for="ladies-0">
      <input type="radio" name="ladies" id="ladies-0" value="Ladies" checked="checked">
      Ladies
    </label>
  </div>
</div>

<!-- Select Basic -->
<div class="form-group">
  <label class="col-md-4 control-label" for="ladiessize">Ladies Size</label>
  <div class="col-md-4">
    <select id="ladiessize" name="ladiessize" class="form-control">
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
</fieldset>
</div>
</div>
        <div class="row">
        	<div class="col-md-5"><img src="http://mbacnationals.com/2017/images/souvenirs/towel.jpg" alt="towel" /></div>
        	<div class="col-md-5">

<!-- Form Name -->
<legend>Towels - $15</legend>

<!-- Multiple Radios (inline) -->
<div class="form-group">
  <label class="col-md-4 control-label" for="Towel">Towel</label>
  <div class="col-md-4"> 
    <label class="radio-inline" for="Towel">
      <input type="radio" name="Towel" id="Towel" value="Towel" checked="checked">
      Towel
    </label>
  </div>
</div>
</div>
</div>
        <div class="row">
        	<div class="col-md-5"><img src="http://mbacnationals.com/2017/images/souvenirs/coozie.jpg" alt="koozies" /></div>
        	<div class="col-md-5">

<!-- Form Name -->
<legend>Koozies - $15</legend>

<!-- Multiple Radios (inline) -->
<div class="form-group">
  <label class="col-md-4 control-label" for="Koozies">Koozies</label>
  <div class="col-md-4"> 
    <label class="radio-inline" for="Koozies">
      <input type="radio" name="Koozies" id="Koozies" value="Koozies" checked="checked">
      Koozies
    </label>
  </div>
</div>
</div>
</div>

</form>

        	</div>
        </div>	

        <p>Moreno Shirts  $40.00 The tangerine is the only color that will not be offered on the website as the officials will be wearing them. 
Bunny Hugs  $45.00 for either style
Jackets men and ladies  $55.00  Men tall starting at Large  $65.00 
I am attaching pictures of the towels and Cozies for water bottles that will be available on line. They will both be $15 unless they want them personalized, then there will be a $1.50 charge for personalizing them, increasing the cost to $16.50. Not sure if Judy got back to you with the shirts, jackets, bunny hugs. I will need to know how many of these two items have been ordered so I can let that supplier know. We would like to have on-line sales start by the middle of April, with cut off June 1. We will have towels at the event, but will not be able to personalize them, and water bottle Coozies will only be on-line sales. Thanks.</p>
    </div>
</asp:Content>
