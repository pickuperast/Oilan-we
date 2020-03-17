import Umnozheniye         from "../umnozheniye/Umnozheniye";
import RunFlashCart        from "../flesh_cart/RunFlashCart";
import Mental              from "../mental/Mental";
import Deleniye            from "../deleniye/Deleniye";
import {createHtmlElement} from "../game";

// =====================================================================

//================== таблица умножения ==================
export function multiplicationTableFunc() {
	document.querySelector('.title').textContent = 'таблица умножения';
	const main = document.querySelector('#main'),
		table = document.querySelector('#app_simulator');
	
	table.innerHTML = null;
	
	const cart = createHtmlElement(`
			<div class="multiplicationTableChoose">
			<div class="counts">
				<h4 class="text">Выбирете цифры: </h4>
				 <div class="radios">
				   <input type="radio" data-name="2" class="radio radio_1" value="1-2">
				   <input type="radio" data-name="3" class="radio radio_2 active" value="3">
				   <input type="radio" data-name="4" class="radio radio_3" value="4">
				   <input type="radio" data-name="5" class="radio radio_4" value="5">
				   <input type="radio" data-name="6" class="radio radio_5" value="6">
				   <input type="radio" data-name="7" class="radio radio_6" value="7">
				   <input type="radio" data-name="8" class="radio radio_7" value="8">
				   <input type="radio" data-name="9" class="radio radio_8" value="9-10">
				   <input type="radio" data-name="random" class="radio radio_Rand" value="Смешанные">
				   <div class="ball"></div>
				 </div>
			</div>
		</div>
		`);
	
	table.appendChild(cart);
	selectNumberOfEquations(cart);
	
	const countRadios = document.querySelectorAll('.counts .radio');
	const ballCount = document.querySelector('.counts .ball');
	let digitNumber = document.querySelector('.counts .radio.active');
// =====================================================================
	addClassActive(countRadios, digitNumber, ballCount);
// =====================================================================
	
	// создаем кнопку "поехали"
	const buttonGO = createHtmlElement(`<input id="buttonGO" type="button" value="поехали!">`);
	main.appendChild(buttonGO);
	buttonGO.addEventListener('click', (e) => {
		const numberOfEquations = document.querySelector('.equationCounts .radio.active');
		const mainCount = document.querySelector('.counts .radio.active').getAttribute('data-name');
		let umn = new Umnozheniye({
			M: +numberOfEquations.value,
			r1: 1,
			r2: 1,
			mainCount
		});
		
		// if (umn.mainCount === 'random') {
		umn.startUmnozheniye();
		// }
		// else {
		// 	table.innerHTML = `<div class="video">
		// 							<video controls autoplay>
		// 		                  <source src="../../video/SIMPLE.mp4"
		// 		                      type='video/mp4; codecs="avc1.42E01E, mp4a.40.2"'>
		//                     </video>
		//                     <input id="buttonMiss" type="button" value="пропустить">\`
		//                   </div>`;
		// 	document.querySelector("#buttonMiss").addEventListener('click', () => {
		// 		umn.startUmnozheniye();
		// 	});
		// }
		console.log(umn);
		buttonGO.remove();
	});
	
}

