using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Jiesen.ConsoleApp
{
    public class Test
    {
        public void Resolve()
        {

        }

        public static void QuickSort(int[] arrays, int low, int high)
        {
            if (low >= high)
            {
                return;
            }

            int left = low, right = high, current = arrays[low];

            while (left < right)
            {
                while (left < right && current <= arrays[right])
                {
                    right--;
                }

                if (left < right)
                {
                    arrays[left++] = arrays[right];
                }

                while (left < right && arrays[left] < current)
                {
                    left++;
                }
                if (left < right)
                {
                    arrays[right--] = arrays[left];
                }
            }

            arrays[left] = current;
            QuickSort(arrays, low, left - 1);
            QuickSort(arrays, left + 1, high);
        }

        public static int BisectionAlgorithm(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1; // 注意

            while (left <= right)
            {
                int mid = (right + left) / 2;
                if (nums[mid] == target)
                    return mid;
                else if (nums[mid] < target)
                    left = mid + 1; // 注意
                else if (nums[mid] > target)
                    right = mid - 1; // 注意
            }
            return -1;
        }
    }
}
