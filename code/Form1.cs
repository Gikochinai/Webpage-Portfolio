using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using PrivateCustomExceptionAssembly;

namespace EDP_Project
{
    public partial class CashierForm : Form
    {
        private SqlConnection sqlConnect;
        private SqlDataAdapter sqlDataAdapter;
        public DataTable dataTable;
        public BindingSource bindingSource;
        private string connectionString;


        public CashierForm()
        {
            InitializeComponent();
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\STI Global City\3rd Year documents\1st Semester\Event-Driven Programming\Activities\Finals\Projects\EDP Project\EDP Project\MenuDB.mdf"";Integrated Security=True";
            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //itemsGridView.Rows.Add("1pc Chicken McDo", "99");
            //itemsGridView.Rows.Add("Crispy Chicken Fillet Ala King", "89");
            //itemsGridView.Rows.Add("Crispy Chicken Fillet", "82");
            //itemsGridView.Rows.Add("Medium Fries", "83");
            //itemsGridView.Rows.Add("BFF Fries", "169");

            string ViewClubMembers = "select * from Menu";
            sqlDataAdapter = new SqlDataAdapter(ViewClubMembers, sqlConnect);
            dataTable.Clear();

            sqlDataAdapter.Fill(dataTable);
            bindingSource.DataSource = dataTable;
            itemsGridView.DataSource = bindingSource;
            itemsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void itemsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void itemsGridView_Click(object sender, EventArgs e)
        {
            item_TxtBox.Text = itemsGridView.CurrentRow.Cells[0].Value.ToString();
            price_TxtBox.Text = itemsGridView.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string orderItem = item_TxtBox.Text;
                string priceItem = price_TxtBox.Text;
                string quantity = quantity_TxtBox.Text;
                
                if (string.IsNullOrWhiteSpace(orderItem) || string.IsNullOrWhiteSpace(priceItem) || string.IsNullOrWhiteSpace(quantity) || double.Parse(quantity) < 1)
                {
                    throw new CustomExceptionClass.InvalidException("Please enter the right values");
                }
                else
                {
                    bool IsExist = false;
                    foreach (DataGridViewRow row in orderListGridView.Rows)
                    {
                        string item = row.Cells[0].Value.ToString();
                        double quantity2 = double.Parse(row.Cells[1].Value.ToString());

                        if (orderItem == item)
                        {
                            row.Cells[1].Value = double.Parse(quantity) + quantity2;
                            IsExist = true;
                            break;
                        }
                    }

                    if (!IsExist)
                    {
                        orderListGridView.Rows.Add(orderItem, quantity, priceItem);
                    }                  
                }

            } catch(CustomExceptionClass.InvalidException m) {
                MessageBox.Show(m.Message);
                quantity_TxtBox.Text = "";
            } catch (FormatException m) {
                MessageBox.Show(m.Message);
                quantity_TxtBox.Text = "";
            }


        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (orderListGridView.CurrentRow != null)
            {
                int RowIndex = orderListGridView.CurrentRow.Index;
                orderListGridView.Rows.RemoveAt(RowIndex);
            }
            else {
                MessageBox.Show("Please select a row to delete.");
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            orderListGridView.Rows.Clear();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (orderListGridView.CurrentRow != null)
            {
                double[] orders = new double[orderListGridView.RowCount];
                Dictionary<string, double[]> AllOrders = new Dictionary<string, double[]>();

                int orderIndex = 0;

                foreach (DataGridViewRow row in orderListGridView.Rows)
                {
                    string item = row.Cells[0].Value.ToString();
                    double quantity = double.Parse(row.Cells[1].Value.ToString());
                    double price = double.Parse(row.Cells[2].Value.ToString());

                    double[] value = { quantity, price };

                    AllOrders.Add(item, value);
                    orders[orderIndex] = quantity * price;
                    orderIndex++;
                }

                /*foreach (double item in orders)
                {
                    Console.WriteLine(item);
                }*/

                DelegateClass.setDictionary(AllOrders);
                DelegateClass.setArray(orders);
                PaymentSystem paymentSystem = new PaymentSystem();
                paymentSystem.ShowDialog();
            }
            else {
                MessageBox.Show("No order have been registered");
            } 
            
        }
    }
}
