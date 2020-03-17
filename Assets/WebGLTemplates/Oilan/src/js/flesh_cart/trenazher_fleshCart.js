import FleshCart                       from "./FleshCart";
import {sound}                         from "../other/sound";
import {createStar, createHtmlElement} from "../game";

// ======================================================================
const audio_Au_t_3 = new Audio(sound.trenazhor.Au_t_3);
const audio_Au_t_4 = new Audio(sound.trenazhor.Au_t_4);
const audio_Au_t_5 = new Audio(sound.trenazhor.Au_t_5);
const audio_Au_t_6 = new Audio(sound.trenazhor.Au_t_6);
const audio_Au_t_7 = new Audio(sound.trenazhor.Au_t_7);
const audio_Au_t_8 = new Audio(sound.trenazhor.Au_t_8);
const audio_Au_t_9 = new Audio(sound.trenazhor.Au_t_9);
// ======================================================================

// ======================================================================
export default class RunFlashCart extends FleshCart {
	
	constructor(bitNumber, bool) {
		// bitNumber --- розрядность числа от 1 до 3
		super();
		this.bitNumber = bitNumber;
		this.bool = bool;
		this.firstAnswer = true; // получение звезди за правельній ответ с первого раза
	}
	
	// ======================================================================
	
	startFlashCart() {
		
		const index = 0;
		const dataObj = this.getFleshCart(); // get level and step
		this.createColumn(this.bitNumber, dataObj, index);
	}
	
