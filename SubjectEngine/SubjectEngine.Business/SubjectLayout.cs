using Framework.Business;
using SubjectEngine.Data;

namespace SubjectEngine.Business
{
	/// <summary>
	/// Summary description for SubjectLayout.
	/// </summary>
	public class SubjectLayout : ChildBusinessObject<Subject, SubjectLayoutData>
	{        
		#region PROPERTIES
		
		public int ItemTypeId
		{
			get { return Data.ItemTypeId; }
			set
			{
				if ( !IsDataValueEqual( Data.ItemTypeId, ( value ) ) )
				{
					Data.ItemTypeId = value;
					OnPropertyChanged( "ItemTypeId" );
				}
			}
		}



		public object SubjectFieldId
		{
			get { return Data.SubjectFieldId; }
			set
			{
				if ( !IsDataValueEqual( Data.SubjectFieldId, ( value ) ) )
				{
					Data.SubjectFieldId = value;
					OnPropertyChanged( "SubjectFieldId" );
				}
			}
		}


		
		public int? SubjectActionId
		{
			get { return Data.SubjectActionId; }
			set
			{
				if ( !IsDataValueEqual( Data.SubjectActionId, ( value ) ) )
				{
					Data.SubjectActionId = value;
					OnPropertyChanged( "SubjectActionId" );
				}
			}
		}


		public string SectionLabel
		{
			get { return Data.SectionLabel; }
			set
			{
				if ( !IsDataValueEqual( Data.SectionLabel, ( value ) ) )
				{
					Data.SectionLabel = value;
					OnPropertyChanged( "SectionLabel" );
				}
			}
		}


		
		public int? RowIndex
		{
			get { return Data.RowIndex; }
			set
			{
				if ( !IsDataValueEqual( Data.RowIndex, ( value ) ) )
				{
					Data.RowIndex = value;
					OnPropertyChanged( "RowIndex" );
				}
			}
		}


		
		public int? CellIndex
		{
			get { return Data.CellIndex; }
			set
			{
				if ( !IsDataValueEqual( Data.CellIndex, ( value ) ) )
				{
					Data.CellIndex = value;
					OnPropertyChanged( "CellIndex" );
				}
			}
		}



		#endregion PROPERTIES


		#region CHILD COLLECTIONS

		#endregion CHILD COLLECTIONS


	}
}
