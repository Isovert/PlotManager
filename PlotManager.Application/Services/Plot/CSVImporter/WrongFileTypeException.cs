using System;

namespace PlotManager.Application.Services
{
    public class WrongFileTypeException : Exception
    {
        public WrongFileTypeException(string message) : base(message)
        {
        }
    }
}