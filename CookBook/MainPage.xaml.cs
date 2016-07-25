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
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            XML.CreateNewXMLFile();

            btnAdd.Click += new RoutedEventHandler(btn_Click);
            btnSend.Click += new RoutedEventHandler(btn_Click);
            btnReview.Click += new RoutedEventHandler(btn_Click);
            btnEdit.Click += new RoutedEventHandler(btn_Click);
            btnDelete.Click += new RoutedEventHandler(btn_Click);
            btnAbout.Click += new RoutedEventHandler(btn_Click);
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case "btnReview":
                    NavigationService.Navigate(new Uri("/Review.xaml", UriKind.Relative));
                    break;
                case "btnSend":
                    NavigationService.Navigate(new Uri("/SendEmail.xaml", UriKind.Relative));
                    break;
                case "btnAdd":
                    NavigationService.Navigate(new Uri("/Add.xaml", UriKind.Relative));
                    break;
                case "btnEdit":
                    NavigationService.Navigate(new Uri("/Edit.xaml", UriKind.Relative));
                    break;
                case "btnDelete":
                    NavigationService.Navigate(new Uri("/Delete.xaml", UriKind.Relative));
                    break;
                case "btnAbout":
                    NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
                    break;
            }
        }
    }
}