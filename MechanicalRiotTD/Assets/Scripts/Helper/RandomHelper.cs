using UnityEngine;

static class RandomHelper
{

    #region -- 方法參考區 --

    public static float RandomRangeSubSpecificRange(float minInclusive, float maxInclusive, float minSubValue, float maxSubValue)
    {

        if(minSubValue < minInclusive || maxSubValue > maxInclusive)
        {
            Debug.LogError("輸入錯誤!");
            return 0f;
        }

        float randomValue;
        if (Random.value > 0.5f)
        {
            randomValue = Random.Range(maxSubValue, maxInclusive);
        }
        else
        {
            randomValue = Random.Range(minInclusive, minSubValue);
        }

        return randomValue;

    }

    #endregion

}
