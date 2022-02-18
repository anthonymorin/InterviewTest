﻿using System;
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
        public Exception? CurrentException;
        public Task<IResult> ProcessOrder(IUserInput input, ILogger log)
        {
            return Task.Run<IResult>(() =>
            {
                CurrentException = null;
                bool cardinfovalid = false;
                bool userinfovalid  =false;
                bool isshippingaddressvalid = true;
                //verify information
                try
                {
                    if(input != null)
                    {
                    Utilities.VerifyInformation(input).Wait();
                    log.Log("Info verified successfully");
                    log.Log(input.UserInfo);
                    log.Log(input.ShippingAddress);
                    log.Log(input.Cart);
                    log.Log(input.Card);
                    userinfovalid = true;
                    }

                }
                catch (Exception ex)
                {
                    CurrentException = ex;
                    log.Log("Exception thrown when verifying info");
                    log.Log(input.UserInfo);
                    log.Log(input.ShippingAddress);
                    log.Log(input.Cart);
                    log.Log(input.Card);
                }

                Result result = new Result();
                result.OrderNumber = ordernumber;
                ordernumber++;

                //process payment
                try
                {
                    double sum = 0;

                    foreach (var item in input.Cart)//ex 3
                    {
                        sum += item.Quantity * (double)item.UnitPrice;
                    }

                    if (userinfovalid)//card info varifcation only if user info is valid
                    {
                        Utilities.SubmitCreditCardOrder(input.Card, (decimal)sum).Wait();
                        
                        log.Log("Payment processed successfully");
                        log.Log(input.UserInfo);
                        log.Log(input.ShippingAddress);
                        log.Log(input.Cart);
                        log.Log(input.Card);
                        cardinfovalid =true;
                    }
                    else
                    {
                        log.Log("failded  userifo validation not processing card validation");
                    }
                }
                catch (Exception ex)
                {
                    CurrentException = ex;
                    log.Log("Exception thrown when processing payment");
                    log.Log(input.UserInfo);
                    log.Log(input.ShippingAddress);
                    log.Log(input.Cart);
                    log.Log(input.Card);
                }

                //initiate shipment
                try
                {
                    if(cardinfovalid)
                    {
                        Utilities.Ship(input.ShippingAddress, input.Cart).Wait();
                        log.Log("Shipment initiated successfully");
                        log.Log(input.UserInfo);
                        log.Log(input.ShippingAddress);
                        log.Log(input.Cart);
                        log.Log(input.Card);
                    }
                }
                catch (Exception ex)
                {
                    isshippingaddressvalid = false;
                    CurrentException = ex;
                    log.Log("Exception thrown when initiating shipment");
                    log.Log(input.UserInfo);
                    log.Log(input.ShippingAddress);
                    log.Log(input.Cart);
                    log.Log(input.Card);

                }

                //send confirmation email
                try
                {
                    if(isshippingaddressvalid)
                    {
                        Utilities.SendConfirmation(input.UserInfo, input.UserInfo).Wait();
                        log.Log("Confirmation email sent successfully");
                        log.Log(input.UserInfo);
                        log.Log(input.ShippingAddress);
                        log.Log(input.Cart);
                        log.Log(input.Card);
                    }
                }
                catch (Exception ex)
                {
                    CurrentException = ex;
                    log.Log("Exception thrown when sending confirmation email");
                    log.Log(input.UserInfo);
                    log.Log(input.ShippingAddress);
                    log.Log(input.Cart);
                    log.Log(input.Card);
                }


                if (CurrentException != null)
                {
                    result.Exception = CurrentException;
                    result.Success = false;
                    log.Log("Order cancelled because exception was thrown");
                    log.Log(input.UserInfo);
                    log.Log(input.ShippingAddress);
                    log.Log(input.Cart);
                    log.Log(input.Card);
                }

                return result;
            });
        }

        public class Result : IResult
        {
            bool isSuccess;
            Exception? exception;

            public int OrderNumber
            {
                get; set;
            }
            public bool Success 
            {
                get { return isSuccess; }
                set { isSuccess = value; }
            }

            public Exception? Exception 
            {
                get => exception; 
                set => exception = value; 
            }
        }
    }
}
