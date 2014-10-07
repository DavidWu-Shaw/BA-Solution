using System.Collections.Generic;
using Framework.Core;

namespace SubjectEngine.Component.Dto
{
    public class SubjectInstanceDto : BaseDto
    {
        public object InstanceId { get; set; }

        // <FieldKey, value>
        public Dictionary<string, object> SystemFieldValues { get; set; }
    }
}
