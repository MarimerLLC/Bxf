﻿#if !XAMARIN
//-----------------------------------------------------------------------
// <copyright file="TriggerAction.cs" company="Marimer LLC">
//     Copyright (c) Marimer LLC. All rights reserved.
//     Website: http://www.lhotka.net/cslanet/
// </copyright>
// <summary>Control used to invoke a method on the DataContext</summary>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel;
#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Bxf.Reflection;
#else
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
#endif

namespace Bxf.Xaml
{
  /// <summary>
  /// Control used to invoke a method on the DataContext
  /// based on an event being raised by a UI control.
  /// </summary>
  public class TriggerAction : FrameworkElement
  {
    /// <summary>
    /// Creates an instance of the object.
    /// </summary>
    public TriggerAction()
    {
      Visibility = Visibility.Collapsed;
      Height = 20;
      Width = 20;
    }

    /// <summary>
    /// Invokes target method.
    /// </summary>
    private void CallMethod(object sender, EventArgs e)
    {
      object target = this.DataContext;
      var cvs = target as CollectionViewSource;
      if (cvs != null && cvs.View != null)
      {
        target = cvs.View.CurrentItem;
      }
      else
      {
        var icv = target as ICollectionView;
        if (icv != null)
          target = icv.CurrentItem;
      }
      if (target == null) return; // can be null at design time - so just exit

      var targetMethod = target.GetType().GetMethod(MethodName);
      if (targetMethod == null)
        throw new MissingMethodException(MethodName);

      var pCount = targetMethod.GetParameters().Length;
      try
      {
        if (pCount == 0)
        {
          targetMethod.Invoke(target, null);
        }
        else if (pCount == 2)
        {
          object parameterValue = null;
          if (RebindParameterDynamically)
            parameterValue = GetMethodParameter();
          else
            parameterValue = MethodParameter;

          targetMethod.Invoke(target, new object[] { this, new ExecuteEventArgs
            {
              MethodParameter = parameterValue,
              TriggerParameter = e,
              TriggerSource = TargetControl
            }});
        }
        else
          throw new NotSupportedException("Target method must have 0 or 2 parameters");
      }
      catch (System.Reflection.TargetInvocationException ex)
      {
        if (ex.InnerException != null)
          throw ex.InnerException;
        else
          throw;
      }
    }

