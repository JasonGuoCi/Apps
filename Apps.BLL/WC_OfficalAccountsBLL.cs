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
    public class WC_OfficalAccountsBLL : BaseBLL, IWC_OfficalAccountsBLL
    {
        [Dependency]
        public IWC_OfficalAccountsRepository m_Rep { get; set; }

        public List<WC_OfficalAccountsModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<WC_OfficalAccounts> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(db).Where(a => a.OfficalName.Contains(queryStr) || a.Remark.Contains(queryStr));
            }
            else
            {
                queryData = m_Rep.GetList(db);
            }
            pager.totalRows = queryData.Count();
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }
        private List<WC_OfficalAccountsModel> CreateModelList(ref IQueryable<WC_OfficalAccounts> queryData)
        {

            List<WC_OfficalAccountsModel> modelList = (from r in queryData
                                                       select new WC_OfficalAccountsModel
                                                       {
                                                           AccessToken = r.AccessToken,
                                                           ApiUrl = r.ApiUrl,
                                                           AppId = r.AppId,
                                                           AppSecret = r.AppSecret,
                                                           Category = r.Category,
                                                           CreateBy = r.CreateBy,
                                                           CreateTime = r.CreateTime,
                                                           Enable = r.Enable,
                                                           Id = r.Id,
                                                           IsDefault = r.IsDefault,
                                                           ModifyBy = r.ModifyBy,
                                                           ModifyTime = r.ModifyTime,
                                                           OfficalCode = r.OfficalCode,
                                                           OfficalId = r.OfficalId,
                                                           OfficalKey = r.OfficalKey,
                                                           OfficalName = r.OfficalName,
                                                           OfficalPhoto = r.OfficalPhoto,
                                                           Remark = r.Remark,
                                                           Token = r.Token
                                                       }).ToList();
            return modelList;
        }

        public bool Create(ref ValidationErrors errors, WC_OfficalAccountsModel model)
        {
            try
            {
                WC_OfficalAccounts entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Suggestion.PrimaryRepeat);
                    return false;
                }
                entity = new WC_OfficalAccounts();
                entity.AccessToken = model.AccessToken;
                entity.ApiUrl = model.ApiUrl;
                entity.AppId = model.AppId;
                entity.AppSecret = model.AppSecret;
                entity.Category = model.Category;
                entity.CreateBy = model.CreateBy;
                entity.CreateTime = model.CreateTime;
                entity.Enable = model.Enable;
                entity.Id = model.Id;
                entity.IsDefault = model.IsDefault;
                entity.ModifyBy = model.ModifyBy;
                entity.ModifyTime = model.ModifyTime;
                entity.OfficalCode = model.OfficalCode;
                entity.OfficalId = model.OfficalId;
                entity.OfficalKey = model.OfficalKey;
                entity.OfficalName = model.OfficalName;
                entity.OfficalPhoto = model.OfficalPhoto;
                entity.Remark = model.Remark;
                entity.Token = model.Token;
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
        public bool Edit(ref ValidationErrors errors, WC_OfficalAccountsModel model)
        {
            try
            {
                WC_OfficalAccounts entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Suggestion.Disable);
                    return false;
                }
                entity.AccessToken = model.AccessToken;
                entity.ApiUrl = model.ApiUrl;
                entity.AppId = model.AppId;
                entity.AppSecret = model.AppSecret;
                entity.Category = model.Category;
                entity.CreateBy = model.CreateBy;
                entity.CreateTime = model.CreateTime;
                entity.Enable = model.Enable;
                entity.Id = model.Id;
                entity.IsDefault = model.IsDefault;
                entity.ModifyBy = model.ModifyBy;
                entity.ModifyTime = model.ModifyTime;
                entity.OfficalCode = model.OfficalCode;
                entity.OfficalId = model.OfficalId;
                entity.OfficalKey = model.OfficalKey;
                entity.OfficalName = model.OfficalName;
                entity.OfficalPhoto = model.OfficalPhoto;
                entity.Remark = model.Remark;
                entity.Token = model.Token;

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
            if (db.WC_OfficalAccounts.SingleOrDefault(a => a.Id == id) != null)
            {
                return true;
            }
            return false;
        }

        public WC_OfficalAccountsModel GetById(string id)
        {
            if (IsExist(id))
            {
                WC_OfficalAccounts entity = m_Rep.GetById(id);
                WC_OfficalAccountsModel model = new WC_OfficalAccountsModel();
                model.AccessToken = entity.AccessToken;
                model.ApiUrl = entity.ApiUrl;
                model.AppId = entity.AppId;
                model.AppSecret = entity.AppSecret;
                model.Category = entity.Category;
                model.CreateBy = entity.CreateBy;
                model.CreateTime = entity.CreateTime;
                model.Enable = entity.Enable;
                model.Id = entity.Id;
                model.IsDefault = entity.IsDefault;
                model.ModifyBy = entity.ModifyBy;
                model.ModifyTime = entity.ModifyTime;
                model.OfficalCode = entity.OfficalCode;
                model.OfficalId = entity.OfficalId;
                model.OfficalKey = entity.OfficalKey;
                model.OfficalName = entity.OfficalName;
                model.OfficalPhoto = entity.OfficalPhoto;
                model.Remark = entity.Remark;
                model.Token = entity.Token;
                return model;
            }
            else
            {
                return null;
            }
        }

        public WC_OfficalAccountsModel GetCurrentAccount()
        {
            WC_OfficalAccounts entity = m_Rep.GetCurrentAccount();
            if (entity == null)
            {
                return new WC_OfficalAccountsModel();
            }

            WC_OfficalAccountsModel model = new WC_OfficalAccountsModel();
            model.Id = entity.Id;
            model.OfficalName = entity.OfficalName;
            model.OfficalCode = entity.OfficalCode;
            model.OfficalPhoto = entity.OfficalPhoto;
            model.ApiUrl = entity.ApiUrl;
            model.Token = entity.Token;
            model.AppId = entity.AppId;
            model.AppSecret = entity.AppSecret;
            model.AccessToken = entity.AccessToken;
            model.Remark = entity.Remark;
            model.Enable = entity.Enable;
            model.IsDefault = entity.IsDefault;
            model.Category = entity.Category;
            model.CreateTime = entity.CreateTime;
            model.CreateBy = entity.CreateBy;
            model.ModifyTime = entity.ModifyTime;
            model.ModifyBy = entity.ModifyBy;
            return model;
        }

        public bool IsExist(string id)
        {
            return m_Rep.IsExist(id);
        }
    }
}
