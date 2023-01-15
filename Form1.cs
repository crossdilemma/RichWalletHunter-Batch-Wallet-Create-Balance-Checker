using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NBitcoin;
using Nethereum.HdWallet;
using System.Net.Http;
using Newtonsoft.Json;

namespace BulkWalletGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
  
        private void Form1_Load(object sender, EventArgs e)
        {
             


        }


        public class ExplorerList
        {
            public string name { get; set; }
            public string urlprefix { get; set; }
            public string apikey { get; set; }
        }

        public class Exp
        {
            public int explorercount { get; set; }
            public List<ExplorerList> explorerList { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(DateTime.Now + " # Loading Explorer Config");
            try
            {
                Exp explorerList = JsonConvert.DeserializeObject<Exp>(File.ReadAllText((@Path.GetDirectoryName(Application.ExecutablePath) + "\\explorerConfig.txt")));

                int length = explorerList.explorercount;

                listView1.Items.Clear();
                for (int i = 0; i < length; i++)
                {

                    string[] row = { explorerList.explorerList[i].name, explorerList.explorerList[i].urlprefix, explorerList.explorerList[i].apikey };
                    var listViewItem = new ListViewItem(row);
                    listView1.Items.Add(listViewItem);
                }
            }
            catch (Exception)
            {
                throw;
            }
            Console.WriteLine(DateTime.Now + " # Explorer Config Successfully Loaded");


        }
        public class Result
        {
            public string account { get; set; }
            public float balance { get; set; }
        }
        public class ExplorerInfo
        {
            public string status { get; set; }
            public string message { get; set; }
            public List<Result> result { get; set; }
        }

        public string GenerateMnemonic()
        {
            Mnemonic mnemogen = new Mnemonic(Wordlist.English, WordCount.Twelve);
            return mnemogen.ToString();
        }



        private void button4_Click(object sender, EventArgs e)
        {

            float balance = 0;
            int loop = 0;
            string balancefound = "";
            DateTime startTime, endTime;
            startTime = DateTime.Now;


            try
            {
                while (1 > 0)
                {
                    string[] mnemo = new string[100];
                    string[] address = new string[100];
                    string[] privatekey = new string[100];
                    float[] explorerResult1 = new float[100];
                    float[] explorerResult2 = new float[100];
                    float[] explorerResult3 = new float[100];
                    float[] explorerResult4 = new float[100];
                    float[] explorerResult5 = new float[100];
                    float[] explorerResult6 = new float[100];
                    float[] explorerResult7 = new float[100];
                    float[] explorerResult8 = new float[100];
                    float[] explorerResult9 = new float[100];
                    float[] explorerResult10 = new float[100];
                    if (loop > 0)
                    {
                        endTime = DateTime.Now;
                        Double elapsedSecs = ((TimeSpan)(endTime - startTime)).TotalSeconds;
                        Console.WriteLine(DateTime.Now + " # No Luck at Batch: " + (loop));
                        Console.WriteLine(DateTime.Now + " # Average Wallet Check Rate: " + (loop * 100) / (elapsedSecs) + " Wallet per Second");
                    }



                    Console.WriteLine(DateTime.Now + " # Generating " + (loop * 100) + " - " + ((loop + 1) * 100) + " Mnemonic & Address & Private Keys");
                    if (balance > 0)
                    {

                        try
                        {
                            FileStream fs2 = new FileStream(@Path.GetDirectoryName(Application.ExecutablePath) + "\\walletdata.txt", FileMode.Append);
                            TextWriter sw2 = new StreamWriter(fs2);
                            sw2.WriteLine(balancefound);
                            sw2.Close();
                             balance = 0;
                        }
                        catch
                        {

                        }
                    }
                    for (int i = 0; i < 100; i++)
                    {
                        mnemo[i] = GenerateMnemonic();
                        var wallet = new Wallet(mnemo[i], "");
                        var account = wallet.GetAccount(0);
                        address[i] = account.Address;
                        privatekey[i] = account.PrivateKey;
                        wallet = null;
                        account = null;
                    }
                    Console.WriteLine(DateTime.Now + " # " + (loop * 100) + " - " + ((loop + 1) * 100) + " Wallet Batch Generated");
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        if (balance > 0)
                        {

                        }
                        else
                        {
                            for (int s = 0; s < 5; s++)
                            {
                                Console.WriteLine(DateTime.Now + " # Checking Balances: " + ((loop * 100) + (s * 20)) + " - " + ((loop * 100) + ((s + 1) * 20)) + " Address Batch");
                                string url = listView1.Items[i].SubItems[1].Text +
                                                "?module=account&action=balancemulti&address=" +
                                                address[s * 20 + 0] + "," +
                                                address[s * 20 + 1] + "," +
                                                address[s * 20 + 2] + "," +
                                                address[s * 20 + 3] + "," +
                                                address[s * 20 + 4] + "," +
                                                address[s * 20 + 5] + "," +
                                                address[s * 20 + 6] + "," +
                                                address[s * 20 + 7] + "," +
                                                address[s * 20 + 8] + "," +
                                                address[s * 20 + 9] + "," +
                                                address[s * 20 + 10] + "," +
                                                address[s * 20 + 11] + "," +
                                                address[s * 20 + 12] + "," +
                                                address[s * 20 + 13] + "," +
                                                address[s * 20 + 14] + "," +
                                                address[s * 20 + 15] + "," +
                                               address[s * 20 + 16] + "," +
                                                address[s * 20 + 17] + "," +
                                                address[s * 20 + 18] + "," +
                                                address[s * 20 + 19] +
                                                "&tag=latest&apikey=" + listView1.Items[i].SubItems[2].Text;
                                HttpClient client = new HttpClient();
                                client.BaseAddress = new Uri(url);
                                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                                HttpResponseMessage response = client.GetAsync(url).Result;
                                var result = response.Content.ReadAsStringAsync().Result;
                                ExplorerInfo myDeserializedClass = JsonConvert.DeserializeObject<ExplorerInfo>(result);
                                if (s == 0)
                                {
                                    for (int j = 0; j < 20; j++)
                                    {
                                        if (i == 0)
                                        {
                                            explorerResult1[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 1)
                                        {
                                            explorerResult2[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 2)
                                        {
                                            explorerResult3[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 3)
                                        {
                                            explorerResult4[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 4)
                                        {
                                            explorerResult5[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 5)
                                        {
                                            explorerResult6[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 6)
                                        {
                                            explorerResult7[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 7)
                                        {
                                            explorerResult8[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 8)
                                        {
                                            explorerResult9[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 9)
                                        {
                                            explorerResult10[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }

                                    }
                                }
                                if (s == 1)
                                {
                                    for (int j = 0; j < 20; j++)
                                    {
                                        if (i == 0)
                                        {
                                            explorerResult1[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 1)
                                        {
                                            explorerResult2[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 2)
                                        {
                                            explorerResult3[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 3)
                                        {
                                            explorerResult4[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 4)
                                        {
                                            explorerResult5[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 5)
                                        {
                                            explorerResult6[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 6)
                                        {
                                            explorerResult7[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 7)
                                        {
                                            explorerResult8[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 8)
                                        {
                                            explorerResult9[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 9)
                                        {
                                            explorerResult10[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }

                                    }
                                }
                                if (s == 2)
                                {
                                    for (int j = 0; j < 20; j++)
                                    {
                                        if (i == 0)
                                        {
                                            explorerResult1[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 1)
                                        {
                                            explorerResult2[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 2)
                                        {
                                            explorerResult3[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 3)
                                        {
                                            explorerResult4[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 4)
                                        {
                                            explorerResult5[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 5)
                                        {
                                            explorerResult6[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 6)
                                        {
                                            explorerResult7[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 7)
                                        {
                                            explorerResult8[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 8)
                                        {
                                            explorerResult9[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 9)
                                        {
                                            explorerResult10[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }

                                    }
                                }
                                if (s == 3)
                                {
                                    for (int j = 0; j < 20; j++)
                                    {
                                        if (i == 0)
                                        {
                                            explorerResult1[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 1)
                                        {
                                            explorerResult2[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 2)
                                        {
                                            explorerResult3[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 3)
                                        {
                                            explorerResult4[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 4)
                                        {
                                            explorerResult5[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 5)
                                        {
                                            explorerResult6[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 6)
                                        {
                                            explorerResult7[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 7)
                                        {
                                            explorerResult8[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 8)
                                        {
                                            explorerResult9[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 9)
                                        {
                                            explorerResult10[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }

                                    }
                                }
                                if (s == 4)
                                {
                                    for (int j = 0; j < 20; j++)
                                    {
                                        if (i == 0)
                                        {
                                            explorerResult1[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 1)
                                        {
                                            explorerResult2[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 2)
                                        {
                                            explorerResult3[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 3)
                                        {
                                            explorerResult4[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 4)
                                        {
                                            explorerResult5[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 5)
                                        {
                                            explorerResult6[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 6)
                                        {
                                            explorerResult7[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 7)
                                        {
                                            explorerResult8[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 8)
                                        {
                                            explorerResult9[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }
                                        if (i == 9)
                                        {
                                            explorerResult10[s * 20 + j] = myDeserializedClass.result[j].balance / 1000000000000000000;
                                        }

                                    }
                                }
                            }
                            if (listView1.Items.Count == 1)
                            {
                                for (int k = 0; k < 100; k++)
                                {

                                    if (explorerResult1[k] > 0)
                                    {
                                        Console.WriteLine(
                                        "Account index : {0}\n" +
                                        "Mnemonic : {1}\n" +
                                        "Address : {2}\n" +
                                        "Private key : {3}\n" +
                                        listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k]);
                                        balance = explorerResult1[k];
                                        balancefound = "Account index : " + (loop * 100) + k + 1 + "\n" +
                                        "Mnemonic : " + mnemo[k] + "\n" +
                                        "Address : " + address[k] + "\n" +
                                        "Private key : " + privatekey[k] + "\n" +
                                        listView1.Items[i].SubItems[0].Text + " Balance : " + balance + "\n";


                                    }
                                    else
                                    {
                                        Console.WriteLine(
                                        "Account index : {0}\n" +
                                        "Mnemonic : {1}\n" +
                                        "Address : {2}\n" +
                                        "Private key : {3}\n" +
                                        listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k]);
                                    }

                                }
                            }
                            if (listView1.Items.Count == 2)
                            {
                                if (i == listView1.Items.Count - 1)
                                {
                                    for (int k = 0; k < 100; k++)
                                    {
                                        if (explorerResult1[k] > 0 | explorerResult2[k] > 0)
                                        {
                                            Console.WriteLine(
                                            "Account index : {0}\n" +
                                            "Mnemonic : {1}\n" +
                                            "Address : {2}\n" +
                                            "Private key : {3}\n" +
                                            listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k]);
                                            balance = explorerResult1[k] + explorerResult2[k];
                                            balancefound = "Account index : " + (loop * 100) + k + 1 + "\n" +
                                            "Mnemonic : " + mnemo[k] + "\n" +
                                            "Address : " + address[k] + "\n" +
                                            "Private key : " + privatekey[k] + "\n";


                                        }

                                        else
                                        {
                                            Console.WriteLine(
                                                "Account index : {0}\n" +
                                                "Mnemonic : {1}\n" +
                                                "Address : {2}\n" +
                                                "Private key : {3}\n" +
                                                listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k]);
                                        }
                                    }
                                }

                            }
                            if (listView1.Items.Count == 3)
                            {
                                if (i == listView1.Items.Count - 1)
                                {
                                    for (int k = 0; k < 100; k++)
                                    {
                                        if (explorerResult1[k] > 0 | explorerResult2[k] > 0 | explorerResult3[k] > 0)
                                        {
                                            Console.WriteLine(
                                            "Account index : {0}\n" +
                                            "Mnemonic : {1}\n" +
                                            "Address : {2}\n" +
                                            "Private key : {3}\n" +
                                            listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k]);
                                            balance = explorerResult1[k] + explorerResult2[k] + explorerResult3[k];
                                            balancefound = "Account index : " + (loop * 100) + k + 1 + "\n" +
                                            "Mnemonic : " + mnemo[k] + "\n" +
                                            "Address : " + address[k] + "\n" +
                                            "Private key : " + privatekey[k] + "\n";


                                        }

                                        else
                                        {

                                            Console.WriteLine(
                                                "Account index : {0}\n" +
                                                "Mnemonic : {1}\n" +
                                                "Address : {2}\n" +
                                                "Private key : {3}\n" +
                                                listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k]);
                                        }
                                    }
                                }

                            }
                            if (listView1.Items.Count == 4)
                            {
                                if (i == listView1.Items.Count - 1)
                                {
                                    for (int k = 0; k < 100; k++)
                                    {
                                        if (explorerResult1[k] > 0 | explorerResult2[k] > 0 | explorerResult3[k] > 0 | explorerResult4[k] > 0)
                                        {
                                            Console.WriteLine(
                                            "Account index : {0}\n" +
                                            "Mnemonic : {1}\n" +
                                            "Address : {2}\n" +
                                            "Private key : {3}\n" +
                                             listView1.Items[i - 3].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k], explorerResult4[k]);
                                            balance = explorerResult1[k] + explorerResult2[k] + explorerResult3[k] + explorerResult4[k];
                                            balancefound = "Account index : " + (loop * 100) + k + 1 + "\n" +
                                            "Mnemonic : " + mnemo[k] + "\n" +
                                            "Address : " + address[k] + "\n" +
                                            "Private key : " + privatekey[k] + "\n";


                                        }

                                        else
                                        {

                                            Console.WriteLine(
                                                "Account index : {0}\n" +
                                                "Mnemonic : {1}\n" +
                                                "Address : {2}\n" +
                                                "Private key : {3}\n" +
                                                listView1.Items[i - 3].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k], explorerResult4[k]);
                                        }
                                    }
                                }
                            }
                            if (listView1.Items.Count == 5)
                            {
                                if (i == listView1.Items.Count - 1)
                                {
                                    for (int k = 0; k < 100; k++)
                                    {
                                        if (explorerResult1[k] > 0 | explorerResult2[k] > 0 | explorerResult3[k] > 0 | explorerResult4[k] > 0 | explorerResult5[k] > 0)
                                        {
                                            Console.WriteLine(
                                            "Account index : {0}\n" +
                                            "Mnemonic : {1}\n" +
                                            "Address : {2}\n" +
                                            "Private key : {3}\n" +
                                              listView1.Items[i - 4].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i - 3].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k], explorerResult4[k], explorerResult5[k]);
                                            balance = explorerResult1[k] + explorerResult2[k] + explorerResult3[k] + explorerResult4[k] + explorerResult5[k];
                                            balancefound = "Account index : " + (loop * 100) + k + 1 + "\n" +
                                            "Mnemonic : " + mnemo[k] + "\n" +
                                            "Address : " + address[k] + "\n" +
                                            "Private key : " + privatekey[k] + "\n";


                                        }

                                        else
                                        {

                                            Console.WriteLine(
                                                "Account index : {0}\n" +
                                                "Mnemonic : {1}\n" +
                                                "Address : {2}\n" +
                                                "Private key : {3}\n" +
                                                listView1.Items[i - 4].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i - 3].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                                listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k], explorerResult4[k], explorerResult5[k]);
                                        }
                                    }
                                }
                            }
                            if (listView1.Items.Count == 6)
                            {
                                if (i == listView1.Items.Count - 1)
                                {
                                    for (int k = 0; k < 100; k++)
                                    {
                                        if (explorerResult1[k] > 0 | explorerResult2[k] > 0 | explorerResult3[k] > 0 | explorerResult4[k] > 0 | explorerResult5[k] > 0 | explorerResult6[k] > 0)
                                        {
                                            Console.WriteLine(
                                            "Account index : {0}\n" +
                                            "Mnemonic : {1}\n" +
                                            "Address : {2}\n" +
                                            "Private key : {3}\n" +
                                             listView1.Items[i - 5].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 4].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 3].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k], explorerResult4[k], explorerResult5[k], explorerResult6[k]);
                                            balance = explorerResult1[k] + explorerResult2[k] + explorerResult3[k] + explorerResult4[k] + explorerResult5[k] + explorerResult6[k];
                                            balancefound = "Account index : " + (loop * 100) + k + 1 + "\n" +
                                            "Mnemonic : " + mnemo[k] + "\n" +
                                            "Address : " + address[k] + "\n" +
                                            "Private key : " + privatekey[k] + "\n";


                                        }

                                        else
                                        {

                                            Console.WriteLine(
                                            "Account index : {0}\n" +
                                            "Mnemonic : {1}\n" +
                                            "Address : {2}\n" +
                                            "Private key : {3}\n" +
                                            listView1.Items[i - 5].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 4].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 3].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k], explorerResult4[k], explorerResult5[k], explorerResult6[k]);
                                        }
                                    }
                                }
                            }
                            if (listView1.Items.Count == 7)
                            {
                                if (i == listView1.Items.Count - 1)
                                {
                                    for (int k = 0; k < 100; k++)
                                    {
                                        if (explorerResult1[k] > 0 | explorerResult2[k] > 0 | explorerResult3[k] > 0 | explorerResult4[k] > 0 | explorerResult5[k] > 0 | explorerResult6[k] > 0 | explorerResult7[k] > 0)
                                        {
                                            Console.WriteLine(
                                            "Account index : {0}\n" +
                                            "Mnemonic : {1}\n" +
                                            "Address : {2}\n" +
                                            "Private key : {3}\n" +
                                            listView1.Items[i - 6].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 5].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 4].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 3].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k], explorerResult4[k], explorerResult5[k], explorerResult6[k], explorerResult7[k]);
                                            balance = explorerResult1[k] + explorerResult2[k] + explorerResult3[k] + explorerResult4[k] + explorerResult5[k] + explorerResult6[k] + explorerResult7[k];
                                            balancefound = "Account index : " + (loop * 100) + k + 1 + "\n" +
                                            "Mnemonic : " + mnemo[k] + "\n" +
                                            "Address : " + address[k] + "\n" +
                                            "Private key : " + privatekey[k] + "\n";


                                        }

                                        else
                                        {

                                            Console.WriteLine(
                                            "Account index : {0}\n" +
                                            "Mnemonic : {1}\n" +
                                            "Address : {2}\n" +
                                            "Private key : {3}\n" +
                                            listView1.Items[i - 6].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 5].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 4].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 3].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k], explorerResult4[k], explorerResult5[k], explorerResult6[k], explorerResult7[k]);
                                        }
                                    }
                                }
                            }
                            if (listView1.Items.Count == 8)
                            {
                                if (i == listView1.Items.Count - 1)
                                {
                                    for (int k = 0; k < 100; k++)
                                    {
                                        if (explorerResult1[k] > 0 | explorerResult2[k] > 0 | explorerResult3[k] > 0 | explorerResult4[k] > 0 | explorerResult5[k] > 0 | explorerResult6[k] > 0 | explorerResult7[k] > 0 | explorerResult8[k] > 0)
                                        {
                                            Console.WriteLine(
                                            "Account index : {0}\n" +
                                            "Mnemonic : {1}\n" +
                                            "Address : {2}\n" +
                                            "Private key : {3}\n" +
                                             listView1.Items[i - 7].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 6].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 5].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 4].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 3].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k], explorerResult4[k], explorerResult5[k], explorerResult6[k], explorerResult7[k], explorerResult8[k]);
                                            balance = explorerResult1[k] + explorerResult2[k] + explorerResult3[k] + explorerResult4[k] + explorerResult5[k] + explorerResult6[k] + explorerResult7[k] + explorerResult8[k];
                                            balancefound = "Account index : " + (loop * 100) + k + 1 + "\n" +
                                            "Mnemonic : " + mnemo[k] + "\n" +
                                            "Address : " + address[k] + "\n" +
                                            "Private key : " + privatekey[k] + "\n";


                                        }

                                        else
                                        {
                                            Console.WriteLine(
                                            "Account index : {0}\n" +
                                            "Mnemonic : {1}\n" +
                                            "Address : {2}\n" +
                                            "Private key : {3}\n" +
                                            listView1.Items[i - 7].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 6].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 5].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 4].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 3].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k], explorerResult4[k], explorerResult5[k], explorerResult6[k], explorerResult7[k], explorerResult8[k]);
                                        }
                                    }
                                }
                            }
                            if (listView1.Items.Count == 9)
                            {
                                if (i == listView1.Items.Count - 1)
                                {
                                    for (int k = 0; k < 100; k++)
                                    {
                                        if (explorerResult1[k] > 0 | explorerResult2[k] > 0 | explorerResult3[k] > 0 | explorerResult4[k] > 0 | explorerResult5[k] > 0 | explorerResult6[k] > 0 | explorerResult7[k] > 0 | explorerResult8[k] > 0 | explorerResult9[k] > 0)
                                        {
                                            Console.WriteLine(
                                            "Account index : {0}\n" +
                                            "Mnemonic : {1}\n" +
                                            "Address : {2}\n" +
                                            "Private key : {3}\n" +
                                              listView1.Items[i - 8].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 7].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 6].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 5].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 4].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 3].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k], explorerResult4[k], explorerResult5[k], explorerResult6[k], explorerResult7[k], explorerResult8[k], explorerResult9[k]);
                                            balance = explorerResult1[k] + explorerResult2[k] + explorerResult3[k] + explorerResult4[k] + explorerResult5[k] + explorerResult6[k] + explorerResult7[k] + explorerResult8[k] + explorerResult9[k];
                                            balancefound = "Account index : " + (loop * 100) + k + 1 + "\n" +
                                            "Mnemonic : " + mnemo[k] + "\n" +
                                            "Address : " + address[k] + "\n" +
                                            "Private key : " + privatekey[k] + "\n";


                                        }

                                        else
                                        {
                                            Console.WriteLine(
                                            "Account index : {0}\n" +
                                            "Mnemonic : {1}\n" +
                                            "Address : {2}\n" +
                                            "Private key : {3}\n" +
                                            listView1.Items[i - 8].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 7].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 6].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 5].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 4].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 3].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k], explorerResult4[k], explorerResult5[k], explorerResult6[k], explorerResult7[k], explorerResult8[k], explorerResult9[k]);
                                        }
                                    }
                                }
                            }
                            if (listView1.Items.Count == 10)
                            {
                                if (i == listView1.Items.Count - 1)
                                {
                                    for (int k = 0; k < 100; k++)
                                    {
                                        if (explorerResult1[k] > 0 | explorerResult2[k] > 0 | explorerResult3[k] > 0 | explorerResult4[k] > 0 | explorerResult5[k] > 0 | explorerResult6[k] > 0 | explorerResult7[k] > 0 | explorerResult8[k] > 0 | explorerResult9[k] > 0 | explorerResult10[k] > 0)
                                        {
                                            Console.WriteLine(
                                            "Account index : {0}\n" +
                                            "Mnemonic : {1}\n" +
                                            "Address : {2}\n" +
                                            "Private key : {3}\n" +
                                            listView1.Items[i - 9].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 8].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 7].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 6].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 5].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 4].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 3].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k], explorerResult4[k], explorerResult5[k], explorerResult6[k], explorerResult7[k], explorerResult8[k], explorerResult9[k], explorerResult10[k]);
                                            balance = explorerResult1[k] + explorerResult2[k] + explorerResult3[k] + explorerResult4[k] + explorerResult5[k] + explorerResult6[k] + explorerResult7[k] + explorerResult8[k] + explorerResult9[k] + explorerResult10[k];
                                            balancefound = "Account index : " + (loop * 100) + k + 1 + "\n" +
                                                                        "Mnemonic : " + mnemo[k] + "\n" +
                                                                        "Address : " + address[k] + "\n" +
                                                                        "Private key : " + privatekey[k] + "\n";


                                        }

                                        else
                                        {

                                            Console.WriteLine(
                                            "Account index : {0}\n" +
                                            "Mnemonic : {1}\n" +
                                            "Address : {2}\n" +
                                            "Private key : {3}\n" +
                                            listView1.Items[i - 9].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 8].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 7].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 6].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 5].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 4].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 3].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 2].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i - 1].SubItems[0].Text + " Balance : 0\n" +
                                            listView1.Items[i].SubItems[0].Text + " Balance : 0\n", (loop * 100) + k + 1, mnemo[k], address[k], privatekey[k], explorerResult1[k], explorerResult2[k], explorerResult3[k], explorerResult4[k], explorerResult5[k], explorerResult6[k], explorerResult7[k], explorerResult8[k], explorerResult9[k], explorerResult10[k]);
                                        }
                                    }
                                }
                            }

                        }
                    }



                    mnemo = null;
                    address = null;
                    privatekey = null;
                    explorerResult1 = null;
                    explorerResult2 = null;
                    explorerResult3 = null;
                    explorerResult4 = null;
                    explorerResult5 = null;
                    explorerResult6 = null;
                    explorerResult7 = null;
                    explorerResult8 = null;
                    explorerResult9 = null;
                    explorerResult10 = null;

                    loop++;



                }
            }
            catch { }
        



    }


   
    }
}
