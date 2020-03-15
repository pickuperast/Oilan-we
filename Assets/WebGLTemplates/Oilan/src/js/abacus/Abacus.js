export default class Abacus {
	
	constructor() {
	}
	
	duplicate(arr) {
		const obj = {};
		return arr.filter((a) => {
			return a in obj ? 0 : obj[a] = 1;
		});
	}
	
	//================================================================================
	
	getAbacusSimple(columns) {
		let genSimple = (lastItem, operation) => {
			let result = 0;
			if (operation) {
				if (lastItem >= 0 && lastItem <= 4) {
					const toNum = 9 - lastItem;
					const toNum2 = 4 - lastItem;
					const randArr = [];
					for (let i = 5; i <= toNum; i++) {
						randArr.push(i);
					}
					for (let i = 1; i <= toNum2; i++) {
						randArr.push(i);
					}
					result = randArr[Math.floor((Math.random() * (randArr.length - 1)))];
					// console.log(`+ last= ${lastItem}  [${randArr}] res= ${result}`);
				}
				else {
					const randArr = [];
					for (let i = 1; i <= (9 - lastItem); i++) {
						randArr.push(i);
					}
					result = randArr[Math.floor((Math.random() * (randArr.length - 1)))];
					// console.log(`+ last= ${lastItem}  [${randArr}] res= ${result}`);
				}
			}
			else {
				if (lastItem >= 1 && lastItem <= 4) {
					const randArr = [];
					for (let i = 1; i <= lastItem; i++) {
						randArr.push(i);
					}
					result = randArr[Math.floor((Math.random() * (randArr.length - 1)))];
					// console.log(`- last= ${lastItem}  [${randArr}] res= ${result}`);
				}
				else {
					const toNum = lastItem - 5;
					const randArr = [];
					for (let i = 1; i <= toNum; i++) {
						randArr.push(i);
					}
					for (let i = 5; i <= lastItem; i++) {
						randArr.push(i);
					}
					let preresult = Math.floor((Math.random() * (randArr.length - 1)));
					result = randArr[preresult];
					// console.log(`- last= ${lastItem}  [${randArr}] res= ${result}`);
				}
				result = (result) * (-1);
			}
			return result;
		};
		
		const result = [],
			sums = [],
			randArr = [],
			operation = Math.random() >= 0.5;
		for (let i = 1; i <= 8; i++) {
			if (i === 5) {
				continue;
			}
			randArr.push(i);
		}
		const res = (first, second, operation) => {
			const sum = first + second;
			let lastOperation = Math.random() >= 0.5;
			if (sum === 9) {
				lastOperation = false;
			}
			if (operation) {
				return genSimple(first + second, lastOperation);
			}
			else {
				second = second * (-1);
				return genSimple(first - second, lastOperation);
			}
		};
		for (let i = 0; i < columns; i++) {
			result[i] = [];
			result[i][0] = randArr[Math.floor((Math.random() * (randArr.length - 1)) + 1)];
			result[i][1] = genSimple(result[i][0], operation);
			result[i][2] = res(result[i][0], result[i][1], operation);
			sums[i] = result[i].reduce(function (a, b) {
				return a + b;
			});
		}
		//проверка на  одинаковые уравнение
		const verifiedArray = this.duplicate(result);
		if (verifiedArray.length < columns) {
			return this.getAbacusSimple(columns);
		}
		else {
			console.table(`======= result ====== `, sums);
			return {
				countsArr: result,
				sumArr: sums
			};
		}
	}
	
	getAbacusSimpleStep_3(columns) {
		const genSimple = (count, operation) => {
			const remainder = count % 10;
			let result = 0;
			
			if (remainder >= 1 && remainder <= 4) {
				const toNum = 10 - remainder;
				const randArr = [];
				for (let i = toNum; i <= 9; i++) {
					randArr.push(i);
				}
				const preresult = Math.floor((Math.random() * (randArr.length)));
				result = randArr[preresult];
//             console.log('1-4| ' + remainder + '| ' + randArr + '| ' + result);
			}
			else if (remainder === 5) {
				result = 5;
//             console.log('5| ' + remainder + '| ' + result);
			
			}
			else if (remainder >= 6 && remainder <= 9) {
				
				const toNum = 10 - remainder;
				const toNum2 = 15 - remainder;
				const randArr = [];
				for (let i = toNum; i <= 5; i++) {
					randArr.push(i);
				}
				for (let i = toNum2; i <= 9; i++) {
					randArr.push(i);
				}
				const preresult = Math.floor((Math.random() * (randArr.length)));
				result = randArr[preresult];
//             console.log('6-9| ' + remainder + '| ' + randArr + '| ' + result);
			}
			// console.log(operation + ' | ' + count + ' | ' + (result));
			if (operation) {
				if ((result % 10) === (10 - remainder)) {
					result = genSimple(count, operation);
					// console.log('---' + (result % 10) + '| ' + (10 - remainder) + ' | ' + count);
				}
			}
			return result;
		};
		
		const result = [],
			sums = [],
			randArr = [];
		for (let i = 2; i <= 29; i++) {
			if (i === 10 || i === 20 || i === 11 || i === 21 || i === 5 || i === 15 || i === 25) {
				continue;
			}
			randArr.push(i);
		}
		
		for (let i = 0; i < columns; i++) {
			result[i] = [];
			result[i][0] = randArr[Math.floor((Math.random() * (randArr.length - 1)) + 1)];
			result[i][1] = genSimple(result[i][0], true);
			result[i][2] = genSimple(result[i][1] + result[i][0], false);
			sums[i] = result[i].reduce(function (a, b) {
				return a + b;
			});
		}
		//проверка на  одинаковые уравнение
		const verifiedArray = this.duplicate(result);
		if (verifiedArray.length < columns) {
			return this.getAbacusSimpleStep_3(columns);
		}
		else {
			console.log(`======= result ====== `, sums);
			return {
				countsArr: result,
				sumArr: sums
			};
		}
	}
	
	getAbacusSimpleStep_4(columns) {
		
		const genSimple = (count, operation) => {
			const remainder = count % 10;
			let result = 0;
			
			if (remainder >= 0 && remainder <= 3) {
				const toNum = remainder + 1;
				const toNum2 = remainder + 6;
				
				const randArr = [];
				for (let i = toNum; i <= 5; i++) {
					randArr.push(i);
				}
				for (let i = toNum2; i <= 9; i++) {
					randArr.push(i);
				}
				const preresult = Math.floor((Math.random() * (randArr.length - 1)));
				result = randArr[preresult];
//             console.log(`| ${remainder} [${randArr}] res= ${result}`);
			
			}
			else if (remainder === 4) {
				result = 5;
//             console.log(`| ${result}`);
			}
			else if (remainder >= 5 && remainder <= 8) {
				const toNum = remainder + 1;
				const randArr = [];
				for (let i = toNum; i <= 9; i++) {
					randArr.unshift(i);
				}
				const preresult = Math.floor((Math.random() * (randArr.length - 1)));
				result = randArr[preresult];
//             console.log(`| ${remainder} [${randArr}] res= ${result}`);
			}
			if (operation && ((count - result) % 10) - 9 === 0) {
				result = genSimple(count, true);
				// console.log(`NewRes=${result}`);
			}
			return result;
		};
		
		const result = [];
		const sums = [];
		const randArr = [];
		for (let i = 30; i <= 47; i++) {
			if (i === 34 || i === 38 || i === 39 || i === 44) {
				continue;
			}
			randArr.push(i);
		}
		const res = (first, second) => {
			let min = first - second * -1;
			return genSimple(min, false);
		};
		
		for (let i = 0; i < columns; i++) {
			result[i] = [];
			result[i][0] = randArr[Math.floor((Math.random() * (randArr.length - 1)) + 1)];
			result[i][1] = genSimple(result[i][0], true) * -1;
			result[i][2] = res(result[i][0], result[i][1]) * (-1);
			sums[i] = result[i].reduce(function (a, b) {
				return a + b;
			});
		}
		
		//проверка на  одинаковые уравнение
		const verifiedArray = this.duplicate(result);
		if (verifiedArray.length < columns) {
			return this.getAbacusSimpleStep_4(columns);
		}
		else {
			console.log(`======= result ====== `, sums);
			return {
				countsArr: result,
				sumArr: sums
			};
		}
	}
	
	getAbacusSimpleStep_5_6(columns) {
		
		const genSimple = (count) => {
			let result = 0;
			
			if (count >= 1 && count <= 4) {
				const toNum = 5 - count;
				const randArr = [];
				
				for (let i = toNum; i <= 4; i++) {
					randArr.push(i);
				}
				
				const preresult = Math.floor((Math.random() * (randArr.length)));
				result = randArr[preresult];
//             console.log('1-4| ' + remainder + '| ' + randArr + '| ' + result);
			}
			
			else {
				const toNum = count - 4;
				const randArr = [];
				for (let i = toNum; i <= 4; i++) {
					randArr.push(i);
				}
				let preresult = Math.floor((Math.random() * (randArr.length)));
				result = randArr[preresult] * (-1);
//             console.log('6-9| ' + remainder + '| ' + randArr + '| ' + result);
			}
			
			return result;
		};
		
		const result = [];
		const sums = [];
		
		for (let i = 0; i < columns; i++) {
			result[i] = [];
			result[i][0] = Math.floor((Math.random() * 8) + 1);
			result[i][1] = genSimple(result[i][0]);
			result[i][2] = genSimple(result[i][1] + result[i][0]);
			sums[i] = result[i].reduce(function (a, b) {
				return a + b;
			});
		}
		
		//проверка на  одинаковые уравнение
		const verifiedArray = this.duplicate(result);
		if (verifiedArray.length < columns) {
			return this.getAbacusSimpleStep_5_6(columns);
		}
		else {
			console.log(`======= result ====== `, sums);
			return {
				countsArr: result,
				sumArr: sums
			};
		}
	}
	
	getAbacusSimpleStep_7_8(columns) {
		
		const genSimple = (count) => {
			let result = 0;
			if (count >= 5 && count <= 8) {
				const toNum = 14 - count;
				const randArr = [];
				for (let i = 6; i <= toNum; i++) {
					randArr.push(i);
				}
				const preresult = Math.floor((Math.random() * (randArr.length)));
				result = randArr[preresult];
//             console.log('5-8| ' + count + '| ' + randArr + '| ' + result);
			
			}
			else {
				const toNum = count - 5;
				const randArr = [];
				for (let i = 6; i <= toNum; i++) {
					randArr.push(i);
				}
				const preresult = Math.floor((Math.random() * (randArr.length)));
				result = randArr[preresult] * (-1);
//             console.log('11-14| ' + count + '| ' + randArr + '| ' + result);
			}
			return result;
		};
		
		const result = [];
		const sums = [];
		const randArr = [];
		for (let i = 5; i <= 14; i++) {
			if (i === 10 || i === 9) {
				continue;
			}
			randArr.push(i);
		}
		for (let i = 0; i < columns; i++) {
			result[i] = [];
			result[i][0] = randArr[Math.floor((Math.random() * (randArr.length - 1)) + 1)];
			result[i][1] = genSimple(result[i][0]);
			result[i][2] = genSimple(result[i][1] + result[i][0]);
			sums[i] = result[i].reduce(function (a, b) {
				return a + b;
			});
		}
		
		//проверка на  одинаковые уравнение
		const verifiedArray = this.duplicate(result);
		if (verifiedArray.length < columns) {
			return this.getAbacusSimpleStep_7_8(columns);
		}
		else {
			console.log(`======= result ====== `, sums);
			return {
				countsArr: result,
				sumArr: sums
			};
		}
	}
	
	getAbacusSimpleStep_9(columns, rows, firstCount, lastCount, digit) {
		
		let genSimple = (prevCount, operation, j) => {
			let result = 0;
			let count = Math.floor((Math.random() * (lastCount - firstCount)) + firstCount);
			if (digit) {
				if (count % 2 === 0) {
					count = Math.floor((Math.random() * 89) + 10);
				}
				else {
					count = Math.floor((Math.random() * 9) + 1);
				}
			}
			if (prevCount >= count) {
				result = count * (-1);
				// console.log(`- prevCount= ${prevCount} count= ${count} `)
			}
			else {
				result = count;
				// console.log(`+ prevCount= ${prevCount} count= ${count} `)
			}
			//проверка и присвоения первого числа в уравнении
			if (j === 0) {
				// result = Math.floor((Math.random() * (8)) + 1);
				result = Math.floor((Math.random() * (lastCount - firstCount)) + firstCount);
			}
			return result;
		};
		
		const result = [];
		const sums = [];
		const operation = Math.random() >= 0.5;
		for (let i = 0; i < columns; i++) {
			result[i] = [];
			for (let j = 0; j < rows; j++) {
				result[i][j] = genSimple(result[i][j - 1], operation, j);
			}
			// console.log(result[i]);
			sums[i] = result[i].reduce(function (a, b) {
				return a + b;
			});
		}
		
		//проверка на  одинаковые уравнение
		const verifiedArray = this.duplicate(result);
		if (verifiedArray.length < columns) {
			return this.getAbacusSimpleStep_9(columns);
		}
		else {
			console.log(`======= result ====== `, sums);
			return {
				countsArr: result,
				sumArr: sums
			};
		}
		
	}
	
	getAbacusSimpleDoubleOne(columns, rows, firstCount, lastCount) {
		
		const genSimple = (prev, j) => {
			let count = (Math.random() * (lastCount - firstCount) + firstCount).toFixed(2);
			// let secondCount = (Math.random() * (this.digit - this.firstCountArr) + this.firstCountArr).toFixed(2);
			let result = 0;
			
			if (prev <= count) {
				result = count;
			}
			else {
				result = count * -1;
			}
			//проверка и присвоения первого числа в уравнении
			if (!j) {
				result = (Math.random() * (lastCount - firstCount) + firstCount).toFixed(2);
				// console.log(`================ firstCount ==================== ${result}`);
			}
			
			result = parseFloat(result).toFixed(2);
			return result.toString().replace(".", ",");
		};
		
		const result = [];
		const sums = [];
		
		for (let i = 0; i < columns; i++) {
			result[i] = [];
			const sumArr = [];
			for (let j = 0; j < rows; j++) {
				result[i][j] = genSimple(result[i][j - 1], j);
			}
// =========================================================================
			result[i].forEach((item) => {
				item = parseFloat(item.replace(",", "."));
				sumArr.push(item);
			});
// =========================================================================
			sums[i] = sumArr.reduce(function (a, b) {
				return a + b;
			}).toFixed(2);
		}
		// console.log(`======= result ====== `, result);
		console.log(`======= sums ====== `, sums);
		
		return {
			countsArr: result,
			sumArr: sums
		}
	}
	
	getAbacusSimpleDouble(columns, rows, firstCount, lastCount, digit) {
		
		const genSimple = (prev, j, sumRes) => {
			const change = j % 2;
			let count = (Math.random() * (lastCount - firstCount) + firstCount).toFixed(2),
				smallCount = (Math.random() * (digit - firstCount) + firstCount).toFixed(2);
			
			let result = count;
			
			if (change) {
				count = smallCount;
			}
			
			if (parseFloat(sumRes) >= parseFloat(count)) {
				result = parseFloat(count) * -1;
				// console.log(`+| prev=${prev} count= ${count} res= ${result}`);
			}
			else {
				result = parseFloat(count);
				// console.log(`-| prev=${prev} count= ${count} res= ${result}`);
			}
			
			//проверка и присвоения первого числа в уравнении
			// if (!j) {
			// 	result = (Math.random() * (this.lastCountArr - this.firstCountArr) + this.firstCountArr).toFixed(2);
			// 	// console.log(`================ firstCount ==================== ${result}`);
			// }
			
			result = parseFloat(result).toFixed(2).toString().replace(".", ",");
			return result;
		};
		
		const result = [];
		const sums = [];
		for (let i = 0; i < columns; i++) {
			result[i] = [];
			const sumArr = [];
			let sumRes = 0;
			for (let j = 0; j < rows; j++) {
				result[i][j] = genSimple(result[i][j - 1], j, sumRes);
				sumRes += parseFloat(result[i][j]);
			}
			// console.log(result[i]);
// =========================================================================
			result[i].forEach((item) => {
				item = parseFloat(item.replace(",", "."));
				sumArr.push(item);
			});
// =========================================================================
			sums[i] = sumArr.reduce(function (a, b) {
				return a + b;
			}).toFixed(2);
		}
		// console.log(`======= result ====== `, result);
		console.log(`======= sum ====== `, sums);
		return {
			countsArr: result,
			sumArr: sums
		}
	}
}