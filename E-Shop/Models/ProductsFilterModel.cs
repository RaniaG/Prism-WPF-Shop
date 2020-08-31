using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Models
{
    public class ProductsFilterModel:BindableBase
    {
        private int minValue;
        public int MinValue
        {
            get { return minValue; }
            set { SetProperty(ref minValue, value); }
        }
        private int maxValue;
        public int MaxValue
        {
            get { return maxValue; }
            set { SetProperty(ref maxValue, value); }
        }
        private int currentMinValue;
        public int CurrentMinValue
        {
            get { return currentMinValue; }
            set { SetProperty(ref currentMinValue, value); }
        }
        private int currentMaxValue;
        public int CurrentMaxValue
        {
            get { return currentMaxValue; }
            set { SetProperty(ref currentMaxValue, value); }
        }
    }
}
