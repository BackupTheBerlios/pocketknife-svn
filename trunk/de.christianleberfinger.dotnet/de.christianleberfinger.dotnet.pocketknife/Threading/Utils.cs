using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace de.christianleberfinger.dotnet.Threading
{
    class ThreadUtils
    {
        public static void waitForThreadToDie(Thread thread)
        {
            if (thread == null)
                return;

            Debug.Assert(Thread.CurrentThread.ManagedThreadId != thread.ManagedThreadId);

            thread.Join();
        }
    }
}
