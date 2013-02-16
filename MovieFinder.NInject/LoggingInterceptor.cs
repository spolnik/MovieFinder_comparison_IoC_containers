using System;
using Ninject.Extensions.Interception;

namespace MovieFinder.NInject
{
    public class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Request.Method.Name;
            try
            {
                Log(methodName, "Logging On Start");
                invocation.Proceed();
                Log(methodName, "Logging On Success");
            }
            catch
            {
                Log(methodName, "Logging On Exception");
                throw;
            }
            finally
            {
                Log(methodName, "Logging On Exit");
            }
        }

        private static void Log(string methodName, string message)
        {
            Console.WriteLine("{0}: {1}", methodName, message);
        }
    }
}