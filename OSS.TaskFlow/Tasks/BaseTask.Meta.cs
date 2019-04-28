﻿using System.Threading.Tasks;
using OSS.TaskFlow.Node.Interfaces;
using OSS.TaskFlow.Node.MetaMos;
using OSS.TaskFlow.Tasks.Interfaces;
using OSS.TaskFlow.Tasks.Mos;

namespace OSS.TaskFlow.Tasks
{
    public abstract partial class BaseTask
    {
        public InstanceType InstanceType { get; protected set; }


        #region 注册存储接口

        internal INodeProvider m_metaProvider;

        internal void RegisteProvider_Internal(INodeProvider metaPpro)
        {
            m_metaProvider = metaPpro;
        }
        
        #endregion
        

        #region 内部扩展方法

        internal abstract Task SaveTaskContext_Internal(TaskContext context, TaskReqData data);
        
        #endregion


    }
}