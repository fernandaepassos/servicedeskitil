//
//		ASP EasyMenu
//	
//		Copyright obout inc      http://www.obout.com

// script version
ob_em_js_version = "509";
// array containing the functions to run upon window.onload
ob_em_gSafeOnLoad = new Array();
// specifies that the function for window.onload has been added to the array
ob_em_loadEventAdded = false;

function ob_EasyMenu(ob_em_menu_id, ob_em_oat_id, ob_em_ui, ob_em_hz_o, ob_em_vt_o, ob_em_mc, ob_em_ds, ob_em_ds_mc, ob_em_ds_ec, ob_em_se)
{	
	// object id
	this.id = 'ob_em_' + ob_em_menu_id;
	// menu id
	this.menu_id = ob_em_menu_id;
	// items
	this.items = new Array();
	// submenus
	this.subMenus = new Array();
	// parentmenu
	this.parentMenu = null;
	// Object to Attach To array
	this.oat_id = ob_em_oat_id;
	this.oat_ids = new Array();
	// Uses Icons
	this.ui = eval(ob_em_ui.toLowerCase());

	// menu classes
	this.classes = ob_em_mc; ob_em_getDefaultClasses(this);

	// horizontal and vertical offset to mouse pointer
	this.hz_o = ob_em_hz_o;
	this.vt_o = ob_em_vt_o;
	// display style
	this.displayPosition = ob_em_ds.toLowerCase();
	this.menuCorner = ob_em_arrangeAlignProperty(ob_em_ds_mc);
	this.elementCorner = ob_em_arrangeAlignProperty(ob_em_ds_ec);
	// show event
	this.showEvent = ob_em_se.toLowerCase();

	// element over which the right click occured
	this.el = null;
	// menu
	this.menu = document.getElementById(this.menu_id);
	this.underlay = this.menu.parentNode; if (this.underlay.tagName.toLowerCase() != "div") return;
	this.menu.menuObject = this;
	// document body
	this.body = document.getElementsByTagName('body')[0];
	// the iframe that will help display the menu
	this.cover_iframe = document.getElementById('obout_easymenu_iframe_' + ob_em_menu_id);

	// additional properties
	this.showCoords = null;

	// the right click event handler
	this.documentMouseDown = ob_em_documentMouseDown;
	// the mouseOver, mouseOut handler
	this.mouseHover = ob_em_mouseHover;
	// shows the menu
	this.showMenu = ob_em_showMenu;
	// hides all menus
	this.hideMenu = ob_em_hideMenu;
	// the click on a menu item handler
	this.menuItemClick = ob_em_menuItemClick;
	// the attach to controls function
	this.attachToControls = ob_em_attachToControls;
	this.attachToControl = ob_em_attachToControl;
	// the detach from controls function
	this.detachFromControl = ob_em_detachFromControl;
	// if the mouse is over the menu
	this.mouseOverMenu = ob_em_mouseOverMenu;
	// gets the menu top left coords
	this.getMenuCoords = ob_em_getMenuCoords;
	// gets the element registered for attachment
	this.getElementAttachedFromTarget = ob_em_getElementAttachedFromTarget;
	// verifies if a menu is among the parents of current menu
	this.menuAmongParents = ob_em_menuAmongParents;
	// gets the topmost parent
	this.getTopMostParent = ob_em_getTopMostParent;
	
	this.rightClickHideMenu = true;
	this.loadEventAdded = false;
	
	ob_em_changeStyle(this.id);

	// wire the mouse over and mouse out events to the menu items
	for (var i=0; i < this.menu.childNodes[0].childNodes[0].childNodes.length; i++)
	{	
		// get the menu item
		obj = this.menu.childNodes[0].childNodes[0].childNodes[i].firstChild;
		
		if (obj.id.toLowerCase().indexOf("_7r14l") == -1)
		{
			// set initial style class
			obj.className = this.classes[obj.getAttribute("type")]["c"];
			// add the item to the items collection of the parent menu
			this.items.push (obj);
			// add a reference to the menu containing this item
			obj.menu = this;

			// create the event collections
			obj.setAttribute("mouseovercollection", new Array());
			obj.setAttribute("mouseoutcollection", new Array());
			obj.setAttribute("mouseclickcollection", new Array());

			// create the added flags attribute
			obj.setAttribute("mouseoveradded", false);
			obj.setAttribute("mouseoutadded", false);
			obj.setAttribute("mouseclickadded", false);
			
			obj.setAttribute ("mouseoverme", false);

			// add the events
			ob_em_safeAddOnEvent("onmouseover", "document.getElementById('" + ob_em_menu_id + "').childNodes[0].childNodes[0].childNodes[" + i + "].firstChild", "document.getElementById('" + ob_em_menu_id + "').childNodes[0].childNodes[0].childNodes[" + i + "].firstChild.mouseovercollection", "document.getElementById('" + ob_em_menu_id + "').childNodes[0].childNodes[0].childNodes[" + i + "].firstChild.mouseoveradded", "mouseHover(document.getElementById('" + ob_em_menu_id + "').childNodes[0].childNodes[0].childNodes[" + i + "].firstChild, ev, true)", this.id);
			ob_em_safeAddOnEvent("onmouseout", "document.getElementById('" + ob_em_menu_id + "').childNodes[0].childNodes[0].childNodes[" + i + "].firstChild", "document.getElementById('" + ob_em_menu_id + "').childNodes[0].childNodes[0].childNodes[" + i + "].firstChild.mouseoutcollection", "document.getElementById('" + ob_em_menu_id + "').childNodes[0].childNodes[0].childNodes[" + i + "].firstChild.mouseoutadded", "mouseHover(document.getElementById('" + ob_em_menu_id + "').childNodes[0].childNodes[0].childNodes[" + i + "].firstChild, ev, false)", this.id);
			ob_em_safeAddOnEvent("onclick", "document.getElementById('" + ob_em_menu_id + "').childNodes[0].childNodes[0].childNodes[" + i + "].firstChild", "document.getElementById('" + ob_em_menu_id + "').childNodes[0].childNodes[0].childNodes[" + i + "].firstChild.mouseclickcollection", "document.getElementById('" + ob_em_menu_id + "').childNodes[0].childNodes[0].childNodes[" + i + "].firstChild.mouseclickadded", "menuItemClick(document.getElementById('" + ob_em_menu_id + "').childNodes[0].childNodes[0].childNodes[" + i + "].firstChild, ev)", this.id);

			obj.setAttribute("oncontextmenu", this.id + ".hideMenu(event, false, false);");
		}
	}

	ob_em_safeAddOnEvent("onload", "window", "ob_em_gSafeOnLoad", "ob_em_loadEventAdded", "attachToControls()", this.id);
	
	this.underlay.style.backgroundColor = "transparent";
	this.cover_iframe.style.border = "1px solid blue";
	
	// if event is mouseover
	if (this.showEvent == "mouseover") 
	{
		// add an extra 10px border around the menu in which menu is not hidden
		this.underlay.style.padding = "10";
		if (document.all) this.underlay.onmouseout = new Function(this.id + ".hideMenu(window.event, true, false);");
		else this.underlay.setAttribute("onomuseout", this.id + ".hideMenu(event, true, false)");
	}
	
	// hide the menu when lost focus or right click occured over it
	if(document.all)
	{
		this.underlay.onblur = new Function(this.id + ".hideMenu(window.event, false, true);");
		this.underlay.oncontextmenu = new Function(this.id + ".hideMenu(window.event, false, true);");
		if (this.showEvent == 'mouseover') this.underlay.onmouseout = new Function(this.id + ".hideMenu(window.event, true, false);")
	}
	else
	{
		this.underlay.setAttribute("onblur", this.id + ".hideMenu(event, false, false)");
		this.underlay.setAttribute("oncontextmenu", "return false;");
		if (this.showEvent == 'mouseover') this.underlay.setAttribute("onmouseout", this.id + ".hideMenu(event, true, false)");
	}
}

