using System;
using SubjectEngine.Component.Dto;
using System.Web.UI.HtmlControls;
using BA.Core;
using Framework.Core;
using System.Collections.Generic;
using Framework.UoW;
using BA.UnityRegistry;
using SubjectEngine.Component;
using CRM.Component;
using Framework.Core.Helpers;

namespace BA.Web.Common.Subject.UI
{
    public class InstanceBaseControl : BaseControl
    {
        protected const string QryMasterInstanceId = "MasterInstanceId";

        private const string SubjectStateKey = "SubjectSessionKey";

        private string UniqueSubjectStateKey { get { return string.Format("{0}_{1}", UniqueID, SubjectStateKey); } }
        private string UniqueInstanceStateKey { get { return string.Format("{0}_{1}", UniqueID, InstanceStateKey); } }

        public int? MasterSubjectInstanceId
        {
            get
            {
                return Request.QueryString[QryMasterInstanceId].TryToParse<int>();
            }
        }

        public string ControlledFieldName { get; set; }
        public object ControlledFieldValue { get; set; }

        public string SubjectType { get; set; }

        private List<DucBaseControl> _ducControlCollection = new List<DucBaseControl>();

        private SubjectDto _currentSubject;
        public SubjectDto CurrentSubject
        {
            get
            {
                if (_currentSubject == null)
                {
                    if (IsInSession(UniqueSubjectStateKey))
                    {
                        _currentSubject = GetFromSession(UniqueSubjectStateKey) as SubjectDto;
                    }
                    else
                    {
                        _currentSubject = WebContext.Current.GetSubject(SubjectType);
                        using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                        {
                            CRMSubjectFacade pa = new CRMSubjectFacade(uow);
                            pa.AttachProperties(_currentSubject);
                        }

                        SaveInSession(_currentSubject, UniqueSubjectStateKey);
                    }
                }

                return _currentSubject;
            }
            set
            {
                _currentSubject = value;
                SaveInSession(_currentSubject, UniqueSubjectStateKey);
            }
        }

        public BaseDto CurrentInstance { protected get; set; }

        protected override void OnInit(EventArgs e)
        {
            if (!IsPostBack)
            {
                RemoveFromSession(UniqueSubjectStateKey);
                RemoveFromSession(UniqueInstanceStateKey);
            }

            base.OnInit(e);
        }

        public virtual string GetTitle()
        {
            ArgumentValidator.IsNotNull("CurrentSubject", CurrentSubject);
            ArgumentValidator.IsNotNull("CurrentSubjectInstance", CurrentInstance);

            string title = string.Empty;

            string label = CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
            if (CurrentInstance.IsNew)
            {
                title = string.Format("{0}: {1} {2}", label, UILabelDef.InstanceNewLabel, label);
            }
            else
            {
                title = string.Format("{0}: {1}", label, CurrentInstance.Display);
            }

            return title;
        }

        protected void CreateLayout(HtmlTable subjectLayoutPlace, SubjectLoadMode loadMode)
        {
            // create rows/cells according to layout information
            if (CurrentSubject.RowIndexMax > 0 && CurrentSubject.ColIndexMax > 0)
            {
                for (int rowIndex = 1; rowIndex <= CurrentSubject.RowIndexMax; rowIndex++)
                {
                    HtmlTableRow row = new HtmlTableRow();
                    row.Height = "22";
                    subjectLayoutPlace.Rows.Add(row);
                    for (int cellIndex = 1; cellIndex <= CurrentSubject.ColIndexMax; cellIndex++)
                    {
                        HtmlTableCell cell = new HtmlTableCell();
                        cell.VAlign = "middle";
                        cell.Align = "left";
                        row.Cells.Add(cell);
                    }
                }
            }

            // bind SubjectField to rows/cells
            foreach (SubjectFieldDto field in CurrentSubject.SubjectFields)
            {
                if (field.RowIndex > 0 && field.ColIndex > 0)
                {
                    HtmlTableCell aCell = subjectLayoutPlace.Rows[field.RowIndex - 1].Cells[field.ColIndex - 1];

                    DucBaseControl ducControl = BindSubjectField(field);
                    if (ducControl != null)
                    {
                        aCell.Controls.Add(ducControl);
                        ducControl.SubjectField = field;
                        ducControl.LoadMode = loadMode;
                        if (field.FieldKey.Equals(ControlledFieldName, StringComparison.OrdinalIgnoreCase))
                        {
                            ducControl.Disabled = true;
                        }

                        _ducControlCollection.Add(ducControl);
                    }
                }
            }
        }

        public void LoadInstanceData()
        {
            if (CurrentInstance.IsNew)
            {
                // For new instance, set Controlled field value
                if (ControlledFieldName.TrimHasValue() && ControlledFieldValue != null)
                {
                    ReflectionHelper.SetValue(CurrentInstance, ControlledFieldName, ControlledFieldValue);
                }
            }
            foreach (DucBaseControl ducControl in _ducControlCollection)
            {
                ducControl.LoadDucValue(CurrentInstance);
            }
        }

        protected void SaveInstanceData()
        {
            foreach (DucBaseControl ducControl in _ducControlCollection)
            {
                ducControl.SaveDucValue(CurrentInstance);
            }
        }

        protected bool ValidateValue()
        {
            bool isValid = true;
            foreach (DucBaseControl ducControl in _ducControlCollection)
            {
                bool valid = ducControl.ValidateValue();
                if (!valid)
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }

        private DucBaseControl BindSubjectField(SubjectFieldDto field)
        {
            string virtualPath = string.Format(ServerPath + DucBaseControl.ControlURLFormatString, field.DucType);

            DucBaseControl ducControl = (DucBaseControl)Page.LoadControl(virtualPath);

            return ducControl;
        }

        private HtmlTableCell AddOneRowCelltoTable(HtmlTable aTable)
        {
            HtmlTableRow aRow = new HtmlTableRow();
            aTable.Rows.Add(aRow);
            HtmlTableCell aCell = new HtmlTableCell();
            aCell.VAlign = "top";
            aCell.Align = "left";
            aRow.Cells.Add(aCell);

            return aCell;
        }

        private HtmlTableCell AddOneNullCelltoTable(HtmlTable aTable)
        {
            HtmlTableRow aRow = new HtmlTableRow();
            aTable.Rows.Add(aRow);
            HtmlTableCell aCell = new HtmlTableCell();
            aCell.VAlign = "top";
            aCell.Align = "left";
            aCell.Height = "10px";
            aRow.Cells.Add(aCell);

            return aCell;
        }
    }
}