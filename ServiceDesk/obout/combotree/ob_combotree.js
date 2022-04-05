function ob_cmb_nodeSelectFilter(node)
{
	/// accept only leaf nodes
	return (ob_hasChildren(node) == false);

	/// accept any node
	/// return true;

	/// accept only nodes with children
    /// return ob_hasChildren(node);
}

function ob_getTreeIdByChildElement(ob_od)
{
	try
	{
		if (ob_od.tagName.toLowerCase() == "body") return;
		if (ob_od.className.toLowerCase() == "ob_tree") return ob_od.id;

		while (ob_od.parentNode.tagName.toLowerCase() != "body")
		{
			if (ob_od.parentNode.className.toLowerCase() == "ob_tree")
				return ob_od.parentNode.id;
			ob_od = ob_od.parentNode;
		}
		
		return;
	}
	catch (e)
	{
	}
	return;
}

function ob_cmb_bind(tree_id, ID, serverSide)
{
	if (typeof treeComboServerSide == 'undefined')
		treeComboServerSide = new Array();
	treeComboServerSide[tree_id] = serverSide;
	if (typeof n_ob_t22 == 'undefined')
	{
    	n_ob_t22 =
		function(ob_od, ev)
		{
			var node = document.getElementById(tree_selected_id);
			if (node != null && ob_cmb_nodeSelectFilter(node))
			{
				var tree_id = ob_getTreeIdByChildElement(node);
				if ((!tree_id) || (!tree2id[tree_id]))
					return;
				var textbox = document.getElementById(tree2id[tree_id] + "_textbox");
				if (textbox != null)
				{
					textbox.value = node.innerHTML;
                    var idBox = document.getElementById(tree2id[tree_id] + "_selectedId");
					if (idBox != null)
					{
						var u = idBox.value != node.id;
						idBox.value = node.id;
						if (treeComboServerSide[tree_id] && u)
							document.forms[0].submit();
					}
				}
				while (node != null && !(typeof(node.id) != 'undefined' && node.tagName.toLowerCase() == 'div' && eval('typeof(ob_em_' + node.id + ')') != "undefined"))
					node = node.parentNode;

				if (node != null) eval('ob_em_' + node.id + '.hideMenu(null, null, true)');
			}
		}
	}
    // override node selection function
	var _ob_t22 = ob_t22;
	ob_t22 = function(ob_od,ev) { _ob_t22(ob_od, ev); n_ob_t22(ob_od, ev); }

	// tree 2 combobox binding is not defined
	if (typeof tree2id == 'undefined')
		tree2id = new Array();

	if (tree2id[tree_id])
		return;
	else
		tree2id[tree_id] = ID;
}