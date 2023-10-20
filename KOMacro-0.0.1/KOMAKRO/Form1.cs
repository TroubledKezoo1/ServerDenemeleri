using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace KOMAKRO
{
    public partial class KOMacro : Form
    {
        public static TcpClient client;
        public static NetworkStream stream;
        public static string username;
        public static string lobyname;
        public static bool ishost;

        private static bool keyPressedByApplication = false;
        private static IntPtr hookId = IntPtr.Zero;
        private static LowLevelKeyboardProc keyboardProc = HookCallback;
        private bool isDragging = false;
        private int mouseX, mouseY;

        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x0100;
        const int WM_KEYUP = 0x101;
        const int KEYEVENTF_KEYDOWN = 0x0000;
        const int KEYEVENTF_KEYUP = 0x0002;

        //Klavye girisi tanimlamalari

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.None, SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        
        private static System.Net.IPAddress ip;
        public KOMacro()
        {
            InitializeComponent();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            btnJoinStop.Enabled = false;
            btnCreateStop.Enabled = false;
            Thread listenThread = new Thread(ListenForServerResponse);
            listenThread.Start();
            client.Connect("37.148.210.156", 8080);
            stream = client.GetStream();
        }
        static void ListenForServerResponse()
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            while (true)
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Sunucu cevabı: " + response);
                keyPressedByApplication = true;

                if (response == "q" || response == "Q")
                {
                    keybd_event((byte)Keys.Q, 0, KEYEVENTF_KEYDOWN, 0);
                    keybd_event((byte)Keys.Q, 0, KEYEVENTF_KEYUP, 0);
                } 
                keyPressedByApplication = false;
            }
        }

        static void LeaveLobby(string usernames , string lobbyNames)
        {
            string[] input = (lobyname+":"+ usernames).Split(':');
                string lobbyName = input[0];
                string username = input[1];
                string leaveLobbyCommand = $"LEAVE_LOBBY:{lobbyNames}:{username}";
                SendCommand(leaveLobbyCommand);
            
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                btnCreateStop.Enabled = true;
                txtJoinLobyName.Enabled = false;
                txtJoinUserName.Enabled = false;
                btnJoinStop.Enabled = false;
                btnJoinConnect.Enabled = false;
                btnCreateStart.Enabled = false;
                username = txtCreateUserName.Text;
                lobyname = txtCreateLobyName.Text;
                ishost = true;
                string createLobbyCommand = $"CREATE_LOBBY:{lobyname}:{5}:{username}";
                SendCommand(createLobbyCommand);
                HookKeyboard();
            }
            catch (Exception)
            {
                Console.WriteLine("Bir Hata Oluştu\nLütfen Tekrar Deneyiniz");
                MessageBox.Show("Bir Hata Oluştu\nLütfen Tekrar Deneyiniz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void btnStop_Click(object sender, EventArgs e)
        {
                txtJoinLobyName.Enabled = true;
                txtJoinUserName.Enabled = true;
                btnJoinStop.Enabled = true;
                btnJoinConnect.Enabled=true;
                btnCreateStart.Enabled = true;
            LeaveLobby(txtCreateUserName.Text,txtCreateLobyName.Text);
            
        }
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (!keyPressedByApplication && nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);  // Read the virtual key code

                // Convert virtual key code to a string representation
                string key = ((Keys)vkCode).ToString();

                // Print the key to the console
                SendCommand(key);
            }

            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        private void HookKeyboard()
        {
            hookId = SetHook(keyboardProc);
        }

        //Klavyeyi dinlemeyi durdur fonksiyonu
        //private void UnhookKeyboard()
        //{
        //    UnhookWindowsHookEx(hookId);
        //}

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                btnJoinStop.Enabled = true;
                txtCreateLobyName.Enabled = false;
                txtCreateUserName.Enabled = false;
                btnCreateStop.Enabled = false;
                btnCreateStart.Enabled = false;
                btnJoinConnect.Enabled = false;

                string lobbyName = txtJoinLobyName.Text;

                username = txtJoinUserName.Text;
                string joinLobbyCommand = $"JOIN_LOBBY:{lobbyName}:{username}";
                SendCommand(joinLobbyCommand);
            }
            catch (Exception)
            {
                Console.WriteLine("Bir Hata Oluştu\nLütfen Tekrar Deneyiniz");
                MessageBox.Show("Bir Hata Oluştu\nLütfen Tekrar Deneyiniz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        static void SendCommand(string command)
        {
            byte[] data = Encoding.UTF8.GetBytes(command);
            stream.Write(data, 0, data.Length);
        }

        private void txtServerHost_Click(object sender, EventArgs e)
        {
            txtCreateLobyName.Text = string.Empty;
        }

        private void txtServerPort_Click(object sender, EventArgs e)
        {
            txtCreateUserName.Text = string.Empty;
        }

        private void txtClientHost_Click(object sender, EventArgs e)
        {
            txtJoinLobyName.Text = string.Empty;
        }

        private void txtClientPort_Click(object sender, EventArgs e)
        {
            txtJoinUserName.Text = string.Empty;
        }

        private void btnClientStop_Click(object sender, EventArgs e)
        {
            txtCreateLobyName.Enabled = true;
            txtCreateUserName.Enabled = true;
            btnCreateStop.Enabled = true;
            btnCreateStart.Enabled = true;
            btnJoinConnect.Enabled = true;
            LeaveLobby(txtJoinUserName.Text,txtJoinLobyName.Text);

        }

        private void KOMacro_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void KOMacro_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        private void X_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void KOMacro_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Left += e.X - mouseX;
                this.Top += e.Y - mouseY;
            }
        }
    }
}