function ob_em_attachToControls ()
{
	// make the menu direct child of the document (if not already)
	if (document.body != this.underlay.parentNode)
	{
		this.underlay.parentNode.removeChild(this.underlay);
		document.body.appendChild(this.underlay);
		this.cover_iframe.parentNode.removeChild(this.cover_iframe);
		document.body.appendChild(this.cover_iframe);
	}

	addedDocumentEventsForThisMenu = false;

	// create the collections and the added attribute
	if (document.ob_em_mouseclickcollection == undefined) document.ob_em_mouseclickcollection = new Array();
	if (document.ob_em_mouseclickadded == undefined) document.ob_em_mouseclickadded = false;
	if (document.ob_em_contextmenucollection == undefined) document.ob_em_contextmenucollection = new Array();
	if (document.ob_em_contextmenuadded == undefined) document.ob_em_contextmenuadded = false;
	if (document.ob_em_mouseovercollection == undefined) document.ob_em_mouseovercollection = new Array();
	if (document.ob_em_mouseoveradded == undefined) document.ob_em_mouseoveradded = false;

	// wire the event to hide the menu upon clicking on the document
	ob_em_safeAddOnEvent("onclick", "document", "document.ob_em_mouseclickcollection", "document.ob_em_mouseclickadded", "documentMouseDown(ev)", this.id);

	// loop through the array of controls ids
	for (var i=0; i < this.oat_id.length; i++)
		this.attachToControl (this.oat_id[i]);
}

