using System;
using Framework.Business;
using SubjectEngine.Data;

namespace SubjectEngine.Business
{
	/// <summary>
	/// Summary description for InstanceFieldValue.
	/// </summary>
	public class SubjectInstanceFieldValue : ChildBusinessObject<SubjectInstance, SubjectInstanceFieldValueData>
	{        
		#region PROPERTIES

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


		
		public int? ValueInt
		{
			get { return Data.ValueInt; }
			set
			{
				if ( !IsDataValueEqual( Data.ValueInt, ( value ) ) )
				{
					Data.ValueInt = value;
					OnPropertyChanged( "ValueInt" );
				}
			}
		}


		public string ValueText
		{
			get { return Data.ValueText; }
			set
			{
				if ( !IsDataValueEqual( Data.ValueText, ( value ) ) )
				{
					Data.ValueText = value;
					OnPropertyChanged( "ValueText" );
				}
			}
		}


		
		public DateTime? ValueDate
		{
			get { return Data.ValueDate; }
			set
			{
				if (!IsDataValueEqual(Data.ValueDate, (value)))
				{
					Data.ValueDate = value;
					OnPropertyChanged("ValueDate");
				}
			}
		}



		#endregion PROPERTIES

	}
}
