mergeInto(LibraryManager.library, {
	GetProgress: function (){
		console.log(progres);
		var returnStr = progres;
		var bufferSize = lengthBytesUTF8(returnStr) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(returnStr, buffer, bufferSize);
		return buffer;
	},
	
	Unity_SetProgress: function (stringVal){
		var m_string = Pointer_stringify(stringVal);
		console.log("Trying to SetProgress: " + m_string);
        SetProgress(m_string);
	},
	
	OpenTrainer: function (stringVal, intValLevel, intValStep, boolVal){
		console.log("Trying to open Trainer: " + Pointer_stringify(stringVal) + ", level: " + intValLevel + ", step: " + intValStep);
		startGame(Pointer_stringify(stringVal), intValLevel, intValStep, boolVal);
	},
	
	Unity_AddStar: function (){
		console.log("calling addStar()");
		addStar();
	},
	
	LibConsoleWriter: function (stringVal){
		console.log(Pointer_stringify(stringVal));
	},
	
	printErr: function (message) { console.error(message); }
});

	
	