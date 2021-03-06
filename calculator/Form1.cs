using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{   
  
    public partial class Form1 : Form
    {   
        private string first_number;
        public string operation;
        public bool check_value;
        public List<String> test_list = new List<string>();
        public List<string> history_test = new List<string> ();
        public Form1()
        {
            check_value = false;
            InitializeComponent();
            using (HistoryContex db = new HistoryContex())
            {
                var items = db.HistoryItems;
                foreach (History i in items)
                {
                    history_test.Add(i.Id.ToString());
                    history_test.Add(i.Date);
                    history_test.Add(i.Operation);

                }
                string[] history_items;
                history_items = history_test.ToArray();
                listBox1.Items.AddRange(history_items);
            }
        }

        
     
        private void AllNubmers(object sender, EventArgs e)
        {
            if (check_value)
            {
                check_value = false;
                textBox1.Text = "0";
            }
            Button b = (Button)sender;
            if (textBox1.Text == "0")
                textBox1.Text = b.Text;
            else
                textBox1.Text += b.Text;
                listBox1.Items.Add(textBox1.Text);
            test_list.Add(b.Text);
        }

        private void Reset(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        private void BasicOperations(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            operation = b.Text;
            first_number = textBox1.Text;
            check_value = true;
            listBox1.Items.Add(operation);
            test_list.Add(operation);
 
        }

        private void Calculate(object sender, EventArgs e)
        {
            double value1, value2, result;
            value1 = Convert.ToDouble(first_number);
            value2 = Convert.ToDouble(textBox1.Text);
            result = 0;

            if (operation == "+")
            {
                result = value1 + value2;
            }

            if (operation == "-")
            {
                result = value1 - value2;
            }

            if (operation == "x")
            {
                result = value1 * value2;
            }

            if (operation == "/")
            {
                result = value1 / value2;
            }

            operation = "=";
            check_value = true;
            textBox1.Text = result.ToString();
            listBox1.Items.Add("=" + result.ToString());
            listBox1.Items.Add(" ");
            test_list.Add("=" +  result.ToString());
            var oper = String.Join(" ", test_list.ToArray());

            using (HistoryContex db = new HistoryContex())
            {
                History test1 = new History { Date = DateTime.Now.ToString(), Operation = oper };
                db.HistoryItems.Add(test1);
                db.SaveChanges();
            }


            
            
        }
    }
}
