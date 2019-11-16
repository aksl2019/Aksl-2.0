using System;
using System.Threading.Tasks;

namespace Contoso.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await WebApiSender.Instance.InitializeTask();

            #region Dataflow Retry
            //await WebApiSender.Instance.DataflowBulkInsertBlockRetryAsync(maxRetryCount: 200, orderCount:1000);//每次发送1000
            //DataflowBulkInserter:Information: CreateBlockers's ExecutionTime=00:00:06.8799907,Count="1000",ThreadId=4,now:"16:25:25.6725444"
            //DataflowBulkInser-1:200000:Information: ----dataflow bulk insert 200000 orders,cost time:"00:14:59.5059706",transport time:00:14:59.2640956,count/time(sec):223,now:"16:25:25.6792083"----
            //DataflowBulkInserter:Information: CreateBlockers's ExecutionTime=00:00:06.7524192,Count="1000",count/time(sec):149,ThreadId=10,now:"00:31:25.8603656"
            //SSqlOrderRepository:Information: ----finish dataflow bulk insert 1000 orders,cost time:00:00:06.7581398,ThreadId=12,now:00:31:25.8632594"----
            //DataflowBulkInser-1:200000:Information: ----dataflow bulk insert 200000 orders,cost time:"00:14:38.1658521",transport time:00:14:37.9381377,count/time(sec):228,now:"00:31:25.8666704"----

            //await WebApiSender.Instance.DataflowBulkInsertBlockRetryTasksAsync( frequency: 1000, taskCount : 20,count : 1,  orderCount :500,  maxRetryCount :10);
            //DataflowBulkInser-1-OrderCount:100000:Information: ----dataflow bulk insert 100000 orders,cost time:"00:06:37.0015124",transport time:00:06:26.9682339,count/time(sec):259,now:"16:00:23.4915202"----

            // await WebApiSender.Instance.DataflowBulkInsertBlockRetryTasksAsync(frequency: 1000, taskCount: 10, count: 1, orderCount:1000, maxRetryCount: 10);
            //DataflowBulkInser-1-OrderCount:100000:Information: ----dataflow bulk insert 100000 orders,cost time:"00:04:48.4800067",transport time:00:04:38.4535051,count/time(sec):360,now:"16:55:39.1291370"----

            // await WebApiSender.Instance.DataflowBulkInsertBlockRetryTasksAsync(frequency: 1000, taskCount: 10, count: 1, orderCount: 1000, maxRetryCount: 20);
            //DataflowBulkInser-1-OrderCount:200000:Information: ----dataflow bulk insert 200000 orders,cost time:"00:11:56.4675053",transport time:00:11:36.4276595,count/time(sec):288,now:"17:13:24.2285559"----
            
            //50万
            await WebApiSender.Instance.DataflowBulkInsertBlockRetryTasksAsync(frequency: 1000, taskCount:10, count: 1, orderCount: 5000, maxRetryCount: 10);
           //DataflowBulkInserter: Information: CreateBlockers's ExecutionTime=00:02:37.1447949,Count="5000",count/time(sec):32,ThreadId=41,now:"17:28:13.5685400"
           //SqlOrderRepository: Information: ----finish dataflow bulk insert 5000 orders,cost time:00:02:37.1579711,ThreadId = 39,now: 17:28:13.5720131"----
           //DataflowBulkInser - 1 - OrderCount:500000:Information: ----dataflow bulk insert 500000 orders,cost time:"00:17:59.0701464",transport time:00:17:48.9966265,count / time(sec):468,now: "17:28:14.5821800"----
         
            // await WebApiSender.Instance.DataflowBulkInsertBlockRetryTasksAsync(frequency: 1000, taskCount: 200, count: 1, orderCount: 500, maxRetryCount: 1);
            //DataflowBulkInserter:Information: CreateBlockers's ExecutionTime=00:07:11.8930123,Count="500",ThreadId=200,now:"21:07:16.8112975"
            //DataflowBulkInser-1-OrderCount:100000:Information: ----dataflow bulk insert 100000 orders,cost time:"00:10:32.9506687",transport time:00:10:31.9331398,count/time(sec):159,now:"21:07:27.9275001"----

            // await WebApiSender.Instance.DataflowBulkInsertBlockRetryTasksAsync(frequency: 1000, taskCount:1000, count: 1, orderCount:100, maxRetryCount: 1);
            //DataflowBulkInserter:Information: CreateBlockers's ExecutionTime=00:25:36.8858849,Count="100",ThreadId=853,now:"22:45:15.6557416"
            //DataflowBulkInser-1-OrderCount:100000:Information: ----dataflow bulk insert 100000 orders,cost time:"00:42:08.7395321",transport time:00:42:07.7359570,count/time(sec):40,now:"22:45:16.6629758"----

            // await WebApiSender.Instance.DataflowBulkInsertBlockRetryTasksAsync(frequency: 500, taskCount: 12, count: 1, orderCount: 5000, maxRetryCount: 2);
            //DataflowBulkInserter: Information: CreateBlockers's ExecutionTime=00:01:25.3308401,Count="5000",ThreadId=24,now:"13:42:18.7197436"
            //SqlOrderRepository: Information: ----finish dataflow bulk insert 5000 orders,cost time:00:01:26.1676555,ThreadId = 13,now: 13:42:18.7226815"----
            //DataflowBulkInser - 1 - OrderCount:120000:Information: ----dataflow bulk insert 120000 orders,cost time:"00:03:03.7720342",transport time:00:03:02.7672859,count / time(sec):657,now: "13:42:19.2406019"----

            // await WebApiSender.Instance.DataflowBulkInsertBlockRetryTasksAsync(frequency: 500, taskCount: 12, count: 1, orderCount: 5000, maxRetryCount: 10);
            //DataflowBulkInserter: Information: CreateBlockers's ExecutionTime=00:03:36.5273079,Count="5000",ThreadId=46,now:"14:35:39.4126236"
            //SqlOrderRepository: Information: ----finish dataflow bulk insert 5000 orders,cost time:00:03:36.5871983,ThreadId = 37,now: 14:35:39.4153365"----
            //DataflowBulkInser - 1 - OrderCount:600000:Information: ----dataflow bulk insert 600000 orders,cost time:"00:25:02.3612023",transport time:00:24:57.2844565,count / time(sec):401,now: "14:35:39.9326504"----

            //await WebApiSender.Instance.DataflowBulkInsertBlockRetryTasksAsync(frequency: 500, taskCount: 12, count: 1, orderCount: 5000, maxRetryCount: 20);
            //DataflowBulkInserter: Information: CreateBlockers's ExecutionTime=00:06:38.4120889,Count="5000",ThreadId=14,now:"00:59:03.6299909"
            //SqlOrderRepository: Information: ----finish dataflow bulk insert 5000 orders,cost time:00:06:38.4171721,ThreadId = 10,now: 00:59:03.6325897"----
            //DataflowBulkInser - 1 - OrderCount:1200000:Information: ----dataflow bulk insert 1200000 orders,cost time:"01:18:47.1240764",transport time:01:18:37.0959011,count / time(sec):255,now: "00:59:04.1426769"----

            // await WebApiSender.Instance.DataflowBulkInsertBlockRetryTasksAsync(frequency: 500, taskCount: 12, count: 1, orderCount: 20000, maxRetryCount: 10);
            //DataflowBulkInserter:Information: CreateBlockers's ExecutionTime=00:26:44.2319721,Count="10000",count/time(sec):7,ThreadId=35,now:"16:03:59.0437152"
            //SqlOrderRepository: Information: ----finish dataflow bulk insert 20000 orders,cost time:00:26:47.2547429,ThreadId = 34,now: 16:03:59.0465232"----
            //DataflowBulkInser - 1 - OrderCount:2400000:Information: ----dataflow bulk insert 2400000 orders,cost time:"02:41:44.4752474",transport time:02:41:39.4640383,count / time(sec):248,now: "16:03:59.5769668"----

            //await WebApiSender.Instance.DataflowBulkInsertBlockRetryTasksAsync(frequency: 500, taskCount:5, count: 1, orderCount: 400000, maxRetryCount: 2);
            //DataflowBulkInserter: Information: CreateBlockers's ExecutionTime=01:02:02.1011743,Count="10000",count/time(sec):3,ThreadId=24,now:"00:07:04.2529236"
            //SqlOrderRepository: Information: ----finish dataflow bulk insert 400000 orders,cost time:05:19:54.5125917,ThreadId = 29,now: 00:07:04.2573159"----
            //DataflowBulkInser - 1 - OrderCount:4000000:Information: ----dataflow bulk insert 4000000 orders,cost time:"07:17:50.5704573",transport time:07:17:49.5641552,count / time(sec):153,now: "00:07:05.5215037"----

            //500万
            //await WebApiSender.Instance.DataflowBulkInsertBlockRetryTasksAsync(frequency: 500, taskCount: 10, count: 1, orderCount: 50000, maxRetryCount: 10);

            #endregion

            //await WebApiSender.Instance.InitializeDataflowBulkInsertBlockTimerAsync(frequency:1000,  maxRetryCount: 2, orderCount:1000);//每隔50毫秒发送1000数据
            //await WebApiSender.Instance.StartDataflowBulkInsertBlockTimerAsync();

            #region Tasks
            #region Dataflow
            //---Dataflow---
            // 200000=20万
            //await WebApiSender.Instance.DataflowBulkInsertBlockTasksAsync(taskCount: 100, orderCount: 10000);//分块发送1百万,每次发送2万

            //1000000=1百万
            //每次插入5000=5千
            //await WebApiSender.Instance.DataflowBulkInsertLoopTasksAsync(taskCount:50, count: 1, orderCount: 20000);//每次发送2万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow bulk insert 1000000 orders,cost time:"01:00:29.4709309,count/time(sec):276,now:"11:54:34.4154042"----

            //每次插入5000=5千
            // await WebApiSender.Instance.DataflowBulkInsertBlockTasksAsync(taskCount: 5, orderCount: 200000);//分块发送1百万,每次发送2万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow bulk insert 1000000 orders,cost time:"01:03:43.0752221,count/time(sec):262,now:"11:10:29.9852570"----

            //每次插入3000=3千
            // await WebApiSender.Instance.DataflowBulkInsertBlockTasksAsync(taskCount: 50, orderCount: 20000);//分块发送1百万,每次发送2万
            // Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow bulk insert 1000000 orders,cost time:"01:59:07.9422652,count/time(sec):140,now:"14:36:44.1348216"----

            //每次插入1000=1千
            //await WebApiSender.Instance.DataflowBulkInsertBlockTasksAsync(taskCount: 50, orderCount: 20000);//分块发送1百万,每次发送2千
            // Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow bulk insert 1000000 orders,cost time:"01:58:54.5734731,count/time(sec):141,now:"11:49:03.7328287"----

            //每次插入10000=1万
            //await WebApiSender.Instance.DataflowBulkInsertBlockTasksAsync(taskCount: 5, orderCount: 200000);//分块发送1百万,每次发送2万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow bulk insert 1000000 orders,cost time:"00:37:40.2140063,count/time(sec):443,now:"00:46:29.0299677"----

            //await WebApiSender.Instance.DataflowBulkInsertBlockTasksAsync(taskCount: 5, orderCount: 200000);//分块发送1百万,每次发送10万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow bulk insert 1000000 orders,cost time:"00:37:13.0939988,count/time(sec):448,now:"15:02:28.8836604"----

            // await WebApiSender.Instance.DataflowBulkInsertBlockTasksAsync(taskCount: 5, orderCount: 200000);//分块发送1百万,每次发送10万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow bulk insert 1000000 orders,cost time:"00:37:06.3198636,count/time(sec):450,now:"12:15:43.8243659"----

            // await WebApiSender.Instance.DataflowBulkInsertBlockTasksAsync(taskCount: 50, orderCount: 20000);//分块发送1百万,每次发送2万
            //Contoso.ConsoleApp.WebApiSender:Information:----finish dataflow pipe bulk insert 1000000 orders,cost time:"00:34:50.3070354,count/time(sec):479,now:"13:02:22.8469882"----

            //2000000=2百万
            //每次插入10000=1万
            // await WebApiSender.Instance.DataflowBulkInsertBlockTasksAsync(taskCount: 5, orderCount: 400000);//分块发送2百万,每次发送10万
            // Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow bulk insert 2000000 orders,cost time:"01:56:46.6078977,count/time(sec):286,now:"02:46:46.0517737"----

            //await WebApiSender.Instance.DataflowBulkInsertBlockTasksAsync(taskCount: 5, orderCount: 400000);//分块发送2百万,每次发送20万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow bulk insert 2000000 orders,cost time:"01:52:57.8044809,count/time(sec):296,now:"11:04:40.4650236"----

            // await WebApiSender.Instance.DataflowBulkInsertBlockTasksAsync(taskCount: 10, orderCount: 200000);//分块发送2百万,每次发送10万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow bulk insert 2000000 orders,cost time:"01:57:24.4672120,count/time(sec):284,now:"13:22:15.2261228"----

            //await WebApiSender.Instance.DataflowBulkInsertBlockTasksAsync(taskCount: 20, orderCount: 100000);//分块发送2百万,每次发送10万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow bulk insert 2000000 orders,cost time:"01:55:48.4536256,count/time(sec):288,now:"15:42:26.7864300"----

            //await WebApiSender.Instance.DataflowBulkInsertBlockTasksAsync(taskCount: 50, orderCount: 40000);//分块发送1百万,每次发送4万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow bulk insert 2000000 orders,cost time:"01:55:28.6960849,count/time(sec):289,now:"17:06:46.9833029"----

            //await WebApiSender.Instance.DataflowBulkInsertBlockTasksAsync(taskCount:100, orderCount: 20000);//分块发送1百万,每次发送2万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow bulk insert 2000000 orders,cost time:"02:07:40.9891989,count/time(sec):262,now:"02:20:29.4557790"----

            //5000000=5百万
            //每次插入10000=1万
            //await WebApiSender.Instance.DataflowBulkInsertBlockTasksAsync(taskCount: 25, orderCount: 200000);//分块发送1百万,每次发送2万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow bulk insert 5000000 orders,cost time:"11:32:51.4852359,count/time(sec):121,now:"22:08:23.1033746"----
            #endregion

            #region DataflowPipe
            //---DataflowPipe---
            //500000=50万
            //await WebApiSender.Instance.DataflowPipeBulkInsertLoopTasksAsync(taskCount: 50, count: 1, orderCount:10000);

            //1000000=1百万
            //每次插入5000=5千
            //await WebApiSender.Instance.DataflowPipeBulkInsertLoopTasksAsync(taskCount: 50, count: 1, orderCount: 20000);
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow pipe bulk insert 1000000 orders,cost time:"01:02:03.8143306,count/time(sec):269,now:"15:05:09.0250174"----

            //每次插入10000=1万
            //await WebApiSender.Instance.DataflowPipeBulkInsertBlockTasksAsync(taskCount: 1, orderCount: 1000000);//分块发送1百万,每次发送5万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow pipe bulk insert 1000000 orders,cost time:"00:55:45.4915117,count/time(sec):299,now:"11:26:42.9827611"----

            // await WebApiSender.Instance.DataflowPipeBulkInsertBlockTasksAsync(taskCount: 5, orderCount: 200000);//分块发送1百万,每次发送2万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow pipe bulk insert 1000000 orders,cost time:"00:37:44.9780221,count/time(sec):442,now:"01:32:24.0196925"----

            // await WebApiSender.Instance.DataflowPipeBulkInsertBlockTasksAsync(taskCount: 5, orderCount: 200000);//分块发送1百万,每次发送10万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow pipe bulk insert 1000000 orders,cost time:"00:37:40.4768341,count/time(sec):443,now:"23:55:49.3508068"----

            //2000000=2百万
            //每次插入10000=1万
            //await WebApiSender.Instance.DataflowPipeBulkInsertBlockTasksAsync(taskCount: 5, orderCount: 400000);//分块发送2百万,每次发送10万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish dataflow pipe bulk insert 2000000 orders,cost time:"01:57:45.7002891,count/time(sec):284,now:"14:40:47.6264107"----
            #endregion

            #region Pipe
            //---Pipe---
            //200000=20万
            //await WebApiSender.Instance.PipeBulkInsertLoopTasksAsync(taskCount: 20, count: 1, orderCount: 10000);
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish pipe bulk insert 200000 orders,cost time:"00:05:25.2280348,count/time(sec):615,now:"22:00:05.3787689"----

            //500000=50万
            //await WebApiSender.Instance.PipeBulkInsertLoopTasksAsync(taskCount:50, count: 1, orderCount: 10000);
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish pipe bulk insert 500000 orders,cost time:"00:19:01.9653945,count/time(sec):438,now:"22:23:30.0623940"----

            //1000000=1百万
            //每次插入5000=5千
            //await WebApiSender.Instance.PipeBulkInsertLoopTasksAsync(taskCount: 50, count: 1, orderCount: 20000);
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish pipe bulk insert 1000000 orders,cost time:"00:58:51.3455652,count/time(sec):284,now:"23:33:44.1985892"----

            //每次插入10000=1万
            //await WebApiSender.Instance.PipeBulkInsertLoopTasksAsync(taskCount: 50, count: 1, orderCount: 20000);//每次发送2万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish pipe bulk insert 1000000 orders,cost time:"00:38:13.5900720,count/time(sec):436,now:"10:04:49.9404141"----

            //每次插入10000=1万
            //await WebApiSender.Instance.PipeBulkInsertLoopTasksAsync(taskCount: 1, count: 1, orderCount: 1000000);//直接发送1百万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish pipe bulk insert 1000000 orders,cost time:"00:56:14.4419582,count/time(sec):297,now:"11:14:48.5785772"----

            //await WebApiSender.Instance.PipeBulkInsertBlockTasksAsync(taskCount: 1, orderCount: 1000000);//分块发送1百万,每次发送10万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish pipe bulk insert 1000000 orders,cost time:"00:53:45.8400618,count/time(sec):310,now:"12:32:04.5169510"----

            //await WebApiSender.Instance.PipeBulkInsertBlockTasksAsync(taskCount: 1, orderCount: 1000000);//分块发送1百万,每次发送5万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish pipe bulk insert 1000000 orders,cost time:"00:54:37.6607726,count/time(sec):306,now:"13:40:06.1188932"----

            // await WebApiSender.Instance.PipeBulkInsertBlockTasksAsync(taskCount: 1, orderCount: 1000000);//分块发送1百万,每次发送2万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish pipe bulk insert 1000000 orders,cost time:"00:56:06.7550548,count/time(sec):298,now:"14:49:17.4080141"----

            //await WebApiSender.Instance.PipeBulkInsertBlockTasksAsync(taskCount: 5, orderCount: 200000);//分块发送1百万,每次发送2万
            //Contoso.ConsoleApp.WebApiSender:Information: ----finish pipe bulk insert 1000000 orders,cost time:"00:37:00.5737009,count/time(sec):451,now:"14:38:27.3129999"----
            #endregion
            #endregion

            #region 20万
            // 200000=20万
            //Dataflow
            //await WebApiSender.Instance.DataflowBulkInsertBlockAsync(orderCount: 200000);
            //DataflowBulkInser-1:200000:Information: ----dataflow bulk insert 200000 orders,cost time:"00:08:07.2216502",transport time:00:08:06.8785159,count/time(sec):411,now:"11:50:21.4192201"----

            //DataflowPipe
            // await WebApiSender.Instance.DataflowPipeBulkInsertBlockAsync(orderCount: 200000);
            //DataflowPipeBulkInser-1:200000:Information: ----dataflow pipe bulk insert 200000 orders,cost time:"00:08:10.3788551",transport time:00:08:10.0490795,count / time(sec):409,now: "12:38:35.1514498"----

            //Pipe
            // await WebApiSender.Instance.PipeBulkInsertBlockAsync(orderCount: 200000);
            //PipeBulkInser-1:200000:Information: ----pipe bulk insert 200000 orders,cost time:"00:08:19.2416583",transport time:00:08:18.8958262,count/time(sec):401,now:"21:01:33.8598987"----
            #endregion

            #region 50万
            //500000=50万
            //Dataflow
            // await WebApiSender.Instance.DataflowBulkInsertBlockAsync(orderCount: 500000);
            //DataflowBulkInser-1:500000:Information: ----dataflow bulk insert 500000 orders,cost time:"00:27:10.3096793",transport time:00:27:09.3834066,count/time(sec):307,now:"12:35:55.3258659"----

            //DataflowPipe
            //  await WebApiSender.Instance.DataflowPipeBulkInsertBlockAsync(orderCount: 500000);
            //DataflowPipeBulkInser - 1:500000:Information: ----dataflow pipe bulk insert 500000 orders,cost time:"00:26:57.4258132",transport time:00:26:56.5935862,count / time(sec):310,now: "13:14:25.6607084"----

            //Pipe
            // await WebApiSender.Instance.PipeBulkInsertBlockAsync(orderCount: 500000);
            //PipeBulkInser-1:500000:Information: ----pipe bulk insert 500000 orders,cost time:"00:26:45.9174576",transport time:00:26:45.1368645,count/time(sec):312,now:"21:34:01.4889887"----
            #endregion

            #region 1百万
            //1000000=1百万
            //Dataflow
            //await WebApiSender.Instance.DataflowBulkInsertBlockAsync(orderCount: 1000000);
            //DataflowBulkInser-1:1000000:Information: ----dataflow bulk insert 1000000 orders,cost time:"01:14:05.3743316",transport time:01:14:04.1211887,count/time(sec):226,now:"14:16:02.9771575"----

            //DataflowPipe
            //  await WebApiSender.Instance.DataflowPipeBulkInsertBlockAsync(orderCount: 1000000);

            //Pipe
            // await WebApiSender.Instance.PipeBulkInsertBlockAsync(orderCount: 1000000);
            #endregion

            #region 2百万
            //2,000,000=2百万
            //Dataflow
            // await WebApiSender.Instance.DataflowBulkInsertBlockAsync(orderCount: 2000000);
            //DataflowBulkInser-1:2000000:Information: ----dataflow bulk insert 2000000 orders,cost time:"03:46:08.2800747",transport time:03:46:04.6435899,count/time(sec):148,now:"21:50:48.9082262"----

            //DataflowPipe

            //Pipe
            #endregion

            //  await WebApiSender.Instance.GetPagedOrdersAsync();

            Console.ReadLine();

            //Console.WriteLine("Hello World!");
        }
    }
}
