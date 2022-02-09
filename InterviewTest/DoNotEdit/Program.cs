using InterviewTest;

BusinessLogic 
    BusinessLogic = new BusinessLogic();

//run simulation and collect output
var results = Task.WhenAll(TestData.GetUserInputs().Select(async (x,i) =>
    {
        Logger
            logger = new Logger();
        return (receivedNumber: i, log:logger, result: await BusinessLogic.ProcessOrder(x, logger));
    }).ToArray())
    .Result
    .OrderBy(x => x.receivedNumber);

//write output to console
foreach(var result in results)
{
    Console.WriteLine($"Order number {result.receivedNumber} =============================");

    Console.WriteLine($"Calculated Order Number = {result.result.OrderNumber}, Processed Successfully = {result.result.Success}, Exception Message = {result.result.Exception?.Message ?? "[No Exception]"}");
    Console.WriteLine();
    
    Console.WriteLine("Log Output");

    foreach (string s in result.log.ToArray())
    {
        Console.WriteLine($"\t{s}");
    }

    Console.WriteLine();
    Console.WriteLine();
}

//all done
Console.WriteLine("All orders have been processed.  Press any key to quit");
Console.Read();

