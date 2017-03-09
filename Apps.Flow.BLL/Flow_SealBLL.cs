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
    public class Flow_SealBLL : BaseBLL, IFlow_SealBLL
    {
        [Dependency]
        public IFlow_SealRepository m_Rep { get; set; }

        public List<Flow_SealModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<Flow_Seal> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(db).Where(a => a.Using.Contains(queryStr) || a.Path.Contains(queryStr));
            }
            else
            {
                queryData = m_Rep.GetList(db);
            }
            pager.totalRows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }
        private List<Flow_SealModel> CreateModelList(ref IQueryable<Flow_Seal> queryData)
        {

            List<Flow_SealModel> modelList = (from r in queryData
                                              select new Flow_SealModel
                                              {
                                                  CreateTime = r.CreateTime,
                                                  Id = r.Id,
                                                  Path = r.Path,
                                                  Using = r.Using
                                              }).ToList();
            return modelList;
        }

        public bool Create(ref ValidationErrors errors, Flow_SealModel model)
        {
            try
            {
                Flow_Seal entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new Flow_Seal();
                entity.CreateTime = model.CreateTime;
                entity.Id = model.Id;
                entity.Path = model.Path;
                entity.Using = model.Using;
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
        public bool Edit(ref ValidationErrors errors, Flow_SealModel model)
        {
            try
            {
                Flow_Seal entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.CreateTime = model.CreateTime;
                entity.Id = model.Id;
                entity.Path = model.Path;
                entity.Using = model.Using;

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
            if (db.Flow_Seal.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }

        public Flow_SealModel GetById(string id)
        {
            if (IsExist(id))
            {
                Flow_Seal entity = m_Rep.GetById(id);
                Flow_SealModel model = new Flow_SealModel();
                model.CreateTime = entity.CreateTime;
                model.Id = entity.Id;
                model.Path = entity.Path;
                model.Using = entity.Using;
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
