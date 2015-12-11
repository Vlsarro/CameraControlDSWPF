using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraControl
{
    [Serializable]
    public class VideoSettings
    {
        private int _VideoInputDeviceIndex = 0;
        private int _VideoCompressorIndex = 0;
        public int CurrentIdDevice { get; set; }
        //public List<VideoDevice> Devices { get; set; } 

        public int VideoInputDeviceIndex
        {
            get { return _VideoInputDeviceIndex; }
            set { _VideoInputDeviceIndex = value; }
        }

        public int VideoCompressorIndex
        {
            get { return _VideoCompressorIndex; }
            set { _VideoCompressorIndex = value; }
        }

        internal void Load()
        {
            throw new NotImplementedException();
        }

        internal void Save()
        {
            throw new NotImplementedException();
        }

        
    }
}
