using System;
using System.Collections.Generic;
using System.Text;
using Framework.Business;
using PA.Data;

namespace PA.Business
{
    /// <summary>
    /// Summary description for Document.
    /// </summary>
    public class Document : BusinessObject<DocumentData>
    {        
        #region PROPERTIES
		public string Code
		{
			get { return Data.Code; }
			set
			{
				if ( !IsDataValueEqual( Data.Code, ( value ) ) )
				{
					Data.Code = value;
					OnPropertyChanged( "Code" );
				}
			}
		}



        public byte[] Content
		{
			get { return Data.Content; }
			set
			{
				if ( !IsDataValueEqual( Data.Content, ( value ) ) )
				{
					Data.Content = value;
                    OnPropertyChanged("Content");
				}
			}
		}


		
		public int? PublishedMode
		{
			get { return Data.PublishedMode; }
			set
			{
				if ( !IsDataValueEqual( Data.PublishedMode, ( value ) ) )
				{
					Data.PublishedMode = value;
					OnPropertyChanged( "PublishedMode" );
				}
			}
		}



        public byte[] Thumbnail
		{
			get { return Data.Thumbnail; }
			set
			{
				if ( !IsDataValueEqual( Data.Thumbnail, ( value ) ) )
				{
					Data.Thumbnail = value;
					OnPropertyChanged( "Thumbnail" );
				}
			}
		}


		public string Notes
		{
			get { return Data.Notes; }
			set
			{
				if ( !IsDataValueEqual( Data.Notes, ( value ) ) )
				{
					Data.Notes = value;
					OnPropertyChanged( "Notes" );
				}
			}
		}


		
		public int? TypeId
		{
			get { return Data.TypeId; }
			set
			{
				if ( !IsDataValueEqual( Data.TypeId, ( value ) ) )
				{
					Data.TypeId = value;
					OnPropertyChanged( "TypeId" );
				}
			}
		}



        public object IssuedById
		{
			get { return Data.IssuedById; }
			set
			{
                if (!IsDataValueEqual(Data.IssuedById, (value)))
				{
                    Data.IssuedById = value;
                    OnPropertyChanged("IssuedById");
				}
			}
		}


		
		public DateTime? IssuedDate
		{
			get { return Data.IssuedDate; }
			set
			{
				if ( !IsDataValueEqual( Data.IssuedDate, ( value ) ) )
				{
					Data.IssuedDate = value;
					OnPropertyChanged( "IssuedDate" );
				}
			}
		}


		public string Source
		{
			get { return Data.Source; }
			set
			{
				if ( !IsDataValueEqual( Data.Source, ( value ) ) )
				{
					Data.Source = value;
					OnPropertyChanged( "Source" );
				}
			}
		}


		public string Description
		{
			get { return Data.Description; }
			set
			{
				if ( !IsDataValueEqual( Data.Description, ( value ) ) )
				{
					Data.Description = value;
					OnPropertyChanged( "Description" );
				}
			}
		}


		
		public int? OriginalCopyId
		{
			get { return Data.OriginalCopyId; }
			set
			{
				if ( !IsDataValueEqual( Data.OriginalCopyId, ( value ) ) )
				{
					Data.OriginalCopyId = value;
					OnPropertyChanged( "OriginalCopyId" );
				}
			}
		}


		public string Extension
		{
			get { return Data.Extension; }
			set
			{
				if ( !IsDataValueEqual( Data.Extension, ( value ) ) )
				{
					Data.Extension = value;
					OnPropertyChanged( "Extension" );
				}
			}
		}



        #endregion PROPERTIES


    }
}
