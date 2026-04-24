using System;
using System.Collections.Generic;
using System.Text;

namespace TEST_APP_
{
    public class OrderCreatedEventArgs : EventArgs
    {
        public Order Order { get; set; }
    }
}
