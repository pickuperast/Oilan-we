import Abacus                          from "./Abacus";
import {sound}                         from "../other/sound";
import {createHtmlElement, createStar} from "../game";
import {image}                         from "../other/image";

const audio_Au_t_1 = new Audio(sound.trenazhor.Au_t_1);
const audio_Au_t_2 = new Audio(sound.trenazhor.Au_t_2);
const audio_Au_t_3 = new Audio(sound.trenazhor.Au_t_3);
const au_AA = new Audio(sound.theory.au_AA);
// =======================================================================================================================


export default class RunAbacus {
	
	constructor(op) {
		this.column = op.column;
		this.row = op.row;
		this.digit = op.digit;
		this.firstCount = op.firstCount;
		this.lastCount = op.lastCount;
		this.firstAnswer = true; // получение звезди за правельній ответ с первого раза
		this.level = op.level;
		this.step = op.step;
	}
	
	startAbacus() {
		try {
			const data = this.choose(this.level, this.step); // отримання левела і степа
			this.firstAnswer = true;
			this.createTableAbacus(data.countsArr); //отрисовка таблици
			this.check(data.sumArr); //проверяем уравнение на правильность
		} catch (e) {
			console.log(e);
		}
	}
	
	createTableAbacus(tableData) {
		document.querySelector('.title').innerHTML = `Аbacus-арифметика`;
		const table = document.querySelector('#app_simulator');
		table.innerHTML = null;
		const main = document.querySelector('#main');
		// audio_Au_t_1.play();
		tableData.forEach((item) => {
			
			const row = createHtmlElement(`<div class="column"></div>`);
			const cell = createHtmlElement(`<div class="inp"></div>`);
			const input = createHtmlElement(`<input class="inp" type="number"/>`);
			
			item.forEach((cellData) => {
				const cell = createHtmlElement(`<div class="column__count"></div>`);
				cell.appendChild(document.createTextNode(cellData));
				row.appendChild(cell);
			});
			table.appendChild(row);
			row.appendChild(cell);
			cell.appendChild(input);
		});
		
		//добавляем класи для изменения шрифтов
		const changeFont = document.querySelectorAll('.column');
		switch (this.level) {
			case 2:
			case 3:
			case 4:
			case 5:
			case 6: {
				changeFont.forEach((item) => {
					item.classList.add('middleFont');
				});
			}
				break;
			case 7:
			case 8:
			case 9:
			case 10: {
				changeFont.forEach((item) => {
					item.classList.add('smallFont');
				});
			}
				break;
		}
		
		// создаем кнопку "ПРОВЕРИТЬ"
		if (!document.querySelector('#button')) {
			const button = createHtmlElement(`
				<input id="button" type="button" value="ПРОВЕРИТЬ">`);
			main.appendChild(button);
		}
	}
	
	choose(level, step) {
		
		let data;
		if (level === 1) {
			switch (step) {
				case 1:
					data = Abacus.prototype.getAbacusSimple(this.column);
					break;
				case 2:
					data = Abacus.prototype.getAbacusSimple(this.column);
					break;
				case 3:
					data = Abacus.prototype.getAbacusSimpleStep_3(this.column);
					break;
				case 4:
					data = Abacus.prototype.getAbacusSimpleStep_4(this.column);
					break;
				case 5:
					data = Abacus.prototype.getAbacusSimpleStep_5_6(this.column);
					break;
				case 6:
					data = Abacus.prototype.getAbacusSimpleStep_5_6(this.column);
					break;
				case 7:
					data = Abacus.prototype.getAbacusSimpleStep_7_8(this.column);
					break;
				case 8:
					data = Abacus.prototype.getAbacusSimpleStep_7_8(this.column);
					break;
				case 9:
					data = Abacus.prototype.getAbacusSimpleStep_9(this.column, this.row, this.firstCount, this.lastCount, this.digit);
					break;
				default:
					console.log('Я таких значений не знаю в 1 левеле');
			}
		}
		else {
			switch (level) {
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
					data = Abacus.prototype.getAbacusSimpleStep_9(this.column, this.row, this.firstCount, this.lastCount, this.digit);
					break;
				case 7:
				case 9:
					data = Abacus.prototype.getAbacusSimpleDoubleOne(this.column, this.row, this.firstCount, this.lastCount);
					break;
				case 8:
				case 10:
					data = Abacus.prototype.getAbacusSimpleDouble(this.column, this.row, this.firstCount, this.lastCount, this.digit);
					break;
				default:
					console.log('Я таких значений не знаю');
			}
		}
		return data;
	}
	
	check(sumArr) {
		const table = document.querySelector('#app_simulator'),
			inp = document.querySelectorAll('.column .inp input'),
			checkAnswer = document.querySelector('#button'),
			arrTypedAnswers = []; // массив ответов
		let stars = document.querySelector('.stars');
		
		checkAnswer.addEventListener('click', () => {
			// audio_Au_t_2.play();
			let loser = 0;
			// проверка на ответ
			for (let i = 0; i < inp.length; i++) {
				if (!inp[i].value) {
					inp[i].style.backgroundColor = '#eb6969';
				}
				
				if (+sumArr[i] === +inp[i].value && inp[i].value !== '') {
					arrTypedAnswers[i] = +inp[i].value;
					const column = inp[i].parentNode.parentNode;
					
					if (this.firstAnswer) {
						// const star = createHtmlElement(`<img class="star_img" src="${image.honorStar.starPng}">`);
						// column.appendChild(star);
						const starCount = stars.innerHTML++;
						createStar(column, starCount + 1);
					}
					
					inp[i].style.backgroundColor = '#94ec5a';
					setTimeout(() => {
						column.remove();
					}, 1000);
				}
				else {
					this.firstAnswer = false;
					inp[i].style.backgroundColor = '#eb6969';
					inp[i].style.color = 'white';
					inp[i].value = '';
					loser++;
				}
			}
			// удаляем пустие елементи массива
			let filtered = arrTypedAnswers.filter(function (el) {
				return el != null;
			});
			
			//======== проверка степа ===================
			if (filtered.length === inp.length) {
				setTimeout(() => {
					table.innerHTML = null;
					console.log(`You are the best!!!`);
					// show modal window
					this.openModal(stars.innerHTML);
				}, 1000);
			}
		});
		return false;
	}
	
	openModal(res) {
		const button = document.querySelector('#button');
		const modalText = document.querySelector('.modal__text'),
			countAll = document.querySelector(".stars"),
			modal = document.querySelector('.modal'),
			modalBtn = document.querySelector('.modal__buttons'),
			repeatBtn = document.querySelector('.repeatButton');
		modal.style.display = 'flex';
		button.remove();
		
		if (res < 0) {
			res = 0;
		}
		let starWord = 'звезд';
		if (res === 1) {
			starWord = `звезду`;
		}
		else if (res <= 4 && res >= 2) {
			starWord = `звезды`;
		}
		modalText.innerText =
			`Ты набрал ${res} ${starWord}!`;
		countAll.innerHTML = res;
		
		if (!repeatBtn) {
			
			const repeatBtn = createHtmlElement(`<input class="button repeatButton"
															value="Повторить"
															type="submit"/>`);
			modalBtn.appendChild(repeatBtn);
			
			repeatBtn.addEventListener('click', () => {
				modal.style.display = 'none';
				repeatBtn.remove();
				this.startAbacus();
			});
		}
		else {
			repeatBtn.addEventListener('click', () => {
				modal.style.display = 'none';
				repeatBtn.remove();
				this.startAbacus();
			});
		}
	}
}
