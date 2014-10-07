using System;
using System.Collections.Generic;
using BA.UnityRegistry;
using BA.Web.Common.Helper;
using Framework.UoW;
using SubjectEngine.Component;
using SubjectEngine.Component.Dto;

namespace BA.Web.Common.Subject
{
    public partial class SubjectMgt : AdminSetupBasePage
    {
        public const string PageUrl = "/Common/Subject/SubjectMgt.aspx";

        private const string InstancesStateKey = "InstancesSessionKey";
        private string UniqueInstancesStateKey { get { return string.Format("{0}_{1}", UniqueID, InstancesStateKey); } }

        private IEnumerable<SubjectDto> _currentInstances;
        public IEnumerable<SubjectDto> CurrentInstances
        {
            get
            {
                if (_currentInstances == null && IsInSession(UniqueInstancesStateKey))
                {
                    _currentInstances = GetFromSession(UniqueInstancesStateKey) as IEnumerable<SubjectDto>;
                }

                return _currentInstances;
            }
            set
            {
                _currentInstances = value;
                SaveInSession(_currentInstances, UniqueInstancesStateKey);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = "Subject";

            InitSubjectList();

            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void InitSubjectList()
        {
            ucListManager.ID = "SubjectList";
            ucListManager.IsChildList = false;
            ucListManager.ListLabel = "Subject List";
            ucListManager.AllowEditAll = true;
            ucListManager.GridPageSize = 30;
            ucListManager.NeedListInstances += new NeedListInstancesEventHandler(ucListManager_NeedListInstances);

            GridViewHyperLinkColumn hc = new GridViewHyperLinkColumn();
            ucListManager.Columns.Add(hc);
            hc.Caption = "Subject Type";
            hc.DataTextField = SubjectDto.FLD_SubjectType;
            hc.DataNavigateUrlFormatString = string.Format("{0}?{1}={{0}}", ServerPath + SubjectManager.PageUrl, SubjectManager.QrySubjectId);
            hc.DataNavigateUrlFields = new string[] { SubjectDto.FLD_StringId };

            ucListManager.Columns.Add(new GridViewDataTextColumn("Subject Label", SubjectDto.FLD_SubjectLabel));
            ucListManager.Columns.Add(new GridViewDataTextColumn("SubjectId Field", SubjectDto.FLD_SubjectIdField));
            ucListManager.Columns.Add(new GridViewDataTextColumn("MasterSubjectId Field", SubjectDto.FLD_MasterSubjectIdField));
            ucListManager.Columns.Add(new GridViewDataCheckColumn("Allow ListAdd", SubjectDto.FLD_AllowListAdd));
            ucListManager.Columns.Add(new GridViewDataCheckColumn("Is AddInGrid", SubjectDto.FLD_IsAddInGrid));
            ucListManager.Columns.Add(new GridViewDataCheckColumn("Allow ListEdit", SubjectDto.FLD_AllowListEdit));
            ucListManager.Columns.Add(new GridViewDataCheckColumn("Is Grid InForm Edit", SubjectDto.FLD_IsGridInFormEdit));
            ucListManager.Columns.Add(new GridViewDataCheckColumn("Allow ListDelete", SubjectDto.FLD_AllowListDelete));
            ucListManager.Columns.Add(new GridViewDataCheckColumn("Allow ListFiltering", SubjectDto.FLD_AllowListFiltering));
            GridViewDataTextColumn rc = new GridViewDataTextColumn("RowIndex Max", SubjectDto.FLD_RowIndexMax);
            ucListManager.Columns.Add(rc);
            rc.IsReadOnly = true;
            GridViewDataTextColumn cc = new GridViewDataTextColumn("ColIndex Max", SubjectDto.FLD_ColIndexMax);
            ucListManager.Columns.Add(cc);
            cc.IsReadOnly = true;
        }

        protected void ucListManager_NeedListInstances(object sender, NeedListInstancesEventArgs e)
        {
            e.Instances = CurrentInstances;
        }

        private void LoadData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SubjectFacade subjectFacade = new SubjectFacade(uow);
                CurrentInstances = subjectFacade.RetrieveAllSubject();
            }
        }
    }
}