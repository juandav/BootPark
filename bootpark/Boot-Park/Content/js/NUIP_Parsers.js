




// if 0 is not containt numbers
function findNumber( text ){
	var numeros="0123456789";
	for (var i = text.length-1; i >= 0; i--) {
		if (numeros.indexOf(text.charAt(i),0)!=-1){
			return i+1;
		}
	}

	return 0;
}