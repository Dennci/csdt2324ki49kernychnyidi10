using Air_Alarm.Models;
using Air_Alarm.Services;
using System.IO.Ports;
using System.Net;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Air_Alarm
{
    public partial class Form1 : Form
    {
        private AlertModel alertModels = new AlertModel();
        private System.Windows.Forms.Timer timer_to_refresh;
        private int secondsRemaining = 15;
        public Form1()
        {
            InitializeComponent();
            GetAlarmMap();
            comboBox1?.Items.AddRange(Regions.Region.ToArray());
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Dock = DockStyle.Fill;
            this.Controls.Add(pictureBox1);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pictureBox1 = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            comboBox1 = new ComboBox();
            label1 = new Label();
            timer_to_refresh = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(78, 135);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(851, 283);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 15000;
            timer1.Tick += timer1_Tick_1;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(420, 50);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(176, 28);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 384);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 2;
            label1.Text = "label1";
            // 
            // timer_to_refresh
            // 
            timer_to_refresh.Enabled = true;
            timer_to_refresh.Interval = 1000;
            timer_to_refresh.Tick += timer_to_refresh_Tick;
            // 
            // Form1
            // 
            ClientSize = new Size(967, 448);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Load += Form1_Load_1;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private async void timer1_Tick_1(object sender, EventArgs e)
        {
            GetAlarmMap();
            alertModels= await AlarmResponse.GetAlarmResponse();
            IsAirAlarm(alertModels);
            secondsRemaining = 15;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
        private void IsAirAlarm(AlertModel alertModel)
        {
            if ( String.IsNullOrEmpty(MyCityModel.Name))
            {
                return;
            }
            var newAlert = alertModel.States[MyCityModel.Name];
            if (newAlert.AlertNow != MyCityModel.AlertNow)
            {
                MyCityModel.AlertNow=newAlert.AlertNow;
                
                var arduinoModel = new AirRaidModel()
                {
                    AlertNow = newAlert.AlertNow,
                    Region = MyCityModel.Name
                };
                XmlSerializer serializer = new XmlSerializer(typeof(AirRaidModel));
                using (StreamWriter writer = new StreamWriter("data.xml"))
                {
                    serializer.Serialize(writer, arduinoModel);
                }

                using (SerialPort port = new SerialPort("COM3", 9600))
                {
                    port.Open();
                    string xmlData = File.ReadAllText("data.xml");
                    port.Write(xmlData);
                }

            }
        }
        private void GetAlarmMap()
        {
            string imageUrl = "https://ubilling.net.ua/aerialalerts/?map=true";
            WebClient webClient = new WebClient();

            // Download the image
            byte[] imageBytes = webClient.DownloadData(imageUrl);

            // Create a MemoryStream from the downloaded bytes
            using (MemoryStream stream = new MemoryStream(imageBytes))
            {
                // Load the image into the PictureBox
                pictureBox1.Image = Image.FromStream(stream);
            }
        }
        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private ComboBox comboBox1;
        private Label label1;
        private System.ComponentModel.IContainer components;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRegion = comboBox1.SelectedItem as string;
            MyCityModel.Name = selectedRegion;
        }

        private void timer_to_refresh_Tick(object sender, EventArgs e)
        {
            secondsRemaining--;
            label1.Text = "Оновлення через: " + secondsRemaining + " с";
        }
    }
}