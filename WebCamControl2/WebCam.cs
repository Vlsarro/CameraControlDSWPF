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

        Capture capture = null;
        Filters filters = null;

        int counter = 1;
        bool startEnable = true;
        private System.Windows.Forms.Panel _panel1;
        //int deviceNumber = 0;


        public WebCam(System.Windows.Forms.Panel panel1)
        {
            _panel1 = panel1;
        }

        public void Start_Preview()
        {
            filters = new Filters();

            if (filters.VideoInputDevices != null)
            {
                try
                {
                    Console.WriteLine("****************\nPREVIEW FUNCTION\n*******************");
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

        public void Preview()
        {
            //TODO
            //try
            //{
            
            if (capture != null)
            {
                capture.Stop();
                capture.PreviewWindow = null;
            }
                capture = new Capture(filters.VideoInputDevices[0], null);
                //capture.VideoCompressor = filters.VideoCompressors[0];

                capture.PreviewWindow = _panel1; //need to allocate wpf window for that

                capture.Start();
            //}
            //catch { }
        }
        public void Stop_Preview()
        {
            Console.WriteLine("****************\nSTOP PREVIEW FUNCTION\n*******************");
            capture.Stop();
            capture.PreviewWindow = null;
        }
        public void Start_Capture(bool captureEnable)
        {
            Console.WriteLine("****************\nSTART CAPTURE FUNCTION\n*******************");

            if (capture == null)
            {
                filters = new Filters();
                if (filters.VideoInputDevices != null)
                {
                    try
                    {
                        capture = new Capture(filters.VideoInputDevices[0], null);
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
                capture.VideoCompressor = filters.VideoCompressors[0];
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
    }
}
