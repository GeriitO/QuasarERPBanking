/*
 * SimpleModal Basic Modal Dialog
 * http://www.ericmmartin.com/projects/simplemodal/
 * http://code.google.com/p/simplemodal/
 *
 * Copyright (c) 2010 Eric Martin - http://ericmmartin.com
 *
 * Licensed under the MIT license:
 *   http://www.opensource.org/licenses/mit-license.php
 *
 * Revision: $Id: basic.js 254 2010-07-23 05:14:44Z emartin24 $
 */

jQuery(function ($) {
	// Load dialog on page load
	//$('#basic-modal-content').modal();

	// Load dialog on click
	$('#basic-modal .basic').click(function (e) {
		$('#basic-modal-content').modal();

		return false;
	});
});

/**
* Force Redraw
*
* Created by Pascal Beyeler (anvio.ch)
* Dual licensed under the MIT and GPL licenses:
* http://www.opensource.org/licenses/mit-license.php
* http://www.gnu.org/licenses/gpl.html
*
*/

/**
* Sometimes the browser just does not redraw the elements after a CSS change or whatever. 
* This plugin allows you to force a redraw in your browser.
*
* @example $('#myelement').forceRedraw(); //works in most cases
* @example $('#myelement').forceRedraw(true); //use this one to force a redraw by changing the element's padding
* @desc force a browser redraw.
*
* @param brutal
* @return void
*
* @name $.fn.forceRedraw
* @cat Plugins/Browser
* @author Pascal Beyeler (anvio.ch)
*/
(function ($) {

    $.fn.forceRedraw = function (brutal) {

        //this fix works for most browsers. it has the same effect as el.className = el.className.
        $(this).addClass('forceRedraw').removeClass('forceRedraw');

        //sometimes for absolute positioned elements the above fix does not work.
        //there's still a "brutal" way to force a redraw by changing the padding.
        if (brutal) {
            var paddingLeft = $(this).css('padding-left');
            var parsedPaddingLeft = parseInt(paddingLeft, 10);
            $(this).css('padding-left', ++parsedPaddingLeft);

            //give it some time to redraw
            window.setTimeout($.proxy(function () {
                //change it back
                $(this).css('padding-left', paddingLeft);
            }, this), 1);
        }
    }

})(jQuery);


/*  
**  SCRIPT PARA MOSTRAR VIDEOS EN UNA VENTANA MODAL (REQUIERE JQUERY-UI)
*/
function showVideo(movie) {
            
    var video;
    if(movie == 'crear'){
        video = 'crear-gris.flv';
    }else if(movie == 'recibir'){
        video = 'recibir-gris.flv';
    }else if(movie == 'finalizar'){
        video = 'finalizar-gris-2.flv';
    }

    ret = '<object id="Object1" type="application/x-shockwave-flash" data="../../Content/videos/player_flv_mini.swf" width="800" height="600">'+
                '<param name="movie" value="../../Content/videos/player_flv_mini.swf" />'+
                '<param name="wmode" value="opaque" /> '+
                '<param name="allowScriptAccess" value="sameDomain" /> '+
                '<param name="quality" value="high" /> '+
                '<param name="menu" value="true" /> '+
                '<param name="autoplay" value="true" /> '+
                '<param name="autoload" value="false" /> '+
                '<param name="FlashVars" value="flv=../../Content/videos/'+video+'&width=800&height=600&autoplay=1&autoload=0&buffer=5&playercolor=000000 &loadingcolor=9b9a9a&buttoncolor=ffffff&slidercolor=ffffff" /> '+
            '</object>';

    $('#movie').html(ret);
    $("#movie").dialog({
        width: 850,
        height: 670,
        modal: true
    });
}


/*
** SCRIPTS PARA HACER LOGOUT SI HAY MUCHO TIEMPO DE INACTIVIDAD EN EL SISTEMA
*/

/*
* jQuery idleTimer plugin
* version 0.8.092209
* by Paul Irish. 
*   http://github.com/paulirish/yui-misc/tree/
* MIT license
 
* adapted from YUI idle timer by nzakas:
*   http://github.com/nzakas/yui-misc/
 
 
* Copyright (c) 2009 Nicholas C. Zakas
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

(function ($) {

    $.idleTimer = function f(newTimeout) {

        //$.idleTimer.tId = -1     //timeout ID

        var idle = false,        //indicates if the user is idle
        enabled = true,        //indicates if the idle timer is enabled
        timeout = 30000,        //the amount of time (ms) before the user is considered idle
        events = 'mousemove keydown DOMMouseScroll mousewheel mousedown', // activity is one of these events
        //f.olddate = undefined, // olddate used for getElapsedTime. stored on the function

        /* (intentionally not documented)
        * Toggles the idle state and fires an appropriate event.
        * @return {void}
        */
    toggleIdleState = function () {

        //toggle the state
        idle = !idle;

        // reset timeout counter
        f.olddate = +new Date;

        //fire appropriate event
        $(document).trigger($.data(document, 'idleTimer', idle ? "idle" : "active") + '.idleTimer');
    },

        /**
        * Stops the idle timer. This removes appropriate event handlers
        * and cancels any pending timeouts.
        * @return {void}
        * @method stop
        * @static
        */
    stop = function () {

        //set to disabled
        enabled = false;

        //clear any pending timeouts
        clearTimeout($.idleTimer.tId);

        //detach the event handlers
        $(document).unbind('.idleTimer');
    },


        /* (intentionally not documented)
        * Handles a user event indicating that the user isn't idle.
        * @param {Event} event A DOM2-normalized event object.
        * @return {void}
        */
    handleUserEvent = function () {

        //clear any existing timeout
        clearTimeout($.idleTimer.tId);

        //if the idle timer is enabled
        if (enabled) {

            //if it's idle, that means the user is no longer idle
            if (idle) {
                toggleIdleState();
            }

            //set a new timeout
            $.idleTimer.tId = setTimeout(toggleIdleState, timeout);
        }
    };

        /**
        * Starts the idle timer. This adds appropriate event handlers
        * and starts the first timeout.
        * @param {int} newTimeout (Optional) A new value for the timeout period in ms.
        * @return {void}
        * @method $.idleTimer
        * @static
        */

        f.olddate = f.olddate || +new Date;

        //assign a new timeout if necessary
        if (typeof newTimeout == "number") {
            timeout = newTimeout;
        } else if (newTimeout === 'destroy') {
            stop();
            return this;
        } else if (newTimeout === 'getElapsedTime') {
            return (+new Date) - f.olddate;
        }

        //assign appropriate event handlers
        $(document).bind($.trim((events + ' ').split(' ').join('.idleTimer ')), handleUserEvent);


        //set a timeout to toggle state
        $.idleTimer.tId = setTimeout(toggleIdleState, timeout);

        // assume the user is active for the first x seconds.
        $.data(document, 'idleTimer', "active");

    }; // end of $.idleTimer()

})(jQuery);


/*
* jQuery Idle Timeout 1.2
* Copyright (c) 2011 Eric Hynds
*
* http://www.erichynds.com/jquery/a-new-and-improved-jquery-idle-timeout-plugin/
*
* Depends:
*  - jQuery 1.4.2+
*  - jQuery Idle Timer (by Paul Irish, http://paulirish.com/2009/jquery-idletimer-plugin/)
*
* Dual licensed under the MIT and GPL licenses:
*   http://www.opensource.org/licenses/mit-license.php
*   http://www.gnu.org/licenses/gpl.html
*
*/

(function ($, win) {

    var idleTimeout = {
        init: function (element, resume, options) {
            var self = this, elem;

            this.warning = elem = $(element);
            this.resume = $(resume);
            this.options = options;
            this.countdownOpen = false;
            this.failedRequests = options.failedRequests;
            this._startTimer();
            this.title = document.title;

            // expose obj to data cache so peeps can call internal methods
            $.data(elem[0], 'idletimeout', this);

            // start the idle timer
            $.idleTimer(options.idleAfter * 1000);

            // once the user becomes idle
            $(document).bind("idle.idleTimer", function () {

                // if the user is idle and a countdown isn't already running
                if ($.data(document, 'idleTimer') === 'idle' && !self.countdownOpen) {
                    self._stopTimer();
                    self.countdownOpen = true;
                    self._idle();
                }
            });

            // bind continue link
            this.resume.bind("click", function (e) {
                e.preventDefault();

                win.clearInterval(self.countdown); // stop the countdown
                self.countdownOpen = false; // stop countdown
                self._startTimer(); // start up the timer again
                self._keepAlive(false); // ping server
                options.onResume.call(self.warning); // call the resume callback
            });
        },

        _idle: function () {
            var self = this,
				options = this.options,
				warning = this.warning[0],
				counter = options.warningLength;

            // fire the onIdle function
            options.onIdle.call(warning);

            // set inital value in the countdown placeholder
            options.onCountdown.call(warning, counter);

            // create a timer that runs every second
            this.countdown = win.setInterval(function () {
                if (--counter === 0) {
                    window.clearInterval(self.countdown);
                    options.onTimeout.call(warning);
                } else {
                    options.onCountdown.call(warning, counter);
                    document.title = options.titleMessage.replace('%s', counter) + self.title;
                }
            }, 1000);
        },

        _startTimer: function () {
            var self = this;

            this.timer = win.setTimeout(function () {
                self._keepAlive();
            }, this.options.pollingInterval * 1000);
        },

        _stopTimer: function () {
            // reset the failed requests counter
            this.failedRequests = this.options.failedRequests;
            win.clearTimeout(this.timer);
        },

        _keepAlive: function (recurse) {
            var self = this,
				options = this.options;

            //Reset the title to what it was.
            document.title = self.title;

            // assume a startTimer/keepAlive loop unless told otherwise
            if (typeof recurse === "undefined") {
                recurse = true;
            }

            // if too many requests failed, abort
            if (!this.failedRequests) {
                this._stopTimer();
                options.onAbort.call(this.warning[0]);
                return;
            }

            $.ajax({
                timeout: options.AJAXTimeout,
                url: options.keepAliveURL,
                error: function () {
                    self.failedRequests--;
                },
                success: function (response) {
                    if ($.trim(response) !== options.serverResponseEquals) {
                        self.failedRequests--;
                    }
                },
                complete: function () {
                    if (recurse) {
                        self._startTimer();
                    }
                }
            });
        }
    };

    // expose
    $.idleTimeout = function (element, resume, options) {
        idleTimeout.init(element, resume, $.extend($.idleTimeout.options, options));
        return this;
    };

    // options
    $.idleTimeout.options = {
        // number of seconds after user is idle to show the warning
        warningLength: 180,

        // url to call to keep the session alive while the user is active
        keepAliveURL: "",

        // the response from keepAliveURL must equal this text:
        serverResponseEquals: "OK",

        // user is considered idle after this many seconds.  10 minutes default
        idleAfter: 600,

        // a polling request will be sent to the server every X seconds
        pollingInterval: 60,

        // number of failed polling requests until we abort this script
        failedRequests: 5,

        // the $.ajax timeout in MILLISECONDS!
        AJAXTimeout: 300,

        // %s will be replaced by the counter value
        titleMessage: 'Advertencia: %s para cerrar sesi�n | ',

        /*
        Callbacks
        "this" refers to the element found by the first selector passed to $.idleTimeout.
        */
        // callback to fire when the session times out
        onTimeout: $.noop,

        // fires when the user becomes idle
        onIdle: $.noop,

        // fires during each second of warningLength
        onCountdown: $.noop,

        // fires when the user resumes the session
        onResume: $.noop,

        // callback to fire when the script is aborted due to too many failed requests
        onAbort: $.noop
    };

})(jQuery, window);

/*
 *  Plugin para hacer un scroll animado para algun anchor
 *  http://djpate.com/2011/01/01/animated-scrollto-effect-jquery-plugin/
 */
(function(a){a.fn.slideto=function(b){var c={slide_duration:"slow",highlight_duration:3000,highlight:true,highlight_color:"#FFFF99",offset:0},b=a.extend(c,b);return this.each(function(){var c=!1;obj=a(this),a("html,body").animate({scrollTop:obj.offset().top+b.offset},b.slide_duration,function(){c==0&&(b.highlight&&a.ui.version&&obj.effect("highlight",{color:b.highlight_color},b.highlight_duration),c=!0)})})}})(jQuery);


/*
|--------------------------------------------------------------------------
| UItoTop jQuery Plugin 1.2 by Matt Varone (Back to Top)
| http://www.mattvarone.com/web-design/uitotop-jquery-plugin/
|--------------------------------------------------------------------------
*/
(function ($) {
    $.fn.UItoTop = function (options) {

        var defaults = {
            text: 'To Top',
            min: 200,
            inDelay: 600,
            outDelay: 400,
            containerID: 'toTop',
            containerHoverID: 'toTopHover',
            scrollSpeed: 1200,
            easingType: 'linear'
        },
            settings = $.extend(defaults, options),
            containerIDhash = '#' + settings.containerID,
            containerHoverIDHash = '#' + settings.containerHoverID;

        $('body').append('<a href="#" id="' + settings.containerID + '">' + settings.text + '</a>');
        $(containerIDhash).hide().on('click.UItoTop', function () {
            $('html, body').animate({ scrollTop: 0 }, settings.scrollSpeed, settings.easingType);
            $('#' + settings.containerHoverID, this).stop().animate({ 'opacity': 0 }, settings.inDelay, settings.easingType);
            return false;
        })
		.prepend('<span id="' + settings.containerHoverID + '"></span>')
		.hover(function () {
		    $(containerHoverIDHash, this).stop().animate({
		        'opacity': 1
		    }, 600, 'linear');
		}, function () {
		    $(containerHoverIDHash, this).stop().animate({
		        'opacity': 0
		    }, 700, 'linear');
		});

        $(window).scroll(function () {
            var sd = $(window).scrollTop();
            if (typeof document.body.style.maxHeight === "undefined") {
                $(containerIDhash).css({
                    'position': 'absolute',
                    'top': sd + $(window).height() - 50
                });
            }
            if (sd > settings.min)
                $(containerIDhash).fadeIn(settings.inDelay);
            else
                $(containerIDhash).fadeOut(settings.Outdelay);
        });
    };
})(jQuery);

/*----------------------------------------------------
* Funciones para la ventana de Reclamos (incidencias)
* ---------------------------------------------------- */

 function reportarIncidencia(corrID) {
     $.get('/Reclamo/Create?correspondenciaId=' + corrID, function (data) {
         //alert(data);
         $("<div id='reporteIncidenciaDialog' >" + data + "</div>").dialog({
             width: 900,
             modal: true,
             beforeClose: function (event, ui) {
                 $('#reporteIncidenciaDialog').remove();
                 //window.location.reload();
             }
         });

         $("#Comentario").charCounter(490, {
                container: '<em style="margin:5px;"></em>',
                format: "Quedan %1",
                pulse: false,
                delay: 100
            });
     });
 }

function guardarReclamo() {
    corrID = $('#CorrespondenciaID').val();
    tipoReclamoID = $('#TipoReclamoID').val();
    comentario = $('#Comentario').val();

    if (tipoReclamoID == null || tipoReclamoID == "") {
        $('#TipoReclamoID').css("background-color", "#FCC");
        return;
    }
    if (comentario == null || comentario == "") {
        $('#Comentario').css("background-color", "#FCC");
        $('#Comentario').focus();
        return;
    }

    if (comentario.length > 499) {
        $('#Comentario').css("background-color", "#FCC");
        $('#Comentario').focus();
        alert('El comentario no debe sobrepasar los 500 caracteres');
        return;
    }

    $.post("/Reclamo/Create",
                {
                    CorrespondenciaID: corrID,
                    TipoReclamoID: tipoReclamoID,
                    Comentario: comentario
                },
                function (data) {
                    //alert(data);
                    if (data) {
                        //$('#ReclamoForm').hide('fast');
                        //$('#reporteIncidenciaDialog').dialog('close');
                        $('#reporteIncidenciaDialog').html('<h2>Su reporte de incidencia fue guardado</h2>');
                        //alert("Su reporte de incidencia fue guardado");
                    } else {
                        alert("Se produjo un error al guardar su incidencia");
                    }
                }
            );
}

function guardarComentario(cerrar) {

    typeof (cerrar) == 'undefined' ? cerrar = false : cerrar = true;
    
    reclamoID = $('#ReclamoID').val();
    comentario = $('#Comentario').val();

    if (comentario == null || comentario == "") {
        $('#Comentario').css("background-color", "#FCC");
        $('#Comentario').focus();
        return;
    }

    if (comentario.length > 499) {
        $('#Comentario').css("background-color", "#FCC");
        $('#Comentario').focus();
        alert('El comentario no debe sobrepasar los 500 caracteres');
        return;
    }

    $.post("/Reclamo/CreateComment",
                {
                    ReclamoID: reclamoID,
                    Comentario: comentario,
                    Cerrar: cerrar
                },
                function (data) {
                    //alert(data);
                    if (data) {
                        $('#reporteIncidenciaDialog').html('<h2>Su comentario fue guardado</h2>');
                        //alert("Su reporte de incidencia fue guardado");
                    } else {
                        alert("Se produjo un error al guardar su comentario");
                    }
                }
            );
}

function cerrarReclamo(id) {
    $.ajax({
        url: "/Reclamo/Cerrar?id=" + id,
        success: function (result) {
            $('#cerrar' + id).html(result);
            $('#r' + id).css('background-color', '#f0fff0');
            $('#ComentarioForm').hide();
        }
    });
}


