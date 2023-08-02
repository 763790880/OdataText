using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TextTSL;

public class Program
{
    public static void Main()
    {
        JArray array = new JArray();
        array.Add(new JObject());
        List<string> list = new List<string>();
        for (int i = 0; i < 50; i++)
        {
            list.Add(i.ToString());
        }
        //list.AsParallel().ForAll(x => {
        //    Console.WriteLine($"我是值{x}");
        //    Thread.Sleep(500);
        //});
        string str = "123";
        Console.WriteLine("我执行完毕了---------------------------------");
        //int[] arry=new int[10] { 2,5,1,2,6,7,8,9,10,23};
        //Console.WriteLine(string.Join(",", arry));
        //QuickSort.Sort(arry);
        //Console.WriteLine(string.Join(",", arry));
        //str=str.Insert(1, "我擦").Insert(2, "牛皮");
        //Console.WriteLine(str);
        //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        //sw.Start();
        //Console.WriteLine("开始执行！");
        //Thread.Sleep(1000);
        //Console.WriteLine($"已经执行了{sw.Elapsed.TotalSeconds}秒");
        //sw.Restart();
        //Thread.Sleep(1000);
        //Console.WriteLine($"又经执行了{sw.Elapsed.TotalSeconds}秒");

        DateTime adate = DateTime.Parse("2023/7/3");
        DateTime bdate = DateTime.Parse("2023-03-01");
        var days=(adate - bdate).TotalDays;
        var val=(days + 90) * 1;
        Console.WriteLine( $"我是最总值{val}" );
    }

    public static IEnumerable<string> Strings()
    {
        List<string> strings = new List<string>();
        strings.Add("23");
        foreach (var item in strings)
        {
            yield return item.ToString();
        }
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
public class QuickSort
{
    public static void Sort(int[] array)
    {
        if (array == null || array.Length == 0)
            return;

        QuickSortRecursive(array, 0, array.Length - 1);
    }

    private static void QuickSortRecursive(int[] array, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(array, left, right);

            QuickSortRecursive(array, left, pivotIndex - 1);
            QuickSortRecursive(array, pivotIndex + 1, right);
        }
    }

    private static int Partition(int[] array, int left, int right)
    {
        int pivot = array[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (array[j] < pivot)
            {
                i++;
                Swap(array, i, j);
            }
        }

        Swap(array, i + 1, right);
        return i + 1;
    }

    private static void Swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
}

