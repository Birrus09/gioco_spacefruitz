using System.Collections.Generic;
namespace PosmosVV.Proc
{
    //genes are 1 or 0
    public class Gamete
    {
        public char GenCode = (char)0xF0;
        public char HighLow = 'H'; // can be Low or High, during sexual reproduction two L and two H gamete combine to form two new Chromosomes (chars inside GenCode)
    }
    public class Chromecouple
    {
        public char[] GenCode = new char[2]; //each char is a Chromosome


        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
        //                                                  //
        // FIRST CHROMOSOME COUPLE IS ALWAYS A FLAG SET     //
        //                                                  //
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//

        public void Mutate()
        {
            //TODO: add xor with random power of 2
            GenCode[0] = (char)(GenCode[0] & 0x04);
        }

        public Chromecouple Mythosis()
        {
            Chromecouple g = new Chromecouple();
            g.GenCode[0] = (char)((this.GenCode[0] & 0xF0) | (this.GenCode[1] & 0x0F));
            g.GenCode[1] = (char)((this.GenCode[1] & 0xF0) | (this.GenCode[0] & 0x0F));
            return g;
        }

        public Gamete[] Meiosis(ref int seed)
        {
            //Each chromosome in a Chromecouple splits and gives a gamete, one H and one L
            Gamete[] g = new Gamete[2];
            g[0] = new Gamete();
            g[0].HighLow = 'H';
            g[1] = new Gamete();
            g[1].HighLow = 'L';
            if (RandomGen.Chance(0.5f, ref seed))
            {
                g[0].GenCode = (char)(this.GenCode[0] & 0xF0);
            }
            else
            {
                g[0].GenCode = (char)((this.GenCode[0] & 0x0F) << 4);
            }
            if (RandomGen.Chance(0.5f, ref seed))
            {
                g[1].GenCode = (char)(this.GenCode[1] & 0x0F);
            }
            else
            {
                g[1].GenCode = (char)((this.GenCode[1] & 0xF0) >> 4);
            }
            return g;
        }

    }

    public class Being
    {
        public Chromecouple[] Chromecouples;
        public int ID = 0;
        public int ChromecoupleLength = 4;
        public char sex = 'F'; //H is hermaphrodite
        public int age = 0;
        public bool Virtual = false; //artificial ins.
        public bool Haploid = false; //Females have 0x00 0xFF, Males have 0xFF 0x00
        public bool Diploid = false; //in Haplodiploid species the first Chromecouple determines queen or worker, in sexual species it determines sex
        public bool Egg = false;


        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
        //                                                  //
        // FIRST CHROMOSOME COUPLE IS ALWAYS A FLAG SET     //
        //                                                  //
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//


        public void initialize_Chromecouples()
        {
            Chromecouples = new Chromecouple[ChromecoupleLength];
        }

        public char Fenotype(int Chromecoupleindex)
        {
            //Fenotype is determined by the strongest chromosome, Genotype is determined by the whole Chromecouple
            if (Chromecouples[Chromecoupleindex].GenCode[0] > Chromecouples[Chromecoupleindex].GenCode[1])
            {
                return Chromecouples[Chromecoupleindex].GenCode[0];
            }
            else
            {
                return Chromecouples[Chromecoupleindex].GenCode[1];
            }
        }

        public char[] Genotype(int Chromecoupleindex)
        {
            return Chromecouples[Chromecoupleindex].GenCode;
        }
    }



    public static class GenFunc
    {

        public static Being Clonation(Being Origin)
        {
            //N -> (N, N)
            Being r = new Being();
            r = Origin;
            r.age = 0;
            return r;
        }


        public static Being Sexual_Reproduction(Being Male, Being Female, ref int seed)
        {
            //M, F -> (M, F)
            if (Male.ID == Female.ID)
            {
                var offspring = new Being();
                offspring.initialize_Chromecouples();

                if (!Male.Haploid && !Female.Diploid)
                {
                    for (int i = 0; i < Female.ChromecoupleLength; i++)
                    {
                        Gamete[] M_gams = Male.Chromecouples[i].Meiosis(ref seed);
                        Gamete[] F_gams = Female.Chromecouples[i].Meiosis(ref seed);
                        offspring.Chromecouples[i].GenCode[0] = (char)(M_gams[0].GenCode | F_gams[1].GenCode);
                        offspring.Chromecouples[i].GenCode[1] = (char)(M_gams[1].GenCode | F_gams[0].GenCode);
                    }
                }
                else
                {
                    offspring.Diploid = true;
                    for (int i = 0; i < Female.ChromecoupleLength; i++)
                    {
                        offspring.Chromecouples[i].GenCode[0] = Male.Chromecouples[i].GenCode[0];
                        offspring.Chromecouples[i].GenCode[1] = Female.Chromecouples[i].GenCode[1];
                    }
                }
                return offspring;
            }
            else
            {
                return null;
            }
        }


