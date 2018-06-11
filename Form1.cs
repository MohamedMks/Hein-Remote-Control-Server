using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Hein_Remote_Control.Properties;

namespace Hein_Remot_Control
{
    public partial class Form1 : Form
    {

    	static int[] control_hein = new int[] { 0x15A0030,0x15C006C,0x15A0293,0x15702B9};
    	static int[] controls_btn = new int[] { 0x2000001};
    	TcpListener listener;
    	IPAddress ipAddress;
    	TcpClient client;
    	//string[] week = new string[] {"Sunday","Monday","Tuesday"};
    	static int[] maincodes = new int[] {0,0x20F0055,0x2080098,0x20200D9,0x1FB011C,0x1F8015D,0x1F601A0,0x1F501E2,0x1F90224,0x1FB0265,0x20302A9,
    		                                0x20702EB,0x20E032C,0x2440053,0x23B0098,0x23200DA,0x22F011A,0x22A015E,0x22F01A0,0x22F01E5,0x22E0223,0x2310266,
    		                                0x23802AA,0x23A02EA,0x2440329,0x263013F,0x25E0195,0x25F01ED,0x2630241}; //28
    	
    	static int[] subcodes  = new int[] {0,0x1D50054,0x1D00098,0x1C700D6,0x1C1011A,0x1C0015C,0x1BC019D,0x1BD01DC,0x1C10221,
    		                                      0x1C50264,0x1C902AB,0x1D202E9,0x1D7032E,0x20C002D,0x2020073,0x1FB00B6,0x1FA00F7,0x1F2013B,
    		                                      0x1F3017B,0x1F501BE,0x1F40204,0x1FD0245,0x2000288,0x20502C6,0x2090308,0x22B013C,0x2230183,0x22501C4,0x22B0244}; //28

        public Form1()
        {
            InitializeComponent();
 
        }
        
        private System.Int32 iHandle , Sub1 , Sub2 , Screen_Host;
         private void Form1Load(object sender, EventArgs e)
        {
         	
        iHandle = Win32.FindWindow("RBWindow", "Hein 4.5.2");
        if (iHandle != null)
        {
           Sub1 =  Win32.FindWindowEx(iHandle, 0, "RB_Pane", "");
           if (Sub1 != null)
           {
               Sub2 =  Win32.FindWindowEx(Sub1, 0, "CefBrowserWindow", "");
               if (Sub2 != null)
               {
                  Screen_Host =  Win32.FindWindowEx(Sub2, 0, "WebViewHost", "");
               //   txt_log.Text = "handler :" + iHandle +"*** sub 1 :" +Sub1 +"*** sub 2  :"+Sub2+"*** sub 3 :" +Screen_Host;
               }
           }
         }

          if(IsAdministrator() == true){
   // iHandle = Win32.FindWindow("RBWindow", "Hein 4.5.2");
          if(iHandle == 0)
          { 
          	txt_log.Text = "Hein 4.5.2  " +" غير مشغل ";
          	btn_start.Enabled = false;
          }else{
          	
          	btn_start.Enabled = true;
          	txt_IP.Text = GetLocalIPAddresstree();
          }
					
		  }else if(IsAdministrator() == false){
					
			 txt_log.Text = "من فضلك شغل البرنامج كمدير Administrator";
		  }
        

          
        }

         delegate void SetTextCallback(string text);
         
         public void SetText(string text)
        {

          if (this.txt_log.InvokeRequired)
            { 
            SetTextCallback d = new SetTextCallback(SetText);
            Invoke(d, new object[] { text });
           }
            else
           {
            txt_log.Text = text;
           }
          // u know   SetText("");

        }
         
