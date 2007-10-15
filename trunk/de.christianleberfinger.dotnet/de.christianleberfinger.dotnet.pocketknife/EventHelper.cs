using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace de.christianleberfinger.dotnet.pocketknife
{
    //using MyGenericHandler = EventHandler<string, int>;

    public class EventHelper
    {
        private delegate void TestEventHandler(string a, int b);
        private event TestEventHandler TestEvent;

        /// <summary>
        /// just for comparison
        /// </summary>
        private void basicInvoke()
        {
            TestEventHandler temp = TestEvent;
            if (temp != null)
            {
                temp(null, 0);
            }
        }

        /// <summary>
        /// Unsafe means: the given arguments aren't checked for type safety (as they are objects)
        /// </summary>
        /// <param name="delegateToInvoke"></param>
        /// <param name="args"></param>
        public static void invokeUnsafe(Delegate delegateToInvoke, params object[] args)
        {
            if (delegateToInvoke == null)
                return;

            Delegate[] delegates = delegateToInvoke.GetInvocationList();
            foreach (Delegate del in delegates)
            {
                try
                {
                    del.DynamicInvoke(args);
                }
                catch(Exception ex) 
                {

                    Console.WriteLine("Error calling: " + del.Method.ToString() + " in " + del.Target.ToString());
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}
