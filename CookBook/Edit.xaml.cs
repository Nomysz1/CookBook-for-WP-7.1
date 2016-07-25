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
    public partial class Edit : PhoneApplicationPage
    {
        int index;
        List<string> list;
        List<string> mirror;
        Stack<Line> lines;
        Canvas Selected;
        bool isSelected;

        public Edit()
        {
            InitializeComponent();

            index = 0;
            list = new List<string>();
            mirror = new List<string>();
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
            this.btnSelect.Click += new RoutedEventHandler(btn_Click);
            this.btnSave.Click += new RoutedEventHandler(btn_Click);
            this.TbSearch.TextChanged += new TextChangedEventHandler(TbSearch_TextChanged);
        }

        void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = TbSearch.Text;
            if (text != "")
            {
                list = Search.Text(this.mirror, text);
                lbTypes.Items.Clear();
                foreach (string element in list)
                    lbTypes.Items.Add(element);
            }
            else
            {
                lbTypes.Items.Clear();
                foreach (string element in mirror)
                    lbTypes.Items.Add(element);
            }
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (isSelected == true)
                Draw.DeletePreviusObjects(Selected, lines);

            if (btn.Name == "btnchristmas" || btn.Name == "btndessert" || btn.Name == "btnfish" || btn.Name == "btnpasta" || btn.Name == "btnpizza"
     || btn.Name == "btnquickandeasy" || btn.Name == "btnsalad" && this.stdet.Visibility != System.Windows.Visibility.Collapsed)
            {
                this.stconf.Visibility = Visibility.Visible;
                this.stdet.Visibility = Visibility.Collapsed;
            }

            switch (btn.Name)
            {
                case "btnchristmas":
                    Draw.SetSelectedCanvas(out Selected, Christmas, out isSelected);
                    Draw.Lines(Christmas, ref lines);
                    XML.UpdateList(lbTypes, Selected);
                    Copy.DoIt(lbTypes, ref mirror);
                    break;
                case "btndessert":
                    Draw.SetSelectedCanvas(out Selected, dessert, out isSelected);
                    Draw.Lines(dessert, ref lines);
                    XML.UpdateList(lbTypes, Selected);
                    Copy.DoIt(lbTypes, ref mirror);
                    break;
                case "btnfish":
                    Draw.SetSelectedCanvas(out Selected, fish, out isSelected);
                    Draw.Lines(fish, ref lines);
                    XML.UpdateList(lbTypes, Selected);
                    Copy.DoIt(lbTypes, ref mirror);
                    break;
                case "btnpasta":
                    Draw.SetSelectedCanvas(out Selected, pasta, out isSelected);
                    Draw.Lines(pasta, ref lines);
                    XML.UpdateList(lbTypes, Selected);
                    Copy.DoIt(lbTypes, ref mirror);
                    break;
                case "btnpizza":
                    Draw.SetSelectedCanvas(out Selected, pizza, out isSelected);
                    Draw.Lines(pizza, ref lines);
                    XML.UpdateList(lbTypes, Selected);
                    Copy.DoIt(lbTypes, ref mirror);
                    break;
                case "btnquickandeasy":
                    Draw.SetSelectedCanvas(out Selected, quickandeasy, out isSelected);
                    Draw.Lines(quickandeasy, ref lines);
                    XML.UpdateList(lbTypes, Selected);
                    Copy.DoIt(lbTypes, ref mirror);
                    break;
                case "btnsalad":
                    Draw.SetSelectedCanvas(out Selected, salad, out isSelected);
                    Draw.Lines(salad, ref lines);
                    XML.UpdateList(lbTypes, Selected);
                    Copy.DoIt(lbTypes, ref mirror);
                    break;
                case "btnSelect":
                    if (this.lbTypes.SelectedIndex != -1)
                    {
                        index = lbTypes.SelectedIndex;
                        string temporary = lbTypes.Items[index].ToString();
                        for (int i = 0; i < mirror.Count; ++i)
                            if (mirror[i] == temporary)
                            {
                                index = i;
                                break;
                            }
                        this.stconf.Visibility = Visibility.Collapsed;
                        this.stdet.Visibility = Visibility.Visible;
                        string title, ingredients, preparation;
                        XML.ShowDetails(index, Selected, out title, out ingredients, out preparation);
                        this.tbTitle.Text = title;
                        this.tbIngredients.Text = ingredients;
                        this.tbPreparation.Text = preparation;
                    }
                    break;
                case "btnSave":
                    XML.SaveSelectedNode(index, Selected, tbTitle, tbIngredients, tbPreparation);
                    this.stconf.Visibility = Visibility.Visible;
                    this.stdet.Visibility = Visibility.Collapsed;
                    this.lbTypes.Items.Clear();
                    break;
            }
            this.TbSearch.Text = "";
        }

        void tb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = sender as TextBox;
            SolidColorBrush scb = new SolidColorBrush(Colors.Black);
            tb.Foreground = scb;
        }
    }
}