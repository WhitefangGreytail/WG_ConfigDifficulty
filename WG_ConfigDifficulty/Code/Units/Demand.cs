using System;
using System.IO;
using System.Reflection;
using ICities;
using UnityEngine;

namespace WG_ConfigDifficulty
{
    public class ConfigDemand : DemandExtensionBase
    {

        public override int OnCalculateResidentialDemand(int originalDemand)
        {
            return DataStore.calcObjects[DataStore.DEMAND_RES].calculateReturnValue(originalDemand);
        }

        public override int OnCalculateCommercialDemand(int originalDemand)
        {
            return DataStore.calcObjects[DataStore.DEMAND_COM].calculateReturnValue(originalDemand);

        }

        public override int OnCalculateWorkplaceDemand(int originalDemand)
        {
            return DataStore.calcObjects[DataStore.DEMAND_IND].calculateReturnValue(originalDemand);
        }

    }
}
