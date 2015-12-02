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
using System.Windows.Forms;

namespace WebCamControl2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WebCam _webcam;
        int VideoInputDeviceIndex = 0;
        int VideoCompressorIndex = 0;
        //MenuCommands _mcommands;
        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Forms.Panel panel1 = new System.Windows.Forms.Panel();
            formController.Child = panel1;
            _webcam = new WebCam(panel1);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the interop host control.
            //System.Windows.Forms.Integration.WindowsFormsHost host =
              //  new System.Windows.Forms.Integration.WindowsFormsHost();

            
            // Create the MaskedTextBox control.
            //MaskedTextBox mtbDate = new MaskedTextBox("00/00/0000");

            // Assign the MaskedTextBox control as the host control's child.
            //host.Child = mtbDate;
            
            // Add the interop host control to the Grid
            // control's collection of child controls.
            //this.grid1.Children.Add(host);
        }

        private void bntStart_Click(object sender, RoutedEventArgs e)
        {

            _webcam.Start_Preview();
        }

        private void bntStop_Click(object sender, RoutedEventArgs e)
        {
            _webcam.Stop_Preview();
        }

        private void bntStartCapture_Click(object sender, RoutedEventArgs e)
        {
            _webcam.Start_Capture(true);
        }

        private void bntStopCapture_Click(object sender, RoutedEventArgs e)
        {
            _webcam.Stop_Capture();
        }

        private void VideoInputDevicesSettings_Click(object sender, RoutedEventArgs e)
        {
            //_webcam.VideoInputDevicesSettings();
            VideoInputSettings vis = new VideoInputSettings();
            vis.Show();
        }

        private void VideoCompressorsSettings_Click(object sender, RoutedEventArgs e)
        {
            VideoCompressorSettings vcs = new VideoCompressorSettings();
            vcs.Show();
        }

    }
}
