using Apps.BLL;
using Apps.DAL;
using Apps.Flow.BLL;
using Apps.Flow.DAL;
using Apps.Flow.IBLL;
using Apps.Flow.IDAL;
using Apps.IBLL;
using Apps.IDAL;
using Apps.MIS.BLL;
using Apps.MIS.DAL;
using Apps.MIS.IBLL;
using Apps.MIS.IDAL;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Core
{

    public class DependencyRegisterType
    {
        //系统注入
        public static void Container_Sys(ref UnityContainer container)
        {
            container.RegisterType<ISysSampleBLL, SysSampleBLL>();//样例
            container.RegisterType<ISysSampleRepository, SysSampleRepository>();

            container.RegisterType<IHomeBLL, HomeBLL>();
            container.RegisterType<IHomeRepository, HomeRepository>();

            container.RegisterType<ISysLogBLL, SysLogBLL>();
            container.RegisterType<ISysLogRepository, SysLogRepository>();

            container.RegisterType<ISysExceptionBLL, SysExceptionBLL>();
            container.RegisterType<ISysExceptionRepository, SysExceptionRepository>();

            container.RegisterType<IAccountBLL, AccountBLL>();
            container.RegisterType<IAccountRepository, AccountRepository>();

            container.RegisterType<ISysUserBLL, SysUserBLL>();
            container.RegisterType<ISysRightRepository, SysRightRepository>();

            container.RegisterType<ISysModuleBLL, SysModuleBLL>();
            container.RegisterType<ISysModuleRepository, SysModuleRepository>();

            container.RegisterType<ISysModuleOperateBLL, SysModuleOperateBLL>();
            container.RegisterType<ISysModuleOperateRepository, SysModuleOperateRepository>();

            container.RegisterType<ISysRoleBLL, SysRoleBLL>();
            container.RegisterType<ISysRoleRepository, SysRoleRepository>();

            container.RegisterType<ISysRightBLL, SysRightBLL>();
            container.RegisterType<ISysRightRepository, SysRightRepository>();

            container.RegisterType<ISysUserBLL, SysUserBLL>();
            container.RegisterType<ISysUserRepository, SysUserRepository>();

            container.RegisterType<IMIS_ArticleBLL, MIS_ArticleBLL>();
            container.RegisterType<IMIS_ArticleRepository, MIS_ArticleRepository>();

            container.RegisterType<IMIS_Article_CategoryBLL, MIS_Article_CategoryBLL>();
            container.RegisterType<IMIS_Article_CategoryRepository, MIS_Article_CategoryRepository>();


            container.RegisterType<ISysStructBLL, SysStructBLL>();
            container.RegisterType<ISysStructRepository, SysStructRepository>();

            container.RegisterType<IFlow_TypeBLL, Flow_TypeBLL>();
            container.RegisterType<IFlow_TypeRepository, Flow_TypeRepository>();

            container.RegisterType<IFlow_StepRuleBLL, Flow_StepRuleBLL>();
            container.RegisterType<IFlow_StepRuleRepository, Flow_StepRuleRepository>();

            container.RegisterType<IFlow_StepBLL, Flow_StepBLL>();
            container.RegisterType<IFlow_StepRepository, Flow_StepRepository>();

            container.RegisterType<IFlow_SealBLL, Flow_SealBLL>();
            container.RegisterType<IFlow_SealRepository, Flow_SealRepository>();

            container.RegisterType<IFlow_FormContentStepCheckStateBLL, Flow_FormContentStepCheckStateBLL>();
            container.RegisterType<IFlow_FormContentStepCheckStateRepository, Flow_FormContentStepCheckStateRepository>();

            container.RegisterType<IFlow_FormContentStepCheckBLL, Flow_FormContentStepCheckBLL>();
            container.RegisterType<IFlow_FormContentStepCheckRepository, Flow_FormContentStepCheckRepository>();

            container.RegisterType<IFlow_FormContentBLL, Flow_FormContentBLL>();
            container.RegisterType<IFlow_FormContentRepository, Flow_FormContentRepository>();

            container.RegisterType<IFlow_FormAttrBLL, Flow_FormAttrBLL>();
            container.RegisterType<IFlow_FormAttrRepository, Flow_FormAttrRepository>();

            container.RegisterType<IFlow_FormBLL, Flow_FormBLL>();
            container.RegisterType<IFlow_FormRepository, Flow_FormRepository>();
        }
    }
}
