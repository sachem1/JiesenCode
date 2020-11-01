﻿namespace Jiesen.Core.Domain.UOW
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// 提交请求
        /// </summary>
        void Commit();

        /// <summary>
        /// 提交所有请求并处理乐观锁的问题
        /// </summary>
        void CommitAndRefreshChanges();

        /// <summary>
        /// 回滚所有的请求
        /// </summary>
        void RollbackChanges();

    }
}