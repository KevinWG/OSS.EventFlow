﻿using System;
using System.Threading.Tasks;
using OSS.Common.BasicMos.Resp;
using OSS.EventTask;
using OSS.EventTask.Extension;
using OSS.EventTask.MetaMos;
using OSS.EventTask.Mos;
using OSS.TaskFlow.Tests.TestOrder.AddOrderNode.Reqs;

namespace OSS.TaskFlow.Tests.TestNodes.AddNode.Tasks
{
    public class InsertOrderTask : EventTask<AddOrderReq, Resp>
    {
        public InsertOrderTask() : base(new TaskMeta
        {
            task_id = "InsertOrderTask",
            task_alias = "添加订单！",
            loop_times = 3,
            revert_effect = RevertEffect.RevertAll
        })
        {

        }

        /// <inheritdoc />
        protected override async Task<DoResp<Resp>> Do(AddOrderReq data, int loopTimes, int triedTimes)
        {
            throw new ArgumentNullException("sssssss");
            return new DoResp<Resp>()
            {
                resp = new IdResp("1000"),
                run_status = TaskRunStatus.RunCompleted
            };
        }
    }


}