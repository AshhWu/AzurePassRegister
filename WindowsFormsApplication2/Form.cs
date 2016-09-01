using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Form : System.Windows.Forms.Form
    {
        string account;
        string pwd;
        string PassCode;
        public Form()
        {
            InitializeComponent();
            webBrowser.Navigate("https://www.microsoftazurepass.com/");

            string line = null;
            int line_number = 1;
            int line_to_delete = 2;

            using (StreamReader reader = new StreamReader("outlook.csv"))
            {
                //System.IO.StreamWriter writer = new System.IO.StreamWriter("write.txt");
                using (StreamWriter writer = new StreamWriter("~outlook.csv"))
                {


                    System.IO.StreamReader file = new System.IO.StreamReader("outlook.csv");

                    //read line
                    /*  Format
                    Index | Account | Password | Applicant
                    */
                    string readline = file.ReadLine();

                    //split by , 
                    string[] words = readline.Split(',');
                    //assign 1st index of array to account 
                    //spilit @outlook.com


                    // SCV file Header
                    if (words[0].Equals("Index"))
                    {
                        readNextRow(line, line_number, line_to_delete, reader, file, writer);
                        Application.Restart();
                    }
                    else
                    {
                        /* 
                          Raw data in CSV : Account_Name@outlook.com\
                          1. spilit by '@'  
                          2. auto named by account 
                          3. add @outlook.com back 
                        */
                        string[] accountArray = words[1].Split('@');
                        //assign account  to fill in only 
                        account = accountArray[0];
                        //assign 2nd index of array to password
                        pwd = words[2];

                        PassCode = words[4];

                        readNextRow(line, line_number, line_to_delete, reader, file, writer);
                    }
                }
            }

            System.IO.File.Delete("outlook.csv");
            System.IO.File.Move("~outlook.csv", "outlook.csv");

        }

        private void readNextRow(string line, int line_number, int line_to_delete, StreamReader reader, System.IO.StreamReader file, StreamWriter writer)
        {
            file.Close();

            while ((line = reader.ReadLine()) != null)
            {

                line_number++;

                if (line_number == line_to_delete)
                    continue;

                writer.WriteLine(line);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string currentUrl = webBrowser.Url.ToString();
            Debug.WriteLine("current url : " + currentUrl);
            if (currentUrl == "https://www.microsoftazurepass.com/")
            {
                HtmlElement fillElement = webBrowser.Document.GetElementById("ddlCountry");
                fillElement.Focus();
                SendKeys.SendWait("T");
                SendKeys.SendWait("{TAB}");

                SendKeys.SendWait(PassCode);
                SendKeys.SendWait("{ENTER}");
            }
            else if (currentUrl == "https://www.microsoftazurepass.com/AAD?returnUrl=%2FRegistration&isAccountExisted=False")
            {
                HtmlElement fillElement = webBrowser.Document.GetElementById("loginLink");
                fillElement.Focus();
                SendKeys.SendWait("{ENTER}");
            }
            else if (currentUrl.StartsWith("https://login.microsoftonline.com/"))
            {
                HtmlElement fillElement = webBrowser.Document.GetElementById("use_another_account");
                fillElement.Focus();
                SendKeys.SendWait("{ENTER}");
            }
            else
            {

            }
        }

    }
}
