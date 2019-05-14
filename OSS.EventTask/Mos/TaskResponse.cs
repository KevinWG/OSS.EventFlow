﻿using OSS.Common.ComModels;
using OSS.Common.ComModels.Enums;
using OSS.EventTask.Util;

namespace OSS.EventTask.Mos
{
    public class TaskResponse<TRes>
    {
        /// <summary>
        ///  运行状态
        /// </summary>
        public TaskRunStatus run_status { get;internal set; }

        public RunCondition task_condition { get;internal set; }

        /// <summary>
        ///  返回信息
        /// </summary>
        public TRes resp { get; internal set; }
    }
    
    public class DoResponse<TRes>
    {
        /// <summary>
        ///  运行状态
        ///     = TaskRunStatus.RunFailed 系统会字段判断是否满足重试条件执行重试
        /// </summary>
        public TaskRunStatus run_status { get; set; }

        /// <summary>
        ///  返回信息
        /// </summary>
        public TRes resp { get; set; }
    }

    public static class TaskResponseExtention
    {
        public static TaskResponse<TRes> WithError<TRes>(this TaskResponse<TRes> res,TaskRunStatus status, RunCondition condition,string msg=null)
            where TRes : ResultMo, new()
        {
            res.run_status = status;
            res.task_condition = condition;
            res.resp =new TRes().WithResult(SysResultTypes.ApplicationError, msg??"Task Error！");
            return res;
        }
    }


}