//================== флеш карти ==================
export function flashCartFunc() {
	document.querySelector('.title').innerHTML = 'Флеш - карты';
	const main = document.querySelector('#main'),
		table = document.querySelector('#app_simulator');
	table.innerHTML = null;
	
	const cart = createHtmlElement(`
		<div class="flashCartChoose">
			<div class="counts">
				<h4 class="text">Выбирете разряд числа: </h4>
				  <div class="radios">
				    <input type="radio" data-name="1" class="radio radio_1" value="1">
				    <input type="radio" data-name="2" class="radio radio_2 active" value="2">
				    <input type="radio" data-name="3" class="radio radio_3" value="3">
				    <input type="radio" data-name="4" class="radio radio_4" value="4">
				    <input type="radio" data-name="5" class="radio radio_5" value="5">
				    <input type="radio" data-name="6" class="radio radio_6" value="6">
				    <input type="radio" data-name="7" class="radio radio_7" value="7">
				    <input type="radio" data-name="random" class="radio radio_Rand" value="Случайные">
				    <div class="ball"></div>
				  </div>
				<div class="flashCartImg"></div>
			</div>
		</div>
		`);
	table.appendChild(cart);
	selectNumberOfEquations(cart);
	
	const countRadios = document.querySelectorAll('.counts .radio');
	const ballcount = document.querySelector('.counts .ball');
	let digitNumber = document.querySelector('.counts .radio.active');
	let choseImg = document.querySelector('.flashCartImg');
	choseImg.innerHTML = `<div class="chooseRozr chooseRozr_2"></div>`;
	
	// =====================================================================
	countRadios.forEach((radio, index) => {
		radio.addEventListener('click', function (e) {
			digitNumber.classList.toggle('active');
			radio.classList.toggle('active');
			digitNumber = radio;
			ballcount.className = `ball pos_${index}`;
			const z = e.target.getAttribute('data-name');
			choseImg.innerHTML = `<div class="chooseRozr chooseRozr_${z}"></div>`;
		});
	});
// =====================================================================
	
	// // создаем кнопку "поехали"
	const buttonGO = createHtmlElement(`<input id="buttonGO" type="button" value="поехали!">`);
	main.appendChild(buttonGO);
	buttonGO.addEventListener('click', (e) => {
		
		const equationCounts = +document.querySelector('.equationCounts .radio.active').value;
		let bitNumber = +digitNumber.getAttribute('data-name');
		
		let flashCart = new RunFlashCart({
			arrLength: equationCounts,
			bitNumber: bitNumber,
			allTrenazer: true
		});
		
		if (isNaN(flashCart.bitNumber)) {
			flashCart.bitNumber = Math.floor(Math.random() * 7) + 1;
			flashCart.bool = true;
		}
		
		flashCart.startFlashCart(flashCart.bitNumber, false);
		console.log(flashCart);
		buttonGO.remove();
	});
}

//================== ментальная арифметика ==================
export function mentalArithmeticFunc() {
	const titleName = `Сложение и вычитание`;
	document.querySelector('.title').innerHTML = titleName;
	document.querySelector('#main .title').style.fontSize = '42px';
	const main = document.querySelector('#main'),
		table = document.querySelector('#app_simulator');
	table.innerHTML = null;
	
	const cart = createHtmlElement(`
		<div class="mentalArithmeticChoose">
			<div class="digitNumber">
				<h4 class="text">Выбирете разрядность чисел: </h4>
				  <div class="radios">
				    <input type="radio" data-name="1" class="radio radio_1 active" value="Однозначные">
				    <input type="radio" data-name="mixed" class="radio radio_2" value="Смешанные">
				    <input type="radio" data-name="2" class="radio radio_3" value="Двухзначные">
				    <div class="ball"></div>
				  </div>
			</div>
			<div class="numberOperations">
				<h4 class="text">Выберите количество операций в одном уравнении:  </h4>
				  <div class="radios">
				    <input type="radio" data-name="1" class="radio radio_1  active" value="1">
				    <input type="radio" data-name="2" class="radio radio_2" value="2">
				    <input type="radio" data-name="3" class="radio radio_3" value="3">
				    <input type="radio" data-name="4" class="radio radio_4" value="4">
				    <input type="radio" data-name="5" class="radio radio_5" value="5">
				    <input type="radio" data-name="6" class="radio radio_6" value="6">
				    <input type="radio" data-name="7" class="radio radio_7" value="7">
				    <input type="radio" data-name="8" class="radio radio_8" value="8">
				    <input type="radio" data-name="9" class="radio radio_9" value="9">
				    <input type="radio" data-name="10" class="radio radio_10" value="10">
				    <div class="ball"></div>
				  </div>
			</div>
			<div class="amountOfTime">
				<h4 class="text">Выберите время для одной операций:  </h4>
				  <div class="radios">
				    <input type="radio" data-name="1" class="radio radio_1  active" value="1">
				    <input type="radio" data-name="2" class="radio radio_2" value="2">
				    <input type="radio" data-name="3" class="radio radio_3" value="3">
				    <input type="radio" data-name="4" class="radio radio_4" value="4">
				    <input type="radio" data-name="5" class="radio radio_5" value="5">
				    <input type="radio" data-name="6" class="radio radio_6" value="6">
				    <input type="radio" data-name="7" class="radio radio_7" value="7">
				    <input type="radio" data-name="8" class="radio radio_8" value="8">
				    <input type="radio" data-name="9" class="radio radio_9" value="9">
				    <input type="radio" data-name="10" class="radio radio_10" value="10">
				    <div class="ball"></div>
				  </div>
			</div>
		</div>
		`);
	table.appendChild(cart);
	selectNumberOfEquations(cart);
	
	const digitNumber = document.querySelectorAll('.digitNumber .radio');
	let digitNumberActive = document.querySelector('.digitNumber .radio.active');
	const ballDigitNumber = document.querySelector('.digitNumber .ball');
	
	const numberOperations = document.querySelectorAll('.numberOperations .radio');
	let numberOperationsActive = document.querySelector('.numberOperations .radio.active');
	const ballNumberOperations = document.querySelector('.numberOperations .ball');
	
	const amountOfTime = document.querySelectorAll('.amountOfTime .radio');
	let amountOfTimeActive = document.querySelector('.amountOfTime .radio.active');
	const ballAmountOfTime = document.querySelector('.amountOfTime .ball');
	
	addClassActive(digitNumber, digitNumberActive, ballDigitNumber);
	addClassActive(numberOperations, numberOperationsActive, ballNumberOperations);
	addClassActive(amountOfTime, amountOfTimeActive, ballAmountOfTime);
// =====================================================================
	
	// // создаем кнопку "поехали"
	const buttonGO = createHtmlElement(`<input id="buttonGO" type="button" value="поехали!">`);
	main.appendChild(buttonGO);
	buttonGO.addEventListener('click', (e) => {
		
		
		const time = +document.querySelector('.amountOfTime .radio.active').getAttribute('data-name');
		const digitNumber = document.querySelector('.digitNumber .radio.active').getAttribute('data-name');
		const numberOperations = +document.querySelector('.numberOperations .radio.active').getAttribute('data-name');
		const equationCounts = +document.querySelector('.equationCounts .radio.active').getAttribute('value');
		
		let mental = new Mental({
			column: equationCounts,
			step: 1,
			digitCapacity: digitNumber,
			row: numberOperations,
			digit: false,
			titleName,
			time: time * 1000
		});
		mental.startMental();
		console.log(mental);
		buttonGO.remove();
	});
}

