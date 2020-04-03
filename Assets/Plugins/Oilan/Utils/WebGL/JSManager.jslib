mergeInto(LibraryManager.library, {
	GetProgress: function (){
		console.log(progres);
		var returnStr = progres;
		var bufferSize = lengthBytesUTF8(returnStr) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(returnStr, buffer, bufferSize);
		return buffer;
	},
	
	LibConsoleWriter: function (stringVal){
		console.log(Pointer_stringify(stringVal));
	},
	
	SetProgress: function (stringVal){
		SetProgress(Pointer_stringify(stringVal));
	},
	
	OpenTrainer: function (stringVal, intValLevel, intValStep, boolVal){
		console.log("Trying to open Trainer: " + Pointer_stringify(stringVal) + ", level: " + intValLevel + ", step: " + intValStep);
		startGame(Pointer_stringify(stringVal), intValLevel, intValStep, boolVal);
	},
	
	printErr: function (message) { console.error(message); }
});

	
	