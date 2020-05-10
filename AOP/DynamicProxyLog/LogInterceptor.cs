using Castle.DynamicProxy;
using Logger;
using Logger.Interfaces;
using Newtonsoft.Json;
using System;

namespace DynamicProxyLog
{
    public class LogInterceptor : IInterceptor
    {
        private readonly ILogger _logger = new LogService($"C:\\DynamicProxy");

        public void Intercept(IInvocation invocation)
        {
            _logger.LogInfo(Environment.NewLine);
            _logger.LogInfo("DynamicProxy");
            _logger.LogInfo($"Method: {invocation.Method.Name}");
            _logger.LogInfo($"Time: {DateTime.Now}");

            foreach (var arg in invocation.Arguments)
            {
                _logger.LogInfo($"Argument: { JsonConvert.SerializeObject(arg ?? "Not serializabl")}");
            }

            invocation.Proceed();
            _logger.LogInfo($"Method result: { JsonConvert.SerializeObject(invocation.ReturnValue ?? "Not serializabl")}");
        }
    }
}