//================== умножения ==================
export function multiplicationsFunc() {
	document.querySelector('.title').innerHTML = `Умножение`;
	const main = document.querySelector('#main'),
		table = document.querySelector('#app_simulator');
	
	table.innerHTML = null;
	
	const cart = createHtmlElement(`
			<div class="multiplicationsChoose">
			<div class="counts">
				<h4 class="text">Выбирете цифры: </h4>
				 <div class="radios">
				   <input type="radio" data-r1="2" data-r2="1" class="radio radio_1" value="2x1">
				   <input type="radio" data-r1="2" data-r2="2" class="radio radio_2 active" value="2x2">
				   <input type="radio" data-r1="2" data-r2="3" class="radio radio_3" value="2x3">
					<input type="radio" data-r1="2" data-r2="4" class="radio radio_4" value="2x4">
				   <input type="radio" data-r1="3" data-r2="1" class="radio radio_5" value="3x1">
				   <input type="radio" data-r1="3" data-r2="2" class="radio radio_6" value="3x2">
				   <input type="radio" data-r1="3" data-r2="3" class="radio radio_7" value="3x3">
				   <input type="radio" data-r1="4" data-r2="1" class="radio radio_8" value="4x1">
					<input type="radio" data-r1="4" data-r2="2" class="radio radio_9" value="4x2">
				   <input type="radio" data-r1="4" data-r2="3" class="radio radio_10" value="4x3">
				   <input type="radio" data-r1="5" data-r2="2" class="radio radio_11" value="5x2">
				   <div class="ball"></div>
				 </div>
			</div>
		</div>
		`);
	
	table.appendChild(cart);
	selectNumberOfEquations(cart);
	
	const countRadios = document.querySelectorAll('.counts .radio');
	const ballCount = document.querySelector('.counts .ball');
	let digitNumber = document.querySelector('.counts .radio.active');
// =====================================================================
	addClassActive(countRadios, digitNumber, ballCount);
// =====================================================================
	
	// создаем кнопку "поехали"
	const buttonGO = createHtmlElement(`<input id="buttonGO" type="button" value="поехали!">`);
	main.appendChild(buttonGO);
	buttonGO.addEventListener('click', (e) => {
		const numberOfEquations = document.querySelector('.equationCounts .radio.active');
		const firstCount = document.querySelector('.counts .radio.active').getAttribute('data-r1');
		const secondCount = document.querySelector('.counts .radio.active').getAttribute('data-r2');
		let umn = new Umnozheniye({
			M: +numberOfEquations.value,
			r1: +firstCount,
			r2: +secondCount,
			titleName: true
		});
		
		umn.startUmnozheniye();
		console.log(umn);
		buttonGO.remove();
	});
}

