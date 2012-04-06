using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages
{
    [DataContract]
    public class CommandResult
    {
        public static CommandResult Success()
        {
            return new CommandResult {IsSuccess = true };
        }

        public static CommandResult Exception(Exception ex)
        {
            return new CommandResult { IsSuccess = false, Error = ex.Message };
        }

        public static CommandResult TimeOut(int timeout)
        {
            return new CommandResult { IsSuccess = false, Error = string.Format("Breake by timeout - {0} sec.", timeout) };
        }

        private CommandResult(){}

        [DataMember]
        public bool IsSuccess { get; private set; }

        [DataMember]
        public String Error { get; private set; }
    }
}