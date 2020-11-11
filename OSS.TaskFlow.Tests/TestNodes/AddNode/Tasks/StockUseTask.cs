﻿using System.Threading.Tasks;
using OSS.Common.BasicMos.Resp;
using OSS.EventTask;
using OSS.EventTask.Extension;
using OSS.EventTask.MetaMos;
using OSS.EventTask.Mos;
using OSS.TaskFlow.Tests.TestOrder.AddOrderNode.Reqs;

namespace OSS.TaskFlow.Tests.TestNodes.AddNode.Tasks
{
    public class StockUseTask : EventTask<AddOrderReq, Resp>
    {

        public StockUseTask() : base(new TaskMeta
        {
            task_id = "StockUseTask",
            task_alias = "扣减库存！",
            loop_times = 3,
            failed_effect = FailedEffect.FailedSelf
        })
        {

        }

        protected override async Task<DoResp<Resp>> Do(AddOrderReq data, int loopTimes, int triedTimes)
        {
            //throw new ArgumentNullException("sssss");
            return new DoResp<Resp>()
            {
                run_status = TaskRunStatus.RunCompleted,
                resp = new Resp()
            };
        }
    }
}