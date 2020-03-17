import {image}             from "./other/image";
import anime               from "animejs";
import RunFlashCart        from "./flesh_cart/RunFlashCart"
import RunAbacus           from "./abacus/RunAbacus";
import Mental              from "./mental/Mental"
import Umnozheniye         from "./umnozheniye/Umnozheniye"
import Deleniye            from "./deleniye/Deleniye";
import {
	divisionFunc,
	flashCartFunc, fractionsFunc,
	mentalArithmeticFunc,
	multiplicationsFunc,
	multiplicationTableFunc
} from "./common/Common";

// export function showModalWindow(res, text) {
// 	const modalText = document.querySelector('.modal__text'),
// 		countAll = document.querySelector(".stars"),
// 		modal = document.querySelector('.modal');
//
// 	modal.style.display = 'flex';
//
// 	function resData(res) {
// 		if (res < 0) {
// 			res = 0;
// 		}
// 		if (res === 1) {
// 			return `звезду`;
// 		}
// 		else if (res <= 4 && res >= 2) {
// 			return `звезды`;
// 		}
// 		return 'звезд';
// 	}
//
// 	modalText.innerText = `${text} Ты набрал ${res} ${resData(res)}!`;
// 	countAll.innerHTML = res;
//
// }

export const createHtmlElement = (str) => {
	const el = document.createElement('div');
	el.innerHTML = str;
	return el.firstElementChild;
};

export function createStar(teg, starCount) {
	//create star
	const stars = document.querySelector('.stars');
	const star = createHtmlElement(`<img class="star_imgFlash" src="${image.honorStar.starPng}">`);
	teg.appendChild(star);
	
	const getCoords = (elem) => {
		let box = elem.getBoundingClientRect();
		return {
			top: box.top + pageYOffset,
			left: box.left + pageXOffset
		};
	};
	// move star
	const tablo = document.querySelector("#tablo img");
	const x = getCoords(tablo).left - getCoords(star).left - 40;
	const y = getCoords(tablo).top - getCoords(star).top - 40;
	
	anime({
		targets: '.star_imgFlash',
		translateX: x,
		translateY: y,
		easing: 'easeInOutQuad',
		opacity: .5,
		scale: 1.5,
		rotate: '2turn'
	});
	setTimeout(() => {
		stars.innerHTML = starCount;
		star.remove();
	}, 1000);
}

export function rozryad(r1, r2) {
	let count1 = 0;
	let count1Last = 0;
	let count2Last = 0;
	let count2 = 0;
	switch (r1) {
		case 1:
			count1 = 9;
			count1Last = 1;
			break;
		case 2:
			count1 = 89;
			count1Last = 10;
			break;
		case 3:
			count1 = 899;
			count1Last = 100;
			break;
		case 4:
			count1 = 8999;
			count1Last = 1000;
			break;
		case 5:
			count1 = 89999;
			count1Last = 10000;
			break;
		case 6:
			count1 = 899999;
			count1Last = 100000;
			break;
		default:
			console.log('r1 hz no no no');
	}
	switch (r2) {
		case 1:
			count2 = 9;
			count2Last = 1;
			break;
		case 2:
			count2 = 89;
			count2Last = 10;
			break;
		case 3:
			count2 = 899;
			count2Last = 100;
			break;
		case 4:
			count2 = 8999;
			count2Last = 1000;
			break;
		default:
			console.log('r2 hz no no no');
	}
	return {
		count1,
		count1Last,
		count2Last,
		count2
	};
}

const start = document.querySelector('#form input[type=submit]');
const common = document.querySelector('#common-button');

// откривает игру на весь екран
const openFullscreen = () => {
	const main = document.querySelector('html');
	if (main.requestFullscreen) {
		main.requestFullscreen();
	} else if (main.mozRequestFullScreen) { /* Firefox */
		main.mozRequestFullScreen();
	} else if (main.webkitRequestFullscreen) { /* Chrome, Safari & Opera */
		main.webkitRequestFullscreen();
	} else if (main.msRequestFullscreen) { /* IE/Edge */
		main.msRequestFullscreen();
	}
};

start.addEventListener('click', function (e) {
	e.preventDefault();
	// openFullscreen();
	const step = +document.querySelector('select[name=step]').value;
	const level = +document.querySelector('select[name=level]').value;
	const trenazhor = document.querySelector('select[name=trenazhor]').value;
	startGame(trenazhor, level, step);
});

