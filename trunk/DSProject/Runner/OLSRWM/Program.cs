using System;
using System.Windows.Forms;
using OLSR.Screens;

namespace OLSR
{
    /// <summary>
    /// 
    /// Created by  APIF Moviquity S.A.
    /// 
    /// http://www.moviquity.com/
    /// 
    /// Developers:
    /// 
    ///     Alberto Martinez Garcia
    ///     Francisco Abril Bucero 
    ///     Jose Manuel Lopez Garcia
    ///     
    ///
    /// Version: 1.1
    ///
    /// 
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(StartScreen.GetInstance());
        }
    }
}