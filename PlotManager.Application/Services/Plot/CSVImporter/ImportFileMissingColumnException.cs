using System;
using System.Collections.Generic;

namespace PlotManager.Application.Services
{
    public class ImportFileMissingColumnException : Exception
    {
        public List<string> ValidationMessages { get; private set; }

        public ImportFileMissingColumnException(string message, List<string> validationMessages) : base(message)
        {
            this.ValidationMessages = validationMessages;
        }
    }
}