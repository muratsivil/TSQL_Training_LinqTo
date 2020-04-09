using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSQL_Training_LinqTo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NorthwindEntities db = new NorthwindEntities();
        //EX-1
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.ToList(); // List all Products information.
        }
        //EX-2
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Categories.ToList(); // List all Categories information.
        }
        //EX-3
        private void button3_Click(object sender, EventArgs e) // List CategoryName and Description from Categories table.
        {
            dataGridView1.DataSource = db.Categories.Select(x => new
            {
                x.CategoryName,
                x.Description
            }).ToList();
        }
        //EX-4
        private void button4_Click(object sender, EventArgs e) // List ProductName, UnitPrice, UnitInStock from Products table.
        {
            dataGridView1.DataSource = db.Products.Select(x => new
            {
                x.ProductName,
                x.UnitPrice,
                x.UnitsInStock
            }).ToList();
        }
        //EX-5
        private void button5_Click(object sender, EventArgs e)  //In this example, I gave names to coloumns.
        {
            dataGridView1.DataSource = db.Employees.Select(x => new
            {
                Name = x.FirstName,
                Surname = x.LastName,
                Title = x.Title
            }).ToList();
        }
        //EX-6
        private void button6_Click(object sender, EventArgs e) // We did the same thing on Products
        {
            dataGridView1.DataSource = db.Products.Select(x => new
            {
                x.ProductName,
                x.UnitPrice,
                StockStatus = x.UnitsInStock

            }).ToList();
        }
        //EX-7
        private void button7_Click(object sender, EventArgs e)      
        {
            /* List the ProductID, ProductName, UnitPrice and UnitInStock of the products table with an unit price greater than 18. */
            dataGridView1.DataSource = db.Products.Where(x=>x.UnitPrice >18).Select(y=> new
            {
                y.ProductID,
                y.ProductName,
                y.UnitPrice,
                y.UnitsInStock
            }).ToArray(); //   *If we type ToArray() function, it settles in order.
        }

        private void button8_Click(object sender, EventArgs e)
        {
            /*List the EmployeeID, FirstName, LastName, Title of the Employee table with an EmployeeID greater than 5. */
            dataGridView1.DataSource = db.Employees.Where(x => x.EmployeeID > 5).Select(y => new
            {
                y.EmployeeID,
                y.FirstName,
                y.LastName,
                y.Title
            }).ToArray();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            /*List the ProductID, ProductName, UnitPrice, UnitInStock of the Products table with an UnitsInStock equals to 0.  */
            dataGridView1.DataSource = db.Products.Where(x => x.UnitsInStock == 0).Select(y => new
            {
                y.ProductID,
                y.ProductName,
                y.UnitPrice,
                y.UnitsInStock
            }).ToArray();
        }

        private void button10_Click(object sender, EventArgs e) // who born between 1952 and 1960?
        {
            // We used "SqlFunctions" class for to reach DatePart function
            dataGridView1.DataSource = db.Employees.Where
                (x => SqlFunctions.DatePart("Year", x.BirthDate) >= 1952 && SqlFunctions.DatePart("Year", x.BirthDate) <= 1960).Select(y => new
                {
                    y.TitleOfCourtesy,
                    y.FirstName,
                    y.LastName,
                    BirthYear = SqlFunctions.DatePart("Year", y.BirthDate),
                    y.Title
                }).ToArray();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Employees.Where(x => x.TitleOfCourtesy == "Mr." || x.TitleOfCourtesy == "Dr.").Select(y => new
            {
                y.TitleOfCourtesy,
                y.FirstName,
                y.LastName
            }).ToArray();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.Where(x => x.UnitPrice == 18 || x.UnitPrice == 19 || x.UnitPrice == 25).ToList();
        }
       
        private void button13_Click(object sender, EventArgs e) // How many products's Stock Stats greater than 100.
        {
            dataGridView1.DataSource = db.Products.Where(x => x.UnitsInStock > 100).Select(y => new
            {
                y.ProductID,
                y.ProductName,
                y.UnitsInStock,
                y.UnitPrice
            }).OrderByDescending(z => z.UnitsInStock).ToArray(); // we used Order By Descending method for sorting the UnitPrice.
        }

        private void button14_Click(object sender, EventArgs e) // List the Employees's EmployeeID between 2 and 8
        {
            dataGridView1.DataSource = db.Employees.Where(x => x.EmployeeID >= 2 && x.EmployeeID <= 8).OrderBy(z => z.EmployeeID).ToList();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.OrderByDescending(x => x.UnitPrice).Take(5).ToList(); // In T-SQL we used TOP function, Take() is the same as TOP function.
        }

        private void button16_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Employees.Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.BirthDate,
                Age = SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now)
            }).OrderByDescending(z => z.Age).ToArray();
        }
       
        private void button17_Click(object sender, EventArgs e)
        {
            //var result = from x in db.Products
            //             join c in db.Categories on x.CategoryID equals c.CategoryID
            //             join o in db.Order_Details on x.ProductID equals o.ProductID

            //             select new
            //             {
            //                 c.CategoryName,
            //             }

        }

        private void button18_Click(object sender, EventArgs e)
        {
            var sonuc = from ord in db.Order_Details
                        join pro in db.Products on ord.Product.ProductName equals pro.ProductName
                        select new { ord, pro } into birlesim
                        where birlesim.ord.UnitPrice > 40
                        group birlesim by birlesim.ord.Quantity into grup
                        orderby grup.Key descending
                        select new
                        {

                            UrunAdlari = grup.FirstOrDefault().pro.ProductName,
                            StoktakiÜrünMiktarı = grup.FirstOrDefault().pro.UnitsInStock,
                            BirimFiyatı = grup.FirstOrDefault().ord.UnitPrice,
                            SatılanAdet = grup.FirstOrDefault().ord.Quantity

                        };
            dataGridView1.DataSource = sonuc.ToList();
        }
    }
}
