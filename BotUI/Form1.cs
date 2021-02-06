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
using System.Reflection;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Configuration;

using Bot;
using BotUI.API;
using LeagueBot;
using LeagueBot.LCU;
using LeagueBot.DEBUG;
using hotKey;

namespace BotUI {
    public partial class Form1 : Form {

        private LeagueBot.Bot bot = new LeagueBot.Bot();
        private Thread botThread = null;


        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Assembly a = Assembly.LoadFrom("Bot.dll");
            Version v = a.GetName().Version;
            botVer.Text = $"Bot version: {v}";

            HotKeyManager.RegisterHotKey(Keys.F10, KeyModifiers.Alt);
            HotKeyManager.HotKeyPressed += new EventHandler<HotKeyEventArgs>(stopButton_Click);
            //set_start_button();


            #if DEBUG
                mode_lable.Text = "DEBUG MODE!";
                DBG.isDBG = true;
                BotConf.FilePath = ConfigurationManager.AppSettings.Get("LOL_FILE_PATH_DBG");
            #else
                BotConf.FilePath = ConfigurationManager.AppSettings.Get("LOL_FILE_PATH");
                mode_lable.Visible = false;
                toolStrip1.Enabled = false;
            #endif

            if(ConfigurationManager.AppSettings.Get("Read me") == "NOPE" && !DBG.isDBG) {
                MessageBox.Show("Read README.MD", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            Task t = Task.Run(() => {
                DBG.init();
                bot.init();
                DBG.log("DBG INIT DONE");
                startButton.Invoke(new Action(() => startButton.Enabled = true));
            });
            

        }

        public static byte[] GetHash(string inputString) {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString) {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2")); // WTF?
            return sb.ToString();
        }

        private async void set_start_button() {
            bool retry = false;
            do {
                bool is_valid = await apiManeger.validateVersion();
                startButton.Enabled = is_valid;

                if (!is_valid) {
                    DialogResult failres = MessageBox.Show(null, "Version mismatch", "LOL BOT", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                    retry = failres == DialogResult.Retry;
                    if(failres == DialogResult.Abort) {
                        Close();
                    } else if(failres == DialogResult.Ignore) {
                        string pass = BotUI.Prompt.ShowDialog("Pass",string.Empty);
                        if(GetHashString(pass) == "F9171164593756E56FB197327B529A4955590566560DBE62D586BFF41BE9D297") { // just for anti dump
                            startButton.Enabled = true;
                        }
                    }
                }
            } while(retry);
        }

        private void startButton_Click(object sender, EventArgs e) {

            if (botThread == null || !bot.working) {
                botThread = new Thread(bot.ThreadProc);
                botThread.Start();
            } else {
                DialogResult res = MessageBox.Show("Bot migth be runing. force new start?","LOL BOT",MessageBoxButtons.YesNo);
                if(res == DialogResult.Yes) {
                    bot.Abort();
                    //botThread.Abort();
                    botThread = null;
                    botThread = new Thread(bot.ThreadProc);
                    botThread.Start();
                }
            }
        }

        private void stopButton_Click(object sender, EventArgs e) {
            bot.Abort();
            if(botThread != null) {
                botThread.Interrupt();
            }
            botThread = null;
        }

        private void isRGM_CheckedChanged(object sender, EventArgs e) {
            bot.isEvent = isRGM.Checked;
            Debug.WriteLine($"event set to {bot.isEvent}");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            DBG.end();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            Form hb = new AboutBox1();
            hb.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) {
            gameFlowPhase phase = clientLCU.GetGamePhase();
            button1.Text = phase.ToString();
        }
    }
}
