﻿#region Copyright (C) 2020 Kevin (OSS开源系列) 公众号：OSSCore

/***************************************************************************
*　　	文件功能描述：OSS.EventFlow -  活动基类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       创建时间： 2020-11-22
*       
*****************************************************************************/

#endregion

using OSS.Pipeline.Mos;
using System.Threading.Tasks;

namespace OSS.Pipeline.Activity
{
    /// <summary>
    /// 主动触发执行活动组件基类(不接收上下文)
    /// </summary>
    public abstract class BaseActivity : BasePipe<EmptyContext, EmptyContext>
    {
        /// <summary>
        /// 外部Action活动基类
        /// </summary>
        protected BaseActivity() : base(PipeType.Activity)
        {
        }

        /// <summary>
        ///  具体执行扩展方法
        /// </summary>
        /// <returns>
        /// 处理结果
        /// False - 触发Block，业务流不再向后续管道传递。
        /// True  - 流体自动流入后续管道
        /// </returns>
        protected abstract Task<bool> Executing();

        internal override async Task<bool> InterHandling(EmptyContext context)
        {
            var res = await Executing();
            if (res)
                await ToNextThrough(context);
            
            return res;
        }
    }

    /// <summary>
    ///  主动触发执行活动组件基类
    ///    接收输入上下文，且此上下文继续传递下一个节点
    /// </summary>
    /// <typeparam name="TContext">输入输出上下文</typeparam>
    public abstract class BaseActivity<TContext> : BasePipe<TContext, TContext>
    {
        /// <summary>
        /// 外部Action活动基类
        /// </summary>
        protected BaseActivity() : base(PipeType.Activity)
        {
        }
        /// <summary>
        ///  具体执行扩展方法
        /// </summary>
        /// <param name="contextData">当前活动上下文（会继续传递给下一个节点）</param>
        /// <returns>
        /// 处理结果
        /// False - 触发Block，业务流不再向后续管道传递。
        /// True  - 流体自动流入后续管道
        /// </returns>
        protected abstract Task<bool> Executing(TContext contextData);

        internal override async Task<bool> InterHandling(TContext context)
        {
            var res = await Executing(context);
            if (res)
            {
                await ToNextThrough(context);
            }
            return res;
        }
    }
    


    /// <summary>
    ///  主动触发执行活动组件基类
    ///       不接收上下文，自身返回处理结果，且结果作为上下文传递给下一个节点
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class BaseEffectActivity<TResult> : BasePipe<EmptyContext, TResult>
    {
        /// <summary>
        /// 外部Action活动基类
        /// </summary>
        protected BaseEffectActivity() : base(PipeType.EffectActivity)
        {
        }

        internal override async Task<bool> InterHandling(EmptyContext context)
        {
            var (is_ok, result) = await Executing();
            if (is_ok)
            {
                await ToNextThrough(result);
            }

            return is_ok;
        }

        /// <summary>
        ///  具体执行扩展方法
        /// </summary>
        /// <returns>
        /// (bool is_ok,TResult result)-（活动是否处理成功，业务结果）
        /// is_ok：
        ///     False - 触发Block，业务流不再向后续管道传递。
        ///     True  - 流体自动流入后续管道
        /// </returns>
        protected abstract Task<(bool is_ok, TResult result)> Executing();
    }

    /// <summary>
    ///  主动触发执行活动组件基类
    ///       接收上下文，自身返回处理结果，且结果作为上下文传递给下一个节点
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class BaseEffectActivity<TContext, TResult> : BasePipe<TContext, TResult>
    {
        /// <summary>
        /// 外部Action活动基类
        /// </summary>
        protected BaseEffectActivity() : base(PipeType.EffectActivity)
        {
        }

        internal override async Task<bool> InterHandling(TContext context)
        {
            var (is_ok, result) = await Executing(context);
            if (is_ok)
                await ToNextThrough(result);
            
            return is_ok;
        }

        /// <summary>
        ///  具体执行扩展方法
        /// </summary>
        /// <param name="contextData">当前活动上下文信息</param>
        /// <returns>
        /// (bool is_ok,TResult result)-（活动是否处理成功，业务结果）
        /// is_ok：
        ///     False - 触发Block，业务流不再向后续管道传递。
        ///     True  - 流体自动流入后续管道
        /// </returns>
        protected abstract Task<(bool is_ok, TResult result)> Executing(TContext contextData);
    }
}