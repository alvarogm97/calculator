using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVCalculator.Models
{
    public class Calculator
    {
        [Key]
        public int res { get; set; }

        public int add(List<int> nums)
        {
            int res = 0;

            foreach(int n in nums)
            {
                res += n;
            }

            return res;
        }

        public int sub(int min, int sub)
        {
            int res = min - sub;

            return res;
        }

        public int mult(List<int> nums)
        {
            int res = 1;

            if (nums.Count == 0)
            {
                res = 0;
            }
            else
            {
                foreach (int n in nums)
                {
                    res *= n;
                }
            }

            return res;
        }

        public int div(int divd, int divs)
        {
            int res;

            if (divs != 0)
            {
                res = divd / divs;
            }
            else
            {
                res = 0;
            }

            return res;
        }

        public int rem(int divd, int divs)
        {
            int res;

            if (divs != 0)
            {
                res = divd % divs;
            }
            else
            {
                res = 0;
            }

            return res;
        }

        public int sqrt(int num)
        {
            double res;

            res = Math.Sqrt(Convert.ToDouble(num));


            return Convert.ToInt32(res);
        }
    }
}