    private void HookEvent(FrameworkElement oldTarget, string oldEvent, FrameworkElement newTarget, string newEvent)
    {
      if (!ReferenceEquals(oldTarget, newTarget) || oldEvent != newEvent)
      {
        if (oldTarget != null && !string.IsNullOrEmpty(oldEvent))
        {
          var eventRef = oldTarget.GetType().GetEvent(oldEvent);
          if (eventRef != null)
          {
            var invoke = eventRef.EventHandlerType.GetMethod("Invoke");
            var p = invoke.GetParameters();
            if (p.Length == 2)
            {
              var p1Type = p[1].ParameterType;
              if (typeof(EventArgs).IsAssignableFrom(p1Type))
              {
                var handler = this.GetType().GetMethod("CallMethod", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
#if WINDOWS_UWP
                var del = handler.CreateDelegate(eventRef.EventHandlerType, this);
#else
                var del = Delegate.CreateDelegate(eventRef.EventHandlerType, this, handler);
#endif
                eventRef.RemoveEventHandler(oldTarget, del);
              }
              else
              {
                throw new NotSupportedException("Invalid trigger event");
              }
            }
            else
              throw new NotSupportedException("Invalid trigger event");
          }
        }

        if (newTarget != null && !string.IsNullOrEmpty(newEvent))
        {
          var eventRef = newTarget.GetType().GetEvent(newEvent);
          if (eventRef != null)
          {
            var invoke = eventRef.EventHandlerType.GetMethod("Invoke");
            var p = invoke.GetParameters();
            if (p.Length == 2)
            {
              var p1Type = p[1].ParameterType;
              if (typeof(EventArgs).IsAssignableFrom(p1Type))
              {
                var handler = this.GetType().GetMethod("CallMethod", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
#if WINDOWS_UWP
                var del = handler.CreateDelegate(eventRef.EventHandlerType, this);
#else
                var del = Delegate.CreateDelegate(eventRef.EventHandlerType, this, handler);
#endif
                eventRef.AddEventHandler(newTarget, del);
              }
              else
              {
                throw new NotSupportedException("Invalid trigger event");
              }
            }
            else
              throw new NotSupportedException("Invalid trigger event");
          }
        }
      }
    }

#region Properties

    /// <summary>
    /// Gets or sets the target UI control.
    /// </summary>
    public static readonly DependencyProperty TargetControlProperty =
      DependencyProperty.Register("TargetControl", typeof(FrameworkElement),
      typeof(TriggerAction),
#if WINDOWS_UWP
      new PropertyMetadata(null, (o, e) =>
#else
      new PropertyMetadata((o, e) =>
#endif
      {
        var ta = (TriggerAction)o;
          ta.HookEvent(
            (FrameworkElement)e.OldValue, ta.TriggerEvent, (FrameworkElement)e.NewValue, ta.TriggerEvent);
        }));
    /// <summary>
    /// Gets or sets the target UI control.
    /// </summary>
    [Category("Common")]
    public FrameworkElement TargetControl
    {
      get { return (FrameworkElement)GetValue(TargetControlProperty); }
      set { SetValue(TargetControlProperty, value); }
    }

    /// <summary>
    /// Gets or sets the name of the event
    /// that will trigger the action.
    /// </summary>
    public static readonly DependencyProperty TriggerEventProperty =
      DependencyProperty.Register("TriggerEvent", typeof(string),
      typeof(TriggerAction), new PropertyMetadata("Click", (o, e) => 
      {
        var ta = (TriggerAction)o;
        ta.HookEvent(ta.TargetControl, (string)e.OldValue, ta.TargetControl, (string)e.NewValue);
      }));
    /// <summary>
    /// Gets or sets the name of the event
    /// that will trigger the action.
    /// </summary>
    [Category("Common")]
    public string TriggerEvent
    {
      get { return (string)GetValue(TriggerEventProperty); }
      set { SetValue(TriggerEventProperty, value); }
    }

    /// <summary>
    /// Gets or sets the name of the method
    /// to be invoked.
    /// </summary>
    public static readonly DependencyProperty MethodNameProperty =
      DependencyProperty.Register("MethodName", typeof(string),
      typeof(TriggerAction), new PropertyMetadata(null));
    /// <summary>
    /// Gets or sets the name of the method
    /// to be invoked.
    /// </summary>
    [Category("Common")]
    public string MethodName
    {
      get { return (string)GetValue(MethodNameProperty); }
      set { SetValue(MethodNameProperty, value); }
    }

    /// <summary>
    /// Gets or sets the value of a parameter to
    /// be passed to the invoked method.
    /// </summary>
    public static readonly DependencyProperty MethodParameterProperty =
      DependencyProperty.Register("MethodParameter", typeof(object),
      typeof(TriggerAction), new PropertyMetadata(null));
    /// <summary>
    /// Gets or sets the value of a parameter to
    /// be passed to the invoked method.
    /// </summary>
    [Category("Common")]
    public object MethodParameter
    {
      get { return GetValue(MethodParameterProperty); }
      set { SetValue(MethodParameterProperty, value); }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the
    /// MethodParameter value should be dynamically rebound
    /// before invoking the target method.
    /// </summary>
    public static readonly DependencyProperty RebindParameterDynamicallyProperty =
      DependencyProperty.Register("RebindParameterDynamically", typeof(bool),
      typeof(TriggerAction), new PropertyMetadata(null));
    /// <summary>
    /// Gets or sets a value indicating whether the
    /// MethodParameter value should be dynamically rebound
    /// before invoking the target method.
    /// </summary>
    [Category("Common")]
    public bool RebindParameterDynamically
    {
      get { return (bool)GetValue(RebindParameterDynamicallyProperty); }
      set { SetValue(RebindParameterDynamicallyProperty, value); }
    }

#endregion

#region GetMethodParameter

    private object GetMethodParameter()
    {
      var be = this.GetBindingExpression(MethodParameterProperty);
      if (be != null && be.ParentBinding != null)
      {
        var newBinding = CopyBinding(be.ParentBinding);
        SetBinding(MethodParameterProperty, newBinding);
      }
      return MethodParameter;
    }

    private static Binding CopyBinding(Binding oldBinding)
    {
      var result = new Binding();
#if !WINDOWS_UWP
      result.BindsDirectlyToSource = oldBinding.BindsDirectlyToSource;
      result.ConverterCulture = oldBinding.ConverterCulture;
      result.NotifyOnValidationError = oldBinding.NotifyOnValidationError;
      result.ValidatesOnExceptions = oldBinding.ValidatesOnExceptions;
#endif
      result.Converter = oldBinding.Converter;
      result.ConverterParameter = oldBinding.ConverterParameter;
      result.Mode = oldBinding.Mode;
      result.Path = oldBinding.Path;
      if (oldBinding.ElementName != null)
        result.ElementName = oldBinding.ElementName;
      else if (oldBinding.RelativeSource != null)
        result.RelativeSource = oldBinding.RelativeSource;
      else
        result.Source = oldBinding.Source;
      result.UpdateSourceTrigger = oldBinding.UpdateSourceTrigger;
      return result;
    }

#endregion
  }
}
#endif