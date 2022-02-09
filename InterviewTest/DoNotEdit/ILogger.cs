using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest
{
    public interface ILogger
    {
        void Log(string message);
        void Log(IUserInfo info);
        void Log(IAddress ShippingAddress);
        void Log(IShoppingCart Cart);
        void Log(ICreditCard Card);
    }

    internal class Logger : ILogger, IEnumerable<string>
    {
        readonly List<string> 
            _log = new List<string>();

        public void Log(string message)
        {
            _log.Add(message);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _log.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Log(IUserInfo info)
        {
            //This implementation intentionally does nothing, but assume it is needed for a future planned logging feature
        }

        public void Log(IAddress ShippingAddress)
        {
            //This implementation intentionally does nothing, but assume it is needed for a future planned logging feature
        }

        public void Log(IShoppingCart Cart)
        {
            //This implementation intentionally does nothing, but assume it is needed for a future planned logging feature
        }

        public void Log(ICreditCard Card)
        {
            //This implementation intentionally does nothing, but assume it is needed for a future planned logging feature
        }
    }
}
