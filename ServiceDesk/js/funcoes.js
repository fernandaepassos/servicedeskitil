
    function FormataData(data)
	{ 
		var mydata = ''; 
		var myobj = ''; 
		var Tecla = event.which;
		mydata = mydata + data.value; 
		myobj = data; 
		if (mydata.length == 2 || mydata.length == 5)
		{ 
			mydata = mydata + '/'; 
			myobj.value = mydata; 
		} 
		if(Tecla == null)
		{
			Tecla = event.keyCode;
			if ( Tecla < 48 ||  Tecla > 57)
			{
				event.returnValue = false;
				alert('Caracter inválido!');
				return false
				if (mydata.length == 10 )
				{
					ValidaData(data);
				}
			}
			event.returnValue = true;
			return true
		}  
	}
	
	function FormataTel(tel)
	{ 
		var mytel = ''; 
		var myobj = ''; 
		var Tecla = event.which;
		mytel = mytel + tel.value; 
		myobj = tel; 
		
		if (mytel.length == 2)
		{ 
			mytel = mytel + '-';
			myobj.value = mytel; 
		}	
		if (mytel.length == 7)
		{ 
			mytel = mytel + '-'; 
			myobj.value = mytel; 
		} 
		
		
		if(Tecla == null)
		{
			Tecla = event.keyCode;
			if ( Tecla < 48 ||  Tecla > 57)
			{
				event.returnValue = false;
				return false
			}
			event.returnValue = true;
			return true
		}  
	}
	function Somente_Numeros(codigo)
	{ 
		var Tecla = event.which;
		if(Tecla == null)
			{
			Tecla = event.keyCode;
			if ( Tecla < 48 ||  Tecla > 57)
			{
				event.returnValue = false;
	    		return false
			}
			event.returnValue = true;
			return true
			}  
	}
	
	function verifica() 
	{
	    if (confirm("Deseja mesmo excluir este item?")) {
		    return true;
	    }
	    else {
		    return false;
	    }
    }
