using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace pogoda
{
    public partial class Form1 : Form
    {
        Class1 objConnect; //object togo classa,

        string conString; //to hold our connection string from the Setting page we set up earlier

        DataSet ds; //contains rows, which correspond to a row in the database table
        DataRow dRow; //


        int inc = 0;//will be used to move from one record to another, and back again

        public Form1()
        {

            InitializeComponent();
            //city.Add("label_Ustkamenogorsk".Split('_'),Name.Split('_'));



        }
        static Dictionary<string, string[]> data = new Dictionary<string, string[]>();
        Dictionary<string, string[]> city = new Dictionary<string, string[]>()
;

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(Label))
                {
                    try
                    {
                        if (label_Ustkamenogorsk.Text == "oskemen")
                        {
                            label_Ustkamenogorsk.Name = "label_Ust-kamenogorsk";
                        }
                        string name = c.Name;
                        name = name.Split('_')[1].ToLower();

                        string url = string.Format("http://www.kazhydromet.kz/rss-pogoda.php?id={0}", name);
                        XmlReader reader = XmlReader.Create(url);


                        SyndicationFeed feed = SyndicationFeed.Load(reader);

                        reader.Close();

                        if (feed.Items.Count() > 0)
                        {
                            string text = feed.Items.ElementAt(0).Summary.Text;

                            string[] arr = text.Split(new string[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);

                            if (arr.Length >= 4)
                            {
                                for (int i = 0; i < arr.Length; ++i)
                                {
                                    arr[i] = arr[i].Trim();
                                }

                                data[name] = arr;
                            }
                        }
                        c.Text = data[name][0];

                        inc = 0;
                        DataRow row = ds.Tables[0].Rows[inc];
                        string string_table = ds.Tables[0].Rows[inc].ItemArray.GetValue(0).ToString();
                        if (string_table == name)
                        {
                            row[1] = data[name][0];
                            ds.Tables[0].Rows.Add(row);
                        }

                        inc++;
                    }
                    catch (Exception ee)
                    {
                        inc = 0;
                        
                        dRow = ds.Tables[0].Rows[1];
                        inc++;

                        c.Text = dRow.ItemArray.GetValue(inc).ToString();
                    }
                }
            }
            objConnect.UpdateDatabase(ds);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            objConnect = new Class1();
            conString = Properties.Settings.Default.Database1ConnectionString;
            objConnect.connection_string = conString;
            objConnect.Sql = Properties.Settings.Default.SQL;

            ds = objConnect.GetConnection;
        }
    }
}














