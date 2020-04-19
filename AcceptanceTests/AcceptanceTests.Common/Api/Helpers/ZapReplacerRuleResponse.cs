using System;
using System.Collections.Generic;
using System.Text;

namespace AcceptanceTests.Common.Api.Helpers
{
    public class ZapReplacerRuleResponse
    { 
        public List<ZapReplacerRule> Rules { get; set; }

       
    }
    public class ZapReplacerRule
    {
        public string Description { get; set; }
    }

}
