using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectX.Capture;
using DShowNET;
using System.Windows;
using System.Threading;

namespace CameraControl
{
    class Camera
    {
        #region Initialization
        int FileNameCounter = 0;
        string _FileNamePrepender;

        VideoSettings _settings;

        public VideoSettings Settings
        {
            get { return _settings; }
            set 
            { 
                _settings = value;
                // Твоя логика смены настроек
                //SetDevice();
            }
        }

        private static readonly log4net.ILog log = LogHelper.GetLogger();

        private System.Windows.Forms.Panel _panel;
        public Camera(VideoSettings settings, System.Windows.Forms.Panel panel, string FileNamePrepender)
        {
            _settings = settings;
            _panel = panel;
            _FileNamePrepender = FileNamePrepender;
        }

        Filters filters = null;
        Capture capture = null;
        #endregion     

        #region Exception handling
        protected void ExceptionHandler(Exception ex)
        {
            log.Error("Some error, check exception", ex);
            MessageBox.Show("Error. Check logfile for details");
            Application.Current.Shutdown();
            Thread.CurrentThread.Abort(); 
        }
        #endregion

        #region Video helper functions
        protected void Preview()
        {
            if (capture == null)
            {
                filters = new Filters();
                if (filters.VideoInputDevices != null)
                {
                    capture = new Capture(filters.VideoInputDevices[_settings.VideoInputDeviceIndex], null);
#if DEBUG
                    Console.WriteLine("****************\nPREVIEW FUNCTION\n****************");
                    Console.WriteLine("****************\nUSING VIDEO INPUT DEVICE: " + filters.VideoInputDevices[_settings.VideoInputDeviceIndex].Name +
                                                        "\n****************");
#endif
                    capture.PreviewWindow = _panel;
                }
                else
                {
                    MessageBox.Show("No video device connected to your PC!");
                }
            }
            else
            {
                capture.Stop();
                capture.PreviewWindow = _panel;
            }
        }

        protected void StartCompressorCapture(Capture capture)
        {
            try
            {
                capture.VideoCompressor = filters.VideoCompressors[_settings.VideoCompressorIndex];
#if DEBUG
                Console.WriteLine("****************\nUSING COMPRESSOR: " + filters.VideoCompressors[_settings.VideoCompressorIndex].Name +
                                                "\n****************");
#endif
                capture.PreviewWindow = _panel;
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }
        }

        protected void tryCapture()
        {
            if (filters.VideoInputDevices != null)
            {
                try
                {
                    capture = new Capture(filters.VideoInputDevices[_settings.VideoInputDeviceIndex], null);
                    StartCompressorCapture(capture);
                }
                catch (Exception ex)
                {
                    ExceptionHandler(ex);
                }
            }
            else
            {
                MessageBox.Show("No video device connected to you PC!");
            }
        }
        
        #endregion

        #region Button control functions
        public void StartPreview()
        {
            try
            {
                Preview();
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }
        }

        public void StartCapture()
        {
#if DEBUG
            Console.WriteLine("****************\nSTART CAPTURE FUNCTION\n****************");
#endif

            if (capture == null)
            {
                filters = new Filters();
                tryCapture();
            }
            else
            {
                capture.Stop();
                StartCompressorCapture(capture);
            }

            FileNameCounter++;
            if (!capture.Cued) capture.Filename = _FileNamePrepender + "_" + FileNameCounter + ".avi";
            try
            {
                capture.Cue(); //need to make an option to rechoose the codec, if program refuses to capture with selected one
                capture.Start();
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }
        }

        public void StopPreview()
        {
#if DEBUG
            Console.WriteLine("****************\nSTOP PREVIEW FUNCTION\n****************");
#endif
            capture.Stop();
            capture.PreviewWindow = null;
        }

        public void StopCapture()
        {
#if DEBUG
            Console.WriteLine("****************\nSTOP CAPTURE FUNCTION\n****************");
#endif
            capture.Stop();
        }
        #endregion

        #region Indication
        public bool isPreviewing()
        {
            try
            {
                if (capture.PreviewWindow != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
                return false;
            }
        }

        public bool isCapturing()
        {
            try
            {
                if (capture.Capturing)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
                return false;
            }
        }
        #endregion
    }
}
