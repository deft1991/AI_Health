// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("uR+lsCsQX5DieKEgr18eqnvwGPXbWFZZadtYU1vbWFhZ5sYPg0W88JHseLtXfJGuodIl0PNkp5eai6P6RSihIAKojMMcCxi00Wvn+lCud8qX9ZSzhvZyvB1E6EDADuAy6lSV1RBunCcWxUOwDCCu9c7Z9sE0LNYo8TVM2o4M/1YI+qpTPCNFTIG0uyIsZWxD5TO3OfnFRifXLup/kLmluBnPpAXjPSgQ5kpD8tqleDQDKeYcrIcCgrnVsPW+ygmw7eFfpniVLvGakkI7vGMFieChY/dtsg9CIy/5ZLPoyeAxRxw/icWE/CJKfqDjpY8cadtYe2lUX1Bz3xHfrlRYWFhcWVqBvij5O9F0IIdSsAdyVhAW6fTQXmRT8JVyEC8nRFtaWFlY");
        private static int[] order = new int[] { 13,1,12,10,7,11,12,8,8,11,12,11,13,13,14 };
        private static int key = 89;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
