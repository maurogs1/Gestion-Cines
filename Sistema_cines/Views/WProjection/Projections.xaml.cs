using BaseClass;
using BaseClass.DataAccess;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Views.WProjection
{
    /// <summary>
    /// Lógica de interacción para Projections.xaml
    /// </summary>
    public partial class Projections : UserControl
    {
        WorkProjection wp;
        User currentUser;
        List<Projection> projections = new List<Projection>();
        List<Projection> filteredProjections = new List<Projection>();
        List<Projection> firstPage = new List<Projection>();
        List<Projection> secondPage = new List<Projection>();
        public static List<Projection> viewProjections = new List<Projection>();

        public Projections()
        {
            InitializeComponent();
            LoadProjections();
        }

        public Projections(User user)
        {
            InitializeComponent();
            currentUser = user;
            if (currentUser.RolId == 2)
            {
                BtnAdd.Visibility = Visibility.Collapsed;
            }
            LoadProjections();
            SetDates();
            FilterProjections();
            
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ModalProjection modalProjection = new ModalProjection();
            modalProjection.Show();
        }
        private void BtnGridStyle_Click(object sender, RoutedEventArgs e)
        {
            LoadProjections();
            viewProjections = projections;
            ListViewMovies.ItemsSource = projections;
        }

        private void LoadProjections()
        {
            wp = new WorkProjection();
            projections = wp.GetAll();
        }

        private void FilterProjections()
        {
            filteredProjections = new List<Projection>();
            foreach(Projection p in projections)
            {
                if (p.Date.CompareTo(dtpStart.Value) > 0)
                {
                    if (p.Date.CompareTo(dtpEnd.Value) < 0)
                    {
                        filteredProjections.Add(p);
                    }
                }
            }
            viewProjections = filteredProjections;
            ListViewMovies.ItemsSource = viewProjections;
        }

        private void SetDates()
        {
            string format = "dd-MM-yyyy  HH:mm ";
            dtpStart.Format = Xceed.Wpf.Toolkit.DateTimeFormat.Custom;
            dtpStart.FormatString = format;
            dtpStart.Value = DateTime.Now;
            dtpEnd.Format = Xceed.Wpf.Toolkit.DateTimeFormat.Custom;
            dtpEnd.FormatString = format;
            dtpEnd.Value = DateTime.Now.AddDays(7);
        }

        private void DtpStart_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            FilterProjections();
        }

        private void DtpEnd_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            FilterProjections();
        }
    }
}
