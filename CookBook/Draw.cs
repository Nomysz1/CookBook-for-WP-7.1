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
    public class Draw
    {
        public static void SetSelectedCanvas(out Canvas cnvs, Canvas cnvs1, out bool isSelected)
        {
            cnvs = cnvs1;
            isSelected = true;
        }
        public static void DeletePreviusObjects(Canvas cnvs, Stack<Line> lines)
        {
            foreach (Line line in lines)
                cnvs.Children.Remove(line);
        }
        public static void Lines(Canvas cnv, ref Stack<Line> lines)
        {
            SolidColorBrush brush = new SolidColorBrush(Colors.Red);
            Line myLine = new Line();
            Line myLine2 = new Line();
            myLine2.Stroke = brush;
            myLine.Stroke = brush;
            myLine.X1 = 90;
            myLine.X2 = 50;
            myLine.Y1 = 35;
            myLine.Y2 = 80;
            myLine.StrokeThickness = 6;
            cnv.Children.Add(myLine);
            myLine2.X1 = 51;
            myLine2.Y1 = 80;
            myLine2.X2 = 30;
            myLine2.Y2 = 50;
            myLine2.StrokeThickness = 6;
            cnv.Children.Add(myLine2);
            lines.Push(myLine);
            lines.Push(myLine2);
        }
    }
}