	createColumn(bitNumber, dataObj, index) {
// ======================================================================
		document.querySelector('.title').innerHTML = `Флеш - карты`;
		
		const row = createHtmlElement(`<div class="column-flash flashLine1"></div>`);
		const row2 = createHtmlElement(`<div class="column-flash"></div>`);
		const row3 = createHtmlElement(`<div class="column-flash"></div>`);
		const row4 = createHtmlElement(`<div class="column-flash"></div>`);
		const row5 = createHtmlElement(`<div class="column-flash"></div>`);
		const row6 = createHtmlElement(`<div class="column-flash"></div>`);
		const row7 = createHtmlElement(`<div class="column-flash"></div>`);
		const divInput = createHtmlElement(`<div class="column-flash__input">
													<input type="number" autofocus/>
												</div>`);
		const table = document.querySelector('#app_simulator');
		table.innerHTML = null;
		const main = document.querySelector('#main');
		
		const createCol = (arr, row) => {
			arr.forEach((stepData) => {
				const cell = createHtmlElement(`
				<div class="column-flash__image">
					<img src=${stepData}>
				</div>`);
				row.appendChild(cell);
			});
			row.appendChild(divInput);
			table.appendChild(row);
		};
		
		const oneColumn = () => {
			createCol(dataObj.countsArr[index], row);
		};
		
		const twoColumn = () => {
			// Последовательность 2,4,8,10 - однозначные, 1,3,5,6,7,9 - двухзначные
			if (this.bool) {
				if (index % 2 !== 0 && index !== 5) {
					createCol(dataObj.countsArr[index], row);
				}
				else {
					row.classList.remove('flashLine1');
					row2.classList.add('flashLine2');
					createCol(dataObj.countsArr[index], row);
					createCol(dataObj.countsArr2[index], row2);
				}
			}
			else {
				row.classList.remove('flashLine1');
				row2.classList.add('flashLine2');
				createCol(dataObj.countsArr[index], row);
				createCol(dataObj.countsArr2[index], row2);
			}
			
		};
		
		const threeColumn = () => {
			//Последовательность можно сделать - 3,7 однозначные, 1,5,9 - двухзначные, 2,4,6,8,10 - трехзначные
			if (this.bool) {
				
				if (index === 2 || index === 6) {
					createCol(dataObj.countsArr[index], row);
				}
				else if (index === 0 || index === 4 || index === 8) {
					row.classList.remove('flashLine1');
					row2.classList.add('flashLine2');
					createCol(dataObj.countsArr[index], row);
					createCol(dataObj.countsArr2[index], row2);
				}
				else {
					row.classList.remove('flashLine1');
					row3.classList.add('flashLine3');
					createCol(dataObj.countsArr[index], row);
					createCol(dataObj.countsArr2[index], row2);
					createCol(dataObj.countsArr3[index], row3);
				}
			}
			else {
				row.classList.remove('flashLine1');
				row3.classList.add('flashLine3');
				createCol(dataObj.countsArr[index], row);
				createCol(dataObj.countsArr2[index], row2);
				createCol(dataObj.countsArr3[index], row3);
			}
		};
		
		const fourColumn = () => {
			// row.classList.remove('flashLine1');
			row4.classList.add('flashLine4');
			createCol(dataObj.countsArr[index], row);
			createCol(dataObj.countsArr2[index], row2);
			createCol(dataObj.countsArr3[index], row3);
			createCol(dataObj.countsArr4[index], row4);
		};
		
		const fiveColumn = () => {
			// Последовательность можно сделать
			// 2,4,8,10 - однозначные, 1,3,5,6,7,9 - двухзначные
			if (this.bool) {
				if (index % 2 !== 0 && index !== 5) {
					// row.classList.remove('flashLine1');
					row4.classList.add('flashLine4');
					createCol(dataObj.countsArr[index], row);
					createCol(dataObj.countsArr2[index], row2);
					createCol(dataObj.countsArr3[index], row3);
					createCol(dataObj.countsArr4[index], row4);
				}
				else {
					row.classList.remove('flashLine1');
					row5.classList.add('flashLine5');
					row2.classList.add('flashLine2');
					createCol(dataObj.countsArr[index], row);
					createCol(dataObj.countsArr2[index], row2);
					createCol(dataObj.countsArr3[index], row3);
					createCol(dataObj.countsArr4[index], row4);
					createCol(dataObj.countsArr5[index], row5);
				}
			}
			else {
				row.classList.remove('flashLine1');
				row5.classList.add('flashLine5');
				row2.classList.add('flashLine2');
				createCol(dataObj.countsArr[index], row);
				createCol(dataObj.countsArr2[index], row2);
				createCol(dataObj.countsArr3[index], row3);
				createCol(dataObj.countsArr4[index], row4);
				createCol(dataObj.countsArr5[index], row5);
			}
		};
		
		const sixColumn = () => {
			//Последовательность можно сделать  3,7 - 4значные, 1,5,9 - 5значные, 2,4,6,8,10 - 6значные
			if (this.bool) {
				if (index === 2 || index === 6) {
					// row.classList.remove('flashLine1');
					row4.classList.add('flashLine4');
					createCol(dataObj.countsArr[index], row);
					createCol(dataObj.countsArr2[index], row2);
					createCol(dataObj.countsArr3[index], row3);
					createCol(dataObj.countsArr4[index], row4);
				}
				else if (index === 0 || index === 4 || index === 8) {
					row.classList.remove('flashLine1');
					row5.classList.add('flashLine5');
					row2.classList.add('flashLine2');
					createCol(dataObj.countsArr[index], row);
					createCol(dataObj.countsArr2[index], row2);
					createCol(dataObj.countsArr3[index], row3);
					createCol(dataObj.countsArr4[index], row4);
					createCol(dataObj.countsArr5[index], row5);
				}
				else {
					row.classList.remove('flashLine1');
					row6.classList.add('flashLine6');
					row3.classList.add('flashLine3');
					createCol(dataObj.countsArr[index], row);
					createCol(dataObj.countsArr2[index], row2);
					createCol(dataObj.countsArr3[index], row3);
					createCol(dataObj.countsArr4[index], row4);
					createCol(dataObj.countsArr5[index], row5);
					createCol(dataObj.countsArr6[index], row6);
				}
			}
			else {
				row.classList.remove('flashLine1');
				row6.classList.add('flashLine6');
				row3.classList.add('flashLine3');
				createCol(dataObj.countsArr[index], row);
				createCol(dataObj.countsArr2[index], row2);
				createCol(dataObj.countsArr3[index], row3);
				createCol(dataObj.countsArr4[index], row4);
				createCol(dataObj.countsArr5[index], row5);
				createCol(dataObj.countsArr6[index], row6);
			}
		};
		
		const sevenColumn = () => {
			// Последовательность можно сделать 1,5 - 4x значные; 3,7 - 5значные; 2,9 - 6значные; 4,6 - 7значные;
			if (this.bool) {
				if (index === 0 || index === 4) {
					row4.classList.add('flashLine4');
					createCol(dataObj.countsArr[index], row);
					createCol(dataObj.countsArr2[index], row2);
					createCol(dataObj.countsArr3[index], row3);
					createCol(dataObj.countsArr4[index], row4);
				}
				else if (index === 2 || index === 6) {
					row.classList.remove('flashLine1');
					row5.classList.add('flashLine5');
					row2.classList.add('flashLine2');
					createCol(dataObj.countsArr[index], row);
					createCol(dataObj.countsArr2[index], row2);
					createCol(dataObj.countsArr3[index], row3);
					createCol(dataObj.countsArr4[index], row4);
					createCol(dataObj.countsArr5[index], row5);
				}
				else if (index === 1 || index === 8) {
					row.classList.remove('flashLine1');
					row6.classList.add('flashLine6');
					row3.classList.add('flashLine3');
					createCol(dataObj.countsArr[index], row);
					createCol(dataObj.countsArr2[index], row2);
					createCol(dataObj.countsArr3[index], row3);
					createCol(dataObj.countsArr4[index], row4);
					createCol(dataObj.countsArr5[index], row5);
					createCol(dataObj.countsArr6[index], row6);
				}
				else {
					row7.classList.add('flashLine7');
					row4.classList.add('flashLine4');
					createCol(dataObj.countsArr[index], row);
					createCol(dataObj.countsArr2[index], row2);
					createCol(dataObj.countsArr3[index], row3);
					createCol(dataObj.countsArr4[index], row4);
					createCol(dataObj.countsArr5[index], row5);
					createCol(dataObj.countsArr6[index], row6);
					createCol(dataObj.countsArr7[index], row7);
				}
			}
			else {
				row7.classList.add('flashLine7');
				row4.classList.add('flashLine4');
				createCol(dataObj.countsArr[index], row);
				createCol(dataObj.countsArr2[index], row2);
				createCol(dataObj.countsArr3[index], row3);
				createCol(dataObj.countsArr4[index], row4);
				createCol(dataObj.countsArr5[index], row5);
				createCol(dataObj.countsArr6[index], row6);
				createCol(dataObj.countsArr7[index], row7);
			}
		};
		
		// проверка и присвоение елементов массива нужного результата
		let mainArr = dataObj.sumArr;
		if (bitNumber === 2) {
			twoColumn();
			mainArr = dataObj.sumAllArr;
			if (this.bool) {
				mainArr = dataObj.sumAllArr.map((i) => {
					if (index % 2 !== 0 && index !== 5) {
						return (i / 10 | 0);
					}
					else return i;
				});
			}
		}
		else if (bitNumber === 3) {
			threeColumn();
			mainArr = dataObj.sumAllArr3;
			if (this.bool) {
				mainArr = dataObj.sumAllArr3.map((i) => {
					if (index === 0 || index === 4 || index === 8) {
						return (i / 10 | 0);
					}
					else if (index === 2 || index === 6) {
						return (i / 100 | 0);
					}
					else return i;
				});
			}
		}
		else if (bitNumber === 4) {
			fourColumn();
			mainArr = dataObj.sumAllArr4;
		}
		else if (bitNumber === 5) {
			fiveColumn();
			mainArr = dataObj.sumAllArr5;
			if (this.bool) {
				mainArr = dataObj.sumAllArr5.map((i) => {
					if (index % 2 !== 0 && index !== 5) {
						return (i / 10 | 0);
					}
					else return i;
				});
			}
		}
		else if (bitNumber === 6) {
			sixColumn();
			mainArr = dataObj.sumAllArr6;
			if (this.bool) {
				mainArr = dataObj.sumAllArr6.map((i) => {
					if (index === 0 || index === 4 || index === 8) {
						return (i / 10 | 0);
					}
					else if (index === 2 || index === 6) {
						return (i / 100 | 0);
					}
					else return i;
				});
			}
		}
		else if (bitNumber === 7) {
			sevenColumn();
			mainArr = dataObj.sumAllArr7;
			if (this.bool) {
				mainArr = dataObj.sumAllArr7.map((i) => {
					if (index === 0 || index === 4) {
						return (i / 1000 | 0);
					}
					else if (index === 2 || index === 6) {
						return (i / 100 | 0);
					}
					else if (index === 1 || index === 8) {
						return (i / 10 | 0);
					}
					else return i;
				});
			}
		}
		else oneColumn();
		console.log(mainArr);
// ======================================================================

// ======================================================================
// создаем кнопку "ПРОВЕРИТЬ"
		if (!document.querySelector('#button')) {
			const button = createHtmlElement(`
				<input id="button" type="button" value="ПРОВЕРИТЬ">`);
			main.appendChild(button);
		}
		const colArr = document.querySelectorAll('.column-flash');
		const inp = document.querySelector('.column-flash__input input');
		let loser = 0;
		button.addEventListener('click', (e) => {
			let res = document.querySelector('.stars').innerHTML;
			if (+mainArr[index] === +inp.value && inp.value !== '') {
				//удаляем колонки с правильним ответом
				for (let i = 0; i < colArr.length; i++) {
					colArr[i].remove();
				}
				e.currentTarget.remove();
				res++;
				if (this.firstAnswer) {
					createStar(table, res);
				}
				index++;
				// виводитсься после окончания уровнений
				if (index === dataObj.sumArr.length) {
					// res = index - loser;
					if (loser <= 2) {
						const text = "Круто!";
						// const text1 = "Вот это да!";
						this.showModalWindow(res, text, audio_Au_t_8);
					}
					else if (5 >= loser && loser <= 8) {
						const text = "Умница!";
						// const text1 = "Молодец!";
						this.showModalWindow(res, text, audio_Au_t_7);
					}
					else {
						let text = "Хорошо, давай попробуем еще раз?";
						this.showModalWindow(res, text, audio_Au_t_5);
					}
					return;
				}
				
				setTimeout(() => {
					this.firstAnswer = true;
					this.createColumn(bitNumber, dataObj, index);
				}, 1000);
			}
			else {
				this.firstAnswer = false;
				// new Audio(sound.tune.Zv_2).play();
				inp.style.background = '#d24a43';
				inp.style.color = '#fff';
				inp.value = '';
				loser++;
			}
		});
	}
	
	showModalWindow(res, text) {
		const modalText = document.querySelector('.modal__text'),
			countAll = document.querySelector(".stars"),
			modal = document.querySelector('.modal'),
			modalBtn = document.querySelector('.modal__buttons'),
			repeatBtn = document.querySelector('.repeatButton');
		modal.style.display = 'flex';
		
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
			`${text}
			Ты набрал ${res} ${starWord}!`;
		countAll.innerHTML = res;
		
		if (!repeatBtn) {
			const repeatBtn = createHtmlElement(`
				<input type="submit" value="Повторить" class="button repeatButton">`);
			
			modalBtn.appendChild(repeatBtn);
			
			repeatBtn.addEventListener('click', () => {
				modal.style.display = 'none';
				repeatBtn.remove();
				this.startFlashCart();
			});
		}
		else {
			repeatBtn.addEventListener('click', () => {
				modal.style.display = 'none';
				repeatBtn.remove();
				this.startFlashCart();
			});
		}
	}
}
