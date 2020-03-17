function testVoid() {
    var num=parseInt(value);
    unityInstance.SendMessage('WebGLMessageHandler', 'RecieveMessageInt',num);
    window.alert('Void');
   console.log(num);
   console.log(typeof (num));
}

function GetUserID(){
	var val = parseInt(value);
	console.log("UserID: "+val);
	return val;
}

function GetProgress(){
	console.log(progres);
	return progres;
}

function SetProgress(JSONinput){
	console.log("Saving progress...");
	console.log("Unity sent this: " + JSONinput);
	var l_progress = JSONinput;
}

function OpenTrainer(StringTrainerType, intLevel, intStep){
	console.log("Trying to open Trainer: " + StringTrainerType + ", level: " + intLevel + ", step: " + intStep);
	//put here code to perform trainer window opening
}

function ConsoleWriter(strInput){
	console.log(strInput);
}

function printErr(strInput){
	console.log("Error: "+ strInput);
}