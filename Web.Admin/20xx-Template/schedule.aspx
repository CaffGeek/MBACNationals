<%@ Language="C#" MasterPageFile="~/MBAC.Master" AutoEventWireup="false" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceholder" runat="server">
    <div id="headerImage" style="margin-top:15px;background-size:cover;background-position:center center;height:375px;background-image:url('images/header_image_schedule.jpg'); "></div>
    <div id="photoCredit"><strong>TODO: description</strong> &bull; Credit: TODO: credit</div>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" runat="server">
    <div class="col-md-2">
        <ul class="sidebarNav">
       		<li><a href="#day1">Day 1</a></li>
            <li><a href="#day2">Day 2</a></li>
            <li><a href="#day3">Day 3</a></li>
            <li><a href="#day4">Day 4</a></li>
            <li><a href="#day5">Day 5</a></li>
            <li><a href="#day6">Day 6</a></li>
            <li><a href="#day7">Day 7</a></li>
        </ul>         
    </div>

    <div class="col-md-10">
        <h2>SCHEDULE</h2>
        <h3 id="day1" class='day'>Day 1 - TODO: Date</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>
        <h3 id="day2" class='day'>Day 2 - TODO: Date</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>
        <h3 id="day3" class='day'>Day 3 - TODO: Date</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>
        <h3 id="day4" class='day'>Day 4 - TODO: Date</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>
        <h3 id="day5" class='day'>Day 5 - TODO: Date</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>
        <h3 id="day6" class='day'>Day 6 - TODO: Date</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>
        <h3 id="day7" class='day'>Day 7 - TODO: Date</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>
    </div>
</asp:Content>