function startGame(trenazhor, level, step) {
	let data;
	let main = document.querySelector('#app_simulator');
	main.innerHTML = null;
	// ====================== flashCart ==========================
	if (trenazhor === 'fleshCart') {
		let fleshCart = new RunFlashCart({
			arrLength: 10,
			bool: false
		});
		if (level === 1) {
			switch (step) {
				case 1:
				case 2:
				case 3:
					fleshCart.bitNumber = 1;
					break;
				case 4:
					fleshCart.bitNumber = 2;
					break;
				case 5:
				case 6:
					fleshCart.bitNumber = 2;
					fleshCart.bool = true;
					break;
				case 7:
					fleshCart.bitNumber = 2;
					break;
				case 8:
				case 9:
					fleshCart.bitNumber = 3;
					fleshCart.bool = true;
					break;
				default:
					'level_1 doesn`t work'
			}
		}
		else if (level === 2) {
			switch (step) {
				case 1:
				case 2:
					fleshCart.bitNumber = 4;
					break;
				case 3:
					fleshCart.bitNumber = 5;
					break;
				case 4:
					fleshCart.bitNumber = 5;
					fleshCart.bool = true;
					break;
				case 5:
					fleshCart.bitNumber = 6;
					break;
				case 6:
					fleshCart.bitNumber = 6;
					fleshCart.bool = true;
					break;
				case 7:
					fleshCart.bitNumber = 7;
					break;
				case 8:
				case 9:
					fleshCart.bitNumber = 7;
					fleshCart.bool = true;
					break;
				default:
					'level_2 doesn`t work'
			}
		}
		fleshCart.startFlashCart();
		console.log(fleshCart);
	}
	
	// ====================== mental ==========================
	else if (trenazhor === 'mental') {
		let mental = new Mental({
			column: 10,
			step: 1,
			row: 1,
			digit: true,
			level,
		});
		
		switch (level) {
			case 3:
				mental.row = 3;
				mental.digit = false;
				break;
			case 4:
				mental.row = 5;
				break;
			case 5:
				mental.row = 6;
				break;
			case 6:
				mental.row = 8;
				break;
			case 7:
				mental.row = 8;
				break;
			case 8:
				mental.row = 10;
				break;
			case 9:
				mental.row = 6;
				mental.digit = false;
				break;
			case 10:
				mental.row = 10;
				break;
			default:
				document.querySelector('#app_simulator').innerHTML = 'hello'
		}
		
		mental.step = step;
		mental.startMental();
		console.log(mental);
	}
	
	// ====================== abacus ==========================
	else if (trenazhor === 'abacus') {
		let abacus = new RunAbacus({
			column: 10,
			firstCount: 1,
			lastCount: 9,
			digit: false
		});
		if (level === 1) {
			abacus.row = 3;
		}
		else {
			switch (level) {
				case 2:
					abacus.row = 3;
					break;
				case 3:
					abacus.row = 7;
					break;
				case 4:
					abacus.row = 8;
					abacus.digit = true;
					break;
				case 5:
					abacus.row = 10;
					abacus.firstCount = 10;
					abacus.lastCount = 89;
					break;
				case 6:
					abacus.row = 10;
					abacus.firstCount = 10;
					abacus.lastCount = 89;
					break;
				case 7:
					abacus.row = 10;
					abacus.firstCount = 0.01;
					abacus.lastCount = 9;
					break;
				case 8:
					abacus.row = 10;
					abacus.firstCount = 0.01;
					abacus.lastCount = 9;
					abacus.digit = 99;
					break;
				case 9:
					abacus.row = 10;
					abacus.firstCount = 10;
					abacus.lastCount = 99.99;
					break;
				case 10:
					abacus.row = 10;
					abacus.firstCount = 10;
					abacus.lastCount = 999;
					abacus.digit = 99;
					break;
			}
		}
		abacus.level = level;
		abacus.step = step;
		abacus.startAbacus();
		console.log(abacus);
	}
	
	// ==================== умножение ========================
	else if (trenazhor === 'umnozhenye') {
		let umn = new Umnozheniye({
			M: 10,
			r1: 2,
			r2: 1
		});
		if (level === 4 && step > 2) {
			umn.r1 = 2;
		}
		else if (level === 5) {
			umn.r1 = 3;
		}
		else if (level === 6) {
			umn.r1 = 4;
		}
		else if (level === 7) {
			if (step < 3) {
				umn.r1 = 4;
				umn.r2 = 1;
			}
			else {
				umn.r1 = 2;
				umn.r2 = 2;
			}
		}
		else if (level === 8) {
			if (step === 1) {
				umn.r1 = 3;
				umn.r2 = 2;
			}
			else if (step === 2) {
				umn.r1 = 2;
				umn.r2 = 3;
			}
			else {
				umn.change = true;
			}
		}
		else if (level === 9) {
			if (step === 1) {
				umn.r1 = 2;
				umn.r2 = 4;
			}
			else if (step === 2) {
				umn.r1 = 3;
				umn.r2 = 3;
			}
			else if (step === 3) {
				umn.r1 = 4;
				umn.r2 = 2;
			}
			else {
				umn.change = true;
			}
		}
		else if (level === 10) {
			if (step === 1) {
				umn.r1 = 4;
				umn.r2 = 3;
			}
			else if (step === 2) {
				umn.r1 = 5;
				umn.r2 = 2;
			}
			else {
				umn.change = true;
			}
		}
		else {
			let tab = createHtmlElement(`
						<div class="deleniye__count">
							choose level or step
						</div>
				`);
			main.appendChild(tab);
			return true;
		}
		umn.level = level;
		umn.startUmnozheniye();
		console.log(umn);
	}
	
	// ==================== деление ========================
	else if (trenazhor === 'deleniye') {
		let del = new Deleniye({
			M: 10,
			level: level,
			step: step,
			digit: false
		});
		if (level === 4 && step > 5) {
			del.R = 2;
			del.r1 = 1;
		}
		else if (level === 5) {
			if (step > 2) {
				del.R = 3;
				del.r1 = 1;
			}
			else {
				del.R = 2;
				del.r1 = 1;
			}
		}
		else if (level === 6) {
			if (step > 3) {
				del.R = 4;
				del.r1 = 1;
			}
			else {
				del.R = 3;
				del.r1 = 1;
			}
		}
		else if (level === 7) {
			if (step >= 5) {
				del.R = 2;
				del.r1 = 2;
			}
			else {
				del.R = 4;
				del.r1 = 1;
			}
		}
		else if (level === 8) {
			if (step < 4) {
				del.R = 2;
				del.r1 = 2;
			}
			else if (step === 4) {
				del.R = 3;
				del.r1 = 2;
			}
			else if (step === 5) {
				del.R = 4;
				del.r1 = 2;
			}
			else {
				del.digit = true;
				del.a1 = 3;
				del.a2 = 2;
				del.b1 = 4;
				del.b2 = 2;
			}
		}
		else if (level === 9) {
			if (step < 4) {
				del.digit = true;
				del.a1 = 3;
				del.a2 = 2;
				del.b1 = 4;
				del.b2 = 2;
			}
			else if (step === 4) {
				del.R = 5;
				del.r1 = 2;
			}
			else if (step === 5) {
				del.R = 5;
				del.r1 = 3;
			}
			else {
				del.digit = true;
				del.a1 = 5;
				del.a2 = 3;
				del.b1 = 5;
				del.b2 = 2;
			}
		}
		else if (level === 10) {
			if (step < 4) {
				del.digit = true;
				del.a1 = 5;
				del.a2 = 3;
				del.b1 = 5;
				del.b2 = 2;
			}
			else if (step === 4) {
				del.R = 6;
				del.r1 = 2;
			}
			else if (step === 5) {
				del.R = 6;
				del.r1 = 3;
			}
			else {
				del.digit = true;
				del.a1 = 6;
				del.a2 = 2;
				del.b1 = 6;
				del.b2 = 3;
			}
		}
		else {
			let tab = createHtmlElement(`
						<div class="deleniye__count">
							choose level or step
						</div>
				`);
			main.appendChild(tab);
			return true;
		}
		del.startDeleniye();
		console.log(del);
	}
}

