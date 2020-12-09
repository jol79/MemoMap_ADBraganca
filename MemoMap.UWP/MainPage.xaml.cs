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
using MemoMap.UWP.Views.GroupViews;
using MemoMap.UWP.Views.Map;
using MemoMap.UWP.Views.AccountSettings;
using MemoMap.UWP.ViewModels;
using Windows.UI.ApplicationSettings;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MemoMap.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel MainViewModel { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            MainViewModel = new MainViewModel();
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {

            NavigationViewItem nav_item = args.InvokedItemContainer as NavigationViewItem;
            if (nav_item != null)
            {
                // access the tag
                switch (nav_item.Tag)
                {
                    // my maps
                    case "my_maps":
                        MainFrame.Navigate(typeof(MyMapsPage));
                        break;
                    case "create_group":
                        MainFrame.Navigate(typeof(CreateGroupPage));
                        break;
                    case "view_groups":
                        MainFrame.Navigate(typeof(ViewGroupPage));
                        break;
                    case "map":
                        MainFrame.Navigate(typeof(MapPage));
                        break;
                    case "account_settings":
                        MainFrame.Navigate(typeof(AccountSettings));
                        break;
                }
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(typeof(ViewGroupPage));
        }

     
        private void ThemeChanger_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.toggleTheme();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Start building "AccountSettingsPane"
            AccountsSettingsPane.GetForCurrentView().AccountCommandsRequested += BuildPaneAsync;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //Delete building "AccountSettingsPane"
            AccountsSettingsPane.GetForCurrentView().AccountCommandsRequested -= BuildPaneAsync;
        }
        //Authentication form building function
        private async void BuildPaneAsync(AccountsSettingsPane s, AccountsSettingsPaneCommandsRequestedEventArgs e)
        {
            var deferral = e.GetDeferral();
            var msaProvider = await WebAuthenticationCoreManager.FindAccountProviderAsync("https://login.microsoft.com", "consumers");
            var command = new WebAccountProviderCommand(msaProvider, GetMsaTokenAsync);
            e.WebAccountProviderCommands.Add(command);
            deferral.Complete();
        }
        //Retrieving account data
        public async void GetMsaTokenAsync(WebAccountProviderCommand command)
        {
            WebTokenRequest request = new WebTokenRequest(command.WebAccountProvider, "wl.basic");
            WebTokenRequestResult result = await WebAuthenticationCoreManager.RequestTokenAsync(request);
            if (result.ResponseStatus == WebTokenRequestStatus.Success)
            {
                WebAccount account = result.ResponseData[0].WebAccount;
                StoreWebAccount(account);
            }
        }
        //Saving data in storage
        private void StoreWebAccount(WebAccount account)
        {
            ApplicationData.Current.LocalSettings.Values["CurrentUserProviderId"] = account.WebAccountProvider.Id;
            ApplicationData.Current.LocalSettings.Values["CurrentUserId"] = account.Id;
        }
    }
}
