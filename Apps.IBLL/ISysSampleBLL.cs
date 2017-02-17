using Apps.Common;
using Apps.Models;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IBLL
{
    public interface ISysSampleBLL
    {
        /// <summary>
        ///获取实体列表
        /// </summary>
        /// <param name="queryStr">搜索条件</param>
        /// <returns>实体列表</returns>
        List<SysSampleModel> GetList(ref GridPager pager, string queryStr);

        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns></returns>
        bool Create(ref ValidationErrors errors, SysSampleModel model);
        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        bool Delete(string id);

        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>true/false</returns>
        bool Edit(SysSampleModel model);

        /// <summary>
        /// 判断是否存在实体
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>true/false</returns>
        bool IsExist(string id);

        /// <summary>
        /// 根据ID获取一个实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>实体</returns>
        SysSampleModel GetById(string id);
    }
}
