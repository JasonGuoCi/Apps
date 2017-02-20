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
using System.Transactions;

namespace Apps.BLL
{
    public class SysStructBLL : BaseBLL, ISysStructBLL
    {
        [Dependency]
        public ISysStructRepository m_Rep { get; set; }

        public List<SysStructModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<SysStruct> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(db).Where(a => a.Name.Contains(queryStr) || a.Remark.Contains(queryStr));
            }
            else
            {
                queryData = m_Rep.GetList(db);
            }
            pager.totalRows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }

        private List<SysStructModel> CreateModelList(ref IQueryable<SysStruct> queryData)
        {
            List<SysStructModel> modelList = (from r in queryData
                                              select new SysStructModel
                                              {
                                                  CreateTime = r.CreateTime,
                                                  Enable = r.Enable,
                                                  Higher = r.Higher,
                                                  Id = r.Id,
                                                  Name = r.Name,
                                                  ParentId = r.ParentId,
                                                  Remark = r.Remark,
                                                  Sort = r.Sort
                                              }).ToList();
            return modelList;
        }

        public bool Create(ref ValidationErrors errors, SysStructModel model)
        {
            try
            {
                SysStruct entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new SysStruct();
                entity.CreateTime = model.CreateTime;
                entity.Enable = model.Enable;
                entity.Higher = model.Higher;
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.ParentId = model.ParentId;
                entity.Remark = model.Remark;
                entity.Sort = model.Sort;
                if (m_Rep.Create(entity) == 1)
                {
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
            }
        }

        public bool Delete(ref ValidationErrors errors, string id)
        {
            try
            {
                if (m_Rep.Delete(id) == 1)
                {
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
            }
        }

        public bool Delete(ref ValidationErrors errors, string[] deleteCollection)
        {
            try
            {
                if (deleteCollection != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        m_Rep.Delete(db, deleteCollection);
                        if (db.SaveChanges() == deleteCollection.Length)
                        {
                            transactionScope.Complete();
                            return true;
                        }
                        else
                        {
                            Transaction.Current.Rollback();
                            return false;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public bool Edit(ref ValidationErrors errors, SysStructModel model)
        {
            try
            {
                SysStruct entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.CreateTime = model.CreateTime;
                entity.Enable = model.Enable;
                entity.Higher = model.Higher;
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.ParentId = model.ParentId;
                entity.Remark = model.Remark;
                entity.Sort = model.Sort;

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

        public bool IsExists(string id)
        {
            if (db.SysStruct.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }

        public SysStructModel GetById(string id)
        {
            if (IsExist(id))
            {
                SysStruct entity = m_Rep.GetById(id);
                SysStructModel model = new SysStructModel();
                model.CreateTime = entity.CreateTime;
                model.Enable = entity.Enable;
                model.Higher = entity.Higher;
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.ParentId = entity.ParentId;
                model.Remark = entity.Remark;
                model.Sort = entity.Sort;
                return model;
            }
            else
            {
                return null;
            }
        }

        public bool IsExist(string id)
        {
            return m_Rep.IsExist(id);
        }
    }
}