function ob_em_attachToControl (id)
{
	// if document is contained, we 'translate' this to document.body
	ob_em_oat = id.toLowerCase() == "document" ? this.body : document.getElementById(id);
	if (ob_em_oat != null)
	{
		// if the menu is already attached to this element, don't attach again
		if (ob_em_searchArrayForElement (this.oat_ids, id) != -1)  return;
		this.oat_ids.push (id);
		
		if (ob_em_oat.ob_em_mouseclickcollection == undefined) ob_em_oat.ob_em_mouseclickcollection = new Array();
		if (ob_em_oat.ob_em_mouseclickadded == undefined) ob_em_oat.ob_em_mouseclickadded = false;
		if (ob_em_oat.ob_em_contextmenucollection == undefined) ob_em_oat.ob_em_contextmenucollection = new Array();
		if (ob_em_oat.ob_em_contextmenuadded == undefined) ob_em_oat.ob_em_contextmenuadded = false;
		if (ob_em_oat.ob_em_mouseovercollection == undefined) ob_em_oat.ob_em_mouseovercollection = new Array();
		if (ob_em_oat.ob_em_mouseoveradded == undefined) ob_em_oat.ob_em_mouseoveradded = false;
		if (ob_em_oat.ob_em_mouseoutcollection == undefined) ob_em_oat.ob_em_mouseoutcollection = new Array();
		if (ob_em_oat.ob_em_mouseoutadded == undefined) ob_em_oat.ob_em_mouseoutadded = false;
		if (ob_em_oat.ob_em_submenu == undefined) ob_em_oat.ob_em_submenu = null;
		
		// if the control to attach to is a menu item belonging to a different menu
		var pm = ob_em_oat.menu;
		if (pm != null && pm.menu != null && typeof(pm.menu.tagName) != 'undefined' && pm.menu.tagName.toLowerCase() == 'div' && pm.id != this.id)
		{
			pm.subMenus.push (this);
			this.parentMenu = pm;
			this.parentItem = ob_em_oat;
			ob_em_oat.ob_em_submenu = this;
			ob_em_oat.setAttribute("nohide", "true");

			// make the submenu arrow cell visible
			if (this.parentItem.firstChild.firstChild.firstChild.firstChild.type == 'm') this.parentItem.firstChild.firstChild.firstChild.firstChild.style.display='block';
			else if (this.parentMenu.ui) this.parentItem.firstChild.firstChild.firstChild.firstChild.nextSibling.nextSibling.style.display='block';
			else this.parentItem.firstChild.firstChild.firstChild.firstChild.nextSibling.style.display='block';
			// make the content cell occupy only one column
			this.parentItem.firstChild.firstChild.firstChild.firstChild.nextSibling.colspan='1';
		}
	
		// register the events
		if (document.layers)
		{
			ob_em_oat.captureEvents(Event.CLICK | Event.MOUSEDOWN | Event.MOUSEUP | Event.CONTEXTMENU | Event.MOUSEOVER | Event.MOUSEOUT);
			ob_em_body.captureEvents(Event.CLICK | Event.MOUSEDOWN | Event.MOUSEUP | Event.CONTEXTMENU | Event.MOUSEOVER | Event.MOUSEOUT);
			document.captureEvents(Event.CLICK | Event.MOUSEDOWN | Event.MOUSEUP | Event.CONTEXTMENU | Event.MOUSEOVER | Event.MOUSEOUT);
		}

		// attach the events
		if (this.showEvent == 'contextmenu')
		{
			ob_em_safeAddOnEvent("oncontextmenu", "document.getElementById('" + ob_em_oat.id + "')", "document.getElementById('" + ob_em_oat.id + "').ob_em_contextmenucollection", "document.getElementById('" + ob_em_oat.id + "').ob_em_contextmenuadded", "documentMouseDown(" + (document.all ? "window.event" : "ev") + ")", this.id);
			if (ob_em_oat != document && !addedDocumentEventsForThisMenu) 
				addedDocumentEventsForThisMenu = true;
		}
		else if (this.showEvent == 'mouseclick')
		{
			ob_em_safeAddOnEvent("onclick", "document.getElementById('" + ob_em_oat.id + "')", "document.getElementById('" + ob_em_oat.id + "').ob_em_mouseclickcollection", "document.getElementById('" + ob_em_oat.id + "').ob_em_mouseclickadded", "documentMouseDown(" + (document.all ? "window.event" : "ev") + ")", this.id);
			ob_em_safeAddOnEvent("oncontextmenu", "document.getElementById('" + ob_em_oat.id + "')", "document.getElementById('" + ob_em_oat.id + "').ob_em_contextmenucollection", "document.getElementById('" + ob_em_oat.id + "').ob_em_contextmenuadded", "hideMenu(" + (document.all ? "window.event" : "ev") + ", true, false)", this.id);
			if (ob_em_oat != document && !addedDocumentEventsForThisMenu) 
				addedDocumentEventsForThisMenu = true;
		}
		else if (this.showEvent == 'mouseover')
		{
			ob_em_safeAddOnEvent("onmouseover", "document.getElementById('" + ob_em_oat.id + "')", "document.getElementById('" + ob_em_oat.id + "').ob_em_mouseovercollection", "document.getElementById('" + ob_em_oat.id + "').ob_em_mouseoveradded", "showMenu(" + (document.all ? "window.event" : "ev") + ")", this.id);
			ob_em_safeAddOnEvent("onmouseout", "document.getElementById('" + ob_em_oat.id + "')", "document.getElementById('" + ob_em_oat.id + "').ob_em_mouseoutcollection", "document.getElementById('" + ob_em_oat.id + "').ob_em_mouseoutadded", "hideMenu(" + (document.all ? "window.event" : "ev") + ", true, false)", this.id);
			ob_em_safeAddOnEvent("oncontextmenu", "document.getElementById('" + ob_em_oat.id + "')", "document.getElementById('" + ob_em_oat.id + "').ob_em_contextmenucollection", "document.getElementById('" + ob_em_oat.id + "').ob_em_contextmenuadded", "hideMenu(" + (document.all ? "window.event" : "ev") + ", true, false)", this.id);
			if (ob_em_oat != document && !addedDocumentEventsForThisMenu)
				addedDocumentEventsForThisMenu = true;
		}
	}
}

