var oId;
var oM;
$('#carouselExampleControls').carousel({
    wrap: false
});

$('#carouselExampleControl').carousel({
    wrap: false
});


$(document).on('click', '#chum .dropdown-menu', function (e) {
    e.stopPropagation();
});
$(document).on('click', '.brn', function (e) {
    e.stopPropagation();
});


function getInfo() {
    var fullName = $('#varS').attr("value");
    var diskNr = $('#diskNr').attr("value");

    $.ajax({
        type: 'GET',
        url: '/demo2/' + fullName + "/" + diskNr,
        success: function (result) {

            oM = result;
            oId = result[3];

        }
    });
};

var dif;
function sDifficulty(that) {
    document.getElementById("diff").innerHTML = that.innerHTML;
    document.getElementById("diff").value = that.getAttribute("value");

}

var place;
function postResult(nMoves, uId, cId, eTime) {


    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();


    var parm = { __RequestVerificationToken: token, Moves: nMoves, IdentityUserID: uId, OptimalID: cId, Elapsed: eTime };

    $.ajax({
        url: "Home/Create/",
        type: 'POST',
        data: parm, dataType: "json", success: function (result) { place = result; document.getElementById("place").innerHTML = "You reached <b>" + result + ".</b> place in this category.";}
    })


};

var layout;
var stDis;

function getScores() {

    $.ajax({
        type: 'GET',
        url: '/results/' + layout + "/" + stDis,
        success: function (result) {
            setTable(result);
        }
    });
};

var oR;
function setTable(info) {
    var table = document.getElementById("table");
    //if (oR == info || info.length == 0) { table.parentElement.classList.add("d-none"); return; }
    //oR = info;
    var table1 = document.getElementById("userName");

    if (info.length > 0 && table.parentElement.classList.contains("d-none") == true) { table.parentElement.classList.remove("d-none"); }
    if (info.length == 0 && table.parentElement.classList.contains("d-none") == false) { table.parentElement.classList.add("d-none"); }


    while (table1.firstChild) {
        table1.removeChild(table1.firstChild);
    }

    for (i = 0; i < info.length; i++) {
        var sm = document.createElement("tr");

        var row = document.createElement("td");
        var row1 = document.createElement("td");
        var row2 = document.createElement("td");
        var row3 = document.createElement("td");


        var convert = (info[i].elapsed).substring(0, (info[i].elapsed.length) - 5);

        row.innerHTML = info[i].userName;
        row1.innerText = convert;
        row2.innerText = info[i].moves;
        row3.innerText = i + 1;

        sm.appendChild(row3);
        sm.appendChild(row);
        sm.appendChild(row1);
        sm.appendChild(row2);


        table1.appendChild(sm);
    }



}

function animationSet() {
    var anb = document.getElementById("animT");
    var anv = anb.value;

    if (anv == "1") {

        anb.setAttribute("value", 0);
        anb.innerHTML = "Off";
    }
    if (anv == "0") {

        anb.setAttribute("value", 1);
        anb.innerHTML = "On ";
    }

}
var display;
var startTime;
var updatedTime;
var difference;
var tInterval;
var savedTime;
var paused = 0;
var running = 0;
var ani;


function create() {

    var stStolpov = document.getElementById("stolpNr").getAttribute("value");
    var stDiskov = document.getElementById("diskNr").getAttribute("value");
    var stVar = document.getElementById("varNr").getAttribute("value");
    var sVar = document.getElementById("varS").getAttribute("value");
    var start = document.getElementById("varS").getAttribute("data-start");
    var end = document.getElementById("varS").getAttribute("data-end");

    if (stStolpov == 0 || stDiskov == 0 || sVar == 0 || stVar == 0) {
        return;
    }
    ani = document.getElementById("animT").value;
    dif = document.getElementById("diff").value;
    layout = document.getElementById("varS").value;
    stDis = document.getElementById("diskNr").value;
    if (document.getElementById("collapseExample").classList.contains("d-none") == false) {
        $("#collapseExample").toggleClass("d-none")
        $("#collapseExample").toggleClass("d-md-flex")
    }
    uStolp(end - 1, start-1); cDisc(start); resetTimer(); startTimer(); getInfo(), getScores(); setVariation();
};

