﻿using System;
using System.Threading.Tasks;

namespace OSS.Pipeline
{


    /// <inheritdoc />
    public class SimpleEffectActivity<TResult> : BaseEffectActivity<TResult>
    {
        private readonly Func<Task<(TrafficSignal traffic_signal, TResult result)>> _exeFunc;

        /// <inheritdoc />
        public SimpleEffectActivity(Func<Task<(TrafficSignal traffic_signal, TResult result)>> exeFunc, string pipeCode=null)
        {
            if (!string.IsNullOrEmpty(pipeCode))
            {
                PipeCode = pipeCode;
            }
            _exeFunc = exeFunc ?? throw new ArgumentNullException(nameof(exeFunc), "执行方法不能为空!");
        }

        /// <inheritdoc />
        protected override Task<(TrafficSignal traffic_signal, TResult result)> Executing()
        {
            return _exeFunc();
        }
    }


    /// <inheritdoc />
    public class SimpleEffectActivity<TFuncPara, TResult>: BaseEffectActivity<TFuncPara, TResult>// : BaseStraightPipe<TFuncPara, TResult>
    {
        private readonly Func<TFuncPara,Task<(TrafficSignal traffic_signal, TResult result)>> _exeFunc;

        /// <inheritdoc />
        public SimpleEffectActivity(Func<TFuncPara,Task<(TrafficSignal traffic_signal, TResult result)>> exeFunc,string pipeCode = null)
        {
            if (!string.IsNullOrEmpty(pipeCode))
            {
                PipeCode = pipeCode;
            }
            _exeFunc = exeFunc ?? throw new ArgumentNullException(nameof(exeFunc), "执行方法不能为空!");
        }

        /// <inheritdoc />
        protected override Task<(TrafficSignal traffic_signal, TResult result)> Executing(TFuncPara contextData)
        {
            return _exeFunc(contextData);
        }
    }

}