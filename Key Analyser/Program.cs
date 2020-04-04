using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Key_Analyser
{
    class Program
    {
        public const int ENROLLMENT = 0;
        public const int VALIDATION = 1;
        public const int PASSIVE = 2;
        public static double t = 1.25;
        public static int STATE = ENROLLMENT;
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        public static string keyString;

        static string path = "";
        static String logpath = AppDomain.CurrentDomain.BaseDirectory;

        static List<String> logs = new List<String>();

        static Form1 mainWindow;
        public static double thresh_R = 1d;
        public static double thresh_A = 1d;

        static long time_last_key = 0;
        static FileStream fs;
        static StreamWriter s;
        static StreamReader sr;

        static List<Ngraph> ngraphs = new List<Ngraph>();
        static List<Ngraph> digraphs = new List<Ngraph>();
        static List<Ngraph> trigraphs = new List<Ngraph>();
        static List<Ngraph> fourgraphs = new List<Ngraph>();
        static List<Ngraph> fivegraphs = new List<Ngraph>();


        static R_Distance R2;
        static R_Distance R3;
        static R_Distance R4;
        static R_Distance R5;

        static A_Distance A2;
        static A_Distance A3;
        static A_Distance A4;
        static A_Distance A5;

        static List<Ngraph> V1_2 = new List<Ngraph>(); //for distance metrics
        static List<Ngraph> V2_2 = new List<Ngraph>(); //for distance metrics

        static List<Ngraph> V1_3 = new List<Ngraph>(); //for distance metrics
        static List<Ngraph> V2_3 = new List<Ngraph>(); //for distance metrics

        static List<Ngraph> V1_4 = new List<Ngraph>(); //for distance metrics
        static List<Ngraph> V2_4 = new List<Ngraph>(); //for distance metrics

        static List<Ngraph> V1_5 = new List<Ngraph>(); //for distance metrics
        static List<Ngraph> V2_5 = new List<Ngraph>(); //for distance metrics

        static List<Ngraph> old_ngraphs = new List<Ngraph>();

        static int cnt_2g = 0;
        static long[] millis_2g = new long[13];
        static string[] keys_2g = new string[13];
        public static Ngraph digraph;

        static int cnt_3g = 0;
        static long[] millis_3g = new long[13];
        static string[] keys_3g = new string[13];
        public static Ngraph trigraph;


        static int cnt_4g = 0;
        static long[] millis_4g = new long[13];
        static string[] keys_4g = new string[13];
        public static Ngraph fourgraph;


        static int cnt_5g = 0;
        static long[] millis_5g = new long[13];
        static string[] keys_5g = new string[13];
        public static Ngraph fivegraph;


        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [STAThread]
        static void Main(string[] args)
        {
            try
            {

                path = AppDomain.CurrentDomain.BaseDirectory;
                path += "/ngraphs.csv";
                //var directory = System.IO.Path.GetDirectoryName(path);


                if (File.Exists(path))
                {
                    readNgraphsFromFile(path);

                    /**
                    List<Ngraph> W1 = new List<Ngraph>();
                    W1.Add(new Ngraph("ion", 150, 1));
                    W1.Add(new Ngraph("tio", 200, 1));
                    W1.Add(new Ngraph("aut", 230, 1));
                    W1.Add(new Ngraph("uth", 250, 1));
                    W1.Add(new Ngraph("ati", 310, 1));

                    List<Ngraph> W2 = new List<Ngraph>();
                    W2.Add(new Ngraph("ion", 230, 1));
                    W2.Add(new Ngraph("tio", 220, 1));
                    W2.Add(new Ngraph("aut", 270, 1));
                    W2.Add(new Ngraph("uth", 190, 1));
                    W2.Add(new Ngraph("ati", 290, 1));

                    R_Distance R_test = new R_Distance(W1, W2);
                    A_Distance A_test = new A_Distance(W1, W2);
                    Debug.WriteLine("R_W1_W2: " + R_test.calc());
                    Debug.WriteLine("A_W1_W2: " + A_test.calc());
                    **/

                    split_old_ngraphs();
                }
                else
                {
                    createFile();
                }



                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                _hookID = SetHook(_proc);
                mainWindow = new Form1();
                Application.Run(mainWindow);

                UnhookWindowsHookEx(_hookID);

                if(STATE == ENROLLMENT) writeOut();
                if(STATE == VALIDATION) writeLog();
            }catch(Exception e)
            {
                MessageBox.Show("Error: " + e.ToString());
            }
        }

        public static void Log(bool authenticated, double A, double R)
        {

            String logout = "" + R + ";" + thresh_R + ";" + A + ";" + thresh_A;
            if (authenticated)
            {
                logout += ";true";
            }
            else
            {
                logout += ";false";
            }
            logs.Add(logout);
        }

        public static void createFile()
        {
            ngraphs.Clear();
            digraphs.Clear();
            trigraphs.Clear();
            fourgraphs.Clear();
            fivegraphs.Clear();
            fs = new FileStream(path, FileMode.Create);
            fs.Close();

        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public static void init_ngraphs()
        {
            cnt_2g = 0;
            millis_2g = new long[13];
            keys_2g = new string[13];


            cnt_3g = 0;
            millis_3g = new long[13];
            keys_3g = new string[13];

            cnt_4g = 0;
            millis_4g = new long[13];
            keys_4g = new string[13];

            cnt_5g = 0;
            millis_5g = new long[13];
            keys_5g = new string[13];
        }

        public static void split_old_ngraphs()
        {
            int len = 0;
            foreach (Ngraph n in old_ngraphs)
            {
                len = n.Graph.Length;
                switch (len)
                {
                    case 2:
                        digraphs.Add(n);
                        break;
                    case 3:
                        trigraphs.Add(n);
                        break;
                    case 4:
                        fourgraphs.Add(n);
                        break;
                    case 5:
                        fivegraphs.Add(n);
                        break;
                }
            }
        }

        /// <summary>
        /// Compares the current ArrayList ngraphs with the stored profile in
        /// split ArrayLists digraphs, trigraphs, fourgraphs, fivegraphs
        /// </summary>
        /// <returns>true if typing sample agrees with stored profile</returns>
        public static bool compare()
        {
            int len = 0;
            int cur_dur = 0;
            double A = -1d;
            double R = -1d;

            foreach (Ngraph n in ngraphs)
            {
                len = n.Graph.Length;
                cur_dur = n.Duration;

                //Debug.WriteLine("ngraph: " + n.Graph);
                //Debug.WriteLine("ngraph: " + n.Duration);

                switch (len)
                {
                    case 2:
                        foreach (Ngraph n2 in digraphs)
                        {

                            if (n2.Graph.Equals(n.Graph))
                            {
                                // Debug.WriteLine("n2graph: " + n2.Graph);
                                // Debug.WriteLine("n2graph: " + n2.Duration);
                                V1_2.Add(n2);
                                V2_2.Add(n);
                                break;
                            }
                        }
                        break;
                    case 3:
                        foreach (Ngraph n2 in trigraphs)
                        {
                            if (n2.Graph.Equals(n.Graph))
                            {
                                //  Debug.WriteLine("n3graph: " + n2.Graph);
                                // Debug.WriteLine("n3graph: " + n2.Duration);
                                V1_3.Add(n2);
                                V2_3.Add(n);
                                break;
                            }
                        }
                        break;
                    case 4:
                        foreach (Ngraph n2 in fourgraphs)
                        {
                            if (n2.Graph.Equals(n.Graph))
                            {
                                V1_4.Add(n2);
                                V2_4.Add(n);
                                break;
                            }
                        }
                        break;
                    case 5:
                        foreach (Ngraph n2 in fivegraphs)
                        {
                            if (n2.Graph.Equals(n.Graph))
                            {
                                V1_5.Add(n2);
                                V2_5.Add(n);
                                break;
                            }
                        }
                        break;
                }
            }

            R2 = new R_Distance(V1_2, V2_2);
            R3 = new R_Distance(V1_3, V2_3);
            R4 = new R_Distance(V1_4, V2_4);
            R5 = new R_Distance(V1_5, V2_5);

            A2 = new A_Distance(V1_2, V2_2, t);
            A3 = new A_Distance(V1_3, V2_3, t);
            A4 = new A_Distance(V1_4, V2_4, t);
            A5 = new A_Distance(V1_5, V2_5, t);

            double a2c = 0d;
            double a3c = 0d;
            double a4c = 0d;
            double a5c = 0d;

            double r2c = 0d;
            double r3c = 0d;
            double r4c = 0d;
            double r5c = 0d;

            if (V1_2.Count > 0)
            {
                a2c = A2.calc();
                a3c = A3.calc();
                a4c = A4.calc();
                a5c = A5.calc();
                A = a2c + a3c * V1_3.Count / V1_2.Count +
                    a4c * V1_4.Count / V1_2.Count +
                    a5c * V1_5.Count / V1_2.Count;

                r2c = R2.calc();
                r3c = R3.calc();
                r4c = R4.calc();
                r5c = R5.calc();
                R = r2c + r3c * V1_3.Count / V1_2.Count +
                    r4c * V1_4.Count / V1_2.Count +
                    r5c * V1_5.Count / V1_2.Count;
            }
       
            //Debug.WriteLine("len ngraph: " + ngraphs.Count);
            //Debug.WriteLine("R: " + R);
            //Debug.WriteLine("A: " + A);

            mainWindow.Set_A_R_values(A, R);

            //TODO Logging
            String logline = "" + thresh_A + ";" + A + ";" + a2c + ";" + V1_2.Count +
                           ";" + a3c + ";" + V1_3.Count + ";" + a4c + ";" + V1_4.Count +
                           ";" + a5c + ";" + V1_5.Count;
            logline += ";" + thresh_R + ";" + R + ";" + r2c + ";" + V1_2.Count +
                           ";" + r3c + ";" + V1_3.Count + ";" + r4c + ";" + V1_4.Count +
                           ";" + r5c + ";" + V1_5.Count;
            string auth_a = "false";
            string auth_r = "false";
            if (A>=0 && A < thresh_A) auth_a = "true";
            if (R >= 0 && R < thresh_R) auth_r = "true";
            logline += ";" + auth_a + ";" + auth_r;

            logs.Add(logline);

            V1_2.Clear();
            V2_2.Clear();
            V1_3.Clear();
            V2_3.Clear();
            V1_4.Clear();
            V2_4.Clear();
            V1_5.Clear();
            V2_5.Clear();
            return false;
        }

        public static void split_new_ngraphs()
        {
            int len = 0;
            bool already_exists = false;
            int cnt = 0;
            foreach (Ngraph n in ngraphs)
            {
                len = n.Graph.Length;
                already_exists = false;
                cnt = 0;
                switch (len)
                {
                    case 2:
                        foreach (Ngraph n_ae in digraphs) //for all already existing ngraphs
                        {

                            if (n_ae.Graph.Equals(n.Graph))
                            {
                                already_exists = true;
                                n_ae.Duration = (n_ae.Duration * n_ae.Amount + n.Duration) / ++n_ae.Amount; //recalculate the Duration (average)
                            }
                        }
                        if (!already_exists)
                        {
                            digraphs.Add(n);
                        }
                        break;
                    case 3:
                        foreach (Ngraph n_ae in trigraphs) //for all already existing ngraphs
                        {
                            if (n_ae.Graph.Equals(n.Graph))
                            {
                                already_exists = true;
                                n_ae.Duration = (n_ae.Duration * n_ae.Amount + n.Duration) / ++n_ae.Amount; //recalculate the Duration (average)
                            }
                        }
                        if (!already_exists)
                        {
                            trigraphs.Add(n);
                        }
                        break;
                    case 4:
                        foreach (Ngraph n_ae in fourgraphs) //for all already existing ngraphs
                        {
                            if (n_ae.Graph.Equals(n.Graph))
                            {
                                already_exists = true;
                                n_ae.Duration = (n_ae.Duration * n_ae.Amount + n.Duration) / ++n_ae.Amount; //recalculate the Duration (average)
                            }
                        }
                        if (!already_exists)
                        {
                            fourgraphs.Add(n);
                        }
                        break;
                    case 5:
                        foreach (Ngraph n_ae in fivegraphs) //for all already existing ngraphs
                        {
                            if (n_ae.Graph.Equals(n.Graph))
                            {
                                already_exists = true;
                                n_ae.Duration = (n_ae.Duration * n_ae.Amount + n.Duration) / ++n_ae.Amount; //recalculate the Duration (average)
                            }
                        }
                        if (!already_exists)
                        {
                            fivegraphs.Add(n);
                        }
                        break;
                }
            }
            ngraphs.Clear();
        }

        public static void readNgraphsFromFile(String path)
        {
            Ngraph n;
            fs = new FileStream(path, FileMode.Open);
            sr = new StreamReader(fs, Encoding.UTF8);
            string fs_line = "";
            int j = 0;
            int k = 0;
            while ((fs_line = sr.ReadLine()) != null)
            {
                j = 0;
                k = 0;
                for (int i = 0; i < fs_line.Length; i++)
                {
                    if (fs_line.ToCharArray()[i] == ';' && j == 0) j = i;
                }

                for (int i = j + 1; i < fs_line.Length; i++)
                {
                    if (fs_line.ToCharArray()[i] == ';') k = i;
                }

                if (fs_line.Length > 2)
                {
                    try
                    {
                        n = new Ngraph(fs_line.Substring(0, j),
                            Int32.Parse(fs_line.Substring(j + 1, k - j - 1)),
                            Int32.Parse(fs_line.Substring(k + 1, fs_line.Length - k - 1)));
                        old_ngraphs.Add(n);
                    }catch(Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }
                }
            }
            fs.Close();
            sr.Close();
        }

        public static void combine_new_ngraphs()
        {
            ngraphs = new List<Ngraph>();
            digraphs.Sort((x, y) => x.Duration.CompareTo(y.Duration));
            trigraphs.Sort((x, y) => x.Duration.CompareTo(y.Duration));
            fourgraphs.Sort((x, y) => x.Duration.CompareTo(y.Duration));
            fivegraphs.Sort((x, y) => x.Duration.CompareTo(y.Duration));
            foreach (Ngraph n in digraphs) ngraphs.Add(n);
            foreach (Ngraph n in trigraphs) ngraphs.Add(n);
            foreach (Ngraph n in fourgraphs) ngraphs.Add(n);
            foreach (Ngraph n in fivegraphs) ngraphs.Add(n);

        }

        public static void writeLog()
        {
            //write log
            if (logs.Count > 0)
            {
                DateTime dt = DateTime.Now;
                String ms = dt.ToString("yyyyMMddHHmmssffff");
                Debug.WriteLine(ms);
                logpath = AppDomain.CurrentDomain.BaseDirectory;
                logpath += "/log" + ms + ".csv";
                fs = new FileStream(logpath, FileMode.Create);
                s = new StreamWriter(fs);
                foreach (String logline in logs)
                {
                    s.WriteLine(logline);
                }

                s.Close();
                fs.Close();
            }
        }

        public static void writeOut()
        {
            split_new_ngraphs(); //split in different ngraph-ArrayLists to identify double entries and to average the duration
            combine_new_ngraphs(); //combine them into one ArrayList to make saving to file easier
            fs = new FileStream(path, FileMode.Open);
            s = new StreamWriter(fs, Encoding.UTF8);
            string pos = "";
            foreach (Ngraph n in ngraphs)
            {
                s.WriteLine(n.Graph + ";" + n.Duration + ";" + n.Amount);
                //Debug.WriteLine(n.Graph);
            }
            ngraphs.Clear();
            s.Close();
            fs.Close();


        }

        public static void save2Ngraph(Ngraph ngraph)
        {
            if (ngraph.Duration > 0 && ngraph.Graph.Length > 0) ngraphs.Add(ngraph);
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if(STATE==PASSIVE) return CallNextHookEx(_hookID, nCode, wParam, lParam);
            long milliseconds = 0;

            Keys key1;

            key1 = (Keys)Marshal.ReadInt32(lParam);
            keyString = key1.ToString();
            //Debug.Write(keyString);

            // account for german keyboard layout:
            if (keyString.Equals("OemOpenBrackets")) keyString = "ß";
            else if (keyString.Equals("Oem10")) keyString = "Ü";
            else if (keyString.Equals("Oemtilde")) keyString = "Ö";
            else if (keyString.Equals("Oem7")) keyString = "Ä";
            else if (keyString.Equals("Oem1")) keyString = "Ä";
            //else if (keyString.Length > 3 && keyString.Substring(0, 3).Equals("Num")) keyString = keyString.Substring(6, 1); //Numpad
            //else if (keyString.Length == 2 && keyString.Substring(0, 1).Equals("D")) keyString = keyString.Substring(1, 1); //Numbers

            //Debug.WriteLine(" "+keyString);

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {

                //accuont

                milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                //Console.WriteLine("Diff: " + (milliseconds - time_last_key)/1000);
                if ((milliseconds - time_last_key) / 1000 > 3 || //reset after 3 seconds of not typing
                                                                    //key.ToString().Equals("Backspace") ||        //or after pressing Space/Enter
                    keyString.Equals("Space") ||
                    keyString.Equals("Return"))
                //key.ToString().Equals("Esc"))
                {
                    //Console.WriteLine("Reset variables");
                    //                    Console.WriteLine("STATE: " + STATE);
                    if (STATE == ENROLLMENT) writeOut();
                    if (STATE == VALIDATION)
                    {
                        compare();
                        ngraphs.Clear();
                    }
                    init_ngraphs();
                }
                else if (keyString.Length == 1) //only take letters/numbers into account
                {
                    //2-graphs
                    keys_2g[cnt_2g] = keyString;
                    //Debug.WriteLine("keys2g: " + keys_2g[cnt_2g]);
                    millis_2g[cnt_2g] = milliseconds;
                    cnt_2g++;

                    if (cnt_2g == 3) //three consecutive keys without keyup
                    {
                        long dur = millis_2g[2] - millis_2g[0];
                        digraph = new Ngraph(keys_2g[0] + keys_2g[1], (int)dur, 1);
                        cnt_2g = 2;
                        for (int i = 0; i < cnt_2g; i++)
                        {
                            keys_2g[i] = keys_2g[i + 1];
                            millis_2g[i] = millis_2g[i + 1];
                        }

                        //Debug.Write("2_down: " + digraph.Graph + " ");
                        //Debug.WriteLine(digraph.Duration);
                        save2Ngraph(digraph);
                    }

                    //3-graphs
                    keys_3g[cnt_3g] = keyString;
                    millis_3g[cnt_3g] = milliseconds;
                    cnt_3g++;

                    if (cnt_3g == 4) //four consecutive keys without keyup
                    {
                        long dur = millis_3g[3] - millis_3g[0];
                        trigraph = new Ngraph(keys_3g[0] + keys_3g[1] + keys_3g[2], (int)dur, 1);
                        cnt_3g = 3;
                        for (int i = 0; i < cnt_3g; i++)
                        {
                            keys_3g[i] = keys_3g[i + 1];
                            millis_3g[i] = millis_3g[i + 1];
                        }

                        // Debug.Write("3_down: " + trigraph.Graph + " ");
                        // Debug.WriteLine(trigraph.Duration);
                        save2Ngraph(trigraph);
                    }

                    //4-graphs
                    keys_4g[cnt_4g] = keyString;
                    millis_4g[cnt_4g] = milliseconds;
                    cnt_4g++;

                    if (cnt_4g == 5) //five consecutive keys without keyup
                    {
                        long dur = millis_4g[4] - millis_4g[0];
                        fourgraph = new Ngraph(keys_4g[0] + keys_4g[1] + keys_4g[2] + keys_4g[3], (int)dur, 1);
                        cnt_4g = 4;
                        for (int i = 0; i < cnt_4g; i++)
                        {
                            keys_4g[i] = keys_4g[i + 1];
                            millis_4g[i] = millis_4g[i + 1];
                        }

                        //Debug.Write("4_down: " + fourgraph.Graph + " ");
                        //Debug.WriteLine(fourgraph.Duration);
                        save2Ngraph(fourgraph);
                    }

                    //5-graphs
                    keys_5g[cnt_5g] = keyString;
                    millis_5g[cnt_5g] = milliseconds;
                    cnt_5g++;

                    if (cnt_5g == 6) //six consecutive keys without keyup
                    {
                        long dur = millis_5g[5] - millis_5g[0];
                        fivegraph = new Ngraph(keys_5g[0] + keys_5g[1] + keys_5g[2] + keys_5g[3] + keys_5g[4], (int)dur, 1);
                        cnt_5g = 5;
                        for (int i = 0; i < cnt_5g; i++)
                        {
                            keys_5g[i] = keys_5g[i + 1];
                            millis_5g[i] = millis_5g[i + 1];
                        }

                        //Debug.Write("5_down: " + fivegraph.Graph + " ");
                        //Debug.WriteLine(fivegraph.Duration);
                        save2Ngraph(fivegraph);
                    }

                    //Debug.Write(milliseconds + " ");
                    //Debug.WriteLine(key.ToString());
                }
            }

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
            {
                milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                if (keyString.Length == 1) //only take letters/numbers into account
                                            //key.ToString().StartsWith("NumPad"))  //also check for number in numpad
                {
                    //2-graphs
                    //Debug.WriteLine("cnt 2g: " + cnt_2g);
                    if (cnt_2g >= 2 && keyString.Equals(keys_2g[1]))
                    {
                        long dur = milliseconds - millis_2g[0];
                        digraph = new Ngraph(keys_2g[0] + keys_2g[1], (int)dur, 1);

                        cnt_2g = 1;
                        keys_2g[0] = keys_2g[1];
                        millis_2g[0] = millis_2g[1];


                        //Debug.Write("2_up: " + digraph.Graph + " ");
                        //Debug.WriteLine(digraph.Duration);
                        save2Ngraph(digraph);
                    }


                    //3-graphs
                    if (cnt_3g >= 3 && keyString.Equals(keys_3g[2]))
                    {
                        long dur = milliseconds - millis_3g[0];
                        trigraph = new Ngraph(keys_3g[0] + keys_3g[1] + keys_3g[2], (int)dur, 1);

                        cnt_3g = 2;
                        for (int i = 0; i < cnt_3g; i++)
                        {
                            keys_3g[i] = keys_3g[i + 1];
                            millis_3g[i] = millis_3g[i + 1];
                        }

                        //Debug.Write("3_up: " + trigraph.Graph + " ");
                        //Debug.WriteLine(trigraph.Duration);
                        save2Ngraph(trigraph);
                    }

                    //4-graphs
                    if (cnt_4g >= 4 && keyString.Equals(keys_4g[3]))
                    {
                        long dur = milliseconds - millis_4g[0];
                        fourgraph = new Ngraph(keys_4g[0] + keys_4g[1] + keys_4g[2] + keys_4g[3], (int)dur, 1);

                        cnt_4g = 3;
                        for (int i = 0; i < cnt_4g; i++)
                        {
                            keys_4g[i] = keys_4g[i + 1];
                            millis_4g[i] = millis_4g[i + 1];
                        }

                        //Debug.Write("4_up: " + fourgraph.Graph + " ");
                        //Debug.WriteLine(fourgraph.Duration);
                        save2Ngraph(fourgraph);
                    }


                    //5-graphs
                    if (cnt_5g >= 5 && keyString.Equals(keys_5g[4]))
                    {
                        long dur = milliseconds - millis_5g[0];
                        fivegraph = new Ngraph(keys_5g[0] + keys_5g[1] + keys_5g[2] + keys_5g[3] + keys_5g[4], (int)dur, 1);

                        cnt_5g = 4;
                        for (int i = 0; i < cnt_5g; i++)
                        {
                            keys_5g[i] = keys_5g[i + 1];
                            millis_5g[i] = millis_5g[i + 1];
                        }

                        // Debug.Write("5_up: " + fivegraph.Graph + " ");
                        //Debug.WriteLine(fivegraph.Duration);
                        save2Ngraph(fivegraph);
                    }
                }
            }
            if (milliseconds > 0) time_last_key = milliseconds;

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }



    }
}
