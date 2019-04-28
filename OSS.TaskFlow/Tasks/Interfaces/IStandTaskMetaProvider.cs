﻿using System.Threading.Tasks;
using OSS.TaskFlow.Tasks.Mos;

namespace OSS.TaskFlow.Tasks.Interfaces
{
    public interface IStandTaskMetaProvider<TReq>:ITaskMetaProvider
    {
        Task SaveTaskContext(TaskContext context, TaskReqData<TReq> data);
    }
}