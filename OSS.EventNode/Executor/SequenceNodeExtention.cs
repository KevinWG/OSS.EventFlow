﻿using System.Collections.Generic;
using System.Threading.Tasks;
using OSS.Common.ComModels;
using OSS.EventNode.Mos;
using OSS.EventTask.Interfaces;
using OSS.EventTask.MetaMos;
using OSS.EventTask.Mos;

namespace OSS.EventNode.Executor
{
    public static class SequenceNodeExtention
    {
        ///  顺序执行
        internal static async Task Excuting_Sequence<TTReq, TTRes>(this BaseNode<TTReq, TTRes> node,
            TTReq req,
            NodeResponse<TTRes> nodeResp, 
            IList<IBaseTask<TTReq>> tasks,
            int triedTimes)
            where TTReq : class 
            where TTRes : ResultMo, new()
        {
            nodeResp.TaskResults = new Dictionary<TaskMeta, TaskResponse<ResultMo>>(tasks.Count);
            nodeResp.node_status = NodeStatus.ProcessCompoleted; // 默认成功，给出最大值，循环内部处理

            //TaskMeta errTaskMeta = null;
            foreach (var tItem in tasks)
            {
                var taskResp = await ExecutorUtil.TryGetTaskItemResult(req, tItem, triedTimes);

                var tMeta = tItem.TaskMeta;
                nodeResp.TaskResults.Add(tMeta, taskResp);

                var haveError = ExecutorUtil.FormatNodeErrorResp(nodeResp, taskResp, tMeta);
                if (haveError)
                {
                    nodeResp.block_taskid = tMeta.task_id;
                    //errTaskMeta = tMeta;
                    break;
                }
            }

            //if (nodeResp.node_status == NodeStatus.ProcessFailedRevert)
            //    await Excuting_SequenceRevert(req, nodeResp, tasks, errTaskMeta);
        }


        //  顺序任务的回退处理
        internal static async Task Excuting_SequenceRevert<TTReq, TTRes>(this BaseNode<TTReq, TTRes> node,TTReq req, NodeResponse<TTRes> nodeResp,
            IList<IBaseTask<TTReq>> tasks,string blockTaskId,int triedTimes)
            where TTReq : class 
            where TTRes : ResultMo, new()
        {
            foreach (var tItem in tasks)
            {
                if (tItem.TaskMeta.task_id== blockTaskId)
                {
                    break;
                }

                var rRes = await ExecutorUtil.TryRevertTask(tItem, req, triedTimes);// tItem.Revert(req);
                if (rRes)
                    nodeResp.RevrtTasks.Add(tItem.TaskMeta);
            }
        }



      

    }
}