function startTimer() {
    var stStolpov = document.getElementById("stolpNr").getAttribute("value");
    var stDiskov = document.getElementById("diskNr").getAttribute("value");
    if (stStolpov > 0 && stDiskov > 0) {
        if (!running) {
            startTime = new Date().getTime();
            tInterval = setInterval(getShowTime, 10);

            paused = 0;
            running = 1;

        }
    }
}

function resetTimer() {

    clearInterval(tInterval);
    savedTime = 0;
    difference = 0;
    paused = 0;
    running = 0;
}

function getShowTime() {
    updatedTime = new Date().getTime();
    if (savedTime) {
        difference = (updatedTime - startTime) + savedTime;
    } else {
        difference = updatedTime - startTime;
    }
    var hours = Math.floor((difference % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    var minutes = Math.floor((difference % (1000 * 60 * 60)) / (1000 * 60));
    var seconds = Math.floor((difference % (1000 * 60)) / 1000);
    var milliseconds = Math.floor((difference % (1000)) / 10);
    hours = (hours < 10) ? "0" + hours : hours;
    minutes = (minutes < 10) ? "0" + minutes : minutes;
    seconds = (seconds < 10) ? "0" + seconds : seconds;
    milliseconds = (milliseconds < 10) ? "0" + milliseconds : milliseconds;
    display = hours + ":" + minutes + ":" + seconds + "." + milliseconds;
    document.getElementById("watch").innerHTML = display;
}

function pauseTimer() {
    if (!difference) {

    } else if (!paused) {
        clearInterval(tInterval);
        savedTime = difference;
        paused = 1;
        running = 0;
    }
}



// Funkcija za stvaritev diskov
function cdisk(id, size) {
    this.id = id;
    this.size = size;

}

// Funkcija za stvaritev stolpov
function cstolp(pos, pass, center) {
    this.disclist = [];
    this.id = pos.toString();
    this.pass = [];

}

//Funkcija za izbiro naloge
function setVariation() {


    var stStolpov = document.getElementById("stolpNr").getAttribute("value");
    var stDiskov = document.getElementById("diskNr").getAttribute("value");
    var stVar = document.getElementById("varNr").getAttribute("data-connections");

    var pic = document.getElementById("example");
    pic.parentElement.parentElement.parentElement.classList.remove("d-none");

    var ori = document.getElementById("varNr").getAttribute("value");
    var ors = document.getElementById("varS").getAttribute("data-pic");

   
    var spic = ors.split(".")[0];
    spic = spic + ".svg";
    if (ors.includes("http")){ spic=ors};

    var nr = document.getElementsByClassName("col-sm").length;
    var cr = 0;

    var duh = document.getElementById("vari");
    var duhpass = duh.getAttribute("data-connections")


    var myobj = JSON.parse(stVar);

    var h = Object.keys(myobj).length;

    pic.setAttribute("src", spic);


    pic.parentElement.classList.remove("d-none");

    for (i = 0; i < h; i++) {
        var array = myobj[i].split(',').map(function (item) {
            return parseInt(item, 10);
        });;


        stolpi[i].pass = array;
    }





};

//Tukaj imam funkcijo ki prikaze naloge za razlicno stevilo stolpov, se ni do konca implementirano
function variations(dis) {

    if (document.getElementById("varNr") != null) {

        document.getElementById("varNr").innerHTML = "-";
        document.getElementById("varNr").value = 0;

        if (document.getElementById("node") != null) {
            document.getElementById("varS").innerHTML = "-";
            document.getElementById("varS").value = 0;
            document.getElementById("node").style.display = "none";

        }


    }
    //document.getElementById("nodes").innerHTML("-");
    var val = dis.getAttribute("value");

    document.getElementById("stolpNr").innerText = val;
    document.getElementById("stolpNr").setAttribute("value", val);

    if (val > 1) {

        var dar = document.getElementById("odlagalisce").children;
        var dr = dar.length;

        for (i = dr; i >= 0; i--) {
            
            if (dar[i] != null && dar[i].firstElementChild.getAttribute("data-nr") == val) {
                if (dar[i].classList.contains("active")) { dar[i].classList.remove("active") };
                var kah = "#" + dar[i].id; var cln = dar[i].cloneNode(true);; $(kah).remove(); document.getElementById("garbo").insertBefore(cln, document.getElementById("garbo").children[0]);
            }
        }




        document.getElementById("vars").style.display = "";
        var parnt = document.getElementById("garbo").children;
     
        var nr = parnt.length;
      

        for (i = nr; i >= 0; i--) {


            /*  if (parnt[i].firstElementChild.classList.contains("d-none") == false) {
                  //parnt[i].firstElementChild.classList.add("d-none");
              }*/


            if (parnt[i] != null && parnt[i].firstElementChild.getAttribute("data-nr") == val) {

            }
            if (parnt[i] != null && parnt[i].firstElementChild.getAttribute("data-nr") != val) {


                if (parnt[i].classList.contains("active")) { parnt[i].classList.remove("active") };
                var kah = "#" + parnt[i].id;  var cln = parnt[i].cloneNode(true);; $(kah).remove(); document.getElementById("odlagalisce").insertBefore(cln, document.getElementById("odlagalisce").children[0]);
            }


        }

        document.getElementById("garbo").firstElementChild.classList.add("active");
    }
    else {
        document.getElementById("vars").style.display = "none";
        document.getElementById("node").style.display = "none";
    }


}

function dDisplay(dis) {
    var val = dis.getAttribute("value");
    document.getElementById("diskNr").innerText = val;
    document.getElementById("diskNr").setAttribute("value", val);
    document.getElementById("diskNr").setAttribute("data-nr", val);
}


function dVariations() {

    var diss = document.getElementById("stolpNr").getAttribute("value");
    var db = document.getElementById("wtf");
    var biss = parseInt(diss);
    document.getElementById("diskShow").classList.add("d-none");
    document.getElementById("diskNr").setAttribute("value", 0);
    document.getElementById("diskNr").innerHTML = "-";

    while (db.firstChild) {
        db.removeChild(db.firstChild);
    }

    for (i = 1; i < 9; i++) {
        var ddi = document.createElement("a")
        ddi.setAttribute("class", "dropdown-item");
        ddi.setAttribute("value", i);
        ddi.innerHTML = i;
        ddi.setAttribute("onclick", "dDisplay(this)");
        db.appendChild(ddi);

    }

}

function pVariations(dis) {
    document.getElementById("node").style.display = "";
    var rset = document.getElementById("varS");
    rset.setAttribute("value", 0);
    rset.innerHTML = "-";
    var val = dis.getAttribute("value");
    var valss = dis.getAttribute("data-pic");
    var varid = dis.getAttribute("data-id");
    var vals = valss.substring(1);
    var conns = dis.getAttribute("data-connections");
    var b;
    function getSecondPart(str) {
        b = str.split('-')[0];
    }


    //document.getElementById("varNr").innerHTML = "<img src='" + vals + "' />";
    document.getElementById("varNr").innerHTML = "<img src='" + valss + "'/>";
    document.getElementById("varNr").setAttribute("value", val);
    document.getElementById("varNr").setAttribute("data-connections", conns);
    document.getElementById("varNr").setAttribute("data-pic", vals);
    document.getElementById("varNr").setAttribute("data-id", varid);
    //var smh = parnt[i].getAttribute("data-var");


    var dar = document.getElementById("odlagaliscec").children;
    var dr = dar.length;

    for (i = dr; i >= 0; i--) {
        if (dar[i] != null && dar[i].firstElementChild.getAttribute("data-var") == varid) {
            if (dar[i].classList.contains("active")) { dar[i].classList.remove("active") };
            var kah = "#" + dar[i].id;  var cln = dar[i].cloneNode(true);; $(kah).remove(); document.getElementById("garbon").insertBefore(cln, document.getElementById("garbon").children[0]);
        }
    }




    document.getElementById("vars").style.display = "";
    var parnt = document.getElementById("garbon").children;
    
    var nr = parnt.length;
    

    for (i = nr; i >= 0; i--) {


        /*  if (parnt[i].firstElementChild.classList.contains("d-none") == false) {
              //parnt[i].firstElementChild.classList.add("d-none");
          }*/


        if (parnt[i] != null && parnt[i].firstElementChild.getAttribute("data-var") == varid) {

        }
        if (parnt[i] != null && parnt[i].firstElementChild.getAttribute("data-var") != varid) {


            if (parnt[i].classList.contains("active")) { parnt[i].classList.remove("active") };
            var kah = "#" + parnt[i].id;  var cln = parnt[i].cloneNode(true);; $(kah).remove(); document.getElementById("odlagaliscec").insertBefore(cln, document.getElementById("odlagaliscec").children[0]);
        }


    }

    document.getElementById("garbon").firstElementChild.classList.add("active");
}


function sVariations(dis) {

    var val = dis.getAttribute("value");
    var vals = dis.getAttribute("data-start");
    var vale = dis.getAttribute("data-end");
    var lay = dis.getAttribute("data-pic");

    document.getElementById("diskShow").classList.remove("d-none");
    document.getElementById("varS").innerHTML = "<img src=" + lay + "\> ";
    document.getElementById("varS").setAttribute("value", val);
    document.getElementById("varS").setAttribute("data-start", vals);
    document.getElementById("varS").setAttribute("data-end", vale);
    document.getElementById("varS").setAttribute("data-pic", lay);



}





//Deklaracija globalnih spremenljivk
var sw = 0;
var poteze = 0;
var current = "";
var target = "";
var start = "";
var cilj = 0;
var mech = "";
var paramx = "";
var divj = "";
var stDis;

//var paramy = "";

var outgoing;
var incoming;
var rectangles;

//Seznam stolpov, ki vsebuje seznam diskov
stolpi = [];

//Funkcija za stvaritev stolpov, pobrise obstojece stolpe in narise izbrano stevilo novih stolpov, odlocil sem se da bom vsak stolp dal v svoj div, tako mi je bolj vsec ker jih lahko lepse razporedim
function uStolp(test, start) {
    //mogoce jih bom poskusil razvrstiti v tabelo (razen ce imate boljsi predlog), tako da se bodo ujemali z nalogo (tako kot so na slikah)
    //var stStolpov = this.options[this.selectedIndex].value;


    poteze = 0;
    document.getElementById("poteze").innerText = poteze;

    var stStolpov = document.getElementById("stolpNr").getAttribute("value");
    var stDiskov = document.getElementById("diskNr").getAttribute("value");

    if (stStolpov > 0 && stDiskov > 0) {

        var obstojeci = document.getElementsByTagName("svg");
        document.getElementById("navodilo").classList.add("d-none");

        var obstojecis = obstojeci;
        var lng = obstojecis.length;

        for (i = 0; i < lng; ++i) {
            var parent = document.getElementById(i + 1).parentElement;
            var grandparent = parent.parentElement;
            grandparent.removeChild(parent);

            stolpi.shift();

        }

        for (i = 0; i < stStolpov; ++i) {

            var novstolp = new cstolp(i + 1, stStolpov, 50 + "%");
            for (k = 0; k < stStolpov; k++) {
                if (k != i) {
                    novstolp.pass.push(k);
                }
            }
            stolpi.push(novstolp);
            j = i;
            js = "div" + j.toString();

            var novidiv = document.createElement("div");
            novidiv.setAttribute("id", js);
            novidiv.setAttribute("class", "col-auto d-flex");
            var novisvg = document.createElementNS("http://www.w3.org/2000/svg", "svg");
            novisvg.setAttribute("class", "outer border border-primary rounded  bg-white d-inline");
            novisvg.setAttribute("id", i + 1);
            novisvg.setAttribute("viewbox", "0 0 100 100");
            novisvg.setAttribute("preserveAspectRatio", "xMaxYMax");
            //novisvg.setAttribute("position", "relative");

            novidiv.appendChild(novisvg);

            var svgText = document.createElementNS("http://www.w3.org/2000/svg", "text")
            svgText.setAttribute("x", "5%");
            svgText.setAttribute("y", "15%");


            svgText.textContent = i + 1;

            novisvg.appendChild(svgText);

            var htmlstolp = document.createElementNS("http://www.w3.org/2000/svg", "g");
            htmlstolp.setAttribute("class", "selection");
            htmlstolp.setAttribute("x1", 49 + "%");
            htmlstolp.setAttribute("id", i + 1);
            novisvg.appendChild(htmlstolp);

            var htmlcrta = document.createElementNS("http://www.w3.org/2000/svg", "line");
            htmlcrta.setAttribute("stroke", "black");
            htmlcrta.setAttribute("stroke-width", "3");
            if (i == test) { htmlcrta.setAttribute("stroke", "royalblue"); };
            if (i == start) { htmlcrta.setAttribute("stroke", "seagreen"); };
            htmlcrta.setAttribute("class", "line");
            htmlcrta.setAttribute("x1", 50 + "%");
            htmlcrta.setAttribute("x2", 50 + "%");
            htmlcrta.setAttribute("y1", "89%");
            htmlcrta.setAttribute("y2", "10%");
            htmlstolp.appendChild(htmlcrta);

            novisvg.onclick = function () {
                doSomething(this)
            };

            var svg = document.getElementById("row");
            svg.appendChild(novidiv);




        }
        addListen();
        cilj = test;
        check = parseInt(test) + 1;
        tests = check.toString();
        document.getElementById(tests).classList.remove("border-primary");
        document.getElementById(tests).classList.add("border-secondary");


    }
    else { $('.alert').addClass("d-block") }
}

//Funkcija za stvaritev diskov, pobrise obstojece diske in narise izbrano stevilo novih diskov, najbrs bom zdruzil funkciji za stvaritev diskov in stolpov, oziroma, da se bosta izvedli ob istem casu, z dodatkom OK gumba, ki bo potrdil izbiro stolpov in diskov ter variante

function cDisc(star) {

    var starting = parseInt(star);

    var stDiskov = document.getElementById("diskNr").getAttribute("value");

    var stDiskov = document.getElementById("diskNr").getAttribute("value");
    var svg = document.getElementById(star);
    var trenutni = document.getElementsByTagName("rect");
    var lng = trenutni.length;
    var vel = 0;
    var barve = ["green", "yellow", "blue", "red", "orange", "DodgerBlue", "Violet", "SlateBlue"];
    stDis = stDiskov;
    for (i = stDiskov; i > 0; i--) {

        var height = 80 - vel * 9;
        var widh = i * 7;
        var ime = "disc" + i;
        var loc = 49 - i * 3;

        var novidisk = new cdisk(ime, i);

        var barva = barve[Math.floor(Math.random() * barve.length)];
        stolpi[starting - 1].disclist.unshift(novidisk);

        var novid = document.createElementNS("http://www.w3.org/2000/svg", "rect");
        novid.setAttribute("class", "disc");
        novid.setAttribute("id", ime);
        novid.setAttribute("rx", "2px");
        novid.setAttribute("stroke", "black");
        novid.setAttribute("width", widh + "%");
        novid.setAttribute("height", "9%")
        novid.setAttribute("x", loc + "%");
        novid.setAttribute("y", height + "%");
        novid.setAttribute("fill", barve[i - 1]);
        novid.setAttribute("size", i);
        vel = vel + 1;
        if (ime == "disc1") { novid.setAttribute("x", "46.5%"); }
        svg.appendChild(novid);


        d1 = "1.30s";
        d2 = "0.1s";
        var dur = document.getElementById("animT").value;
        if (dur == 0) { d1 = "0.001s", d2 = "0.001s" };

        var novia = document.createElementNS("http://www.w3.org/2000/svg", "animate");
        novia.setAttribute("id", "a" + i.toString());
        novia.setAttribute("attributeType", "CSS");
        novia.setAttribute("attributeName", "opacity");
        novia.setAttribute("values", "1;0;1");
        novia.setAttribute("keyTimes", "0; 0.5; 1");
        novia.setAttribute("dur", d1);
        novia.setAttribute("begin", "indefinite");
        novia.setAttribute("repeatCount", 1);
        novia.setAttribute("restart", "always");

        var novias = document.createElementNS("http://www.w3.org/2000/svg", "animate");
        novias.setAttribute("id", "as" + i.toString());
        novias.setAttribute("attributeType", "CSS");
        novias.setAttribute("attributeName", "opacity");
        novias.setAttribute("values", "1;0;1");
        novias.setAttribute("keyTimes", "0; 0.5; 1");
        novias.setAttribute("dur", d2);
        novias.setAttribute("begin", "indefinite");
        novias.setAttribute("repeatCount", 1);
        novias.setAttribute("restart", "always");


        var novian = document.createElementNS("http://www.w3.org/2000/svg", "animate");
        novian.setAttribute("id", "va" + i.toString());
        novian.setAttribute("attributeType", "css");
        novian.setAttribute("attributeName", "y");
        novian.setAttribute("from", "-20%");
        novian.setAttribute("to", "-10%");

        novian.setAttribute("dur", d1);
        novian.setAttribute("begin", "indefinite");
        novian.setAttribute("repeatCount", 1);
        novian.setAttribute("restart", "always");



        novid.appendChild(novia);
        novid.appendChild(novian);
        novid.appendChild(novias);


    }

    /* for (i = stStolpov; i > 0; i--) {
         var stalp = document.getElementById(i);
         var novib = document.createElementNS("http://www.w3.org/2000/svg", "rect");
         novib.setAttribute("width", "100%");
         novib.setAttribute("height", "100%")
         novib.setAttribute("x", "0%");
         novib.setAttribute("y", "0%");
         novib.setAttribute("opacity", "0");
         stalp.appendChild(novib);
 
     }*/





}


function anm(setlist, paramy, anc, a1a, a2a) {
    setlist.setAttribute("y", paramy);
    setlist.setAttribute("opacity", "1");

}


function ending() {
    $("#collapseExample").toggleClass("d-none")
    $("#collapseExample").toggleClass("d-md-flex")
    $('#navodilo').toggleClass("d-none");
    var bv = document.getElementsByTagName("svg");

    for (i = bv.length; i > 0; i--) {
        var parent = document.getElementById(i
        ).parentElement;
        var grandparent = parent.parentElement;
        grandparent.removeChild(parent);
        stolpi.shift(i);
    }

    if (document.getElementById("vic") != null) {
        document.getElementById("navodilo").removeChild(document.getElementById("vic"));
    }


    var noviAlert = document.createElement("div");
    noviAlert.setAttribute("id", "vic");
    noviAlert.setAttribute("class", "alert container alert-primary alert-dismissible show  bg-primary text-light")
    var noviHeading = document.createElement("h5");
    noviHeading.setAttribute("class", "alert-heading");
    noviHeading.innerText = "CONGRATULATIONS!";
    var noviHr = document.createElement("hr");
    var noviText = document.createElement("p");
    noviText.setAttribute("class", "mb-0");

    var noviButton = document.createElement("button");
    noviButton.setAttribute("class", "left close");
    noviButton.setAttribute("type", "button");
    noviButton.setAttribute("data-dismiss", "alert");
    noviButton.setAttribute("aria-label", "Close");

    var noviSpan = document.createElement("span");
    noviSpan.setAttribute("aria-hidden", "true");
    noviSpan.innerHTML = "&times";

    $('.alert').removeClass("d-block")
    noviAlert.appendChild(noviHeading);
    //noviAlert.appendChild(noviHr);
    noviAlert.appendChild(noviText);
    noviButton.appendChild(noviSpan);
    //noviAlert.appendChild(noviButton);
    var sklon = " moves."
    if (poteze == 1) { sklon = " move."; }

    var txt = "You completed the task in " + display + " and " + poteze + sklon;
    noviText.innerText = txt;

    var sklon2 = " moves."
    if (oM[2] == 1) { sklon2 = " move." }

    if (poteze > oM[2] && oM[2] != 0) { noviText.innerText = txt + " The task could have been completed in " + oM[2] + sklon2; }
    if (oM[2] == 0) { noviText.innerText = txt + "The optimal number of moves has not been set for this task."; }
    if (oM[2] == poteze) { noviText.innerText = txt +" Which is the optimal number of moves."; }

    var mesto = document.createElement("p");
    mesto.id = "place";
    noviAlert.appendChild(mesto);

    document.getElementById("navodilo").appendChild(noviAlert);

  
  

    document.getElementById("navodilo").classList.remove("d-none");




    if (document.getElementById("hId") != undefined) {

        $.when(
            postResult(poteze, document.getElementById("hId").value, oId, display)).then(function () { getScores() });
    }
}



function addListen() {

    rectangles = document.getElementsByTagName("svg");

}



//Funckija, ki registrira klik in izvede premik. Obstaja kaksna boljsa varjanta za belezenje klikov, jaz sem naredil malo po kmecko
function doSomething(we) {


    var clickedtagName = we.tagName;
    var clickedId = we.id;
    if (clickedtagName == "svg" && clickedId != "" && stolpi[cilj].disclist.length != stDis) {

        if (outgoing == null) {

            outgoing = we;
            var highlight = stolpi[outgoing.id - 1].pass;
            we.classList.add("bground");

            for (i = 0; i < highlight.length; i++) {

                j = (highlight[i]);
                divj = "div" + j.toString();
                var b = document.getElementById(divj);
                d = b.getElementsByTagName("svg");


                if (dif == 0) {
                    if (stolpi[j].disclist[0] == null || stolpi[clickedId - 1].disclist[0].size < stolpi[j].disclist[0].size) {
                        d[0].classList.add("border-success");
                    }
                }
                if (dif == 1) {
                    d[0].classList.add("border-success");
                }


            }

            return;
        }

        if (incoming == null && outgoing != undefined) { //Ce koncni stolp se ni izbran in je zacetni stolp definiran zabelezi element ki smo ga kliknili kot koncni stolp
            incoming = we;



            if (stolpi[outgoing.id - 1].disclist.length == 0 || stolpi[incoming.id - 1].disclist.length && stolpi[incoming.id - 1].disclist[0].size < stolpi[outgoing.id - 1].disclist[0].size || outgoing == incoming) {

                incoming.classList.add("shake");
                var cls = incoming.classList;

                incoming.addEventListener("animationend", function () {
                    if (cls.contains("shake") == true) {
                        incoming.classList.remove("shake");
                        incoming = undefined;
                        outgoing = undefined;
                    }
                }, false);




                var reset = document.getElementsByTagName("svg");

                for (i = 0; i < reset.length; i++) { reset[i].classList.remove("border-success"); reset[i].classList.remove("bground"); }
                return;
            }
            else if (stolpi[incoming.id - 1].disclist[0] == null || stolpi[incoming.id - 1].disclist[0].size > stolpi[outgoing.id - 1].disclist[0].size) {

                var outin = stolpi[outgoing.id - 1].pass;
                var inid = parseInt(stolpi[incoming.id - 1].id - 1);



                if (outin.includes(inid)) { //tukaj preverim ce sta stolpa med sabo povezana





                    var setlist = document.getElementById(stolpi[outgoing.id - 1].disclist[0].id);

                    setlist.setAttribute("y", "-120%");
                    setlist.setAttribute("visibility", "hidden");
                    var paramy = 80 - (stolpi[incoming.id - 1].disclist.length) * 9;
                    var anc = "a" + setlist.getAttribute("size");
                    var vanc = "va" + setlist.getAttribute("size");
                    var a1a = document.getElementById(vanc);

                    var sa = "as" + setlist.getAttribute("size");
                    var a3a = document.getElementById(sa);
                    var a2a = document.getElementById(anc);


                    var old_element = a1a;
                    var new_element = old_element.cloneNode(true);
                    old_element.parentNode.replaceChild(new_element, old_element);
                    var old_element = a2a;
                    var new_element = old_element.cloneNode(true);
                    old_element.parentNode.replaceChild(new_element, old_element);
                    var old_element = a3a;
                    var new_element = old_element.cloneNode(true);
                    old_element.parentNode.replaceChild(new_element, old_element);








                    paramy = paramy.toString() + "%";
                    document.getElementById(anc).endElement();
                    document.getElementById(vanc).endElement();
                    document.getElementById(sa).endElement();
                    document.getElementById(anc).setAttribute("xlink:href", "#" + setlist.id);
                    document.getElementById(anc).beginElement(); //Pogon animacij
                    document.getElementById(vanc).setAttribute("xlink:href", "#" + setlist.id);
                    document.getElementById(vanc).setAttribute("to", paramy);
                    document.getElementById(vanc).beginElement(); //Pogon animacij
                    document.getElementById(sa).beginElement(); //Pogon animacij



                    if (ani == "1") {
                        document.getElementById(sa).addEventListener('endEvent', function () { setlist.setAttribute("visibility", "visible"); }, false);
                        document.getElementById(vanc).addEventListener('endEvent', anm(setlist, paramy, vanc, a1a, a2a), false);
                        document.getElementById(anc).addEventListener('endEvent', function () {

                        }







                            , false);

                    };
                    if (ani == "0") {
                        setlist.setAttribute("y", paramy);
                        setlist.setAttribute("opacity", "1");
                        setlist.setAttribute("visibility", "visible");
                    }





                    document.getElementById(incoming.id).appendChild(document.getElementById(stolpi[outgoing.id - 1].disclist[0].id));

                    stolpi[incoming.id - 1].disclist.unshift(stolpi[outgoing.id - 1].disclist[0]);
                    stolpi[outgoing.id - 1].disclist.splice(0, 1);

                    poteze = poteze + 1;
                    document.getElementById("poteze").innerText = poteze;

                    if (stolpi[cilj].disclist.length == stDis) {

                        pauseTimer();

                        if (document.getElementById("animT").value == "1") {
                            document.getElementById(anc).addEventListener('endEvent', ending(), false);
                        }
                        if (document.getElementById("animT").value == "0") {
                            ending();
                        
                        }
                    }




                }
                if (outin.includes(inid) == false) {
                    incoming.classList.add("shake");
                    var cls = incoming.classList;
                    incoming.addEventListener("animationend", function () {
                        if (cls.contains("shake") == true) {

                            cls.remove("shake");
                            incoming = undefined;
                            outgoing = undefined;
                            return;
                        }
                    }, false);
                }
            }


            var reset = document.getElementsByTagName("svg");
            for (i = 0; i < reset.length; i++) { reset[i].classList.remove("border-success"); reset[i].classList.remove("bground"); }

            incoming = undefined;
            outgoing = undefined;
        };

        //e.stopPropagation();

    }
    //}
}




