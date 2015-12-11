using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DirectX.Capture;
using DShowNET;
using System.Collections.ObjectModel;

namespace CameraControl
{
    /// <summary>
    /// Interaction logic for VideoCompressorSettings.xaml
    /// </summary>
    public partial class VideoCompressorSettings : Window
    {
        Filters filters = new Filters();
        ObservableCollection<Filter> _compressors = new ObservableCollection<Filter>();
        ObservableCollection<Filter> VideoComperssorsList = new ObservableCollection<Filter>();
        private VideoSettings _settings;

        private void GetCompressors()
        {
            _compressors.Clear();
            foreach (Filter f in filters.VideoCompressors)
                _compressors.Add(f);
        }


        public VideoCompressorSettings(VideoSettings settings)
        {
            // TODO: Complete member initialization
            this._settings = settings;
            GetCompressors();
            VideoCompressorsList.ItemsSource = _compressors;
        }

        private void VideoCompressorsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //if (VideoInputDevicesList.SelectedItem != null)
            //this.Title = string(VideoInputDevicesList.SelectedItem);
        }

        private void btnShowSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            foreach (Filter device in VideoCompressorsList.SelectedItems)
                MessageBox.Show(string.Format("name {0}, MonikerString {1}", device.Name, device.MonikerString));
        }

        private void btnSelectLast_Click(object sender, RoutedEventArgs e)
        {
            VideoCompressorsList.SelectedIndex = VideoCompressorsList.Items.Count - 1;
        }

        private void btnSelectNext_Click(object sender, RoutedEventArgs e)
        {
            int nextIndex = 0;
            if ((VideoCompressorsList.SelectedIndex >= 0) && (VideoCompressorsList.SelectedIndex < (VideoCompressorsList.Items.Count - 1)))
                nextIndex = VideoCompressorsList.SelectedIndex + 1;
            VideoCompressorsList.SelectedIndex = nextIndex;
        }

        private void btnSelectSave_Click(object sender, RoutedEventArgs e)
        {
            if (VideoCompressorsList.SelectedValue != null)
               _settings.VideoCompressorIndex = (int)VideoCompressorsList.SelectedValue;
            _settings.Save();
        }
    }

}
