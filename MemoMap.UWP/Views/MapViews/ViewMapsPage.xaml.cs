﻿using MemoMap.Domain.Models;
using MemoMap.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MemoMap.UWP.Views.MapViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewMapsPage : Page
    {
        public MapViewModel MapViewModel { get; set; }

        public ViewMapsPage()
        {
            this.InitializeComponent();
            MapViewModel = new MapViewModel();
        }

        private async void deleteMap_Click(object sender, RoutedEventArgs e)
        {
            // check if data exists in the database, if true delete
            if(sender is FrameworkElement b && b.DataContext is Map map)
            {
                await MapViewModel.DeleteAsync(map);
            }
        }

        private void editMap_Click(object sender, RoutedEventArgs e)
        {
            // check if data exists in the database, if true navigate
            if (sender is FrameworkElement b && b.DataContext is Map map)
            {
                MapViewModel.Map = map;
                this.Frame.Navigate(typeof(CreateMapPage), map);
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await MapViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }

        private void createNewMap_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateMapPage));
        }
    }
}