        public static Being ParthenoGenesis(Being Parent, ref int seed)
        {
            Being offspring = new Being();
            offspring.initialize_Chromecouples();
            if (Parent.Haploid)
            {
                //male drones in haplodiploid species
                offspring = Clonation(Parent);
                offspring.Egg = true;
            }
            else
            {
                for (int i = 0; i < Parent.ChromecoupleLength; i++)
                {
                    offspring.Chromecouples[i] = Parent.Chromecouples[i].Mythosis();
                }
            }
            return offspring;
        }

        public static Being DiploidEgg(Being Queen, ref int seed)
        {
            if (Queen.Diploid == true)
            {
                Being offspring = new Being();
                offspring.initialize_Chromecouples();
                offspring.Haploid = true;
                offspring.Egg = true;
                offspring.sex = 'F';
                for (int i = 0; i <  Queen.ChromecoupleLength; i++)
                {
                    Gamete[] FemGam = Queen.Chromecouples[i].Meiosis(ref seed);
                    offspring.Chromecouples[i].GenCode[1] = (char)(FemGam[0].GenCode | FemGam[1].GenCode);
                }
                return offspring;
            }
            else return null;

        }
        public static Being AndroGenesis(Being Female, Being Male, ref int seed)
        {
            Being offspring = new Being();
            offspring.initialize_Chromecouples();

            if (Male.ID == Female.ID){
                for (int i = 0; i < Male.ChromecoupleLength; i++)
                {
                    offspring.Chromecouples[i] = Male.Chromecouples[i].Mythosis();
                }
                return offspring;
            }
            else {
                    return null;
            }
        }

        //public static Being HybridoGenesis(Being Female, Being Male, ref int seed)
        //{
            //Ff1 + Mm2 -> (F, Mm) (lost Chromecouple, new species)
        //}

        public static void Crispr(Being Subject, char Chromosome, int Chromecoupleindex, int Chromindex = 0)
        {
            if (Chromecoupleindex < Subject.ChromecoupleLength)
            {
                Subject.Chromecouples[Chromecoupleindex].GenCode[Chromindex] = Chromosome;
            }
        }

    }
    public class Fertilizable_Eusocial_Being : Being
    {
        //ant queens and stuff like that
        public Being Virtual_Genetic_provider;

        public void Assimilate_sperm(Being Male)
        { 
            Virtual_Genetic_provider = Male;
            Virtual_Genetic_provider.Virtual = true;
            this.Diploid = true;
            this.Haploid = false;

        }
        public Being Lay_Eusocial_Egg(ref int seed)
        {
            Being n = new Being();
            n = GenFunc.Sexual_Reproduction(Virtual_Genetic_provider, this, ref seed);
            return n;
        }
    }

}


//example:
// Being Species_Sblibup = new Being(null, 123, 6, 'V', -1, true, false, false, false)
// Species_Sblibup.initialize_Chromecouple
// starter1 = Genfunc.Clonation(Species_Sblibup)
// starter1.sex = 'F'
// starter1.virtual = false
// starter2 = Genfunc.Clonation(Species_Sblibup)
// starter2.sex = 'F'
// starter2.virtual = false
// starter3 = Genfunc.Clonation(Species_Sblibup)
// starter3.sex = 'F'
// starter3.virtual = false
// now you have population starters
//probably should keep population in a vector


//example for haplodiploid species:
//Being Species_Sblibup = new Being(null, 123, 'V', -1, true, true, false, false)
//Species_Sblibup.initialize_Chromecouple
//Sbliblup_male_drone = Genfunc.Clonation(Species_Sblibup)
//Sbliblup_male_drone.sex = 'M'
//Fertilizable_Eusocial_Being Sblibup_Queen = Genfunc.Clonation(Species_Sblibup)
//Sblibup_Queen.sex = 'F'
//Sblibup_Queen.assimilate_sperm(Sbliblup_male_drone);

//now the queen can:
//Sbliblup_Queen.lay_eusocial_egg if species lays eggs (1st chromosome, 8th gene on 1)
//Genfunc.lay_egg(Sbliblup_Queen, ref seed) if species lays pupae (1st chromosome, 8th gene on 0)
