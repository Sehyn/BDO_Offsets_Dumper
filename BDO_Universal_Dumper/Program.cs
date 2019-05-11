using System;
using System.Diagnostics;
using System.Linq;
using Memory;
using System.IO;
using System.Threading;
using System.Security.Principal;

namespace BDO_Universal_Dumper
{
    class Program
    {

        public static Mem m = new Mem();
        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        static void Main(string[] args)
        {
            try
            {
                if (IsAdministrator())
                {
                    #region Init Stuff
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Title = "- Black Desert Online Universal Offsets Dumper @GitHub Sehyn | @UnknownCheats 123lkj12lk312kjl3  -";
                    Process targetProcess = null;
                    var Path = DateTime.Now.ToString("yyyy-MM-dd") + " Offsets.txt";
                    targetProcess = Process.GetProcessesByName("BlackDesert64").FirstOrDefault();
                    while (targetProcess == null)
                    {
                        #region ASCII Waiting
                        string Wait = @"
$$\      $$\           $$\   $$\     $$\                             
$$ | $\  $$ |          \__|  $$ |    \__|                            
$$ |$$$\ $$ | $$$$$$\  $$\ $$$$$$\   $$\ $$$$$$$\   $$$$$$\          
$$ $$ $$\$$ | \____$$\ $$ |\_$$  _|  $$ |$$  __$$\ $$  __$$\         
$$$$  _$$$$ | $$$$$$$ |$$ |  $$ |    $$ |$$ |  $$ |$$ /  $$ |        
$$$  / \$$$ |$$  __$$ |$$ |  $$ |$$\ $$ |$$ |  $$ |$$ |  $$ |        
$$  /   \$$ |\$$$$$$$ |$$ |  \$$$$  |$$ |$$ |  $$ |\$$$$$$$ |$$\ $$\ 
\__/     \__| \_______|\__|   \____/ \__|\__|  \__| \____$$ |\__|\__|
                                                   $$\   $$ |        
                                                   \$$$$$$  |        
                                                    \______/         
";
                        #endregion

                        targetProcess = Process.GetProcessesByName("BlackDesert64").FirstOrDefault();
                        Console.WriteLine(Wait);
                        Console.WriteLine("----------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("Waiting for Black Desert Online..");
                        Console.WriteLine("----------------------------------------------------------------------------------------------------------");

                        System.Threading.Thread.Sleep(500);
                        Console.Clear();
                    }
                    #region ASCII Initializing
                    string Init = @"
$$$$$$\           $$\   $$\     $$\           $$\ $$\           $$\                     
\_$$  _|          \__|  $$ |    \__|          $$ |\__|          \__|                    
  $$ |  $$$$$$$\  $$\ $$$$$$\   $$\  $$$$$$\  $$ |$$\ $$$$$$$$\ $$\ $$$$$$$\   $$$$$$\  
  $$ |  $$  __$$\ $$ |\_$$  _|  $$ | \____$$\ $$ |$$ |\____$$  |$$ |$$  __$$\ $$  __$$\ 
  $$ |  $$ |  $$ |$$ |  $$ |    $$ | $$$$$$$ |$$ |$$ |  $$$$ _/ $$ |$$ |  $$ |$$ /  $$ |
  $$ |  $$ |  $$ |$$ |  $$ |$$\ $$ |$$  __$$ |$$ |$$ | $$  _/   $$ |$$ |  $$ |$$ |  $$ |
$$$$$$\ $$ |  $$ |$$ |  \$$$$  |$$ |\$$$$$$$ |$$ |$$ |$$$$$$$$\ $$ |$$ |  $$ |\$$$$$$$ |
\______|\__|  \__|\__|   \____/ \__| \_______|\__|\__|\________|\__|\__|  \__| \____$$ |
                                                                              $$\   $$ |
                                                                              \$$$$$$  |
                                                                               \______/ 
";
                    #endregion
                    System.Console.WriteLine(Init);
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------");
                    System.Console.WriteLine("Initializing...");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------");


                    Thread.Sleep(4000);
                    System.Console.Clear();
                    long lTime;
                    var sigScan = new SigScanSharp(targetProcess.Handle);
                    sigScan.SelectModule(targetProcess.MainModule);
                    #endregion

                    #region Patterns
                    var Playeroffset = sigScan.FindPattern("48 8B 05 ? ? ? ? 48 8B F9 48 85 C0 0F 84 ? ? ? ? 48", out lTime);
                    var Mountoffset = sigScan.FindPattern("48 8B 1D ? ? ? ? 48 85 DB 0F 84 ? ? ? ? 8B 8B", out lTime);
                    var Zoomoffset = sigScan.FindPattern("F3 0F 10 15 ? ? ? ? F3 0F 10 0D ? ? ? ? 0F 2F C2", out lTime);
                    var Numbersoffset = sigScan.FindPattern("48 FF 05 ? ? ? ? 0F 57 C9 C7 44 24 ? ? ? ? ?", out lTime);
                    var Nopinoffset = sigScan.FindPattern("89 B4 87 ? ? ? ? 44 89 A4 87 ? ? ? ?", out lTime);
                    var AntiCheatoffset = sigScan.FindPattern("C6 42 0C 00 80 7A 0C 01 75 69 45 8B 82 ? ? ? ?", out lTime);
                    var FallNop = sigScan.FindPattern("F3 0F 10 05 ? ? ? ? 48 83 B8 ? ? ? ? ? 74 08 F3 0F 59 05 ? ? ? ? 0F 2F C6 E9 ? ? ? ?", out lTime);
                    var FixTP = sigScan.FindPattern("0F 29 81 ? ? ? ? 48 85 C0 74 3C 48 8B 80 ? ? ? ? 48 8D 93 ? ? ? ? 48 8B 49 20", out lTime);
                    //var Tradepatch1 = sigScan.FindPattern("40 55 56 57 41 56 48 83 EC 48 48 8D B1 ? ? ? ? 48 8B E9 48 8B CE E8 ? ? ? ? 8B 85", out lTime);
                    //var WagonPatch = sigScan.FindPattern("74 10 83 C8 FF 66 3B 81 ? ? ? ?", out lTime);
                    var NoCD = sigScan.FindPattern("39 BA ? ? ? ? 76 52 4D 3B 43 08 73 12 39 79 0C B8 ? ? ? ? 41 BA ? ? ? ? 44 0F 44 D0", out lTime);
                    //var IDK = sigScan.FindPattern("66 0F 6E 84 81 ? ? 00 00", out lTime);
                    #endregion

                    #region InitializeProcess

                    if (m.OpenProcess("Blackdesert64"))
                    {
                        using (File.Create(Path))
                            Console.Clear();
                        #region ASCII Done

                        string Done = @"
$$$$$$$\                                $$\ 
$$  __$$\                               $$ |
$$ |  $$ | $$$$$$\  $$$$$$$\   $$$$$$\  $$ |
$$ |  $$ |$$  __$$\ $$  __$$\ $$  __$$\ $$ |
$$ |  $$ |$$ /  $$ |$$ |  $$ |$$$$$$$$ |\__|
$$ |  $$ |$$ |  $$ |$$ |  $$ |$$   ____|    
$$$$$$$  |\$$$$$$  |$$ |  $$ |\$$$$$$$\ $$\ 
\_______/  \______/ \__|  \__| \_______|\__|
                                            
                                            
                                            
";
                        #endregion

                        Console.WriteLine(Done);
                        Write.W("------------------", false);
                        Write.W("Black Desert Universal Dumper - Open Source by @123lkj12lk312kjl3 - Github: @Sehyn ", false);
                        Write.W("------------------", false);
                        Write.W("", true);



                        #endregion
                    }

                    #region ByteRead
                    byte[] localpbytes = m.readBytes(Playeroffset.ToString("X"), 7);
                    byte[] localmbytes = m.readBytes(Mountoffset.ToString("X"), 8);
                    byte[] localzbytes = m.readBytes(Zoomoffset.ToString("X"), 8);
                    byte[] localnbytes = m.readBytes(Numbersoffset.ToString("X"), 7);
                    byte[] localsbytes = m.readBytes(FallNop.ToString("X"), 8);
                    #endregion

                    #region LocalPlayer
                    if (localpbytes.Length > 2)
                    {

                        Console.WriteLine("----------------------------------------------------------------------------------------------------------");


                        var player = BitConverter.ToUInt32(localpbytes, 3);
                        var LocalPlayer = player + Playeroffset + 7;

                        Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "LocalPlayer = 0x" + LocalPlayer.ToString("X") + ";"));
                        Write.W("LocalPlayer = 0x" + LocalPlayer.ToString("X") + ";", false);
                    }
                    else
                    {
                        Console.WriteLine("LocalPlayer couldn't be found.");
                        Write.W("LocalPlayer coudln't be found.", false);
                    }
                    #endregion
                    #region MountAddress
                    if (localmbytes.Length > 2)
                    {
                        var mount = BitConverter.ToUInt32(localmbytes, 3);
                        var LocalMount = mount + Mountoffset + 7;
                        Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "LocalMount = 0x" + LocalMount.ToString("X") + ";"));
                        Write.W("LocalMount = 0x" + LocalMount.ToString("X") + ";", true);
                    }
                    else
                    {
                        Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "LocalMount couldn't be found."));
                        Write.W("LocalMount couldn't be found", true);
                    }
                    #endregion

                    #region ZoomAddress
                    if (localzbytes.Length > 2)
                    {
                        var Zoom = BitConverter.ToUInt32(localzbytes, 4);
                        var LocalZoom = Zoom + Zoomoffset + 8;
                        Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "MaxZoomAddr = 0x" + LocalZoom.ToString("X") + ";"));
                        Write.W("MaxZoomAddr = 0x" + LocalZoom.ToString("X") + ";", true);
                    }
                    else
                    {
                        Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Zoom couldn't be found."));
                        Write.W("Zoom couldn't be found.", true);
                    }
                    #endregion

                    #region PlayerNearbyAddress
                    if (localnbytes.Length > 2)
                    {
                        var Numbers = BitConverter.ToUInt32(localnbytes, 3);
                        var LocalNumbers = Numbers + Numbersoffset + 7;
                        Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Player Nearby Address = 0x" + LocalNumbers.ToString("X") + ";"));
                        Write.W("Player Nearby Address = 0x" + LocalNumbers.ToString("X") + ";", true);
                    }
                    else
                    {
                        Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Player Nearby Address couldn't be found."));
                        Write.W("Player Nearby Address couldn't be found.", true);
                    }
                    #endregion

                    #region AS/CS UNLOCK
                    if (Nopinoffset > 0)
                    {
                        Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Unlock AS/CS Limit Address = 0x" + Nopinoffset.ToString("X") + ";"));
                        Write.W("Unlock AS/CS Limit Address = 0x" + Nopinoffset.ToString("X") + ";", true);
                    }
                    else
                    {
                        Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "AS/CS Limit Address couldn't be found."));
                        Write.W("AS/CS Limit Address couldn't be found.", true);
                    }
                    #endregion
                    #region AntiCheat
                    if (AntiCheatoffset > 0)
                    {
                        Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "AntiCheat = 0x" + AntiCheatoffset.ToString("X") + ";"));
                        Write.W("AntiCheat = 0x" + AntiCheatoffset.ToString("X") + ";", true);
                    }
                    else
                    {
                        Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "AntiCheat nop address couldn't be found."));
                        Write.W("AntiCheat nop address couldn't be found.", true);
                    }
                    #endregion
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("@UnknownCheats: 123lkj12lk312kjl3");
                    Console.WriteLine("@Github: Sehyn");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------");

                    Write.W("", true);
                    Write.W("------------------", false);
                    Write.W("- Black Desert Universal Dumper -", false);
                    Write.W("------------------", false);


                    Console.ReadLine();
                }
                else
                {
                    #region ASCII Error2
                    string Error2 = @"
$$$$$$$$\                                         
$$  _____|                                        
$$ |       $$$$$$\   $$$$$$\   $$$$$$\   $$$$$$\  
$$$$$\    $$  __$$\ $$  __$$\ $$  __$$\ $$  __$$\ 
$$  __|   $$ |  \__|$$ |  \__|$$ /  $$ |$$ |  \__|
$$ |      $$ |      $$ |      $$ |  $$ |$$ |      
$$$$$$$$\ $$ |      $$ |      \$$$$$$  |$$ |      
\________|\__|      \__|       \______/ \__|      
                                                  
                                                  
                                                  
";
                    #endregion

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Error2);
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Make sure you open the Offset Dumper with administrator privileges.");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------");

                    Console.ReadLine();
                }
            }
            catch (Exception ah)
            {
                #region ASCII Error
                string Error = @"
$$$$$$$$\                                         
$$  _____|                                        
$$ |       $$$$$$\   $$$$$$\   $$$$$$\   $$$$$$\  
$$$$$\    $$  __$$\ $$  __$$\ $$  __$$\ $$  __$$\ 
$$  __|   $$ |  \__|$$ |  \__|$$ /  $$ |$$ |  \__|
$$ |      $$ |      $$ |      $$ |  $$ |$$ |      
$$$$$$$$\ $$ |      $$ |      \$$$$$$  |$$ |      
\________|\__|      \__|       \______/ \__|      
                                                  
                                                  
                                                  
";
                #endregion

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Error);
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Most of the time you're not able to access the game memory because AntiCheat had time to be initialized.");
                Console.WriteLine("Make sure you open the Offset Dumper then the game and with administrator privileges.");
                Console.WriteLine("Regardless here the original error log: " + ah.Message);
                Console.WriteLine("----------------------------------------------------------------------------------------------------------");

                Console.ReadLine();




            }
        }



    }
}

