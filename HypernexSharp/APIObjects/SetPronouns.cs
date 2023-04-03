using System;
using SimpleJSON;

namespace HypernexSharp.APIObjects
{
    public class SetPronouns
    {
        public bool Remove { get; }
        public PronounObject nominativeId { get; }
        public PronounObject accusativeId { get; }
        public PronounObject reflexiveId;
        public PronounObject independentId;
        public PronounObject dependentId;
        public bool DisplayThree;

        public JSONNode GetNode()
        {
            if (nominativeId == null || accusativeId == null)
                throw new Exception("Invalid SetPronoun");
            JSONObject o = new JSONObject();
            o.Add("nominativeId", nominativeId.Id);
            o.Add("accusativeId", accusativeId.Id);
            if(reflexiveId != null)
                o.Add("reflexiveId", reflexiveId.Id);
            if(independentId != null)
                o.Add("independentId", independentId.Id);
            if(dependentId != null)
                o.Add("dependentId", dependentId.Id);
            o.Add("DisplayThree", DisplayThree);
            return o;
        }

        public SetPronouns(bool remove) => Remove = remove;

        public SetPronouns(PronounObject nominativeId, PronounObject accusativeId)
        {
            this.nominativeId = nominativeId;
            this.accusativeId = accusativeId;
        }
    }
}