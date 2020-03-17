import {image} from "../other/image";

export default class FleshCart {
	
	constructor() {
		this.arrLength = 10
	}
	
	getFleshCart() {
		
		const x = image.flashCart;
		const arrFlashName = [
			x.star, x.ball, x.circle,
			x.apple, x.fish, x.heart,
			x.cat, x.dog, x.flower, x.bear,
			x.star, x.ball, x.circle,
			x.apple, x.fish, x.heart,
			x.cat, x.dog, x.flower, x.bear
		];
		
		const newArr = a => a.map((x) => x.slice());
		
		const flash = [];
		const sumArr = [];
		
		for (let i = 0; i < this.arrLength; i++) {
			flash[i] = [''];
			sumArr[i] = [];
		}
		
		const flash2 = newArr(flash);
		const flash3 = newArr(flash);
		const flash4 = newArr(flash);
		const flash5 = newArr(flash);
		const flash6 = newArr(flash);
		const flash7 = newArr(flash);
		const sumArr2 = newArr(sumArr);
		const sumArr3 = newArr(sumArr);
		const sumArr4 = newArr(sumArr);
		const sumArr5 = newArr(sumArr);
		const sumArr6 = newArr(sumArr);
		const sumArr7 = newArr(sumArr);
		
		let finSumArr = [];
		let finSumArr2 = [];
		let finSumArr3 = [];
		let finSumArr4 = [];
		let finSumArr5 = [];
		let finSumArr6 = [];
		let finSumArr7 = [];
		const finishSumArr = [];
		const finishSumArr3 = [];
		const finishSumArr4 = [];
		const finishSumArr5 = [];
		const finishSumArr6 = [];
		const finishSumArr7 = [];
		
		const addFirstElemToArr = (flash) => {
			for (let i = 0; i < flash.length; i++) {
				const randFirstCount = Math.random() >= 0.5;
				if (randFirstCount) {
					flash[i].shift();
					flash[i].unshift(arrFlashName[i]);
				}
			}
		};
		
		const addElemToArr = (flash2) => {
			for (let i = 0; i < this.arrLength; i++) {
				const rand = Math.floor(Math.random() * 5); // рандом от 0 до 4
				for (let j = 0; j < rand; j++) {
					flash2[i].push(arrFlashName[i]);
				}
			}
		};
		
		const calculateSum = (flash, sumArr, finSumArr) => {
			for (let i = 0; i < flash.length; i++) {
				// if (!flash[i][0].length) {
				if (!flash[i][0]) {
					sumArr[i].push(flash[i].length - 1);
				}
				else {
					sumArr[i].push(5);
					sumArr[i].push(flash[i].length - 1);
				}
			}
			sumArr.forEach((value, index) => {
				finSumArr[index] = sumArr[index].reduce(function (a, b) {
					return a + b;
				});
			});
			
		};
		
		//добавляем елементи в массив (кроме первого)
		for (let i = 0; i < this.arrLength; i++) {
			const rand = Math.floor((Math.random() * (5 - 1)) + 1); // рандом от 1 до 4
			for (let j = 0; j < rand; j++) {
				flash[i].push(arrFlashName[i]);
			}
		}
		
		//добавляем елементи в массив (кроме первого)
		addElemToArr(flash2);
		addElemToArr(flash3);
		addElemToArr(flash4);
		addElemToArr(flash5);
		addElemToArr(flash6);
		addElemToArr(flash7);
		
		//добавляем первий елемент массива
		addFirstElemToArr(flash);
		addFirstElemToArr(flash2);
		addFirstElemToArr(flash3);
		addFirstElemToArr(flash4);
		addFirstElemToArr(flash5);
		addFirstElemToArr(flash6);
		addFirstElemToArr(flash7);
		
		// считаем сумму
		calculateSum(flash, sumArr, finSumArr);
		calculateSum(flash2, sumArr2, finSumArr2);
		calculateSum(flash3, sumArr3, finSumArr3);
		calculateSum(flash4, sumArr4, finSumArr4);
		calculateSum(flash5, sumArr5, finSumArr5);
		calculateSum(flash6, sumArr6, finSumArr6);
		calculateSum(flash7, sumArr7, finSumArr7);

		
		for (let i = 0; i < finSumArr2.length; i++) {
			finishSumArr[i] = String(finSumArr[i]) + finSumArr2[i];
		}
		
		for (let i = 0; i < finSumArr3.length; i++) {
			finishSumArr3[i] = String(finSumArr[i]) + finSumArr2[i] + finSumArr3[i];
		}
		for (let i = 0; i < finSumArr4.length; i++) {
			finishSumArr4[i] = String(finSumArr[i]) + finSumArr2[i] + finSumArr3[i] + finSumArr4[i];
		}
		for (let i = 0; i < finSumArr5.length; i++) {
			finishSumArr5[i] = String(finSumArr[i]) + finSumArr2[i] + finSumArr3[i] + finSumArr4[i] + finSumArr5[i];
		}
		for (let i = 0; i < finSumArr6.length; i++) {
			finishSumArr6[i] = String(finSumArr[i]) + finSumArr2[i] + finSumArr3[i] + finSumArr4[i] + finSumArr5[i] + finSumArr6[i];
		}
		for (let i = 0; i < finSumArr7.length; i++) {
			finishSumArr7[i] = String(finSumArr[i]) + finSumArr2[i] + finSumArr3[i] + finSumArr4[i] + finSumArr5[i] + finSumArr6[i] + finSumArr7[i];
		}
		
		return {
			countsArr: flash,
			countsArr2: flash2,
			countsArr3: flash3,
			countsArr4: flash4,
			countsArr5: flash5,
			countsArr6: flash6,
			countsArr7: flash7,
			sumArr: finSumArr,
			sumArr2: finSumArr2,
			sumAllArr: finishSumArr,
			sumAllArr3: finishSumArr3,
			sumAllArr4: finishSumArr4,
			sumAllArr5: finishSumArr5,
			sumAllArr6: finishSumArr6,
			sumAllArr7: finishSumArr7
		}
	}
}


