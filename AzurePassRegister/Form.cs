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
        string promoCode;
        int stage;
        bool promoCodeIsAlreadyUsed;

        public Form()
        {
            InitializeComponent();

            //Disable Script Errors in WebBrowser Control
            webBrowser.ScriptErrorsSuppressed = true;

            webBrowser.Navigate("https://account.windowsazure.com/Subscriptions");

            //excel control
            string line = null;
            int line_number = 1;
            int line_to_delete = 2;
            promoCodeIsAlreadyUsed = false;

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

                        promoCode = words[4];

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
            url.Text = currentUrl;

            // 1. Login page
            if(currentUrl.StartsWith("https://login.live.com/login.srf?"))
            {
                Debug.WriteLine("1. Login page");
                infoText.Text = account;

                stage++;
                Debug.WriteLine("stage:"+stage);
                if(stage == 3)
                {
                    infoText.Text = account;
                    SendKeys.SendWait(account);
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait(pwd);
                    SendKeys.SendWait("{ENTER}");
                }
            }

            // 2. Azure subscription page
            else if(currentUrl == "https://account.windowsazure.com/Subscriptions")
            {
                Debug.WriteLine("2. Azure subscription page");

                HtmlElement subscriptionList = webBrowser.Document.GetElementById("subscriptions-list");
                if (subscriptionList != null)
                {
                    Debug.WriteLine("azure pass already exist.");
                    infoText.Text = account + " : azure pass already exist.";
                    Debug.WriteLine("exit");
                    Application.Restart();
                }
                else
                {
                    stage++;
                    Debug.WriteLine("stage:" + stage);

                    infoText.Text = account + " : register azure pass";
                    webBrowser.Navigate("https://www.microsoftazurepass.com/");
                }
            }

            // 3. Azure Pass home page
            else if (currentUrl == "https://www.microsoftazurepass.com/")
            {
                Debug.WriteLine("3. Azure Pass home page");

                if (!promoCodeIsAlreadyUsed)
                {
                    promoCodeIsAlreadyUsed = true;
                    infoText.Text = account;

                    HtmlElement fillElement = webBrowser.Document.GetElementById("ddlCountry");
                    fillElement.SetAttribute("value", "830974e9-147b-e011-8167-001f29c6fb82");

                    fillElement = webBrowser.Document.GetElementById("tbPromo");
                    fillElement.SetAttribute("value", promoCode);
                    fillElement.Focus();
                    SendKeys.SendWait("{ENTER}");
                }
                else
                {
                    Debug.WriteLine("Promo code is already used.");
                    infoText.Text = account + " : Promo code is already used";
                }
            }

            // 4. login bottom
            else if (currentUrl == "https://www.microsoftazurepass.com/AAD?returnUrl=%2FRegistration&isAccountExisted=False")
            {
                Debug.WriteLine("4. login bottom");

                HtmlElement fillElement = webBrowser.Document.GetElementById("loginLink");
                fillElement.Focus();
                SendKeys.SendWait("{ENTER}");
            }
            // Azure pass already exist
            else if (currentUrl == "https://www.microsoftazurepass.com/Registration/AzureAccountExist")
            {
                Debug.WriteLine("Azure pass already exist");
                infoText.Text = account + " : Azure pass already exist";
                //go to info form page
                webBrowser.Navigate("https://account.windowsazure.com/signup?offer=MS-AZR-0124P");
            }

            // 5. Authentication
            else if (currentUrl == "https://www.microsoftazurepass.com/Registration?authType=AAD")
            {
                Debug.WriteLine("5. Authentication");
                infoText.Text = account + " : Authentication";

                HtmlElement submit = webBrowser.Document.GetElementById("LastName");
                submit.Focus();
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{ENTER}");
                infoText.Text = account + " : Authentication click";

            }

            // Go to info form page
            else if (currentUrl == "https://account.windowsazure.com/signup?offer=MS-AZR-0044P")
            {
                webBrowser.Navigate("https://account.windowsazure.com/signup?offer=MS-AZR-0124P");
            }
            else if (currentUrl == "https://www.microsoftazurepass.com/Message")
            {
                webBrowser.Navigate("https://account.windowsazure.com/signup?offer=MS-AZR-0124P");
            }

            // 6. Fill in the info form
            else if (currentUrl == "https://account.windowsazure.com/signup?offer=MS-AZR-0124P")
            {
                stage++;
                Debug.WriteLine("stage:" + stage);
                infoText.Text = account + " : Filling in info";

                if (stage == 15)
                {
                    string result = await WaitFor(13500);

                        infoText.Text = account + ": Tax";
                        HtmlElement fillElement = webBrowser.Document.GetElementById("CustomerInfo_TaxId");
                        fillElement.SetAttribute("value", "00000000");
                        fillElement.Focus();
                        SendKeys.SendWait("{TAB}");
                        SendKeys.SendWait("{ENTER}");

                        result = await WaitFor(1000);

                        infoText.Text = account + " : Phone";
                        fillElement = webBrowser.Document.GetElementById("phone-no-countrySelector");
                        fillElement.Focus();
                        SendKeys.SendWait("{TAB}");
                        SendKeys.SendWait("912345678");
                        SendKeys.SendWait("{TAB}");
                        SendKeys.SendWait("{ENTER}");

                        result = await WaitFor(15000);

                    infoText.Text = account + " : Check agreement";
                    HtmlElement check = webBrowser.Document.GetElementById("checkbox-rateplan");
                    check.Focus();
                    SendKeys.SendWait(" ");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{TAB}");
                    SendKeys.SendWait("{ENTER}");
                }
            }

            // 7. Success
            else if (currentUrl.StartsWith("https://account.windowsazure.com/signuplanding/"))
            {
                infoText.Text = account + " : Success!";
                stage++;
                Debug.WriteLine("stage:" + stage);
                if(stage == 19)
                {
                    Debug.WriteLine("exit");
                    Application.Restart();
                }
            }
            // 8. Exit
            else if(currentUrl.StartsWith("https://www.google.com/"))
            {
                Debug.WriteLine("exit");
                Application.Restart();
            }

            //Other situation
            else
            {
                Debug.WriteLine("Not in the situation");
                infoText.Text = account + " : Not in the situation";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void refresh_Click(object sender, EventArgs e)
        {
            webBrowser.Refresh();
        }

        private void url_Click(object sender, EventArgs e)
        {

        }

        private void url_TextChanged(object sender, EventArgs e)
        {

        }

        private void GoButtom_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(url.Text);
        }
    }
}
