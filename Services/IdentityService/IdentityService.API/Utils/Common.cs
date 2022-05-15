using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Utils
{
    public static class Common
    {
        public static bool IsGuid(string value)
        {
            return Guid.TryParse(value, out _);
        }

        public static bool CompareByteArrays(byte[] arr1, byte[] arr2)
        {
            return StructuralComparisons.StructuralEqualityComparer.Equals(arr1, arr2);
        }
    }
}