function ob_em_detachFromControl (id)
{
	// position of element in list of elements to which this element is attached
	var positionInArray = ob_em_searchArrayForElement (this.oat_ids, id);
	// if the menu didn't attach to this id
	if (positionInArray == -1) return;

	// if document is contained, we 'translate' this to document.body
	ob_em_oat = id.toLowerCase() == "document" ? this.body : document.getElementById(id);

	// if the element is not null
	if (ob_em_oat != null)
	{
		// if menu is shown on right click
		if (this.showEvent == 'contextmenu')
		{
			// remove oncontextmenu
			ob_em_removeEvent (ob_em_oat.oncontextmenu, ob_em_oat.ob_em_contextmenucollection, this.id + ".documentMouseDown");
		}
		// if menu is shown on mouse click
		else if (this.showEvent == 'mouseclick')
		{
			// remove onclick
			ob_em_removeEvent (ob_em_oat.onclick, ob_em_oat.ob_em_mouseclickcollection, this.id + ".documentMouseDown");
		}
		// if menu is shown on mouse over
		else if (this.showEvent == 'mouseover')
		{
			// remove onmouseover
			ob_em_removeEvent (ob_em_oat.onmouseover, ob_em_oat.ob_em_mouseovercollection, this.id + ".showMenu");
			// remove onmouseout
			ob_em_removeEvent (ob_em_oat.onmouseout, ob_em_oat.ob_em_mouseoutcollection, this.id + ".hideMenu");
		}

		// if the control was attached to to a menu item belonging to a different menu
		var pm = ob_em_oat.menu;
		if (pm != null && pm.menu != null && typeof(pm.menu.tagName) != 'undefined' && pm.menu.tagName.toLowerCase() == 'div' && pm.id != this.id)
		{
			var subMenuPosition = ob_em_searchArrayForElement (pm.subMenus, this);
			if (subMenuPosition != -1) pm.subMenus.splice (subMenuPosition, 1);
			this.parentMenu = pm;
			this.parentItem = ob_em_oat;
			ob_em_oat.ob_em_submenu = null;
			ob_em_oat.setAttribute("nohide", "false");

			// make the submenu arrow cell invisible
			if (this.parentItem.firstChild.firstChild.firstChild.firstChild.type == 'm') this.parentItem.firstChild.firstChild.firstChild.firstChild.style.display='none';
			else if (this.parentMenu.ui) this.parentItem.firstChild.firstChild.firstChild.firstChild.nextSibling.nextSibling.style.display='none';
			else this.parentItem.firstChild.firstChild.firstChild.firstChild.nextSibling.style.display='none';
			// make the content cell occupy both collumns
			this.parentItem.firstChild.firstChild.firstChild.firstChild.nextSibling.colspan='2';

			this.parentMenu = null;
			this.parentItem = null;
		}

		// remove from list with elements to which it's attached
		this.oat_ids.splice(positionInArray, 1);
	}
}

function ob_em_documentMouseDown (e)
{
	if (!e) e = window.event;
	if (!e) 
	{
		this.hideMenu(null, true, false);
		return false;
	}
	
	var eventType = e.type.toLowerCase();
	if (e.type.toLowerCase() == "click") eventType = "mouseclick";
	if (!document.all && e.type.toLowerCase() == "click" && e.which == 3) eventType = "contextmenu";
	
	if (eventType.toLowerCase() == this.showEvent.toLowerCase())
	{
		// try to show menu
		return this.showMenu(e);
	}
	// if a click has taken place inside the menu and the menu was visible
	else if (this.mouseOverMenu(e)) return true;
 	// hide the menus
 	else 
 	{
 		this.rightClickHideMenu = true;
 		this.hideMenu(e, false, false);
 		return true;
 	}
	
	return false;
}

