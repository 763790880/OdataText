using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextTSL;

public class Program
{
    public static void Main()
    {
        
    }


    public static int MaxBananas(string S)
    {
        int[] count = new int[1024];
        foreach (char c in S)
        {
            count[c - 'A']++;
        }
        int result = 0;
        while (true)
        {
            bool canDelete = true;
            for (int i = 0; i < 26; i++)
            {
                if (count[i] == 0) continue;
                if (i == 'B' - 'A' && count[i] >= 1
                    && count['A' - 'A'] >= 3
                    && count['N' - 'A'] >= 2)
                {
                    count[i]--;
                    count['A' - 'A'] -= 3;
                    count['N' - 'A'] -= 2;
                    result++;
                    canDelete = true;
                    break;
                }
                else
                {
                    canDelete = false;
                }
            }
            if (!canDelete) break;
        }

        return result;
    }

    /// <summary>
    /// 香蕉答案
    /// </summary>
    /// <param name="S"></param>
    /// <returns></returns>
    public static int MaxBananas1(string S)
    {
        int countB = 0, countA = 0, countN = 0;
        foreach (char c in S)
        {
            if (c == 'B') countB++;
            else if (c == 'A') countA++;
            else if (c == 'N') countN++;
        }
        return Math.Min(Math.Min(countB, countA / 3), countN / 2);
    }

    /// <summary>
    /// 查询偶数次数答案
    /// </summary>
    /// <param name="S"></param>
    /// <returns></returns>
    public static int LongestEvenCharacterSubstring(string S)
    {
        int N = S.Length;
        int result = 0;
        Dictionary<int, int> evenCount = new Dictionary<int, int>();
        evenCount.Add(0, -1);
        int mask = 0;
        for (int i = 0; i < N; i++)
        {
            int c = S[i] - 'a';
            mask ^= (1 << c);
            if (evenCount.ContainsKey(mask))
            {
                result = Math.Max(result, i - evenCount[mask]);
            }
            else
            {
                evenCount.Add(mask, i);
            }
        }
        return result;
    }




}