/*--------------------------------------------------------------------
* Funcion para prevenir que el backspace lleve a la pantalla anterior  --> Modificado para que no acepte '<' o '>' como input
* -------------------------------------------------------------------- */

// Prevent the backspace key from navigating back.
$(document).unbind('keydown').bind('keydown', function (event) {
    var doPrevent = false;
    if (event.keyCode === 8) {
        var d = event.srcElement || event.target;
        //console.log(d.tagName.toUpperCase());
        //console.log(d.type.toUpperCase());
        if ((d.tagName.toUpperCase() === 'INPUT' && (d.type.toUpperCase() === 'TEXT' || d.type.toUpperCase() === 'PASSWORD' || d.type.toUpperCase() === 'SEARCH'))
             || d.tagName.toUpperCase() === 'TEXTAREA') {
            doPrevent = d.readOnly || d.disabled;
        }
        else {
            doPrevent = true;
        }
    }
    // para alt el keycode es 18 / 190 para el punto/ event.keyCode ===  || event.keyCode === 124  226
    //Si es '>' o '<' no se acepta como input (es 226 en chrome y explorer, 60 en firefox)
    //if ((event.keyCode === 18) || (event.keyCode === 60) || (event.keyCode === 190) || (event.keyCode === 124)) {
    if ((event.keyCode === 18) || (event.keyCode === 60) || (event.keyCode === 124)) {
        return false;
        
        doPrevent = true;
        
        alert('No se permite el ingreso de caracteres especiales, presione fuera del formulario e ingrese nuevamente los datos');
    }
    else {
        return true;
    }

    if (doPrevent==true) {
       event.preventDefault();
    }
});

/*
*   *** QUITAR LOS CARACTERES: '<' y '>'  DE LOS INPUT Y TEXTAREA AL PERDER EL FOCO ***
*/
jQuery(function ($) {
    function replaceDangerousCharacters(elem) {
        var cad = elem.val();
        cad = cad.replace('>', '');
        //cad = cad.replace('<', '');


        cad = cad.replace(/>/g, "");
        cad = cad.replace('<', '');
        //cad = cad.replace('.', '');
        cad = cad.replace(/|/gi, '');
        cad.de
        //console.log(cad);
        elem.val(cad);
    }

    $("input").blur(function () {
        //console.log($(this).val());
        //console.log($(this).attr('type'));
        var elem = $(this);
        var type = elem.attr('type');
        if (type == 'text') {
            replaceDangerousCharacters(elem);
        }
    });

    $("textarea").blur(function () {
        //console.log($(this).val());
        var elem = $(this);
        replaceDangerousCharacters(elem);
    });
});





/**
 *
 * jquery.charcounter.js version 1.2
 * requires jQuery version 1.2 or higher
 * Copyright (c) 2007 Tom Deater (http://www.tomdeater.com)
 * Licensed under the MIT License:
 * http://www.opensource.org/licenses/mit-license.php
 * 
 */
 
(function($) {
	/**
	 * attaches a character counter to each textarea element in the jQuery object
	 * usage: $("#myTextArea").charCounter(max, settings);
	 */
	
	$.fn.charCounter = function (max, settings) {
		max = max || 100;
		settings = $.extend({
			container: "<span></span>",
			classname: "charcounter",
			format: "(%1 characters remaining)",
			pulse: true,
			delay: 0
		}, settings);
		var p, timeout;
		
		function count(el, container) {
			el = $(el);
			if (el.val().length > max) {
			    el.val(el.val().substring(0, max));
			    if (settings.pulse && !p) {
			    	pulse(container, true);
			    };
			};
			if (settings.delay > 0) {
				if (timeout) {
					window.clearTimeout(timeout);
				}
				timeout = window.setTimeout(function () {
					container.html(settings.format.replace(/%1/, (max - el.val().length)));
				}, settings.delay);
			} else {
				container.html(settings.format.replace(/%1/, (max - el.val().length)));
			}
		};
		
		function pulse(el, again) {
			if (p) {
				window.clearTimeout(p);
				p = null;
			};
			el.animate({ opacity: 0.1 }, 100, function () {
				$(this).animate({ opacity: 1.0 }, 100);
			});
			if (again) {
				p = window.setTimeout(function () { pulse(el) }, 200);
			};
		};
		
		return this.each(function () {
			var container;
			if (!settings.container.match(/^<.+>$/)) {
				// use existing element to hold counter message
				container = $(settings.container);
			} else {
				// append element to hold counter message (clean up old element first)
				$(this).next("." + settings.classname).remove();
				container = $(settings.container)
								.insertAfter(this)
								.addClass(settings.classname);
			}
			$(this)
				.unbind(".charCounter")
				.bind("keydown.charCounter", function () { count(this, container); })
				.bind("keypress.charCounter", function () { count(this, container); })
				.bind("keyup.charCounter", function () { count(this, container); })
				.bind("focus.charCounter", function () { count(this, container); })
				.bind("mouseover.charCounter", function () { count(this, container); })
				.bind("mouseout.charCounter", function () { count(this, container); })
				.bind("paste.charCounter", function () { 
					var me = this;
					setTimeout(function () { count(me, container); }, 10);
				});
			if (this.addEventListener) {
				this.addEventListener('input', function () { count(this, container); }, false);
			};
			count(this, container);
		});
	};

})(jQuery);



