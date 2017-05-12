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
    public class Flow_FormContentStepCheckStateBLL : BaseBLL, IFlow_FormContentStepCheckStateBLL
    {
        [Dependency]
        public IFlow_FormContentStepCheckStateRepository m_Rep { get; set; }

        public List<Flow_FormContentStepCheckStateModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<Flow_FormContentStepCheckState> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(db).Where(a => a.StepCheckId.Contains(queryStr) || a.UserId.Contains(queryStr));
            }
            else
            {
                queryData = m_Rep.GetList(db);
            }
            pager.totalRows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }

        private List<Flow_FormContentStepCheckStateModel> CreateModelList(ref IQueryable<Flow_FormContentStepCheckState> queryData)
        {
            List<Flow_FormContentStepCheckStateModel> modelList = (from r in queryData
                                                                   select new Flow_FormContentStepCheckStateModel
                                                                   {
                                                                       CheckFlag = r.CheckFlag,
                                                                       CreateTime = r.CreateTime,
                                                                       Id = r.Id,
                                                                       Reamrk = r.Reamrk,
                                                                       StepCheckId = r.StepCheckId,
                                                                       TheSeal = r.TheSeal,
                                                                       UserId = r.UserId
                                                                   }).ToList();
            return modelList;
        }

        public bool Create(ref ValidationErrors errors, Flow_FormContentStepCheckStateModel model)
        {
            try
            {
                Flow_FormContentStepCheckState entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new Flow_FormContentStepCheckState();
                entity.CheckFlag = model.CheckFlag;
                entity.CreateTime = model.CreateTime;
                entity.Id = model.Id;
                entity.Reamrk = model.Reamrk;
                entity.StepCheckId = model.StepCheckId;
                entity.TheSeal = model.TheSeal;
                entity.UserId = model.UserId;
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
        public bool Edit(ref ValidationErrors errors, Flow_FormContentStepCheckStateModel model)
        {
            try
            {
                Flow_FormContentStepCheckState entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.CheckFlag = model.CheckFlag;
                entity.CreateTime = model.CreateTime;
                entity.Id = model.Id;
                entity.Reamrk = model.Reamrk;
                entity.StepCheckId = model.StepCheckId;
                entity.TheSeal = model.TheSeal;
                entity.UserId = model.UserId;

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
            if (db.Flow_FormContentStepCheckState.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }

        public Flow_FormContentStepCheckStateModel GetById(string id)
        {
            if (IsExist(id))
            {
                Flow_FormContentStepCheckState entity = m_Rep.GetById(id);
                Flow_FormContentStepCheckStateModel model = new Flow_FormContentStepCheckStateModel();
                model.CheckFlag = entity.CheckFlag;
                model.CreateTime = entity.CreateTime;
                model.Id = entity.Id;
                model.Reamrk = entity.Reamrk;
                model.StepCheckId = entity.StepCheckId;
                model.TheSeal = entity.TheSeal;
                model.UserId = entity.UserId;
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

        public Flow_FormContentStepCheckStateModel GetByStepCheckId(string id)
        {
            throw new NotImplementedException();
        }
    }
}
