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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CameraControl
{
    /// <summary>
    /// Interaction logic for MainUserControl.xaml
    /// </summary>
    public partial class MainUserControl : UserControl
    {
        #region Initialization
        Camera _camera1;
        Camera _camera2;
        VideoSettings _settings;
        #endregion
        public MainUserControl()
        {
            InitializeComponent();

            _settings = new VideoSettings();
            //_settings.Load();

            //first video source
            System.Windows.Forms.Panel panel1 = new System.Windows.Forms.Panel();
            formController1.Child = panel1;
            _camera1 = new Camera(_settings, panel1, "camera1");

            //second video source
            System.Windows.Forms.Panel panel2 = new System.Windows.Forms.Panel();
            formController2.Child = panel2;
            _camera2 = new Camera(_settings, panel2, "camera2");
        }
        
        private void bntStartPreview_Click(object sender, RoutedEventArgs e)
        {

            //foreach camera in cameras
            //camera.StartPreview()
            _camera1.StartPreview();
            //_camera2.StartPreview();
            if (_camera1.isPreviewing())
            {
                PreviewIndicator.Fill = new SolidColorBrush(Colors.Red);
            }
        }

        private void bntStopPreview_Click(object sender, RoutedEventArgs e)
        {
            _camera1.StopPreview();
            //_camera2.StopPreview();
            if (!_camera1.isPreviewing())
            {
                PreviewIndicator.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF551812"));
            }
        }

        private void bntStopCapture_Click(object sender, RoutedEventArgs e)
        {
            _camera1.StopCapture();
            //_camera2.StopCapture();
            if (!_camera1.isCapturing())
            {
                CaptureIndicator.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF16511B"));
            }
        }

        private void bntStartCapture_Click(object sender, RoutedEventArgs e)
        {
            _camera1.StartCapture();
            //_camera2.StartCapture();
            if (_camera1.isPreviewing())
            {
                PreviewIndicator.Fill = new SolidColorBrush(Colors.Red);
            }
            if(_camera1.isCapturing())
            {
                CaptureIndicator.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF08F71C"));
            }
        }

        private void VideoInputDevicesSettings_Click(object sender, RoutedEventArgs e)
        {
            VideoInputSettings vis = new VideoInputSettings(_settings);
            vis.Show();
            //camera.Settings = _settings;
        }

        private void VideoCompressorsSettings_Click(object sender, RoutedEventArgs e)
        {
            VideoCompressorSettings vcs = new VideoCompressorSettings(_settings);
            vcs.Show();
            //camera.Settings = _settings;
        }
    }
}
