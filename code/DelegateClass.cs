using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDP_Project
{
    public delegate Dictionary<string, double[]> DelegateDictionary();
    public delegate double[] DelegateArray();

    class DelegateClass
    {
        private static Dictionary<string, double[]> dictionary;
        private static double[] array;

        public static Dictionary<string, double[]> getDictionary()
        {
            return dictionary;
        }

        public static double[] getArray()
        {
            return array;
        }

        public static void setDictionary(Dictionary<string, double[]> dictionary)
        {
            DelegateClass.dictionary = dictionary;
        }

        public static void setArray(double[] arr) { 
           array = arr;
        }
    }
}
