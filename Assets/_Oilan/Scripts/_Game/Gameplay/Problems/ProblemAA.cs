using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oilan
{
    public class problemValues
    {
        public List<List<int>> countsArr;
        public List<int> sumArr;
    }
    
    public class ProblemAA
    {

        private bool duplicate(List<List<int>> arr)
        {
            bool result = true;

            for (int i = 0; i < arr.Count; i++)
            {
                for (int j = 0; j < arr.Count; j++)
                {
                    if (i != j)
                    {
                        int similarity = 0;
                        for (int k = 0; k < arr[i].Count; k++)
                        {
                            if (arr[i][k] == arr[j][k])
                            {
                                similarity++;
                            }
                        }

                        if (similarity == arr[i].Count)
                        {
                            result = false;
                            return result;
                        }

                    }
                }
            }
            return result;
        }

        private int genSimple(int lastItem, bool operation)
        {
            int result = 0;

            List<int> randArr = new List<int>();

            if (operation)
            {
                if (lastItem >= 0 && lastItem <= 4)
                {
                    int toNum = 9 - lastItem;
                    int toNum2 = 4 - lastItem;

                    for (int i = 5; i <= toNum; i++)
                    {
                        randArr.Add(i);
                    }

                    for (int i = 1; i <= toNum2; i++)
                    {
                        randArr.Add(i);
                    }
                }
                else
                {
                    for (int i = 1; i <= (9 - lastItem); i++)
                    {
                        randArr.Add(i);
                    }
                }
                result = randArr[UnityEngine.Random.Range(0, randArr.Count - 1)];
            }
            else
            {
                if (lastItem >= 1 && lastItem <= 4)
                {
                    for (int i = 1; i <= lastItem; i++)
                    {
                        randArr.Add(i);
                    }
                }
                else
                {
                    int toNum = lastItem - 5;

                    for (int i = 1; i <= toNum; i++)
                    {
                        randArr.Add(i);
                    }
                    for (int i = 5; i <= lastItem; i++)
                    {
                        randArr.Add(i);
                    }
                }
                result = randArr[UnityEngine.Random.Range(0, randArr.Count - 1)];
                result = (-1) * result;
            }

            return result;
        }

        private int res(int first, int second, bool operation)
        {

            int sum = first + second;
            bool lastOperation = UnityEngine.Random.Range(0f, 1f) > 0.5f;

            if (sum == 9)
            {
                lastOperation = false;
            }

            if (operation)
            {
                return genSimple(first + second, lastOperation);
            }
            else
            {
                second = second * (-1);
                return genSimple(first - second, lastOperation);
            }
        }

        public problemValues getAbacusSimple(int columns)
        {
            List<List<int>> result = new List<List<int>>();
            List<int> sums = new List<int>();
            List<int> randArr = new List<int>();

            bool operation = UnityEngine.Random.Range(0f, 1f) > 0.5f;

            for (int i = 1; i <= 8; i++)
            {
                if (i == 5)
                {
                    continue;
                }
                randArr.Add(i);
            }

            for (int i = 0; i < columns; i++)
            {
                result.Add(new List<int>());

                result[i].Add(randArr[UnityEngine.Random.Range(0, randArr.Count - 1) + 1]);
                result[i].Add(genSimple(result[i][0], operation));
                result[i].Add(res(result[i][0], result[i][1], operation));

                int sum = 0;
                foreach (int item in result[i])
                {
                    sum += item;
                }

                sums.Add(sum);
            }

            //check similar problems
            bool verifiedArray = duplicate(result);

            if (!verifiedArray)
            {
                return getAbacusSimple(columns);
            }
            else
            {
                problemValues resultingVal = new problemValues();

                resultingVal.countsArr = result;
                resultingVal.sumArr = sums;

                return resultingVal;
            }

        }

    }
}