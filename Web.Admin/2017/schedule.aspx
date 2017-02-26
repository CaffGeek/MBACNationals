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
        <h3 id="day1" class='day'>Day 1 - Wednesday, June 28th</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>Delta/Mariott</span><br />Registrations for Early Arrivals<br />Participants & Guests<br /></p>
        <h3 id="day2" class='day'>Day 2 - Thursday, June 29th</h3><h4 class='time'>8:00 am - 4:00 pm</h4><p class='details'><span class='location'>Delta/Mariott</span><br />Registrations for Early Arrivals<br />Participants & Guests<br /></p><h4 class='time'>11:30 am - 3:00 pm</h4><p class='details'><span class='location'>TBD</span><br />Practice Lanes Available<br />Participants<br /></p><h4 class='time'>4:00 pm - 5:00 pm</h4><p class='details'><span class='location'>Delta/Mariott</span><br />Officials Meeting - Judges of Play<br /></p><h4 class='time'>5:00 pm - 6:00 pm</h4><p class='details'><span class='location'>Delta/Mariott</span><br />Coaches/Managers Meeting<br />All Divisions<br /></p><h4 class='time'>6:00 pm - 7:30 pm</h4><p class='details'><span class='location'>Delta/Mariott</span><br />Team & Provincial Photos<br /></p><h4 class='time'>8:00 pm - Midnight</h4><p class='details'><span class='location'>Delta/Mariott</span><br />Opening Ceremonies & Meet & Greet<br />Participants & Guests<br /></p>
        <h3 id="day3" class='day'>Day 3 - Friday, June 30th</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>
        <h3 id="day4" class='day'>Day 4 - Saturday, July 1st</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>
        <h3 id="day5" class='day'>Day 5 - Sunday, July 2nd</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>
        <h3 id="day6" class='day'>Day 6 - Monday, July 3rd</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>
        <h3 id="day7" class='day'>Day 7 - Tuesday, July 4th</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>
    </div>
</asp:Content>