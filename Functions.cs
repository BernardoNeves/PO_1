using System;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace PO
{
    public class Functions
    {
        Requests Requests = new Requests(); // requests functions
        Vehicles Vehicles = new Vehicles(); // vehicles functions
        public Dictionary<string, Vehicle> vehicles;
        public Dictionary<int, Request> requests;

        public void SetVehiclesDic(Dictionary<string, Vehicle> vehDic)
        {
            vehicles = vehDic;
        }

        public void SetRequestsDic(Dictionary<int, Request> requestDic)
        {
            requests = requestDic;
        }

        public void VehicleService() // lists all of the requests that will be using this vehicle in chronological order
        {
            string code = "";
            int i = 0, Choice = -1;
            Console.Write("\n\t\t> ");
            Choice = int.Parse(Console.ReadLine());

            foreach (Vehicle veh in vehicles.Values)
            {
                i++;
                if (Choice == i)
                {
                    code = veh.GetCode();
                    Console.Clear();
                    Console.Write("\n\n\t Chosen Vehicle: Code: {0} | Type: {1} Price Rate: {2} | Autonomy: {3} \n", veh.GetCode(), veh.GetVehName(), veh.GetPrice(), veh.GetAutonomy());
                    break;
                }
            }

            foreach (Vehicle veh in vehicles.Values)
            {
                if (veh.GetCode() == code)
                {
                    int autonomy = veh.GetAutonomy(), time = 0;
                    foreach (Request req in requests.Values)
                    {
                        if (req.GetCodeRequest() == code)
                        {
                            Console.Write("\t Order: {0} | NIF: {1} | Initial Time: {2}", req.GetOrderNumber(), req.GetNIF(), time);
                            time += req.GetTime();
                            Console.Write(" | Final Time: {0} | Initial Autonomy: {1} | Vehicle Code: {2}\n", time, autonomy, req.GetCodeRequest());
                            autonomy -= req.GetDistance();
                            if (autonomy < 0)
                            {
                                autonomy = 0;
                            }
                        }
                    }
                }
            }
        }

        public void Price() // Gets the Price of the Trip
        {
            float Minutes, Cost, FullPrice;
            string VehicleCode;

            Console.Write("\n\tInsert the Order Number: "); // Asks for the OrderNumber
            int Number = Int32.Parse(Console.ReadLine());

            foreach (Request Req in requests.Values) // Loops through all the Requests in the Dictionary
            {
                if (Req.GetOrderNumber() == Number) // Until it finds the Request with the Order Number given
                {
                    Minutes = Req.GetTime();
                    VehicleCode = Req.GetCodeRequest();

                    foreach (Vehicle Veh in vehicles.Values) // Loops through all the Vehicles in the Dictionary
                    {
                        if (Veh.GetCode() == VehicleCode) // Until it finds the Vehicles with the Code of the Request
                        {
                            Cost = Veh.GetPrice();
                            FullPrice = Minutes * Cost; // Operation to get  the Price of the  Trip
                            Console.Write("\n\tThe Price of the Trip is: {0}\n\n", FullPrice); // Prints the Price of the Trip
                        }
                    }
                }
            }
        }

        public int GetInt(int min, int max) // Gets an Int beetwen two given Number
        {
            int Choice = -1;

            Console.Write("\n> ");
            Choice = Int32.Parse(Console.ReadLine());

            while (Choice > max || Choice < min)
            {
                Console.WriteLine("\n\t//Invalid Option//\n\n");
                Console.WriteLine("\n> ");
                Choice = Int32.Parse(Console.ReadLine());
            }
            Console.Clear();
            return Choice;
        }
    }
}
