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
                    string name = c.Name;
                    //if(city.ContainsKey(name))
                    //{
                    //    name = city[name];
                    //}
                    name= name.Split('_')[1].ToLower();
                    //MessageBox.Show(c.Name.Split('_')[1]);
                    //string name = c.Name.Split('_')[1].ToLower();
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
                        c.Text = data[name][0];

                        if(label_Ustkamenogorsk.Text=="oskemen")
                        {
                            label_Ustkamenogorsk.Name = "label_Ust-kamenogorsk";
                        }
                       

                    }

                }
            }
        }

        private void label_Karaganda_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
