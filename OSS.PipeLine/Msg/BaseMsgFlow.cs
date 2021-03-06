﻿#region Copyright (C) 2020 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：OSS.EventFlow -  消息流体基类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       创建时间： 2020-11-22
*       
*****************************************************************************/

#endregion

using System;
using System.Threading.Tasks;
using OSS.DataFlow;
using OSS.Pipeline.Base;

namespace OSS.Pipeline
{
    /// <summary>
    ///  消息流基类
    /// </summary>
    /// <typeparam name="TMsg"></typeparam>
    public abstract class BaseMsgFlow<TMsg> : BaseThreeWayPipe<TMsg, TMsg, TMsg>,IDataSubscriber<TMsg>
    {
        // 内部异步处理入口
        private readonly IDataPublisher<TMsg> _pusher;

        /// <summary>
        ///  异步缓冲连接器
        /// </summary>
        /// <param name="pipeCode"> 作为缓冲DataFlow 对应的Key   默认对应的flow是异步线程池</param>
        protected BaseMsgFlow(string pipeCode = null) : this(pipeCode, null)
        {
        }

        /// <summary>
        ///  异步缓冲连接器
        /// </summary>
        /// <param name="pipeCode">缓冲DataFlow 对应的Key   默认对应的flow是异步线程池</param>
        /// <param name="option"></param>
        protected BaseMsgFlow(string pipeCode, DataFlowOption option) : base(pipeCode,PipeType.MsgFlow)
        {
            if (string.IsNullOrEmpty(pipeCode))
            {
                throw new ArgumentNullException(nameof(pipeCode), "消息类型PipeCode不能为空!");
            }

            _pusher = CreateFlow(pipeCode, this, option);
        }
        
        /// <summary>
        ///  创建消息流
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="option"></param>
        /// <param name="pipeDataKey"></param>
        /// <returns></returns>
        protected abstract IDataPublisher<TMsg> CreateFlow(string pipeDataKey,IDataSubscriber<TMsg> subscriber, DataFlowOption option);

        #region 流体内部业务处理

        /// <inheritdoc />
        internal override async Task<TrafficResult> InterPreCall(TMsg context,string prePipeCode)
        {
            var pushRes = await _pusher.Publish(PipeCode,context);
            return pushRes
                ? new TrafficResult(SignalFlag.Green_Pass, string.Empty, string.Empty)
                : new TrafficResult(SignalFlag.Red_Block, PipeCode, $"({this.GetType().Name})推送消息失败!");
        }

        /// <inheritdoc />
        internal override Task<TrafficResult<TMsg, TMsg>> InterProcessPackage(TMsg context, string prePipeCode)
        {
            return Task.FromResult(new TrafficResult<TMsg, TMsg>(SignalFlag.Green_Pass, string.Empty, string.Empty,
                context,context));
        }
        
        #endregion

        /// <summary>
        ///  订阅唤起操作
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> Subscribe(TMsg data)
        {
            return (await InterProcess(data,string.Empty)).signal==SignalFlag.Green_Pass;
        }
    }

}
