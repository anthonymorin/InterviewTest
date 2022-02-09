using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest
{
    public static class Utilities
    {
        public static async Task VerifyInformation(IUserInput input)
        {

            //simulating a potentially long-running web api call
            await Task.Delay((((int)input.Card.CCV) % 5) * 1000);

            //for the purposes of this excercise, this is the only case we will fail on
            if (input.Card.CCV < 0 || input.Card.CCV > 999)
                throw new Exception($"CCV {input.Card.CCV} is out of range");            
        }

        public static async Task SubmitCreditCardOrder(ICreditCard card, decimal total)
        {
            //simulating a potentially long-running web api call
            await Task.Delay((((int)total) % 3) * 1000);
        }

        internal static async Task Ship(IAddress shippingAddress, IShoppingCart cart)
        {
            await Task.Delay((((int)cart.Count()) % 3) * 1000);
        }

        internal static async Task SendConfirmation(IUserInfo userInfo, IUserInfo info)
        {
            await Task.Delay((((int)info.Email.Length) % 3) * 1000);
        }
    }
}