function ob_em_mouseHover (obj, e, over)
{
	// set the event for ie
	if (!e) e = window.event;

	if (over && obj.getAttribute ("mouseoverme").toString().toLowerCase() == 'true') return;
	else if (over) obj.setAttribute ("mouseoverme", true);
	else obj.setAttribute ("mouseoverme", false);

	if (obj.ob_em_submenu != null && obj.ob_em_submenu.menu != null && obj.ob_em_submenu.menu.parentNode.style.visibility.toLowerCase() == "visible") return;
	
	// we set the class name to that of the element type (if over, concatenated with _o)
	obj.className = this.classes[obj.getAttribute("type")]["c" + (over ? "_o" : "")];

	try
	{
		// if there is a link on this menu item
		if (obj.firstChild.tagName.toLowerCase() == 'a') parentObj = obj.firstChild;
		else parentObj = obj;

		// the first cell (content, submenu or icon)
		tmpObj = parentObj.firstChild.firstChild.firstChild.firstChild;
		tmpObj.className = this.classes[obj.getAttribute("type")]["c_" + tmpObj.getAttribute("type") + (over ? "_o" : "")];
		
		// the second cell (content)
		tmpObj = parentObj.firstChild.firstChild.firstChild.firstChild.nextSibling;
		tmpObj.className = this.classes[obj.getAttribute("type")]["c_" + tmpObj.getAttribute("type") + (over ? "_o" : "")];

		// if the menu uses icons
		if (this.ui)
		{
			// the third cell (submenu or icon)
			tmpObj = parentObj.firstChild.firstChild.firstChild.firstChild.nextSibling.nextSibling;
			tmpObj.className = this.classes[obj.getAttribute("type")]["c_" + tmpObj.getAttribute("type") + (over ? "_o" : "")];
		}
	}
	catch (e) {}
}

function ob_em_showMenu(e)
{
	if (!e) e = window.event;

	var prev_el = this.el;

	// set the element over which the right click has taken place
	if (e.target) this.el = e.target;
	else if (e.srcElement) this.el = e.srcElement;
	this.getElementAttachedFromTarget();

	if (this.el == null)
	{
		this.el = prev_el;
		if (!this.mouseOverMenu(e))
		{
			this.el = null;
			if (this.underlay.style.visibility.toLowerCase() == "visible") this.hideMenu(e, true, false);
			return true;
		}
	}

	// if by any chance a menu is to be shown without having its parent visible, it will not be shown
	if (this.parentMenu != null && this.el.menu != null && this.el.menu == this.parentMenu && this.parentMenu.underlay.style.visibility.toLowerCase() == 'hidden') return;
	// close all menus attached to siblings of the parentItem
	if (this.parentMenu != null) 
		for (var i=0; i < this.parentMenu.subMenus.length; i++)
			if (this.parentMenu.subMenus[i] != this) this.parentMenu.subMenus[i].hideMenu(e, false, true);

	// get the coords of the menu
	this.showCoords = this.getMenuCoords (e, true);
	
	// if the event is not mouseover or the cursor is not over the menu, hide all the menus
	ob_em_hideAllMenus(e, false, this.parentMenu != null ? this : null);

	// do not attempt redraw unles displaying at cursor
	if (this.displayPosition != 'cursor' && this.underlay.style.visibility.toLowerCase() == "visible") return;

	// change the stylesheet to that of the menu
	ob_em_changeStyle(this.id);

	// set the menu coordinates
	this.underlay.style.left = this.showCoords[0] + "px";
	this.underlay.style.top = this.showCoords[1] + "px";

	// unmark all items
	for (var i=0; i < this.items.length; i++) if (this.items[i].id != obj.id) this.mouseHover (this.items[i], e, false);

	// make the menu visible
	this.underlay.style.visibility = "visible";
	this.menu.style.visibility = "visible";
	this.menu.style.display = "block";
	
	// set the coords and dimensions of the iframe
	if (document.all)
	{
		this.cover_iframe.style.width = this.menu.offsetWidth - 2;
		this.cover_iframe.style.height = this.menu.offsetHeight - 2;
    }

    if (this.showEvent == "mouseover")
	{
		if (this.menuCorner[0].toLowerCase() == "top") this.cover_iframe.style.top = this.showCoords[1] + 10; else this.cover_iframe.style.top = this.showCoords[1] + 10;
		if (this.menuCorner[1].toLowerCase() == "left") this.cover_iframe.style.left = this.showCoords[0] + 10; else this.cover_iframe.style.left = this.showCoords[0] + 10;
	}
	else
	{
		this.cover_iframe.style.top = this.underlay.style.top;
		this.cover_iframe.style.left = this.underlay.style.left;
	}

    this.cover_iframe.style.zIndex = this.underlay.style.zIndex - 1;
    this.cover_iframe.style.display = "block";
	
	// stop event propagation
	e.returnValue = false;
	e.cancelBubble = true; 
	if (e.stopPropagation) e.stopPropagation();
	
	return false;
}

function ob_em_hideAllMenus(e, withVerify, menu)
{
	// loop through all the menus ids in the array ob_em_Menus
	for (i=0; i < ob_em_Menus.length; i++)
	{
		try
		{
			if (typeof(eval(ob_em_Menus[i])) != 'undefined' && eval(ob_em_Menus[i]) != null && eval(ob_em_Menus[i]).menu.style.visibility.toLowerCase() == "visible")
			{
				// if the menu is not among the current menu parents, hide it
				if (menu == null || menu.id.substr(0, 6) != 'ob_em_' || menu.menuAmongParents(ob_em_Menus[i]))
					eval(ob_em_Menus[i]).hideMenu(e, withVerify, false);
			}
		}
		catch (e) {}
	}
		
	return false;
}

