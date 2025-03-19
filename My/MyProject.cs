// Decompiled with JetBrains decompiler
// Type: My.MyProject
// Assembly: Stub, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 292CE86C-33B7-4D1B-9DE2-B41CEB9702D8
// Assembly location: C:\Users\User\Desktop\Client.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

/**
 * @file MyProject.cs
 * @brief Provides centralized access to common objects such as Computer, Application, User, and WebServices.
 *
 * This file is auto-generated and implements thread-safe singleton providers 
 * for various frequently used objects.
 */

#nullable disable
namespace My
{

  // The MyProject module provides static properties for accessing common objects.
  [StandardModule]
  [HideModuleName]
  [GeneratedCode("MyTemplate", "11.0.0.0")]
  internal sealed class MyProject
  {
    // Thread-safe object provider for MyComputer, MyApplication, User, and MyWebServices.
    private static readonly MyProject.ThreadSafeObjectProvider<MyComputer> m_ComputerObjectProvider = new MyProject.ThreadSafeObjectProvider<MyComputer>();
    private static readonly MyProject.ThreadSafeObjectProvider<MyApplication> m_AppObjectProvider = new MyProject.ThreadSafeObjectProvider<MyApplication>();
    private static readonly MyProject.ThreadSafeObjectProvider<User> m_UserObjectProvider = new MyProject.ThreadSafeObjectProvider<User>();
    private static readonly MyProject.ThreadSafeObjectProvider<MyProject.MyWebServices> m_MyWebServicesObjectProvider = new MyProject.ThreadSafeObjectProvider<MyProject.MyWebServices>();

    /**
        * @brief Gets a thread-safe instance of the object.
        *
        * @return A singleton instance of the object.
        */
    [HelpKeyword("My.Computer")]
    internal static MyComputer Computer
    {
      [DebuggerHidden]
      get => MyProject.m_ComputerObjectProvider.GetInstance;
    }

    [HelpKeyword("My.Application")]
    internal static MyApplication Application
    {
      [DebuggerHidden]
      get => MyProject.m_AppObjectProvider.GetInstance;
    }

    [HelpKeyword("My.User")]
    internal static User User
    {
      [DebuggerHidden]
      get => MyProject.m_UserObjectProvider.GetInstance;
    }

    [HelpKeyword("My.WebServices")]
    internal static MyProject.MyWebServices WebServices
    {
      [DebuggerHidden]
      get => MyProject.m_MyWebServicesObjectProvider.GetInstance;
    }

    /**
 * @brief Provides access to web service client instances.
 *
 * This nested class is used for creating and disposing SOAP client instances.
 */
    [EditorBrowsable(EditorBrowsableState.Never)]
    [MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")]
    internal sealed class MyWebServices
    {

      /**
          * @brief Compares the current instance with another object.
          *
          * @param o The object to compare with.
          * @return True if equal; otherwise false.
          */
      [DebuggerHidden]
      [EditorBrowsable(EditorBrowsableState.Never)]
      public override bool Equals(object o) => base.Equals(RuntimeHelpers.GetObjectValue(o));

      /**
       * @brief Returns the hash code for this instance.
       *
       * @return The hash code.
       */
      [EditorBrowsable(EditorBrowsableState.Never)]
      [DebuggerHidden]
      public override int GetHashCode() => base.GetHashCode();
      /**
       * @brief Retrieves the Type of the MyWebServices instance.
       *
       * @return The Type object.
       */
      [EditorBrowsable(EditorBrowsableState.Never)]
      [DebuggerHidden]
      internal new Type GetType() => typeof(MyProject.MyWebServices);
      /**
       * @brief Converts the MyWebServices instance to its string representation.
       *
       * @return The string representation.
       */
      [EditorBrowsable(EditorBrowsableState.Never)]
      [DebuggerHidden]
      public override string ToString() => base.ToString();
      /**
       * @brief Creates an instance of type T if the current instance is null.
       *
       * @tparam T The type to create.
       * @param instance The instance to check.
       * @return A new instance of T if instance is null; otherwise, the existing instance.
       */
      [DebuggerHidden]
      private static T Create__Instance__<T>(T instance) where T : new()
      {
        return (object)instance == null ? new T() : instance;
      }
      /**
       * @brief Disposes of an instance by setting it to its default value.
       *
       * @tparam T The type of the instance.
       * @param instance Reference to the instance to dispose.
       */
      [DebuggerHidden]
      private void Dispose__Instance__<T>(ref T instance) => instance = default(T);
      /**
       * @brief Default constructor for MyWebServices.
       */
      [EditorBrowsable(EditorBrowsableState.Never)]
      [DebuggerHidden]
      public MyWebServices()
      {
      }
    }
    /**
     * @brief Provides a thread-safe object provider for creating singleton instances.
     *
     * @tparam T The type of the object to be provided.
     */
    [ComVisible(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal sealed class ThreadSafeObjectProvider<T> where T : new()
    {
      /**
             * @brief Gets the thread-local instance of type T.
             *
             * If the instance does not exist for the current thread, it creates a new one.
             *
             * @return The thread-safe instance of type T.
             */
      internal T GetInstance
      {
        [DebuggerHidden]
        get
        {
          if ((object)MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue == null)
            MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue = new T();
          return MyProject.ThreadSafeObjectProvider<T>.m_ThreadStaticValue;
        }
      }

      [EditorBrowsable(EditorBrowsableState.Never)]
      [DebuggerHidden]
      public ThreadSafeObjectProvider()
      {
      }
    }
  }
}