//================== деление ==================
export function divisionFunc() {
	document.querySelector('.title').innerHTML = `Деление`;
	const main = document.querySelector('#main'),
		table = document.querySelector('#app_simulator');
	
	table.innerHTML = null;
	
	const cart = createHtmlElement(`
			<div class="divisionChoose">
			<div class="counts">
				<h4 class="text">Выбирете цифры: </h4>
				 <div class="radios">
				   <input type="radio" data-r1="2" data-r2="1" class="radio radio_1" value="2:1">
				   <input type="radio" data-r1="2" data-r2="2" class="radio radio_2 active" value="2:2">
				   <input type="radio" data-r1="3" data-r2="1" class="radio radio_3" value="3:1">
					<input type="radio" data-r1="3" data-r2="2" class="radio radio_4" value="3:2">
				   <input type="radio" data-r1="4" data-r2="1" class="radio radio_5" value="4:1">
				   <input type="radio" data-r1="4" data-r2="2" class="radio radio_6" value="4:2">
				   <input type="radio" data-r1="5" data-r2="2" class="radio radio_7" value="5:2">
				   <input type="radio" data-r1="5" data-r2="3" class="radio radio_8" value="5:3">
					<input type="radio" data-r1="6" data-r2="2" class="radio radio_9" value="6:2">
				   <input type="radio" data-r1="6" data-r2="3" class="radio radio_10" value="6:3">
				   <div class="ball"></div>
				 </div>
			</div>
		</div>
		`);
	
	table.appendChild(cart);
	selectNumberOfEquations(cart);
	
	const countRadios = document.querySelectorAll('.counts .radio');
	const ballCount = document.querySelector('.counts .ball');
	let digitNumber = document.querySelector('.counts .radio.active');
// =====================================================================
	addClassActive(countRadios, digitNumber, ballCount);
// =====================================================================
	
	// создаем кнопку "поехали"
	const buttonGO = createHtmlElement(`<input id="buttonGO" type="button" value="поехали!">`);
	main.appendChild(buttonGO);
	buttonGO.addEventListener('click', (e) => {
		const numberOfEquations = document.querySelector('.equationCounts .radio.active');
		const firstCount = document.querySelector('.counts .radio.active').getAttribute('data-r1');
		const secondCount = document.querySelector('.counts .radio.active').getAttribute('data-r2');
		let del = new Deleniye({
			M: +numberOfEquations.value,
			R: +firstCount,
			r1: +secondCount,
			titleName: true
		});
		
		del.startDeleniye();
		console.log(del);
		buttonGO.remove();
	});
}

