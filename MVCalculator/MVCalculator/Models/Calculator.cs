using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCalculator.Models
{
    public class Calculator
    {
        [Key]
        public int res { get; set; }

        // Operates the addition
        public int add(List<int> nums)
        {
            int res = 0;

            foreach(int n in nums)
            {
                res += n;
            }

            return res;
        }

        // Operates the subtraction
        public int sub(int min, int sub)
        {
            if(min == 0 || sub == 0)
            {
                throw new NullReferenceException("");
            }

            int res = min - sub;

            return res;
        }

        // Operates the multiply
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

        // Operates the division
        public int div(int divd, int divs)
        {
            int res;
            if (divd == 0 || divs == 0)
            {
                throw new NullReferenceException("");
            }

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

        // Gets the remainder
        public int rem(int divd, int divs)
        {
            int res;
            if (divd == 0 || divs == 0)
            {
                throw new NullReferenceException("");
            }

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

        // Operates the square root
        public int sqrt(int num)
        {
            double res;
            if (num == 0)
            {
                throw new NullReferenceException("");
            }

            res = Math.Sqrt(Convert.ToDouble(num));


            return Convert.ToInt32(res);
        }
    }
}
