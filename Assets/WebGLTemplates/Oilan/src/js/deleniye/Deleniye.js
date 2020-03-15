import {choseTrenazer, createHtmlElement, createStar, rozryad, showAllTrenazer} from "../game";
import {sound}                                                                  from "../other/sound";

export default class Deleniye {
	constructor(op) {
		this.level = op.level;
		this.step = op.step;
		this.change = op.change;
		this.M = op.M;
		this.r1 = op.r1;
		this.r2 = op.r2;
		this.a1 = op.a1;
		this.a2 = op.a2;
		this.b1 = op.b1;
		this.b2 = op.b2;
		this.R = op.R;
		this.titleName = op.titleName;
		this.digit = op.digit;
		this.iterator = 0;
		this.firstAnswer = true; // получение звезди за правельній ответ с первого раза
		this.stars = document.querySelector('.stars').innerHTML;
	}
	
	startDeleniye() {
		try {
			const dataObj = this.createCounts();
			this.showCount(dataObj);
		} catch (error) {
			throw error;
		}
	}
	
	createCounts() {
		if (this.digit) {
			if (this.iterator % 2) {
				this.R = this.a1;
				this.r1 = this.a2;
			}
			else {
				this.R = this.b1;
				this.r1 = this.b2;
			}
		}
		
		function isInteger(num) {
			return (num ^ 0) === num;
		}
		
		let counts = rozryad(this.R, this.r1);
		let delimoe = Math.floor((Math.random() * counts.count1) + counts.count1Last);
		let delitel = Math.floor((Math.random() * counts.count2) + counts.count2Last);
		let result = delimoe / delitel;
		
		while (!isInteger(result)) {
			console.log(`=======================`);
			counts = rozryad(this.R, this.r1);
			delimoe = Math.floor((Math.random() * counts.count1) + counts.count1Last);
			// delitel = Math.floor((Math.random() * counts.count2) + counts.count2Last);
			result = delimoe / delitel;
			if (result === 1) {
				delimoe = Math.floor((Math.random() * counts.count1) + counts.count1Last);
				delitel = Math.floor((Math.random() * counts.count2) + counts.count2Last);
				result = delimoe / delitel;
			}
		}
		console.log(result);
		return {
			delimoe,
			delitel,
			result
		};
	}
	
	showCount(arrData) {
		let fontSmall = '';
		if (this.r1 >= 3 || this.r2 >= 3) {
			fontSmall = 'deleniye__middleFont';
		}
		if (this.titleName) {
			document.querySelector('.title').textContent = 'деление';
		}
		else {
			document.querySelector('.title').textContent = 'Abacus - деление';
		}
		
		
		const main = document.querySelector('#main'),
			table = document.querySelector('#app_simulator'),
			inputAnswer = createHtmlElement(`<input class="deleniye__inputAnswer ${fontSmall}" type="number"/>`);
		
		table.innerHTML = null;
		
		const cart = createHtmlElement(`<div class="deleniye"></div>`);
		const cartCount = createHtmlElement(`
						<div class="deleniye__count ${fontSmall}">
							${arrData.delimoe}
							<span>:</span>
							${arrData.delitel}
							<span>=</span>
						</div>
				`);
		
		table.appendChild(cart);
		cart.appendChild(cartCount);
		cart.appendChild(inputAnswer);
		
		// создаем кнопку "ПРОВЕРИТЬ"
		if (!document.querySelector('#button')) {
			const button = createHtmlElement(`
				<input id="button" type="button" value="ПРОВЕРИТЬ">`);
			main.appendChild(button);
		}
		
		button.addEventListener('click', (e) => {
			
			console.log(this.stars);
			
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
						this.startDeleniye();
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
		});
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
				this.startDeleniye();
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
				this.startDeleniye();
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
	}
}
