import {createHtmlElement, createStar, rozryad, showAllTrenazer, choseTrenazer} from "../game";
import {sound}                                                                  from "../other/sound";

export default class Umnozheniye {
	constructor(op) {
		this.level = op.level || null;
		this.change = op.change || null;
		this.M = op.M;
		this.r1 = op.r1;
		this.r2 = op.r2;
		this.digit = op.digit || null;
		this.titleName = op.titleName || null; // название заголовка
		this.mainCount = op.mainCount; // рандомное число для общего тренажора
		this.iterator = 0;
		this.firstAnswer = true; // получение звезди за правельній ответ с первого раза
		this.stars = document.querySelector('.stars').innerHTML;
	}
	
	startUmnozheniye() {
		try {
			const dataObj = this.createCounts();
			this.showCount(dataObj);
		} catch (error) {
			throw error;
		}
	}
	
	createCounts() {
		if (this.level === 8 && this.change) {
			if (this.iterator % 2) {
				this.r1 = 3;
				this.r2 = 2;
			}
			else {
				this.r1 = 2;
				this.r2 = 3;
			}
		}
		else if (this.level === 9 && this.change) {
			switch (this.iterator) {
				case 0:
				case 3:
				case 6:
				case 9:
					this.r1 = 2;
					this.r2 = 4;
					break;
				case 1:
				case 4:
				case 7:
					this.r1 = 3;
					this.r2 = 3;
					break;
				case 2:
				case 5:
				case 8:
					this.r1 = 4;
					this.r2 = 2;
					break;
			}
		}
		else if (this.level === 10 && this.change) {
			if (this.iterator % 2) {
				this.r1 = 5;
				this.r2 = 2;
			}
			else {
				this.r1 = 4;
				this.r2 = 3;
			}
		}
		
		let counts = rozryad(this.r1, this.r2);
		let count1;
		
		if (this.mainCount !== undefined && this.mainCount !== 'random') {
			count1 = +this.mainCount;
		}
		else {
			count1 = Math.floor((Math.random() * counts.count1) + counts.count1Last);
		}
		
		const count2 = Math.floor((Math.random() * counts.count2) + counts.count2Last);
		const result = count1 * count2;
		console.log(result);
		return {
			count1,
			count2,
			result
		};
	}
	
	showCount(arrData) {
		let fontSmall = '';
		if (this.r1 >= 3 || this.r2 >= 3) {
			fontSmall = 'umnozheniye__middleFont';
		}
		if (this.mainCount) {
			document.querySelector('.title').textContent = 'таблица умножения';
		}
		else if (this.titleName) {
			document.querySelector('.title').textContent = 'умножение';
		}
		else {
			document.querySelector('.title').textContent = 'Abacus - умножение';
		}
		
		const main = document.querySelector('#main'),
			table = document.querySelector('#app_simulator'),
			inputAnswer = createHtmlElement(`<input class="umnozheniye__inputAnswer ${fontSmall}" type="number" autofocus/>`);
		
		table.innerHTML = null;
		
		const cart = createHtmlElement(`<div class="umnozheniye"></div>`);
		const cartCount = createHtmlElement(`
						<div class="umnozheniye__count ${fontSmall}">
							${arrData.count1} x ${arrData.count2} =
						</div>
				`);
		
		table.appendChild(cart);
		cart.appendChild(cartCount);
		cart.appendChild(inputAnswer);
		
		// создаем кнопку "ПРОВЕРИТЬ"
		if (!document.querySelector('#button')) {
			const button = createHtmlElement(`
				<input id="button" type="button" value="ПРОВЕРИТЬ">`);
			// setTimeout(function () {
				main.appendChild(button);
				button.addEventListener('click', function (e) {
					callbackEvent(e);
				});
			// },1000);
		}
		
		inputAnswer.addEventListener('keyup', (e) => {
			if (e.keyCode === 13) callbackEvent(e);
		});
		
		
		
		let callbackEvent = (e) => {
			
			if (+arrData.result === +inputAnswer.value && inputAnswer.value !== '') {
				this.iterator++;
				e.target.remove();
				inputAnswer.style.background = '#94ec5a';
				cart.style.opacity = '0';
				
				if (this.firstAnswer) {
					this.stars++;
					// new Audio(sound.tune.Zv_3).play();
					createStar(table, +this.stars);
				}
				if (this.iterator === this.M) {
					this.firstAnswer = true;
					this.iterator = 0;
					e.target.remove();
					this.showModalWindow(this.stars, 'СУПЕР!!!');
				}
				else {
					this.firstAnswer = true;
					setTimeout(() => {
						this.startUmnozheniye();
					}, 1000);
				}
			}
			else {
				this.firstAnswer = false;
				console.log(` не верно `);
				// new Audio(sound.tune.Zv_2).play();
				inputAnswer.style.background = '#d24a43';
				inputAnswer.value = '';
				inputAnswer.style.color = '#ffffff';
			}
		};
		
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
				this.startUmnozheniye();
			});
			
			nextBtn.addEventListener('click', () => {
				modal.style.display = 'none';
				repeatBtn.remove();
				nextBtn.remove();
				showAllTrenazer();
				choseTrenazer();
			});
			
		}
		else {
			repeatBtn.addEventListener('click', () => {
				modal.style.display = 'none';
				repeatBtn.remove();
				nextBtn.remove();
				this.iterator = 0;
				this.startUmnozheniye();
			});
			nextBtn.addEventListener('click', () => {
				modal.style.display = 'none';
				repeatBtn.remove();
				nextBtn.remove();
				this.iterator = 0;
				showAllTrenazer();
				choseTrenazer();
			});
		}
	};
}
   