﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibDnaSerial
{
    /// <summary>
    /// Represents a DNA device
    /// </summary>
    public class DnaDevice
    {
        /// <summary>
        /// Serial port the device is connected to
        /// </summary>
        public string SerialPort { get; set; }

        /// <summary>
        /// Manufacturer name read from device
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Product name read from device
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Features read from device
        /// </summary>
        public List<string> Features { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} ({2})", Manufacturer, ProductName, SerialPort);
        }
    }
}
