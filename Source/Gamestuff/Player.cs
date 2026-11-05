using System.Reflection;

namespace Spacefruitz{
class Governor{
    public string name;
    public string position;
    public int lvl;
    public float growthrate;
    public int xp = 0;
    public int xptresh;

    public Government(string name, string position, int lvl, float growthrate, int xp, int xptresh)
    {
        this.name = name;
        this.position = position;
        this.lvl = lvl;
        this.growthrate = growthrate;
        this.xp = xp;
        this.xptresh = xptresh;
    }
};




class Government{
    public string name;
    public List<string> GovSlots;
    public List<string> GovSlotsName;
    public List<string> PolSlot;
    public List<Governor> officials;
    public string type;
    public string ethic;
    public List<string> values;
};

class Species{
    public string name;
    public string Comp;
    public bool Etherotrophic;
    public string Class;
    public List<string> Traits;
    public string preferred_planet;

    Species(string name, string Comp, bool Etherotrophic, string Class, List<string> Traits, string preferred_planet){
        this->name = name;
        this->Comp = Comp;
        this->Etherotrophic = Etherotrophic;
        this->Class = Class;
        this->Traits = Traits;
        this->preferred_planet = preferred_planet;
    }
};

class Player
{
    public Government Gov = new Government("Default", "Default", 1, 1.0f, 0, 100);
    public Species Founding_Species = new Species("ixlian", "carbon", true, "Arthropod", ["egg-laying", "exoskeleton"], "gaia");

    public List<PosmosVV.proc.systems> Domains = new List<PosmosVV.proc.systems>();

    public void FindStarter(ref PosmosVV.proc.Galaxy galaxy)
        {
            for (int i = 0; i < galaxy.systems.Count; i++)
            {
                for (int j = 0; j < galaxy.systems[i].Primary_bodies.Count; j++)
                {
                    if (galaxy.systems[i].Primary_bodies[j].type == Founding_Species.preferred_planet)
                    {
                        Domains.Add(galaxy.systems[i]);
                        galaxy.systems[i].Primary_bodies[j].DOM = new Domain();
                        galaxy.systems[i].Primary_bodies[j].DOM.DOMINIT(20);
                    };
                }
            }
        }

}
}
