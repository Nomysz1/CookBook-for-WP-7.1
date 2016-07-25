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
using Microsoft.Phone.Tasks;

namespace CookBook
{
    public partial class SendEmail : PhoneApplicationPage
    {
        List<string> list;
        List<string> mirror;
        Stack<Line> lines;
        Canvas Selected;
        bool isSelected;

        EmailAddressChooserTask emailaddr;

        public SendEmail()
        {
            InitializeComponent();

            list = new List<string>();
            mirror = new List<string>();
            lines = new Stack<Line>();
            Selected = new Canvas();
            Selected = null;
            isSelected = false;

            emailaddr = new EmailAddressChooserTask();

            this.emailaddr.Completed += new EventHandler<EmailResult>(emailaddr_Completed);

            this.btnchristmas.Click += new RoutedEventHandler(btn_Click);
            this.btndessert.Click += new RoutedEventHandler(btn_Click);
            this.btnfish.Click += new RoutedEventHandler(btn_Click);
            this.btnpasta.Click += new RoutedEventHandler(btn_Click);
            this.btnpizza.Click += new RoutedEventHandler(btn_Click);
            this.btnquickandeasy.Click += new RoutedEventHandler(btn_Click);
            this.btnsalad.Click += new RoutedEventHandler(btn_Click);
            this.btnConfirm.Click += new RoutedEventHandler(btn_Click);
            this.btnBack.Click += new RoutedEventHandler(btn_Click);
            this.btnSend.Click += new RoutedEventHandler(btn_Click);
            this.TbAddr.Tap += new EventHandler<GestureEventArgs>(TbAddr_Tap);
            this.TbSearch.TextChanged += new TextChangedEventHandler(TbSearch_TextChanged);
        }

        void TbAddr_Tap(object sender, GestureEventArgs e)
        {
            emailaddr.Show();
        }

        void emailaddr_Completed(object sender, EmailResult e)
        {
            if (e.TaskResult == TaskResult.OK)
                this.TbAddr.Text = e.Email;
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
                case "btnConfirm":
                    if (this.lbTypes.SelectedIndex != -1)
                    {
                        int index = lbTypes.SelectedIndex;
                        string temporary = lbTypes.Items[index].ToString();
                        for (int i = 0; i < mirror.Count; ++i)
                            if (mirror[i] == temporary)
                            {
                                index = i;
                                break;
                            }
                        string title, ingredients, preparation;
                        XML.ShowDetails(index, Selected, out title, out ingredients, out preparation);
                        this.tbTitle.Text = title;
                        string content = String.Format("Ingredients : {0}\nThe preparation : {1}", ingredients, preparation);
                        this.tbContent.Text = content;
                        this.stDetails1.Visibility = System.Windows.Visibility.Collapsed;
                        this.stDetails2.Visibility = System.Windows.Visibility.Collapsed;
                        this.stconf.Visibility = Visibility.Collapsed;
                        this.stdet.Visibility = Visibility.Visible;
                    }
                    break;
                case "btnBack":
                    this.stDetails1.Visibility = System.Windows.Visibility.Visible;
                    this.stDetails2.Visibility = System.Windows.Visibility.Visible;
                    this.stconf.Visibility = Visibility.Visible;
                    this.stdet.Visibility = Visibility.Collapsed;
                    this.mirror.Clear();
                    this.lbTypes.Items.Clear();
                    break;
                case "btnSend":
                    EmailComposeTask email = new EmailComposeTask();
                    email.To = TbAddr.Text;
                    email.Subject = tbTitle.Text;
                    email.Body = tbContent.Text;
                    email.Show();
                    break;
            }
            this.TbSearch.Text = "";
        }
    }
}