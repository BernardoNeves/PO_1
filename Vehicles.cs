using System;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace PO
{
    public class Vehicles
    {
        public readonly string VehicleFile = @"D:\IPCA\github\PO\files\vehicles.txt";
        public Dictionary<string, Vehicle> vehicles;
        public Dictionary<int, Request> requests;

        //Constructor
        public Vehicles()
        {
            this.vehicles = new Dictionary<string, Vehicle>();
        }

        public Dictionary<string, Vehicle> GetVehiclesDic()
        {
            return vehicles;
        }

        public void SetRequestsDic(Dictionary<int, Request> requestDic)
        {
            requests = requestDic;
        }

        public void addVehicle(Vehicle veh) //use this to add the vehicle to the dictionary
        {

            if (!vehicles.ContainsKey(veh.GetCode()))
            {
                vehicles.Add(veh.GetCode(), veh);
            }
        }

        public void removeVehicle(Vehicle veh) // removes a given vehicle from the dictionary
        {
            if (vehicles.ContainsKey(veh.GetCode()))
            {
                vehicles.Remove(veh.GetCode());
            }
        }

        public void writeVehicle() // adds the vehicle to the file
        {
            if (File.Exists(VehicleFile))
            {
                int codeValue = 1;
                string code = "M_" + codeValue;
                
                while(vehicles.ContainsKey(code)){
                    codeValue++;
                    code = "M_" + codeValue;
                }

                string vehName;
                float price;
                int autonomy;

                Console.WriteLine("Insert the vehicle type: ");
                vehName = Console.ReadLine();
                Console.WriteLine("Insert the vehicle price: ");
                price = float.Parse(Console.ReadLine());
                Console.WriteLine("Insert the vehicle autonomy: ");
                autonomy = Int32.Parse(Console.ReadLine());

                Vehicle veh = new Vehicle(code, vehName, price, autonomy);

                addVehicle(veh);
                File.AppendAllText(VehicleFile, String.Format("{0} {1} {2} {3}\n", veh.GetCode(), veh.GetVehName(), veh.GetPrice().ToString(), veh.GetAutonomy().ToString()));
                System.Console.WriteLine("Vehicle added to file.");
            }
            else
            {
                System.Console.WriteLine("Error! Couldn't find vehicles file!");
            }
        }

        public void deleteVehicle() // removes the vehicle from the file
        {
            if (File.Exists(VehicleFile)) // Verify if file exists
            {
                List<String> vehicleList = new List<String>(); // List to store all the vehicles
                string deletingLine = "";

                System.Console.WriteLine("Insert a vehicle code:");
                string cCode = Console.ReadLine();
                bool found = false;
                Vehicle veh = new Vehicle("", "", 0f, 0);

                foreach(Vehicle item in vehicles.Values){
                    if(item.GetCode() == cCode){
                        veh = item;
                        found = true;
                        break;
                    }
                }

                if(!found){
                    System.Console.WriteLine("Vehicle not found!");
                    return;
                }

                using (StreamReader reader = new StreamReader(VehicleFile)) // Get all vehicles from the file
                {
                    string line = reader.ReadLine();

                    while (line != null)
                    {
                        vehicleList.Add(line); // Adds each vehicle to the List
                        if (line.Equals(String.Format("{0} {1} {2} {3}", veh.GetCode(), veh.GetVehName(), veh.GetPrice().ToString(), veh.GetAutonomy().ToString())))
                        {
                            //Check if the given vehicle matches any of the ones in the file
                            deletingLine = line;
                        }

                        line = reader.ReadLine();
                    }
                }

                if (deletingLine != "")
                {
                    if (vehicleList.Contains(deletingLine)) // extra verify if the list has the given vehicle
                    {
                        vehicleList.Remove(deletingLine); // removes the vehicle from the list

                        File.WriteAllText(VehicleFile, String.Empty); // empties the text file

                        using (StreamWriter writer = new StreamWriter(VehicleFile))
                        {
                            foreach (string item in vehicleList)
                            {
                                writer.WriteLineAsync(item); // writes each line of code to the file ( basically just reset for the vehicles list)
                            }
                        }

                    }
                    else
                    {
                        System.Console.WriteLine("Error! Vehicle already deleted or not found.");
                    }
                }
                else
                {
                    System.Console.WriteLine("Error! Vehicle doesn't exist!");
                }

                System.Console.WriteLine("Vehicle deleted from file.");
            }
            else
            {
                System.Console.WriteLine("Error! Couldn't find vehicles file!");
            }
        }

        public void getVehicles()
        {
            using (StreamReader reader = new StreamReader(VehicleFile))
            {

                string line = reader.ReadLine();

                while (line != null)
                {
                    string[] valores = line.Split(' ');
                    Vehicle newVeh = new Vehicle(valores[0], valores[1], float.Parse(valores[2]), Int32.Parse(valores[3]));
                    addVehicle(newVeh);
                    line = reader.ReadLine(); // reads the next line
                }
                System.Console.WriteLine("Vehicles loaded.");
            }
        }

        public void printVehicles()
        {
            int i = 0;
            Console.WriteLine("\n\t\t--- Vehicle List ---\n");
            foreach (Vehicle veh in vehicles.Values)
            {
                i++;
                Console.Write("\t Order: {0} | Code: {1} | Type: {2} Price Rate: {3} | Autonomy: {4} \n", i, veh.GetCode(), veh.GetVehName(), veh.GetPrice(), veh.GetAutonomy());
            }
        }
    }
}