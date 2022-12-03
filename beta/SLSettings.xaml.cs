using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
// Adding 
using Windows.Storage;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;
using Windows.System;
using Windows.UI.ApplicationSettings;
using Microsoft.Toolkit.Uwp.Helpers;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace SociaLink_Win10
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class SLSettings : Page
    {
        public SLSettings()
        {
            this.InitializeComponent();
            CustomizeTitleBar();
            // Set theme for window root
            FrameworkElement root = (FrameworkElement)Window.Current.Content;
            root.RequestedTheme = DarkLightMode.Theme;
            ElementSoundPlayer.State = ElementSoundPlayerState.On;
            SLIDEVOLUM.Value = ElementSoundPlayer.Volume * 100;
            if (ElementSoundPlayer.State == ElementSoundPlayerState.On)
                TOGGLEAUDIO.IsOn = true;
            if (ElementSoundPlayer.SpatialAudioMode == ElementSpatialAudioMode.On && TOGGLEAUDIO.IsOn == true)
                CECKBSPATIAL.IsChecked = true;
        }
        private void CustomizeTitleBar()
        {
            // Customisation de la bar de titre
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(customTitleBar);
        }

        /// <summary>
        /// Set the theme toggle to the correct position (off for the default theme, and on for the non-default).
        /// </summary>
        private void SetThemeToggle(ElementTheme theme)
        {
            if (theme == DarkLightMode.DEFAULTTHEME)
                tglAppTheme.IsOn = false;
            else
                tglAppTheme.IsOn = true;
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            FrameworkElement window = (FrameworkElement)Window.Current.Content;

            if (((ToggleSwitch)sender).IsOn)
            {
                DarkLightMode.Theme = DarkLightMode.NONDEFLTHEME;
                window.RequestedTheme = DarkLightMode.NONDEFLTHEME;
                Grid_Them_View_dark.Visibility = Visibility.Collapsed;
                Grid_Them_View_light.Visibility = Visibility.Visible;
            }
            else
            {
                DarkLightMode.Theme = DarkLightMode.DEFAULTTHEME;
                window.RequestedTheme = DarkLightMode.DEFAULTTHEME;
                Grid_Them_View_dark.Visibility = Visibility.Visible;
                Grid_Them_View_light.Visibility = Visibility.Collapsed;
            }
        }
        private void TOGGLEAUDIO_Toggled(object sender, RoutedEventArgs e)
        {
            if (TOGGLEAUDIO.IsOn == true)
            {
                CECKBSPATIAL.IsEnabled = true;
                ElementSoundPlayer.State = ElementSoundPlayerState.On;
                SLIDEVOLUM.IsEnabled = true;
                VOLUM_I_TXT.Text = "Volume de l'interaction (Ajustable)";
                soundSelection.IsEnabled = true;
                BTN_TESTEAUDIO.IsEnabled = true;
            }
            else
            {
                CECKBSPATIAL.IsEnabled = false;
                CECKBSPATIAL.IsChecked = false;

                ElementSoundPlayer.State = ElementSoundPlayerState.Off;
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.Off;
                SLIDEVOLUM.IsEnabled = false;
                VOLUM_I_TXT.Text = "Volume de l'interaction (Désactivé)";
                soundSelection.IsEnabled = false;
                BTN_TESTEAUDIO.IsEnabled = false;
            }
        }

        private void CECKBSPATIAL_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TOGGLEAUDIO.IsOn == true)
            {
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.Off;
                SLIDEVOLUM.IsEnabled = true;
                VOLUM_I_TXT.Text = "Volume de l'interaction (Ajustable)";
            }
        }

        private void CECKBSPATIAL_Checked(object sender, RoutedEventArgs e)
        {
            if (TOGGLEAUDIO.IsOn == true)
            {
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.On;
                SLIDEVOLUM.IsEnabled = false;
                VOLUM_I_TXT.Text = "Volume de l'interaction (Indisponible)";
            }
        }

        private void SLIDEVOLUM_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            var msg = String.Format("{0}%", e.NewValue);
            this.TXTSTATUSVOLUM.Text = msg + "%";

            Slider slider = sender as Slider;
            var volumeLevel = slider.Value / 100;
            ElementSoundPlayer.Volume = volumeLevel;
        }

        private void BTN_TESTEAUDIO_Click(object sender, RoutedEventArgs e)
        {
            ElementSoundPlayer.Play((ElementSoundKind)soundSelection.SelectedIndex);
        }
        private void BTN_BACK_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft });
        }

        private void Page_Loading(FrameworkElement sender, object args)
        {
            TXT_VERSION_APPS.Text = "Version : " + $"{SystemInformation.Instance.ApplicationVersion.Major}.{SystemInformation.Instance.ApplicationVersion.Minor}.{SystemInformation.Instance.ApplicationVersion.Build}.{SystemInformation.Instance.ApplicationVersion.Revision}";
        }
    }
}
