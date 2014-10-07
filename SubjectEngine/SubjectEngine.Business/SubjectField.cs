using System;
using Framework.Business;
using Framework.Core;
using SubjectEngine.Data;
using SubjectEngine.Service.Contract;

namespace SubjectEngine.Business
{
    public class SubjectField : ChildBusinessObject<Subject, SubjectFieldData>
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();

            FieldDataType = new LazyBoProperty<DataType, IDataTypeService>(UnitOfWork,
                () => Data.FieldDataTypeData,
                o => Data.FieldDataTypeData = o.Cast<DataTypeData>());
        }

        public LazyProperty<DataType> FieldDataType
        {
            get;
            private set;
        }

        #region PROPERTIES

        public string FieldKey
        {
            get { return Data.FieldKey; }
            set
            {
                if (!IsDataValueEqual(Data.FieldKey, (value)))
                {
                    Data.FieldKey = value;
                    OnPropertyChanged("FieldKey");
                }
            }
        }


        public string FieldLabel
        {
            get { return Data.FieldLabel; }
            set
            {
                if (!IsDataValueEqual(Data.FieldLabel, (value)))
                {
                    Data.FieldLabel = value;
                    OnPropertyChanged("FieldLabel");
                }
            }
        }


        public string HelpText
        {
            get { return Data.HelpText; }
            set
            {
                if (!IsDataValueEqual(Data.HelpText, (value)))
                {
                    Data.HelpText = value;
                    OnPropertyChanged("HelpText");
                }
            }
        }

        public object PickupEntityId
        {
            get { return Data.PickupEntityId; }
            set
            {
                if (!IsDataValueEqual(Data.PickupEntityId, (value)))
                {
                    Data.PickupEntityId = value;
                    OnPropertyChanged("PickupEntityId");
                }
            }
        }

        public object LookupSubjectId
        {
            get { return Data.LookupSubjectId; }
            set
            {
                if (!IsDataValueEqual(Data.LookupSubjectId, (value)))
                {
                    Data.LookupSubjectId = value;
                    OnPropertyChanged("LookupSubjectId");
                }
            }
        }



        public bool IsRequired
        {
            get { return Data.IsRequired; }
            set
            {
                if (!IsDataValueEqual(Data.IsRequired, (value)))
                {
                    Data.IsRequired = value;
                    OnPropertyChanged("IsRequired");
                }
            }
        }



        public bool IsBuiltIn
        {
            get { return Data.IsBuiltIn; }
            set
            {
                if (!IsDataValueEqual(Data.IsBuiltIn, (value)))
                {
                    Data.IsBuiltIn = value;
                    OnPropertyChanged("IsBuiltIn");
                }
            }
        }

        public bool IsReadonly
        {
            get { return Data.IsReadonly; }
            set
            {
                if (!IsDataValueEqual(Data.IsReadonly, (value)))
                {
                    Data.IsReadonly = value;
                    OnPropertyChanged("IsReadonly");
                }
            }
        }

        public bool IsShowInGrid
        {
            get { return Data.IsShowInGrid; }
            set
            {
                if (!IsDataValueEqual(Data.IsShowInGrid, (value)))
                {
                    Data.IsShowInGrid = value;
                    OnPropertyChanged("IsShowInGrid");
                }
            }
        }

        public bool IsLinkInGrid
        {
            get { return Data.IsLinkInGrid; }
            set
            {
                if (!IsDataValueEqual(Data.IsLinkInGrid, (value)))
                {
                    Data.IsLinkInGrid = value;
                    OnPropertyChanged("IsLinkInGrid");
                }
            }
        }

        public int Sort
        {
            get { return Data.Sort; }
            set { Data.Sort = value; }
        }

        public int RowIndex
        {
            get
            {
                return Data.RowIndex;
            }
            set
            {
                if (!IsDataValueEqual(Data.RowIndex, (value)))
                {
                    Data.RowIndex = value;
                    OnPropertyChanged("RowIndex");
                }
            }
        }

        public int ColIndex
        {
            get
            {
                return Data.ColIndex;
            }
            set
            {
                if (!IsDataValueEqual(Data.ColIndex, (value)))
                {
                    Data.ColIndex = value;
                    OnPropertyChanged("ColIndex");
                }
            }
        }

        public int? MaxLength
        {
            get { return Data.MaxLength; }
            set { Data.MaxLength = value; }
        }

        public string NavigateUrlFormatString
        {
            get { return Data.NavigateUrlFormatString; }
            set { Data.NavigateUrlFormatString = value; }
        }

        #endregion PROPERTIES
    }
}
