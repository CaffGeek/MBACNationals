
$(window).scroll(function () {
    if ($(this).scrollTop() > 100) {
        $('.navbar-fixed-top').addClass('opaque');
    } else {
        $('.navbar-fixed-top').removeClass('opaque');
    }
});
function schedule() {
    var now = new Date();
    var date = now.getDate();
    var month = now.getMonth();
    var month = month + 1;
    var yyyy = now.getFullYear(); //yields year

    var msg;

    //Day 1 at bottom, because it's the default
    if (yyyy == 2016 && month == 6 && date == 28)
        msg = "<h3 class='day'>Day 2 - TODO: Date</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>";
    else if (yyyy == 2016 && month == 6 && date == 29)
        msg = "<h3 class='day'>Day 3 - TODO: Date</h3><h4 class='time'>8:00 AM</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>";
    else if (yyyy == 2016 && month == 6 && date == 30)
        msg = "<h3 class='day'>Day 4 - TODO: Date</h3><h4 class='time'>7:30 AM</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>";
    else if (yyyy == 2016 && month == 7 && date == 1)
        msg = "<h3 class='day'>Day 5 - TODO: Date</h3><h4 class='time'>8:00 AM</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>";
    else if (yyyy == 2016 && month == 7 && date == 2)
        msg = "<h3 class='day'>Day 6 - TODO: Date</h3><h4 class='time'>8:00 AM</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>";
    else if (yyyy == 2016 && month == 7 && date == 3)
        msg = "<h3 class='day'>Day 7 - TODO: Date</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>";
    else
        msg = "<h3 class='day'>Day 1 - TODO: Date</h3><h4 class='time'>All Day</h4><p class='details'><span class='location'>TODO: Location</span><br /></p>";

    $('.message').html(msg);  //add message to the element with class message
}
schedule();