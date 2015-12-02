using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectX.Capture;
using DShowNET;
using System.Windows;

namespace WebCamControl2
{
    class WebCam
    {

        #region Initialization
        int counter = 1;
        int VideoInputDeviceIndex = 0; 
        int VideoCompressorIndex = 0;
        private System.Windows.Forms.Panel _panel1;
        //int deviceNumber = 0;
        public WebCam(System.Windows.Forms.Panel panel1)
        {
            _panel1 = panel1;
        }

        Filters filters = null;
        Capture capture = null;
        #endregion

        #region Settings
        //1 stands for video devices, 2 stands for compressors
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
        public void printDevicesAndCompressors()
        {
            List<string> devices = new List<string>();
            List<string> compressors = new List<string>();
            devices = EnumerateDevices(1);
            compressors = EnumerateDevices(2);
            //Firstly print out video devices
            foreach(string device in devices)
            {
                Console.WriteLine(device + "\n");
            }
            Console.WriteLine("\n");
            foreach (string compressor in compressors)
            {
                Console.WriteLine(compressor + "\n");
            }
        }
        #endregion

        #region Menu Commands
        public void VideoInputDevicesSettings()
        {
            
        }

        public void VideoCompressorsSettings()
        {

        }
        #endregion

        #region Video functions
        public void Preview()
        {
            if (capture != null)
            {
                capture.Stop();
                capture.PreviewWindow = null;
            }
            capture = new Capture(filters.VideoInputDevices[VideoInputDeviceIndex], null);

            capture.PreviewWindow = _panel1; 

            capture.Start();
        }
        bool startEnable = true;
        public void Start_Preview()
        {
            filters = new Filters();

            //printDevicesAndCompressors();

            if (filters.VideoInputDevices != null)
            {
                try
                {
                    Console.WriteLine("****************\nPREVIEW FUNCTION\n*******************");
                    Console.WriteLine("****************\nUSING VIDEO INPUT DEVICE: " + filters.VideoInputDevices[VideoInputDeviceIndex].Name +
                                                        "\n*******************");
                    Preview();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Maybe any other software is already using your device. \n\n Error Message: \n\n" + ex);
                }
            }
            else
            {
                startEnable = false;
                MessageBox.Show("No video device connected to you PC!");
            }
        }

        public void Stop_Preview()
        {
            Console.WriteLine("****************\nSTOP PREVIEW FUNCTION\n*******************");
            capture.Stop();
            capture.PreviewWindow = null;
        }
        public void Start_Capture(bool captureEnable)
        {
            // Solved bug with compressor and previewing video in Capture mode after StartP-StopP sequence
            Console.WriteLine("****************\nSTART CAPTURE FUNCTION\n*******************");

            if (capture == null)
            {
                filters = new Filters();
                if (filters.VideoInputDevices != null)
                {
                    try
                    {
                        capture = new Capture(filters.VideoInputDevices[VideoInputDeviceIndex], null);
                        capture.VideoCompressor = filters.VideoCompressors[VideoCompressorIndex];
                        Console.WriteLine("****************\nUSING COMPRESSOR: " + filters.VideoCompressors[VideoCompressorIndex].Name +
                                                        "\n*******************");
                        capture.PreviewWindow = _panel1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Maybe any other software is already using your device. \n\n Error Message: \n\n" + ex);
                    }
                }
                else
                {
                    startEnable = false;
                    MessageBox.Show("No video device connected to you PC!");
                }
            }
            else
            {
                capture.Stop();
                Console.WriteLine("****************\nUSING COMPRESSOR: " + filters.VideoCompressors[VideoCompressorIndex].Name +
                                                        "\n*******************");
                capture.VideoCompressor = filters.VideoCompressors[VideoCompressorIndex];
                capture.PreviewWindow = _panel1;
            }

            if (captureEnable == true)
            {
                counter++;
                if (!capture.Cued) capture.Filename = counter + ".avi";
                capture.Cue();
            }
            capture.Start();
        }
        public void Stop_Capture()
        {
            Console.WriteLine("****************\nSTOP CAPTURE FUNCTION\n*******************");
            capture.Stop();
            capture.PreviewWindow = null;
        }
        #endregion
    }
}
