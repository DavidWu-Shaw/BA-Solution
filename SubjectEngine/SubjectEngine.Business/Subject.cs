using System.Linq;
using Framework.Business;
using SubjectEngine.Data;

namespace SubjectEngine.Business
{
    public class Subject : BusinessObject<SubjectData>
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();

            SubjectFields = new ChildBoCollection<SubjectData, SubjectField, SubjectFieldData>
                (Service, Data.SubjectFieldsData, this);

            SubjectChildLists = new ChildBoCollection<SubjectData, SubjectChildList, SubjectChildListData>
                (Service, Data.SubjectChildListsData, this);

            SubjectLayouts = new ChildBoCollection<SubjectData, SubjectLayout, SubjectLayoutData>
                (Service, Data.SubjectLayoutsData, this);

            SubjectInstances = new ChildBoCollection<SubjectData, SubjectInstance, SubjectInstanceData>
                (Service, Data.SubjectInstancesData, this);
        }

        [ChildCollection]
        public ChildBoCollection<SubjectData, SubjectLayout, SubjectLayoutData> SubjectLayouts
        {
            get;
            private set;
        }

        [ChildCollection]
        public ChildBoCollection<SubjectData, SubjectField, SubjectFieldData> SubjectFields
        {
            get;
            private set;
        }

        [ChildCollection]
        public ChildBoCollection<SubjectData, SubjectChildList, SubjectChildListData> SubjectChildLists
        {
            get;
            private set;
        }

        [ChildCollection]
        public ChildBoCollection<SubjectData, SubjectInstance, SubjectInstanceData> SubjectInstances
        {
            get;
            private set;
        }

        public bool IsChildSubject
        {
            get
            {
                return MasterSubjectId != null;
            }
        }

        public int RowIndexMax
        {
            get
            {
                if (SubjectFields.Count > 0)
                {
                    return SubjectFields.Max(f => f.RowIndex);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int ColIndexMax
        {
            get
            {
                if (SubjectFields.Count > 0)
                {
                    return SubjectFields.Max(f => f.ColIndex);
                }
                else
                {
                    return 0;
                }
            }
        }

        public object MasterSubjectId
        {
            get { return Data.MasterSubjectId; }
            set
            {
                if (!IsDataValueEqual(Data.MasterSubjectId, (value)))
                {
                    Data.MasterSubjectId = value;
                    OnPropertyChanged("MasterSubjectId");
                }
            }
        }

        public string SubjectType
        {
            get { return Data.SubjectType; }
            set
            {
                if (!IsDataValueEqual(Data.SubjectType, (value)))
                {
                    Data.SubjectType = value;
                    OnPropertyChanged("SubjectType");
                }
            }
        }

        public string SubjectLabel
        {
            get { return Data.SubjectLabel; }
            set { Data.SubjectLabel = value; }
        }

        public string Description
        {
            get { return Data.Description; }
            set
            {
                if (!IsDataValueEqual(Data.Description, (value)))
                {
                    Data.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public string TableName
        {
            get { return Data.TableName; }
            set
            {
                if (!IsDataValueEqual(Data.TableName, (value)))
                {
                    Data.TableName = value;
                    OnPropertyChanged("TableName");
                }
            }
        }

        public string ManageUrl
        {
            get { return Data.ManageUrl; }
            set
            {
                if (!IsDataValueEqual(Data.ManageUrl, (value)))
                {
                    Data.ManageUrl = value;
                    OnPropertyChanged("ManageUrl");
                }
            }
        }

        public string EditUrl
        {
            get { return Data.EditUrl; }
            set
            {
                if (!IsDataValueEqual(Data.EditUrl, (value)))
                {
                    Data.EditUrl = value;
                    OnPropertyChanged("EditUrl");
                }
            }
        }

        public string ListUrl
        {
            get { return Data.ListUrl; }
            set
            {
                if (!IsDataValueEqual(Data.ListUrl, (value)))
                {
                    Data.ListUrl = value;
                    OnPropertyChanged("ListUrl");
                }
            }
        }

        public string ImportUrl
        {
            get { return Data.ImportUrl; }
            set { Data.ImportUrl = value; }
        }

        public bool AllowListImport
        {
            get { return Data.AllowListImport; }
            set { Data.AllowListImport = value; }
        }

        public bool AllowListFiltering
        {
            get { return Data.AllowListFiltering; }
            set { Data.AllowListFiltering = value; }
        }
        public bool AllowListAdd
        {
            get { return Data.AllowListAdd; }
            set { Data.AllowListAdd = value; }
        }

        public bool AllowListEdit
        {
            get { return Data.AllowListEdit; }
            set { Data.AllowListEdit = value; }
        }

        public bool AllowListDelete
        {
            get { return Data.AllowListDelete; }
            set { Data.AllowListDelete = value; }
        }

        public bool IsAddInGrid
        {
            get { return Data.IsAddInGrid; }
            set { Data.IsAddInGrid = value; }
        }

        public bool IsGridInFormEdit
        {
            get { return Data.IsGridInFormEdit; }
            set { Data.IsGridInFormEdit = value; }
        }

        public string SubjectIdField
        {
            get { return Data.SubjectIdField; }
            set { Data.SubjectIdField = value; }
        }
        public string MasterSubjectIdField
        {
            get { return Data.MasterSubjectIdField; }
            set { Data.MasterSubjectIdField = value; }
        }
    }
}
