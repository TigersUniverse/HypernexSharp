using System.Collections.Generic;
using SimpleJSON;

namespace HypernexSharp.APIObjects
{
    public class Pronouns
    {
        private static readonly List<PronounObject> ListedPronouns = new List<PronounObject>
        {
            new PronounObject(0, "He", "Him", "Himself", "His", "His"),
            new PronounObject(1, "She", "Her", "Herself", "Hers", "Her"),
            new PronounObject(2, "They", "Them", "Themselves"),
            new PronounObject(3, "It", "It", "Itself", dependentGenitiveCase: "Its"),
            new PronounObject(4, "Any", "Any"),
            new PronounObject(5, "Ze", "Zir", "Zirself", "Zirs"),
            new PronounObject(6, "Ze", "Hir", "Hirself", "Hirs"),
            new PronounObject(7, "Xe", "Xem", "Xyrself", "Xyrs"),
            new PronounObject(8, "Ey", "Em", "Eirself", "Eirs"),
            new PronounObject(9, "Fae", "Faer", "Faerself", "Faers"),
            new PronounObject(10, "Em", "Eir", "Eirself", "Eirs"),
            new PronounObject(11, "Ve", "Ver", "Verself", "Vis"),
            new PronounObject(12, "Ne", "Nem", "Nemself", "Nirs"),
            new PronounObject(13, "Per", "Pers", "Perself", "Pers"),
            new PronounObject(-1, "Other", "Other"),
            new PronounObject(-2, "Ask", "Ask"),
            new PronounObject(-3, "Avoid", "Avoid")
        };

        public static PronounObject GetPronounObjectById(int id)
        {
            foreach (PronounObject listedPronoun in ListedPronouns)
            {
                if (listedPronoun.Id == id)
                    return listedPronoun;
            }
            return null;
        }

        public string NominativeCase { get; set; }
        public string AccusativeCase { get; set; }
        public string ReflexivePronoun { get; set; }
        public string IndependentGenitiveCase { get; set; }
        public string DependentGenitiveCase { get; set; }
        public bool Action { get; set; }
        public bool DisplayThree { get; set; }
        public List<PronounCases> Display { get; set; } = new List<PronounCases>();

        public static Pronouns FromJSON(JSONNode node)
        {
            Pronouns pronouns = new Pronouns
            {
                NominativeCase = node["NominativeCase"].Value,
                AccusativeCase = node["AccusativeCase"].Value,
                Action = node["Action"].AsBool,
                DisplayThree = node["DisplayThree"].AsBool
            };
            if (node.HasKey("ReflexivePronoun"))
                pronouns.ReflexivePronoun = node["ReflexivePronoun"].Value;
            if (node.HasKey("IndependentGenitiveCase"))
                pronouns.IndependentGenitiveCase = node["IndependentGenitiveCase"].Value;
            if (node.HasKey("DependentGenitiveCase"))
                pronouns.DependentGenitiveCase = node["DependentGenitiveCase"].Value;
            if (node.HasKey("Display"))
            {
                pronouns.Display = new List<PronounCases>();
                JSONArray cases = node["Display"].AsArray;
                foreach (KeyValuePair<string,JSONNode> keyValuePair in cases)
                {
                    int c = keyValuePair.Value.AsInt;
                    pronouns.Display.Add((PronounCases) c);
                }
            }
            return pronouns;
        }

        public override string ToString()
        {
            if (Display.Count <= 1)
                return NominativeCase ?? "" + "/" + AccusativeCase;
            string t = "";
            int i = 0;
            foreach (PronounCases pronounCases in Display)
            {
                switch (pronounCases)
                {
                    case PronounCases.NominativeCase:
                        t += NominativeCase ?? "";
                        break;
                    case PronounCases.AccusativeCase:
                        t += AccusativeCase ?? "";
                        break;
                    case PronounCases.ReflexivePronoun:
                        t += ReflexivePronoun ?? "";
                        break;
                    case PronounCases.IndependentGenitiveCase:
                        t += IndependentGenitiveCase ?? "";
                        break;
                    case PronounCases.DependentGenitiveCase:
                        t += DependentGenitiveCase ?? "";
                        break;
                }
                i++;
                if (i >= Display.Count || (!DisplayThree && i > 1))
                    break;
                t += "/";
            }
            return t;
        }
    }

    public class PronounObject
    {
        public int Id { get; }
        public string NominativeCase { get; }
        public string AccusativeCase { get; }
        public string ReflexivePronoun { get; }
        public string IndependentGenitiveCase { get; }
        public string DependentGenitiveCase { get; }

        public PronounObject(int id, string nominativeCase, string accusativeCase, string reflexivePronoun = "",
            string independentGenitiveCase = "", string dependentGenitiveCase = "")
        {
            Id = id;
            NominativeCase = nominativeCase;
            AccusativeCase = accusativeCase;
            if (!string.IsNullOrEmpty(reflexivePronoun))
                ReflexivePronoun = reflexivePronoun;
            if (!string.IsNullOrEmpty(independentGenitiveCase))
                IndependentGenitiveCase = independentGenitiveCase;
            if (!string.IsNullOrEmpty(dependentGenitiveCase))
                DependentGenitiveCase = dependentGenitiveCase;
        }
    }
}