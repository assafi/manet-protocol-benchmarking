using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Runner
{
    static class Program
    {
        public const string VERSION = "0.9.0";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new Runner());
        }
    }
}