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
    public class Flow_FormAttrBLL : BaseBLL, IFlow_FormAttrBLL
    {
        [Dependency]
        public IFlow_FormAttrRepository m_Rep { get; set; }

        public List<Flow_FormAttrModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<Flow_FormAttr> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(db).Where(a => a.Name.Contains(queryStr) || a.AttrType.Contains(queryStr));
            }
            else
            {
                queryData = m_Rep.GetList(db);
            }
            pager.totalRows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }

        private List<Flow_FormAttrModel> CreateModelList(ref IQueryable<Flow_FormAttr> queryData)
        {
            List<Flow_FormAttrModel> modelList = (from r in queryData
                                                  select new Flow_FormAttrModel
                                                  {
                                                      AttrType = r.AttrType,
                                                      CheckJS = r.CheckJS,
                                                      CreateTime = r.CreateTime,
                                                      Id = r.Id,
                                                      IsValid = (bool)r.IsValid,
                                                      Name = r.Name,
                                                      OptionList = r.OptionList,
                                                      Title = r.Title,
                                                      TypeId = r.TypeId
                                                  }).ToList();
            return modelList;
        }

        public bool Create(ref ValidationErrors errors, Flow_FormAttrModel model)
        {
            try
            {
                Flow_FormAttr entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new Flow_FormAttr();
                entity.AttrType = model.AttrType;
                entity.CheckJS = model.CheckJS;
                entity.CreateTime = model.CreateTime;
                entity.Id = model.Id;
                entity.IsValid = model.IsValid;
                entity.Name = model.Name;
                entity.OptionList = model.OptionList;
                entity.Title = model.Title;
                entity.TypeId = model.TypeId;
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

        public bool Edit(ref ValidationErrors errors, Flow_FormAttrModel model)
        {
            try
            {
                Flow_FormAttr entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.AttrType = model.AttrType;
                entity.CheckJS = model.CheckJS;
                entity.CreateTime = model.CreateTime;
                entity.Id = model.Id;
                entity.IsValid = model.IsValid;
                entity.Name = model.Name;
                entity.OptionList = model.OptionList;
                entity.Title = model.Title;
                entity.TypeId = model.TypeId;

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
            if (db.Flow_FormAttr.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }
        public Flow_FormAttrModel GetById(string id)
        {
            if (IsExist(id))
            {
                Flow_FormAttr entity = m_Rep.GetById(id);
                Flow_FormAttrModel model = new Flow_FormAttrModel();
                model.AttrType = entity.AttrType;
                model.CheckJS = entity.CheckJS;
                model.CreateTime = entity.CreateTime;
                model.Id = entity.Id;
                model.IsValid = (bool)entity.IsValid;
                model.Name = entity.Name;
                model.OptionList = entity.OptionList;
                model.Title = entity.Title;
                model.TypeId = entity.TypeId;
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
