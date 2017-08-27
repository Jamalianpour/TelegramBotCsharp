using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TelegramBotSharp;
using System.IO;
using System.Diagnostics;
using System.Device.Location;
using System.Data.SQLite;

namespace AllBotManager
{
    public partial class Form1 : Form
    { 
        public Form1()
        {
            InitializeComponent();
        }

        public static TelegramBot Mybot;
        public string contents;
        public static Bitmap BM = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        public static double Latitude, Longitude;
        List<HistoryItem> allHistoryItems = new List<HistoryItem>();
        SaveTarget ST = new SaveTarget();
        public bool flagSendTo = false;
        DateTime time = new DateTime();


        private void Connect_Click(object sender, EventArgs e)
        {
            Mybot = new TelegramBot(TokenTxt.Text);
            nameText.Text = Mybot.Me.FirstName;
            idText.Text = Mybot.Me.Id.ToString();
            PollMesssage();
        }

        public async void PollMesssage()
        {
            while (true)
            {
                var result = await Mybot.GetMessages();
                foreach (TelegramBotSharp.Types.Message m in result)
                {
                    if (m.Chat != null)
                    {
                        listBox1.Items.Add(m.From.Username + " : " + m.Text);
                    }
                    else
                    {
                        listBox1.Items.Add(m.From.Username + " : " + m.Text);
                    }
                    ControlMessage(m);
                }
            }
        }

