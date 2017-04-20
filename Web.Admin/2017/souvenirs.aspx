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
<div class="col-xs-12 col-md-6">
 
                <!-- First product box start here-->
                <div class="prod-info-main prod-wrap clearfix">
 
                      <div class="row">
 
                           <div class="col-md-5 col-sm-12 col-xs-12">
 
                            <div class="product-image">
 
                             <img src="images/products/p4.png" class="img-responsive">
 
                               <span class="tag2 hot">
 
                                 SPECIAL
 
                               </span>
 
                       </div>
 
                  </div>
 
    <div class="col-md-7 col-sm-12 col-xs-12">
 
                  <div class="product-deatil">
 
                               <h5 class="name">
 
                               <a href="#">
 
                                Product Code/Name here
 
                               </a>
 
                               <a href="#">
 
                               <span>Product Category</span>
 
                               </a>                           
 
                        </h5>
 
                            <p class="price-container">
 
                             <span>$199</span>
 
                           </p>
 
              <span class="tag1"></span>
 
   </div>
 
  <div class="description">
 
      <p>A Short product description here </p>
 
   </div>
 
      <div class="product-info smart-form">
 
         <div class="row">
 
         <div class="col-md-12">
 
          <a href="#" class="btn btn-danger">Add to cart</a>
 
             <a href="javascript:void(0);" class="btn btn-info">More info</a>
 
       </div>
 
      <div class="col-md-12">
 
         <div class="rating">Rating:
 
          <label for="stars-rating-5"><i class="fa fa-star text-danger"></i></label>
 
          <label for="stars-rating-4"><i class="fa fa-star text-danger"></i></label>
 
          <label for="stars-rating-3"><i class="fa fa-star text-danger"></i></label>
 
          <label for="stars-rating-2"><i class="fa fa-star text-warning"></i></label>
 
          <label for="stars-rating-1"><i class="fa fa-star text-warning"></i></label>
 
         </div>
 
       </div>
 
    </div>
 
   </div>
 
  </div>
 
  </div>
</div>
 
<!-- end product -->
 
</div>
</div>
</asp:Content>
