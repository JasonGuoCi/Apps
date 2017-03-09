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
    public class Flow_StepRuleBLL : BaseBLL, IFlow_StepRuleBLL
    {
        [Dependency]
        public IFlow_StepRuleRepository m_Rep { get; set; }

        public List<Flow_StepRuleModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<Flow_StepRule> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(db).Where(a => a.StepId.Contains(queryStr) || a.Operator.Contains(queryStr));
            }
            else
            {
                queryData = m_Rep.GetList(db);
            }
            pager.totalRows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }
        private List<Flow_StepRuleModel> CreateModelList(ref IQueryable<Flow_StepRule> queryData)
        {

            List<Flow_StepRuleModel> modelList = (from r in queryData
                                                  select new Flow_StepRuleModel
                                                  {
                                                      AttrId = r.AttrId,
                                                      Id = r.Id,
                                                      NextStep = r.NextStep,
                                                      Operator = r.Operator,
                                                      Result = r.Result,
                                                      StepId = r.StepId
                                                  }).ToList();
            return modelList;
        }

        public bool Create(ref ValidationErrors errors, Flow_StepRuleModel model)
        {
            try
            {
                Flow_StepRule entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new Flow_StepRule();
                entity.AttrId = model.AttrId;
                entity.Id = model.Id;
                entity.NextStep = model.NextStep;
                entity.Operator = model.Operator;
                entity.Result = model.Result;
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
        public bool Edit(ref ValidationErrors errors, Flow_StepRuleModel model)
        {
            try
            {
                Flow_StepRule entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.AttrId = model.AttrId;
                entity.Id = model.Id;
                entity.NextStep = model.NextStep;
                entity.Operator = model.Operator;
                entity.Result = model.Result;
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
            if (db.Flow_StepRule.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }

        public Flow_StepRuleModel GetById(string id)
        {
            if (IsExist(id))
            {
                Flow_StepRule entity = m_Rep.GetById(id);
                Flow_StepRuleModel model = new Flow_StepRuleModel();
                model.AttrId = entity.AttrId;
                model.Id = entity.Id;
                model.NextStep = entity.NextStep;
                model.Operator = entity.Operator;
                model.Result = entity.Result;
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
