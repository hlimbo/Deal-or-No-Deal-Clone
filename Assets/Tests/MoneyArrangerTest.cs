using System.Collections;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class MoneyArrangerTest
    {
        // A Test behaves as an ordinary method
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
    }
}
