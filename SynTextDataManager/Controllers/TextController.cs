using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SynTextLibrary;


namespace SynTextDataManager.Controllers
{
    public class TextController : ApiController
    {
        // GET: api/Text
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Text/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Text
        public SampleText Post(SampleText text)
        {
            TextAnalyser textAnalyser = new TextAnalyser();
            string output = textAnalyser.GetReadabilityLevel(text.Text);
            SampleText sample = new SampleText();
            sample.Text = output;
            return sample;
            //return text.Text;
        }

        // PUT: api/Text/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Text/5
        public void Delete(int id)
        {
        }
    }
}
