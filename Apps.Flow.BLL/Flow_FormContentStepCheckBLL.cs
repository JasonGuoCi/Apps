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
    public class Flow_FormContentStepCheckBLL : BaseBLL, IFlow_FormContentStepCheckBLL
    {
        [Dependency]
        public IFlow_FormContentStepCheckRepository m_Rep { get; set; }

        public List<Flow_FormContentStepCheckModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<Flow_FormContentStepCheck> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(db).Where(a => a.ContentId.Contains(queryStr) || a.StepId.Contains(queryStr));
            }
            else
            {
                queryData = m_Rep.GetList(db);
            }
            pager.totalRows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }

        private List<Flow_FormContentStepCheckModel> CreateModelList(ref IQueryable<Flow_FormContentStepCheck> queryData)
        {
            List<Flow_FormContentStepCheckModel> modelList = (from r in queryData
                                                              select new Flow_FormContentStepCheckModel
                                                              {
                                                                  ContentId = r.ContentId,
                                                                  CreateTime = r.CreateTime,
                                                                  Id = r.Id,
                                                                  IsEnd = r.IsEnd,
                                                                  State = r.State,
                                                                  StateFlag = r.StateFlag,
                                                                  StepId = r.StepId
                                                              }).ToList();
            return modelList;
        }

        public bool Create(ref ValidationErrors errors, Flow_FormContentStepCheckModel model)
        {
            try
            {
                Flow_FormContentStepCheck entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new Flow_FormContentStepCheck();
                entity.ContentId = model.ContentId;
                entity.CreateTime = model.CreateTime;
                entity.Id = model.Id;
                entity.IsEnd = model.IsEnd;
                entity.State = model.State;
                entity.StateFlag = model.StateFlag;
                entity.StepId = model.StepId;
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

        public bool Edit(ref ValidationErrors errors, Flow_FormContentStepCheckModel model)
        {
            try
            {
                Flow_FormContentStepCheck entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.ContentId = model.ContentId;
                entity.CreateTime = model.CreateTime;
                entity.Id = model.Id;
                entity.IsEnd = model.IsEnd;
                entity.State = model.State;
                entity.StateFlag = model.StateFlag;
                entity.StepId = model.StepId;

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
            if (db.Flow_FormContentStepCheck.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }
        public Flow_FormContentStepCheckModel GetById(string id)
        {
            if (IsExist(id))
            {
                Flow_FormContentStepCheck entity = m_Rep.GetById(id);
                Flow_FormContentStepCheckModel model = new Flow_FormContentStepCheckModel();
                model.ContentId = entity.ContentId;
                model.CreateTime = entity.CreateTime;
                model.Id = entity.Id;
                model.IsEnd = entity.IsEnd;
                model.State = entity.State;
                model.StateFlag = entity.StateFlag;
                model.StepId = entity.StepId;
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
