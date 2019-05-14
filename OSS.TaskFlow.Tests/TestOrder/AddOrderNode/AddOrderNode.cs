﻿using System.Collections.Generic;
using System.Threading.Tasks;
using OSS.Common.ComModels;
using OSS.EventNode;
using OSS.EventNode.MetaMos;
using OSS.EventTask.Interfaces;
using OSS.TaskFlow.Tests.TestOrder.AddOrderNode.Reqs;
using OSS.TaskFlow.Tests.TestOrder.AddOrderNode.Tasks;

namespace OSS.TaskFlow.Tests.TestOrder.Nodes
{
    /// <summary>
    ///   添加订单节点
    /// </summary>
    public class AddOrderNode : BaseNode<AddOrderReq, ResultIdMo>
    {
        private static NodeMeta nodeMeta=new NodeMeta()
        {
            flow_id = "Order_Flow",
            node_alias = ""
        };
        public AddOrderNode()
        {

        }





        // 获取所有执行任务
        private static List<IBaseTask<AddOrderReq>> list;
        protected override async Task<IList<IBaseTask<AddOrderReq>>> GetTasks() => list;

        static AddOrderNode()
        {
            list = new List<IBaseTask<AddOrderReq>>()
            {
                new CouponUseTask(),
                new PriceComputeTask(),
                new StockUseTask(),
                new InsertOrderTask()
            };
        }
    }
}