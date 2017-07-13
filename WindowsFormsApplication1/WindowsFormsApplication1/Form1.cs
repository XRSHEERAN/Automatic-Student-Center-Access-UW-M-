using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private int Loaded;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Loaded = 0;
            
            webBrowser1.Navigate("https://my.wisc.edu");
            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(load_ready);
            while ( Loaded<1)
                Application.DoEvents();
            //Fill in the user_name & password
            Loaded = 0;
            HtmlElement usr_name= webBrowser1.Document.GetElementById("j_username");
            HtmlElement usr_pass = webBrowser1.Document.GetElementById("j_password");
            HtmlElement usr_submit = webBrowser1.Document.All["_eventId_proceed"];
            usr_name.InnerText = textBox1.Text;
            usr_pass.InnerText = textBox2.Text;
            usr_submit.InvokeMember("click");

            while (Loaded<2)
                Application.DoEvents();
            
            //prevent potetial input mistake
            String content = webBrowser1.Document.Body.InnerHtml;
            if (content.Contains("Login failed"))
                MessageBox.Show("please enter a valid account/pass");
            
            webBrowser1.Navigate("https://my.wisc.edu/portal/p/student-center/max/action.uP?pP_action=loginAction");

        }
        private void load_ready(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Loaded +=1;
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
