

      <hr>

      <footer>
      	<ul class="footerNav">
      			<li><a href="index.php">Home</a></li>
                <li><a href="results.php">Results</a></li>
                <li><a href="news.php">News</a></li>
                <li><a href="schedule.php">Schedule</a></li>
                <li><a href="lanedraw.php">Lane Draw</a></li>
                <li><a href="photos.php">Photos</a></li>
                <li><a href="contingents.php">Contingents</a></li>
                <li><a href="souvenirs.php">Souvenirs</a></li>
                <li><a href="centres.php">Centres</a></li>
                <li><a href="hotel.php">Hotel</a></li>
    	</ul>
        <p>&copy; Site Design by Charlene McIvor & Chad Hurd</p>
      </footer>
    </div> <!-- /container -->


    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script>window.jQuery || document.write('<script src="assets/js/vendor/jquery.min.js"><\/script>')</script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="bootstrap/js/ie10-viewport-bug-workaround.js"></script>
    <script>
    $(window).scroll(function() {
    if($(this).scrollTop() > 100) {
        $('.navbar-fixed-top').addClass('opaque');
    } else {
        $('.navbar-fixed-top').removeClass('opaque');
    }
});
function schedule()
  {
    var now = new Date();
    var date = now.getDate();
    var month = now.getMonth();
    var month = month + 1;
    var yyyy = now.getFullYear(); //yields year

    var msg;
    if (yyyy == 2016 && month == 6 && date == 27)
      msg = "<h3 class='day'>Day 1 - Monday June 27</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>Sheraton Hotel</span><br />Registrations for Early Arrivals<br />Participants & Guests<br /></p>";
    else if(yyyy == 2016 && month == 6 && date == 28)
      msg = "<h3 class='day'>Day 2 - Tuesday June 28</h3><h4 class='time'>8:00 AM - 4:00 PM</h4><p class='details'><span class='location'>Barnston Room</span><br />Registration</p><h4 class='time'>11:00 AM - 4:30 PM</h4><p class='details'><span class='location'>TBA</span><br />Team & Singles Practice Times at Starting Centres</p><h4 class='time'>5:00 PM</h4><p class='details'><span class='location'>Dogwood Room</span><br />Coach's, Manager's & Tournament Singles Meeting</p><h4 class='time'>5:30 PM</h4><p class='details'><span class='location'>Dogwood Room</span><br />Tournament Officials Meeting</p><h4 class='time'>7:00 PM - 10:00 PM</h4><p class='details'><span class='location'>Guildford Ballroom</span><br />Opening Ceremonies - Meet & Greet</p><h4 class='time'>10:00 PM - 12:00 AM</h4><p class='details'><span class='location'>Tynhead Room 1</span><br />Team & Provincial Photos</p>";
    else if(yyyy == 2016 && month == 6 && date == 29)
      msg = "<h3 class='day'>Day 3 - Wednesday June 29</h3><h4 class='time'>8:00 AM</h4><p class='details'><span class='location'>Sheraton</span><br />Buses Depart</p><h4 class='time'>8:30 AM - 9:00 AM</h4><p class='details'><span class='location'>All Centres</span><br />Warm Up & Announcements</p><h4 class='time'>9:00 AM - 5:45 PM</h4><p class='details'><span class='location'>All Centres</span><br />All Teams Bowl 6 Games</p><h4 class='time'>9:00 AM - 1:00 PM</h4><p class='details'><span class='location'>Scottsdale Lanes</span><br />Singles Bowl 7 Games</p><h4 class='time'>1:30 PM</h4><p class='details'><span class='location'>Scottsdale Lanes</span><br />Singles - Shuttle Vans Depart</p><h4 class='time'>6:00 PM</h4><p class='details'><span class='location'>All Centres</span><br />Buses Depart</p><h4 class='time'>7:00 PM</h4><p class='details'><br />Free Night</p>";
    else if(yyyy == 2016 && month == 6 && date == 30)
      msg = "<h3 class='day'>Day 4 - Thursday June 30</h3><h4 class='time'>7:30 AM</h4><p class='details'><span class='location'>Sheraton</span><br />Buses Depart</p><h4 class='time'>8:00 AM - 8:30 AM</h4><p class='details'><span class='location'>All Centres</span><br />Warm Up & Announcements</p><h4 class='time'>8:30 AM - 5:45 PM</h4><p class='details'><span class='location'>All Centres</span><br />All Teams Bowl 6 Games</p><h4 class='time'>9:00 AM - 1:00 PM</h4><p class='details'><span class='location'>Scottsdale Lanes</span><br />Singles Bowl 7 Games</p><h4 class='time'>1:30 PM</h4><p class='details'><span class='location'>Scottsdale Lanes</span><br />Singles - Shuttle Vans Depart</p><h4 class='time'>6:00 PM</h4><p class='details'><span class='location'>All Centres</span><br />Buses Depart</p><h4 class='time'>6:00 PM - 6:30PM</h4><p class='details'><span class='location'>Guildford Ballroom</span><br />Cocktails</p><h4 class='time'>6:30 PM - 1:00AM</h4><p class='details'><span class='location'>Guildford Ballroom</span><br />B.C. Night - Dirty Thirties Theme</p>";
    else if(yyyy == 2016 && month == 7 && date == 1)
      msg = "<h3 class='day'>Day 5 - Friday July 1</h3><h4 class='time'>8:00 AM</h4><p class='details'><span class='location'>Sheraton</span><br />Buses Depart</p><h4 class='time'>8:30 - 9:00 AM</h4><p class='details'><span class='location'>All Centres</span><br />Warm Up & Announcements</p><h4 class='time'>9:00 AM - 5:45 PM</h4><p class='details'><span class='location'>All Centres</span><br />All Teams Bowl 6 Games</p><h4 class='time'>9:00 AM - 1:00 PM</h4><p class='details'><span class='location'>Willowbrook Lanes</span><br />Singles Bowl 7 Games</p><h4 class='time'>1:00 PM - 2:00 PM</h4><p class='details'><span class='location'>Willowbrook Lanes</span><br />Singles Tie Breakers if Necessary</p><h4 class='time'>2:30 PM</h4><p class='details'><span class='location'>Willowbrook Lanes</span><br />Singles - Shuttle Vans Depart</p><h4 class='time'>5:45 PM</h4><p class='details'><span class='location'>All Centres</span><br />Buses Depart</p><h4 class='time'>6:30 PM - 8:00PM</h4><p class='details'><span class='location'>Sheraton/Holliday Park</span><br />Shuttle Service for Canada Day Celebrations</p><h4 class='time'>8:00 PM - 1:00 AM</h4><p class='details'><span class='location'>Barnston Room</span><br />Hospitality Room</p><h4 class='time'>11:00PM</h4><p class='details'><span class='location'>Barnston Room</span><br />Elimination Draw</p>";
    else if(yyyy == 2016 && month == 7 && date == 2)
      msg = "<h3 class='day'>Day 6 - Saturday July 2</h3><h4 class='time'>8:00 AM</h4><p class='details'><span class='location'>Sheraton</span><br />Buses Depart</p><h4 class='time'>8:30 - 9:00 AM</h4><p class='details'><span class='location'>All Centres</span><br />Warm Up & Announcements</p><h4 class='time'>9:00 AM - 1:00 PM</h4><p class='details'><span class='location'>All Centres</span><br />All Teams Bowl 3 Games</p><h4 class='time'>12:30 PM</h4><p class='details'><span class='location'>Sheraton</span><br />Singles - Shuttle Vans Depart</p><h4 class='time'>1:00 PM</h4><p class='details'><span class='location'>All Centres</span><br />Buses Depart</p><h4 class='time'>2:00 PM</h4><p class='details'><span class='location'>Scottsdale Lanes</span><br />Tournament Singles Bronze Medal Game</p><h4 class='time'>2:45 PM</h4><p class='details'><span class='location'>Scottsdale Lanes</span><br />Tournament Singles Gold Medal Game</p><h4 class='time'>3:30 PM</h4><p class='details'><span class='location'>Scottsdale Lanes</span><br />Buses Depart</p><h4 class='time'>5:00 PM - 6:00 PM</h4><p class='details'><span class='location'>Green Timbers Room 1</span><br />Cocktails - Delegates</p><h4 class='time'>5:00 PM - 6:00 PM</h4><p class='details'><span class='location'>Guildford Ballroom</span><br />Cocktails - Participants & Guests</p><h4 class='time'>6:00PM - 6:30 PM</h4><p class='details'><span class='location'>Guildford Ballroom</span><br />Victory Banquet - Parade of Provinces</p><h4 class='time'>6:30PM - 9:30 PM</h4><p class='details'><span class='location'>Guildford Ballroom</span><br />Victory Banquet - Dinner & Awards</p><h4 class='time'>9:30PM - 1:00 AM</h4><p class='details'><span class='location'>Guildford Ballroom</span><br />Victory Banquet - Dance</p>";
    else if(yyyy == 2016 && month == 7 && date == 3)
      msg = "<h3 class='day'>Day 7 - Sunday July 3</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>Vancouver & Abottsford</span><br />Various Departures</p><h4 class='time'>10:00 AM - 5:00 PM</h4><p class='details'><span class='location'>Green Timbers Room 1</span><br />Provincial Delegates Meeting</p>";
    else
      msg = "<h3 class='day'>Day 1 - Monday June 27</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>Sheraton Hotel</span><br />Registrations for Early Arrivals<br />Participants & Guests<br /></p>";

  $('.message').html(msg);  //add message to the element with class message
}
schedule();


</script>
  </body>
</html>
