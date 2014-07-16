
//funcion que crea el menu recibiendo un arreglo con los datos del menu
function CreaMenu(Data) {
    var $menu;
    var $menuH;
    var $menu2;
    var nom_menu;
    var nom_menuH;
    var _Lista_Huerfanos = new Array();
    jQuery.each(Data, function () {

        var cod = this[0];
        var nombre = this[1];
        var desc = this[2];
        var url = this[3];
        var padre = this[4];

        if (padre == 0) {
            $('#ul-menu').append('<li id="menu' + cod + '"  class="_barraMenu" title="' + desc + '"><a href="' + url + '" >' + nombre + '</a></li> ');
           
        }
        else {
            $menu = $('#ul-menu').find('#menu' + padre + '');
            nom_menu = $menu.text();

            if ($menu.length > 0) {
                //esta el padre
                $menuH = $('#ul-menu').find('#menuU' + padre + '');
                if ($menuH.length ==0) {//no hay UL
                    $menu.append('<ul id="menuU' + padre + '"></ul>');
                    $('#menuU' + padre + '').append('<li id="menu' + cod + '" title="' + desc + '"><a href="' + url + '" >' + nombre + '</a></li> ');
                    
                }
                else {


                    $menuH.append('<li id="menu' + cod + '"title="' + desc + '" ><a href="' + url + '" >' + nombre + '</a></li> ');
                }
                //$('#menu' + padre + '').append('<li ><a href="' + url + '" >' + nombre + '</a><ul id="menu' + cod + '"></ul></li> ');

            } //falta hacer algo si el papa no esta
            else {
                var _datos = cod + "/" + padre;
                _Lista_Huerfanos.push(_datos);
                $('#ul-menu').append('<li id="menu' + cod + '" title="' + desc + '"><a href="' + url + '" >' + nombre + '</a></li> ');
                
            }
        }

    });
    var _largo = _Lista_Huerfanos.length;
    for (a = 0; a < _largo; a++) {

        var _d = _Lista_Huerfanos[a].split("/");
        $menu = $('#ul-menu').find('#menu' + _d[1] + '');
        if ($menu.length > 0) {//papa esta
            $menuH = $('#ul-menu').find('#menuU' + _d[1] + '');
            //alert(nom_menu+ _d[1] + "-"+ nom_menuH+_d[]);
            $menu2 = $('#ul-menu').find('#menu' + _d[0] + '');
            if ($menuH.length == 0) {
                
                $menu.append('<ul id="menuU' + _d[1] + '"></ul>');
                $('#menuU' + _d[1] + '').append($menu2);
            }
            else {
                $menuH.append($menu2);
            }
            $('#ul-menu').remove('#menu' + _d[0] + '');
        }
    }

}

//funcion que activa las animaciones, agrega css de los menus
(function($) {
    var defaults = {
        vertical: false,
        menuItemSelector: 'li',
        menuGroupSelector: 'ul',
        rootClass: 'menu',
        menuItemClass: 'menu-item ui-corner-all ui-state-default',
        menuGroupClass: 'menu-group',
        verticalClass: 'menu-vertical',
        horizontalClass: 'menu-horizontal',
        hasVerticalClass: 'menu-has-vertical',
        hashorizontalClass: 'menu-has-horizontal',
        hoverClass: 'menu-hover ui-state-hover ui-corner-all',
        showDuration: 350,
        hideDuration: 100
    }
	function menu() {
		var option = (typeof(arguments[0])!='string') ? $.extend(defaults,arguments[0]) : $.extend(defaults,{});
		var $menu = $(this).addClass(option.rootClass+' '+option.menuGroupClass).addClass((option.vertical) ? option.verticalClass : option.horizontalClass);
		var $menuItems = $menu.find(option.menuItemSelector).addClass(option.menuItemClass);
		var $menuGroups = $menu.find(option.menuGroupSelector).addClass(option.menuGroupClass);
		
		$menuItems.hover(
			function(e) {
				$(this).addClass(option.hoverClass);
			},
			function(e) {
				$(this).removeClass(option.hoverClass);
			}
		);
        
		$menuGroups.parent().each(function(index){
			var $parentMenuItem = $(this); // menu item that has menu group
			var displayDirection = ($parentMenuItem.parent().hasClass(option.horizontalClass)) ? 'bottom' : 'right';
			$parentMenuItem.addClass((displayDirection=='bottom') ? option.hasVerticalClass : option.hashorizontalClass);
			var $menuGroup = $parentMenuItem.find(option.menuGroupSelector+':first').addClass(option.verticalClass);
			$parentMenuItem.hover(
				function(e) {
					var offset = (displayDirection=='bottom') ? {left:'0',top:''} : {left:$(this).width()+'px',top:'0'};
					$menuGroup.css({left:offset.left,top:offset.top}).fadeIn(option.showDuration);
				},
				function(e) {
					$menuGroup.fadeOut(option.hideDuration);
				}
			);
		});
        
		$menu.find('a[href^="#"]').click(function() {
			$menuGroups.fadeOut(option.hideDuration);
			return ($(this).attr('href') != '#');
		})
		return this;
	}
	$.fn.extend({
		ptMenu:menu
	});
})(jQuery);


