using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VehicleVenna_UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void submitButton_Click(object sender, EventArgs e)
        {
            vehicle newVehicle = new vehicle(buyerFirstNameTextBox.Text, buyerLastNameTextBox.Text, buyerEmailTextBox.Text,
                MakeTextBox.Text, ModelTextBox.Text, yearTextBox.Text, priceTextBox.Text, comboBox1.SelectedIndex.ToString());

            string url = "http://localhost:7071/api/Function1";
            string url2 = "https://it435function2.azurewebsites.net/api/Function1?code=i1mySFDjRXqChk4AQDOBmoaarVHPXzA3q4MzQgD//kCtYsvxN4Wdhw==";
            var client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(url2, newVehicle);
            this.Hide();
            MessageBox.Show(response.Content.ReadAsStringAsync().Result.ToString());
            this.Show();
        }
    }
    public enum VehicleTypes
    {
        Car,
        SportsCar,
        Truck,
        Motorcycle,
        MotorHome
    }
    public class vehicle
    {
        public vehicle(string bfName, string blName, string bEmail, 
            string make, string model, string year, string listPrice, 
            string vehicleType)
        {
            buyerFirstName = bfName;
            buyerLastName = blName;
            buyerEmail = bEmail;
            Make = make;
            Model = model;
            Year = year;
            ListPrice = listPrice;
            type = (VehicleTypes)Enum.Parse(typeof(VehicleTypes), vehicleType);
        }
        public VehicleTypes type { get; set; }
        public string buyerFirstName { get; set; }
        public string buyerLastName { get; set; }
        public string buyerEmail { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string ListPrice { get; set; }
    }
}
