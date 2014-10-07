using System;
using Framework.Business;
using SubjectEngine.Data;

namespace SubjectEngine.Business
{
    /// <summary>
    /// Summary description for SubjectInstance.
    /// </summary>
    public class SubjectInstance : ChildBusinessObject<Subject, SubjectInstanceData>
    {
        #region PROPERTIES

        public object InstanceId { get { return Data.InstanceId; } set { Data.InstanceId = value; } }

        public DateTime InstanceCreatedDate
        {
            get { return Data.InstanceCreatedDate; }
            set
            {
                if (!IsDataValueEqual(Data.InstanceCreatedDate, (value)))
                {
                    Data.InstanceCreatedDate = value;
                    OnPropertyChanged("InstanceCreatedDate");
                }
            }
        }

        #endregion PROPERTIES


        #region CHILD COLLECTIONS

        [ChildCollection]
        public ChildBoCollection<SubjectInstanceData, SubjectInstanceFieldValue, SubjectInstanceFieldValueData> SubjectInstanceFieldValues
        {
            get;
            private set;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            //InstanceFieldValues = new ChildBoCollection<SubjectInstanceData, InstanceFieldValue, InstanceFieldValueData>
            //    (Service, Data.InstanceFieldValuesData, this);
        }

        #endregion CHILD COLLECTIONS
        
    }
}