(function ($) {
    $.widget("ui.combobox", {
        _create: function () {
            var self = this,
					select = this.element.hide(),
					selected = select.children(":selected"),
					value = selected.val() ? selected.text() : "";
            var input = this.input = $("<input id='input_"+this.element.attr('id')+"'>")
					.insertAfter(select)
					.val(value)
					.autocomplete({
					    delay: 0,
					    minLength: 0,
					    source: function (request, response) {
					        var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
					        response(select.children("option").map(function () {
					            var text = $(this).text();
					            if (this.value && (!request.term || matcher.test(text)))
					                return {
					                    label: text.replace(
											new RegExp(
												"(?![^&;]+;)(?!<[^<>]*)(" +
												$.ui.autocomplete.escapeRegex(request.term) +
												")(?![^<>]*>)(?![^&;]+;)", "gi"
											), "<strong>$1</strong>"),
					                    value: text,
					                    option: this
					                };
					        }));
					    },
					    select: function (event, ui) {
					        ui.item.option.selected = true;
					        self._trigger("selected", event, {
					            item: ui.item.option
					        });
                                                                
                            select.trigger("change");
                            
					    },
					    change: function (event, ui) {
					        //input.val($(this).val());
                            if (!ui.item) {
					            var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex($(this).val()) + "$", "i"),
									valid = false;
					            select.children("option").each(function () {
					                if ($(this).text().match(matcher)) {    
                                    
					                    this.selected = valid = true;
					                    return false;
					                }
					            });
					            if (!valid) {
					                // remove invalid value, as it didn't match anything
					                $(this).val("");
					                select.val("");
					                input.data("autocomplete").term = "";
					                return false;
					            }
					        }
					    }
					})
					.addClass("ui-widget ui-widget-content ui-corner-left");

            input.data("autocomplete")._renderItem = function (ul, item) {
                return $("<li></li>")
						.data("item.autocomplete", item)
						.append("<a>" + item.label + "</a>")
						.appendTo(ul);
            };

            this.button = $("<button type='button'>&nbsp;</button>")
					.attr("tabIndex", -1)
					.attr("title", "Show All Items")
					.insertAfter(input)
					.button({
					    icons: {
					        primary: "ui-icon-triangle-1-s"
					    },
					    text: false
					})
					.removeClass("ui-corner-all")
					.addClass("ui-corner-right ui-button-icon")
					.click(function () {
					    // close if already visible
					    if (input.autocomplete("widget").is(":visible")) {
					        input.autocomplete("close");
					        return;
					    }

					    // work around a bug (likely same cause as #5265)
					    $(this).blur();

					    // pass empty string as value to search for, displaying all results
					    input.autocomplete("search", "");
					    input.focus();
					});
        },

        destroy: function () {
            this.input.remove();
            this.button.remove();
            this.element.show();
            $.Widget.prototype.destroy.call(this);
        }
    });
})(jQuery);