using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectX.Capture;
using DShowNET;
using System.Windows;
using System.Windows.Input;

namespace WebCamControl2
{
    public class MenuCommands
    {

        #region Initialization
        Filters filters = null;
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
            foreach (string device in devices)
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

        public static RoutedUICommand ExitCommand;
        static MenuCommands()
        {
            InputGestureCollection exitInputs = new InputGestureCollection();
            exitInputs.Add(new KeyGesture(Key.F4, ModifierKeys.Alt));
            ExitCommand = new RoutedUICommand("Exit application",
                "ExitApplication",
                typeof(MenuCommands), exitInputs);
        }
        public static void VideoInputDevicesSettings()
        {

        }

        public static void VideoCompressorsSettings()
        {

        }
    }
}
