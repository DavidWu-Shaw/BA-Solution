using System.Collections.Generic;
using Framework.Data;

namespace SubjectEngine.Data
{
	public class DEntityData : DataObject
	{        
		public DEntityData()
		{
			DEntityItemsData = new List<DEntityItemData>();
		}

		public virtual string Code { get; set; } 
		public virtual string Description { get; set; } 
		public virtual int? EntityTypeId { get; set; }
		public virtual bool IsBuiltIn { get; set; }
		public virtual bool AllowAddItem { get; set; }
		public virtual bool AllowEditItem { get; set; }
		public virtual bool AllowDeleteItem { get; set; }

		public virtual IList<DEntityItemData> DEntityItemsData { get; set; }
	}
}
