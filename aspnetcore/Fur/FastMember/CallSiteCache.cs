using System.Collections;
using System.Runtime.CompilerServices;
using System;
using Microsoft.CSharp.RuntimeBinder;

namespace Fur.FastMember
{
    internal static class CallSiteCache
    {
        private static readonly Hashtable getters = new Hashtable(), setters = new Hashtable();

        internal static object GetValue(string name, object target)
        {
            CallSite<Func<CallSite, object, object>> callSite = (CallSite<Func<CallSite, object, object>>)getters[name];
            if (callSite == null)
            {
                CallSite<Func<CallSite, object, object>> newSite = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, name, typeof(CallSiteCache), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }));
                lock (getters)
                {
                    callSite = (CallSite<Func<CallSite, object, object>>)getters[name];
                    if (callSite == null)
                    {
                        getters[name] = callSite = newSite;
                    }
                }
            }
            return callSite.Target(callSite, target);
        }
        internal static void SetValue(string name, object target, object value)
        {
            CallSite<Func<CallSite, object, object, object>> callSite = (CallSite<Func<CallSite, object, object, object>>)setters[name];
            if (callSite == null)
            {
                CallSite<Func<CallSite, object, object, object>> newSite = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, name, typeof(CallSiteCache), new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null) }));
                lock (setters)
                {
                    callSite = (CallSite<Func<CallSite, object, object, object>>)setters[name];
                    if (callSite == null)
                    {
                        setters[name] = callSite = newSite;
                    }
                }
            }
            callSite.Target(callSite, target, value);
        }
        
    }
}