export function showAllTrenazer() {
	document.querySelector('.title').innerHTML = 'Общий тренажер';
	const table = document.querySelector('#app_simulator');
	table.innerHTML = null;
	
	const carts = createHtmlElement(`<ul class="common">
				<li class="multiplicationTable" data-title="Таблица умножения"></li>
				<li class="flashCart" data-title="Флеш карты"></li>
				<li class="mentalArithmetic" data-title="Сложение и вычитание"></li>
				<li class="multiplications" data-title="Умножение"></li>
				<li class="division" data-title="Деление"></li>
				<li class="fractions" data-title="Дроби"></li>
	</ul>`);
	table.appendChild(carts);
}

export function choseTrenazer() {
	let cartsElement = document.querySelectorAll('.common li');
	
	cartsElement.forEach(function (e) {
		e.addEventListener('click', function () {
			switch (e.className) {
				case 'multiplicationTable':
					multiplicationTableFunc();
					break;
				case 'flashCart':
					flashCartFunc();
					break;
				case 'mentalArithmetic':
					mentalArithmeticFunc();
					break;
				case 'multiplications':
					multiplicationsFunc();
					break;
				case 'division':
					divisionFunc();
					break;
				case 'fractions':
					fractionsFunc();
					break;
				default:
					console.log('i dont no');
			}
		})
	})
}

common.addEventListener('click', function (e) {
	e.preventDefault();
	showAllTrenazer();
	choseTrenazer();
	//удаляем кнопку "проверить"
	if (document.querySelector('#button')) {
		document.querySelector('#button').remove();
	}
	//удаляем кнопку "поехали!"
	if (document.querySelector('#buttonGO')) {
		document.querySelector('#buttonGO').remove();
	}
});

