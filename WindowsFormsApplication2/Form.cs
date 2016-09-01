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
        int stage;
        public Form()
        {
            InitializeComponent();
            //webBrowser.Navigate("https://www.microsoftazurepass.com/");
            webBrowser.Navigate("https://account.windowsazure.com/Subscriptions");
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
                        //assign account  to fill in only 
                        account = words[1];
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
            stage = 0;
            Debug.WriteLine("stage:"+stage);
        }

        public async Task<string> WaitAsynchronouslyAsync()
        {
            await Task.Delay(13000);
            return "Finished";
        }
        public async Task<string> WaitFor(int n)
        {
            Debug.WriteLine("wait");
            await Task.Delay(n);
            Debug.WriteLine("start");
            return "Finished";
        }
        private async void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string currentUrl = webBrowser.Url.ToString();
            Debug.WriteLine("current url : " + currentUrl);
            if(currentUrl.StartsWith("https://login.live.com/login.srf?"))
            {
                stage++;
                Debug.WriteLine("stage:"+stage);
                if(stage == 3)
                {
                    accountText.Text = account;
                    SendKeys.SendWait(account);
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait(pwd);
                    SendKeys.SendWait("{ENTER}");
                }
            }
            else if(currentUrl == "https://account.windowsazure.com/Subscriptions")
            {
                stage++;
                Debug.WriteLine("stage:" + stage);
                if (stage == 6)
                {
                    webBrowser.Navigate("https://account.windowsazure.com/signup?offer=MS-AZR-0044P");
                }
            }
            else if (currentUrl == "https://account.windowsazure.com/signup?offer=MS-AZR-0044P")
            {
                stage++;
                Debug.WriteLine("stage:" + stage);
                if (stage == 14)
                {

                   webBrowser.Navigate("https://account.windowsazure.com/signup?offer=MS-AZR-0124P");
                    
                }
            }
            else if(currentUrl == "https://account.windowsazure.com/signup?offer=MS-AZR-0124P")
            {
                stage++;
                Debug.WriteLine("stage:" + stage);
                if(stage == 20)
                {
                    Debug.WriteLine("Wait!");
                    string result = await WaitAsynchronouslyAsync();
                    Debug.WriteLine("start");
                    HtmlElement fillElement = webBrowser.Document.GetElementById("CustomerInfo_TaxId");
                    fillElement.SetAttribute("value", "00000000");
                    fillElement.Focus();
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{ENTER}");

                    WaitFor(1000);

                    fillElement = webBrowser.Document.GetElementById("phone-no-countrySelector");
                    fillElement.Focus();
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("912345678");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{ENTER}");

                    WaitFor(15000);

                    fillElement = webBrowser.Document.GetElementById("checkbox-rateplan");
                    fillElement.Focus();
                    SendKeys.SendWait(" ");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{TAB}");
                    //SendKeys.SendWait("{ENTER}");
                }
            }
            /*
            if (currentUrl == "https://www.microsoftazurepass.com/")
            {
                HtmlElement fillElement = webBrowser.Document.GetElementById("ddlCountry");
                fillElement.SetAttribute("value", "830974e9-147b-e011-8167-001f29c6fb82");

                fillElement = webBrowser.Document.GetElementById("tbPromo");
                fillElement.SetAttribute("value", PassCode);
                fillElement.Focus();
                SendKeys.SendWait("{ENTER}");
            }
            else if (currentUrl == "https://www.microsoftazurepass.com/AAD?returnUrl=%2FRegistration&isAccountExisted=False")
            {
                HtmlElement fillElement = webBrowser.Document.GetElementById("loginLink");
                fillElement.Focus();
                SendKeys.SendWait("{ENTER}");
            }
            
            else if (currentUrl.StartsWith("https://login.microsoftonline.com/72f988bf-86f1-41af-91ab-2d7cd011db47") && !(currentUrl.EndsWith("#")))
            {
                HtmlElement fillElement = webBrowser.Document.GetElementById("use_another_account");
                fillElement.Focus();
                SendKeys.SendWait("{ENTER}");
            }
            else if (currentUrl.StartsWith("https://login.microsoftonline.com/") && currentUrl.EndsWith("#"))
            {
                accountText.Text = account;
                Debug.WriteLine("account:" + account);
                HtmlElement fillElement = webBrowser.Document.GetElementById("cred_userid_inputtext");
                fillElement.Focus();
                SendKeys.SendWait(account);
                SendKeys.SendWait("{TAB}");
                
            }
            else if (currentUrl.StartsWith("https://login.live.com/"))
            {
                Debug.WriteLine("outlook login");
                accountText.Text = pwd;
                Debug.WriteLine("password");
                SendKeys.Send(pwd);
                
            }
            else if (currentUrl == "https://www.microsoftazurepass.com/Registration?authType=AAD")
            {
                HtmlElement submit = webBrowser.Document.GetElementById("LastName");
                submit.Focus();
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{ENTER}");
            }
            else if(currentUrl == "https://www.microsoftazurepass.com/Message")
            {
                webBrowser.Navigate("https://account.windowsazure.com/");
            }
            else if(currentUrl == "https://account.windowsazure.com/signup?offer=ms-azr-0124p")
            {
                
                    HtmlElement fillElement = webBrowser.Document.GetElementById("CustomerInfo_TaxId");
                    fillElement.SetAttribute("value", "00000000");
                    fillElement.Focus();
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{ENTER}");

                    fillElement = webBrowser.Document.GetElementById("phone-no-hint");
                    fillElement.SetAttribute("value", "912345678");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{ENTER}");

                    fillElement = webBrowser.Document.GetElementById("checkbox-rateplan");
                    SendKeys.SendWait(" ");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{ENTER}");
                
            }
            else
            {

            } */
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
