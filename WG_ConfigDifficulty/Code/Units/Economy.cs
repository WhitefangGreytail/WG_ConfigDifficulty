using System;
using System.IO;
using System.Reflection;
using ICities;
using UnityEngine;

namespace WG_ConfigDifficulty
{
    public class ConfigEconomy : EconomyExtensionBase
    {
        public override int OnGetConstructionCost(int originalConstructionCost, Service service, SubService subService, Level level)
        {
            try
            {
                // This value is used as the input for relocation and for refund
                return DataStore.calcObjects[DataStore.CONSTRUCT].calculateReturnValue(originalConstructionCost);
            }
#pragma warning disable
            catch (Exception e)
#pragma warning enable
            {
                return originalConstructionCost;
            }
        }

        public override int OnGetMaintenanceCost(int originalMaintenanceCost, Service service, SubService subService, Level level)
        {
            try
            {
                return DataStore.calcObjects[DataStore.MAINT].calculateReturnValue(originalMaintenanceCost);
            }
#pragma warning disable
            catch (Exception e)
#pragma warning enable
            {
                return originalMaintenanceCost;
            }
        }

        public override int OnGetRelocationCost(int constructionCost, int relocationCost, Service service, SubService subService, Level level)
        {
            try
            {
               return DataStore.calcObjects[DataStore.RELOC].calculateReturnValue(constructionCost);
            }
#pragma warning disable
            catch (Exception e)
#pragma warning enable
            {
                return relocationCost;
            }
        }

        public override int OnGetRefundAmount(int constructionCost, int refundAmount, Service service, SubService subService, Level level)
        {
            try
            {
                return DataStore.calcObjects[DataStore.REFUND].calculateReturnValue(constructionCost);
            }
#pragma warning disable
            catch (Exception e)
#pragma warning enable
            {
                return refundAmount;
            }
        }

    }
}
