using Apps.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Apps.Models.WeChat.WC_MessageResponseModel;

namespace Apps.DAL
{
    public class WC_MessageResponseRepository : IWC_MessageResponseRepository, IDisposable
    {
        public IQueryable<WC_MessageResponse> GetList(AppsDBEntities db)
        {
            IQueryable<WC_MessageResponse> list = db.WC_MessageResponse.AsQueryable();
            return list;
        }

        public int Create(WC_MessageResponse entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.WC_MessageResponse.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                WC_MessageResponse entity = db.WC_MessageResponse.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.WC_MessageResponse.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<WC_MessageResponse> collection = from f in db.WC_MessageResponse
                                                        where deleteCollection.Contains(f.Id)
                                                        select f;
            foreach (var deleteItem in collection)
            {
                db.WC_MessageResponse.Remove(deleteItem);
            }
        }

        public int Edit(WC_MessageResponse entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.WC_MessageResponse.Attach(entity);
                db.Entry<WC_MessageResponse>(entity).State = EntityState.Modified;
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                return db.SaveChanges();
            }
        }

        public WC_MessageResponse GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.WC_MessageResponse.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                WC_MessageResponse entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }
        public void Dispose()
        {

        }

        public bool PostData(WC_MessageResponse model)
        {
            //如果所有开关都关掉，证明不启用回复
            if (model.Category == null)
            {
                return true;
            }

            //全部设置为不默认
            ExecuteSqlCommand(string.Format("Update [dbo].[WC_MessageResponse] set IsDefault=0 where OfficalAccountId='{0}' and MessageRule={1}", model.OfficalAccountId, model.MessageRule));

            //默认回复和订阅回复，且不是图文另外处理，因为他们有三种模式，但是只有一个是默认的
            if (model.Category != (int)WeChatReplyCategory.Image && (model.MessageRule == (int)WeChatRequestRuleEnum.Default || model.MessageRule == (int)WeChatRequestRuleEnum.Subscriber))
            {
                //查看数据库是否存在数据
                using (AppsDBEntities db = new AppsDBEntities())
                {
                    var entity = db.WC_MessageResponse.Where(p => p.OfficalAccountId == model.OfficalAccountId && p.MessageRule == model.MessageRule && p.Category == model.Category).FirstOrDefault();
                    if (entity != null)
                    {
                        //删除原来的
                        db.WC_MessageResponse.Remove(entity);
                    }
                }
            }
            //全部设置为默认
            ExecuteSqlCommand(string.Format("Update [dbo].[WC_MessageResponse] set IsDefault=1 where OfficalAccountId='{0}' and MessageRule={1} and Category={2}", model.OfficalAccountId, model.MessageRule, model.Category));
            //修改
            if (IsExist(model.Id))
            {
                using (AppsDBEntities db = new AppsDBEntities())
                {
                    db.Entry<WC_MessageResponse>(model).State = EntityState.Modified;
                    return Edit(model) == 1 ? true : false;
                }
            }
            else
            {
                return Create(model) == 1 ? true : false;
            }

        }

        /// <summary>
        /// 执行一条SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteSqlCommand(string sql)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.Database.ExecuteSqlCommand(sql);
            }
        }
    }
}
