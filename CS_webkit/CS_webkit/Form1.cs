using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_webkit
{
    public partial class Form1 : Form
    {
        public ArrayList m_ALUrl = new ArrayList();
        public int m_intCount;
        public int m_intCloseCount;
        public String m_StrBuf = "";
        WebKit.WebKitBrowser browser = new WebKit.WebKitBrowser();
        public Form1()
        {
            InitializeComponent();
            browser.Dock = DockStyle.Fill;
            this.Controls.Add(browser);
            browser.Navigate("http://www.google.com");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_intCount = 0;
            m_intCloseCount = 0;
            m_StrBuf = "";
            StreamReader sr = new StreamReader("google_seo.txt");
            while (!sr.EndOfStream)// 每次讀取一行，直到檔尾
            {
                string line = sr.ReadLine();// 讀取文字到 line 變數
                if (line != "")
                {
                    m_ALUrl.Add(line);
                }
            }
            sr.Close();// 關閉串流

            Random R = new Random();
            int i = R.Next(0, (m_ALUrl.Count - 1));
            browser.Navigate(m_ALUrl[i].ToString());

            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            m_intCount++;

            Random R = new Random();
            int i = R.Next(0, (m_ALUrl.Count - 1));

            if (m_intCount >= 300)
            {

                browser.Navigate(m_ALUrl[i].ToString());
                m_intCount = 0;
                m_StrBuf = String.Format("{0}/{1}", i, m_ALUrl.Count);
                m_intCloseCount++;
            }
            this.Text = "" + m_intCount + "sec ~ " + m_StrBuf;
            if (m_intCloseCount >= 3)
            {
                this.Close();
            }
            timer1.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process proc = Process.Start("CS_webkit.exe");
        }
    }
}
