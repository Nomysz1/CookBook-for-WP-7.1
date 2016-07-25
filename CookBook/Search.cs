using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace CookBook
{
    public class Search
    {
        public static List<string> Text(List<string> list, string words)
        {
            List<string> matched = new List<string>();

            foreach (string element in list)
                if (element.Contains(words))
                    matched.Add(element);

            return matched;
        }
    }
}