function ob_em_hideMenu(e, withVerify, force)
{
	if (force == null) force = false;
	if (!e) e = window.event;
	
	if (this.id.substring(0, 6).toLowerCase() != 'ob_em_')
	{
		if (eval('ob_em_' + this.id) != null) eval('ob_em_' + this.id).hideMenu(e, withVerify, force);
		return;
	}

	menu = this.menu != undefined ? this.menu : this;
	if (menu.parentNode.style.visibility == 'hidden') return;

	var isMouseOverMenu = e ? (this.showCoords != null ? this.mouseOverMenu(e, this.showCoords) : false) : false;

	if (!force && !this.rightClickHideMenu && isMouseOverMenu == null) return;

	if (force || (menu.parentNode.style.visibility == 'hidden' || (this.showEvent != 'mouseover' || (e && (!withVerify || !isMouseOverMenu) ))))
	{
		// set its coordinates to 0,0 and visibility to hidden
		menu.parentNode.style.left = 0;
		menu.parentNode.style.top = 0;
		menu.parentNode.style.visibility = "hidden";
		menu.parentNode.style.display = "none";

		// if the iframe is not set, try to set it
		if (!this.cover_iframe) this.cover_iframe = document.getElementById('obout_easymenu_iframe_' + menu.id);

		// hide the iframe
		if (this.cover_iframe) this.cover_iframe.style.display = "none";
		
		// hide all submenus
		for (var i=0; i < this.subMenus.length; i++) this.subMenus[i].hideMenu (e, false, false);
		
		// if menu has parentMenu
		if (this.parentMenu != null) 
		{
			// unmark parent item
			this.parentMenu.mouseHover (this.parentItem, e, false);
			
			// close the parent menu if mouse is not over it
			this.parentMenu.hideMenu (e, true, false);
		}
		
		this.showCoords = null;

		this.rightClickHideMenu = true;
	}
}

function ob_em_menuItemClick (menuObject, menuEvent)
{
	if (!menuEvent) menuEvent = window.event;
	
	// set the params that the client can make use of
	var targetEl = this.el;
	var easyMenu = this;
	
	if (this.parentMenu != null) targetEl = this.getTopMostParent().el;
	
	// hide the menu
	this.rightClickHideMenu = true;
	if (menuObject.getAttribute("nohide") == null || menuObject.getAttribute("nohide").toString().toLowerCase() == "false") this.hideMenu(menuEvent, false, false);
	else this.mouseHover (menuObject, menuEvent, true);
	
	// execute client script
	eval(menuObject.getAttribute("OnClientClick"));
	// if item has a url set
	if (menuObject.firstChild.tagName.toLowerCase() == "a")
		menuObject.firstChild.click();
	
	// set back the autohide on click
	if (menuObject.getAttribute("nohide") != null) this.rightClickHideMenu = false;
	
	return false;
}

function ob_em_safeAddOnEvent(eventName, object, collection, eventAdded, functionToExecute, menuId)
{
	obj = eval (object);

	if(((document.all)&&((navigator.appVersion.indexOf("Mac")!=-1))) && ((document.all)&&(navigator.appVersion.indexOf("MSIE 4.")!=-1))) 
	{
		eval("obj." + eventName + ' = (function() {ob_em_safeOnEvent(collection)})');
		eval(collection + "[" + collection + ".length] = Function(" + menuId  + "." + functionToExecute + ")");
	}
	// if there are already functions wired for the event
	else if (eval("obj." + eventName))
	{
		// if the existing functions in the event were not already added to the loading array
		if(eval(eventAdded + " == false"))
		{
			eval(eventAdded + "= true");
			eval(collection + "[0] = obj." + eventName);
			eval('obj.' + eventName + ' = function(ev) {ob_em_safeOnEvent(collection, ev)}');
		}
		fct = function (ev) {eval(menuId + "." + functionToExecute);};
		eval(collection + "[" + collection + ".length] = " + fct);
	}
	else 
	{
		eval('obj.' + eventName + '= function (ev) {' + menuId + '.' + functionToExecute + ';}');
	}
}

function ob_em_safeOnEvent(collection, ev) 
{
	for(var i = 0; i < eval(collection).length; i++) 
	{
		ran = false;

		// runs each function registered in collection
		try
		{
			eval(collection)[i](ev);
			ran = true;
		}
		catch (e) {}
		
		// just to be on the safe side
		if (!ran)
		{
			try
			{
				eval (collection)[i];
			}
			catch (e) {}
		}
	}
}

// returns all stylesheets in the page
function ob_em_getAllSheets()
{
	// for ICEbrowser support (if exists)
	if (!window.ScriptEngine && navigator.__ice_version)
		return document.styleSheets;

	if (document.getElementsByTagName)
	{
		// link tags
		var Lt = document.getElementsByTagName('link');
		// style tags
		var St = document.getElementsByTagName('style');
	} 
	else if (document.styleSheets && document.all)
	{
		// link tags
		var Lt = document.all.tags('LINK');
		// style tags
		var St = document.all.tags('STYLE');
	} 
	else 
	{ 
		return []; 
	} 

	// add all links
	for (var x = 0, os = []; Lt[x]; x++)
	{
		var rel = Lt[x].rel ? Lt[x].rel : Lt[x].getAttribute ? Lt[x].getAttribute('rel') : '';
		// if is a stylesheet link
		if (typeof(rel) == 'string' && rel.toLowerCase().indexOf('style') + 1)
		{
			os[os.length] = Lt[x];
		}
	} 

	// add all styles
	for (var x = 0; St[x]; x++ )
	{
		os[os.length] = St[x];
	} 
	return os;
}

