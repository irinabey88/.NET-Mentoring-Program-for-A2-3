using Logger;
using Logger.Interfaces;
using Newtonsoft.Json;
using PostSharp.Aspects;
using System;

namespace CodeRewriting
{
    [Serializable]
    public class LogDataAspect : OnMethodBoundaryAspect
    {
        private readonly ILogger _logger = new LogService($"C:\\PostSharp");

        public override void OnEntry(MethodExecutionArgs args)
        {
            _logger.LogInfo($"PostSharp OnEntry: {args.Method.Name}");
            _logger.LogInfo($"Time: {DateTime.Now}");           
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            foreach (var arg in args.Arguments)
            {
                _logger.LogInfo($"Argument: { JsonConvert.SerializeObject(arg ?? "Not serializabl")}");

            }
            _logger.LogInfo($"PostSharp OnSuccess result: { JsonConvert.SerializeObject(args.ReturnValue ?? "Not serializabl")}");
        }
    }
}
