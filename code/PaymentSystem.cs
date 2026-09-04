using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrivateCustomExceptionAssembly;


namespace EDP_Project
{
    public partial class PaymentSystem : Form
    {
        private double [] orders;
        private Dictionary<string, double[]> AllOrders;
        private DelegateDictionary delegateDictionary;
        private DelegateArray delegateArray;
        public PaymentSystem()
        {
            InitializeComponent();
            delegateDictionary = new DelegateDictionary(DelegateClass.getDictionary);
            delegateArray = new DelegateArray(DelegateClass.getArray);
            orders = delegateArray();
            AllOrders = delegateDictionary();
        }

        private void PaymentSystem_Load(object sender, EventArgs e)
        {            
            double TotalAmount = Add(orders);
            totalAmount_TxtBox.Text = TotalAmount.ToString();
        }

        static double Add(double[] orders) {
            double temp = 0;
            foreach (double item in orders) { 
                temp += item;
            }
           return temp;
        }

        private void recieved_TxtBox_TextChanged(object sender, EventArgs e)
        {
            try {
                if (!string.IsNullOrWhiteSpace(received_TxtBox.Text)) {
                    change_TxtBox.Text = (double.Parse(received_TxtBox.Text) - double.Parse(totalAmount_TxtBox.Text)).ToString();
                }    
            } catch (FormatException m){
                MessageBox.Show(m.Message);
                received_TxtBox.Text = "";
            }
            
        }

        public void DisplayConsole() {
            Console.WriteLine();
            for (int x = 0; x < 65; x++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.WriteLine("Quantity \t Item \t\t\t\t\t Price");
            for (int x = 0; x < 65; x++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            foreach (KeyValuePair<string, double[]> keyValue in AllOrders)
            {
                Console.WriteLine($"{keyValue.Value[0]} \t\t {keyValue.Key.PadRight(30)} \t {keyValue.Value[1]}");
            }
            for (int x = 0; x < 65; x++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.WriteLine($"Total Amount: {totalAmount_TxtBox.Text}");
            Console.WriteLine($"Received: {received_TxtBox.Text}");
            Console.WriteLine($"Change: {change_TxtBox.Text}");
            for (int x = 0; x < 65; x++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try {
                double customerPayment = double.Parse(received_TxtBox.Text);
                double totalAmount = double.Parse(totalAmount_TxtBox.Text);
                if (customerPayment < totalAmount)
                {
                    throw new CustomExceptionClass.InvalidException("Please input valid values");
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter("receipt.txt"))
                    {
                        sw.WriteLine();
                        for (int x = 0; x < 65; x++)
                        {
                            sw.Write("-");
                        }
                        sw.WriteLine();
                        sw.WriteLine("Quantity \t Item \t\t\t\t\t Price");
                        for (int x = 0; x < 65; x++)
                        {
                            sw.Write("-");
                        }
                        sw.WriteLine();
                        foreach (KeyValuePair<string, double[]> keyValue in AllOrders)
                        {
                            sw.WriteLine($"{keyValue.Value[0]} \t\t {keyValue.Key.PadRight(30)} \t {keyValue.Value[1]}");
                        }
                        for (int x = 0; x < 65; x++)
                        {
                            sw.Write("-");
                        }
                        sw.WriteLine();
                        sw.WriteLine($"Total Amount: {totalAmount_TxtBox.Text}");
                        sw.WriteLine($"Received: {received_TxtBox.Text}");
                        sw.WriteLine($"Change: {change_TxtBox.Text}");
                        for (int x = 0; x < 65; x++)
                        {
                            sw.Write("-");
                        }
                        sw.WriteLine();
                        sw.Close();
                    }
                    LoadingForm loadingForm = new LoadingForm();
                    loadingForm.ShowDialog();
                    DisplayConsole();
                    MessageBox.Show("Receipt has successfully printed");
                    Close();
                }
            } catch (CustomExceptionClass.InvalidException m) {
                MessageBox.Show(m.Message);
                received_TxtBox.Text = "";
            }
            catch (FormatException m) {
                MessageBox.Show(m.Message);
                received_TxtBox.Text = "";
            }
            
           
        }
    }
}
