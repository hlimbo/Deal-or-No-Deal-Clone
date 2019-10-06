using System.Collections;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameLogicTests
    {
        [UnityTest]
        public IEnumerator VerifyMoneyAmountsAssignedAreUnique()
        {
            GameObject obj = new GameObject();
            obj.AddComponent<MoneyArranger>();
            MoneyArranger moneyArranger = obj.GetComponent<MoneyArranger>();
            moneyArranger.LoadSuitcaseData();
            moneyArranger.ShuffleMoneyAmountsInSuitcases();

            Dictionary<float, int> randomMoneyAmounts = new Dictionary<float, int>();
            foreach(var suitcase in moneyArranger.suitcases)
            {
                if (randomMoneyAmounts.ContainsKey(suitcase.moneyAmount))
                    ++randomMoneyAmounts[suitcase.moneyAmount];
                else
                    randomMoneyAmounts.Add(suitcase.moneyAmount, 1);
            }

            bool allSuitcasesHaveUniqueMoneyValues = true;
            StringBuilder sb = new StringBuilder();
            sb.Append("Expected all suitcase money values to be unique but got:\n");
            foreach(var moneyPair in randomMoneyAmounts)
            {
                if(moneyPair.Value > 1)
                {
                    sb.Append($"Money Value: {moneyPair.Key} | Number of duplicates: {moneyPair.Value}\n");
                    allSuitcasesHaveUniqueMoneyValues = false;
                }
            }

            string errorMsg = sb.ToString();
            Assert.IsTrue(allSuitcasesHaveUniqueMoneyValues, errorMsg);
            yield return null;
        }

        [UnityTest]
        public IEnumerator VerifyBankerCalculatesInitialAverageCorrectly()
        {
            GameObject obj = new GameObject();
            obj.AddComponent<Banker>();
            Banker banker = obj.GetComponent<Banker>();
            yield return null;
            float expectedAverageMoneyAmount = banker.TotalMoneyAmount / banker.ClosedSuitcaseCount;
            Assert.AreEqual(expectedAverageMoneyAmount, banker.AverageMoneyAmount, 0.001f);
        }

        [UnityTest]
        public IEnumerator VerifyBankerCalculatesAverageWhenSuitcaseCountIsZero()
        {
            GameObject obj = new GameObject();
            obj.AddComponent<Banker>();
            Banker banker = obj.GetComponent<Banker>();
            yield return null;
            for(int i = 0;i < GameConstants.SUITCASE_COUNT; ++i)
            {
                banker.DecrementSuitcaseCount();
            }

            Assert.AreEqual(0, banker.ClosedSuitcaseCount);
            Assert.AreEqual(0f, banker.AverageMoneyAmount, 0.001f);
        }

        [UnityTest]
        public IEnumerator VerifyBankerUpdatesTotalMoneyAmountCorrectly()
        {
            GameObject obj = new GameObject();
            obj.AddComponent<Banker>();
            Banker banker = obj.GetComponent<Banker>();
            yield return null;

            float expectedTotalMoneyAmount = banker.TotalMoneyAmount - 100000;
            banker.ReduceTotalMoneyAmount(100000);
            Assert.AreEqual(expectedTotalMoneyAmount, banker.TotalMoneyAmount);
            
        }
    }
}
