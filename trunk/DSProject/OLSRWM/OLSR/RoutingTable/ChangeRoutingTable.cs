using System;
using System.Runtime.InteropServices;
using OLSR.Configuration;

namespace OLSR.OLSR.RoutingTable
{
    /// <summary>
    /// 
    /// Created by  APIF Moviquity S.A.
    /// 
    /// http://www.moviquity.com/
    /// 
    /// Clase encargada de añadir las nuevas entradas en la tabla de rutas
    /// 
    /// Version: 1.3
    ///
    /// </summary>
    class ChangeRoutingTable
    {
        # region Variables
        [DllImport("routewin.dll", EntryPoint = "sayHello")]
        private static extern int sayHello();
        [DllImport("routewin.dll", EntryPoint = "getNumInterfaces")]
        private static extern int getNumInterfaces();
        [DllImport("routewin.dll", EntryPoint = "getTableRoute")]
        private static extern bool getTableRoute(ref TableRouteEntry entry, int index, int num);
        [DllImport("routewin.dll", EntryPoint = "updateTableRoute")]
        private static extern int updateTableRoute(int num);
        [DllImport("routewin.dll", EntryPoint = "getInterfaceInfo")]
        private static extern bool getInterfaceInfo(ref InterfaceEntry entry, int index, int num);
        [DllImport("routewin.dll", EntryPoint = "updateInterfaces")]
        private static extern int updateInterfaces(int num);
        [DllImport("routewin.dll", EntryPoint = "addRoute")]
        private static extern int addRoute(TableRouteEntry entry, int num);
        [DllImport("routewin.dll", EntryPoint = "deleteRoute")]
        private static extern int deleteRoute(String entry, int num);
        [DllImport("routewin.dll", EntryPoint = "resetDevice")]
        private static extern bool resetDevice(int num);

        [StructLayout(LayoutKind.Sequential)]
        public struct TableRouteEntry
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public String ip;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public String mask;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public String nextHop;
            public UInt32 ifIndex;
            public UInt32 type;
            public UInt32 proto;
            public UInt32 age;
            public UInt32 metric;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct InterfaceEntry
        {
            public UInt32 ifIndex;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public String ip;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public String mask;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public String bcast;
            public UInt32 reas;
            public UInt32 wType;//Tipo Interfaz: 0. Primary IP Address
            //1. Dynamic IP Address
            //2. Address is on disconnected interface
            //3. Address is being deleted
            //4. Transient address            
        };

        #endregion

        /// <summary>
        /// Metodo encargado de añadir una ruta a la tabla de rutas del sistema
        /// </summary>
        /// <param name="IPAddRoute">IP a añadir en la tabla</param>
        /// <param name="gateway">Puerta enlace</param>
        public static void AddRoute2RoutingTable(string IPAddRoute, string gateway)
        {
            if (!(IPAddRoute.Equals(OLSRParameters.Originator_Addr.ToString()) || gateway.Equals(OLSRParameters.Originator_Addr.ToString())))
            {
                uint? index = SearchIfaceIndex();

                //Añadimos la entrada en la tabla de rutas
                if (index != null)
                {
                    var tb = new TableRouteEntry();

                    if (!IPAddRoute.Equals(gateway))
                    {
                        tb.ip = IPAddRoute;

                        if (!IPAddRoute.Equals("0.0.0.0"))
                            tb.mask = "255.255.255.255";
                        else
                        {
                            DeleteRoute2RoutingTable("0.0.0.0");
                            tb.mask = "0.0.0.0";
                        }

                        tb.metric = 0;
                        tb.nextHop = gateway;
                        tb.proto = 2;
                        tb.type = 3;
                        tb.ifIndex = (uint) index;
                        tb.age = 8;

                        //LogWriter.GetInstance().AddText(" Add Route : " + IPAddRoute + " - " + gateway);

                        int code = addRoute(tb, 1256895683);

                        //LogWriter.GetInstance().AddText(" Codigo error: " + code);
                    }

                    ////Si la entrada es Superpeer, añadimos ruta por defecto
                    //if (IPAddRoute.EndsWith(".1") && !OLSRParameters.Originator_Addr.ToString().EndsWith(".1"))
                    //{
                    //    DeleteRoute2RoutingTable("0.0.0.0");

                    //    tb = new TableRouteEntry();

                    //    tb.ip = "0.0.0.0";
                    //    tb.mask = "0.0.0.0";
                    //    tb.metric = 0;
                    //    tb.nextHop = gateway;
                    //    tb.proto = 2;
                    //    tb.type = 3;
                    //    tb.ifIndex = (uint) index;
                    //    tb.age = 8;

                    //    addRoute(tb, 1256895683);
                    //}
                }
            }

        }

