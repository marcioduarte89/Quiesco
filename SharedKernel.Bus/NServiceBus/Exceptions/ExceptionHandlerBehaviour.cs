using NServiceBus.Logging;
using NServiceBus.Pipeline;
using System;
using System.Threading.Tasks;

namespace SharedKernel.Bus.NServiceBus.Exceptions
{
    public class ExceptionHandlerBehaviour : Behavior<ITransportReceiveContext>
    {
        static ILog Log = LogManager.GetLogger(typeof(ExceptionHandlerBehaviour));

        public override Task Invoke(ITransportReceiveContext context, Func<Task> next)
        {
            try
            {
                return next();
            }
            catch (Exception ex)
            {
                //Throwing will eventually send the message to the error queue
                Log.Error("Message failed.", ex);
                throw;
            }
        }
    }
}
