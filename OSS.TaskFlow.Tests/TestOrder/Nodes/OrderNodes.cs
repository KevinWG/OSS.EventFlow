﻿using System.Collections.Generic;
using System.Threading.Tasks;
using OSS.Common.ComModels;
using OSS.TaskFlow.Node;
using OSS.TaskFlow.Tasks.MetaMos;
using OSS.TaskFlow.Tests.TestOrder.Tasks;

namespace OSS.TaskFlow.Tests.TestOrder.Nodes
{
   
    //public class CheckOrderNode: BaseNode<OrderCheckReq>
    //{
    //    protected override Task<ResultListMo<TaskMeta>> GetTaskMetas(NodeContext req)
    //    {
    //        var addOrderTaskMeta = new TaskMeta();
    //        addOrderTaskMeta.task_id = "CheckOrder";
    //        addOrderTaskMeta.task_alias = "校验订单";
    //        addOrderTaskMeta.run_type = RunType.PauseOnFailed;
            
    //        var notifyTaskMeta = new TaskMeta();
    //        notifyTaskMeta.task_id = "Notify";
    //        notifyTaskMeta.task_alias = "添加订单通知";
    //        notifyTaskMeta.run_type = RunType.PauseOnFailed;

    //        var list = new List<TaskMeta>();
    //        list.Add(addOrderTaskMeta);
    //        list.Add(notifyTaskMeta);

    //        return Task.FromResult(new ResultListMo<TaskMeta>(list));
    //    }

    //    protected override BaseTask GetTaskByMeta(TaskMeta meta)
    //    {
    //        switch (meta.task_id)
    //        {
    //            case "CheckOrder":
    //                return new OrderCheckTask();
    //            case "AddOrder":
    //                return new OrderNotifyTask();
    //        }
    //        return null;
    //    }
    //}


    
}
