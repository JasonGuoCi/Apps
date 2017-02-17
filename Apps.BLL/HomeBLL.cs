using Apps.IBLL;
using Apps.IDAL;
using Apps.Models;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.BLL
{
    public class HomeBLL : IHomeBLL
    {
        /// <summary>
        /// 获取menu根据persionID
        /// </summary>
        /// <param name="personId"></param>
        /// <returns>menu list</returns>
        /// 
        [Dependency]
        public IHomeRepository homeRepository { get; set; }
        //public List<SysModule> GetMenuByPersonId(string personId)
        //{
        //    return homeRepository.GetMenuByPersonId(personId);
        //}

        public List<SysModule> GetMenuByPersonId(string personId, string moduleId)
        {
            return homeRepository.GetMenuByPersonId(personId, moduleId);
        }
        //public List<SysModuleModel> GetMenuByPersonId2(string personId, string moduleId)
        //{
        //    IQueryable<SysModule> queryData = null;
        //    queryData = homeRepository.GetMenuByPersonId(personId, moduleId);
        //    return CreateModelList(ref queryData);
        //}

        //private List<SysModuleModel> CreateModelList(ref IQueryable<SysModule> queryData)
        //{
        //    //throw new NotImplementedException();

        //    List<SysModuleModel> modelList = (from r in queryData
        //                                      select new SysModuleModel
        //                                      {
        //                                          Id = r.Id,
        //                                          Name = r.Name,
        //                                          EnglishName = r.EnglishName,
        //                                          ParentId = r.ParentId,
        //                                          Url = r.Url,
        //                                          Iconic = r.Iconic,
        //                                          Sort = r.Sort,
        //                                          Remark = r.Remark,
        //                                          Enable = r.Enable,
        //                                          CreatePerson = r.CreatePerson,
        //                                          CreateTime = r.CreateTime,
        //                                          IsLast = r.IsLast
        //                                      }).ToList();
        //    return modelList;
        //}
    }
}
