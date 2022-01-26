using System;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace PO
{
    public class Requests
    {
        public readonly string RequestsFile = @"D:\IPCA\github\PO\files\requests.txt";
        public Dictionary<int, Request> requests;
        public Dictionary<string, Vehicle> vehicles;
        Vehicles Vehicles = new Vehicles(); // vehicles functions


        //Constructor
        public Requests()
        {
            this.requests = new Dictionary<int, Request>();
        }

        public Dictionary<int, Request> GetRequestsDic()
        {
            return requests;
        }

        public void SetVehiclesDic(Dictionary<string, Vehicle> vehDic)
        {
            vehicles = vehDic;
        }

        public void addRequest(Request request) // After reading the requests.txt (File), use this to add the request to the class
        {
            if (!requests.ContainsKey(request.OrderNumber))
            {
                requests.Add(request.OrderNumber, request);
            }
        }
        public void removeRequest(Request request) // Removes a given request from the dictionary
        {
            if (requests.ContainsKey(request.OrderNumber))
            {
                requests.Remove(request.OrderNumber);
            }
        }
        public void writeRequest() // adds the request to the file
        {
            if (File.Exists(RequestsFile))
            {
                int NIF;
                int Time;
                int Distance;
                bool found = false;
                int orderN = 1;

                while (requests.ContainsKey(orderN))
                {
                    orderN++;
                }

                do
                {
                    Console.WriteLine("Insert a NIF: ");
                    NIF = Int32.Parse(Console.ReadLine());
                } while (NIF.ToString().Length != 9);

                Console.WriteLine("Insert the Distance: ");
                Distance = Int32.Parse(Console.ReadLine());

                List<Vehicle> vehiclesList = new List<Vehicle>(); // List to store all the vehicles

                foreach (Vehicle item in vehicles.Values)
                {
                    if (!item.GetInUse())
                    {
                        vehiclesList.Add(item);
                    }
                }

                foreach (Vehicle item in vehiclesList)
                {
                    if (item.GetAutonomy() >= Distance)
                    {
                        if (item.GetVehName() == "Car")
                        {
                            Time = Distance;
                        }
                        else
                        {
                            Time = Decimal.ToInt32(Distance * 25 / 60);
                        }
                        var request = new Request(orderN, NIF, item.GetCode(), Time, Distance);
                        addRequest(request);
                        File.AppendAllText(RequestsFile, string.Format("{0} {1} {2} {3} {4}\n", request.GetOrderNumber().ToString(), request.GetNIF().ToString(), request.GetCodeRequest(), request.GetTime().ToString(), request.GetDistance().ToString()));
                        item.SetInUse(true);
                        Vehicles.removeVehicle(item);
                        Vehicles.addVehicle(item);
                        System.Console.WriteLine("Request added to file.");
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    System.Console.WriteLine("No vehicles available for that request!");
                }

            }
            else
            {
                System.Console.WriteLine("Error! Couldn't find requests file!");
            }
        }

        public void deleteRequest() // removes the request from the file
        {
            if (File.Exists(RequestsFile)) // Verify if file exists
            {
                List<String> requestList = new List<String>(); // List to store all the requests
                string deletingLine = "";

                System.Console.WriteLine("Insert an order number:");
                int orderN = Int32.Parse(Console.ReadLine());
                bool found = false;
                Request request = new Request(0, 0, "", 0, 0);

                foreach (Request item in requests.Values)
                {
                    if (item.GetOrderNumber() == orderN)
                    {
                        request = item;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    System.Console.WriteLine("Request not found!");
                    return;
                }

                using (StreamReader reader = new StreamReader(RequestsFile)) // Get all requests from the file
                {
                    string line = reader.ReadLine();

                    while (line != null)
                    {
                        requestList.Add(line); // Adds each request to the List
                        if (line.Equals(string.Format("{0} {1} {2} {3} {4}\n", request.OrderNumber.ToString(), request.NIF.ToString(), request.Code, request.Time.ToString(), request.Distance.ToString()))) ;
                        {
                            //Check if the given request matches any of the ones in the file
                            deletingLine = line;
                        }

                        line = reader.ReadLine();
                    }
                }

                if (deletingLine != "")
                {
                    if (requestList.Contains(deletingLine)) // extra verify if the list has the given request
                    {
                        requestList.Remove(deletingLine); // removes the request from the list

                        File.WriteAllText(RequestsFile, String.Empty); // empties the text file


                        using (StreamWriter writer = new StreamWriter(RequestsFile))
                        {
                            foreach (string item in requestList)
                            {
                                writer.WriteLineAsync(item); // writes each line of code to the file ( basically just reset for the requests list)
                            }
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Error! Request already deleted or not found.");
                    }
                }
                else
                {
                    System.Console.WriteLine("Error! Request doesn't exist!");
                }

                System.Console.WriteLine("Request deleted from file.");
            }
            else
            {
                System.Console.WriteLine("Error! Couldn't find requests file!");
            }
        }

        public void getRequests()
        {
            using (StreamReader reader = new StreamReader(RequestsFile))
            {

                string line = reader.ReadLine();

                while (line != null)
                {
                    string[] valores = line.Split(' ');
                    Request newRequest = new Request(Int32.Parse(valores[0]), Int32.Parse(valores[1]), valores[2], Int32.Parse(valores[3]), Int32.Parse(valores[4]));
                    addRequest(newRequest);
                    line = reader.ReadLine(); // reads the next line
                }
                System.Console.WriteLine("Requests loaded.");
            }
        }

        public void printRequests()
        {
            Console.WriteLine("\n\t\t--- Request List ---\n");
            foreach (Request req in requests.Values)
            {
                Console.Write("\t Order: {0} | NIF:  {1} | Vehicle Code: {2} | Time: {3} | Distance: {4}\n", req.GetOrderNumber(), req.GetNIF(), req.GetCodeRequest(), req.GetTime(), req.GetDistance());
            }
        }
    }
}