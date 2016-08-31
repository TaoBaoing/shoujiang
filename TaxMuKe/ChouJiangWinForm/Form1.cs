using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ChouJiangWinForm
{
    public partial class Form1 : Form
    {
        private  readonly string HostAddress = "http://121.42.136.226:8084/";
        private List<LuckyDto> mCanUseLucky; 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            GetCanUseLucky();
        }
        
        public  void GetCanUseLucky()
        {
            WebClient wc = new WebClient();
            wc.DownloadStringAsync(new Uri(HostAddress + "KaHaoRecord/GetCanUseLucky"));
            wc.DownloadStringCompleted += Wc_DownloadStringCompleted;
        }

        private  void Wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            mCanUseLucky = JsonConvert.DeserializeObject<List<LuckyDto>>(e.Result);
            
            button1.Enabled = true;
        }

        private bool isyunxing = false;
        private string luckcode;
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "开始")
            {
                isyunxing = true;
                button1.Text = "暂停";

                if (mCanUseLucky.Count < 1)
                {
                    MessageBox.Show("没有可抽奖的号码");
                    label1.Text = "";
                    return;
                }
                while (isyunxing)
                {
                    foreach (var pair in mCanUseLucky)
                    {
                        if (!isyunxing)
                        {
                            break;
                        }

                        Thread.Sleep(5);
                        Application.DoEvents();
                        label1.Text = pair.LuckCode;
                        luckcode = pair.Code;
                       
                    }
                }
               
            }
            else
            {
                if (isyunxing)
                {
                    isyunxing = false;
                }
                //luckcode 中奖卡号
                button1.Text = "开始";
                mCanUseLucky.Remove(mCanUseLucky.First(x => x.Code == luckcode));
                //MessageBox.Show(luckcode);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isyunxing = false;
            Thread.Sleep(10);
            Application.ExitThread();
            Application.Exit();
        }
    }
}
