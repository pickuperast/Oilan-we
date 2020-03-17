import {choseTrenazer, createHtmlElement, createStar, showAllTrenazer} from "../game";

export default class Mental {
	constructor(op) {
		this.level = op.level;
		this.digit = op.digit;
		this.column = op.column;
		this.row = op.row;
		this.iterator = 0;
		this.digitCapacity = op.digitCapacity || null; //розрядность числа
		this.titleName = op.titleName || null; //название тренажора в заголовке
		this.time = op.time || null;
		this.lastCountArr = op.lastCountArr || 9;
		this.firstCountArr = op.firstCountArr || 1;
		this.firstAnswer = true; // получение звезди за правельн ответ с первого раза
	}
	
	startMental() {
		try {
			document.querySelector('#app_simulator').innerHTML = null;
			if (this.digitCapacity) {
				switch (this.digitCapacity) {
					case '1':
						this.level = 3;
						this.digit = false;
						break;
					case '2':
						this.level = 7;
						this.digit = false;
						break;
					case 'mixed':
						this.level = 5;
						this.digit = true;
						break;
					case 'dr_1':
						this.level = 10;
						this.digit = false;
						break;
					case 'dr_1_2':
						this.level = 10;
						this.digit = true;
						break;
					case 'dr_2':
						this.level = 10;
						this.digit = false;
						this.lastCountArr = 89;
						this.firstCountArr = 10;
						break;
					case 'dr_2_3':
						this.level = 10;
						this.digit = true;
						this.lastCountArr = 899;
						this.firstCountArr = 100;
						break;
					default:
						console.log('i dont no !!!!');
				}
			}
			const dataObj = this.choose(this.level);
			console.log(dataObj);
			this.createCartMental(dataObj);
		} catch (error) {
			throw error;
		}
	}
	
	createCount() {
		if (!this.time) {
			this.time = 2000;
		}
		let genSimple = (prevCount) => {
			let result = 0;
			let count = Math.floor((Math.random() * (this.lastCountArr - this.firstCountArr)) + this.firstCountArr);
			if (this.digit) {
				if (count % 2 === 0) {
					this.lastCountArr = 89;
					this.firstCountArr = 10;
				}
				else {
					this.lastCountArr = 9;
					this.firstCountArr = 1;
				}
				count = Math.floor((Math.random() * (this.lastCountArr - this.firstCountArr)) + this.firstCountArr);
			}
			if (this.level === 8 || this.level === 7) {
				this.lastCountArr = 89;
				this.firstCountArr = 10;
				count = Math.floor((Math.random() * (this.lastCountArr - this.firstCountArr)) + this.firstCountArr);
			}
			if (prevCount >= count) {
				result = count * (-1);
			}
			else {
				result = count;
			}
			return result;
		};
		
		const result = [];
		for (let i = 0; i < this.row; i++) {
			result[i] = genSimple(result[i - 1]);
		}
		const sums = result.reduce((a, b) => { return a + b });
		return {
			countsArr: result,
			sumArr: sums
		};
	}
	
	createCountDouble() {
		if (!this.time) {
			this.time = 3000;
		}
		let genSimple = (lastSum) => {
			const rand = Math.random() >= 0.5;
			let result = 0;
			let count = ((Math.random() * (this.lastCountArr - this.firstCountArr)) + this.firstCountArr).toFixed(2);
			if (this.digit) {
				if (rand % 2 === 1) {
					this.lastCountArr = 89;
					this.firstCountArr = 10;
				}
				else if(this.digitCapacity === 'dr_2_3'){
					this.lastCountArr = 899;
					this.firstCountArr = 100;
				}
				else {
					this.lastCountArr = 9;
					this.firstCountArr = 1;
				}
				count = ((Math.random() * (this.lastCountArr - this.firstCountArr)) + this.firstCountArr).toFixed(2);
			}
			if (lastSum >= count) {
				result = count * (-1);
			}
			else {
				result = count;
			}
			result = parseFloat(result).toFixed(2);
			return +result;
		};
		
		let sumArr = [];
		let result = [];
		result[0] = +((Math.random() * (this.lastCountArr - this.firstCountArr)) + this.firstCountArr).toFixed(2);
		for (let i = 0; i < this.row - 1; i++) {
			const lastSum = result.reduce((a, b) => a + b);
			result.push(genSimple(lastSum));
		}
		result.forEach((item) => {
			sumArr.push(item.toString().replace(".", ","));
		});
		return {
			countsArr: sumArr,
			sumArr: result.reduce((a, b) => a + b).toFixed(2)
		}
	}
	
	choose(level) {
		let data;
		switch (level) {
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
				data = this.createCount();
				break;
			case 9:
			case 10:
				data = this.createCountDouble();
				break;
			default:
				console.log('Я таких значений не знаю');
		}
		return data;
	};
	
