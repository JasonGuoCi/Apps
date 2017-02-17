using Apps.BLL.Core;
using Apps.Common;
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
    public class SysModuleBLL : BaseBLL, ISysModuleBLL
    {
        [Dependency]
        public ISysModuleRepository m_Rep { get; set; }
        //AppsDBEntities db = new AppsDBEntities();
        public List<SysModuleModel> GetList(string parentId)
        {
            IQueryable<SysModule> queryData = null;
            queryData = m_Rep.GetList(db).Where(a => a.ParentId == parentId).OrderBy(a => a.Sort);
            return CreateModelList(ref queryData);
        }

        private List<SysModuleModel> CreateModelList(ref IQueryable<SysModule> queryData)
        {
            //throw new NotImplementedException();

            List<SysModuleModel> modelList = (from r in queryData
                                              select new SysModuleModel
                                              {
                                                  Id = r.Id,
                                                  Name = r.Name,
                                                  EnglishName = r.EnglishName,
                                                  ParentId = r.ParentId,
                                                  Url = r.Url,
                                                  Iconic = r.Iconic,
                                                  Sort = r.Sort,
                                                  Remark = r.Remark,
                                                  Enable = r.Enable,
                                                  CreatePerson = r.CreatePerson,
                                                  CreateTime = r.CreateTime,
                                                  IsLast = r.IsLast
                                              }).ToList();
            return modelList;
        }

        public List<SysModule> GetModuleBySystem(string parentId)
        {
            return m_Rep.GetModuleBySystem(db, parentId).ToList();
        }

        public bool Create(ref ValidationErrors errors, SysModuleModel model)
        {
            try
            {
                SysModule entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }

                entity = new SysModule();
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.EnglishName = model.EnglishName;
                entity.ParentId = model.ParentId;
                entity.Url = model.Url;
                entity.Iconic = model.Iconic;
                entity.Sort = model.Sort;
                entity.Remark = model.Remark;
                entity.Enable = model.Enable;
                entity.CreatePerson = model.CreatePerson;
                entity.CreateTime = model.CreateTime;
                entity.IsLast = model.IsLast;

                if (m_Rep.Create(entity) == 1)
                {
                    db.P_Sys_InsertSysRight();
                    return true;
                }
                else
                {
                    errors.Add(Suggestion.InsertFail);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
                //throw;
            }
        }

        public bool Delete(ref ValidationErrors errors, string id)
        {
            try
            {
                //检查是否有下级
                if (db.SysModule.AsQueryable().Where(a => a.SysModule2.Id == id).Count() > 0)
                {
                    errors.Add("有下属关联，请先删除下属！");
                    return false;
                }
                m_Rep.Delete(db, id);
                if (db.SaveChanges() > 0)
                {
                    //清理无用的项
                    db.P_Sys_ClearUnusedRightOperate();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
                //throw;
            }
        }

        public bool Edit(ref ValidationErrors errors, SysModuleModel model)
        {
            try
            {
                SysModule entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.Name = model.Name;
                entity.EnglishName = model.EnglishName;
                entity.ParentId = model.ParentId;
                entity.Url = model.Url;
                entity.Iconic = model.Iconic;
                entity.Sort = model.Sort;
                entity.Remark = model.Remark;
                entity.Enable = model.Enable;
                entity.IsLast = model.IsLast;
                if (m_Rep.Edit(entity) == 1)
                {
                    return true;
                }
                else
                {
                    errors.Add(Suggestion.EditFail);
                    return false;
                }
            }
            catch (Exception ex)
            {

                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public SysModuleModel GetById(string id)
        {
            if (IsExist(id))
            {
                SysModule entity = m_Rep.GetById(id);
                SysModuleModel model = new SysModuleModel();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.EnglishName = entity.EnglishName;
                model.ParentId = entity.ParentId;
                model.Url = entity.Url;
                model.Iconic = entity.Iconic;
                model.Sort = entity.Sort;
                model.Remark = entity.Remark;
                model.Enable = entity.Enable;
                model.CreatePerson = entity.CreatePerson;
                model.CreateTime = entity.CreateTime;
                model.IsLast = entity.IsLast;
                return model;
            }
            else
            {
                return null;
            }
        }

        public bool IsExist(string id)
        {
            return m_Rep.IsExists(id);
        }
    }
}
