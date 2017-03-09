using Apps.BLL;
using Apps.BLL.Core;
using Apps.Common;
using Apps.Flow.IBLL;
using Apps.Flow.IDAL;
using Apps.Models;
using Apps.Models.Flow;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Apps.Flow.BLL
{
    public class Flow_TypeBLL : BaseBLL, IFlow_TypeBLL
    {
        [Dependency]
        public IFlow_TypeRepository m_Rep { get; set; }

        public List<Flow_TypeModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<Flow_Type> queryData = null;
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
        private List<Flow_TypeModel> CreateModelList(ref IQueryable<Flow_Type> queryData)
        {

            List<Flow_TypeModel> modelList = (from r in queryData
                                              select new Flow_TypeModel
                                              {
                                                  CreateTime = r.CreateTime,
                                                  Id = r.Id,
                                                  Name = r.Name,
                                                  Remark = r.Remark,
                                                  Sort = r.Sort
                                              }).ToList();
            return modelList;
        }

        public bool Create(ref ValidationErrors errors, Flow_TypeModel model)
        {
            try
            {
                Flow_Type entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new Flow_Type();
                entity.CreateTime = model.CreateTime;
                entity.Id = model.Id;
                entity.Name = model.Name;
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
        public bool Edit(ref ValidationErrors errors, Flow_TypeModel model)
        {
            try
            {
                Flow_Type entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.CreateTime = model.CreateTime;
                entity.Id = model.Id;
                entity.Name = model.Name;
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
            if (db.Flow_Type.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }

        public Flow_TypeModel GetById(string id)
        {
            if (IsExist(id))
            {
                Flow_Type entity = m_Rep.GetById(id);
                Flow_TypeModel model = new Flow_TypeModel();
                model.CreateTime = entity.CreateTime;
                model.Id = entity.Id;
                model.Name = entity.Name;
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
