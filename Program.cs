using System;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace PO // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main()
        {
            Functions Functions = new Functions(); // extra functions
            Vehicles Vehicles = new Vehicles(); // vehicles functions
            Requests Requests = new Requests(); // requests functions

            Vehicles.getVehicles();
            Requests.getRequests();

            var vehicles = new Dictionary<string, Vehicle>(Vehicles.GetVehiclesDic());
            var requests = new Dictionary<int, Request>(Requests.GetRequestsDic());
            Requests.SetVehiclesDic(vehicles);
            Vehicles.SetRequestsDic(requests);
            Functions.SetVehiclesDic(vehicles);
            Functions.SetRequestsDic(requests);

            int Choice = -1;
            do
            {
                Console.Clear();
                Console.WriteLine("\n\t\t--- Bolt of EDJD ---\n");
                Console.WriteLine("\t1 - List Vehicles");
                Console.WriteLine("\t2 - List Requests");
                Console.WriteLine("\t3 - Add Vehicle");
                Console.WriteLine("\t4 - Remove Vehicle");
                Console.WriteLine("\t5 - Add Request");
                Console.WriteLine("\t6 - Remove Request");
                Console.WriteLine("\t7 - Vehicle list of service");
                Console.WriteLine("\t8 - Price of the the Trip");
                Console.WriteLine("\t0 - Exit");

                Choice = Functions.GetInt(0, 8);
                switch (Choice)
                {
                    case 1:
                        Functions.SetVehiclesDic(vehicles);
                        Vehicles.printVehicles();
                        break;
                    case 2:
                        Functions.SetRequestsDic(requests);
                        Requests.printRequests();
                        break;
                    case 3:
                        Vehicles.writeVehicle();
                        vehicles = Vehicles.GetVehiclesDic();
                        break;
                    case 4:
                        Vehicles.deleteVehicle();
                        vehicles = Vehicles.GetVehiclesDic();
                        break;
                    case 5:
                        Requests.writeRequest();
                        requests = Requests.GetRequestsDic();
                        vehicles = Vehicles.GetVehiclesDic();
                        break;
                    case 6:
                        Requests.deleteRequest();
                        requests = Requests.GetRequestsDic();
                        break;
                    case 7:
                        Functions.SetRequestsDic(requests);
                        Functions.SetVehiclesDic(vehicles);
                        Vehicles.printVehicles();
                        Functions.VehicleService();
                        break;
                    case 8:
                        Functions.SetRequestsDic(requests);
                        Functions.SetVehiclesDic(vehicles);
                        Requests.printRequests();
                        Functions.Price();
                        break;
                    case 0:
                        Console.WriteLine("\n\n\n \t\t Have a nice day \n\n\t\t\t :)");
                        Console.ReadKey();
                        return;
                    default:
                        Console.WriteLine("\n\n\t\t Errou !!!\n");
                        break;
                }
                Console.WriteLine("\n\t\tPress any key to go back");
                Console.ReadKey();
            } while (Choice != 0);
        }
    }
}
