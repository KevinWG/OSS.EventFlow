using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common.ComModels;
using OSS.TaskFlow.Tests.TestOrder;
using OSS.TaskFlow.Tests.TestOrder.Nodes;

namespace OSS.TaskFlow.Tests
{
    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            //var order = new OrderInfo()
            //{
            //    order_alias = "���Զ���!",
            //    id = 123456,
            //    price = 10.23M
            //};

            //var orderNode = new AddOrderNode();
            //var req = new ExcuteReq();

            //req.body = order ;

            //req.flow_id = "OrderFlow";
            //req.node_id = "AddOrder";

            //try
            //{
            //    await orderNode.Excute(req);
            //}
            //catch (Exception e)
            //{

            //}
        }



        public class RESTest:ResultMo
        {
            public string NN { get; set; }
        }
        
    
    }
}
