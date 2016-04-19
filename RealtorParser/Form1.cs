using System;
using System.Windows.Forms;
using RealtorParser.Senders;

namespace RealtorParser
{
    public partial class Form1 : Form
    {
        readonly RequestSender requestSender = new RequestSender();
        readonly Analyzer analyzer = new Analyzer();
        readonly DataAccess dataAccess = new DataAccess();
        readonly DataCollector dataCollector = new DataCollector();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void GetDataButtn_Click(object sender, EventArgs e)
        {
            Status.Text = "Processing...";

            var searchResult = requestSender.SendPostRequest();

            //analyzer.GetPriceValues(searchResult.Results);

            dataAccess.SaveData(searchResult.Results);

            Status.Text = "Done";
        }

        private void getDetails_Click(object sender, EventArgs e)
        {
            Status.Text = "Processing...";
            dataCollector.GetAllGenericDetails();
            Status.Text = "Done";
        }
    }
}
