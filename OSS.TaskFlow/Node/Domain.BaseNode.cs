﻿using System.Threading.Tasks;
using OSS.Common.ComModels;
using OSS.TaskFlow.Node.Mos;
using OSS.TaskFlow.Tasks.Mos;

namespace OSS.TaskFlow.Node
{
    /// <summary>
    ///  基础领域节点
    ///   todo 获取领域信息
    /// </summary>
    public abstract partial class BaseDomainNode<TReq, TDomain, TRes> : BaseNode<TRes>
        where TRes : ResultMo, new()
    {
        /// <summary>
        ///   主执行方法
        /// </summary>
        /// <param name="con"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<TRes> Excute(NodeContext con, TaskReqData<TReq, TDomain> req)
        {
            return (TRes)await Excute_Internal(con, req);
        }

        #region  进入-执行-返回 -   对外扩展方法


        public Task Activate(NodeContext context)
        {
            return MoveIn(context);
        }


        /// <summary>
        ///  前置进入方法
        /// </summary>
        /// <returns></returns>
        protected internal virtual Task MoveIn(NodeContext con)
        {
            return Task.CompletedTask;
        }

        #endregion
        
        #region 执行 -- 对外扩展方法
         


        protected virtual Task ExcutePre(NodeContext con, TaskReqData<TReq, TDomain> req)
        {
            return Task.CompletedTask;
        }


        #endregion
        
        #region 重写父类扩展
        
       
        internal override Task ExcutePre_Internal(NodeContext con, TaskReqData req)
        {
            return ExcutePre(con,(TaskReqData<TReq, TDomain>)req);
        }  
        #endregion
    }
}