// disables all style sheets except for those set as parameters
function ob_em_changeStyle()
{
	for (var x = 0, ss = ob_em_getAllSheets(); ss[x]; x++)
	{
		// disable it
		if (ss[x].title)
		{
			ss[x].disabled = true;
		}
		// loop through the arguments
		for (var y = 0; y < arguments.length; y++) 
		{
			// if found in arguments
			if(ss[x].title == arguments[y])
			{
				// enable it
				ss[x].disabled = false; 
			} 
		}
	} 
}

// gets the absolute left position of the element
function ob_em_getLeft(obj)
{
    var pos = 0;
	
    if (!obj) return 0;
	if (obj.offsetParent)
	{
		while (obj.offsetParent)
		{
			pos += obj.offsetLeft;
			obj = obj.offsetParent;
		}
	}
	else if (obj.x) pos += obj.x;
    return pos;
}

// gets the absolute top position of the element
function ob_em_getTop(obj)
{
    var pos = 0;

    if (!obj) return 0;
    if (obj.offsetParent) 
    {
        while (obj.offsetParent) 
        {
            pos += obj.offsetTop;
            obj = obj.offsetParent;
        }
    }
    else if (obj.y) pos += obj.y;
    return pos;
}

function ob_em_getMenuCoords (e, returnCurrentIfVisible)
{
	if (this.el == null) return new Array (0,0);
	
	if (this.underlay.style.visibility == "hidden" || returnCurrentIfVisible)
	{
		var windowWidth = 0;
		var windowHeight = 0;
		var scrollLeft = document.body.scrollLeft + document.documentElement.scrollLeft;
		var scrollTop = document.body.scrollTop + document.documentElement.scrollTop;

		cNode = this.underlay;
		do
		{
			cNode = cNode.parentNode;
			if (cNode != null && cNode != document.body)
			{
				if (typeof(cNode.scrollLeft) != 'undefined') scrollLeft += cNode.scrollLeft;
				if (typeof(cNode.scrollTop) != 'undefined') scrollTop += cNode.scrollTop;
			}
			else break;
		}
		while (true);

		// display the menu on page (needed to be able to compute width)
		this.underlay.style.visibility = "hidden";
		this.underlay.style.display = "none";
		this.underlay.style.display = "block";

		// compute initial menuLeft and menuTop
		if (this.displayPosition == 'cursor')
		{
			var menuLeft = e.clientX + this.hz_o + scrollLeft;
			var menuTop = e.clientY + this.vt_o + scrollTop;
		}
		else
		{
			// align to top left of element with top left of menu
			var menuTop = ob_em_getTop(this.el) + this.vt_o;
			var menuLeft = ob_em_getLeft(this.el) + this.hz_o;
			
			if (this.elementCorner[0].toLowerCase() == 'bottom')
				if (this.el != null && this.el.offsetHeight != null && typeof(this.el.offsetHeight) != 'undefined') menuTop += this.el.offsetHeight + 1;
			if (this.menuCorner[0].toLowerCase() == 'bottom')
				menuTop -= this.underlay.offsetHeight;
			else menuTop += 1;
			if (this.elementCorner[1].toLowerCase() == 'right')
				if (this.el != null && this.el.offsetWidth != null && typeof(this.el.offsetWidth) != 'undefined') menuLeft += this.el.offsetWidth + 1;
			if (this.menuCorner[1].toLowerCase() == 'right')
				menuLeft -= this.underlay.offsetWidth;
			else menuLeft += 1;
		}

		// compute the Window Width and Window Height
		if (typeof(window.innerWidth) == 'number')
		{
			windowWidth = window.innerWidth;
			windowHeight = window.innerHeight;
		}
		else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight))
		{
			windowWidth = document.documentElement.clientWidth;
			windowHeight = document.documentElement.clientHeight;
		}
		else if (document.body && (document.body.clientWidth || document.body.clientHeight))
		{
			windowWidth = document.body.clientWidth;
			windowHeight = document.body.clientHeight;	
		}
		
		windowWidth += scrollLeft;
		windowHeight += scrollTop;

		// if align is cursor then verify that the menu is inside the visible zone
		if (this.displayPosition.toLowerCase() == 'cursor')
		{
			// if menu would be outside the document on the right but would fit in the left of the mouse pointer
			if ((menuLeft + this.underlay.offsetWidth) > windowWidth && (menuLeft - this.underlay.offsetWidth) > scrollLeft)
				menuLeft -= this.menu.offsetWidth;
			// if menu would be outside the document on the bottom but would fit in the top of the mouse pointer
			if ((menuTop + this.underlay.offsetHeight) > windowHeight && (menuTop - this.underlay.offsetHeight) > scrollTop)
				menuTop -= this.menu.offsetHeight;
		}

		if (this.showEvent == "mouseover")
		{
			if (this.displayPosition.toLowerCase() == 'cursor')
			{
				if (this.menuCorner[0].toLowerCase() == "top") menuTop += 10; else menuTop -= 10;
				if (this.menuCorner[1].toLowerCase() == "left") menuLeft += 10; else menuLeft -= 10;
			}
			else
			{
				if (this.menuCorner[0].toLowerCase() == "top") menuTop -= 10; else menuTop += 10;
				if (this.menuCorner[1].toLowerCase() == "left") menuLeft -= 10; else menuLeft += 10;
			}
		}

		return new Array(menuLeft, menuTop);
	}
	else
		return new Array(this.underlay.offsetLeft, this.underlay.offsetTop);
}

