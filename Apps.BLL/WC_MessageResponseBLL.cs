using Apps.BLL.Core;
using Apps.Common;
using Apps.IBLL;
using Apps.IDAL;
using Apps.Models;
using Apps.Models.WeChat;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Apps.BLL
{
    public class WC_MessageResponseBLL : BaseBLL, IWC_MessageResponseBLL
    {
        [Dependency]
        public IWC_MessageResponseRepository m_Rep { get; set; }

        public List<WC_MessageResponseModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<WC_MessageResponse> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(db).Where(a => a.Remark.Contains(queryStr) || a.TextContent.Contains(queryStr));
            }
            else
            {
                queryData = m_Rep.GetList(db);
            }
            pager.totalRows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }
        private List<WC_MessageResponseModel> CreateModelList(ref IQueryable<WC_MessageResponse> queryData)
        {

            List<WC_MessageResponseModel> modelList = (from r in queryData
                                                       select new WC_MessageResponseModel
                                                       {
                                                           Category = r.Category,
                                                           CreateBy = r.CreateBy,
                                                           CreateTime = r.CreateTime,
                                                           Enable = r.Enable,
                                                           Id = r.Id,
                                                           ImgTextContext = r.ImgTextContext,
                                                           ImgTextLink = r.ImgTextLink,
                                                           ImgTextUrl = r.ImgTextUrl,
                                                           IsDefault = r.IsDefault,
                                                           MatchKey = r.MatchKey,
                                                           MeidaLink = r.MeidaLink,
                                                           MeidaUrl = r.MeidaUrl,
                                                           MessageRule = r.MessageRule,
                                                           ModifyBy = r.ModifyBy,
                                                           ModifyTime = r.ModifyTime,
                                                           OfficalAccountId = r.OfficalAccountId,
                                                           Remark = r.Remark,
                                                           Sort = r.Sort,
                                                           TextContent = r.TextContent
                                                       }).ToList();
            return modelList;
        }

        public bool Create(ref ValidationErrors errors, WC_MessageResponseModel model)
        {
            try
            {
                WC_MessageResponse entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new WC_MessageResponse();
                entity.Category = model.Category;
                entity.CreateBy = model.CreateBy;
                entity.CreateTime = model.CreateTime;
                entity.Enable = model.Enable;
                entity.Id = model.Id;
                entity.ImgTextContext = model.ImgTextContext;
                entity.ImgTextLink = model.ImgTextLink;
                entity.ImgTextUrl = model.ImgTextUrl;
                entity.IsDefault = model.IsDefault;
                entity.MatchKey = model.MatchKey;
                entity.MeidaLink = model.MeidaLink;
                entity.MeidaUrl = model.MeidaUrl;
                entity.MessageRule = model.MessageRule;
                entity.ModifyBy = model.ModifyBy;
                entity.ModifyTime = model.ModifyTime;
                entity.OfficalAccountId = model.OfficalAccountId;
                entity.Remark = model.Remark;
                entity.Sort = model.Sort;
                entity.TextContent = model.TextContent;
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
        public bool Edit(ref ValidationErrors errors, WC_MessageResponseModel model)
        {
            try
            {
                WC_MessageResponse entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.Category = model.Category;
                entity.CreateBy = model.CreateBy;
                entity.CreateTime = model.CreateTime;
                entity.Enable = model.Enable;
                entity.Id = model.Id;
                entity.ImgTextContext = model.ImgTextContext;
                entity.ImgTextLink = model.ImgTextLink;
                entity.ImgTextUrl = model.ImgTextUrl;
                entity.IsDefault = model.IsDefault;
                entity.MatchKey = model.MatchKey;
                entity.MeidaLink = model.MeidaLink;
                entity.MeidaUrl = model.MeidaUrl;
                entity.MessageRule = model.MessageRule;
                entity.ModifyBy = model.ModifyBy;
                entity.ModifyTime = model.ModifyTime;
                entity.OfficalAccountId = model.OfficalAccountId;
                entity.Remark = model.Remark;
                entity.Sort = model.Sort;
                entity.TextContent = model.TextContent;

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
            if (db.WC_MessageResponse.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }

        public WC_MessageResponseModel GetById(string id)
        {
            if (IsExist(id))
            {
                WC_MessageResponse entity = m_Rep.GetById(id);
                WC_MessageResponseModel model = new WC_MessageResponseModel();
                model.Category = entity.Category;
                model.CreateBy = entity.CreateBy;
                model.CreateTime = entity.CreateTime;
                model.Enable = entity.Enable;
                model.Id = entity.Id;
                model.ImgTextContext = entity.ImgTextContext;
                model.ImgTextLink = entity.ImgTextLink;
                model.ImgTextUrl = entity.ImgTextUrl;
                model.IsDefault = entity.IsDefault;
                model.MatchKey = entity.MatchKey;
                model.MeidaLink = entity.MeidaLink;
                model.MeidaUrl = entity.MeidaUrl;
                model.MessageRule = entity.MessageRule;
                model.ModifyBy = entity.ModifyBy;
                model.ModifyTime = entity.ModifyTime;
                model.OfficalAccountId = entity.OfficalAccountId;
                model.Remark = entity.Remark;
                model.Sort = entity.Sort;
                model.TextContent = entity.TextContent;
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

        public bool PostData(ref ValidationErrors errors, WC_MessageResponseModel model)
        {
            try
            {
                WC_MessageResponse entity = new WC_MessageResponse();
                if (IsExists(model.Id))
                {
                    entity = m_Rep.GetById(model.Id);
                }

                entity.Id = model.Id;
                entity.OfficalAccountId = model.OfficalAccountId;
                entity.MessageRule = model.MessageRule;
                entity.Category = model.Category;
                entity.MatchKey = model.MatchKey;
                entity.TextContent = model.TextContent;
                entity.ImgTextContext = model.ImgTextContext;
                entity.ImgTextUrl = model.ImgTextUrl;
                entity.ImgTextLink = model.ImgTextLink;
                entity.MeidaUrl = model.MeidaUrl;
                entity.Enable = model.Enable;
                entity.IsDefault = model.IsDefault;
                entity.Remark = model.Remark;
                entity.CreateBy = model.CreateBy;
                entity.CreateTime = model.CreateTime;
                entity.Sort = model.Sort;
                entity.ModifyBy = model.ModifyBy;
                entity.ModifyTime = model.ModifyTime;
                if (m_Rep.PostData(entity))
                {
                    return true;
                }
                else
                {
                    //errors.Add(Resource.NoDataChange);
                    errors.Add("No Data Change");
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
    }
}