/*!
* TableSorter 2.17.2 min - Client-side table sorting with ease!
* Copyright (c) 2007 Christian Bach
*/
!function (g) { g.extend({ tablesorter: new function () { function d() { var b = arguments[0], a = 1 < arguments.length ? Array.prototype.slice.call(arguments) : b; if ("undefined" !== typeof console && "undefined" !== typeof console.log) console[/error/i.test(b) ? "error" : /warn/i.test(b) ? "warn" : "log"](a); else alert(a) } function v(b, a) { d(b + " (" + ((new Date).getTime() - a.getTime()) + "ms)") } function p(b) { for (var a in b) return !1; return !0 } function n(b, a, c) { if (!a) return ""; var h, e = b.config, r = e.textExtraction || "", k = "", k = "basic" === r ? g(a).attr(e.textAttribute) || a.textContent || a.innerText || g(a).text() || "" : "function" === typeof r ? r(a, b, c) : "function" === typeof (h = f.getColumnData(b, r, c)) ? h(a, b, c) : a.textContent || a.innerText || g(a).text() || ""; return g.trim(k) } function t(b) { var a = b.config, c = a.$tbodies = a.$table.children("tbody:not(." + a.cssInfoBlock + ")"), h, e, r, k, l, m, g, u, p, q = 0, s = "", t = c.length; if (0 === t) return a.debug ? d("Warning: *Empty table!* Not building a parser cache") : ""; a.debug && (p = new Date, d("Detecting parsers for each column")); for (e = []; q < t; ) { h = c[q].rows; if (h[q]) for (r = a.columns, k = 0; k < r; k++) { l = a.$headers.filter('[data-column="' + k + '"]:last'); m = f.getColumnData(b, a.headers, k); u = f.getParserById(f.getData(l, m, "sorter")); g = "false" === f.getData(l, m, "parser"); a.empties[k] = f.getData(l, m, "empty") || a.emptyTo || (a.emptyToBottom ? "bottom" : "top"); a.strings[k] = f.getData(l, m, "string") || a.stringTo || "max"; g && (u = f.getParserById("no-parser")); if (!u)a: { l = b; m = h; g = -1; u = k; for (var A = void 0, x = f.parsers.length, z = !1, F = "", A = !0; "" === F && A; ) g++, m[g] ? (z = m[g].cells[u], F = n(l, z, u), l.config.debug && d("Checking if value was empty on row " + g + ", column: " + u + ': "' + F + '"')) : A = !1; for (; 0 <= --x; ) if ((A = f.parsers[x]) && "text" !== A.id && A.is && A.is(F, l, z)) { u = A; break a } u = f.getParserById("text") } a.debug && (s += "column:" + k + "; parser:" + u.id + "; string:" + a.strings[k] + "; empty: " + a.empties[k] + "\n"); e[k] = u } q += e.length ? t : 1 } a.debug && (d(s ? s : "No parsers detected"), v("Completed detecting parsers", p)); a.parsers = e } function x(b) { var a, c, h, e, r, k, l, m, y, u, p, q = b.config, s = q.$table.children("tbody"), t = q.parsers; q.cache = {}; if (!t) return q.debug ? d("Warning: *Empty table!* Not building a cache") : ""; q.debug && (m = new Date); q.showProcessing && f.isProcessing(b, !0); for (r = 0; r < s.length; r++) if (p = [], a = q.cache[r] = { normalized: [] }, !s.eq(r).hasClass(q.cssInfoBlock)) { y = s[r] && s[r].rows.length || 0; for (h = 0; h < y; ++h) if (u = { child: [] }, k = g(s[r].rows[h]), l = [], k.hasClass(q.cssChildRow) && 0 !== h) c = a.normalized.length - 1, a.normalized[c][q.columns].$row = a.normalized[c][q.columns].$row.add(k), k.prev().hasClass(q.cssChildRow) || k.prev().addClass(f.css.cssHasChild), u.child[c] = g.trim(k[0].textContent || k[0].innerText || k.text() || ""); else { u.$row = k; u.order = h; for (e = 0; e < q.columns; ++e) "undefined" === typeof t[e] ? q.debug && d("No parser found for cell:", k[0].cells[e], "does it have a header?") : (c = n(b, k[0].cells[e], e), c = "no-parser" === t[e].id ? "" : t[e].format(c, b, k[0].cells[e], e), l.push(c), "numeric" === (t[e].type || "").toLowerCase() && (p[e] = Math.max(Math.abs(c) || 0, p[e] || 0))); l[q.columns] = u; a.normalized.push(l) } a.colMax = p } q.showProcessing && f.isProcessing(b); q.debug && v("Building cache for " + y + " rows", m) } function z(b, a) { var c = b.config, h = c.widgetOptions, e = b.tBodies, r = [], k = c.cache, d, m, y, u, n, q; if (p(k)) return c.appender ? c.appender(b, r) : b.isUpdating ? c.$table.trigger("updateComplete", b) : ""; c.debug && (q = new Date); for (n = 0; n < e.length; n++) if (d = g(e[n]), d.length && !d.hasClass(c.cssInfoBlock)) { y = f.processTbody(b, d, !0); d = k[n].normalized; m = d.length; for (u = 0; u < m; u++) r.push(d[u][c.columns].$row), c.appender && (!c.pager || c.pager.removeRows && h.pager_removeRows || c.pager.ajax) || y.append(d[u][c.columns].$row); f.processTbody(b, y, !1) } c.appender && c.appender(b, r); c.debug && v("Rebuilt table", q); a || c.appender || f.applyWidget(b); b.isUpdating && c.$table.trigger("updateComplete", b) } function C(b) { return /^d/i.test(b) || 1 === b } function D(b) { var a, c, h, e, r, k, l, m = b.config; m.headerList = []; m.headerContent = []; m.debug && (l = new Date); m.columns = f.computeColumnIndex(m.$table.children("thead, tfoot").children("tr")); e = m.cssIcon ? '<i class="' + (m.cssIcon === f.css.icon ? f.css.icon : m.cssIcon + " " + f.css.icon) + '"></i>' : ""; m.$headers.each(function (d) { c = g(this); a = f.getColumnData(b, m.headers, d, !0); m.headerContent[d] = g(this).html(); r = m.headerTemplate.replace(/\{content\}/g, g(this).html()).replace(/\{icon\}/g, e); m.onRenderTemplate && (h = m.onRenderTemplate.apply(c, [d, r])) && "string" === typeof h && (r = h); g(this).html('<div class="' + f.css.headerIn + '">' + r + "</div>"); m.onRenderHeader && m.onRenderHeader.apply(c, [d]); this.column = parseInt(g(this).attr("data-column"), 10); this.order = C(f.getData(c, a, "sortInitialOrder") || m.sortInitialOrder) ? [1, 0, 2] : [0, 1, 2]; this.count = -1; this.lockedOrder = !1; k = f.getData(c, a, "lockedOrder") || !1; "undefined" !== typeof k && !1 !== k && (this.order = this.lockedOrder = C(k) ? [1, 1, 1] : [0, 0, 0]); c.addClass(f.css.header + " " + m.cssHeader); m.headerList[d] = this; c.parent().addClass(f.css.headerRow + " " + m.cssHeaderRow).attr("role", "row"); m.tabIndex && c.attr("tabindex", 0) }).attr({ scope: "col", role: "columnheader" }); B(b); m.debug && (v("Built headers:", l), d(m.$headers)) } function E(b, a, c) { var h = b.config; h.$table.find(h.selectorRemove).remove(); t(b); x(b); H(h.$table, a, c) } function B(b) { var a, c, h = b.config; h.$headers.each(function (e, r) { c = g(r); a = "false" === f.getData(r, f.getColumnData(b, h.headers, e, !0), "sorter"); r.sortDisabled = a; c[a ? "addClass" : "removeClass"]("sorter-false").attr("aria-disabled", "" + a); b.id && (a ? c.removeAttr("aria-controls") : c.attr("aria-controls", b.id)) }) } function G(b) { var a, c, h = b.config, e = h.sortList, r = e.length, d = f.css.sortNone + " " + h.cssNone, l = [f.css.sortAsc + " " + h.cssAsc, f.css.sortDesc + " " + h.cssDesc], m = ["ascending", "descending"], y = g(b).find("tfoot tr").children().add(h.$extraHeaders).removeClass(l.join(" ")); h.$headers.removeClass(l.join(" ")).addClass(d).attr("aria-sort", "none"); for (a = 0; a < r; a++) if (2 !== e[a][1] && (b = h.$headers.not(".sorter-false").filter('[data-column="' + e[a][0] + '"]' + (1 === r ? ":last" : "")), b.length)) { for (c = 0; c < b.length; c++) b[c].sortDisabled || b.eq(c).removeClass(d).addClass(l[e[a][1]]).attr("aria-sort", m[e[a][1]]); y.length && y.filter('[data-column="' + e[a][0] + '"]').removeClass(d).addClass(l[e[a][1]]) } h.$headers.not(".sorter-false").each(function () { var b = g(this), a = this.order[(this.count + 1) % (h.sortReset ? 3 : 2)], a = b.text() + ": " + f.language[b.hasClass(f.css.sortAsc) ? "sortAsc" : b.hasClass(f.css.sortDesc) ? "sortDesc" : "sortNone"] + f.language[0 === a ? "nextAsc" : 1 === a ? "nextDesc" : "nextNone"]; b.attr("aria-label", a) }) } function L(b) { if (b.config.widthFixed && 0 === g(b).find("colgroup").length) { var a = g("<colgroup>"), c = g(b).width(); g(b.tBodies[0]).find("tr:first").children("td:visible").each(function () { a.append(g("<col>").css("width", parseInt(g(this).width() / c * 1E3, 10) / 10 + "%")) }); g(b).prepend(a) } } function M(b, a) { var c, h, e, f, d, l = b.config, m = a || l.sortList; l.sortList = []; g.each(m, function (b, a) { f = parseInt(a[0], 10); if (e = l.$headers.filter('[data-column="' + f + '"]:last')[0]) { h = (h = ("" + a[1]).match(/^(1|d|s|o|n)/)) ? h[0] : ""; switch (h) { case "1": case "d": h = 1; break; case "s": h = d || 0; break; case "o": c = e.order[(d || 0) % (l.sortReset ? 3 : 2)]; h = 0 === c ? 1 : 1 === c ? 0 : 2; break; case "n": e.count += 1; h = e.order[e.count % (l.sortReset ? 3 : 2)]; break; default: h = 0 } d = 0 === b ? h : d; c = [f, parseInt(h, 10) || 0]; l.sortList.push(c); h = g.inArray(c[1], e.order); e.count = 0 <= h ? h : c[1] % (l.sortReset ? 3 : 2) } }) } function N(b, a) { return b && b[a] ? b[a].type || "" : "" } function O(b, a, c) { var h, e, d, k = b.config, l = !c[k.sortMultiSortKey], m = k.$table; m.trigger("sortStart", b); a.count = c[k.sortResetKey] ? 2 : (a.count + 1) % (k.sortReset ? 3 : 2); k.sortRestart && (e = a, k.$headers.each(function () { this === e || !l && g(this).is("." + f.css.sortDesc + ",." + f.css.sortAsc) || (this.count = -1) })); e = a.column; if (l) { k.sortList = []; if (null !== k.sortForce) for (h = k.sortForce, c = 0; c < h.length; c++) h[c][0] !== e && k.sortList.push(h[c]); h = a.order[a.count]; if (2 > h && (k.sortList.push([e, h]), 1 < a.colSpan)) for (c = 1; c < a.colSpan; c++) k.sortList.push([e + c, h]) } else { if (k.sortAppend && 1 < k.sortList.length) for (c = 0; c < k.sortAppend.length; c++) d = f.isValueInArray(k.sortAppend[c][0], k.sortList), 0 <= d && k.sortList.splice(d, 1); if (0 <= f.isValueInArray(e, k.sortList)) for (c = 0; c < k.sortList.length; c++) d = k.sortList[c], h = k.$headers.filter('[data-column="' + d[0] + '"]:last')[0], d[0] === e && (d[1] = h.order[a.count], 2 === d[1] && (k.sortList.splice(c, 1), h.count = -1)); else if (h = a.order[a.count], 2 > h && (k.sortList.push([e, h]), 1 < a.colSpan)) for (c = 1; c < a.colSpan; c++) k.sortList.push([e + c, h]) } if (null !== k.sortAppend) for (h = k.sortAppend, c = 0; c < h.length; c++) h[c][0] !== e && k.sortList.push(h[c]); m.trigger("sortBegin", b); setTimeout(function () { G(b); I(b); z(b); m.trigger("sortEnd", b) }, 1) } function I(b) { var a, c, h, e, d, k, g, m, y, n, t, q = 0, s = b.config, w = s.textSorter || "", x = s.sortList, z = x.length, B = b.tBodies.length; if (!s.serverSideSorting && !p(s.cache)) { s.debug && (d = new Date); for (c = 0; c < B; c++) k = s.cache[c].colMax, g = s.cache[c].normalized, g.sort(function (c, d) { for (a = 0; a < z; a++) { e = x[a][0]; m = x[a][1]; q = 0 === m; if (s.sortStable && c[e] === d[e] && 1 === z) break; (h = /n/i.test(N(s.parsers, e))) && s.strings[e] ? (h = "boolean" === typeof s.string[s.strings[e]] ? (q ? 1 : -1) * (s.string[s.strings[e]] ? -1 : 1) : s.strings[e] ? s.string[s.strings[e]] || 0 : 0, y = s.numberSorter ? s.numberSorter(c[e], d[e], q, k[e], b) : f["sortNumeric" + (q ? "Asc" : "Desc")](c[e], d[e], h, k[e], e, b)) : (n = q ? c : d, t = q ? d : c, y = "function" === typeof w ? w(n[e], t[e], q, e, b) : "object" === typeof w && w.hasOwnProperty(e) ? w[e](n[e], t[e], q, e, b) : f["sortNatural" + (q ? "Asc" : "Desc")](c[e], d[e], e, b, s)); if (y) return y } return c[s.columns].order - d[s.columns].order }); s.debug && v("Sorting on " + x.toString() + " and dir " + m + " time", d) } } function J(b, a) { b[0].isUpdating && b.trigger("updateComplete"); g.isFunction(a) && a(b[0]) } function H(b, a, c) { var h = b[0].config.sortList; !1 !== a && !b[0].isProcessing && h.length ? b.trigger("sorton", [h, function () { J(b, c) }, !0]) : (J(b, c), f.applyWidget(b[0], !1)) } function K(b) { var a = b.config, c = a.$table; c.unbind("sortReset update updateRows updateCell updateAll addRows updateComplete sorton appendCache updateCache applyWidgetId applyWidgets refreshWidgets destroy mouseup mouseleave ".split(" ").join(a.namespace + " ")).bind("sortReset" + a.namespace, function (c, e) { c.stopPropagation(); a.sortList = []; G(b); I(b); z(b); g.isFunction(e) && e(b) }).bind("updateAll" + a.namespace, function (c, e, d) { c.stopPropagation(); b.isUpdating = !0; f.refreshWidgets(b, !0, !0); f.restoreHeaders(b); D(b); f.bindEvents(b, a.$headers, !0); K(b); E(b, e, d) }).bind("update" + a.namespace + " updateRows" + a.namespace, function (a, c, d) { a.stopPropagation(); b.isUpdating = !0; B(b); E(b, c, d) }).bind("updateCell" + a.namespace, function (h, e, d, f) { h.stopPropagation(); b.isUpdating = !0; c.find(a.selectorRemove).remove(); var l, m; l = c.find("tbody"); m = g(e); h = l.index(g.fn.closest ? m.closest("tbody") : m.parents("tbody").filter(":first")); var p = g.fn.closest ? m.closest("tr") : m.parents("tr").filter(":first"); e = m[0]; l.length && 0 <= h && (l = l.eq(h).find("tr").index(p), m = m.index(), a.cache[h].normalized[l][a.columns].$row = p, e = a.cache[h].normalized[l][m] = "no-parser" === a.parsers[m].id ? "" : a.parsers[m].format(n(b, e, m), b, e, m), "numeric" === (a.parsers[m].type || "").toLowerCase() && (a.cache[h].colMax[m] = Math.max(Math.abs(e) || 0, a.cache[h].colMax[m] || 0)), H(c, d, f)) }).bind("addRows" + a.namespace, function (h, e, d, f) { h.stopPropagation(); b.isUpdating = !0; if (p(a.cache)) B(b), E(b, d, f); else { e = g(e); var l, m, v, u, x = e.filter("tr").length, q = c.find("tbody").index(e.parents("tbody").filter(":first")); a.parsers && a.parsers.length || t(b); for (h = 0; h < x; h++) { m = e[h].cells.length; u = []; v = { child: [], $row: e.eq(h), order: a.cache[q].normalized.length }; for (l = 0; l < m; l++) u[l] = "no-parser" === a.parsers[l].id ? "" : a.parsers[l].format(n(b, e[h].cells[l], l), b, e[h].cells[l], l), "numeric" === (a.parsers[l].type || "").toLowerCase() && (a.cache[q].colMax[l] = Math.max(Math.abs(u[l]) || 0, a.cache[q].colMax[l] || 0)); u.push(v); a.cache[q].normalized.push(u) } H(c, d, f) } }).bind("updateComplete" + a.namespace, function () { b.isUpdating = !1 }).bind("sorton" + a.namespace, function (a, e, d, k) { var l = b.config; a.stopPropagation(); c.trigger("sortStart", this); M(b, e); G(b); l.delayInit && p(l.cache) && x(b); c.trigger("sortBegin", this); I(b); z(b, k); c.trigger("sortEnd", this); f.applyWidget(b); g.isFunction(d) && d(b) }).bind("appendCache" + a.namespace, function (a, c, d) { a.stopPropagation(); z(b, d); g.isFunction(c) && c(b) }).bind("updateCache" + a.namespace, function (c, e) { a.parsers && a.parsers.length || t(b); x(b); g.isFunction(e) && e(b) }).bind("applyWidgetId" + a.namespace, function (c, e) { c.stopPropagation(); f.getWidgetById(e).format(b, a, a.widgetOptions) }).bind("applyWidgets" + a.namespace, function (a, c) { a.stopPropagation(); f.applyWidget(b, c) }).bind("refreshWidgets" + a.namespace, function (a, c, d) { a.stopPropagation(); f.refreshWidgets(b, c, d) }).bind("destroy" + a.namespace, function (a, c, d) { a.stopPropagation(); f.destroy(b, c, d) }).bind("resetToLoadState" + a.namespace, function () { f.refreshWidgets(b, !0, !0); a = g.extend(!0, f.defaults, a.originalSettings); b.hasInitialized = !1; f.setup(b, a) }) } var f = this; f.version = "2.17.2"; f.parsers = []; f.widgets = []; f.defaults = { theme: "default", widthFixed: !1, showProcessing: !1, headerTemplate: "{content}", onRenderTemplate: null, onRenderHeader: null, cancelSelection: !0, tabIndex: !0, dateFormat: "mmddyyyy", sortMultiSortKey: "shiftKey", sortResetKey: "ctrlKey", usNumberFormat: !0, delayInit: !1, serverSideSorting: !1, headers: {}, ignoreCase: !0, sortForce: null, sortList: [], sortAppend: null, sortStable: !1, sortInitialOrder: "asc", sortLocaleCompare: !1, sortReset: !1, sortRestart: !1, emptyTo: "bottom", stringTo: "max", textExtraction: "basic", textAttribute: "data-text", textSorter: null, numberSorter: null, widgets: [], widgetOptions: { zebra: ["even", "odd"] }, initWidgets: !0, initialized: null, tableClass: "", cssAsc: "", cssDesc: "", cssNone: "", cssHeader: "", cssHeaderRow: "", cssProcessing: "", cssChildRow: "tablesorter-childRow", cssIcon: "tablesorter-icon", cssInfoBlock: "tablesorter-infoOnly", selectorHeaders: "> thead th, > thead td", selectorSort: "th, td", selectorRemove: ".remove-me", debug: !1, headerList: [], empties: {}, strings: {}, parsers: [] }; f.css = { table: "tablesorter", cssHasChild: "tablesorter-hasChildRow", childRow: "tablesorter-childRow", header: "tablesorter-header", headerRow: "tablesorter-headerRow", headerIn: "tablesorter-header-inner", icon: "tablesorter-icon", info: "tablesorter-infoOnly", processing: "tablesorter-processing", sortAsc: "tablesorter-headerAsc", sortDesc: "tablesorter-headerDesc", sortNone: "tablesorter-headerUnSorted" }; f.language = { sortAsc: "Ascending sort applied, ", sortDesc: "Descending sort applied, ", sortNone: "No sort applied, ", nextAsc: "activate to apply an ascending sort", nextDesc: "activate to apply a descending sort", nextNone: "activate to remove the sort" }; f.log = d; f.benchmark = v; f.construct = function (b) { return this.each(function () { var a = g.extend(!0, {}, f.defaults, b); a.originalSettings = b; !this.hasInitialized && f.buildTable && "TABLE" !== this.tagName ? f.buildTable(this, a) : f.setup(this, a) }) }; f.setup = function (b, a) { if (!b || !b.tHead || 0 === b.tBodies.length || !0 === b.hasInitialized) return a.debug ? d("ERROR: stopping initialization! No table, thead, tbody or tablesorter has already been initialized") : ""; var c = "", h = g(b), e = g.metadata; b.hasInitialized = !1; b.isProcessing = !0; b.config = a; g.data(b, "tablesorter", a); a.debug && g.data(b, "startoveralltimer", new Date); a.supportsDataObject = function (a) { a[0] = parseInt(a[0], 10); return 1 < a[0] || 1 === a[0] && 4 <= parseInt(a[1], 10) } (g.fn.jquery.split(".")); a.string = { max: 1, min: -1, emptyMin: 1, emptyMax: -1, zero: 0, none: 0, "null": 0, top: !0, bottom: !1 }; /tablesorter\-/.test(h.attr("class")) || (c = "" !== a.theme ? " tablesorter-" + a.theme : ""); a.$table = h.addClass(f.css.table + " " + a.tableClass + c).attr({ role: "grid" }); a.$headers = g(b).find(a.selectorHeaders); a.namespace = a.namespace ? "." + a.namespace.replace(/\W/g, "") : ".tablesorter" + Math.random().toString(16).slice(2); a.$tbodies = h.children("tbody:not(." + a.cssInfoBlock + ")").attr({ "aria-live": "polite", "aria-relevant": "all" }); a.$table.find("caption").length && a.$table.attr("aria-labelledby", "theCaption"); a.widgetInit = {}; a.textExtraction = a.$table.attr("data-text-extraction") || a.textExtraction || "basic"; D(b); L(b); t(b); a.delayInit || x(b); f.bindEvents(b, a.$headers, !0); K(b); a.supportsDataObject && "undefined" !== typeof h.data().sortlist ? a.sortList = h.data().sortlist : e && h.metadata() && h.metadata().sortlist && (a.sortList = h.metadata().sortlist); f.applyWidget(b, !0); 0 < a.sortList.length ? h.trigger("sorton", [a.sortList, {}, !a.initWidgets, !0]) : (G(b), a.initWidgets && f.applyWidget(b, !1)); a.showProcessing && h.unbind("sortBegin" + a.namespace + " sortEnd" + a.namespace).bind("sortBegin" + a.namespace + " sortEnd" + a.namespace, function (c) { clearTimeout(a.processTimer); f.isProcessing(b); "sortBegin" === c.type && (a.processTimer = setTimeout(function () { f.isProcessing(b, !0) }, 500)) }); b.hasInitialized = !0; b.isProcessing = !1; a.debug && f.benchmark("Overall initialization time", g.data(b, "startoveralltimer")); h.trigger("tablesorter-initialized", b); "function" === typeof a.initialized && a.initialized(b) }; f.getColumnData = function (b, a, c, h) { if ("undefined" !== typeof a && null !== a) { b = g(b)[0]; var e, d = b.config; if (a[c]) return h ? a[c] : a[d.$headers.index(d.$headers.filter('[data-column="' + c + '"]:last'))]; for (e in a) if ("string" === typeof e && (b = h ? d.$headers.eq(c).filter(e) : d.$headers.filter('[data-column="' + c + '"]:last').filter(e), b.length)) return a[e] } }; f.computeColumnIndex = function (b) { var a = [], c = 0, h, e, d, f, l, m, p, v, n, q; for (h = 0; h < b.length; h++) for (l = b[h].cells, e = 0; e < l.length; e++) { d = l[e]; f = g(d); m = d.parentNode.rowIndex; f.index(); p = d.rowSpan || 1; v = d.colSpan || 1; "undefined" === typeof a[m] && (a[m] = []); for (d = 0; d < a[m].length + 1; d++) if ("undefined" === typeof a[m][d]) { n = d; break } c = Math.max(n, c); f.attr({ "data-column": n }); for (d = m; d < m + p; d++) for ("undefined" === typeof a[d] && (a[d] = []), q = a[d], f = n; f < n + v; f++) q[f] = "x" } return c + 1 }; f.isProcessing = function (b, a, c) { b = g(b); var d = b[0].config; b = c || b.find("." + f.css.header); a ? ("undefined" !== typeof c && 0 < d.sortList.length && (b = b.filter(function () { return this.sortDisabled ? !1 : 0 <= f.isValueInArray(parseFloat(g(this).attr("data-column")), d.sortList) })), b.addClass(f.css.processing + " " + d.cssProcessing)) : b.removeClass(f.css.processing + " " + d.cssProcessing) }; f.processTbody = function (b, a, c) { b = g(b)[0]; if (c) return b.isProcessing = !0, a.before('<span class="tablesorter-savemyplace"/>'), c = g.fn.detach ? a.detach() : a.remove(); c = g(b).find("span.tablesorter-savemyplace"); a.insertAfter(c); c.remove(); b.isProcessing = !1 }; f.clearTableBody = function (b) { g(b)[0].config.$tbodies.children().detach() }; f.bindEvents = function (b, a, c) { b = g(b)[0]; var d, e = b.config; !0 !== c && (e.$extraHeaders = e.$extraHeaders ? e.$extraHeaders.add(a) : a); a.find(e.selectorSort).add(a.filter(e.selectorSort)).unbind(["mousedown", "mouseup", "sort", "keyup", ""].join(e.namespace + " ")).bind(["mousedown", "mouseup", "sort", "keyup", ""].join(e.namespace + " "), function (c, f) { var l; l = c.type; if (!(1 !== (c.which || c.button) && !/sort|keyup/.test(l) || "keyup" === l && 13 !== c.which || "mouseup" === l && !0 !== f && 250 < (new Date).getTime() - d)) { if ("mousedown" === l) return d = (new Date).getTime(), /(input|select|button|textarea)/i.test(c.target.tagName) ? "" : !e.cancelSelection; e.delayInit && p(e.cache) && x(b); l = g.fn.closest ? g(this).closest("th, td")[0] : /TH|TD/.test(this.tagName) ? this : g(this).parents("th, td")[0]; l = e.$headers[a.index(l)]; l.sortDisabled || O(b, l, c) } }); e.cancelSelection && a.attr("unselectable", "on").bind("selectstart", !1).css({ "user-select": "none", MozUserSelect: "none" }) }; f.restoreHeaders = function (b) { var a = g(b)[0].config; a.$table.find(a.selectorHeaders).each(function (b) { g(this).find("." + f.css.headerIn).length && g(this).html(a.headerContent[b]) }) }; f.destroy = function (b, a, c) { b = g(b)[0]; if (b.hasInitialized) { f.refreshWidgets(b, !0, !0); var d = g(b), e = b.config, r = d.find("thead:first"), k = r.find("tr." + f.css.headerRow).removeClass(f.css.headerRow + " " + e.cssHeaderRow), l = d.find("tfoot:first > tr").children("th, td"); !1 === a && 0 <= g.inArray("uitheme", e.widgets) && (d.trigger("applyWidgetId", ["uitheme"]), d.trigger("applyWidgetId", ["zebra"])); r.find("tr").not(k).remove(); d.removeData("tablesorter").unbind("sortReset update updateAll updateRows updateCell addRows updateComplete sorton appendCache updateCache applyWidgetId applyWidgets refreshWidgets destroy mouseup mouseleave keypress sortBegin sortEnd resetToLoadState ".split(" ").join(e.namespace + " ")); e.$headers.add(l).removeClass([f.css.header, e.cssHeader, e.cssAsc, e.cssDesc, f.css.sortAsc, f.css.sortDesc, f.css.sortNone].join(" ")).removeAttr("data-column").removeAttr("aria-label").attr("aria-disabled", "true"); k.find(e.selectorSort).unbind(["mousedown", "mouseup", "keypress", ""].join(e.namespace + " ")); f.restoreHeaders(b); d.toggleClass(f.css.table + " " + e.tableClass + " tablesorter-" + e.theme, !1 === a); b.hasInitialized = !1; delete b.config.cache; "function" === typeof c && c(b) } }; f.regex = { chunk: /(^([+\-]?(?:0|[1-9]\d*)(?:\.\d*)?(?:[eE][+\-]?\d+)?)?$|^0x[0-9a-f]+$|\d+)/gi, chunks: /(^\\0|\\0$)/, hex: /^0x[0-9a-f]+$/i }; f.sortNatural = function (b, a) { if (b === a) return 0; var c, d, e, g, k, l; d = f.regex; if (d.hex.test(a)) { c = parseInt(b.match(d.hex), 16); e = parseInt(a.match(d.hex), 16); if (c < e) return -1; if (c > e) return 1 } c = b.replace(d.chunk, "\\0$1\\0").replace(d.chunks, "").split("\\0"); d = a.replace(d.chunk, "\\0$1\\0").replace(d.chunks, "").split("\\0"); l = Math.max(c.length, d.length); for (k = 0; k < l; k++) { e = isNaN(c[k]) ? c[k] || 0 : parseFloat(c[k]) || 0; g = isNaN(d[k]) ? d[k] || 0 : parseFloat(d[k]) || 0; if (isNaN(e) !== isNaN(g)) return isNaN(e) ? 1 : -1; typeof e !== typeof g && (e += "", g += ""); if (e < g) return -1; if (e > g) return 1 } return 0 }; f.sortNaturalAsc = function (b, a, c, d, e) { if (b === a) return 0; c = e.string[e.empties[c] || e.emptyTo]; return "" === b && 0 !== c ? "boolean" === typeof c ? c ? -1 : 1 : -c || -1 : "" === a && 0 !== c ? "boolean" === typeof c ? c ? 1 : -1 : c || 1 : f.sortNatural(b, a) }; f.sortNaturalDesc = function (b, a, c, d, e) { if (b === a) return 0; c = e.string[e.empties[c] || e.emptyTo]; return "" === b && 0 !== c ? "boolean" === typeof c ? c ? -1 : 1 : c || 1 : "" === a && 0 !== c ? "boolean" === typeof c ? c ? 1 : -1 : -c || -1 : f.sortNatural(a, b) }; f.sortText = function (b, a) { return b > a ? 1 : b < a ? -1 : 0 }; f.getTextValue = function (b, a, c) { if (c) { var d = b ? b.length : 0, e = c + a; for (c = 0; c < d; c++) e += b.charCodeAt(c); return a * e } return 0 }; f.sortNumericAsc = function (b, a, c, d, e, g) { if (b === a) return 0; g = g.config; e = g.string[g.empties[e] || g.emptyTo]; if ("" === b && 0 !== e) return "boolean" === typeof e ? e ? -1 : 1 : -e || -1; if ("" === a && 0 !== e) return "boolean" === typeof e ? e ? 1 : -1 : e || 1; isNaN(b) && (b = f.getTextValue(b, c, d)); isNaN(a) && (a = f.getTextValue(a, c, d)); return b - a }; f.sortNumericDesc = function (b, a, c, d, e, g) { if (b === a) return 0; g = g.config; e = g.string[g.empties[e] || g.emptyTo]; if ("" === b && 0 !== e) return "boolean" === typeof e ? e ? -1 : 1 : e || 1; if ("" === a && 0 !== e) return "boolean" === typeof e ? e ? 1 : -1 : -e || -1; isNaN(b) && (b = f.getTextValue(b, c, d)); isNaN(a) && (a = f.getTextValue(a, c, d)); return a - b }; f.sortNumeric = function (b, a) { return b - a }; f.characterEquivalents = { a: "\u00e1\u00e0\u00e2\u00e3\u00e4\u0105\u00e5", A: "\u00c1\u00c0\u00c2\u00c3\u00c4\u0104\u00c5", c: "\u00e7\u0107\u010d", C: "\u00c7\u0106\u010c", e: "\u00e9\u00e8\u00ea\u00eb\u011b\u0119", E: "\u00c9\u00c8\u00ca\u00cb\u011a\u0118", i: "\u00ed\u00ec\u0130\u00ee\u00ef\u0131", I: "\u00cd\u00cc\u0130\u00ce\u00cf", o: "\u00f3\u00f2\u00f4\u00f5\u00f6", O: "\u00d3\u00d2\u00d4\u00d5\u00d6", ss: "\u00df", SS: "\u1e9e", u: "\u00fa\u00f9\u00fb\u00fc\u016f", U: "\u00da\u00d9\u00db\u00dc\u016e" }; f.replaceAccents = function (b) { var a, c = "[", d = f.characterEquivalents; if (!f.characterRegex) { f.characterRegexArray = {}; for (a in d) "string" === typeof a && (c += d[a], f.characterRegexArray[a] = new RegExp("[" + d[a] + "]", "g")); f.characterRegex = new RegExp(c + "]") } if (f.characterRegex.test(b)) for (a in d) "string" === typeof a && (b = b.replace(f.characterRegexArray[a], a)); return b }; f.isValueInArray = function (b, a) { var c, d = a.length; for (c = 0; c < d; c++) if (a[c][0] === b) return c; return -1 }; f.addParser = function (b) { var a, c = f.parsers.length, d = !0; for (a = 0; a < c; a++) f.parsers[a].id.toLowerCase() === b.id.toLowerCase() && (d = !1); d && f.parsers.push(b) }; f.getParserById = function (b) { if ("false" == b) return !1; var a, c = f.parsers.length; for (a = 0; a < c; a++) if (f.parsers[a].id.toLowerCase() === b.toString().toLowerCase()) return f.parsers[a]; return !1 }; f.addWidget = function (b) { f.widgets.push(b) }; f.getWidgetById = function (b) { var a, c, d = f.widgets.length; for (a = 0; a < d; a++) if ((c = f.widgets[a]) && c.hasOwnProperty("id") && c.id.toLowerCase() === b.toLowerCase()) return c }; f.applyWidget = function (b, a) { b = g(b)[0]; var c = b.config, d = c.widgetOptions, e = [], p, k, l; !1 !== a && b.hasInitialized && (b.isApplyingWidgets || b.isUpdating) || (c.debug && (p = new Date), c.widgets.length && (b.isApplyingWidgets = !0, c.widgets = g.grep(c.widgets, function (a, b) { return g.inArray(a, c.widgets) === b }), g.each(c.widgets || [], function (a, b) { (l = f.getWidgetById(b)) && l.id && (l.priority || (l.priority = 10), e[a] = l) }), e.sort(function (a, b) { return a.priority < b.priority ? -1 : a.priority === b.priority ? 0 : 1 }), g.each(e, function (e, f) { if (f) { if (a || !c.widgetInit[f.id]) f.hasOwnProperty("options") && (d = b.config.widgetOptions = g.extend(!0, {}, f.options, d)), f.hasOwnProperty("init") && f.init(b, f, c, d), c.widgetInit[f.id] = !0; !a && f.hasOwnProperty("format") && f.format(b, c, d, !1) } })), setTimeout(function () { b.isApplyingWidgets = !1 }, 0), c.debug && (k = c.widgets.length, v("Completed " + (!0 === a ? "initializing " : "applying ") + k + " widget" + (1 !== k ? "s" : ""), p))) }; f.refreshWidgets = function (b, a, c) { b = g(b)[0]; var h, e = b.config, p = e.widgets, k = f.widgets, l = k.length; for (h = 0; h < l; h++) k[h] && k[h].id && (a || 0 > g.inArray(k[h].id, p)) && (e.debug && d('Refeshing widgets: Removing "' + k[h].id + '"'), k[h].hasOwnProperty("remove") && e.widgetInit[k[h].id] && (k[h].remove(b, e, e.widgetOptions), e.widgetInit[k[h].id] = !1)); !0 !== c && f.applyWidget(b, a) }; f.getData = function (b, a, c) { var d = ""; b = g(b); var e, f; if (!b.length) return ""; e = g.metadata ? b.metadata() : !1; f = " " + (b.attr("class") || ""); "undefined" !== typeof b.data(c) || "undefined" !== typeof b.data(c.toLowerCase()) ? d += b.data(c) || b.data(c.toLowerCase()) : e && "undefined" !== typeof e[c] ? d += e[c] : a && "undefined" !== typeof a[c] ? d += a[c] : " " !== f && f.match(" " + c + "-") && (d = f.match(new RegExp("\\s" + c + "-([\\w-]+)"))[1] || ""); return g.trim(d) }; f.formatFloat = function (b, a) { if ("string" !== typeof b || "" === b) return b; var c; b = (a && a.config ? !1 !== a.config.usNumberFormat : "undefined" !== typeof a ? a : 1) ? b.replace(/,/g, "") : b.replace(/[\s|\.]/g, "").replace(/,/g, "."); /^\s*\([.\d]+\)/.test(b) && (b = b.replace(/^\s*\(([.\d]+)\)/, "-$1")); c = parseFloat(b); return isNaN(c) ? g.trim(b) : c }; f.isDigit = function (b) { return isNaN(b) ? /^[\-+(]?\d+[)]?$/.test(b.toString().replace(/[,.'"\s]/g, "")) : !0 } } }); var n = g.tablesorter; g.fn.extend({ tablesorter: n.construct }); n.addParser({ id: "no-parser", is: function () { return !1 }, format: function () { return "" }, type: "text" }); n.addParser({ id: "text", is: function () { return !0 }, format: function (d, v) { var p = v.config; d && (d = g.trim(p.ignoreCase ? d.toLocaleLowerCase() : d), d = p.sortLocaleCompare ? n.replaceAccents(d) : d); return d }, type: "text" }); n.addParser({ id: "digit", is: function (d) { return n.isDigit(d) }, format: function (d, v) { var p = n.formatFloat((d || "").replace(/[^\w,. \-()]/g, ""), v); return d && "number" === typeof p ? p : d ? g.trim(d && v.config.ignoreCase ? d.toLocaleLowerCase() : d) : d }, type: "numeric" }); n.addParser({ id: "currency", is: function (d) { return /^\(?\d+[\u00a3$\u20ac\u00a4\u00a5\u00a2?.]|[\u00a3$\u20ac\u00a4\u00a5\u00a2?.]\d+\)?$/.test((d || "").replace(/[+\-,. ]/g, "")) }, format: function (d, v) { var p = n.formatFloat((d || "").replace(/[^\w,. \-()]/g, ""), v); return d && "number" === typeof p ? p : d ? g.trim(d && v.config.ignoreCase ? d.toLocaleLowerCase() : d) : d }, type: "numeric" }); n.addParser({ id: "ipAddress", is: function (d) { return /^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$/.test(d) }, format: function (d, g) { var p, w = d ? d.split(".") : "", t = "", x = w.length; for (p = 0; p < x; p++) t += ("00" + w[p]).slice(-3); return d ? n.formatFloat(t, g) : d }, type: "numeric" }); n.addParser({ id: "url", is: function (d) { return /^(https?|ftp|file):\/\//.test(d) }, format: function (d) { return d ? g.trim(d.replace(/(https?|ftp|file):\/\//, "")) : d }, type: "text" }); n.addParser({ id: "isoDate", is: function (d) { return /^\d{4}[\/\-]\d{1,2}[\/\-]\d{1,2}/.test(d) }, format: function (d, g) { return d ? n.formatFloat("" !== d ? (new Date(d.replace(/-/g, "/"))).getTime() || d : "", g) : d }, type: "numeric" }); n.addParser({ id: "percent", is: function (d) { return /(\d\s*?%|%\s*?\d)/.test(d) && 15 > d.length }, format: function (d, g) { return d ? n.formatFloat(d.replace(/%/g, ""), g) : d }, type: "numeric" }); n.addParser({ id: "usLongDate", is: function (d) { return /^[A-Z]{3,10}\.?\s+\d{1,2},?\s+(\d{4})(\s+\d{1,2}:\d{2}(:\d{2})?(\s+[AP]M)?)?$/i.test(d) || /^\d{1,2}\s+[A-Z]{3,10}\s+\d{4}/i.test(d) }, format: function (d, g) { return d ? n.formatFloat((new Date(d.replace(/(\S)([AP]M)$/i, "$1 $2"))).getTime() || d, g) : d }, type: "numeric" }); n.addParser({ id: "shortDate", is: function (d) { return /(^\d{1,2}[\/\s]\d{1,2}[\/\s]\d{4})|(^\d{4}[\/\s]\d{1,2}[\/\s]\d{1,2})/.test((d || "").replace(/\s+/g, " ").replace(/[\-.,]/g, "/")) }, format: function (d, g, p, w) { if (d) { p = g.config; var t = p.$headers.filter("[data-column=" + w + "]:last"); w = t.length && t[0].dateFormat || n.getData(t, n.getColumnData(g, p.headers, w), "dateFormat") || p.dateFormat; d = d.replace(/\s+/g, " ").replace(/[\-.,]/g, "/"); "mmddyyyy" === w ? d = d.replace(/(\d{1,2})[\/\s](\d{1,2})[\/\s](\d{4})/, "$3/$1/$2") : "ddmmyyyy" === w ? d = d.replace(/(\d{1,2})[\/\s](\d{1,2})[\/\s](\d{4})/, "$3/$2/$1") : "yyyymmdd" === w && (d = d.replace(/(\d{4})[\/\s](\d{1,2})[\/\s](\d{1,2})/, "$1/$2/$3")) } return d ? n.formatFloat((new Date(d)).getTime() || d, g) : d }, type: "numeric" }); n.addParser({ id: "time", is: function (d) { return /^(([0-2]?\d:[0-5]\d)|([0-1]?\d:[0-5]\d\s?([AP]M)))$/i.test(d) }, format: function (d, g) { return d ? n.formatFloat((new Date("2000/01/01 " + d.replace(/(\S)([AP]M)$/i, "$1 $2"))).getTime() || d, g) : d }, type: "numeric" }); n.addParser({ id: "metadata", is: function () { return !1 }, format: function (d, n, p) { d = n.config; d = d.parserMetadataName ? d.parserMetadataName : "sortValue"; return g(p).metadata()[d] }, type: "numeric" }); n.addWidget({ id: "zebra", priority: 90, format: function (d, v, p) { var w, t, x, z, C, D, E = new RegExp(v.cssChildRow, "i"), B = v.$tbodies; v.debug && (C = new Date); for (d = 0; d < B.length; d++) w = B.eq(d), D = w.children("tr").length, 1 < D && (x = 0, w = w.children("tr:visible").not(v.selectorRemove), w.each(function () { t = g(this); E.test(this.className) || x++; z = 0 === x % 2; t.removeClass(p.zebra[z ? 1 : 0]).addClass(p.zebra[z ? 0 : 1]) })); v.debug && n.benchmark("Applying Zebra widget", C) }, remove: function (d, n, p) { var w; n = n.$tbodies; var t = (p.zebra || ["even", "odd"]).join(" "); for (p = 0; p < n.length; p++) w = g.tablesorter.processTbody(d, n.eq(p), !0), w.children().removeClass(t), g.tablesorter.processTbody(d, w, !1) } }) } (jQuery);

/* tablesorter pager plugin updated 6/18/2014 (v2.17.2) */
; (function (h) { var k = h.tablesorter; h.extend({ tablesorterPager: new function () { this.defaults = { container: null, ajaxUrl: null, customAjaxUrl: function (b, a) { return a }, ajaxObject: { dataType: "json" }, processAjaxOnInit: !0, ajaxProcessing: function (b) { return [0, [], null] }, output: "{startRow} - {endRow} de {totalRows}", updateArrows: !0, page: 0, pageReset: 0, size: 10, savePages: !0, storageKey: "tablesorter-pager", fixedHeight: !1, countChildRows: !1, removeRows: !1, cssFirst: ".first", cssPrev: ".prev", cssNext: ".next", cssLast: ".last", cssGoto: ".gotoPage", cssPageDisplay: ".pagedisplay", cssPageSize: ".pagesize", cssErrorRow: "tablesorter-errorRow", cssDisabled: "disabled", totalRows: 0, totalPages: 0, filteredRows: 0, filteredPages: 0, ajaxCounter: 0, currentFilters: [], startRow: 0, endRow: 0, $size: null, last: {} }; var w = this, p = function (b, a) { var c = b.cssDisabled, e = !!a, f = e || 0 === b.page, g = Math.min(b.totalPages, b.filteredPages), e = e || b.page === g - 1 || 0 === b.totalPages; b.updateArrows && (b.$container.find(b.cssFirst + "," + b.cssPrev)[f ? "addClass" : "removeClass"](c).attr("aria-disabled", f), b.$container.find(b.cssNext + "," + b.cssLast)[e ? "addClass" : "removeClass"](c).attr("aria-disabled", e)) }, t = function (b, a, c) { var e, f, g, l = b.config; e = l.$table.hasClass("hasFilters") && !a.ajaxUrl; g = []; f = a.size || 10; g = [l.widgetOptions && l.widgetOptions.filter_filteredRow || "filtered", l.selectorRemove]; a.countChildRows && g.push(l.cssChildRow); g.join("|"); a.totalPages = Math.ceil(a.totalRows / f); a.filteredRows = e ? 0 : a.totalRows; a.filteredPages = a.totalPages; e && (h.each(l.cache[0].normalized, function (d, b) { a.filteredRows += a.regexRows.test(b[l.columns].$row[0].className) ? 0 : 1 }), a.filteredPages = Math.ceil(a.filteredRows / f) || 0); if (0 <= Math.min(a.totalPages, a.filteredPages) && (g = a.size * a.page > a.filteredRows, a.startRow = g ? 1 : 0 === a.filteredRows ? 0 : a.size * a.page + 1, a.page = g ? 0 : a.page, a.endRow = Math.min(a.filteredRows, a.totalRows, a.size * (a.page + 1)), e = a.$container.find(a.cssPageDisplay), g = (a.ajaxData && a.ajaxData.output ? a.ajaxData.output || a.output : a.output).replace(/\{page([\-+]\d+)?\}/gi, function (d, b) { return a.totalPages ? a.page + (b ? parseInt(b, 10) : 1) : 0 }).replace(/\{\w+(\s*:\s*\w+)?\}/gi, function (d) { d = d.replace(/[{}\s]/g, ""); var b = d.split(":"), c = a.ajaxData, e = /(rows?|pages?)$/i.test(d) ? 0 : ""; return 1 < b.length && c && c[b[0]] ? c[b[0]][b[1]] : a[d] || (c ? c[d] : e) || e }), e.length && (e["INPUT" === e[0].tagName ? "val" : "html"](g), a.$goto.length))) { g = ""; f = Math.min(a.totalPages, a.filteredPages); for (e = 1; e <= f; e++) g += "<option>" + e + "</option>"; a.$goto.html(g).val(a.page + 1) } p(a); a.initialized && !1 !== c && (l.$table.trigger("pagerComplete", a), a.savePages && k.storage && k.storage(b, a.storageKey, { page: a.page, size: a.size })) }, u = function (b, a) { var c, e = b.config, f = e.$tbodies.eq(0); a.fixedHeight && (f.find("tr.pagerSavedHeightSpacer").remove(), c = h.data(b, "pagerSavedHeight")) && (c -= f.height(), 5 < c && h.data(b, "pagerLastSize") === a.size && f.children("tr:visible").length < a.size && f.append('<tr class="pagerSavedHeightSpacer ' + e.selectorRemove.replace(/(tr)?\./g, "") + '" style="height:' + c + 'px;"></tr>')) }, z = function (b, a) { var c = b.config.$tbodies.eq(0); c.find("tr.pagerSavedHeightSpacer").remove(); h.data(b, "pagerSavedHeight", c.height()); u(b, a); h.data(b, "pagerLastSize", a.size) }, v = function (b, a) { if (!a.ajaxUrl) { var c, e = 0, f = b.config, g = f.$tbodies.eq(0).children("tr"), h = g.length, d = a.page * a.size, m = d + a.size, n = f.widgetOptions && f.widgetOptions.filter_filteredRow || "filtered", r = 0; for (c = 0; c < h; c++) g[c].className.match(n) || (r === d && g[c].className.match(f.cssChildRow) ? g[c].style.display = "none" : (g[c].style.display = r >= d && r < m ? "" : "none", r += g[c].className.match(f.cssChildRow + "|" + f.selectorRemove.slice(1)) && !a.countChildRows ? 0 : 1, r === m && "none" !== g[c].style.display && g[c].className.match(k.css.cssHasChild) && (e = c))); if (0 < e && g[e].className.match(k.css.cssHasChild)) for (; ++e < h && g[e].className.match(f.cssChildRow); ) g[e].style.display = "" } }, A = function (b, a) { a.size = parseInt(a.$size.val(), 10) || a.size; h.data(b, "pagerLastSize", a.size); p(a); a.removeRows || (v(b, a), h(b).bind("sortEnd.pager filterEnd.pager", function () { v(b, a) })) }, B = function (b, a, c, e, f) { if ("function" === typeof c.ajaxProcessing) { var g, l, d, m, n, r, s = a.config, q = s.$table, p = ""; g = c.ajaxProcessing(b, a, e) || [0, []]; b = q.find("thead th").length; k.showError(a); if (f) s.debug && k.log("Ajax Error", e, f), k.showError(a, 0 === e.status ? "Not connected, verify Network" : 404 === e.status ? "Requested page not found [404]" : 500 === e.status ? "Internal Server Error [500]" : "parsererror" === f ? "Requested JSON parse failed" : "timeout" === f ? "Time out error" : "abort" === f ? "Ajax Request aborted" : "Uncaught error: " + e.statusText + " [" + e.status + "]"), s.$tbodies.eq(0).children().detach(), c.totalRows = 0; else { h.isArray(g) ? (e = isNaN(g[0]) && !isNaN(g[1]), f = g[e ? 1 : 0], c.totalRows = isNaN(f) ? c.totalRows || 0 : f, e = 0 === c.totalRows ? [""] : g[e ? 0 : 1] || [], n = g[2]) : (c.ajaxData = g, c.totalRows = g.total, n = g.headers, e = g.rows); r = e.length; if (e instanceof jQuery) c.processAjaxOnInit && (s.$tbodies.eq(0).children().detach(), s.$tbodies.eq(0).append(e)); else if (r) { for (g = 0; g < r; g++) { p += "<tr>"; for (f = 0; f < e[g].length; f++) p += /^\s*<td/.test(e[g][f]) ? h.trim(e[g][f]) : "<td>" + e[g][f] + "</td>"; p += "</tr>" } c.processAjaxOnInit && s.$tbodies.eq(0).html(p) } c.processAjaxOnInit = !0; n && n.length === b && (m = (l = q.hasClass("hasStickyHeaders")) ? s.widgetOptions.$sticky.children("thead:first").children("tr").children() : "", d = q.find("tfoot tr:first").children(), s.$headers.filter("th").each(function (a) { var b = h(this), c; b.find("." + k.css.icon).length ? (c = b.find("." + k.css.icon).clone(!0), b.find(".tablesorter-header-inner").html(n[a]).append(c), l && m.length && (c = m.eq(a).find("." + k.css.icon).clone(!0), m.eq(a).find(".tablesorter-header-inner").html(n[a]).append(c))) : (b.find(".tablesorter-header-inner").html(n[a]), l && m.length && m.eq(a).find(".tablesorter-header-inner").html(n[a])); d.eq(a).html(n[a]) })) } s.showProcessing && k.isProcessing(a); c.totalPages = Math.ceil(c.totalRows / (c.size || 10)); c.last.totalRows = c.totalRows; c.last.currentFilters = c.currentFilters; c.last.sortList = (s.sortList || []).join(","); t(a, c); u(a, c); q.trigger("updateCache", [function () { c.initialized && setTimeout(function () { q.trigger("applyWidgets").trigger("pagerChange", c) }, 0) } ]) } c.initialized || (c.initialized = !0, h(a).trigger("applyWidgets").trigger("pagerInitialized", c)) }, G = function (b, a) { var c = F(b, a), e = h(document), f, g = b.config; "" !== c && (g.showProcessing && k.isProcessing(b, !0), e.bind("ajaxError.pager", function (c, d, f, g) { B(null, b, a, d, g); e.unbind("ajaxError.pager") }), f = ++a.ajaxCounter, a.ajaxObject.url = c, a.ajaxObject.success = function (c, d, g) { f < a.ajaxCounter || (B(c, b, a, g), e.unbind("ajaxError.pager"), "function" === typeof a.oldAjaxSuccess && a.oldAjaxSuccess(c)) }, g.debug && k.log("ajax initialized", a.ajaxObject), h.ajax(a.ajaxObject)) }, F = function (b, a) { var c = b.config, e = a.ajaxUrl ? a.ajaxUrl.replace(/\{page([\-+]\d+)?\}/, function (b, c) { return a.page + (c ? parseInt(c, 10) : 0) }).replace(/\{size\}/g, a.size) : "", f = c.sortList, g = a.currentFilters || h(b).data("lastSearch") || [], l = e.match(/\{\s*sort(?:List)?\s*:\s*(\w*)\s*\}/), d = e.match(/\{\s*filter(?:List)?\s*:\s*(\w*)\s*\}/), m = []; l && (l = l[1], h.each(f, function (a, b) { m.push(l + "[" + b[0] + "]=" + b[1]) }), e = e.replace(/\{\s*sort(?:List)?\s*:\s*(\w*)\s*\}/g, m.length ? m.join("&") : l), m = []); d && (d = d[1], h.each(g, function (a, b) { b && m.push(d + "[" + a + "]=" + encodeURIComponent(b)) }), e = e.replace(/\{\s*filter(?:List)?\s*:\s*(\w*)\s*\}/g, m.length ? m.join("&") : d), a.currentFilters = g); "function" === typeof a.customAjaxUrl && (e = a.customAjaxUrl(b, e)); c.debug && k.log("Pager ajax url: " + e); return e }, x = function (b, a, c) { var e, f, g, l, d = h(b); e = b.config; var m = e.$table.hasClass("hasFilters"), n = c.page * c.size, r = c.size; if (!(1 > (a && a.length || 0))) { c.page >= c.totalPages && C(b, c); c.isDisabled = !1; c.initialized && d.trigger("pagerChange", c); if (c.removeRows) { k.clearTableBody(b); e = k.processTbody(b, e.$tbodies.eq(0), !0); f = m ? 0 : n; g = m ? 0 : n; for (l = 0; l < r && f < a.length; ) m && /filtered/.test(a[f][0].className) || (g++, g > n && l <= r && (l++, e.append(a[f]))), f++; k.processTbody(b, e, !1) } else v(b, c); t(b, c); c.isDisabled || u(b, c); d.trigger("applyWidgets"); b.isUpdating && d.trigger("updateComplete", b) } }, D = function (b, a) { a.ajax ? p(a, !0) : (a.isDisabled = !0, h.data(b, "pagerLastPage", a.page), h.data(b, "pagerLastSize", a.size), a.page = 0, a.size = a.totalRows, a.totalPages = 1, h(b).addClass("pagerDisabled").removeAttr("aria-describedby").find("tr.pagerSavedHeightSpacer").remove(), x(b, b.config.rowsCopy, a), b.config.debug && k.log("pager disabled")); a.$size.add(a.$goto).each(function () { h(this).attr("aria-disabled", "true").addClass(a.cssDisabled)[0].disabled = !0 }) }, q = function (b, a, c) { if (!a.isDisabled) { var e = b.config, f = h(b), g = a.last, l = Math.min(a.totalPages, a.filteredPages); 0 > a.page && (a.page = 0); a.page > l - 1 && 0 !== l && (a.page = l - 1); g.currentFilters = "" === (g.currentFilters || []).join("") ? [] : g.currentFilters; a.currentFilters = "" === (a.currentFilters || []).join("") ? [] : a.currentFilters; if (g.page !== a.page || g.size !== a.size || g.totalRows !== a.totalRows || (g.currentFilters || []).join(",") !== (a.currentFilters || []).join(",") || g.sortList !== (e.sortList || []).join(",")) e.debug && k.log("Pager changing to page " + a.page), a.last = { page: a.page, size: a.size, sortList: (e.sortList || []).join(","), totalRows: a.totalRows, currentFilters: a.currentFilters || [] }, a.ajax ? G(b, a) : a.ajax || x(b, e.rowsCopy, a), h.data(b, "pagerLastPage", a.page), a.initialized && !1 !== c && (f.trigger("pageMoved", a).trigger("applyWidgets"), b.isUpdating && f.trigger("updateComplete")) } }, y = function (b, a, c) { c.size = a || c.size || 10; c.$size.val(c.size); h.data(b, "pagerLastPage", c.page); h.data(b, "pagerLastSize", c.size); c.totalPages = Math.ceil(c.totalRows / c.size); c.filteredPages = Math.ceil(c.filteredRows / c.size); q(b, c) }, H = function (b, a) { a.page = 0; q(b, a) }, C = function (b, a) { a.page = Math.min(a.totalPages, a.filteredPages) - 1; q(b, a) }, I = function (b, a) { a.page++; a.page >= Math.min(a.totalPages, a.filteredPages) - 1 && (a.page = Math.min(a.totalPages, a.filteredPages) - 1); q(b, a) }, J = function (b, a) { a.page--; 0 >= a.page && (a.page = 0); q(b, a) }, E = function (b, a, c) { var e, f = b.config; a.$size.add(a.$goto).removeClass(a.cssDisabled).removeAttr("disabled").attr("aria-disabled", "false"); a.isDisabled = !1; a.page = h.data(b, "pagerLastPage") || a.page || 0; a.size = h.data(b, "pagerLastSize") || parseInt(a.$size.find("option[selected]").val(), 10) || a.size || 10; a.$size.val(a.size); a.totalPages = Math.ceil(Math.min(a.totalRows, a.filteredRows) / a.size); b.id && (e = b.id + "_pager_info", a.$container.find(a.cssPageDisplay).attr("id", e), f.$table.attr("aria-describedby", e)); c && (f.$table.trigger("updateRows"), y(b, a.size, a), A(b, a), u(b, a), f.debug && k.log("pager enabled")) }; w.appender = function (b, a) { var c = b.config, e = c.pager; e.ajax || (c.rowsCopy = a, e.totalRows = e.countChildRows ? c.$tbodies.eq(0).children("tr").length : a.length, e.size = h.data(b, "pagerLastSize") || e.size || 10, e.totalPages = Math.ceil(e.totalRows / e.size), x(b, a, e), t(b, e, !1)) }; w.construct = function (b) { return this.each(function () { if (this.config && this.hasInitialized) { var a, c, e, f = this, g = f.config, l = g.widgetOptions, d = g.pager = h.extend(!0, {}, h.tablesorterPager.defaults, b), m = g.$table, n = d.$container = h(d.container).addClass("tablesorter-pager").show(); g.debug && k.log("Pager initializing"); d.oldAjaxSuccess = d.oldAjaxSuccess || d.ajaxObject.success; g.appender = w.appender; k.filter && 0 <= h.inArray("filter", g.widgets) && (d.currentFilters = g.$table.data("lastSearch") || k.filter.setDefaults(f, g, g.widgetOptions) || [], k.setFilters(f, d.currentFilters, !1)); d.savePages && k.storage && (a = k.storage(f, d.storageKey) || {}, d.page = isNaN(a.page) ? d.page : a.page, d.size = (isNaN(a.size) ? d.size : a.size) || 10, h.data(f, "pagerLastSize", d.size)); d.regexRows = new RegExp("(" + (l.filter_filteredRow || "filtered") + "|" + g.selectorRemove.substring(1) + "|" + g.cssChildRow + ")"); m.unbind("filterStart filterEnd sortEnd disable enable destroy update updateRows updateAll addRows pageSize ".split(" ").join(".pager ")).bind("filterStart.pager", function (a, b) { d.currentFilters = b; !1 !== d.pageReset && (g.lastCombinedFilter || "") !== (b || []).join("") && (d.page = d.pageReset) }).bind("filterEnd.pager sortEnd.pager", function () { d.initialized && (t(f, d, !1), q(f, d, !1), u(f, d)) }).bind("disable.pager", function (a) { a.stopPropagation(); D(f, d) }).bind("enable.pager", function (a) { a.stopPropagation(); E(f, d, !0) }).bind("destroy.pager", function (a) { a.stopPropagation(); D(f, d); d.$container.hide(); f.config.appender = null; d.initialized = !1; delete f.config.rowsCopy; h(f).unbind("destroy.pager sortEnd.pager filterEnd.pager enable.pager disable.pager"); k.storage && k.storage(f, d.storageKey, "") }).bind("update.pager updateRows.pager updateAll.pager addRows.pager ", function (a) { a.stopPropagation(); u(f, d); a = g.$tbodies.eq(0).children("tr"); d.totalRows = a.length - (d.countChildRows ? 0 : a.filter("." + g.cssChildRow).length); d.totalPages = Math.ceil(d.totalRows / d.size); t(f, d); v(f, d) }).bind("pageSize.pager", function (a, b) { a.stopPropagation(); y(f, parseInt(b, 10) || 10, d); v(f, d); t(f, d, !1); d.$size.length && d.$size.val(d.size) }).bind("pageSet.pager", function (a, b) { a.stopPropagation(); d.page = (parseInt(b, 10) || 1) - 1; d.$goto.length && d.$goto.val(d.size); q(f, d); t(f, d, !1) }); c = [d.cssFirst, d.cssPrev, d.cssNext, d.cssLast]; e = [H, J, I, C]; n.find(c.join(",")).attr("tabindex", 0).unbind("click.pager").bind("click.pager", function (a) { a.stopPropagation(); var b = h(this), g = c.length; if (!b.hasClass(d.cssDisabled)) for (a = 0; a < g; a++) if (b.is(c[a])) { e[a](f, d); break } }); d.$goto = n.find(d.cssGoto); d.$goto.length && d.$goto.unbind("change").bind("change", function () { d.page = h(this).val() - 1; q(f, d); t(f, d, !1) }); d.$size = n.find(d.cssPageSize); d.$size.length && d.$size.unbind("change.pager").bind("change.pager", function () { d.$size.val(h(this).val()); h(this).hasClass(d.cssDisabled) || (y(f, parseInt(h(this).val(), 10), d), z(f, d)); return !1 }); d.initialized = !1; m.trigger("pagerBeforeInitialized", d); E(f, d, !1); "string" === typeof d.ajaxUrl ? (d.ajax = !0, g.widgetOptions.filter_serversideFiltering = !0, g.serverSideSorting = !0, q(f, d)) : (d.ajax = !1, h(this).trigger("appendCache", !0), A(f, d)); z(f, d); d.ajax || (d.initialized = !0, h(f).trigger("pagerInitialized", d)) } }) } } }); k.showError = function (k, p) { h(k).each(function () { var k = this.config, u = k.pager && k.pager.cssErrorRow || k.widgetOptions.pager_css && k.widgetOptions.pager_css.errorRow || "tablesorter-errorRow"; k && ("undefined" === typeof p ? k.$table.find("thead").find(k.selectorRemove).remove() : (/tr\>/.test(p) ? h(p) : h('<tr><td colspan="' + k.columns + '">' + p + "</td></tr>")).click(function () { h(this).remove() }).appendTo(k.$table.find("thead:first")).addClass(u + " " + k.selectorRemove.replace(/^[.#]/, "")).attr({ role: "alert", "aria-live": "assertive" })) }) }; h.fn.extend({ tablesorterPager: h.tablesorterPager.construct }) })(jQuery);

/*! tableSorter 2.16+ widgets - updated 6/18/2014 (v2.17.2) */
; (function (k) {
    var d = k.tablesorter = k.tablesorter || {};
    d.themes = { bootstrap: { table: "table table-bordered table-striped", caption: "caption", header: "bootstrap-header", footerRow: "", footerCells: "", icons: "", sortNone: "bootstrap-icon-unsorted", sortAsc: "icon-chevron-up glyphicon glyphicon-chevron-up", sortDesc: "icon-chevron-down glyphicon glyphicon-chevron-down", active: "", hover: "", filterRow: "", even: "", odd: "" }, jui: { table: "ui-widget ui-widget-content ui-corner-all", caption: "ui-widget-content ui-corner-all", header: "ui-widget-header ui-corner-all ui-state-default", footerRow: "", footerCells: "", icons: "ui-icon", sortNone: "ui-icon-carat-2-n-s", sortAsc: "ui-icon-carat-1-n", sortDesc: "ui-icon-carat-1-s", active: "ui-state-active", hover: "ui-state-hover", filterRow: "", even: "ui-widget-content", odd: "ui-state-default"} }; k.extend(d.css, { filterRow: "tablesorter-filter-row", filter: "tablesorter-filter", wrapper: "tablesorter-wrapper", resizer: "tablesorter-resizer", grip: "tablesorter-resizer-grip", sticky: "tablesorter-stickyHeader", stickyVis: "tablesorter-sticky-visible" });
    d.storage = function (b, a, d, c) { b = k(b)[0]; var f, g, h = !1; f = {}; g = b.config; var l = k(b); b = c && c.id || l.attr(c && c.group || "data-table-group") || b.id || k(".tablesorter").index(l); c = c && c.url || l.attr(c && c.page || "data-table-page") || g && g.fixedUrl || window.location.pathname; if ("localStorage" in window) try { window.localStorage.setItem("_tmptest", "temp"), h = !0, window.localStorage.removeItem("_tmptest") } catch (n) { } k.parseJSON && (h ? f = k.parseJSON(localStorage[a] || "{}") : (g = document.cookie.split(/[;\s|=]/), f = k.inArray(a, g) + 1, f = 0 !== f ? k.parseJSON(g[f] || "{}") : {})); if ((d || "" === d) && window.JSON && JSON.hasOwnProperty("stringify")) f[c] || (f[c] = {}), f[c][b] = d, h ? localStorage[a] = JSON.stringify(f) : (d = new Date, d.setTime(d.getTime() + 31536E6), document.cookie = a + "=" + JSON.stringify(f).replace(/\"/g, '"') + "; expires=" + d.toGMTString() + "; path=/"); else return f && f[c] ? f[c][b] : "" };
    d.addHeaderResizeEvent = function (b, a, d) { var c; d = k.extend({}, { timer: 250 }, d); var f = b.config, g = f.widgetOptions, h = function (a) { g.resize_flag = !0; c = []; f.$headers.each(function () { var a = k(this), b = a.data("savedSizes") || [0, 0], d = this.offsetWidth, e = this.offsetHeight; if (d !== b[0] || e !== b[1]) a.data("savedSizes", [d, e]), c.push(this) }); c.length && !1 !== a && f.$table.trigger("resize", [c]); g.resize_flag = !1 }; h(!1); clearInterval(g.resize_timer); if (a) return g.resize_flag = !1; g.resize_timer = setInterval(function () { g.resize_flag || h() }, d.timer) };
    d.addWidget({ id: "uitheme", priority: 10, format: function (b, a, e) { var c, f, g, h = d.themes; c = a.$table; g = a.$headers; var l = a.theme || "jui", n = h[l] || h.jui, h = n.sortNone + " " + n.sortDesc + " " + n.sortAsc; a.debug && (f = new Date); c.hasClass("tablesorter-" + l) && a.theme !== l && b.hasInitialized || ("" !== n.even && (e.zebra[0] += " " + n.even), "" !== n.odd && (e.zebra[1] += " " + n.odd), c.find("caption").addClass(n.caption), b = c.removeClass("" === a.theme ? "" : "tablesorter-" + a.theme).addClass("tablesorter-" + l + " " + n.table).find("tfoot"), b.length && b.find("tr").addClass(n.footerRow).children("th, td").addClass(n.footerCells), g.addClass(n.header).not(".sorter-false").bind("mouseenter.tsuitheme mouseleave.tsuitheme", function (a) { k(this)["mouseenter" === a.type ? "addClass" : "removeClass"](n.hover) }), g.find("." + d.css.wrapper).length || g.wrapInner('<div class="' + d.css.wrapper + '" style="position:relative;height:100%;width:100%"></div>'), a.cssIcon && g.find("." + d.css.icon).addClass(n.icons), c.hasClass("hasFilters") && g.find("." + d.css.filterRow).addClass(n.filterRow)); for (c = 0; c < a.columns; c++) b = a.$headers.add(a.$extraHeaders).filter('[data-column="' + c + '"]'), e = d.css.icon ? b.find("." + d.css.icon) : b, a.$headers.filter('[data-column="' + c + '"]:last')[0].sortDisabled ? (b.removeClass(h), e.removeClass(h + " " + n.icons)) : (g = b.hasClass(d.css.sortAsc) ? n.sortAsc : b.hasClass(d.css.sortDesc) ? n.sortDesc : b.hasClass(d.css.header) ? n.sortNone : "", b[g === n.sortNone ? "removeClass" : "addClass"](n.active), e.removeClass(h).addClass(g)); a.debug && d.benchmark("Applying " + l + " theme", f) }, remove: function (b, a, e) { b = a.$table; a = a.theme || "jui"; e = d.themes[a] || d.themes.jui; var c = b.children("thead").children(), f = e.sortNone + " " + e.sortDesc + " " + e.sortAsc; b.removeClass("tablesorter-" + a + " " + e.table).find(d.css.header).removeClass(e.header); c.unbind("mouseenter.tsuitheme mouseleave.tsuitheme").removeClass(e.hover + " " + f + " " + e.active).find("." + d.css.filterRow).removeClass(e.filterRow); c.find("." + d.css.icon).removeClass(e.icons) } });
    d.addWidget({ id: "columns", priority: 30, options: { columns: ["primary", "secondary", "tertiary"] }, format: function (b, a, e) { var c, f, g, h, l, n, p, m, s = a.$table, r = a.$tbodies, t = a.sortList, x = t.length, v = e && e.columns || ["primary", "secondary", "tertiary"], w = v.length - 1; p = v.join(" "); a.debug && (c = new Date); for (g = 0; g < r.length; g++) f = d.processTbody(b, r.eq(g), !0), h = f.children("tr"), h.each(function () { l = k(this); if ("none" !== this.style.display && (n = l.children().removeClass(p), t && t[0] && (n.eq(t[0][0]).addClass(v[0]), 1 < x))) for (m = 1; m < x; m++) n.eq(t[m][0]).addClass(v[m] || v[w]) }), d.processTbody(b, f, !1); b = !1 !== e.columns_thead ? ["thead tr"] : []; !1 !== e.columns_tfoot && b.push("tfoot tr"); if (b.length && (h = s.find(b.join(",")).children().removeClass(p), x)) for (m = 0; m < x; m++) h.filter('[data-column="' + t[m][0] + '"]').addClass(v[m] || v[w]); a.debug && d.benchmark("Applying Columns widget", c) }, remove: function (b, a, e) { var c = a.$tbodies, f = (e.columns || ["primary", "secondary", "tertiary"]).join(" "); a.$headers.removeClass(f); a.$table.children("tfoot").children("tr").children("th, td").removeClass(f); for (a = 0; a < c.length; a++) e = d.processTbody(b, c.eq(a), !0), e.children("tr").each(function () { k(this).children().removeClass(f) }), d.processTbody(b, e, !1) } });
    d.addWidget({ id: "filter", priority: 50, options: { filter_childRows: !1, filter_columnFilters: !0, filter_cssFilter: "", filter_external: "", filter_filteredRow: "filtered", filter_formatter: null, filter_functions: null, filter_hideEmpty: !0, filter_hideFilters: !1, filter_ignoreCase: !0, filter_liveSearch: !0, filter_onlyAvail: "filter-onlyAvail", filter_placeholder: { search: "", select: "" }, filter_reset: null, filter_saveFilters: !1, filter_searchDelay: 300, filter_selectSource: null, filter_startsWith: !1, filter_useParsedData: !1, filter_serversideFiltering: !1, filter_defaultAttrib: "data-value" }, format: function (b, a, e) { a.$table.hasClass("hasFilters") || d.filter.init(b, a, e) }, remove: function (b, a, e) { var c, f = a.$tbodies; a.$table.removeClass("hasFilters").unbind("addRows updateCell update updateRows updateComplete appendCache filterReset filterEnd search ".split(" ").join(a.namespace + "filter ")).find("." + d.css.filterRow).remove(); for (a = 0; a < f.length; a++) c = d.processTbody(b, f.eq(a), !0), c.children().removeClass(e.filter_filteredRow).show(), d.processTbody(b, c, !1); e.filter_reset && k(document).undelegate(e.filter_reset, "click.tsfilter") } });
    d.filter = { regex: { regex: /^\/((?:\\\/|[^\/])+)\/([mig]{0,3})?$/, child: /tablesorter-childRow/, filtered: /filtered/, type: /undefined|number/, exact: /(^[\"|\'|=]+)|([\"|\'|=]+$)/g, nondigit: /[^\w,. \-()]/g, operators: /[<>=]/g }, types: { regex: function (b, a, e, c) { if (d.filter.regex.regex.test(a)) { var f; b = d.filter.regex.regex.exec(a); try { f = (new RegExp(b[1], b[2])).test(c) } catch (g) { f = !1 } return f } return null }, operators: function (b, a, e, c, f, g, h, l, n) { if (/^[<>]=?/.test(a)) { var p; e = h.config; b = d.formatFloat(a.replace(d.filter.regex.operators, ""), h); l = e.parsers[g]; e = b; if (n[g] || "numeric" === l.type) p = l.format(k.trim("" + a.replace(d.filter.regex.operators, "")), h, [], g), b = "number" !== typeof p || "" === p || isNaN(p) ? b : p; c = !n[g] && "numeric" !== l.type || isNaN(b) || "undefined" === typeof f ? isNaN(c) ? d.formatFloat(c.replace(d.filter.regex.nondigit, ""), h) : d.formatFloat(c, h) : f; />/.test(a) && (p = />=/.test(a) ? c >= b : c > b); /</.test(a) && (p = /<=/.test(a) ? c <= b : c < b); p || "" !== e || (p = !0); return p } return null }, notMatch: function (b, a, e, c, f, g, h, l) { if (/^\!/.test(a)) { a = a.replace("!", ""); if (d.filter.regex.exact.test(a)) return a = a.replace(d.filter.regex.exact, ""), "" === a ? !0 : k.trim(a) !== c; b = c.search(k.trim(a)); return "" === a ? !0 : !(l.filter_startsWith ? 0 === b : 0 <= b) } return null }, exact: function (b, a, e, c, f, g, h, l, n, p) { return d.filter.regex.exact.test(a) ? (b = a.replace(d.filter.regex.exact, ""), p ? 0 <= k.inArray(b, p) : b == c) : null }, and: function (b, a, e, c) { if (d.filter.regex.andTest.test(b)) { b = a.split(d.filter.regex.andSplit); a = 0 <= c.search(k.trim(b[0])); for (e = b.length - 1; a && e; ) a = a && 0 <= c.search(k.trim(b[e])), e--; return a } return null }, range: function (b, a, e, c, f, g, h, l, k) { if (d.filter.regex.toTest.test(a)) { b = h.config; var p = a.split(d.filter.regex.toSplit); e = d.formatFloat(p[0].replace(d.filter.regex.nondigit, ""), h); l = d.formatFloat(p[1].replace(d.filter.regex.nondigit, ""), h); if (k[g] || "numeric" === b.parsers[g].type) a = b.parsers[g].format("" + p[0], h, b.$headers.eq(g), g), e = "" === a || isNaN(a) ? e : a, a = b.parsers[g].format("" + p[1], h, b.$headers.eq(g), g), l = "" === a || isNaN(a) ? l : a; a = !k[g] && "numeric" !== b.parsers[g].type || isNaN(e) || isNaN(l) ? isNaN(c) ? d.formatFloat(c.replace(d.filter.regex.nondigit, ""), h) : d.formatFloat(c, h) : f; e > l && (c = e, e = l, l = c); return a >= e && a <= l || "" === e || "" === l } return null }, wild: function (b, a, e, c, f, g, h, l, n, p) { return /[\?|\*]/.test(a) || d.filter.regex.orReplace.test(b) ? (b = h.config, a = a.replace(d.filter.regex.orReplace, "|"), !b.$headers.filter('[data-column="' + g + '"]:last').hasClass("filter-match") && /\|/.test(a) && (a = k.isArray(p) ? "(" + a + ")" : "^(" + a + ")$"), (new RegExp(a.replace(/\?/g, "\\S{1}").replace(/\*/g, "\\S*"))).test(c)) : null }, fuzzy: function (b, a, d, c) { if (/^~/.test(a)) { b = 0; d = c.length; var f = a.slice(1); for (a = 0; a < d; a++) c[a] === f[b] && (b += 1); return b === f.length ? !0 : !1 } return null } }, init: function (b, a, e) { d.language = k.extend(!0, {}, { to: "to", or: "or", and: "and" }, d.language); var c, f, g, h, l, n, p; c = d.filter.regex; a.debug && (n = new Date); a.$table.addClass("hasFilters"); k.extend(c, { child: new RegExp(a.cssChildRow), filtered: new RegExp(e.filter_filteredRow), alreadyFiltered: new RegExp("(\\s+(" + d.language.or + "|-|" + d.language.to + ")\\s+)", "i"), toTest: new RegExp("\\s+(-|" + d.language.to + ")\\s+", "i"), toSplit: new RegExp("(?:\\s+(?:-|" + d.language.to + ")\\s+)", "gi"), andTest: new RegExp("\\s+(" + d.language.and + "|&&)\\s+", "i"), andSplit: new RegExp("(?:\\s+(?:" + d.language.and + "|&&)\\s+)", "gi"), orReplace: new RegExp("\\s+(" + d.language.or + ")\\s+", "gi") }); !1 !== e.filter_columnFilters && a.$headers.filter(".filter-false").length !== a.$headers.length && d.filter.buildRow(b, a, e); a.$table.bind("addRows updateCell update updateRows updateComplete appendCache filterReset filterEnd search ".split(" ").join(a.namespace + "filter "), function (c, f) { a.$table.find("." + d.css.filterRow).toggle(!(e.filter_hideEmpty && k.isEmptyObject(a.cache))); /(search|filter)/.test(c.type) || (c.stopPropagation(), d.filter.buildDefault(b, !0)); "filterReset" === c.type ? d.filter.searching(b, []) : "filterEnd" === c.type ? d.filter.buildDefault(b, !0) : (f = "search" === c.type ? f : "updateComplete" === c.type ? a.$table.data("lastSearch") : "", /(update|add)/.test(c.type) && "updateComplete" !== c.type && (a.lastCombinedFilter = null, a.lastSearch = []), d.filter.searching(b, f, !0)); return !1 }); e.filter_reset && (e.filter_reset instanceof k ? e.filter_reset.click(function () { a.$table.trigger("filterReset") }) : k(e.filter_reset).length && k(document).undelegate(e.filter_reset, "click.tsfilter").delegate(e.filter_reset, "click.tsfilter", function () { a.$table.trigger("filterReset") })); if (e.filter_functions) for (h = 0; h < a.columns; h++) if (p = d.getColumnData(b, e.filter_functions, h)) if (g = a.$headers.filter('[data-column="' + h + '"]:last'), c = "", !0 === p && !g.hasClass("filter-false")) d.filter.buildSelect(b, h); else if ("object" === typeof p && !g.hasClass("filter-false")) { for (f in p) "string" === typeof f && (c += "" === c ? '<option value="">' + (g.data("placeholder") || g.attr("data-placeholder") || e.filter_placeholder.select || "") + "</option>" : "", c += '<option value="' + f + '">' + f + "</option>"); a.$table.find("thead").find("select." + d.css.filter + '[data-column="' + h + '"]').append(c) } d.filter.buildDefault(b, !0); d.filter.bindSearch(b, a.$table.find("." + d.css.filter), !0); e.filter_external && d.filter.bindSearch(b, e.filter_external); e.filter_hideFilters && d.filter.hideFilters(b, a); a.showProcessing && a.$table.bind("filterStart" + a.namespace + "filter filterEnd" + a.namespace + "filter", function (c, e) { g = e ? a.$table.find("." + d.css.header).filter("[data-column]").filter(function () { return "" !== e[k(this).data("column")] }) : ""; d.isProcessing(b, "filterStart" === c.type, e ? g : "") }); a.debug && d.benchmark("Applying Filter widget", n); a.$table.bind("tablesorter-initialized pagerInitialized", function () { l = d.filter.setDefaults(b, a, e) || []; l.length && d.setFilters(b, l, !0); a.$table.trigger("filterFomatterUpdate"); e.filter_initialized || (e.filter_initialized = !0, a.$table.trigger("filterInit")) }) }, setDefaults: function (b, a, e) { var c, f = d.getFilters(b) || []; e.filter_saveFilters && d.storage && (c = d.storage(b, "tablesorter-filters") || [], (b = k.isArray(c)) && "" === c.join("") || !b || (f = c)); if ("" === f.join("")) for (b = 0; b < a.columns; b++) f[b] = a.$headers.filter('[data-column="' + b + '"]:last').attr(e.filter_defaultAttrib) || f[b]; a.$table.data("lastSearch", f); return f }, buildRow: function (b, a, e) { var c, f, g, h, l = a.columns; g = '<tr class="' + d.css.filterRow + '">'; for (c = 0; c < l; c++) g += "<td></td>"; a.$filters = k(g + "</tr>").appendTo(a.$table.children("thead").eq(0)).find("td"); for (c = 0; c < l; c++) f = a.$headers.filter('[data-column="' + c + '"]:last'), g = d.getColumnData(b, e.filter_functions, c), g = e.filter_functions && g && "function" !== typeof g || f.hasClass("filter-select"), h = "false" === d.getData(f[0], d.getColumnData(b, a.headers, c), "filter"), g ? g = k("<select>").appendTo(a.$filters.eq(c)) : ((g = d.getColumnData(b, e.filter_formatter, c)) ? ((g = g(a.$filters.eq(c), c)) && 0 === g.length && (g = a.$filters.eq(c).children("input")), g && (0 === g.parent().length || g.parent().length && g.parent()[0] !== a.$filters[c]) && a.$filters.eq(c).append(g)) : g = k('<input type="search">').appendTo(a.$filters.eq(c)), g && g.attr("placeholder", f.data("placeholder") || f.attr("data-placeholder") || e.filter_placeholder.search || "")), g && (f = (k.isArray(e.filter_cssFilter) ? "undefined" !== typeof e.filter_cssFilter[c] ? e.filter_cssFilter[c] || "" : "" : e.filter_cssFilter) || "", g.addClass(d.css.filter + " " + f).attr("data-column", c), h && (g.attr("placeholder", "").addClass("disabled")[0].disabled = !0)) }, bindSearch: function (b, a, e) { b = k(b)[0]; a = k(a); if (a.length) { var c = b.config, f = c.widgetOptions, g = f.filter_$externalFilters; !0 !== e && (f.filter_$anyMatch = a.filter('[data-column="all"]'), f.filter_$externalFilters = g && g.length ? f.filter_$externalFilters.add(a) : a, d.setFilters(b, c.$table.data("lastSearch") || [], !1 === e)); a.attr("data-lastSearchTime", (new Date).getTime()).unbind(["keypress", "keyup", "search", "change", ""].join(c.namespace + "filter ")).bind(["keyup", "search", "change", ""].join(c.namespace + "filter "), function (a) { k(this).attr("data-lastSearchTime", (new Date).getTime()); if (27 === a.which) this.value = ""; else if ("number" === typeof f.filter_liveSearch && this.value.length < f.filter_liveSearch && "" !== this.value || "keyup" === a.type && (32 > a.which && 8 !== a.which && !0 === f.filter_liveSearch && 13 !== a.which || 37 <= a.which && 40 >= a.which || 13 !== a.which && !1 === f.filter_liveSearch)) return; d.filter.searching(b, "change" !== a.type, !0) }).bind("keypress." + c.namespace + "filter", function (a) { 13 === a.which && (a.preventDefault(), k(this).blur()) }); c.$table.bind("filterReset", function () { a.val("") }) } }, checkFilters: function (b, a, e) { var c = b.config, f = c.widgetOptions, g = k.isArray(a), h = g ? a : d.getFilters(b, !0), l = (h || []).join(""); if (!k.isEmptyObject(c.cache) && (g && (d.setFilters(b, h, !1, !0 !== e), f.filter_initialized || (c.lastCombinedFilter = "")), f.filter_hideFilters && c.$table.find("." + d.css.filterRow).trigger("" === l ? "mouseleave" : "mouseenter"), c.lastCombinedFilter !== l || !1 === a)) if (!1 === a && (c.lastCombinedFilter = null, c.lastSearch = []), f.filter_initialized && c.$table.trigger("filterStart", [h]), c.showProcessing) setTimeout(function () { d.filter.findRows(b, h, l); return !1 }, 30); else return d.filter.findRows(b, h, l), !1 }, hideFilters: function (b, a) { var e, c, f; k(b).find("." + d.css.filterRow).addClass("hideme").bind("mouseenter mouseleave", function (b) { e = k(this); clearTimeout(f); f = setTimeout(function () { /enter|over/.test(b.type) ? e.removeClass("hideme") : k(document.activeElement).closest("tr")[0] !== e[0] && "" === a.lastCombinedFilter && e.addClass("hideme") }, 200) }).find("input, select").bind("focus blur", function (b) { c = k(this).closest("tr"); clearTimeout(f); f = setTimeout(function () { if ("" === d.getFilters(a.$table).join("")) c["focus" === b.type ? "removeClass" : "addClass"]("hideme") }, 200) }) }, findRows: function (b, a, e) { if (b.config.lastCombinedFilter !== e) { var c, f, g, h, l, n, p, m, s, r, t, x, v, w, z, y, A, B, L, C, G, H, I, J, M, D, E = d.filter.regex, q = b.config, u = q.widgetOptions, N = q.columns, K = q.$table.children("tbody"), O = ["range", "notMatch", "operators"], F = q.$headers.map(function (a) { return q.parsers && q.parsers[a] && q.parsers[a].parsed || d.getData && "parsed" === d.getData(q.$headers.filter('[data-column="' + a + '"]:last'), d.getColumnData(b, q.headers, a), "filter") || k(this).hasClass("filter-parsed") }).get(); q.debug && (L = new Date); for (l = 0; l < K.length; l++) if (!K.eq(l).hasClass(q.cssInfoBlock || d.css.info)) { n = d.processTbody(b, K.eq(l), !0); m = q.columns; g = k(k.map(q.cache[l].normalized, function (a) { return a[m].$row.get() })); if ("" === e || u.filter_serversideFiltering) g.removeClass(u.filter_filteredRow).not("." + q.cssChildRow).show(); else { g = g.not("." + q.cssChildRow); f = g.length; y = !0; p = q.lastSearch || q.$table.data("lastSearch") || []; for (r = 0; r < m; r++) s = a[r] || "", y || (r = m), y = y && p.length && 0 === s.indexOf(p[r] || "") && !E.alreadyFiltered.test(s) && !/[=\"\|!]/.test(s) && !(/(>=?\s*-\d)/.test(s) || /(<=?\s*\d)/.test(s)) && !("" !== s && q.$filters && q.$filters.eq(r).find("select").length && !q.$headers.filter('[data-column="' + r + '"]:last').hasClass("filter-match")); p = g.not("." + u.filter_filteredRow).length; y && 0 === p && (y = !1); q.debug && d.log("Searching through " + (y && p < f ? p : "all") + " rows"); if (u.filter_$anyMatch && u.filter_$anyMatch.length || a[q.columns]) C = u.filter_$anyMatch && u.filter_$anyMatch.val() || a[q.columns] || "", q.sortLocaleCompare && (C = d.replaceAccents(C)), G = C.toLowerCase(); for (h = 0; h < f; h++) if (s = g[h].className, !(E.child.test(s) || y && E.filtered.test(s))) { B = !0; s = g.eq(h).nextUntil("tr:not(." + q.cssChildRow + ")"); r = s.length && u.filter_childRows ? s.text() : ""; r = u.filter_ignoreCase ? r.toLocaleLowerCase() : r; p = g.eq(h).children(); C && (H = p.map(function (a) { F[a] ? a = q.cache[l].normalized[h][a] : (a = u.filter_ignoreCase ? k(this).text().toLowerCase() : k(this).text(), q.sortLocaleCompare && (a = d.replaceAccents(a))); return a }).get(), I = H.join(" "), J = I.toLowerCase(), M = q.cache[l].normalized[h].slice(0, -1).join(" "), A = null, k.each(d.filter.types, function (a, c) { if (0 > k.inArray(a, O) && (w = c(C, G, I, J, M, N, b, u, F, H), null !== w)) return A = w, !1 }), B = null !== A ? A : 0 <= (J + r).indexOf(G)); for (m = 0; m < N; m++) a[m] && (c = q.cache[l].normalized[h][m], u.filter_useParsedData || F[m] ? t = c : (t = k.trim(p.eq(m).text()), t = q.sortLocaleCompare ? d.replaceAccents(t) : t), x = !E.type.test(typeof t) && u.filter_ignoreCase ? t.toLocaleLowerCase() : t, z = B, a[m] = q.sortLocaleCompare ? d.replaceAccents(a[m]) : a[m], v = u.filter_ignoreCase ? (a[m] || "").toLocaleLowerCase() : a[m], (D = d.getColumnData(b, u.filter_functions, m)) ? !0 === D ? z = q.$headers.filter('[data-column="' + m + '"]:last').hasClass("filter-match") ? 0 <= x.search(v) : a[m] === t : "function" === typeof D ? z = D(t, c, a[m], m, g.eq(h)) : "function" === typeof D[a[m]] && (z = D[a[m]](t, c, a[m], m, g.eq(h))) : (A = null, k.each(d.filter.types, function (d, e) { w = e(a[m], v, t, x, c, m, b, u, F); if (null !== w) return A = w, !1 }), null !== A ? z = A : (t = (x + r).indexOf(v), z = !u.filter_startsWith && 0 <= t || u.filter_startsWith && 0 === t)), B = z ? B : !1); g.eq(h).toggle(B).toggleClass(u.filter_filteredRow, !B); s.length && s.toggleClass(u.filter_filteredRow, !B) } } d.processTbody(b, n, !1) } q.lastCombinedFilter = e; q.lastSearch = a; q.$table.data("lastSearch", a); u.filter_saveFilters && d.storage && d.storage(b, "tablesorter-filters", a); q.debug && d.benchmark("Completed filter widget search", L); u.filter_initialized && q.$table.trigger("filterEnd"); setTimeout(function () { q.$table.trigger("applyWidgets") }, 0) } }, getOptionSource: function (b, a, e) { var c, f = b.config, g = [], h = !1, l = f.widgetOptions.filter_selectSource, n = k.isFunction(l) ? !0 : d.getColumnData(b, l, a); !0 === n ? h = l(b, a, e) : "object" === k.type(l) && n && (h = n(b, a, e)); !1 === h && (h = d.filter.getOptions(b, a, e)); h = k.grep(h, function (a, b) { return k.inArray(a, h) === b }); f.$headers.filter('[data-column="' + a + '"]:last').hasClass("filter-select-nosort") || (k.each(h, function (c, d) { g.push({ t: d, p: f.parsers && f.parsers[a].format(d, b, [], a) }) }), c = f.textSorter || "", g.sort(function (e, f) { var g = e.p.toString(), h = f.p.toString(); return k.isFunction(c) ? c(g, h, !0, a, b) : "object" === typeof c && c.hasOwnProperty(a) ? c[a](g, h, !0, a, b) : d.sortNatural ? d.sortNatural(g, h) : !0 }), h = [], k.each(g, function (a, b) { h.push(b.t) })); return h }, getOptions: function (b, a, d) { var c, f, g, h, l = b.config, n = l.widgetOptions, p = l.$table.children("tbody"), m = []; for (c = 0; c < p.length; c++) if (!p.eq(c).hasClass(l.cssInfoBlock)) for (h = l.cache[c], f = l.cache[c].normalized.length, b = 0; b < f; b++) g = h.row ? h.row[b] : h.normalized[b][l.columns].$row[0], d && g.className.match(n.filter_filteredRow) || (n.filter_useParsedData ? m.push("" + h.normalized[b][a]) : (g = g.cells[a]) && m.push(k.trim(g.textContent || g.innerText || k(g).text()))); return m }, buildSelect: function (b, a, e, c) { if (b.config.cache && !k.isEmptyObject(b.config.cache)) { a = parseInt(a, 10); var f; f = b.config; var g = f.widgetOptions, h = f.$headers.filter('[data-column="' + a + '"]:last'), h = '<option value="">' + (h.data("placeholder") || h.attr("data-placeholder") || g.filter_placeholder.select || "") + "</option>", l = d.filter.getOptionSource(b, a, c), n = f.$table.find("thead").find("select." + d.css.filter + '[data-column="' + a + '"]').val(); for (b = 0; b < l.length; b++) c = l[b].replace(/\"/g, "&quot;"), h += "" !== l[b] ? '<option value="' + c + '"' + (n === c ? ' selected="selected"' : "") + ">" + l[b] + "</option>" : ""; f = (f.$filters ? f.$filters : f.$table.children("thead")).find("." + d.css.filter); g.filter_$externalFilters && (f = f && f.length ? f.add(g.filter_$externalFilters) : g.filter_$externalFilters); f.filter('select[data-column="' + a + '"]')[e ? "html" : "append"](h); g.filter_functions || (g.filter_functions = {}); g.filter_functions[a] = !0 } }, buildDefault: function (b, a) { var e, c, f = b.config, g = f.widgetOptions, h = f.columns; for (e = 0; e < h; e++) c = f.$headers.filter('[data-column="' + e + '"]:last'), !c.hasClass("filter-select") && !0 !== d.getColumnData(b, g.filter_functions, e) || c.hasClass("filter-false") || d.filter.buildSelect(b, e, a, c.hasClass(g.filter_onlyAvail)) }, searching: function (b, a, e) { if ("undefined" === typeof a || !0 === a) { var c = b.config.widgetOptions; clearTimeout(c.searchTimer); c.searchTimer = setTimeout(function () { d.filter.checkFilters(b, a, e) }, c.filter_liveSearch ? c.filter_searchDelay : 10) } else d.filter.checkFilters(b, a, e) } };
    d.getFilters = function (b, a, e, c) { var f, g = !1, h = b ? k(b)[0].config : "", l = h ? h.widgetOptions : ""; if (!0 !== a && l && !l.filter_columnFilters) return k(b).data("lastSearch"); if (h && (h.$filters && (f = h.$filters.find("." + d.css.filter)), l.filter_$externalFilters && (f = f && f.length ? f.add(l.filter_$externalFilters) : l.filter_$externalFilters), f && f.length)) for (g = e || [], b = 0; b < h.columns + 1; b++) a = f.filter('[data-column="' + (b === h.columns ? "all" : b) + '"]'), a.length && (a = a.sort(function (a, b) { return k(b).attr("data-lastSearchTime") - k(a).attr("data-lastSearchTime") }), k.isArray(e) ? (c ? a.slice(1) : a).val(e[b]).trigger("change.tsfilter") : (g[b] = a.val() || "", a.slice(1).val(g[b])), b === h.columns && a.length && (l.filter_$anyMatch = a)); 0 === g.length && (g = !1); return g };
    d.setFilters = function (b, a, e, c) { var f = b ? k(b)[0].config : ""; b = d.getFilters(b, !0, a, c); f && e && (f.lastCombinedFilter = null, f.lastSearch = [], d.filter.searching(f.$table[0], a, c), f.$table.trigger("filterFomatterUpdate")); return !!b };
    d.addWidget({ id: "stickyHeaders", priority: 60, options: { stickyHeaders: "", stickyHeaders_attachTo: null, stickyHeaders_offset: 0, stickyHeaders_filteredToTop: !0, stickyHeaders_cloneId: "-sticky", stickyHeaders_addResizeEvent: !0, stickyHeaders_includeCaption: !0, stickyHeaders_zIndex: 2 }, format: function (b, a, e) { if (!(a.$table.hasClass("hasStickyHeaders") || 0 <= k.inArray("filter", a.widgets) && !a.$table.hasClass("hasFilters"))) { var c = a.$table, f = k(e.stickyHeaders_attachTo), g = c.children("thead:first"), h = f.length ? f : k(window), l = g.children("tr").not(".sticky-false").children(), n = "." + d.css.headerIn, p = c.find("tfoot"), m = isNaN(e.stickyHeaders_offset) ? k(e.stickyHeaders_offset) : "", s = f.length ? 0 : m.length ? m.height() || 0 : parseInt(e.stickyHeaders_offset, 10) || 0, r = e.$sticky = c.clone().addClass("containsStickyHeaders").css({ position: f.length ? "absolute" : "fixed", margin: 0, top: s, left: 0, visibility: "hidden", zIndex: e.stickyHeaders_zIndex ? e.stickyHeaders_zIndex : 2 }), t = r.children("thead:first").addClass(d.css.sticky + " " + e.stickyHeaders), x, v = "", w = 0, z = "collapse" !== c.css("border-collapse") && !/(webkit|msie)/i.test(navigator.userAgent), y = function () { s = m.length ? m.height() || 0 : parseInt(e.stickyHeaders_offset, 10) || 0; w = 0; z && (w = 2 * parseInt(l.eq(0).css("border-left-width"), 10)); r.css({ left: f.length ? (parseInt(f.css("padding-left"), 10) || 0) + parseInt(a.$table.css("padding-left"), 10) + parseInt(a.$table.css("margin-left"), 10) + parseInt(c.css("border-left-width"), 10) : g.offset().left - h.scrollLeft() - w, width: c.width() }); x.filter(":visible").each(function (b) { b = l.filter(":visible").eq(b); var c = z && k(this).attr("data-column") === "" + parseInt(a.columns / 2, 10) ? 1 : 0; k(this).css({ width: b.width() - w }).find(n).width(b.find(n).width() - c) }) }; r.attr("id") && (r[0].id += e.stickyHeaders_cloneId); r.find("thead:gt(0), tr.sticky-false").hide(); r.find("tbody, tfoot").remove(); e.stickyHeaders_includeCaption ? r.find("caption").css("margin-left", "-1px") : r.find("caption").remove(); x = t.children().children(); r.css({ height: 0, width: 0, padding: 0, margin: 0, border: 0 }); x.find("." + d.css.resizer).remove(); c.addClass("hasStickyHeaders").bind("pagerComplete.tsSticky", function () { y() }); d.bindEvents(b, t.children().children(".tablesorter-header")); c.after(r); h.bind("scroll.tsSticky resize.tsSticky", function (a) { if (c.is(":visible")) { var b = c.offset(), d = e.stickyHeaders_includeCaption ? 0 : c.find("caption").outerHeight(!0), d = (f.length ? f.offset().top : h.scrollTop()) + s - d, k = c.height() - (r.height() + (p.height() || 0)), b = d > b.top && d < b.top + k ? "visible" : "hidden", d = { visibility: b }; f.length ? d.top = f.scrollTop() : d.left = g.offset().left - h.scrollLeft() - w; r.removeClass("tablesorter-sticky-visible tablesorter-sticky-hidden").addClass("tablesorter-sticky-" + b).css(d); if (b !== v || "resize" === a.type) y(), v = b } }); e.stickyHeaders_addResizeEvent && d.addHeaderResizeEvent(b); c.hasClass("hasFilters") && (c.bind("filterEnd", function () { var b = k(document.activeElement).closest("td"), b = b.parent().children().index(b); r.hasClass(d.css.stickyVis) && e.stickyHeaders_filteredToTop && (window.scrollTo(0, c.position().top), 0 <= b && a.$filters && a.$filters.eq(b).find("a, select, input").filter(":visible").focus()) }), d.filter.bindSearch(c, x.find("." + d.css.filter)), e.filter_hideFilters && d.filter.hideFilters(r, a)); c.trigger("stickyHeadersInit") } }, remove: function (b, a, e) { a.$table.removeClass("hasStickyHeaders").unbind("pagerComplete.tsSticky").find("." + d.css.sticky).remove(); e.$sticky && e.$sticky.length && e.$sticky.remove(); k(".hasStickyHeaders").length || k(window).unbind("scroll.tsSticky resize.tsSticky"); d.addHeaderResizeEvent(b, !1) } });
    d.addWidget({ id: "resizable", priority: 40, options: { resizable: !0, resizable_addLastColumn: !1, resizable_widths: [] }, format: function (b, a, e) { if (!a.$table.hasClass("hasResizable")) { a.$table.addClass("hasResizable"); d.resizableReset(b, !0); var c, f, g, h, l = {}, n = a.$table, p = 0, m = null, s = null, r = 20 > Math.abs(n.parent().width() - n.width()), t = function () { d.storage && m && s && (l = {}, l[m.index()] = m.width(), l[s.index()] = s.width(), m.width(l[m.index()]), s.width(l[s.index()]), !1 !== e.resizable && d.storage(b, "tablesorter-resizable", a.$headers.map(function () { return k(this).width() }).get())); p = 0; m = s = null; k(window).trigger("resize") }; if (l = d.storage && !1 !== e.resizable ? d.storage(b, "tablesorter-resizable") : {}) for (h in l) !isNaN(h) && h < a.$headers.length && a.$headers.eq(h).width(l[h]); c = n.children("thead:first").children("tr"); c.children().each(function () { var e; e = k(this); h = e.attr("data-column"); e = "false" === d.getData(e, d.getColumnData(b, a.headers, h), "resizable"); c.children().filter('[data-column="' + h + '"]')[e ? "addClass" : "removeClass"]("resizable-false") }); c.each(function () { g = k(this).children().not(".resizable-false"); k(this).find("." + d.css.wrapper).length || g.wrapInner('<div class="' + d.css.wrapper + '" style="position:relative;height:100%;width:100%"></div>'); e.resizable_addLastColumn || (g = g.slice(0, -1)); f = f ? f.add(g) : g }); f.each(function () { var a = k(this), b = parseInt(a.css("padding-right"), 10) + 10; a.find("." + d.css.wrapper).append('<div class="' + d.css.resizer + '" style="cursor:w-resize;position:absolute;z-index:1;right:-' + b + 'px;top:0;height:100%;width:20px;"></div>') }).bind("mousemove.tsresize", function (a) { if (0 !== p && m) { var b = a.pageX - p, c = m.width(); m.width(c + b); m.width() !== c && r && s.width(s.width() - b); p = a.pageX } }).bind("mouseup.tsresize", function () { t() }).find("." + d.css.resizer + ",." + d.css.grip).bind("mousedown", function (b) { m = k(b.target).closest("th"); var c = a.$headers.filter('[data-column="' + m.attr("data-column") + '"]'); 1 < c.length && (m = m.add(c)); s = b.shiftKey ? m.parent().find("th").not(".resizable-false").filter(":last") : m.nextAll(":not(.resizable-false)").eq(0); p = b.pageX }); n.find("thead:first").bind("mouseup.tsresize mouseleave.tsresize", function () { t() }).bind("contextmenu.tsresize", function () { d.resizableReset(b); var a = k.isEmptyObject ? k.isEmptyObject(l) : !0; l = {}; return a }) } }, remove: function (b, a) { a.$table.removeClass("hasResizable").children("thead").unbind("mouseup.tsresize mouseleave.tsresize contextmenu.tsresize").children("tr").children().unbind("mousemove.tsresize mouseup.tsresize").find("." + d.css.resizer + ",." + d.css.grip).remove(); d.resizableReset(b) } });
    d.resizableReset = function (b, a) { k(b).each(function () { var e, c = this.config, f = c && c.widgetOptions; b && c && (c.$headers.each(function (a) { e = k(this); f.resizable_widths[a] ? e.css("width", f.resizable_widths[a]) : e.hasClass("resizable-false") || e.css("width", "") }), d.storage && !a && d.storage(this, "tablesorter-resizable", {})) }) };
    d.addWidget({ id: "saveSort", priority: 20, options: { saveSort: !0 }, init: function (b, a, d, c) { a.format(b, d, c, !0) }, format: function (b, a, e, c) { var f, g = a.$table; e = !1 !== e.saveSort; var h = { sortList: a.sortList }; a.debug && (f = new Date); g.hasClass("hasSaveSort") ? e && b.hasInitialized && d.storage && (d.storage(b, "tablesorter-savesort", h), a.debug && d.benchmark("saveSort widget: Saving last sort: " + a.sortList, f)) : (g.addClass("hasSaveSort"), h = "", d.storage && (h = (e = d.storage(b, "tablesorter-savesort")) && e.hasOwnProperty("sortList") && k.isArray(e.sortList) ? e.sortList : "", a.debug && d.benchmark('saveSort: Last sort loaded: "' + h + '"', f), g.bind("saveSortReset", function (a) { a.stopPropagation(); d.storage(b, "tablesorter-savesort", "") })), c && h && 0 < h.length ? a.sortList = h : b.hasInitialized && h && 0 < h.length && g.trigger("sorton", [h])) }, remove: function (b) { d.storage && d.storage(b, "tablesorter-savesort", "") } })
})(jQuery);



/*
 * Treeview 1.5pre - jQuery plugin to hide and show branches of a tree
 * 
 * http://bassistance.de/jquery-plugins/jquery-plugin-treeview/
 * http://docs.jquery.com/Plugins/Treeview
 *
 * Copyright (c) 2007 J�rn Zaefferer
 *
 * Dual licensed under the MIT and GPL licenses:
 *   http://www.opensource.org/licenses/mit-license.php
 *   http://www.gnu.org/licenses/gpl.html
 *
 * Revision: $Id: jquery.treeview.js 5759 2008-07-01 07:50:28Z joern.zaefferer $
 *
 */

;(function($) {

	// TODO rewrite as a widget, removing all the extra plugins
	$.extend($.fn, {
		swapClass: function(c1, c2) {
			var c1Elements = this.filter('.' + c1);
			this.filter('.' + c2).removeClass(c2).addClass(c1);
			c1Elements.removeClass(c1).addClass(c2);
			return this;
		},
		replaceClass: function(c1, c2) {
			return this.filter('.' + c1).removeClass(c1).addClass(c2).end();
		},
		hoverClass: function(className) {
			className = className || "hover";
			return this.hover(function() {
				$(this).addClass(className);
			}, function() {
				$(this).removeClass(className);
			});
		},
		heightToggle: function(animated, callback) {
			animated ?
				this.animate({ height: "toggle" }, animated, callback) :
				this.each(function(){
					jQuery(this)[ jQuery(this).is(":hidden") ? "show" : "hide" ]();
					if(callback)
						callback.apply(this, arguments);
				});
		},
		heightHide: function(animated, callback) {
			if (animated) {
				this.animate({ height: "hide" }, animated, callback);
			} else {
				this.hide();
				if (callback)
					this.each(callback);				
			}
		},
		prepareBranches: function(settings) {
			if (!settings.prerendered) {
				// mark last tree items
				this.filter(":last-child:not(ul)").addClass(CLASSES.last);
				// collapse whole tree, or only those marked as closed, anyway except those marked as open
				this.filter((settings.collapsed ? "" : "." + CLASSES.closed) + ":not(." + CLASSES.open + ")").find(">ul").hide();
			}
			// return all items with sublists
			return this.filter(":has(>ul)");
		},
		applyClasses: function(settings, toggler) {
			// TODO use event delegation
			this.filter(":has(>ul):not(:has(>a))").find(">span").unbind("click.treeview").bind("click.treeview", function(event) {
				// don't handle click events on children, eg. checkboxes
				if ( this == event.target )
					toggler.apply($(this).next());
			}).add( $("a", this) ).hoverClass();
			
			if (!settings.prerendered) {
				// handle closed ones first
				this.filter(":has(>ul:hidden)")
						.addClass(CLASSES.expandable)
						.replaceClass(CLASSES.last, CLASSES.lastExpandable);
						
				// handle open ones
				this.not(":has(>ul:hidden)")
						.addClass(CLASSES.collapsable)
						.replaceClass(CLASSES.last, CLASSES.lastCollapsable);
						
	            // create hitarea if not present
				var hitarea = this.find("div." + CLASSES.hitarea);
				if (!hitarea.length)
					hitarea = this.prepend("<div class=\"" + CLASSES.hitarea + "\"/>").find("div." + CLASSES.hitarea);
				hitarea.removeClass().addClass(CLASSES.hitarea).each(function() {
					var classes = "";
					$.each($(this).parent().attr("class").split(" "), function() {
						classes += this + "-hitarea ";
					});
					$(this).addClass( classes );
				})
			}
			
			// apply event to hitarea
			this.find("div." + CLASSES.hitarea).click( toggler );
		},
		treeview: function(settings) {
			
			settings = $.extend({
				cookieId: "treeview"
			}, settings);
			
			if ( settings.toggle ) {
				var callback = settings.toggle;
				settings.toggle = function() {
					return callback.apply($(this).parent()[0], arguments);
				};
			}
		
			// factory for treecontroller
			function treeController(tree, control) {
				// factory for click handlers
				function handler(filter) {
					return function() {
						// reuse toggle event handler, applying the elements to toggle
						// start searching for all hitareas
						toggler.apply( $("div." + CLASSES.hitarea, tree).filter(function() {
							// for plain toggle, no filter is provided, otherwise we need to check the parent element
							return filter ? $(this).parent("." + filter).length : true;
						}) );
						return false;
					};
				}
				// click on first element to collapse tree
				$("a:eq(0)", control).click( handler(CLASSES.collapsable) );
				// click on second to expand tree
				$("a:eq(1)", control).click( handler(CLASSES.expandable) );
				// click on third to toggle tree
				$("a:eq(2)", control).click( handler() ); 
			}
		
			// handle toggle event
			function toggler() {
				$(this)
					.parent()
					// swap classes for hitarea
					.find(">.hitarea")
						.swapClass( CLASSES.collapsableHitarea, CLASSES.expandableHitarea )
						.swapClass( CLASSES.lastCollapsableHitarea, CLASSES.lastExpandableHitarea )
					.end()
					// swap classes for parent li
					.swapClass( CLASSES.collapsable, CLASSES.expandable )
					.swapClass( CLASSES.lastCollapsable, CLASSES.lastExpandable )
					// find child lists
					.find( ">ul" )
					// toggle them
					.heightToggle( settings.animated, settings.toggle );
				if ( settings.unique ) {
					$(this).parent()
						.siblings()
						// swap classes for hitarea
						.find(">.hitarea")
							.replaceClass( CLASSES.collapsableHitarea, CLASSES.expandableHitarea )
							.replaceClass( CLASSES.lastCollapsableHitarea, CLASSES.lastExpandableHitarea )
						.end()
						.replaceClass( CLASSES.collapsable, CLASSES.expandable )
						.replaceClass( CLASSES.lastCollapsable, CLASSES.lastExpandable )
						.find( ">ul" )
						.heightHide( settings.animated, settings.toggle );
				}
			}
			this.data("toggler", toggler);
			
			function serialize() {
				function binary(arg) {
					return arg ? 1 : 0;
				}
				var data = [];
				branches.each(function(i, e) {
					data[i] = $(e).is(":has(>ul:visible)") ? 1 : 0;
				});
				$.cookie(settings.cookieId, data.join(""), settings.cookieOptions );
			}
			
			function deserialize() {
				var stored = $.cookie(settings.cookieId);
				if ( stored ) {
					var data = stored.split("");
					branches.each(function(i, e) {
						$(e).find(">ul")[ parseInt(data[i]) ? "show" : "hide" ]();
					});
				}
			}
			
			// add treeview class to activate styles
			this.addClass("treeview");
			
			// prepare branches and find all tree items with child lists
			var branches = this.find("li").prepareBranches(settings);
			
			switch(settings.persist) {
			case "cookie":
				var toggleCallback = settings.toggle;
				settings.toggle = function() {
					serialize();
					if (toggleCallback) {
						toggleCallback.apply(this, arguments);
					}
				};
				deserialize();
				break;
			case "location":
				var current = this.find("a").filter(function() {
					return this.href.toLowerCase() == location.href.toLowerCase();
				});
				if ( current.length ) {
					// TODO update the open/closed classes
					var items = current.addClass("selected").parents("ul, li").add( current.next() ).show();
					if (settings.prerendered) {
						// if prerendered is on, replicate the basic class swapping
						items.filter("li")
							.swapClass( CLASSES.collapsable, CLASSES.expandable )
							.swapClass( CLASSES.lastCollapsable, CLASSES.lastExpandable )
							.find(">.hitarea")
								.swapClass( CLASSES.collapsableHitarea, CLASSES.expandableHitarea )
								.swapClass( CLASSES.lastCollapsableHitarea, CLASSES.lastExpandableHitarea );
					}
				}
				break;
			}
			
			branches.applyClasses(settings, toggler);
				
			// if control option is set, create the treecontroller and show it
			if ( settings.control ) {
				treeController(this, settings.control);
				$(settings.control).show();
			}
			
			return this;
		}
	});
	
	// classes used by the plugin
	// need to be styled via external stylesheet, see first example
	$.treeview = {};
	var CLASSES = ($.treeview.classes = {
		open: "open",
		closed: "closed",
		expandable: "expandable",
		expandableHitarea: "expandable-hitarea",
		lastExpandableHitarea: "lastExpandable-hitarea",
		collapsable: "collapsable",
		collapsableHitarea: "collapsable-hitarea",
		lastCollapsableHitarea: "lastCollapsable-hitarea",
		lastCollapsable: "lastCollapsable",
		lastExpandable: "lastExpandable",
		last: "last",
		hitarea: "hitarea"
	});
	
})(jQuery);



/*
*    *********   Ocultar/Mostrar Lista de Mensajeros    *********
*/
function ocultarMostrarListaMensajeros() {
    if ($('#EmpresaID').val() == 1) {//Mensajer�a Interna
        //alert('selecciono Mensajeria interna');
        $('.ListaMensajeros').show();
    } else {
        $('.ListaMensajeros').hide();
    }
}

jQuery(function ($) {

    ocultarMostrarListaMensajeros();

    $('#EmpresaID').change(function () {
        ocultarMostrarListaMensajeros();
    });

});

/* Opciones del Claendario*/
var calendarOptions = {
    dateFormat: "dd/mm/yy",
    dayNamesMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"],
    monthNames: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"]
};


// isNumberKey solo permite numeros en los input que le hacen referencia

//function isNumber(evt) {

//    var charCode = (evt.which) ? evt.which : event.keyCode;
//    alert(charCode.toString());
//    if (charCode > 47 && charCode < 58) {
//        //alert("aaaaa");
//        return true;
//    }
    
//    return false;
//}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if (charCode == 8) return true;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

        return true;
}