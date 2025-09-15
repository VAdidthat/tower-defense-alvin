using System.Collections;
using System.Collections.Generic;

namespace Statuses
{
    public class NullStatus : BaseStatus
    {
        protected override void OnInitialized(){}
        protected override void OnBegin(){}
        protected override void OnEnd(){}

        protected override IEnumerator OnExecuting()
        {
            return null;
        }
    }
}