using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry_ECommerce_Task.Interfaces
{
    public interface IShippingService
    {
        string GetName();
        double GetWeight();
    }
}
