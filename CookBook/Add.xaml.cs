using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace CookBook
{
    public partial class Add : PhoneApplicationPage
    {
        Stack<Line> lines;
        Canvas Selected;
        bool isSelected;

        public Add()
        {
            InitializeComponent();

            tbIngredients.Text = "Ingredients";
            tbPreparation.Text = "Preparation";
            tbTitle.Text = "Title";

            lines = new Stack<Line>();
            Selected = new Canvas();
            Selected = null;
            isSelected = false;

            this.btnchristmas.Click += new RoutedEventHandler(btn_Click);
            this.btndessert.Click += new RoutedEventHandler(btn_Click);
            this.btnfish.Click += new RoutedEventHandler(btn_Click);
            this.btnpasta.Click += new RoutedEventHandler(btn_Click);
            this.btnpizza.Click += new RoutedEventHandler(btn_Click);
            this.btnquickandeasy.Click += new RoutedEventHandler(btn_Click);
            this.btnsalad.Click += new RoutedEventHandler(btn_Click);
            this.btnConfirm.Click +=new RoutedEventHandler(btn_Click);

            this.tbIngredients.MouseLeftButtonDown += new MouseButtonEventHandler(tb_MouseLeftButtonDown);
            this.tbPreparation.MouseLeftButtonDown += new MouseButtonEventHandler(tb_MouseLeftButtonDown);
            this.tbTitle.MouseLeftButtonDown += new MouseButtonEventHandler(tb_MouseLeftButtonDown);
        }

        void tb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.Text = "";
            SolidColorBrush scb = new SolidColorBrush(Colors.Black);
            tb.Foreground = scb;
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (isSelected == true)
                Draw.DeletePreviusObjects(Selected, lines);

            switch (btn.Name)
            {
                case "btnchristmas":
                    Draw.SetSelectedCanvas(out Selected, Christmas, out isSelected);
                    Draw.Lines(Christmas, ref lines);
                    break;
                case "btndessert":
                    Draw.SetSelectedCanvas(out Selected, dessert, out isSelected);
                    Draw.Lines(dessert, ref lines);
                    break;
                case "btnfish":
                    Draw.SetSelectedCanvas(out Selected, fish, out isSelected);
                    Draw.Lines(fish, ref lines);
                    break;
                case "btnpasta":
                    Draw.SetSelectedCanvas(out Selected, pasta, out isSelected);
                    Draw.Lines(pasta, ref lines);
                    break;
                case "btnpizza":
                    Draw.SetSelectedCanvas(out Selected, pizza, out isSelected);
                    Draw.Lines(pizza, ref lines);
                    break;
                case "btnquickandeasy":
                    Draw.SetSelectedCanvas(out Selected, quickandeasy, out isSelected);
                    Draw.Lines(quickandeasy, ref lines);
                    break;
                case "btnsalad":
                    Draw.SetSelectedCanvas(out Selected, salad, out isSelected);
                    Draw.Lines(salad, ref lines);
                    break;
                case "btnConfirm":
                    data data_ = new data();
                    if (tbIngredients.Text.Length != 0 && tbPreparation.Text.Length != 0 && tbTitle.Text.Length != 0 && Selected != null)
                    {
                        data_.date = DateTime.Now.ToString();
                        data_.type = Selected.Name;
                        data_.title = tbTitle.Text;
                        data_.ingredients = tbIngredients.Text;
                        data_.preparation = tbPreparation.Text;

                        XML.AddNode(ref data_);

                        tbIngredients.Text = "Ingredients";
                        tbPreparation.Text = "Preparation";
                        tbTitle.Text = "Title";
                    }
                    else
                    {
                        MessageBox.Show("You did not fill all of data");
                    }
                    break;
            }
        }
    }
}