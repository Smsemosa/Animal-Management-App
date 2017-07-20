using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
  public  enum AnimalTypeEnum
    {
        Cattle=1,
        Sheep,
        Goats,
        Pig,
        Horse,

    }
  public enum GenderEnum
  {
      Male = 1,
      Female,
      Not_Sure

  }
  public enum StatusEnum
  {
      Alive = 1,
      Dead,
      Sold,
      Borrowed
  }
  public enum FrameSize
  { 
      Small=1,Medium,Large,XLarge
  }

  public enum BreedingStatus
  {
      Mating = 1, Recovery_After_Mating, Culled,Castrated,Breeding
  }


}
