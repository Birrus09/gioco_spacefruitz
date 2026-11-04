namespace PosmosVV.Proc
{
    public static class ByteUtil
    {
        public static byte[] RGB_Parser(string arg)
        {
            byte[] c = new byte[3];
            c[0] = (byte)(arg[0] + arg[1]);
            c[1] = (byte)(arg[2] + arg[3]);
            c[2] = (byte)(arg[4] + arg[5]);
            return c;
        }
    }


    public class Body
    {
        public int id;
        public int[] coords;
        public string type;
        public int size;
        public float temperature;
        public int magnetic_field;
        public float rotation_speed;
        public float revolution_speed;
        public List<Body> Orbits;
        public int distance;
        public bool hasOrbitals = false;
        public byte[] RGB = new byte[3];

        public Body()
        {
            size = 0;
            temperature = 0.0f;
            magnetic_field = 0;
            rotation_speed = 0.0f;
            revolution_speed = 0.0f;
            RGB = new byte[3];
            distance = 0;
            hasOrbitals = false;
        }

        public void FindType(ref int seed)
        {
            double ypol = RandomGen.IntervalF(-50.0f, 50.0f, ref seed);
            /*             rocky
              temp --------------------- >
            hum
             |  barren    arid      molten
             |  #7D4700   #B0AB6B   #361203
             |  #DB7A00   #FFCB87   #FF3B3B
             |  
             |   tundra   gaia      primworld
             |   #4B636B  #247596   #3967A3
             |   #C1D0D6  #1EB042   #409981
             |
             |   frozen   tropical   stormworld
             |   #4B636B  #247596    #3967A3
             |   #C1D0D6  #1EB042    #409981
             V
            */
            if (magnetic_field / size > 0.9 && size > 400)
            {
                type = "gas giant";
                RGB = ByteUtil.RGB_Parser("361203");
                return;
            }

            if (temperature > 150.0f)
            {
                if (ypol > 10.0f)
                {
                    type = "stormworld";
                    RGB = ByteUtil.RGB_Parser("3967A3");
                }
                else if (ypol < -10.0f)
                {
                    type = "molten";
                    RGB = ByteUtil.RGB_Parser("361203");
                }
                else
                {
                    type = "primworld";
                    RGB = ByteUtil.RGB_Parser("3967A3");
                    if (temperature > 400.0f)
                    {
                        type = "molten";
                        RGB = ByteUtil.RGB_Parser("361203");
                    }
                }
            }
            else if (temperature < 0.0f)
            {
                if (ypol > 10.0f)
                {
                    type = "frozen";
                    RGB = ByteUtil.RGB_Parser("4B636B");
                }
                else if (ypol < -10.0f)
                {
                    type = "barren";
                    RGB = ByteUtil.RGB_Parser("7D4700");
                }
                else
                {
                    type = "tundra";
                    RGB = ByteUtil.RGB_Parser("4B636B");
                }

            }
            else
            {
                if (ypol > 10.0f)
                {
                    type = "tropical";
                    RGB = ByteUtil.RGB_Parser("247596");
                }
                else if (ypol < -10.0f)
                {
                    type = "arid";
                    RGB = ByteUtil.RGB_Parser("B0AB6B");
                }
                else
                {
                    type = "gaia";
                    RGB = ByteUtil.RGB_Parser("247596");
                }
            }
        }
    }

    public class Star : Body
    {
        public int out_temp;
        public int radiation;

        public void gen(ref int seed)
        {
            id = 1;
            coords = new int[2];
            coords[0] = 0;
            coords[1] = 0;
            size = RandomGen.Interval(100, 500, ref seed);
            temperature = RandomGen.IntervalF(3000.0f, 6000.0f, ref seed) * size * 2 / 500;
            out_temp = (int)temperature / 10;
            radiation = RandomGen.Interval(100, 10000, ref seed) * out_temp * size / 500;
        }

    }



    class System
    {

        public int sys_id;
        public List<Body> Primary_bodies; //no orbitals, they are inherited;
        public int[] coordinates;

        public System()
        {
            sys_id = 0;
            Primary_bodies = new List<Body>();
            coordinates = new int[2];
        }
        public void GenerateSystem(ref int seed)
        {
            sys_id = 1;
            Primary_bodies = new List<Body>();
            Star star = new Star();
            star.gen(ref seed);
            Primary_bodies.Add(star);

            //add smal chance for binary star system
            //TODO

            int bodiesss = RandomGen.Interval(5, 9, ref seed);
            for (int i = 0; i < bodiesss; i++)
            {
                Body b = new Body();
                b.size = RandomGen.Interval(80, 450, ref seed);
                b.distance = (int)(( 400.0f / (float)(Primary_bodies[i].size) + i+1) * 100); // orbit goes from +distance to -distance
                b.coords = new int[2];
                b.coords[0] = b.distance;
                b.coords[1] = 0;
                b.id = 1;
                if (RandomGen.Chance(Utility.maximum(0.0f, b.size / 450.0f - 0.2f), ref seed))
                {
                    b.hasOrbitals = true;
                    b.Orbits = new List<Body>();
                    for (int j = 0; j < RandomGen.Interval(1, 4, ref seed); j++)
                    {
                        Body o = new Body();
                        o.distance = (int)(RandomGen.IntervalF(0.83f, 1.24f, ref seed) * j * 40);
                        o.size = RandomGen.Interval(20, 120, ref seed);
                        o.coords = new int[2];
                        o.coords[0] = 0;
                        o.coords[1] = o.distance;
                        o.type = "moon";
                        if (o.size > b.size * 0.5f)
                        {
                            o.type = "twin planet";
                            b.type = "twin planet";
                        }
                        else if (o.size > 70)
                        {
                            o.type = "orbital";
                        }
                        o.temperature = RandomGen.IntervalF(-20.0f, 20.0f, ref seed);
                        o.temperature = (b.temperature) * 0.2f + (star.out_temp - 2000.0f) * 1.4f;
                        b.Orbits.Add(o);
                    }
                }
                b.magnetic_field = (int)RandomGen.IntervalF(0.6f, 1.2f, ref seed) * b.size / 2;
                b.revolution_speed = RandomGen.IntervalF(10.0f, 100.0f, ref seed);
                b.rotation_speed = RandomGen.IntervalF(0.1f, 1.0f, ref seed);

                b.temperature = (star.temperature * (bodiesss - i)/(bodiesss)) * 0.3f -800.0f;

                
                b.FindType(ref seed);


                Primary_bodies.Add(b);
            }


        }

    }



    class Galaxy
    {
        public List<System> systems;
        int starsize = 0;
        public Galaxy()
        {
            systems = new List<System>();
        }


        public void GenerateGalaxy(int seed, int size = 15000, float deviation = 0.0855f) //0-7
        {
            systems = new List<System>();
            // scatter spiral without central radius
            int central_radius = size / 10;
            for (int i = 0; i < size; i++)
            {
                if (Utility.IsPrime(i))
                {
                    if (i > central_radius)
                    {
                        System s = new System();
                        s.coordinates = new int[2];
                        s.coordinates[0] = (int)(i * Math.Cos((double)i / RandomGen.IntervalF(1.0f - 0.0001f * deviation, 1.0f + 0.0001f * deviation, ref seed)));
                        s.coordinates[1] = (int)(i * Math.Sin((double)i / RandomGen.IntervalF(1.0f - 0.0001f * deviation, 1.0f + 0.0001f * deviation, ref seed)));
                        s.GenerateSystem(ref seed);
                        if (Math.Sqrt(s.coordinates[0] * s.coordinates[0] + s.coordinates[1] * s.coordinates[1]) > central_radius)
                        {
                            systems.Add(s);
                            starsize++;
                        }
                       
                    }


                }

            }
        }

        public void GenerateGalaxy2(int seed, int size = 30000, float Spiral = 0.15f, float spread = 0.24f, float density = 0.27f)
        {
            systems = new List<System>();
            for (int i = 0; i < size; i++)
            {
                if (Utility.IsPrime(i) && !RandomGen.Chance((float)i/(2 * size * density), ref seed))
                {
                    float angle = i * Spiral + RandomGen.IntervalF(-spread, spread, ref seed);
                    System s = new System();
                    s.GenerateSystem(ref seed);
                    int central_radius = size / 40;
                    s.coordinates = new int[2];
                    s.coordinates[0] = (int)(i * Math.Cos(angle))/2;
                    s.coordinates[1] = (int)(i * Math.Sin(angle))/2;
                    if (Math.Sqrt(s.coordinates[0] * s.coordinates[0] + s.coordinates[1] * s.coordinates[1]) > central_radius)
                    {
                        systems.Add(s);
                        starsize++;
                    }
                }
               
            }
        }

    }
}
