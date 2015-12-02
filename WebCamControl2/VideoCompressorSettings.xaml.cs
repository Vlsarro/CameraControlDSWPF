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
using DShowNET;
using DirectX.Capture;

namespace WebCamControl2
{
    /// <summary>
    /// Interaction logic for VideoCompressorSettings.xaml
    /// </summary>
    public partial class VideoCompressorSettings : Window
    {

        Filters filters = new Filters();

        #region Enumerate Devices
        public List<string> EnumerateDevices(int mode)
        {
            if (mode == 1)
            {
                if (filters.VideoInputDevices != null)
                {
                    List<string> devices = new List<string>();
                    foreach (Filter f in filters.VideoInputDevices)
                    {
                        devices.Add(f.Name);
                    }
                    return devices;
                }
                return null;
            }
            else if (mode == 2)
            {
                if (filters.VideoCompressors != null)
                {
                    List<string> compressors = new List<string>();
                    foreach (Filter f in filters.VideoCompressors)
                    {
                        compressors.Add(f.Name);
                    }
                    return compressors;
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        #endregion

        public VideoCompressorSettings()
        {
            InitializeComponent();
            List<string> VideoCompressorsDevices = new List<string>();
            VideoCompressorsDevices = EnumerateDevices(2);
            VideoCompressorsList.ItemsSource = VideoCompressorsDevices;
            foreach (string device in VideoCompressorsDevices)
            {
                Console.WriteLine(device + "\n");
            }
        }

        private void VideoCompressorsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //if (VideoInputDevicesList.SelectedItem != null)
            //this.Title = string(VideoInputDevicesList.SelectedItem);
        }

        private void btnShowSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            foreach (string device in VideoCompressorsList.SelectedItems)
                MessageBox.Show(device);
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
    }
}