         public void Setlabel(string text)
        {
          if (this.lbl_IP.InvokeRequired)
            { 
            SetTextCallback d = new SetTextCallback(SetText);
            Invoke(d, new object[] { text });
           }
            else
           {
            lbl_IP.Text = text;
           }
          // u know   SetText("");

        }
         
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
               
            }
            throw new Exception("خطأ في نظام IPv4 تأكد انك متصل ب WI-FI");
        }
        


        
        public string GetLocalIPAddresstree()
        {
          string result;
          result = System.Net.Dns.GetHostByName(Environment.MachineName).AddressList[3].ToString();
          return result;
        }
        
        static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole (WindowsBuiltInRole.Administrator);
        }
        
        private void Btn_startClick(object sender, EventArgs e)  // about
        {
        	
        	if(IsAdministrator() == true){
        		    Thread ThreadingServer = new Thread(StartServer);
                    ThreadingServer.Start();
                    lbl_IP.Text =  txt_IP.Text;
					btn_start.Enabled = false;
					btn_stop.Enabled = true;
					
				}
				else if(IsAdministrator() == false){
					
					txt_log.Text = "من فضلك شغل البرنامج كمدير Administrator";
				}

             
        }
        
        const int PORT_NO = 5000;
        //const string SERVER_IP = txt_IP.Text;

        private void StartServer()
        {
        	try
              {
        		
                ipAddress = IPAddress.Parse(txt_IP.Text);
                SetText("Starting TCP listener...");
                listener = new TcpListener(ipAddress, 5000);
                SetText("Hein  مشغل المرجو الاتصال باستخدم العنوان " +txt_IP.Text+" في تطبيق الاندرويد" );
               
                listener.Start();
                while (true)
                {
               // Socket client = listener.AcceptSocket();
                client = listener.AcceptTcpClient();
                NetworkStream nwStream = client.GetStream();
                SetText("تم الاتصال بنجاح");

                var childSocketThread = new Thread(() =>
                {

                byte[] buffer = new byte[client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                
                        if (dataReceived.Contains("BEINSPORTCONNECTED"))
                        {
                        	System.Media.SystemSounds.Beep.Play();
                        }else if (dataReceived.Contains("BEINSPORTPLAYPAUSE"))
                        {
                        	Win32.SendMessage(Screen_Host, Win32.WM_LBUTTONDOWN, 0x00000001, control_hein[0]);
                        	
                        }else if (dataReceived.Contains("BEINSPORTSTOP"))
                        {
                        	Win32.SendMessage(Screen_Host, Win32.WM_LBUTTONDOWN, 0x00000001, control_hein[1]);

                        }else if (dataReceived.Contains("BEINSPORTMUTE"))
                        {
                        	Win32.SendMessage(Screen_Host, Win32.WM_LBUTTONDOWN, 0x00000001, control_hein[2]);
                        }else if (dataReceived.Contains("BEINSPORTFULL"))
                        {
                        	Win32.SendMessage(Screen_Host, Win32.WM_LBUTTONDOWN, 0x00000001, control_hein[3]);
                        	
                        }// ----
                        else if (dataReceived.Contains("BEINSPORT1HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[1]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[1]);
                        	SetText("BEIN SPORT 1HD");
                        }
                        else if (dataReceived.Contains("BEINSPORT2HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[2]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[2]);
                        	SetText("BEIN SPORT 2HD");
                        }
                        else if (dataReceived.Contains("BEINSPORT3HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[3]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[3]);
                        	SetText("BEIN SPORT 3HD");
                        }
                        else if (dataReceived.Contains("BEINSPORT4HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[4]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[4]);
                        	SetText("BEIN SPORT 4HD");
                        }else if (dataReceived.Contains("BEINSPORT5HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[5]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[5]);
                        	SetText("BEIN SPORT 5HD");
                        }else if (dataReceived.Contains("BEINSPORT6HD"))
                        {
                            Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[6]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[6]);
                        	SetText("BEIN SPORT 6HD");
                            
                        }else if (dataReceived.Contains("BEINSPORT7HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[7]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[7]);
                        	SetText("BEIN SPORT 7HD");
                        	
                        }else if (dataReceived.Contains("BEINSPORT8HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[8]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[8]);
                        	SetText("BEIN SPORT 8HD");
                        }else if (dataReceived.Contains("BEINSPORT9HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[9]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[9]);
                        	SetText("BEIN SPORT 9HD");
                        }else if (dataReceived.Contains("BEINSPORT10HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[10]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[10]);
                        	SetText("BEIN SPORT 10HD");
                        }else if (dataReceived.Contains("BEINSPORT11HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[11]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[11]);
                        	SetText("BEIN SPORT 11HD");
                        }else if (dataReceived.Contains("BEINSPORT12HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[12]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[12]);
                        	SetText("BEIN SPORT 12HD");
                        }else if (dataReceived.Contains("BEINSPORT13HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[13]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[13]);
                        	SetText("BEIN SPORT 13HD");
                        }else if (dataReceived.Contains("BEINSPORT14HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[14]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[14]);
                        	SetText("BEIN SPORT 14HD");
                        }else if (dataReceived.Contains("BEINSPORT15HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[15]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[15]);
                        	SetText("BEIN SPORT 15HD");
                        }else if (dataReceived.Contains("BEINSPORT16HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[16]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[16]);
                        	SetText("BEIN SPORT 16HD");
                        }else if (dataReceived.Contains("BEINSPORT17HD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[17]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[17]);
                        	SetText("BEIN SPORT 17HD");
                        }else if (dataReceived.Contains("BEINSPORTNEWSHD"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[18]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[18]);
                        	SetText("BEIN SPORT NEWS");
                        }else if (dataReceived.Contains("BEINSPORT"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[19]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[19]);
                        	SetText("BEIN SPORT");
                        }else if (dataReceived.Contains("BEINMOVIES1"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[20]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[20]);
                        	SetText("BEIN SPORT MOVIES 1HD");
                        }else if (dataReceived.Contains("BEINMOVIES2"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[21]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[21]);
                        	SetText("BEIN SPORT MOVIES 2HD");
                        }else if (dataReceived.Contains("BEINMOVIES3"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[22]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[22]);
                        	SetText("BEIN SPORT MOVIES 3HD");
                        }else if (dataReceived.Contains("BEINMOVIES4"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[23]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[23]);
                        	SetText("BEIN SPORT MOVIES 4HD");
                        }else if (dataReceived.Contains("BEJUNIOR"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[24]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[24]);
                        	SetText("BEIN SPORT JENIOR");
                        }else if (dataReceived.Contains("MAX1"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[25]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[25]);
                        	SetText("BEIN SPORT MAX 1");
                        }else if (dataReceived.Contains("MAX2"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[26]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[26]);
                        	SetText("BEIN SPORT MAX 2");
                        }else if (dataReceived.Contains("MAX3"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[27]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[27]);
                        	SetText("BEIN SPORT MAX 3");
                        }else if (dataReceived.Contains("MAX4"))
                        {
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, maincodes[28]);
                        	Win32.SendMessage(iHandle, Win32.WM_LBUTTONDOWN, 0x00000001, subcodes[28]);
                        	SetText("BEIN SPORT MAX 4");
                        }
                        
                        
               client.Close();
               });
               childSocketThread.Start();
              }

               listener.Stop();
          }
          catch (Exception e)
          {
              SetText(" حدث خطأ اعد تشغيل البرنامج");
          }

        }
        
        
        void Btn_exitClick(object sender, EventArgs e)
        {
        	 Environment.Exit(0);
        }
        
       
        
        void Btn_stopClick(object sender, EventArgs e)
        {

        	if(IsAdministrator() == true){

                    
                    SetText("تم ايقاف الريموت ");

                btn_start.Enabled = true;
                btn_stop.Enabled = false;
                lbl_IP.Text = "";
					
				}
				else if(IsAdministrator() == false){
					
					txt_log.Text = "من فضلك شغل البرنامج كمدير Administrator";
				}
        	
        	
                
        	
        }


        
        void Txt_IPTextChanged(object sender, EventArgs e)
        {
        	btn_start.Enabled = true;
        }

        
        void Button1Click(object sender, EventArgs e)
        {
        	Win32.SendMessage(Screen_Host, Win32.WM_LBUTTONDOWN, 0x00000001, control_hein[0]);
        	SetText("Play/Pause");
        }
        
        void Button2Click(object sender, EventArgs e)
        {
        	Win32.SendMessage(Screen_Host, Win32.WM_LBUTTONDOWN, 0x00000001, control_hein[1]);
        	SetText("Stop");
        }
        
        void Button3Click(object sender, EventArgs e)
        {
        	Win32.SendMessage(Screen_Host, Win32.WM_LBUTTONDOWN, 0x00000001, control_hein[2]);
        	SetText("MUTE");
        }
        
        void Button4Click(object sender, EventArgs e)
        {
        	Win32.SendMessage(Screen_Host, Win32.WM_LBUTTONDOWN, 0x00000001, control_hein[3]);
        	SetText("FullScreen");
        }
    }
}