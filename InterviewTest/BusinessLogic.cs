using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest
{
    /// <summary>
    /// Welcome to my general c# knowledge test
    ///
    /// IMPORTANT: Before you start, please create a branch from 'master' so you can commit onto your own branch
    ///
    /// The code in this page has various known issues.  
    /// These could include, and may not be limited to:
    ///     stylistic problems
    ///     redundancy
    ///     bad practices
    ///     antipatterns
    ///     concurrency issues
    ///     inefficiency
    ///     
    /// Running the current implementation, the output is not correct
    /// You should hopefully be able to infer what the correct output should be by assessing the current, incorrect output.
    /// 
    /// I would like you to refactor this code to make it as correct and readable as you can.
    /// 
    /// You should read the files in the DoNotEdit folder for context, but those classes should not be changed.
    /// 
    /// The following pages are a useful refresher for some of the issues in this code 
    /// https://en.wikipedia.org/wiki/Anti-pattern#Software_engineering
    /// https://www.c-sharpcorner.com/article/common-code-smell-mistakes-in-c-sharp-part-1/
    /// 
    /// Please commit your work on your own branch and push to the remote repo when you finish
    /// </summary>
    internal class BusinessLogic
    {
        public int ordernumber = 0;
        public Task<IResult> ProcessOrder(IUserInput input, ILogger log)
        {
            return ProcessOrderImpl(input, log);
        }

        private async Task<IResult> ProcessOrderImpl(IUserInput input, ILogger log)
        {
            Result result = new Result(){
                Success = true,
                Exception = null,
                OrderNumber = 0
            };

                //verify information
                try
                {
                    await Utilities.VerifyInformation(input);
                    NicerLog("Info verified successfully", input);
                }
                catch (Exception ex)
                {
                    return FailedStep(ex, result, "verification");
                }
                
                lock (this)
                {
                    // Assumption: Order number doesn't matter if info not verified
                    result.OrderNumber = ordernumber;
                    ordernumber++;
                }
                
                //process payment
                try
                {
                    decimal sum = 0;

                    foreach (var item in input.Cart)
                    {
                        sum += item.Quantity * item.UnitPrice;
                    }

                    await Utilities.SubmitCreditCardOrder(input.Card, sum);
                    NicerLog("Payment processed successfully", input);
  
                    }
                    catch (Exception ex)
                    {
                        return FailedStep(ex, result, "payment");
                    }


                //initiate shipment
                
                    try
                    {
                        // maybe try to get rid of exception, handle more gracefully
                        await Utilities.Ship(input.ShippingAddress, input.Cart);
                        NicerLog("Shipment initiated  successfully", input);
                    }
                    catch (Exception ex)
                    {
                        return FailedStep(ex, result, "Shipping");
                    }


                //send confirmation email
                    try
                    {
                        // maybe try to get rid of exception, handle more gracefully
                        await Utilities.SendConfirmation(input.UserInfo, input.UserInfo);
                        NicerLog("Confirmation email sent successfully", input);
                    }
                    catch (Exception ex)
                    {
                        return FailedStep(ex, result, "Confirmation");
                    }

                return result;
        }

        private void NicerLog(string msg, IUserInput input)
        {
            log.Log(msg);
            log.Log(input.UserInfo);
            log.Log(input.ShippingAddress);
            log.Log(input.Cart);
            log.Log(input.Card);
        }

        private Result FailedStep(Exception ex, Result result, string msg)
        {
                    result.Exception = ex;
                    result.Success = false;
                    log.Log("Order cancelled because exception was thrown: " + msg);
                    log.Log(input.UserInfo);
                    log.Log(input.ShippingAddress);
                    log.Log(input.Cart);
                    log.Log(input.Card);

                    return result;
        }


        public class Result : IResult
        {
            // bool isSuccess; // delete
            // Exception? exception; // dlete

            public int OrderNumber
            {
                get; set;
            }
            public bool Success 
            {
                get; set;
                // get { return isSuccess; }
                // set { isSuccess = value; }
            }

            public Exception? Exception 
            {
                get; set;
                // get => exception; 
                // set => exception = value; 
            }
        }
    }
}
