using System;
using System.Collections.Specialized;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace PO
{
    public class Vehicle
    {
        //Vars
        public string code;
        public string vehName;
        public float price;
        public int autonomy;
        public bool inUse;

        //Constructor
        public Vehicle(string code, string vehName, float price, int autonomy)
        {
            this.code = code;
            this.vehName = vehName;
            this.price = price;
            this.autonomy = autonomy;
            this.inUse = false;
        }

        public string GetCode()
        {
            return code;
        }


        public string GetVehName()
        {
            return vehName;
        }

        public float GetPrice()
        {
            return price;
        }

        public int GetAutonomy()
        {
            return autonomy;
        }

        public bool GetInUse()
        {
            return inUse;
        }

        public void SetInUse(bool inUse)
        {
            this.inUse = inUse;
        }
    }

    public class Request
    {
        //Vars
        public int OrderNumber;
        public int NIF;
        public string Code;
        public int Time;
        public int Distance;

        //Constructor
        public Request(int orderNumber, int nif, string Code, int time, int distance)
        {
            this.OrderNumber = orderNumber;
            this.NIF = nif;
            this.Code = Code;
            this.Time = time;
            this.Distance = distance;
        }

        public int GetOrderNumber()
        {
            return OrderNumber;
        }

        public int GetNIF()
        {
            return NIF;
        }

        public string GetCodeRequest()
        {
            return Code;
        }

        public int GetTime()
        {
            return Time;
        }

        public int GetDistance()
        {
            return Distance;
        }
    }
}