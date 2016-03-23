
$(document).scannerDetection( function( data ) { 
	
	//var separator = '0F' || '0M';
	//var a = data.split('0F');
    data = data.replace(/ /g,"");;
	var patron = /0F|0M/g;
	var section = data.split(patron);


	var firstIndex = findNumber(section[0]);
	var lastIndex = section[0].length-1;
	var name = section[0].substr( firstIndex, lastIndex );
	var dn = section[0].substr( firstIndex-10, 10 );
	var rn =dn.substr(0,1);
    var dni= rn!=1?parseInt(section[0].substr( firstIndex-9, 10)):dn;
    console.log('dni: ' + dni);
    SPIDENTIFICACION.setValue(dni);
	console.log( 'name: ' + name );
}).keydown(function(e){

	var not = e.keyCode == 32;
   	// var not = e.keyCode != 116 || e.keyCode == 50 || e.keyCode == 51 || e.keyCode == 9 || e.keyCode == 48;
    if (not) { 
      return false;
    };
});


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