//================== дроби ==================
export function fractionsFunc() {
	
	const titleName = `дроби`;
	document.querySelector('.title').innerHTML = titleName;
	const main = document.querySelector('#main'),
		table = document.querySelector('#app_simulator');
	table.innerHTML = null;
	
	const cart = createHtmlElement(`
		<div class="fractionsChoose">
			<div class="digitNumber">
				<h4 class="text">Выбирете разрядность чисел: </h4>
				  <div class="radios">
				    <input type="radio" data-name="dr_1" class="radio radio_1 active" value="1">
				    <input type="radio" data-name="dr_1_2" class="radio radio_2" value="1-2">
				    <input type="radio" data-name="dr_2" class="radio radio_3" value="2">
				    <input type="radio" data-name="dr_2_3" class="radio radio_4" value="2-3">
				    <div class="ball"></div>
				  </div>
			</div>
			<div class="numberOperations">
				<h4 class="text">Выберите количество операций в одном уравнении:  </h4>
				  <div class="radios">
				    <input type="radio" data-name="1" class="radio radio_1  active" value="1">
				    <input type="radio" data-name="2" class="radio radio_2" value="2">
				    <input type="radio" data-name="3" class="radio radio_3" value="3">
				    <input type="radio" data-name="4" class="radio radio_4" value="4">
				    <input type="radio" data-name="5" class="radio radio_5" value="5">
				    <input type="radio" data-name="6" class="radio radio_6" value="6">
				    <input type="radio" data-name="7" class="radio radio_7" value="7">
				    <input type="radio" data-name="8" class="radio radio_8" value="8">
				    <input type="radio" data-name="9" class="radio radio_9" value="9">
				    <input type="radio" data-name="10" class="radio radio_10" value="10">
				    <div class="ball"></div>
				  </div>
			</div>
			<div class="amountOfTime">
				<h4 class="text">Выберите время для одной операций:  </h4>
				  <div class="radios">
				    <input type="radio" data-name="1" class="radio radio_1  active" value="1">
				    <input type="radio" data-name="2" class="radio radio_2" value="2">
				    <input type="radio" data-name="3" class="radio radio_3" value="3">
				    <input type="radio" data-name="4" class="radio radio_4" value="4">
				    <input type="radio" data-name="5" class="radio radio_5" value="5">
				    <input type="radio" data-name="6" class="radio radio_6" value="6">
				    <input type="radio" data-name="7" class="radio radio_7" value="7">
				    <input type="radio" data-name="8" class="radio radio_8" value="8">
				    <input type="radio" data-name="9" class="radio radio_9" value="9">
				    <input type="radio" data-name="10" class="radio radio_10" value="10">
				    <div class="ball"></div>
				  </div>
			</div>
		</div>
		`);
	table.appendChild(cart);
	selectNumberOfEquations(cart);
	
	const digitNumber = document.querySelectorAll('.digitNumber .radio');
	const digitNumberActive = document.querySelector('.digitNumber .radio.active');
	const ballDigitNumber = document.querySelector('.digitNumber .ball');
	
	const numberOperations = document.querySelectorAll('.numberOperations .radio');
	const numberOperationsActive = document.querySelector('.numberOperations .radio.active');
	const ballNumberOperations = document.querySelector('.numberOperations .ball');
	
	const amountOfTime = document.querySelectorAll('.amountOfTime .radio');
	const amountOfTimeActive = document.querySelector('.amountOfTime .radio.active');
	const ballAmountOfTime = document.querySelector('.amountOfTime .ball');
	
	addClassActive(digitNumber, digitNumberActive, ballDigitNumber);
	addClassActive(numberOperations, numberOperationsActive, ballNumberOperations);
	addClassActive(amountOfTime, amountOfTimeActive, ballAmountOfTime);
// =====================================================================
	
	// // создаем кнопку "поехали"
	const buttonGO = createHtmlElement(`<input id="buttonGO" type="button" value="поехали!">`);
	main.appendChild(buttonGO);
	buttonGO.addEventListener('click', (e) => {
		
		const time = +document.querySelector('.amountOfTime .radio.active').getAttribute('data-name');
		const digitNumber = document.querySelector('.digitNumber .radio.active').getAttribute('data-name');
		const numberOperations = +document.querySelector('.numberOperations .radio.active').getAttribute('data-name');
		const equationCounts = +document.querySelector('.equationCounts .radio.active').getAttribute('value');
		
		let mental = new Mental({
			column: equationCounts,
			step: 1,
			digitCapacity: digitNumber,
			row: numberOperations,
			digit: false,
			titleName,
			time: time * 1000
		});
		mental.startMental();
		console.log(mental);
		buttonGO.remove();
	});
}

// =====================================================================
function selectNumberOfEquations(table) {
	const line = createHtmlElement(`
		<div class="equationCounts">
			<h4 class="text">Выбирете количество уравнений:</h4>
			 <div class="radios">
			    <input type="radio" name="radio" class="radio" value="5">
			    <input type="radio" name="radio" class="radio active" value="10">
			    <input type="radio" name="radio" class="radio" value="15">
			    <input type="radio" name="radio" class="radio" value="20">
			    <div class="ball"></div>
			 </div>
		</div>
	`);
	table.appendChild(line);
	
	const equationCountsRadios = document.querySelectorAll('.equationCounts .radio');
	let numberOfEquations = document.querySelector('.equationCounts .radio.active');
	const ballEquationCounts = document.querySelector('.equationCounts .ball');
	addClassActive(equationCountsRadios, numberOfEquations, ballEquationCounts);
}

const addClassActive = (arrRadio, radioActive, ball) => {
	arrRadio.forEach((radio, index) => {
		radio.addEventListener('click', function (e) {
			if (radioActive) radioActive.classList.toggle('active');
			radio.classList.toggle('active');
			radioActive = radio;
			ball.className = `ball pos_${index}`;
		});
	});
};