	createCartMental(arrData) {
		if (this.titleName) {
			document.querySelector('.title').innerHTML = (this.titleName);
		}
		else {
			document.querySelector('.title').innerHTML = 'Mental-арифметика';
		}
		const main = document.querySelector('#main'),
			table = document.querySelector('#app_simulator'),
			inputAnswer = createHtmlElement(`<input class="inputAnswer" type="number"/>`),
			time2 = this.time * 1.1;
		
		table.innerHTML = null;
		const cart = createHtmlElement(`<div class="mental"></div>`);
		table.appendChild(cart);
		
		
		if(this.digitCapacity === 'dr_2_3'){
			cart.style.fontSize = '5rem';
		}
		const myInterval = () => {
			const cartCount = createHtmlElement(`<div class="mental__count">${arrData.countsArr[current]}</div>`);
			cart.appendChild(cartCount);
			setTimeout(() => { cartCount.remove() }, this.time);
			current++;
			if (current > this.row) {
				current = 0;
				clearInterval(int);
				cart.remove();
				table.appendChild(inputAnswer);
				// создаем кнопку "ПРОВЕРИТЬ"
				if (!document.querySelector('#button')) {
					const button = createHtmlElement(`
						<input id="button" type="button" value="ПРОВЕРИТЬ">`);
					main.appendChild(button);
				}
				button.addEventListener('click', (e) => {
					clearInterval(int);
					let result = document.querySelector('.stars').innerHTML;
					result++;
					if (+arrData.sumArr === +inputAnswer.value && inputAnswer.value !== '') {
						this.iterator++;
						e.target.remove();
						inputAnswer.style.background = '#94ec5a';
						
						if (this.firstAnswer) {
							createStar(table, +result);
						}
						
						if (this.iterator === this.column) {
							this.iterator = 0;
							e.target.remove();
							this.showModalWindow(result, 'СУПЕР!!!');
						}
						
						else {
							this.firstAnswer = true;
							setTimeout(() => {
								this.startMental();
							}, time2);
						}
					}
					else {
						if (this.iterator === this.column) {
							this.iterator = 0;
							e.target.remove();
							this.showModalWindow(result, 'СУПЕР!!!');
						}
						console.log(` не верно `);
						// new Audio(sound.tune.Zv_2).play();
						inputAnswer.style.background = '#d24a43';
						inputAnswer.style.color = '#ffffff';
						e.target.remove();
						setTimeout(() => {
							this.firstAnswer = false;
							this.createCartMental(arrData);
						}, time2);
					}
				});
			}
		};
		let current = 0;
		myInterval();
		let int = setInterval(myInterval, time2);
		//проверка на вискакивание поля ответа в в других тренажорах
		document.querySelector('#form input[type=submit]').addEventListener('click', () => clearInterval(int));
		document.querySelector('#common-button').addEventListener('click', () => clearInterval(int));
		return false;
	}
	
	showModalWindow(res, text) {
		const modalText = document.querySelector('.modal__text'),
			countAll = document.querySelector(".stars"),
			modal = document.querySelector('.modal'),
			modalBtn = document.querySelector('.modal__buttons'),
			nextBtn = document.querySelector('.nextButton'),
			repeatBtn = document.querySelector('.repeatButton');
		modal.style.display = 'flex';
		
		function resData(res) {
			if (res < 0) {
				res = 0;
			}
			if (res === 1) {
				return `звезду`;
			}
			else if (res <= 4 && res >= 2) {
				return `звезды`;
			}
			return 'звезд';
		}
		
		modalText.innerText = `${text} Ты набрал ${res} ${resData(res)}!`;
		countAll.innerHTML = res;
		
		if (!repeatBtn || !nextBtn) {
			const repeatBtn = createHtmlElement(`
				<input type="submit" value="Повторить" class="button repeatButton">`);
			const nextBtn = createHtmlElement(`
				<input type="submit" value="Продолжить" class="button nextButton">`);
			modalBtn.appendChild(repeatBtn);
			modalBtn.appendChild(nextBtn);
			
			repeatBtn.addEventListener('click', () => {
				modal.style.display = 'none';
				repeatBtn.remove();
				nextBtn.remove();
				this.startMental();
			});
			
			nextBtn.addEventListener('click', () => {
				modal.style.display = 'none';
				repeatBtn.remove();
				nextBtn.remove();
				//if digitCapacity == null
				showAllTrenazer();
				choseTrenazer();
			});
		}
		else {
			repeatBtn.addEventListener('click', () => {
				modal.style.display = 'none';
				repeatBtn.remove();
				nextBtn.remove();
				this.startMental();
			});
			nextBtn.addEventListener('click', () => {
				modal.style.display = 'none';
				repeatBtn.remove();
				nextBtn.remove();
				showAllTrenazer();
				choseTrenazer();
			});
		}
	};
}