        /// <summary>
        /// Metodo encargado de buscar el indice de la interfaz donde tenemos que añadir las
        /// nuevas rutas
        /// </summary>
        /// <returns>Indice de la interfaz de red</returns>
        private static uint? SearchIfaceIndex()
        {

            int numInterfaces = updateInterfaces(1256895683);

            for (int x = 0; x < numInterfaces; x++)
            {
                var interfaz = new InterfaceEntry();
                getInterfaceInfo(ref interfaz, x, 1256895683);
                if (interfaz.ip.Equals(OLSRParameters.Originator_Addr.ToString()))
                {
                    return interfaz.ifIndex;
                }

            }
            return null;
        }

        /// <summary>
        /// Metodo encargado de borrar las entradas en la tabla de rutas cuya ip se corresponda 
        /// </summary>
        /// <param name="IPRemoveRoute">IP a borrar</param>
        public static void DeleteRoute2RoutingTable(string IPRemoveRoute)
        {
            if (!IPRemoveRoute.Equals("127.0.0.0") && !IPRemoveRoute.Equals("255.255.255.255"))
            {
                deleteRoute(IPRemoveRoute, 1256895683);
            }
        }

        /// <summary>
        /// Metodo encargado de buscar una ruta en el sistema
        /// </summary>
        /// <param name="pIp"> cadena con la direccion IP</param>
        /// <returns>Boolean si existe o no la ruta en el sistema</returns>
        public static bool SearchRoute( string pIp )
        {
            //recuperar listado de rutas
            int numEntries = updateTableRoute(1256895683);

            for (int x = 0; x < numEntries; x++)
            {
                TableRouteEntry table = new TableRouteEntry();
                getTableRoute(ref table, x, 1256895683);

                if (table.ip.Equals(pIp))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Metodo encargado de buscar rutas en la tabla de rutas repetidas
        /// </summary>
        /// <param name="pIp">Direccion ip</param>
        /// <returns>Boolean si existe o no rutas repetidas en el sistema para esa IP</returns>
        public static bool SearchRepeatRoutes(string pIp)
        {
            //recuperar listado de rutas
            int cont = 0;
            int numEntries = updateTableRoute(1256895683);

            for (int x = 0; x < numEntries; x++)
            {
                TableRouteEntry table = new TableRouteEntry();
                getTableRoute(ref table, x, 1256895683);

                if (table.ip.Equals(pIp))
                    cont++;
            }
            if(cont > 1)
                return true;
            return false;
        }

        /// <summary>
        /// Metodo encargado de borrar rutas del sistema
        /// </summary>
        /// <param name="pIp">Cadena con la direccion IP a borrar</param>
        public static void DeleteRoute(string pIp)
        {
            int numEntries = updateTableRoute(1256895683);

            for (int x = 0; x < numEntries; x++)
            {
                TableRouteEntry table = new TableRouteEntry();
                getTableRoute(ref table, x, 1256895683);

                if(table.ip.Equals(pIp))
                    deleteRoute(table.ip, 1256895683);
            }

            numEntries = updateTableRoute(1256895683);

            for (int x = 0; x < numEntries; x++)
            {
                TableRouteEntry table = new TableRouteEntry();
                getTableRoute(ref table, x, 1256895683);

                if (table.ip.Equals("0.0.0.0"))
                    AddRoute2RoutingTable(pIp,table.nextHop);
            }

        }

    }
}