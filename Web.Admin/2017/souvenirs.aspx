<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top: 15px; background-size: cover; background-position: center center; height: 375px; background-image: url('images/header_image_photos.jpg');"></div>
    <div id="photoCredit"><strong>Regina</strong> &bull; Credit: Flickr Commons - Blake Handley</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <div class="col-md-2">
        <ul class="sidebarNav">
            <li>instructions</li>
            <p>Souvenirs will only be available for pre-order <strong>until June 1st!</strong> So please place orders as soon as possible. </p>
            <p>Customization of Towels & Koozies is only available by pre-order online and Koozies are only available online.</p>
        </ul>
    </div>

<div class="col-md-10" id="souvenirs">
<h2>Souvenirs</h2>

<p>To order, please download and fill out this <a href="2017_Souvenirs.xls">order form</a> and email it to Judy @ <a href="mailto:classicmakings@yourlink.ca">classicmakings@yourlink.ca</a> </p>

<form method="post">

<div class="row">
  <div class="col-md-5"><img class="souvenirs" src="/2017/images/souvenirs/MorenoShirt.jpg" alt="moreno shirt" /></div>
  <div class="col-md-7">

<h4>Moreno Shirt - $40</h4>
<p>Advanced WEBTech™ 100 wicking fabric offers breathable moisture
management. Classic flat knit collar with long, slimming two-button
placket (women's). Contrast 'X' shaped bartacks at slits and taping on
inside of slits.</p>
<h5>Sizes Available:</h5>
<ul>
<li><strong>Mens</strong> S - 5XL</li>
<li><strong>Ladies</strong> XS - 3XL</li>
</ul>
<h5>Colours Available:</h5>
<ul>
<li>White</li>
<li>Mocha</li>
<li>Pink Zircon (Ladies Only)</li>
<li>Red</li>
<li>Maroon</li>
<li>Chill</li>
<li>Olympic Blue</li>
<li>Navy</li>
<li>Forest Green</li>
<li>Green Tea</li>
<li>Steel Grey</li>
<li>Black</li>
</ul>
</div>

<hr />

<div class="row">
  <div class="col-md-5"><img  class="souvenirs"  src="/2017/images/souvenirs/L00660L1.jpg" alt="hoodies" /></div>
  <div class="col-md-7">

<!-- Form Name -->
<legend>Bunny Hug - $45</legend>
<p>80% Cotton / 20% polyester ringspun cotton blended fleece pullover hoodie (280gsm). Double layer hood lined with jersey. Contrast chevron tape at neck seam. Adjustable contrast flat draw cord. Double layer ribbed cuff and hem with lycra.</p>
<h5>Sizes Available:</h5>
<ul>
<li><strong>Mens</strong> S - 4XL</li>
<li><strong>Ladies</strong> XS - 2XL</li>
</ul>
<h5>Colours Available:</h5>
<ul>
<li>Black</li>
<li>Red</li>
<li>Blue</li>
<li>Navy</li>
<li>Grey</li>
</ul>
</div>

<hr />

<div class="row">
        	<div class="col-md-5"><img  class="souvenirs" src="/2017/images/souvenirs/L00671L1.jpg" alt="zip hoodie" /></div>
  <div class="col-md-7">

<!-- Form Name -->
<legend>Zip Hoodie - $45</legend>

<p>80% Cotton / 20% polyester ringspun cotton blended fleece pullover hoodie (280gsm). Double layer hood lined with jersey. Contrast chevron tape at neck seam. Adjustable contrast flat draw cord. YKK aluminum metal front zippered closure. Double layer ribbed cuff and hem with lycra.</p>
<h5>Sizes Available:</h5>
<ul>
<li><strong>Mens</strong> S - 4XL</li>
<li><strong>Ladies</strong> XS - 2XL</li>
</ul>
<h5>Colours Available:</h5>
<ul>
<li>Black</li>
<li>Red</li>
<li>Blue</li>
<li>Navy</li>
<li>Grey</li>
</ul>
</div>

<hr />

<div class="row">
        	<div class="col-md-5"><img  class="souvenirs" src="/2017/images/souvenirs/L00671L1.jpg" alt="zip hoodie" /></div>
  <div class="col-md-7">

<!-- Form Name -->
<legend>Soft-Shell Jackets - $55</legend>

<!-- Multiple Radios (inline) -->
<div class="row form-group">
  <label class="col-md-4 control-label" for="Mens" style="margin-top:10px;">Mens</label>
  <div class="col-md-4"> 
    <div class="checkbox">
    <label for="mens-0">
      <input type="checkbox" name="jacketMens" id="mens-0" value="mens">
      Mens
    </label>
	</div>
  </div>
</div>

<!-- Select Basic -->
<div class="row form-group">
  <label class="col-md-4 control-label" for="menssize" style="margin-top:10px;">Mens Size</label>
  <div class="col-md-4">
    <select id="menssize" name="jacketMenssize" class="form-control">
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
      <input type="checkbox" name="jacketLadies" id="ladies-0" value="Ladies">
      Ladies
    </label>
	</div>
  </div>
</div>

<!-- Select Basic -->
<div class="row form-group">
  <label class="col-md-4 control-label" for="ladiessize" style="margin-top:10px;">Ladies Size</label>
  <div class="col-md-4">
    <select id="ladiessize" name="jacketLadiessize" class="form-control">
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
    <select id="colour" name="jacketColour" class="form-control">
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

<div class="row">
	<div class="col-md-5"><img  class="souvenirs" src="/2017/images/souvenirs/towel.jpg" alt="towel" /></div>
	<div class="col-md-7">

<!-- Form Name -->
<legend>Towel - $15</legend>
<p>Towel customization is available by online pre-order only.</p>

</div>
<hr />


<div class="row">
        	<div class="col-md-5"><img  class="souvenirs" src="/2017/images/souvenirs/coozie.jpg" alt="koozie" /></div>
  <div class="col-md-7">

<!-- Form Name -->
<legend>Bottle Koozie - $15<br />
<strong>ONLY AVAILABLE ONLINE</Strong></legend>

<p>Bottle koozies are only available by online pre-order.</p>

</div>


</div>

<hr />
</form>
</div>
</asp:Content>