        private void ControlMessage(TelegramBotSharp.Types.Message m)
        {
            TelegramBotSharp.Types.MessageTarget target = (TelegramBotSharp.Types.MessageTarget)m.Chat ?? m.From;

            //target.Id = 21329358;


            ST.SearchAndAdd(target.Id);

            if (!m.Text.Equals(null) || m.Text != "")
            {
                if (m.Text.Contains("Slam") || m.Text.Contains("Salam") || m.Text.Contains("سلام") || m.Text == "slm" || m.Text == "Slm")
                    Mybot.SendMessage(target, "سلام");
                else if (m.Text.Contains("جمال") || m.Text.Contains("jamal"))
                    Mybot.SendMessage(target, "جان جمال");
                else if (m.Text.Contains("/start"))
                    Mybot.SendMessage(target, "به ربات مدیریت کامپیوتر من خوش امدید😁.\nبا استفاده از این ربات می توانید کامپیوتر شخصی خود را مدیرت کنید.");
                else if (m.Text.Contains("خوبی؟") || m.Text.Contains("خوبی") || m.Text.Contains("khobi") || m.Text.Contains("Chetori"))
                    Mybot.SendMessage(target, "ممنون شکر خدا");
                else if (m.Text.Contains("😂"))
                    Mybot.SendMessage(target, "چرا می خند!!!");
                else if (m.Text == "Who are u?" || m.Text == "Who are u" || m.Text == "Who are you?" || m.Text == "who are you?")
                    Mybot.SendMessage(target, "I'm just a Bot😁");
                else if (m.Text == "ساعت")
                {
                    Mybot.SendMessage(target, DateTime.Now.ToString());
                }
                else if (m.Text.Contains("Send me"))
                {
                    foreach (string file in Directory.EnumerateFiles("D:\\music", "*.zip"))
                    {
                        MessageBox.Show(file);
                    }
                }
                else if (m.Text == "ارسال به همه")
                {
                    if (textBox1.Text != "" && textBox1.Text != null)
                    {
                        Mybot.SendMessage(target, textBox1.Text);
                        textBox1.Text = null;
                    }
                    else
                        MessageBox.Show("لطفا متن پیام خود را وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (m.Text == "Sh")
                {
                    Graphics GH = Graphics.FromImage(BM as Image);
                    GH.CopyFromScreen(0, 0, 0, 0, BM.Size);
                    SaveFileDialog SFD = new SaveFileDialog();
                    SFD.Filter = "Image File | *.PNG";
                    SFD.FileName = "salase.png";
                    BM.Save(SFD.FileName);
                    Mybot.SendPhoto(target, new FileStream("salase.png", FileMode.Open), "", "salase.png");
                }
                else if (m.Text == "GetLoc")
                {
                    GetLoc();
                    Mybot.SendMessage(target, "Latitude : " + Latitude.ToString() + "\n" + "Longitude : " + Longitude.ToString());
                }
                else if (m.Text == "Gethistory")
                {
                    Mybot.SendMessage(target, "Out of Order!!!!!!\nSorry");
                }
                else if (m.Text == "Get info")
                {
                    Mybot.SendMessage(target, GetInfo());
                }
                else if (m.Text == "forward")
                {
                    Mybot.ForwardMessage(m, target);
                }
                else if (m.Text.Contains("Lock pc"))
                    Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
                else if (m.Text.Contains("Shutdown"))
                {
                    Sh();
                    Mybot.SendMessage(target, "کامپیوتر خاموش شد.");
                }
                else if (m.Text.Contains("Pala"))
                    OpenFile.Open("steam://rungameid/444090");
                else if (m.Text.Contains("Iopen") || m.Text.Contains("/command1"))
                    OpenFile.Open();
                else if (m.Text.Contains("Unity5.3.5") || m.Text.Contains("U5.3"))
                    OpenFile.Open("C:\\Program Files\\Unity\\Editor\\Unity.exe");
                else if (m.Text.Contains("Unity5.5.1") || m.Text.Contains("U5.5"))
                    OpenFile.Open("D:\\Program Files\\Unity5.5.1\\Unity\\Editor\\Unity.exe");
                else if (m.Text == "Vs")
                    OpenFile.Open("C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\Common7\\IDE\\devenv.exe");
                else if (m.Text == "Edit code")
                    OpenFile.Open("C:\\Users\\Mohammad\\Documents\\Visual Studio 2015\\Projects\\AllBotManager\\AllBotManager.sln");
                else if (m.Text.Contains("www."))
                    OpenFile.Open("Chrome.exe", m.Text);
                else if (m.Text == "en")
                    OpenFile.Open("Chrome.exe", "enroll.azad.ac.ir/Login.aspx");
                else if (m.Text == "V3" || m.Text == "v3")
                    OpenFile.Open("Chrome.exe", "www.varzesh3.com");
                else if (m.Text == "U3" || m.Text == "u3")
                    OpenFile.Open("Chrome.exe", "unity3d.com");
                else if (m.Text.Contains("D:\\") || m.Text.Contains("C:\\") || m.Text.Contains("E:\\"))
                    OpenFile.Open(m.Text);
                else
                    Mybot.SendMessage(target, "😁");
            }

            ST.SaveArray();
        }

        public static void Sh()
        {
            var psi = new ProcessStartInfo("shutdown", "/s /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }

        public static void GetLoc()
        {
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
            watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));
            GeoCoordinate cooder = watcher.Position.Location;

            if(cooder.IsUnknown != true)
            {
                //MessageBox.Show(cooder.Latitude.ToString() + cooder.Longitude.ToString(), "GPS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Latitude = cooder.Latitude;
                Longitude = cooder.Longitude;
            }
        }

        public static string GetInfo()
        {
            string ComputerName = SystemInformation.ComputerName;
            string UserName = SystemInformation.UserName;
            return "ComputerName is : " + ComputerName + "\nUserName is : " + UserName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Connect_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        void a(TelegramBotSharp.Types.Message m)
        {
            TelegramBotSharp.Types.MessageTarget target = (TelegramBotSharp.Types.MessageTarget)m.Chat ?? m.From;

            target.Id = 21329358;

            Mybot.SendMessage(target, textBox1.Text);
        }
    }

    class OpenFile
    {
        public static void Open(string path, string web)
        {
            Process.Start(path, web);
        }
        public static void Open(string path)
        {
            Process.Start(path);
        }
        public static void Open()
        {
            Process.Start("Chrome.exe");
        }
    }

    public class HistoryItem
    {
        public string URL { get; set; }
        public string Title { get; set; }
        public DateTime VisitedTime { get; set; }
    }

    public class BrowserHistory
    {
        List<HistoryItem> allHistoryItems = new List<HistoryItem>();
        public void GetHistory()
        {
            string chromeHistoryFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                + @"\Google\Chrome\User Data\Default\History";

            if(File.Exists(chromeHistoryFile))
            {
                SQLiteConnection connection = new SQLiteConnection ("Data Source=" + chromeHistoryFile + ";Version=3;New=False;Compress=True;");
                connection.Open();

                DataSet dataset = new DataSet();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("select * from urls order by last_visit_time desc", connection);
                adapter.Fill(dataset);

                if (dataset != null && dataset.Tables.Count > 0 & dataset.Tables[0] != null)
                {
                    DataTable dt = dataset.Tables[0];

                    foreach (DataRow historyRow in dt.Rows)
                    {
                        HistoryItem historyItem = new HistoryItem();
                        {
                            historyItem.URL = Convert.ToString(historyRow["url"]);
                            historyItem.Title = Convert.ToString(historyRow["title"]);
    
                    };

                        // Chrome stores time elapsed since Jan 1, 1601 (UTC format) in microseconds
                        long utcMicroSeconds = Convert.ToInt64(historyRow["last_visit_time"]);

                        // Windows file time UTC is in nanoseconds, so multiplying by 10
                        DateTime gmtTime = DateTime.FromFileTimeUtc(10 * utcMicroSeconds);

                        // Converting to local time
                        DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(gmtTime, TimeZoneInfo.Local);
                        historyItem.VisitedTime = localTime;

                        allHistoryItems.Add(historyItem);
                    }
                }
            }
        }
    }
}