function ob_em_mouseOverMenu (e, coords)
{
	if (this.underlay.style.visibility != 'visible') return false;
	
	if (!coords) coords = this.getMenuCoords(e, false);
	var menuLeft = eval(coords[0]);
	var menuTop = eval(coords[1]);
	
	var scrollLeft = document.body.scrollLeft + document.documentElement.scrollLeft
	var scrollTop = document.body.scrollTop + document.documentElement.scrollTop;

	var mouseLeft = e.clientX == -1 ? ob_em_getLeft(e.fromElement) - e.offsetX : e.clientX;
	var mouseTop = e.clientY == -1 ? ob_em_getTop(e.fromElement) - e.offsetY : e.clientY;
	
	var result = (this.underlay.style.visibility == 'visible' && 
				((mouseLeft + scrollLeft) >= menuLeft) && 
				((mouseLeft + scrollLeft) <= (menuLeft + this.underlay.offsetWidth)) && 
				((mouseTop + scrollTop) >= menuTop) && 
				((mouseTop + scrollTop) <= (menuTop + this.underlay.offsetHeight)));

	for (var i=0; i < this.subMenus.length; i++)
		result = result || this.subMenus[i].mouseOverMenu (e);
	
	return result;
}

function ob_em_getElementAttachedFromTarget ()
{
	if (this.el == null) return null;

	do
	{
		var index = ob_em_searchArrayForElement(this.oat_ids, this.el.id);
		if (index > -1) return this.oat_ids[index];
		else this.el = this.el.parentNode;
	}
	while (this.el != null)
	
	return null;
}

function ob_em_searchArrayForElement (array, element)
{
	if (element == null) return -1;
	
	for (var i = 0; i < array.length; i++)
	{
		if (typeof(element) == 'string') {if (array[i].toLowerCase() == element.toLowerCase()) return i;}
		else if (array[i] == element) return i;
	}
		
	return -1;
}

function ob_em_arrangeAlignProperty (align)
{
	var result = new Array();
	if (align.toLowerCase().substr(0, 3) == 'top')
	{
		result[0] = align.substr(0, 3);
		result[1] = align.substr(3, align.length);
	}
	else
	{
		result[0] = align.substr(0, 6);
		result[1] = align.substr(6, align.length);
	}
	return result;
}

function ob_em_menuAmongParents (menu)
{
	var result = false;
	
	while (menu != null)
		if (this.id == menu.id)
		{
			result = true;
			break;
		}
		else menu = menu.parentMenu;
	return result;
}

function ob_em_getTopMostParent ()
{
	var result = null;
	var menu = this.parentMenu;
	
	while (menu != null) 
	{
		result = menu;
		menu = menu.parentMenu;
	}
	
	return result;
}

function ob_em_removeEvent (ev, collection, fct)
{
	// check if the collection has elements
	if (collection.length == 0)
	{
		if (ev.toString().indexOf(fct) != -1)
			ev = null;
	}
	else
	{
		for (var i=0; i < collection.length; i++)
			if (collection[i].toString().indexOf(fct) != -1 || collection[i].toString() == 'function (ev) {eval(menuId + "." + functionToExecute);}')
				collection.splice(i, 1);
	}
}

function ob_em_getDefaultClasses (menu)
{
	for (var itemType in menu.classes)
	{
		if (menu.classes[itemType] == 'default')
		{
			// (mi - MenuItem, ms - MenuSeparator, i - IconCell, c - ContentCell, _o - MouseOver)
			switch (itemType.toLowerCase())
			{
				case "menuseparator": menu.classes[itemType] = {"c":"easyMenuSeparator","c_o":"easyMenuSeparatorOver","c_c":"easyMenuSeparatorContentCell","c_c_o":"easyMenuSeparatorContentCellOver","c_i":"easyMenuSeparatorIconCell","c_i_o":"easyMenuSeparatorIconCellOver","c_m":"easyMenuSeparatorSubMenuCell","c_m_o":"easyMenuSeparatorSubMenuCellOver"}; break;
				default: menu.classes[itemType] = {"c":"easyMenuItem","c_o":"easyMenuItemOver","c_c":"easyMenuItemContentCell","c_c_o":"easyMenuItemContentCellOver","c_i":"easyMenuItemIconCell","c_i_o":"easyMenuItemIconCellOver","c_m":"easyMenuItemSubMenuCell","c_m_o":"easyMenuItemSubMenuCellOver"}; break;
			}
		}
	}
}