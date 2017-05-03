using Apps.BLL;
using Apps.BLL.Core;
using Apps.Common;
using Apps.Flow.IBLL;
using Apps.Flow.IDAL;
using Apps.Models;
using Apps.Models.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Apps.Flow.BLL
{
    public class Flow_FormBLL : BaseBLL, IFlow_FormBLL
    {
        [Dependency]
        public IFlow_FormRepository m_Rep { get; set; }

        public List<Flow_FormModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<Flow_Form> queryData = null;
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
        private List<Flow_FormModel> CreateModelList(ref IQueryable<Flow_Form> queryData)
        {

            List<Flow_FormModel> modelList = (from r in queryData
                                              select new Flow_FormModel
                                              {
                                                  AttrA = r.AttrA,
                                                  AttrB = r.AttrB,
                                                  AttrC = r.AttrC,
                                                  AttrD = r.AttrD,
                                                  AttrE = r.AttrE,
                                                  AttrF = r.AttrF,
                                                  AttrG = r.AttrG,
                                                  AttrH = r.AttrH,
                                                  AttrI = r.AttrI,
                                                  AttrJ = r.AttrJ,
                                                  AttrK = r.AttrK,
                                                  AttrL = r.AttrL,
                                                  AttrM = r.AttrM,
                                                  AttrN = r.AttrN,
                                                  AttrO = r.AttrO,
                                                  AttrP = r.AttrP,
                                                  AttrQ = r.AttrQ,
                                                  AttrR = r.AttrR,
                                                  AttrS = r.AttrS,
                                                  AttrT = r.AttrT,
                                                  AttrU = r.AttrU,
                                                  AttrV = r.AttrV,
                                                  AttrW = r.AttrW,
                                                  AttrX = r.AttrX,
                                                  AttrY = r.AttrY,
                                                  AttrZ = r.AttrZ,
                                                  CreateTime = r.CreateTime,
                                                  HtmlForm = r.HtmlForm,
                                                  Id = r.Id,
                                                  Name = r.Name,
                                                  Remark = r.Remark,
                                                  State = r.State,
                                                  TypeId = r.TypeId,
                                                  UsingDep = r.UsingDep
                                              }).ToList();
            return modelList;
        }
        public List<Flow_FormModel> GetListByTypeId(string typeId)
        {

            IQueryable<Flow_Form> queryData = null;
            if (!string.IsNullOrWhiteSpace(typeId))
            {
                queryData = m_Rep.GetList(db).Where(a => a.TypeId.Contains(typeId));
            }
            else
            {
                queryData = m_Rep.GetList(db);
            }

            return CreateModelList(ref queryData);
        }
        public bool Create(ref ValidationErrors errors, Flow_FormModel model)
        {
            try
            {
                Flow_Form entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new Flow_Form();
                entity.AttrA = model.AttrA;
                entity.AttrB = model.AttrB;
                entity.AttrC = model.AttrC;
                entity.AttrD = model.AttrD;
                entity.AttrE = model.AttrE;
                entity.AttrF = model.AttrF;
                entity.AttrG = model.AttrG;
                entity.AttrH = model.AttrH;
                entity.AttrI = model.AttrI;
                entity.AttrJ = model.AttrJ;
                entity.AttrK = model.AttrK;
                entity.AttrL = model.AttrL;
                entity.AttrM = model.AttrM;
                entity.AttrN = model.AttrN;
                entity.AttrO = model.AttrO;
                entity.AttrP = model.AttrP;
                entity.AttrQ = model.AttrQ;
                entity.AttrR = model.AttrR;
                entity.AttrS = model.AttrS;
                entity.AttrT = model.AttrT;
                entity.AttrU = model.AttrU;
                entity.AttrV = model.AttrV;
                entity.AttrW = model.AttrW;
                entity.AttrX = model.AttrX;
                entity.AttrY = model.AttrY;
                entity.AttrZ = model.AttrZ;
                entity.CreateTime = model.CreateTime;
                entity.HtmlForm = model.HtmlForm;
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.Remark = model.Remark;
                entity.State = model.State;
                entity.TypeId = model.TypeId;
                entity.UsingDep = model.UsingDep;
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
        public bool Edit(ref ValidationErrors errors, Flow_FormModel model)
        {
            try
            {
                Flow_Form entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.AttrA = model.AttrA;
                entity.AttrB = model.AttrB;
                entity.AttrC = model.AttrC;
                entity.AttrD = model.AttrD;
                entity.AttrE = model.AttrE;
                entity.AttrF = model.AttrF;
                entity.AttrG = model.AttrG;
                entity.AttrH = model.AttrH;
                entity.AttrI = model.AttrI;
                entity.AttrJ = model.AttrJ;
                entity.AttrK = model.AttrK;
                entity.AttrL = model.AttrL;
                entity.AttrM = model.AttrM;
                entity.AttrN = model.AttrN;
                entity.AttrO = model.AttrO;
                entity.AttrP = model.AttrP;
                entity.AttrQ = model.AttrQ;
                entity.AttrR = model.AttrR;
                entity.AttrS = model.AttrS;
                entity.AttrT = model.AttrT;
                entity.AttrU = model.AttrU;
                entity.AttrV = model.AttrV;
                entity.AttrW = model.AttrW;
                entity.AttrX = model.AttrX;
                entity.AttrY = model.AttrY;
                entity.AttrZ = model.AttrZ;
                entity.CreateTime = model.CreateTime;
                entity.HtmlForm = model.HtmlForm;
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.Remark = model.Remark;
                entity.State = model.State;
                entity.TypeId = model.TypeId;
                entity.UsingDep = model.UsingDep;

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
            if (db.Flow_Form.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }

        public Flow_FormModel GetById(string id)
        {
            if (IsExist(id))
            {
                Flow_Form entity = m_Rep.GetById(id);
                Flow_FormModel model = new Flow_FormModel();
                model.AttrA = entity.AttrA;
                model.AttrB = entity.AttrB;
                model.AttrC = entity.AttrC;
                model.AttrD = entity.AttrD;
                model.AttrE = entity.AttrE;
                model.AttrF = entity.AttrF;
                model.AttrG = entity.AttrG;
                model.AttrH = entity.AttrH;
                model.AttrI = entity.AttrI;
                model.AttrJ = entity.AttrJ;
                model.AttrK = entity.AttrK;
                model.AttrL = entity.AttrL;
                model.AttrM = entity.AttrM;
                model.AttrN = entity.AttrN;
                model.AttrO = entity.AttrO;
                model.AttrP = entity.AttrP;
                model.AttrQ = entity.AttrQ;
                model.AttrR = entity.AttrR;
                model.AttrS = entity.AttrS;
                model.AttrT = entity.AttrT;
                model.AttrU = entity.AttrU;
                model.AttrV = entity.AttrV;
                model.AttrW = entity.AttrW;
                model.AttrX = entity.AttrX;
                model.AttrY = entity.AttrY;
                model.AttrZ = entity.AttrZ;
                model.CreateTime = entity.CreateTime;
                model.HtmlForm = entity.HtmlForm;
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.Remark = entity.Remark;
                model.State = entity.State;
                model.TypeId = entity.TypeId;
                model.UsingDep = entity.UsingDep;
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
