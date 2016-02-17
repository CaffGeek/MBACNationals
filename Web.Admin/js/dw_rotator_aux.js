/*************************************************************************
    This code is from Dynamic Web Coding at dyn-web.com
    Copyright 2008-2012 by Sharon Paine 
    See Terms of Use at www.dyn-web.com/business/terms.php
    regarding conditions under which you may use this code.
    This notice must be retained in the code as is!
    
    for use with dw_rotator.js
    version date: Oct 2010
*************************************************************************/


// display image at random
// rObj: object literal holding data 
// either uses document.write to insert, or returns string
function dw_getRandomImage(rObj, bReturn) {
    var imgAr = rObj.images;  if (!imgAr ) return '';
    var num = Math.floor( Math.random() * imgAr.length );
    var imgStr = '';   var imgFile = imgAr[ num ];
    rObj.num = num; // hold which img selected (for ctr)
    var path = rObj.path || ''; var id = rObj.id || '';
    var title, alt = '', url;
    // If there are *any* entries for actions, alt or title include them here 
    if (rObj.alt) {
        alt = rObj.alt[num]? rObj.alt[num]: rObj.alt[0]? rObj.alt[0]: '';
    }
    if (rObj.title) {
        title = rObj.title[num]? rObj.title[num]: rObj.title[0]? rObj.title[0]: '';
    }
    if (rObj.actions) {
        url = rObj.actions[num]? rObj.actions[num]: rObj.actions[0]? rObj.actions[0]: null;
    }
    if (url) {
        imgStr += '<a href="';
        imgStr += typeof url == 'string'? url: 'javascript: void ' + url;
        imgStr += rObj.loadNewWin? '" target="_blank">': '">';
    }
    
    imgStr += '<img src="' + path + imgFile + '"';
    imgStr += id? ' id="' + id + '"': '';
    if (title) {
        imgStr += ' title="' + title + '"';
    }
    imgStr += ' alt="' + alt + '" border="0" />';
    if (url) {
        imgStr += '</a>';
    }
    if (bReturn) {
        return imgStr;
    } else {
        document.write(imgStr); document.close();
        return '';
    }
}

/////////////////////////////////////////////////////////////////////
//  code to add stop/restart links

dw_Rotator.addControls = function() {
    var els = dw_getElementsByClassName('rotator_controls');
    for (var i=0; els[i]; i++) {
        var links = els[i].getElementsByTagName('a');
        for (var j=0; links[j]; j++) {
            if ( dw_hasClass( links[j], 'stop') ) {
                links[j].onclick = function () { dw_Rotator.stop(); return false }
            } else if ( dw_hasClass( links[j], 'start') ) {
                links[j].onclick = function () { dw_Rotator.restart(); return false }
            } 
        }
        els[i].style.display = 'block';
    }
}

// restart rotation for all instances 
dw_Rotator.restart = function() {
    dw_Rotator.start(dw_Rotator.resumeDelay); // start, but with shorter delay
}

function dw_hasClass(el, cl) {
    var re = new RegExp("\\b" + cl + "\\b", "i");
    if ( re.test( el.className ) ) {
        return true;
    }
    return false;
}

function dw_getElementsByClassName(sClass, sTag, oCont) {
    var result = [], list, i;
    var re = new RegExp("\\b" + sClass + "\\b", "i");
    oCont = oCont? oCont: document;
    if ( document.getElementsByTagName ) {
        if ( !sTag || sTag == "*" ) {
            list = oCont.all? oCont.all: oCont.getElementsByTagName("*");
        } else {
            list = oCont.getElementsByTagName(sTag);
        }
        for (i=0; list[i]; i++) 
            if ( re.test( list[i].className ) ) result.push( list[i] );
    }
    return result;
};


/////////////////////////////////////////////////////////////////////
// example use of function pointer in actions
// id: id by which the instance can be obtained using dw_Rotator.getInstanceById
// (id passed to constructor - id attached to img tag)
function displayImgInSubWin(id) {
    var rObj = dw_Rotator.getInstanceById(id);
    // notice access to properties of the instance available here 
    // file name could be based on the image file name, perhaps to display a larger version
    var file = rObj.imgs[rObj.ctr].src;
    openSubWin(file);
    return false;
}

// arguments: file to open, subwindow name, left, top, width, height, other attributes
// common attributes: (comma separator, no spaces!)
// "resizable,scrollbars,toolbar,location,directories,status,menubar"
// all but url are optional with defaults provided below 
function openSubWin(url, nm, x, y, w, h, atts) {
    nm = nm || "subwindow";
    atts = atts || "menubar,resizable,scrollbars";
    w = w || 600; h = h || 450;
    x = (typeof x=="number")? x: Math.round( (screen.availWidth - w)/2 );
    y = (typeof y=="number")? y: Math.round( (screen.availHeight - h)/2 );
    atts += ',width='+w+',height='+h+',left='+x+',top='+y;
    var win = window.open(url, nm, atts); 
    if ( win && !win.closed ) {
        win.focus(); 
    } 
    //return false; // if onclick 
}
