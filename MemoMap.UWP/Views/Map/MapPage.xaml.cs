﻿using System;
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
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;



namespace MemoMap.UWP.Views.Map
{
    public sealed partial class MapPage : Page
    {
        public MapPage()
        {
            this.InitializeComponent();
        }

        private void MemoMap_MapTapped(MapControl sender, MapInputEventArgs args)
        {
            var GeoPosition = args.Location.Position;

            latitude.Text = GeoPosition.Latitude.ToString();
            longtitute.Text = GeoPosition.Longitude.ToString();

            // change button visibility
            AddPoint.IsEnabled = true;
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            var landmarks = new List<MapElement>();
            
            double lat = Convert.ToDouble(latitude.Text);
            double longt = Convert.ToDouble(longtitute.Text);

            // spec location
            BasicGeoposition pos = new BasicGeoposition { Latitude = lat, Longitude = longt };
            Geopoint position = new Geopoint(pos);

            var spaceNeedleIcon = new MapIcon
            {
                Location = position,
                NormalizedAnchorPoint = new Point(0.5, 1.0),
                ZIndex = 0,
                Title = "New Point"
            };

            landmarks.Add(spaceNeedleIcon);

            var LandMarksLayer = new MapElementsLayer
            {
                ZIndex = 1,
                MapElements = landmarks
            };

            MemoMap.Layers.Add(LandMarksLayer);

            MemoMap.Center = position;
            MemoMap.ZoomLevel = 14;
        }
    }
}
